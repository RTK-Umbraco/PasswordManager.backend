# Template

## Migrations

Create tables for database go to directory "src/PaymentCards/PaymentCards.Infrastructure" to run EF core migraiton commands

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --verbose --context PaymentCardContext --project . --startup-project ../PaymentCards.Api.Service
```


```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update --verbose --context PaymentCardContext --project . --startup-project ../PaymentCards.Api.Service
```


To add a new migration:

```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add AddUser --context PaymentCardContext --project . --startup-project ../PaymentCards.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations add AddPaymentCard --context PaymentCardContext --project . --startup-project ../PaymentCards.Api.Service
```

To remove latest added:
```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations remove --context PaymentCardContext --project . --startup-project ../PaymentCards.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations remove --context PaymentCardContext --project . --startup-project ../PaymentCards.Api.Service
```
