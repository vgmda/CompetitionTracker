using System;
namespace CompetitionLibrary
{
    public class MatchupEntry
    {
        /// <summary>
        /// Represents one team in the match-up.
        /// </summary>
        public Team TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular team.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the match-up that this team came
        /// from as the winner.
        /// </summary>
        public Matchup ParentMatchup { get; set; }

        

    }
}

