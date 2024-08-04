FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /root

# Common
COPY ["src/Common/Shared/Shared.csproj", "src/Common/Shared/"]
COPY ["src/Common/Domain/Domain.csproj", "src/Common/Domain/"]
COPY ["src/Common/Application/Application.csproj", "src/Common/Application/"]
COPY ["src/Common/Application.Contracts/Application.Contracts.csproj", "src/Common/Application.Contracts/"]
COPY ["src/Common/Authorization/Authorization.csproj", "src/Common/Authorization/"]
COPY ["src/Common/Infrastructure/Infrastructure.csproj", "src/Common/Infrastructure/"]
COPY ["src/Common/Persistence/Persistence.csproj", "src/Common/Persistence/"]
COPY ["src/Common/Endpoints/Endpoints.csproj", "src/Common/Endpoints/"]

# Modules - Users
COPY ["src/Modules/Users/Modules.Users.Domain/Modules.Users.Domain.csproj", "src/Modules/Users/Modules.Users.Domain/"]
COPY ["src/Modules/Users/Modules.Users.Application/Modules.Users.Application.csproj", "src/Modules/Users/Modules.Users.Application/"]
COPY ["src/Modules/Users/Modules.Users.Endpoints/Modules.Users.Endpoints.csproj", "src/Modules/Users/Modules.Users.Endpoints/"]
COPY ["src/Modules/Users/Modules.Users.Infrastructure/Modules.Users.Infrastructure.csproj", "src/Modules/Users/Modules.Users.Infrastructure/"]
COPY ["src/Modules/Users/Modules.Users.Persistence/Modules.Users.Persistence.csproj", "src/Modules/Users/Modules.Users.Persistence/"]

# Modules - Training
COPY ["src/Modules/Training/Modules.Training.Domain/Modules.Training.Domain.csproj", "src/Modules/Training/Modules.Training.Domain/"]
COPY ["src/Modules/Training/Modules.Training.Application/Modules.Training.Application.csproj", "src/Modules/Training/Modules.Training.Application/"]
COPY ["src/Modules/Training/Modules.Training.Endpoints/Modules.Training.Endpoints.csproj", "src/Modules/Training/Modules.Training.Endpoints/"]
COPY ["src/Modules/Training/Modules.Training.Infrastructure/Modules.Training.Infrastructure.csproj", "src/Modules/Training/Modules.Training.Infrastructure/"]
COPY ["src/Modules/Training/Modules.Training.Persistence/Modules.Training.Persistence.csproj", "src/Modules/Training/Modules.Training.Persistence/"]

# Modules - Notifications
COPY ["src/Modules/Notifications/Modules.Notifications.Domain/Modules.Notifications.Domain.csproj", "src/Modules/Notifications/Modules.Notifications.Domain/"]
COPY ["src/Modules/Notifications/Modules.Notifications.Application/Modules.Notifications.Application.csproj", "src/Modules/Notifications/Modules.Notifications.Application/"]
COPY ["src/Modules/Notifications/Modules.Notifications.Infrastructure/Modules.Notifications.Infrastructure.csproj", "src/Modules/Notifications/Modules.Notifications.Infrastructure/"]
COPY ["src/Modules/Notifications/Modules.Notifications.Persistence/Modules.Notifications.Persistence.csproj", "src/Modules/Notifications/Modules.Notifications.Persistence/"]

# Executable
COPY ["src/App/App.csproj", "src/App/"]

RUN dotnet restore "src/App/App.csproj"

COPY . .

WORKDIR "/root/src/App"
RUN dotnet build "App.csproj" --configuration Release --output /app/build

FROM build AS publish
RUN dotnet publish "App.csproj" --configuration Release --output /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.dll"]