using CompetitionLibrary;
using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CreateTeamForm : Form, ITeamRequester
    {
        private List<Person> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<Person> selectedTeamMembers = new List<Person>();
        
        private ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            // CreateSampleData();

            WireUpLists();
        }

        /// <summary>
        /// Create sample data for the lists for testing purposes
        /// </summary>
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new Person { FirstName = "Test1", LastName = "Tester1" });
            availableTeamMembers.Add(new Person { FirstName = "Test2", LastName = "Tester2" });

            selectedTeamMembers.Add(new Person { FirstName = "Test3", LastName = "Tester3" });
            selectedTeamMembers.Add(new Person { FirstName = "Test4", LastName = "Tester4" });

        }
        /// <summary>
        /// Refreshing data binding with the lists
        /// </summary
        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

            if (selectTeamMemberDropDown.SelectedItem == null)
            {
                // Do nothing
                // TODO - Return previous object Person p
            }

        }


        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Person p = new Person();

                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.MobileNumber = mobileValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                mobileValue.Text = "";

            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check it and try again.");
            }
        }

        private bool ValidateForm()
        {
            // TODO - Add validation to the form 

            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }

            if (emailValue.Text.Length == 0)
            {
                return false;
            }

            if (mobileValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            Person p = (Person)selectTeamMemberDropDown.SelectedItem;

            if (selectTeamMemberDropDown.SelectedItem == null)
            {
                MessageBox.Show("Please choose a name");
            }

            if (p != null)
            {
                
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists();
            }
        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            Person p = (Person)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }

        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            Team t = new Team();

            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);

            //callingForm.TeamComplete(t);
            //this.Close();
        }

        public void TeamComplete(Team model)
        {
            throw new NotImplementedException();
        }
    }
}
