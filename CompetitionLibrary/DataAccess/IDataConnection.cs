using CompetitionLibrary.Models;

namespace CompetitionLibrary.DataAccess

{
    public interface IDataConnection
    {
        void CreatePrize(Prize model);
        void CreatePerson(Person model);
        void CreateTeam(Team model);
        void CreateCompetition(Competition model);
        void UpdateMatchup(Matchup model);
        List<Person> GetPerson_All();
        List<Team> GetTeam_All();
        List<Competition> GetCompetition_All();

    }
}
