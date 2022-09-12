using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace Windows.Minecraft.Service
{
    internal static class Program
    {
        // Main()
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                if (args.Count() > 0)
                {
                    switch (args[0].ToUpperInvariant())
                    {
                        case "/I":
                            ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });

                            if (!EventLog.SourceExists(Installer.ServiceName))
                            {
                                EventLog.CreateEventSource(Installer.ServiceName, "Application");
                            }

                            break;

                        case "/U":
                            ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });

                            if (EventLog.SourceExists(Installer.ServiceName))
                            {
                                EventLog.DeleteEventSource(Installer.ServiceName, "Application");
                            }

                            break;
                    }
                }
            }
            else
            {
                ServiceBase.Run(new ServiceBase[] { new Minecraft() });
            }
        }
    }

}
