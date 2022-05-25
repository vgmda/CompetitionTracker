using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CompetitionViewerForm : Form
    {
        private Competition competition;

        public CompetitionViewerForm(Competition competitionModel)
        {
            InitializeComponent();

            competition = competitionModel;

            LoadFormData();
        }
        private void LoadFormData()
        {
            competitionName.Text = competition.CompetitionName;
        }

    }
}