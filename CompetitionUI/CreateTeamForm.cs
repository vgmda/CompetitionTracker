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


            }
            else
            {
               
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
