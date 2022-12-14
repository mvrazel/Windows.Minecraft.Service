# Windows.Minecraft.Service
A simple Windows Service wrapper for Minecraft Server

This service wrapper program allows the Java-based Minecraft Server to run as a self-starting/stopping Windows Service.

## Dependencies
1. An appropriate JDK ([Microsoft's](https://docs.microsoft.com/en-us/java/openjdk/download) is fine)
2. The [Minecraft Server](https://www.minecraft.net/en-us/download/server) .JAR file from Mojang
3. Visual Studio 2022 to compile the code ([Community Edition](https://visualstudio.microsoft.com/downloads/) is fine)
## Compiling
Simply open the solution (.SLN) file, select the configuration (Debug, Release, etc.) and compile (Control-Shift-B).
## Installation
These instructions assume that Visual Studio 2022 has already compiled the service wrapper code.

First, open an administrative command prompt and create new new service directory structure somewhere --
```
C:
md C:\Windows.Minecraft.Service\Minecraft
cd C:\Windows.Minecraft.Service\Minecraft
```
Next, download the [Minecraft Server](https://www.minecraft.net/en-us/download/server) .JAR file from Mojang into the "C:\Windows.Minecraft.Service\Minecraft" directory. Make a note of the name of the file. In the same directory, create a text file called "eula.txt" with the following content --
```
eula=true
```
Return to the parent directory --
```
cd ..
```
Copy the following files from the Visual Studio output directory (e.g., "bin\Release") --
* Windows.Minecraft.Service.exe
* Windows.Minecraft.Service.config

Optionally edit the "Windows.Minecraft.Service.config" and adjust the "Parameters" key in the "appSettings" section (example follows) --
```
<appSettings>
  <add key="Parameters" value="-Xmx1024M -Xms1024M -jar minecraft_server.1.19.2.jar nogui"/>
</appSettings>
```
Note: Make sure the name of the .JAR file matches the name of the file in the "Minecraft" directory. Other parameters may be different depending on your needs.
Next, install the service file. You can use the standard "INSTALLUTIL.EXE" file or simply run the following --
```
Windows.Minecraft.Service.exe /i
```
A new service called "Minecraft Service for Windows" should now be listed in the SERVICES.MSC app which can be started from there, or from a command prompt as follows --
```
sc start Windows.Minecraft.Service
```
# Uninstallation
To uninstall the service, open an administrative command prompt and do the following --
```
C:
cd C:\Windows.Minecraft.Service
sc stop Windows.Minecraft.Service
Windows.Minecraft.Service.exe /u
```
