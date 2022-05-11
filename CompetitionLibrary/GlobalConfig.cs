using CompetitionLibrary.DataAccess;
using System.Configuration;

namespace CompetitionLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connections { get; private set; }




        public static void InitializeConnections(string connectionType)
        {
            if (connectionType == "sql")
            {
                // TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connections = sql;
            }

            else if (connectionType == "text")
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
