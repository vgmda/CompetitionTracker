using CompetitionLibrary.Models;
using System.Configuration;

namespace CompetitionLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) // fileName = Prize.csv
        {

            // C:\Users\vasile.gancin\Documents\CompetitionTracker\Prize.csv
            return $"{ConfigurationManager.AppSettings["filePath"] }\\{ fileName } ";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<Prize> ConvertToPrize(this List<string> lines)
        {
            List<Prize> output = new List<Prize>();

            foreach (string line in lines)
            {
                // cols = columns
                string[] cols = line.Split(',');
                Prize p = new Prize();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }

            return output;
        }

        public static List<Person> ConvertToPerson(this List<string> lines)
        {
            List<Person> output = new List<Person>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                Person p = new Person();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.MobileNumber = cols[4];
                output.Add(p);


            }

            return output;

        }

        public static void SaveToPrizeFile(this List<Prize> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (Prize p in models)
            {
                lines.Add($"{ p.Id },{ p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
