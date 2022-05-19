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
        }

        private static int (int teamCount)
        {
            int output = 1;
            // 
            int val = 2;

            while (true)
            {

            }

        }

        private static List<Team> RandomizeTeamOrder(List<Team> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
