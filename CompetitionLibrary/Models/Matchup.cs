using System;
namespace CompetitionLibrary.Models
{
    public class Matchup
    {

        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();

        public Team Winner { get; set; }

        public int MatchupRound { get; set; }


    }
}

