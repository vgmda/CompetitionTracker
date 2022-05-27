using CompetitionLibrary.Models;
using System.Configuration;

namespace CompetitionLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) // fileName = Prize.csv
        {

            // C:\Users\vasile.gancin\Documents\CompetitionTracker\Prize.csv
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<Competition> ConvertToCompetition(
        this List<string> lines,
        string teamFileName,
        string peopleFileName,
        string prizeFileName)
        {
            // id = cols[0]
            // CompetitionName = cols[1]
            // EntryFee = cols[2]
            // EnteredTeams = cols[3]
            // Prizes = cols[4]
            // Rounds = cols[5]
            // id,CompetitionName,EntryFee,(id|id|id - Entered Teams),(id|id|id - Prizes),(Rounds - id^id^id|id^id^id|id^id^id)
            List<Competition> output = new List<Competition>();
            List<Team> teams = teamFileName.FullFilePath().LoadFile().ConvertToTeam(peopleFileName);
            List<Prize> prizes = prizeFileName.FullFilePath().LoadFile().ConvertToPrize();
            List<Matchup> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchup();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                Competition comp = new Competition();
                // id,CompetitionName,EntryFee
                comp.Id = int.Parse(cols[0]);
                comp.CompetitionName = cols[1];
                comp.EntryFee = decimal.Parse(cols[2]);


                string[] teamIds = cols[3].Split('|');

                foreach (string id in teamIds)
                {
                    comp.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }

                if (cols[4].Length > 0)
                {
                    string[] prizeIds = cols[4].Split('|');

                    foreach (string id in prizeIds)
                    {
                        comp.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                    }
                }

                // Capture Rounds information
                string[] rounds = cols[5].Split('|');


                foreach (string round in rounds)
                {
                    string[] msText = round.Split('^');
                    List<Matchup> ms = new List<Matchup>();

                    foreach (string matchupModelTextId in msText)
                    {
                        ms.Add(matchups.Where(x => x.Id == int.Parse(matchupModelTextId)).First());
                    }
                    comp.Rounds.Add(ms);
                }

                output.Add(comp);
            }

            return output;
        }

        public static List<Prize> ConvertToPrize(this List<string> lines)
        {
            List<Prize> output = new List<Prize>();

            foreach (string line in lines)
            {
                // cols = columns
                string[] cols = line.Split(',');
                Prize p = new Prize();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }

            return output;
        }

        public static List<Person> ConvertToPerson(this List<string> lines)
        {
            List<Person> output = new List<Person>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                Person p = new Person();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.MobileNumber = cols[4];
                output.Add(p);

            }

            return output;

        }

        public static List<Team> ConvertToTeam(this List<string> lines, string peopleFileName)
        {
            // Id, Team Name, List of Ids sparated by the pipe
            // 3,Test's Team,1|2|3

            List<Team> output = new List<Team>();
            List<Person> people = peopleFileName.FullFilePath().LoadFile().ConvertToPerson();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                Team t = new Team();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];


                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }

                output.Add(t);

            }

            return output;

        }

        public static List<MatchupEntry> ConvertToMatchupEntry(this List<string> lines)
        {
            // id=0,TeamCompeting=1,Score=2,ParentMatchup=3
            List<MatchupEntry> output = new List<MatchupEntry>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                MatchupEntry me = new MatchupEntry();
                me.Id = int.Parse(cols[0]);
                if (cols[1].Length == 0)
                {
                    me.TeamCompeting = null;
                }
                else
                {
                    me.TeamCompeting = LookupTeamById(int.Parse(cols[1]));
                }

                me.Score = double.Parse(cols[2]);
                int parentId = 0;
                if (int.TryParse(cols[3], out parentId))
                {
                    me.ParentMatchup = LookupMatchupById(parentId);
                }
                else
                {
                    me.ParentMatchup = null;
                }

                output.Add(me);
            }

            return output;
        }

        private static List<MatchupEntry> ConvertStringToMatchupEntry(string input)
        {
            string[] ids = input.Split('|');
            List<MatchupEntry> output = new List<MatchupEntry>();
            List<string> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile();
            List<string> matchingEntries = new List<string>();

            foreach (string id in ids)
            {
                foreach (string entry in entries)
                {
                    string[] cols = entry.Split(',');

                    if (cols[0] == id)
                    {
                        matchingEntries.Add(entry);
                    }
                }
            }

            output = matchingEntries.ConvertToMatchupEntry();

            return output;
        }

        public static List<Matchup> ConvertToMatchup(this List<string> lines)
        {
            // id=0,entries=1(pipe delimite by Id),winner=2,matchupRound=3
            List<Matchup> output = new List<Matchup>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                Matchup p = new Matchup();
                p.Id = int.Parse(cols[0]);
                p.Entries = ConvertStringToMatchupEntry(cols[1]);
                if (cols[2].Length == 0)
                {
                    p.Winner = null;
                }
                else
                {
                    p.Winner = LookupTeamById(int.Parse(cols[2]));
                }

                p.MatchupRound = int.Parse(cols[3]);
                output.Add(p);
            }

            return output;
        }

        private static string ConvertRoundListToString(List<List<Matchup>> rounds)
        {
            // (Rounds - id^id^id|id^id^id|id^id^id)
            string output = "";

            if (rounds.Count == 0)
            {
                return "";
            }

            foreach (List<Matchup> r in rounds)
            {
                output += $"{ConvertMatchupListToString(r)}|";
            }

            // Removing the trailing pipe character
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertMatchupListToString(List<Matchup> matchups)
        {
            string output = "";

            if (matchups.Count == 0)
            {
                return "";
            }

            foreach (Matchup m in matchups)
            {
                output += $"{m.Id}^";
            }

            // Removing the trailing pipe character
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertPrizeListToString(List<Prize> prizes)
        {
            string output = "";

            if (prizes.Count == 0)
            {
                return "";
            }

            foreach (Prize p in prizes)
            {
                output += $"{p.Id}|";
            }

            // Removing the trailing pipe character
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertTeamListToString(List<Team> teams)
        {
            string output = "";

            if (teams.Count == 0)
            {
                return "";
            }

            foreach (Team t in teams)
            {
                output += $"{t.Id}|";
            }

            // Removing the trailing pipe character
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertPeopleListToString(List<Person> people)
        {
            string output = "";

            if (people.Count == 0)
            {
                return "";
            }

            foreach (Person p in people)
            {
                output += $"{p.Id}|";
            }

            // Removing the trailing pipe character
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        private static string ConvertMatchupEntryListToString(List<MatchupEntry> entries)
        {
            string output = "";

            if (entries.Count == 0)
            {
                return "";
            }

            foreach (MatchupEntry e in entries)
            {
                output += $"{e.Id}|";
            }

            output = output.Substring(0, output.Length - 1);

            return output;
        }

        public static void SaveToPrizeFile(this List<Prize> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (Prize p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<Person> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (Person p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.MobileNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<Team> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Team t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToCompetitionFile(this List<Competition> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Competition comp in models)
            {
                lines.Add($"{comp.Id},{comp.CompetitionName},{comp.EntryFee},{ConvertTeamListToString(comp.EnteredTeams)},{ConvertPrizeListToString(comp.Prizes)},{ConvertRoundListToString(comp.Rounds)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);

        }

        public static void SaveRoundsToFile(this Competition model, string matchupFile, string matchupEntryFile)
        {
            // Loop through each Round
            // Loop through each Matchup
            // Get the Id for the new matchup and save the record
            // Loop through each Entry, get the Id and save it

            foreach (List<Matchup> round in model.Rounds)
            {
                foreach (Matchup matchup in round)
                {
                    // Load all of the matchups from the file
                    // Get the top Id and add one
                    // Store the Id
                    // Save the matchup record
                    matchup.SaveMatchupToFile(matchupFile, matchupEntryFile);
                }
            }
        }

        public static void SaveMatchupToFile(this Matchup matchup, string matchupFile, string matchupEntryFile)
        {
            List<Matchup> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchup();

            int currentId = 1;

            if (matchups.Count > 0)
            {
                currentId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
            }

            matchup.Id = currentId;

            matchups.Add(matchup);

            foreach (MatchupEntry entry in matchup.Entries)
            {
                entry.SaveEntryToFile(matchupEntryFile);
            }
            // Save to file
            List<string> lines = new List<string>();

            foreach (Matchup m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner = m.Winner.Id.ToString();
                }
                lines.Add($"{m.Id},{ConvertMatchupEntryListToString(m.Entries)},{winner},{m.MatchupRound}");
            }

            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);
        }

        public static void UpdateMatchupToFile(this Matchup matchup)
        {
            List<Matchup> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchup();
            
            foreach (Matchup m in matchups)
            {
                if (m.Id == matchup.Id)
                {
                    matchups.Remove(m);
                }
            }

            matchups.Add(matchup);

            foreach (MatchupEntry entry in matchup.Entries)
            {
                entry.SaveEntryToFile(matchupEntryFile);
            }
            // Save to file
            List<string> lines = new List<string>();

            foreach (Matchup m in matchups)
            {
                string winner = "";
                if (m.Winner != null)
                {
                    winner = m.Winner.Id.ToString();
                }
                lines.Add($"{m.Id},{ConvertMatchupEntryListToString(m.Entries)},{winner},{m.MatchupRound}");
            }

            File.WriteAllLines(GlobalConfig.MatchupFile.FullFilePath(), lines);
        }

        public static void SaveEntryToFile(this MatchupEntry entry, string matchupEntryFile)
        {
            List<MatchupEntry> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntry();

            int currentId = 1;
            if (entries.Count > 0)
            {
                currentId = entries.OrderByDescending(x => x.Id).First().Id + 1;
            }

            entry.Id = currentId;
            entries.Add(entry);

            List<string> lines = new List<string>();

            foreach (MatchupEntry e in entries)
            {
                string parent = "";
                if (e.ParentMatchup != null)
                {
                    parent = e.ParentMatchup.Id.ToString();
                }
                string teamCompeting = "";
                if (e.TeamCompeting != null)
                {
                    teamCompeting = e.TeamCompeting.Id.ToString();
                }
                lines.Add($"{e.Id},{teamCompeting},{e.Score},{parent}");
            }

            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);

        }

        private static Team LookupTeamById(int id)
        {
            List<string> teams = GlobalConfig.TeamFile.FullFilePath().LoadFile();

            foreach (string team in teams)
            {
                string[] cols = team.Split(',');
                if (cols[0] == id.ToString())
                {
                    List<string> matchingTeams = new List<string>();
                    matchingTeams.Add(team);
                    return matchingTeams.ConvertToTeam(GlobalConfig.PeopleFile).First();
                }
            }

            return null;
        }

        private static Matchup LookupMatchupById(int id)
        {
            List<string> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile();

            foreach (string matchup in matchups)
            {
                string[] cols = matchup.Split(',');
                if (cols[0] == id.ToString())
                {
                    List<string> matchingMatchups = new List<string>();
                    matchingMatchups.Add(matchup);
                    return matchingMatchups.ConvertToMatchup().First();
                }
            }

            return null;
        }


    }
}
