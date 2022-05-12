using CompetitionLibrary;
using CompetitionLibrary.Models;

namespace CompetitionUI
{
    public partial class CreateTeamForm : Form
    {
        public CreateTeamForm()
        {
            InitializeComponent();
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

            if(emailValue.Text.Length == 0)
            {
                return false;
            }

            if(mobileValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
