﻿using CompetitionLibrary.Models;

namespace CompetitionLibrary.DataAccess

{
    public interface IDataConnection
    {
        Prize CreatePrize(Prize model);
        Person CreatePerson(Person model);
        Team CreateTeam(Team model);
        Competition CreateCompetition(Competition model);
        List<Person> GetPerson_All();
        List<Team> GetTeam_All();
    }
}
