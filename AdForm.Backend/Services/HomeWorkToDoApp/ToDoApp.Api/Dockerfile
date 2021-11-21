#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/AdFormAssignment/ToDoApp.Api/ToDoApp.Api.csproj", "Services/AdFormAssignment/ToDoApp.Api/"]
COPY ["Services/AdFormAssignment/ToDoApp.Business/ToDoApp.Business.csproj", "Services/AdFormAssignment/ToDoApp.Business/"]
COPY ["Services/AdFormAssignment/ToDoApp.DataService/ToDoApp.DataService.csproj", "Services/AdFormAssignment/ToDoApp.DataService/"]
COPY ["Model/Entities/Adform.Entities/AdForm.Entities.csproj", "Model/Entities/Adform.Entities/"]
COPY ["Core/AdForm.SDK/AdForm.SDK.csproj", "Core/AdForm.SDK/"]
COPY ["Datapersistence/DBService/AdForm.DBService/AdForm.DBService.csproj", "Datapersistence/DBService/AdForm.DBService/"]
COPY ["Services/AdFormAssignment/ToDoApp.Shared/ToDoApp.Shared.csproj", "Services/AdFormAssignment/ToDoApp.Shared/"]
RUN dotnet restore "Services/AdFormAssignment/ToDoApp.Api/ToDoApp.Api.csproj"
COPY . .
WORKDIR "/src/Services/AdFormAssignment/ToDoApp.Api"
RUN dotnet build "ToDoApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoApp.Api.dll"]
