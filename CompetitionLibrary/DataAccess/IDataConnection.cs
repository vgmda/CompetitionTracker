using CompetitionLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionLibrary.DataAccess

{
    public interface IDataConnection
    {
        Prize CreatePrize(Prize model);
    }
}
