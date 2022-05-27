using CompetitionLibrary.Models;
using Dapper;
using System.Data;


namespace CompetitionLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "Competitions";

        public void CreatePerson(Person model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@MobileNumber", model.MobileNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }
        }

        // TODO - Make the CreatePrize() method actually save to the database - DONE!
        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public void CreatePrize(Prize model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }

        }

        public void CreateTeam(Team model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                foreach (Person tm in model.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", model.Id);
                    p.Add("@PersonId", tm.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);

                }
            }

        }

        public void CreateCompetition(Competition model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                SaveCompetition(connection, model);

                SaveCompetitionPrizes(connection, model);

                SaveCompetitionEntries(connection, model);

                SaveCompetitionRounds(connection, model);

            }
        }

        private void SaveCompetition(IDbConnection connection, Competition model)
        {
            // dbo.spCompetitions_Insert
            var p = new DynamicParameters();
            p.Add("@CompetitionName", model.CompetitionName);
            p.Add("@EntryFee", model.EntryFee);
            p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            connection.Execute("dbo.spCompetitions_Insert", p, commandType: CommandType.StoredProcedure);

            model.Id = p.Get<int>("@id");
        }

        private void SaveCompetitionPrizes(IDbConnection connection, Competition model)
        {
            // dbo.spCompetitionPrizes_Insert
            foreach (Prize pz in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@CompetitionId", model.Id);
                p.Add("@PrizeId", pz.Id);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCompetitionPrizes_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        private void SaveCompetitionEntries(IDbConnection connection, Competition model)
        {
            // dbo.spCompetitionEntries_Insert
            foreach (Team tm in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@CompetitionId", model.Id);
                p.Add("@TeamId", tm.Id);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spCompetitionEntries_Insert", p, commandType: CommandType.StoredProcedure);

            }
        }

        private void SaveCompetitionRounds(IDbConnection connection, Competition model)
        {
            // List<List<Matchup>> Rounds
            // List<MatchupEntry> Entries

            // Loop through the rounds
            // Loop through the matchups
            // Save the matchup
            // Loop through the entries and save them

            // The best code is the code which is simplistic enough for even the newer developer to understand..
            // ..and yet powerful enough to get the job done.
            // var can be used instead of List<Matchup>
            foreach (List<Matchup> round in model.Rounds)
            {
                foreach (Matchup matchup in round)
                {
                    var p = new DynamicParameters();
                    p.Add("@CompetitionId", model.Id);
                    p.Add("@MatchupRound", matchup.MatchupRound);
                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);

                    matchup.Id = p.Get<int>("@id");

                    foreach (MatchupEntry entry in matchup.Entries)
                    {
                        p = new DynamicParameters();

                        p.Add("@MatchupId", matchup.Id);

                        if (entry.ParentMatchup == null)
                        {
                            p.Add("@ParentMatchupId", null);
                        }
                        else
                        {
                            p.Add("@ParentMatchupId", entry.ParentMatchup.Id);
                        }
                        if (entry.TeamCompeting == null)
                        {
                            p.Add("@TeamCompetingId", null);
                        }
                        else
                        {
                            p.Add("@TeamCompetingId", entry.TeamCompeting.Id);
                        }
                        p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spMatchupEntries_Insert", p, commandType: CommandType.StoredProcedure);

                    }

                }
            }

        }

        public List<Person> GetPerson_All()
        {
            List<Person> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<Person>("dbo.spPeople_GetAll").ToList();
            }

            return output;

        }

        public List<Team> GetTeam_All()
        {
            List<Team> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<Team>("dbo.spTeam_GetAll").ToList();

                foreach (Team team in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TeamId", team.Id);
                    team.TeamMembers = connection.Query<Person>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }

            }

            return output;
        }

        public List<Competition> GetCompetition_All()
        {
            List<Competition> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<Competition>("dbo.spCompetitions_GetAll").ToList();
                var p = new DynamicParameters();

                foreach (Competition c in output)
                {
                    // Populate prizes
                    p = new DynamicParameters();
                    p.Add("@CompetitionId", c.Id);

                    c.Prizes = connection.Query<Prize>("dbo.spPrizes_GetByCompetition", p, commandType: CommandType.StoredProcedure).ToList();

                    // Populate Teams
                    p = new DynamicParameters();
                    p.Add("@CompetitionId", c.Id);
                    c.EnteredTeams = connection.Query<Team>("dbo.spTeam_GetByCompetition", p, commandType: CommandType.StoredProcedure).ToList();

                    foreach (Team team in c.EnteredTeams)
                    {
                        p = new DynamicParameters();
                        p.Add("@TeamId", team.Id);

                        team.TeamMembers = connection.Query<Person>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                    }

                    // Populate Rounds
                    p = new DynamicParameters();
                    p.Add("@CompetitionId", c.Id);
                    List<Matchup> matchups = connection.Query<Matchup>("dbo.spMatchups_GetByCompetition", p, commandType: CommandType.StoredProcedure).ToList();

                    foreach (Matchup m in matchups)
                    {
                        p = new DynamicParameters();
                        p.Add("@MatchupId", m.Id);
                        m.Entries = connection.Query<MatchupEntry>("dbo.spMatchupEntries_GetByMatchup", p, commandType: CommandType.StoredProcedure).ToList();

                        List<Team> allTeams = GetTeam_All();

                        if (m.WinnerId > 0)
                        {
                            m.Winner = allTeams.Where(x => x.Id == m.WinnerId).First();
                        }

                        foreach (var me in m.Entries)
                        {
                            if (me.TeamCompetingId > 0)
                            {
                                me.TeamCompeting = allTeams.Where(x => x.Id == me.TeamCompetingId).First();
                            }

                            if (me.ParentMatchupId > 0)
                            {
                                me.ParentMatchup = matchups.Where(x => x.Id == me.ParentMatchupId).First();
                            }
                        }

                    }

                    // List<List<Matchup>>
                    List<Matchup> currentRow = new List<Matchup>();
                    int currentRound = 1;

                    foreach (Matchup m in matchups)
                    {
                        if (m.MatchupRound > currentRound)
                        {
                            c.Rounds.Add(currentRow);
                            currentRow = new List<Matchup>();
                            currentRound += 1;
                        }

                        currentRow.Add(m);
                    }

                    c.Rounds.Add(currentRow);
                }

            }

            return output;
        }

        public void UpdateMatchup(Matchup model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                // dbo.spMatchups_Update = @id, @WinnerId
                var p = new DynamicParameters();
                if (model.Winner != null)
                {
                    p.Add("@id", model.Id);
                    p.Add("@WinnerId", model.Winner.Id);

                    connection.Execute("dbo.spMatchups_Update", p, commandType: CommandType.StoredProcedure);
                }

                // dbo.spMatchupEntries_Update = @id, @TeamCompetingId, @Score
                foreach (MatchupEntry me in model.Entries)
                {
                    if (me.TeamCompeting != null)
                    {
                        p = new DynamicParameters();
                        p.Add("@id", me.Id);
                        p.Add("@TeamCompetingId", me.TeamCompeting.Id);
                        p.Add("@Score", me.Score);

                        connection.Execute("dbo.spMatchupEntries_Update", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }
    }
}
