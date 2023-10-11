.PHONY: dev
dev:
	dotnet run --project Travel-Agency-Api

.PHONY: restore
restore:
	dotnet restore

.PHONY: db
db:
	dotnet ef database update --project Travel-Agency-DataBase

.PHONY: migrate
migrate:
	dotnet ef migrations add $(name) --project Travel-Agency-DataBase


.PHONY: build
build:
	dotnet build