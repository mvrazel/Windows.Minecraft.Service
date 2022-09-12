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
