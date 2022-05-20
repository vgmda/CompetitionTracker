﻿using CompetitionLibrary.Models;

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
