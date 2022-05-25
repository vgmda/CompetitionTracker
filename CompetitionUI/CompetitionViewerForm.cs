using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CompetitionViewerForm : Form
    {
        private Competition competition;
        List<int> rounds = new List<int>();
        List<Matchup> selectedMatchups = new List<Matchup>();

        public CompetitionViewerForm(Competition competitionModel)
        {
            InitializeComponent();

            competition = competitionModel;

            LoadFormData();

            LoadRounds();

        }
        private void LoadFormData()
        {
            competitionName.Text = competition.CompetitionName;
        }

        private void WireUpRoundsLists()
        {
            roundDropDown.DataSource = null;
            roundDropDown.DataSource = rounds;

        }
        private void WireUpMathupsLists()
        {
            matchupListBox.DataSource = null;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";

        }


        private void LoadRounds()
        {
            rounds = new List<int>();

            rounds.Add(1);
            int currentRound = 1;

            foreach(List<Matchup> matchups in competition.Rounds)
            {
                if (matchups.First().MatchupRound > currentRound)
                {
                    currentRound = matchups.First().MatchupRound;
                    rounds.Add(currentRound);
                }

            }

            WireUpRoundsLists();
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchupList();
        }
        private void LoadMatchupList()
        {
            int round = (int)roundDropDown.SelectedItem;

            foreach (List<Matchup> matchups in competition.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups = matchups;
                }
            }

            WireUpMathupsLists();
        }
    }
}