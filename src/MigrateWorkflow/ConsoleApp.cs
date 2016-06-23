using System;
using System.Diagnostics;
using System.IO;
using CLAP;

namespace MigrateWorkflow
{
    public class ConsoleApp
    {
        private const string Help = "Migrations Command Line Utility. Applies any pending migrations to the database as part of a pipeline of pluggable workflow steps";
        private const string StartUpDirHelp = "Specifies the working directory of your application. If a relative path is supplied, this will be relative to the windows current directory";
        private const string StartUpConfigFileHelp = "Specifies the Web.config or App.config file of your application";
        private const string StartUpDataDirHelp = "Specifies the directory to use when resolving connection strings containing the |DataDirectory| substitution string.";
        private const string ConnectionStrNameHelp = "Specifies the name of the connection string to use from the specified configuration file. If omitted, the context's default connection will be used";

        [Verb(IsDefault = true, Description = Help)]
        public static void Run([Required, Description(ConnectionStrNameHelp)]string connectionStringName,
            [Required, Description(StartUpConfigFileHelp)] string startUpConfigurationFile,
            [Description(StartUpDataDirHelp)]string startUpDataDirectory,
            [Description(StartUpDataDirHelp)]string startupDirectory, 
            string[] preUpgradeCommands, 
            string[] postUpgradeCommands, bool silent)
        {
            ExecuteUpgrade(upgradeCommand);
        }

        private static void ExecuteUpgrade(string upgradeCommand)
        {
            const int oneMinute = 60000;
            string batchFile = upgradeCommand;

            var process = new Process
            {
                StartInfo =
                {
                    FileName = batchFile,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                }
            };

            process.Start();
            process.WaitForExit(oneMinute);
        }
    }
}