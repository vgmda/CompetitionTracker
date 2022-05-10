using CompetitionLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionLibrary.DataAccess

{
    public class TextConnector : IDataConnection
    {
        // TODO - Write up the createPrize for text files
        public Prize CreatePrize(Prize model)
        {
            model.Id = 1;
           
            return model;
        }
    }
}
