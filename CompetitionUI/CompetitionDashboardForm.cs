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
        }
    }
}
