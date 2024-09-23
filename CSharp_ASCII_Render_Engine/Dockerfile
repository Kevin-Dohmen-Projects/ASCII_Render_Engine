# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CSharp_ASCII_Render_Engine/CSharp_ASCII_Render_Engine.csproj", "CSharp_ASCII_Render_Engine/"]
RUN dotnet restore "./CSharp_ASCII_Render_Engine/CSharp_ASCII_Render_Engine.csproj"
COPY . .
WORKDIR "/src/CSharp_ASCII_Render_Engine"
RUN dotnet build "./CSharp_ASCII_Render_Engine.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CSharp_ASCII_Render_Engine.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharp_ASCII_Render_Engine.dll"]