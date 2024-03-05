# Template

## Migrations

Create tables for database go to directory "src/Project/Project.Infrastructure" to run EF core migraiton commands

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --verbose --context PasswordContext --project . --startup-project ../Password.Api.Service
```


```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update --verbose --context PasswordContext --project . --startup-project ../Password.Api.Service
```


To add a new migration:

```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add AddUser --context PasswordContext --project . --startup-project ../Password.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations add AddMembers --context PasswordContext --project . --startup-project ../Password.Api.Service
```

To remove latest added:
```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations remove --context PasswordContext --project . --startup-project ../Password.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations remove --context PasswordContext --project . --startup-project ../Password.Api.Service
```
