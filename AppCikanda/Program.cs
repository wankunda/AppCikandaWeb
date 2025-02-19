namespace AppCikanda
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string data = @"Config\Data";
            if (!Directory.Exists(data))
                Directory.CreateDirectory(data);
            Application.Run(new MainForm());
        }
    }
}