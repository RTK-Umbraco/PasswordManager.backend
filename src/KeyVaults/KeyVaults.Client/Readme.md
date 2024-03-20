### Create Api Generator that will generate swagger.json and api client


Install tool manifest file in your solution, run the following command in root. This needs to be done only one time
 ```
dotnet new tool-manifest
```

### Generating Api Client
```
dotnet apigenerator generate -b PasswordManager -n KeyVaults -s C:\Code\PasswordManager\src\KeyVaults\KeyVaults.Client\swagger.json -o C:\Code\PasswordManager\src\KeyVaults\KeyVaults.Client
```
