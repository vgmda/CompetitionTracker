using CompetitionLibrary.Models;
using System.Configuration;

namespace CompetitionLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) // fileName = Prize.csv
        {

            // C:\Users\vasile.gancin\Documents\CompetitionTracker\Prize.csv
            return $"{ConfigurationManager.AppSettings["filePath"] }\\{ fileName } ";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
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

                string[] prizeIds = cols[4].Split('|');

                foreach (string id in prizeIds)
                {
                    comp.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }

                // TODO - Capture Rounds information

                output.Add(comp);
            }

            return output;
        }

        public static void SaveToPrizeFile(this List<Prize> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (Prize p in models)
            {
                lines.Add($"{ p.Id },{ p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }


        public static void SaveToPeopleFile(this List<Person> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (Person p in models)
            {
                lines.Add($"{ p.Id },{ p.FirstName },{ p.LastName },{ p.EmailAddress },{ p.MobileNumber }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<Team> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Team t in models)
            {
                lines.Add($"{ t.Id },{ t.TeamName },{ ConvertPeopleListToString(t.TeamMembers) }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToCompetitionFile(this List<Competition> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Competition comp in models)
            {
                lines.Add($@"{ comp.Id },
                    { comp.CompetitionName },
                    { comp.EntryFee },
                    { ConvertTeamListToString(comp.EnteredTeams) },
                    { ConvertPrizeListToString(comp.Prizes) }");
            }

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
                output += $"{ p.Id }|";
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
                output += $"{ t.Id }|";
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
                output += $"{ p.Id }|";
            }

            // Removing the trailing pipe character
            output = output.Substring(0, output.Length - 1);

            return output;
        }
    }
}
