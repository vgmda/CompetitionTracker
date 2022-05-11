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


            
            // Find the max ID
            // Add the new record with the new ID (max + 1)
            // Convert the prizes to list<string>
            // Save the list<string> to the text file



        }
    }
}
