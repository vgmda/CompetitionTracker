using CompetitionLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int byes = 0;
        }

        private static int NumberOfByes(int roudns, int numberOfTeams)
        {

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
