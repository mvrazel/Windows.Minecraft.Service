using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;

namespace Windows.Minecraft.Service
{
    internal class Minecraft : ServiceBase
    {
        private readonly EventLog eventLog = new EventLog()
        {
            Source = Installer.ServiceName
        };

        // Minecraft Server process exited outside of the Service Control Manager...
        private bool exited = false;

        // Minecraft Server process...
        private readonly Process process = new Process()
        {
            EnableRaisingEvents = true,
            StartInfo = new ProcessStartInfo()
            {
                Arguments = ConfigurationManager.AppSettings["Parameters"],
                FileName = "java",
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = string.Format("{0}\\Minecraft", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            }
        };

        // CONSTRUCTOR
        internal Minecraft()
        {
            ServiceName = Installer.ServiceName;

            // Standard error data handler...
            process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (e.Data != null)
                {
                    eventLog.WriteEntry(e.Data, EventLogEntryType.Error);
                }
            });

            // External shutdown or abort event handler...
            process.Exited += new EventHandler((sender, e) =>
            {
                exited = true;

                switch (process.ExitCode)
                {
                    case 0:
                        eventLog.WriteEntry("Operator shutdown received from game client.", EventLogEntryType.Information);
                        break;

                    default:
                        eventLog.WriteEntry(string.Format("Minecraft Server aborted with exit code {0}.", process.ExitCode), EventLogEntryType.Warning);
                        break;
                }

                Stop();
            });

            // Standard output data handler...
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (e.Data != null)
                {
                    if (e.Data.Contains("ERROR"))
                    {
                        eventLog.WriteEntry(e.Data, EventLogEntryType.Error);
                    }

                    if (e.Data.Contains("INFO"))
                    {
                        eventLog.WriteEntry(e.Data, EventLogEntryType.Information);
                    }

                    if (e.Data.Contains("WARN"))
                    {
                        eventLog.WriteEntry(e.Data, EventLogEntryType.Warning);
                    }
                }
            });
        }

        // Dispose()
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        // OnStart()
        protected override void OnStart(string[] args)
        {
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
        }

        // OnStop()
        protected override void OnStop()
        {
            if (!exited)
            {
                process.StandardInput.WriteLine("/save-all");
                process.StandardInput.WriteLine("/stop");
                process.WaitForExit();
            }

            process.Close();
        }
    }
}
