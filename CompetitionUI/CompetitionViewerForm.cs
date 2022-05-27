using CompetitionLibrary;
using CompetitionLibrary.Models;
using System.ComponentModel;

namespace CompetitionUI
{
    public partial class CompetitionViewerForm : Form
    {
        private Competition competition;
        BindingList<int> rounds = new BindingList<int>();
        BindingList<Matchup> selectedMatchups = new BindingList<Matchup>();


        public CompetitionViewerForm(Competition competitionModel)
        {
            InitializeComponent();

            competition = competitionModel;

            WireUpLists();

            LoadFormData();

            LoadRounds();

        }
        private void LoadFormData()
        {
            competitionName.Text = competition.CompetitionName;
        }

        private void WireUpLists()
        {
            roundDropDown.DataSource = rounds;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = "DisplayName";

        }

        private void LoadRounds()
        {
            rounds.Clear();

            rounds.Add(1);
            int currentRound = 1;

            foreach (List<Matchup> matchups in competition.Rounds)
            {
                if (matchups.First().MatchupRound > currentRound)
                {
                    currentRound = matchups.First().MatchupRound;
                    rounds.Add(currentRound);
                }
            }

            LoadMatchups(1);
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void LoadMatchups(int round)
        {
            foreach (List<Matchup> matchups in competition.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();

                    foreach (Matchup m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckbox.Checked)
                        {
                            selectedMatchups.Add(m);
                        }

                    }
                }
            }

            if (selectedMatchups.Count > 0)
            {
                LoadMatchup(selectedMatchups.First());
            }

            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            // if (selectedMatchups.Count > 0)
            bool isVisible = (selectedMatchups.Count > 0);

            teamOneName.Visible = isVisible;
            teamOneScoreLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;
            teamTwoName.Visible = isVisible;
            teamTwoScoreLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;
            versusLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
        }

        private void LoadMatchup(Matchup m)
        {

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                        teamTwoName.Text = "<bye>";
                        teamTwoScoreValue.Text = "0";
                    }
                    else
                    {
                        teamOneName.Text = "Not Yet Set";
                        teamOneScoreValue.Text = "";
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        teamTwoName.Text = m.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoName.Text = "Not Yet Set";
                        teamTwoScoreValue.Text = "";
                    }
                }
            }
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var m = (Matchup)matchupListBox.SelectedItem;

            if (m != null)
            {
                LoadMatchup(m);
            }
            // LoadMatchup((Matchup)matchupListBox.SelectedItem);
        }

        private void unplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            Matchup m = (Matchup)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);

                        if (scoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("WARNING: Please enter a valid score for Team No: 1");
                            return;
                        }
                    }
                }
                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);

                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("WARNING: Please enter a valid score for Team No: 2");
                            return;
                        }
                    }
                }
            }

            if (teamOneScore > teamTwoScore)
            {
                // Team one wins
                m.Winner = m.Entries[0].TeamCompeting;
            }
            else if (teamTwoScore > teamOneScore)
            {
                // Team two wins
                m.Winner = m.Entries[1].TeamCompeting;
            }
            else
            {
                // Tie games functionality is not handled in this app.. for now.
                MessageBox.Show("ERROR: Tie games are not handled");
            }

            foreach (List<Matchup> round in competition.Rounds)
            {
                // rm = Round Matchup
                foreach (Matchup rm in round)
                {
                    foreach (MatchupEntry me in rm.Entries)
                    {
                        if (me.ParentMatchup != null)
                        {
                            if (me.ParentMatchup.Id == m.Id)
                            {
                                me.TeamCompeting = m.Winner;
                                GlobalConfig.Connection.UpdateMatchup(rm);
                            }
                        }
                    }
                }

            }

            LoadMatchups((int)roundDropDown.SelectedItem);

            // Call Sql update method
            GlobalConfig.Connection.UpdateMatchup(m);
        }
    }
}