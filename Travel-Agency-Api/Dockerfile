FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Travel-Agency-Api/Travel-Agency-Api.csproj", "Travel-Agency-Api/"]
RUN dotnet restore "Travel-Agency-Api/Travel-Agency-Api.csproj"
COPY . .
WORKDIR "/src/Travel-Agency-Api"
RUN dotnet build "Travel-Agency-Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Travel-Agency-Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Travel-Agency-Api.dll"]
