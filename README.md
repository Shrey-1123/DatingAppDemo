# DatingAppDemo
Extensions in Vs code for .Net
- C#
- C# Extensions
- .Net Core Tools : to build and run test >net Core apps
- Nuget Package Manager
- Visual Studio IntelliCode
- vs code icons
- Material Icon Theme
- SqlIte , for Microsoft.EF.core.Sqlite : for database

Namespaces needed:
-     PackageReference Include="Swashbuckle.AspNetCore" Version="5.6.3"
-     PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.5"
-     PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.5"
-     PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite.Core" Version="5.0.5"
-     PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="5.0.5"
-     PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="5.0.5"

-------------------------------------------------------------------------------------------------------------------------------------
Extensions in Vs code for angular
- angularLnaguage Service : Similar to c# extension for angular suggestions
- Angular snippents v9
- brackets pair Colorizer2

----------------------------------------------------------------------------------------------------------------------------------------
Extensions for HTML
- Auto rename tag
- Prettier code formatter

-------------------------------------------------------------------------------------------------
Adding Node 
website to install nodejs using nvm : https://joachim8675309.medium.com/installing-node-js-with-nvm-4dc469c977d9
website for chocolatey : https://community.chocolatey.org/packages/chocolatey/0.10.15

- install node using nvm to your system
- use chocolatey command
--------
Install with PowerShell.exe
With PowerShell, there is an additional step. You must ensure Get-ExecutionPolicy is not Restricted. We suggest using Bypass to bypass the policy to get things installed or AllSigned for quite a bit more security.

Run Get-ExecutionPolicy. If it returns Restricted, then run Set-ExecutionPolicy AllSigned or Set-ExecutionPolicy Bypass -Scope Process.
Now run the following command
	Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
--------
 choco upgrade chocolatey
# chose desired node version
$version = "8.12.0"
# install nvm w/ cmder
choco install cmder
choco install nvm
refreshenv
# install node
nvm install $version
nvm use $version
------------------------------------------------------------------------------------------------------------------
Git
To add git version control
- git status
- git init : to intialize repo in application folder, 
- dotnet new -h
- dotnet gitignore : after this gitignore file created
  Inside git ignore add entry to file which we dont want in our public repo e.g application.json( for production server)
- Create a new repo on github
- git remote add origin *url for github repo*
- git push -u origin master : here -u stands for upstream
- authenticate git hub using github username and password
----------------------------------------------------------------------------------------------------------------------------------
Adding Angular using CLI
website( go into get started sections>setup and copy commands) : https://angular.io/guide/setup-local
- npm install -g @angular/cli
- ng new client :  creating and giving name to our angular project
 after this process a client angular project will be created, here client name is choosen by us for our angular project
- cd my-app
  ng serve --open
