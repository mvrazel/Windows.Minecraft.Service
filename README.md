# Windows.Minecraft.Service
A simple Windows Service wrapper for Minecraft Server

## Installation
These instructions assume that Visual Studio 2022 has already compiled the service wrapper code.

First, open an administrative command prompt and create new new service directory structure somewhere --
```
C:
md C:\Windows.Minecraft.Service\Minecraft
cd C:\Windows.Minecraft.Service\Minecraft
```
Next, download the [Minecraft Server .JAR file](https://www.minecraft.net/en-us/download/server) from Mojang into the "C:\Windows.Minecraft.Service\Minecraft" directory. Make a note of the name of the file. In the same directory, create a text file called "eula.txt" with the following content --
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

Edit the "Windows.Minecraft.Service.config" and place a "Parameters" item in the "AppSettings" section (example follows) --
```
<appSettings>
  <add key="Parameters" value="-server -Xmx8G -Xms4G -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M -jar minecraft_server.1.19.2.jar nogui"/>
</appSettings>
```
Note: Make sure the name of the .JAR file matches the name of the file in the "Minecraft" directory. Other parameters may be different depending on your needs.
Next, install the service file. You can use the standard "INSTALLUTIL.EXE" file or simply run the following --
```
Windows.Minecraft.Service.exe /i
```
A new service called "Minecraft Service for Windows" should now be listed in the SERVICES.MSC app.