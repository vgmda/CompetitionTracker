using CompetitionLibrary.DataAccess;
using System.Configuration;

namespace CompetitionLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connections { get; private set; }




        public static void InitializeConnections(DatabaseType db)
        {
            switch (db)
            {
                case DatabaseType.Sql:
                    break;
                case DatabaseType.TextFile:
                    break;
                default:
                    break;
            }


            if (db == DatabaseType.Sql)
            {
                // TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connections = sql;
            }

            else if (db == DatabaseType.TextFile)
            {
                // TODO - Create the Text Connection
                TextConnector text = new TextConnector();
                Connections = text;
            }
        }


        // Connection string value from App.config
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }



    
}
