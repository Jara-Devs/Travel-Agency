.PHONY: dev
dev:
	dotnet run --project Travel-Agency-Api

.PHONY: restore
restore:
	dotnet restore

.PHONY: db
db:
	dotnet ef database update --project Travel-Agency-DataBase\Travel-Agency-DataBase.csproj --startup-project Travel-Agency-Api\Travel-Agency-Api.csproj

.PHONY: migrate
migrate:
	dotnet ef migrations add "$(name)" --project Travel-Agency-DataBase\Travel-Agency-DataBase.csproj --startup-project Travel-Agency-Api\Travel-Agency-Api.csproj

.PHONY: remove
remove:
	dotnet ef  migrations remove --project Travel-Agency-DataBase\Travel-Agency-DataBase.csproj --startup-project Travel-Agency-Api\Travel-Agency-Api.csproj


.PHONY: build
build:
	dotnet build


.PHONY: seed
seed:
	dotnet run --project Travel-Agency-Seed\Travel-Agency-Seed.csproj