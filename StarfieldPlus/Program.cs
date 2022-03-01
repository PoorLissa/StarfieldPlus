using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;   // For dll import


namespace StarfieldPlus
{
    internal static class Program
    {
        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);


        private enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

//          Application.SetHighDpiMode(HighDpiMode.SystemAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (args.Length > 0)
            {
                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = null;

                // Handle cases where arguments are separated by colon.
                // Examples: /c:1234567 or /P:1234567
                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else
                {
                    if (args.Length > 1)
                        secondArgument = args[1];
                }

                switch (firstArgument)
                {
                    // Configuration mode
                    case "/c":
                        // TODO
                        break;

                    // Preview mode
                    case "/p":
                        // TODO
                        break;

                    // Full-screen mode
                    case "/s":
                        ShowScreenSaver();
                        Application.Run();
                        break;

                    default:
                        MessageBox.Show($"Sorry, but the command line argument {firstArgument} is not valid", "ScreenSaver",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
            else
            {
                // No arguments - treat like /c
                // todo: remove this later and really treat it like /c
                ShowScreenSaver();
                Application.Run();
            }
        }



        static void ShowScreenSaver()
        {
            SetDpiAwareness();

            foreach (Screen screen in Screen.AllScreens)
            {
                // todo: make it work on each monitor
                if (screen.Bounds.Width > 3000)
                {
                    Form1 ss = new Form1(screen.Bounds);
                    ss.Show();
                    break;
                }
            }

            return;
        }



        private static void SetDpiAwareness()
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
                }
            }
            catch (EntryPointNotFoundException)
            {
                // this exception occures if OS does not implement this API, just ignore it.
            }

            return;
        }
    }
}
