﻿using Microsoft.Extensions.DependencyInjection;

namespace PasswordManager.KeyVaults.ApplicationServices.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServiceServices(this IServiceCollection services)
    {
        //Add application service services
        //Use scoped as method to add services
        return services;
    }
}