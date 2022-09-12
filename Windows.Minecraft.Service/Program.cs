// Windows.Minecraft.Service - A simple Windows Service wrapper for Minecraft Server
// Copyright (C) 2022 Michael Vrazel

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>

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
