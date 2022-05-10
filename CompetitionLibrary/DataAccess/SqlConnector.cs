using CompetitionLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        // TODO - Make the CreatePrize() method actually save to the database
        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier</returns>
        public Prize CreatePrize(Prize model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournamets"))
            {

            }
        }
    }
}
