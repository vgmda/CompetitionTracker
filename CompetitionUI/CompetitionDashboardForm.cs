using CompetitionLibrary;
using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CompetitionDashboardForm : Form
    {
        List<Competition> competitions = GlobalConfig.Connection.GetCompetition_All();

        public CompetitionDashboardForm()
        {
            InitializeComponent();

            WireUpLists();

        }

        private void WireUpLists()
        {
            loadExistingCompetitionDropDown.DataSource = competitions;
            loadExistingCompetitionDropDown.DisplayMember = "CompetitionName";
        }

        private void createCompetitionButton_Click_1(object sender, EventArgs e)
        {
            CreateCompetitionForm frm = new CreateCompetitionForm();
            frm.Show();

        }

        private void loadCompetitionButton_Click(object sender, EventArgs e)
        {
            Competition comp = (Competition)loadExistingCompetitionDropDown.SelectedItem;
            CompetitionViewerForm frm = new CompetitionViewerForm(comp);
            frm.Show();
        }
    }
}
