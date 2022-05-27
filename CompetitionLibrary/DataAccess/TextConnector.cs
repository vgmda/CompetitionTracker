﻿using CompetitionLibrary.DataAccess.TextHelpers;
using CompetitionLibrary.Models;

namespace CompetitionLibrary.DataAccess

{
    public class TextConnector : IDataConnection
    {
        public void CreatePerson(Person model)
        {
            List<Person> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPerson();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            people.Add(model);
            people.SaveToPeopleFile(GlobalConfig.PeopleFile);
        }

        public void CreatePrize(Prize model)
        {
            // Load the text file and convert the text to List<Prize>
            List<Prize> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrize();

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
            prizes.SaveToPrizeFile(GlobalConfig.PrizesFile);
        }

        public List<Person> GetPerson_All()
        {
            // Methods already implemented, when returned, it will load and read all people from the file
            return GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPerson();
        }

        public void CreateTeam(Team model)
        {
            List<Team> teams = GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeam(GlobalConfig.PeopleFile);

            // Find the max ID 
            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            teams.Add(model);
            teams.SaveToTeamFile(GlobalConfig.TeamFile);
        }

        public List<Team> GetTeam_All()
        {
            return GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeam(GlobalConfig.PeopleFile);
        }

        public void CreateCompetition(Competition model)
        {
            List<Competition> competitions = GlobalConfig.CompetitionFile
                .FullFilePath()
                .LoadFile()
                .ConvertToCompetition(GlobalConfig.TeamFile, GlobalConfig.PeopleFile, GlobalConfig.PrizesFile);

            int currentId = 1;

            if (competitions.Count > 0)
            {
                currentId = competitions.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            model.SaveRoundsToFile(GlobalConfig.MatchupFile, GlobalConfig.MatchupEntryFile);
            competitions.Add(model);
            competitions.SaveToCompetitionFile(GlobalConfig.CompetitionFile);
        }

        public List<Competition> GetCompetition_All()
        {
            return GlobalConfig.CompetitionFile
                .FullFilePath()
                .LoadFile()
                .ConvertToCompetition(GlobalConfig.TeamFile, GlobalConfig.PeopleFile, GlobalConfig.PrizesFile);
        }

        public void UpdateMatchup(Matchup model)
        {
            model.UpdateMatchupToFile();
        }
    }
}
