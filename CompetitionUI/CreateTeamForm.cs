using CompetitionLibrary;
using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CreateTeamForm : Form
    {
        private List<Person> availableTeamMembers = new List<Person>();
        private List<Person> selectedTeamMembers = new List<Person>();




        public CreateTeamForm()
        {
            InitializeComponent();

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

        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

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

                GlobalConfig.Connection.CreatePerson(p);

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
    }
}
