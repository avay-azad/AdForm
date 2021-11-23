Homework_Adform
Name: ToDoApp.Api (.Net Core)

 

Description : 

A rest api project to do CRUD operations for todoitems or lists via HTTP Verbs (GET, POST, PUT, DELETE, PATCH).

It includes functionality to create labels which can be assigned to items or lists. It also includes  authorization via JWT Token. The only thing which must be taken care of is connection string in appsettings.json.
It also logs each and every request/response or error if any.

Added support for GraphQL and unit test cases.

DB Setup -

1. Database is configured and migration is already present. It will be created when the application runs for the first time automatically. Only the connection string in appsettings.json needs to be changed accordingly.
2. If database is to be updated with changes "Update-database" command will update the database.


How to run application
Approch 1 : Through visual studio
Step 1: Clone repo:
git clone https://github.com/avay-azad/AdForm.git

Step 2: AdForm.Backend solution in visual studio

Step 3 : setup statup project ToDoApp and run application


Approch 2 : Through .Net Cli command

Step 1: Clone repo in destination folder: git clone https://github.com/avay-azad/AdForm.git

Step 2: Go to the project folder and run “dotnet restore” in cmd.

Step 3: Go the folder “AdForm.Backend\Services\HomeWorkToDoApp\ToDoApp.Api” and run “dotnet run” in cmd.

Navigate to http://localhost:5000/PlayGround to play with GraphQl UI.


Authorization

Send token in "Authorization" header as "Bearer <token>" (Example: "Bearer sampletoken")

I have created 3 user 
1)UserName = "avay",Password = "Avay@123"
1)UserName = "amar",Password = "Amar@123",
1)UserName = "azad",Password = "Azad@123",


Note - 
1. A user has to create todo list first in order to add todo item. 
2. One username can not be registered again.
3. symetric encryption/decryption and Base64 password encoding algorithm is used.
4. For response, a custom apiresponse class is setup for details.
5. For all error response should be in one format 
6. Serilog estensions is used for logging, logs can be checked in C:\ProjectAssignment\AdForm.Backend\Services\HomeWorkToDoApp\ToDoApp.Api\AdFormLog folder for current date text file
7. For seeding data, only three user is added. 
8. for checking Api you need to add List,Label,Items 