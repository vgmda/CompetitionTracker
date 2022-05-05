using System;
namespace CompetitionLibrary
{
    public class Matchup
    {
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();

        public Team Winner { get; set; }

        public int MatchupRound { get; set; }


    }
}

