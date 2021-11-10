#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/AdFormAssignment/AdFormAssignment.Api/AdFormAssignment.Api.csproj", "Services/AdFormAssignment/AdFormAssignment.Api/"]
COPY ["Services/AdFormAssignment/AdFormAssignment.Business/AdFormAssignment.Business.csproj", "Services/AdFormAssignment/AdFormAssignment.Business/"]
COPY ["Services/AdFormAssignment/AdFormAssignment.DataService/AdFormAssignment.DataService.csproj", "Services/AdFormAssignment/AdFormAssignment.DataService/"]
COPY ["Model/Entities/Adform.Entities/AdForm.Entities.csproj", "Model/Entities/Adform.Entities/"]
COPY ["Core/AdForm.SDK/AdForm.SDK.csproj", "Core/AdForm.SDK/"]
COPY ["Datapersistence/DBService/AdForm.DBService/AdForm.DBService.csproj", "Datapersistence/DBService/AdForm.DBService/"]
COPY ["Services/AdFormAssignment/AdFormAssignment.Shared/AdFormAssignment.Shared.csproj", "Services/AdFormAssignment/AdFormAssignment.Shared/"]
RUN dotnet restore "Services/AdFormAssignment/AdFormAssignment.Api/AdFormAssignment.Api.csproj"
COPY . .
WORKDIR "/src/Services/AdFormAssignment/AdFormAssignment.Api"
RUN dotnet build "AdFormAssignment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdFormAssignment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdFormAssignment.Api.dll"]