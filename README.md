Homework_Adform
Name: ToDo App Backend API (.Net Core)

 

Description
A rest api project to do CRUD operations for todoitems or lists via HTTP Verbs (GET, POST, PUT, DELETE, PATCH).

It includes functionality to create labels which can be assigned to items or lists. It also includes  authorization via JWT Token. The only thing which must be taken care of is connection string in appsettings.json.
It also logs each and every request/response or error if any.

Added support for GraphQL and unit test cases.

How to run application

Step 1: Clone repo:
git clone https://github.com/avay-azad/AdForm.git

Step 2: Go the folder "AdForm.Backend" and run
“dotnet restore”

Step 3: Go the folder “AdForm.Backend\Services\AdFormAssignment\AdFormAssignment.Api” and run 
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