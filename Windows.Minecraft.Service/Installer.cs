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

using System.ComponentModel;
using System.Reflection;
using System.ServiceProcess;

namespace Windows.Minecraft.Service
{
    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer
    {
        private static readonly ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller()
        {
            Account = ServiceAccount.NetworkService,
            Password = null,
            Username = null
        };

        private static readonly ServiceInstaller serviceInstaller = new ServiceInstaller()
        {
            Description = "Service wrapper for Minecraft Server",
            DisplayName = "Minecraft Service for Windows",
            ServiceName = Assembly.GetExecutingAssembly().GetName().Name
        };

        // PROPERTIES
        internal static string ServiceName { get { return serviceInstaller.ServiceName; } }

        // CONSTRUCTOR
        public Installer()
        {
            Installers.AddRange(new System.Configuration.Install.Installer[]
            {
                serviceProcessInstaller,
                serviceInstaller
            });
        }

        // Dispose()
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
