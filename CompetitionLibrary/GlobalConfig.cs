using CompetitionLibrary.DataAccess;
using System.Configuration;

namespace CompetitionLibrary
{
    public static class GlobalConfig
    {
        public const string PrizesFile = "Prize.csv";
        public const string PeopleFile = "Person.csv";
        public const string TeamFile = "Team.csv";
        public const string CompetitionFile = "Competition.csv";
        public const string MatchupFile = "Matchup.csv";
        public const string MatchupEntryFile = "MatchupEntry.csv";

        public static IDataConnection Connection { get; private set; }
        public static void InitializeConnections(DatabaseType db)
        {

            if (db == DatabaseType.Sql)
            {
                // TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }

            else if (db == DatabaseType.TextFile)
            {
                // TODO - Create the Text Connection
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }
        // Connection string value from App.config
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }

}
