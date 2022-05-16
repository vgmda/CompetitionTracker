using CompetitionLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionUI
{
    public interface ITeamRequester
    {
        void TeamComplete(Team model);
    }
}
