using CompetitionLibrary.Models;
using CompetitionLibrary.DataAccess.TextHelpers;

namespace CompetitionLibrary.DataAccess

{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "Prize.csv";


        // TODO - Write up the createPrize for text files
        public Prize CreatePrize(Prize model)
        {
            // Load the text file and convert the text to List<Prize>
            List<Prize> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrize();


            
            // Convert the prizes to list<string>
            // Save the list<string> to the text file

            // Find the max ID 
            int currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            model.Id = currentId;
            // Add the new record with the new ID (max + 1)
            prizes.Add(model);


        }
    }
}
