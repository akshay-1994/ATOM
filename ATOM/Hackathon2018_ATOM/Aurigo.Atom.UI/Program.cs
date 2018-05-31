using System;
using System.Windows.Forms;
using Aurigo.Atom.Generator.Core.Config;
using Aurigo.Atom.UI.DTO;
using System.Diagnostics;

namespace Aurigo.Atom.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BuildConfigurations();

            Application.Run(new FormSelector());
        }

        private static void BuildConfigurations()
        {
            AppConfig.Build();

            Configurator.AddTestCaseComponentConfig(AppConfig.TestCaseComponentConfiguration);
        }

        public static void ExecuteCommand(string Command)
        {
            ProcessStartInfo ProcessInfo;
            Process Process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + Command);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;

            Process = Process.Start(ProcessInfo);
        }
    }
}