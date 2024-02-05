#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Directory.Build.props", "src/"]
COPY ["src/services/UserManagementService/UserManagement.API/UserManagement.API.csproj", "src/services/UserManagementService/UserManagement.API/"]
COPY ["src/common/Common.Http/Common.Http.csproj", "src/common/Common.Http/"]
COPY ["src/common/Common.Domain/Common.Domain.csproj", "src/common/Common.Domain/"]
COPY ["src/services/UserManagementService/UserManagement.Application/UserManagement.Application.csproj", "src/services/UserManagementService/UserManagement.Application/"]
COPY ["src/common/Common.EventBus/Common.EventBus.csproj", "src/common/Common.EventBus/"]
COPY ["src/shared/Shared.EventBus.Contracts/Shared.EventBus.Contracts.csproj", "src/shared/Shared.EventBus.Contracts/"]
COPY ["src/services/UserManagementService/UserManagement.Domain/UserManagement.Domain.csproj", "src/services/UserManagementService/UserManagement.Domain/"]
COPY ["src/services/UserManagementService/UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "src/services/UserManagementService/UserManagement.Infrastructure/"]
COPY ["src/common/Common.EntityFrameworkCore/Common.EntityFrameworkCore.csproj", "src/common/Common.EntityFrameworkCore/"]
COPY ["src/common/Common.Security/Common.Security.csproj", "src/common/Common.Security/"]
RUN dotnet restore "src/services/UserManagementService/UserManagement.API/UserManagement.API.csproj"
COPY . .
WORKDIR "/src/src/services/UserManagementService/UserManagement.API"
RUN dotnet build "UserManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.API.dll"]