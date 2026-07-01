# FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
# WORKDIR /src

# COPY ["Backend/Backend.csproj", "Backend/"]
# RUN dotnet restore "Backend/Backend.csproj"

# COPY . .
# WORKDIR "/src/Backend"
# RUN dotnet publish "Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
# WORKDIR /app
# COPY --from=build /app/publish .

# ENV ASPNETCORE_URLS=http://+:10000
# EXPOSE 10000

# ENTRYPOINT ["dotnet", "Backend.dll"]

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Backend/*.csproj ./Backend/
RUN dotnet restore

# copy everything else and build app
COPY Backend/. ./Backend/
WORKDIR /source/Backend
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Backend.dll"]