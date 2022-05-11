using CompetitionLibrary.Models;

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
