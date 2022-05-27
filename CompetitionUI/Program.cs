using CompetitionLibrary;

namespace CompetitionUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the database connections

            CompetitionLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql);
            // Pre-refactoring code

            Application.Run(new CompetitionDashboardForm());
        }
    }
}