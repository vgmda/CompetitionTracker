namespace CompetitionLibrary.Models
{
    /// <summary>
    /// Represents one competition, with all of the rounds, matchups, prizes and outcomes
    /// </summary>
    public class Competition
    {
        public event EventHandler<DateTime> OnCompetitionComplete;
        /// <summary>
        /// The unique identifier for the competition
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name given to this competition
        /// </summary>
        public string CompetitionName { get; set; }
        /// <summary>
        /// The amount of money each team needs to put up to enter
        /// </summary>
        public decimal EntryFee { get; set; }
        /// <summary>
        /// The list of team that have been entered
        /// </summary>
        public List<Team> EnteredTeams { get; set; } = new List<Team>();
        /// <summary>
        /// The list of prizes for the various places
        /// </summary>
        public List<Prize> Prizes { get; set; } = new List<Prize>();
        /// <summary>
        /// The matchups per round
        /// </summary>
        public List<List<Matchup>> Rounds { get; set; } = new List<List<Matchup>>();
        public void CompleteCompetition()
        {
            OnCompetitionComplete?.Invoke(this, DateTime.Now);
        }

    }
}

