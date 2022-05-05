using System;
namespace CompetitionLibrary
{
    public class MatchupEntry
    {
        /// <summary>
        /// Reprsents one team in the matchup.
        /// </summary>
        public Team TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particual team.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup that this team came
        /// from as the winner.
        /// </summary>
        public Matchup ParentMatchup { get; set; }


    }
}

