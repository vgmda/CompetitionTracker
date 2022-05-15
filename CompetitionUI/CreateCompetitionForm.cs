using CompetitionLibrary;
using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CreateCompetitionForm : Form
    {
        List<Team> availableTeams = GlobalConfig.Connection.GetTeam_All();

        public CreateCompetitionForm()
        {
            InitializeComponent();

            InitializeLists();
        }

        private void InitializeLists()
        {
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";
        }
    }
}
