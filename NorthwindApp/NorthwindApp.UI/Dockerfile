FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["NorthwindApp.Core/NorthwindApp.Core.csproj", "NorthwindApp.Core/"]
COPY ["NorthwindApp.DAL.Interfaces/NorthwindApp.DAL.Interfaces.csproj", "NorthwindApp.DAL.Interfaces/"]
COPY ["NorthwindApp.DAL/NorthwindApp.DAL.csproj", "NorthwindApp.DAL/"]
COPY ["NorthwindApp.Models/NorthwindApp.Models.csproj", "NorthwindApp.Models/"]
COPY ["NorthwindApp.Services.Interfaces/NorthwindApp.Services.Interfaces.csproj", "NorthwindApp.Services.Interfaces/"]
COPY ["NorthwindApp.Services/NorthwindApp.Services.csproj", "NorthwindApp.Services/"]
COPY ["NorthwindApp.UI/NorthwindApp.UI.csproj", "NorthwindApp.UI/"]

RUN dotnet restore "NorthwindApp.UI/NorthwindApp.UI.csproj"
COPY . .
WORKDIR "/src/NorthwindApp.UI"
RUN dotnet build "NorthwindApp.UI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NorthwindApp.UI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NorthwindApp.UI.dll"]