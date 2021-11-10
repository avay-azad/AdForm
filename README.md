Homework_Adform
Name: ToDo App Backend API (.Net Core)

 

Description
A rest api project to do CRUD operations for todoitems or lists via HTTP Verbs (GET, POST, PUT, DELETE, PATCH).

It includes functionality to create labels which can be assigned to items or lists. It also includes  authorization via JWT Token. The only thing which must be taken care of is connection string in appsettings.json.
It also logs each and every request/response or error if any.

Added support for GraphQL and unit test cases.


How to run application
Approch 1 : Through visual studio
Step 1: Clone repo:
git clone https://github.com/avay-azad/AdForm.git

Step 2: AdForm.Backend solution in visual studio
Step 3 : Open package Manage consloe and select project AdForm.DBService and run command
 Update-Database –verbose

Step 4 : setup statup project AdFormAssignment.Api andrun application


Approch 2 : Through .Net Cli command

Step 1: Clone repo:
git clone https://github.com/avay-azad/AdForm.git

Step 2: Go the folder "AdForm.Backend" and run
“dotnet restore”

setup Database 

Step 3 : based on enviornment Change connection string in file appsettings.Development or appsettings.Production
like : "ConnectionStrings": {
    "AdFormDataContext": "server=AVAYAZAD-01; database=AdFormDB_Development;Trusted_Connection=True;"
  }
  
Step 4: Go the folder "AdForm.Backend\Datapersistence\DBService\AdForm.DBService" and run following command
4.1: dotnet tool install --global dotnet-ef
4.2 : dotnet tool update --global dotnet-ef
4.3 :dotnet add package Microsoft.EntityFrameworkCore.Design
4.4: dotnet ef

Step 5: Go the folder “AdForm.Backend\Services\AdFormAssignment\AdFormAssignment.Api” and run 
 dotnet ef database update -- --environment Development

Step 6: Go the folder “AdForm.Backend\Services\AdFormAssignment\AdFormAssignment.Api” and run 
“dotnet run”

Navigate to http://localhost:5000/ in a browser to play with the Swagger UI.
Navigate to https://localhost:5001/ in a browser to play with the Swagger UI.


Authorization

Send token in "Authorization" header as "Bearer <token>" (Example: "Bearer sampletoken")

Note
=>Search field is available in PaginationParameters type.

=>In Version please pass 1.


I have created 3 user 
1)UserName = "avay",Password = "Avay@123"
1)UserName = "amar",Password = "Amar@123",
1)UserName = "azad",Password = "Azad@123",