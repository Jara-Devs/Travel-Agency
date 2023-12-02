FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Travel-Agency-Seed/Travel-Agency-Seed.csproj", "Travel-Agency-Seed/"]
RUN dotnet restore "Travel-Agency-Seed/Travel-Agency-Seed.csproj"
COPY . .
WORKDIR "/src/Travel-Agency-Seed"
RUN dotnet build "Travel-Agency-Seed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Travel-Agency-Seed.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Travel-Agency-Seed.dll"]
