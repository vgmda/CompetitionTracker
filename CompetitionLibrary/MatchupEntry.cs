using System;
namespace CompetitionLibrary
{
    public class MatchupEntry
    {
        public Team TeamCompeting { get; set; }

        public double Score { get; set; }

        public Matchup ParentMatchup { get; set; }


    }
}

