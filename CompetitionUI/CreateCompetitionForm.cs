using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CreateCompetitionForm : Form
    {
        List<Team> availableTeams = new List<Team>();

        public CreateCompetitionForm()
        {
            InitializeComponent();
        }
    }
}
