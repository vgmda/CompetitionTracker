using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionLibrary.DataAccess.TextConnector
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
    }
}
