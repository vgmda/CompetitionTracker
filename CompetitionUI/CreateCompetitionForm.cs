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
        private void removeSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            Team t = (Team)competitionTeamsListBox.SelectedItem;

            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);
                WireUpLists();
            }
        }
        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            Prize p = (Prize)competitionTeamsListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);
                WireUpLists();

            }

        }

        private void createCompetitionButton_Click(object sender, EventArgs e)
        {
            // Validate data
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid Entry Fee.",
                    "Invalid Fee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Create Competition model
            Competition comp = new Competition();

            comp.CompetitionName = competitionNameValue.Text;
            comp.EntryFee = fee;
            comp.Prizes = selectedPrizes;
            comp.EnteredTeams = selectedTeams;

            // Wire up the matchups
            CompetitionLogic.CreateRounds(comp);

            // Create Competition entry
            // Create all of the prizes entries
            // Create all of the team entries
            GlobalConfig.Connection.CreateCompetition(comp);

        }

    }

}
