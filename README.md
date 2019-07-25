1.enable CORS in your Web API, you should install the Microsoft.AspNet.WebApi.Cors package, which is available on NuGet.

In Visual Studio, select Library Package Manager from the Tools menu, and then select Package Manager Console. Write the following command in the console window:
Install-Package Microsoft.AspNet.WebApi.Cors -pre -project WebService

2. add web API (name:Repository) to IIS:
 Site name: Name of website to be appeared in IIS listing.
 Application pool: Select an application pool or keep is the default to create new application pool same name as sitename.
 Physical path: Enter the location of website pages on system.
 Binding:
 Type: Select protocol http to configure
 Port: Enter port 54749  framework :4 .
 Start Website immediately: keep this box checked to start site.

