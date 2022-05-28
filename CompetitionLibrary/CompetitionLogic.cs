using CompetitionLibrary.Models;
using System.Configuration;

namespace CompetitionLibrary
{
    public static class CompetitionLogic
    {
        // Order the list randomly of teams
        // Check if it is big enough - if not, add in byes - 2(teams)*2(teams)*2(teams)*2(teams) = 2^4
        // Create first round of matchups
        // Create every round after that - 8 matchups > 4 matchups > 2 matchups > 1 matchup
        public static void CreateRounds(Competition model)
        {
            List<Team> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
            int byes = NumberOfByes(rounds, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));

            CreateOtherRounds(model, rounds);
        }
        public static void UpdateCompetitionResults(Competition model)
        {
            List<Matchup> toScore = new List<Matchup>();

            foreach (List<Matchup> round in model.Rounds)
            {
                foreach (Matchup rm in round)
                {
                    // rm.Entries.Count == 1, if this is a bye entry
                    if (rm.Winner == null && (rm.Entries.Any(x =>x.Score != 0) || rm.Entries.Count == 1))
                    {
                        toScore.Add(rm);
                    }
                }
            }

            MarkWinnersInMatchups(toScore);

            AdvanceWinners(toScore, model);

            // Call Sql update method
            // GlobalConfig.Connection.UpdateMatchup(m);
            toScore.ForEach(x => GlobalConfig.Connection.UpdateMatchup(x));
            

        }
        private static void AdvanceWinners(List<Matchup> models, Competition competition)
        {
            foreach (Matchup m in models)
            {
                foreach (List<Matchup> round in competition.Rounds)
                {
                    // rm = Round Matchup
                    foreach (Matchup rm in round)
                    {
                        foreach (MatchupEntry me in rm.Entries)
                        {
                            if (me.ParentMatchup != null)
                            {
                                if (me.ParentMatchup.Id == m.Id)
                                {
                                    me.TeamCompeting = m.Winner;
                                    GlobalConfig.Connection.UpdateMatchup(rm);
                                }
                            }
                        }
                    }
                } 
            }
        }
        private static void MarkWinnersInMatchups(List<Matchup> models)
        {
            // greater or lesser
            string greaterWins = ConfigurationManager.AppSettings["greaterWins"];

            foreach (Matchup m in models)
            {
                // Checks for bye week entry
                if (m.Entries.Count == 1)
                {
                    m.Winner = m.Entries[0].TeamCompeting;
                    // continue's job is to continue this foreach at the next iteration
                    continue;
                }

                // 0 means false, or low score wins
                if (greaterWins == "0")
                {
                    // Entry on the left is the winner
                    if (m.Entries[0].Score < m.Entries[1].Score)
                    {
                        m.Winner = m.Entries[0].TeamCompeting;
                    }
                    // Entry on the left is the winner
                    else if (m.Entries[1].Score < m.Entries[0].Score)
                    {
                        m.Winner = m.Entries[1].TeamCompeting;
                    }
                    else
                    {
                        throw new Exception("ERROR: Tie games are not handled");
                    }
                }
                else
                {
                    // 1 means true, or high score wins
                    if (m.Entries[0].Score > m.Entries[1].Score)
                    {
                        m.Winner = m.Entries[0].TeamCompeting;
                    }
                    else if (m.Entries[1].Score > m.Entries[0].Score)
                    {
                        m.Winner = m.Entries[1].TeamCompeting;
                    }
                    else
                    {
                        throw new Exception("ERROR: Tie games are not handled");
                    }
                } 
            }

            //if (teamOneScore > teamTwoScore)
            //{
            //    // Team one wins
            //    m.Winner = m.Entries[0].TeamCompeting;
            //}
            //else if (teamTwoScore > teamOneScore)
            //{
            //    // Team two wins
            //    m.Winner = m.Entries[1].TeamCompeting;
            //}
            //else
            //{
            //    MessageBox.Show("ERROR: Tie games are not handled");
            //}


        }
        private static void CreateOtherRounds(Competition model, int rounds)
        {
            int round = 2;
            List<Matchup> previousRound = model.Rounds[0];
            List<Matchup> currentRound = new List<Matchup>();
            Matchup currentMatchup = new Matchup();

            // round = current round
            // rounds = total number of rounds
            while (round <= rounds)
            {
                foreach (Matchup match in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntry { ParentMatchup = match });

                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new Matchup();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;
                currentRound = new List<Matchup>();
                round += 1;

            }
        }
        private static List<Matchup> CreateFirstRound(int byes, List<Team> teams)
        {
            List<Matchup> output = new List<Matchup>();
            Matchup current = new Matchup();

            foreach (Team team in teams)
            {
                current.Entries.Add(new MatchupEntry { TeamCompeting = team });

                // If there is a bye available
                if (byes > 0 || current.Entries.Count > 1)
                {
                    // Always 1 as this is the first round
                    current.MatchupRound = 1;
                    output.Add(current);
                    current = new Matchup();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }
            }

            return output;

        }
        private static int NumberOfByes(int rounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            // 1 based counting system
            // Counting rounds starting at 1
            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;

            return output;

        }
        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            // val = valuation
            int val = 2;

            // Check if team count is bigger than val count
            // If teamCount is 2
            // Then this will never execute and will return 1 round
            while (val < teamCount)
            {
                output += 1;
                val *= 2;

            }

            // Return 1 round
            return output;

        }
        private static List<Team> RandomizeTeamOrder(List<Team> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
