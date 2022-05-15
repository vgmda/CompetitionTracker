using CompetitionLibrary;
using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CreateCompetitionForm : Form
    {
        List<Team> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<Team> selectedTeams = new List<Team>();
        List<Prize> selectedPrizes = new List<Prize>();

        public CreateCompetitionForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";


            competitionTeamsListBox.DataSource = null;
            competitionTeamsListBox.DataSource = selectedTeams;
            competitionTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            Team t = (Team)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }
    }
}
