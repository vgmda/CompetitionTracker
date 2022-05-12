using CompetitionLibrary.Models;

namespace CompetitionLibrary.DataAccess

{
    public interface IDataConnection
    {
        Prize CreatePrize(Prize model);
        Person CreatePerson(Person model);
    }
}
