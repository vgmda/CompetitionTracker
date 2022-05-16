using CompetitionLibrary;
using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CreateCompetitionForm : Form, IPrizeRequester, ITeamRequester
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

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // Call the CreatePrizeForm() class when the button is pressed
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        public void PrizeComplete(Prize model)
        {
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(Team model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void createCompetitionButton_Click(object sender, EventArgs e)
        {

        }

        
    }

}
