using CompetitionLibrary.DataAccess.TextHelpers;
using CompetitionLibrary.Models;

namespace CompetitionLibrary.DataAccess

{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "Prize.csv";
        private const string PeopleFile = "Person.csv";
        private const string TeamFile = "Team.csv";
        private const string CompetitionFile = "CompetitionModels.csv";

        public Person CreatePerson(Person model)
        {
            List<Person> people = PeopleFile.FullFilePath().LoadFile().ConvertToPerson();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            people.Add(model);
            people.SaveToPeopleFile(PeopleFile);

            return model;
        }


        // TODO - Write up the createPrize for text files
        public Prize CreatePrize(Prize model)
        {
            // Load the text file and convert the text to List<Prize>
            List<Prize> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrize();

            // Find the max ID 
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            // Add the new record with the new ID (max + 1)
            prizes.Add(model);

            // Convert the prizes to list<string>
            // Save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }


        public List<Person> GetPerson_All()
        {
            // Methods already implemented, when returned, it will load and read all people from the file
            return PeopleFile.FullFilePath().LoadFile().ConvertToPerson();
        }

        public Team CreateTeam(Team model)
        {
            List<Team> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeam(PeopleFile);

            // Find the max ID 
            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            teams.Add(model);
            teams.SaveToTeamFile(TeamFile);

            return model;


        }
        public List<Team> GetTeam_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeam(PeopleFile);
        }

        public Competition CreateCompetition(Competition model)
        {
            List<Competition> competitions = CompetitionFile.FullFilePath().LoadFile().ConvertToCompetitionModels();
        }
    }
}
