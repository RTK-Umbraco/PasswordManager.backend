# Template

## Migrations

Create tables for database go to directory "src/Users/Users.Infrastructure" to run EF core migraiton commands

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --verbose --context UserContext --project . --startup-project ../Users.Api.Service
```


```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update --verbose --context UserContext --project . --startup-project ../Users.Api.Service
```


To add a new migration:

```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add AddUser --context UserContext --project . --startup-project ../Users.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations add AddUser --context UserContext --project . --startup-project ../Users.Api.Service
```

To remove latest added:
```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations remove --context UserContext --project . --startup-project ../Users.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations remove --context UserContext --project . --startup-project ../Users.Api.Service
```
