using BDS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BDS.Infrastructure.Configurations;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        AddDbContext(services, configuration);
        AddToken(services, configuration);
        //  AddRepositories(services);
        // if (configuration.IsUnitTestEnvironment())
        //     return;
        // AddFluenteMigrator(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var version = new Version(8, 4, 0);
        var serverVersion = new MySqlServerVersion(version);

        services.AddDbContext<ApplicationDbContext>(config => config.UseMySql(connectionString, serverVersion));
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void AddToken(IServiceCollection services, IConfigurationManager configuration)
    {
        // var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        // var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        //
        // services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    // private static void AddRepositories(IServiceCollection services)
    // {
    //     services.AddScoped<IUserRepository, UserRepository>();
    //     services.AddScoped<IUnitOfWork, UnitOfWork>();
    // }
    //
    // private static void AddFluenteMigrator(IServiceCollection services, IConfigurationManager configuration)
    // {
    //     var connectionString = configuration.GetConnectionString("Connection");
    //
    //     services.AddFluentMigratorCore().ConfigureRunner(options =>
    //     {
    //         options.AddMySql8().WithGlobalConnectionString(connectionString)
    //             .ScanIn(Assembly.Load("MyRecipes.Infrastructure")).For.All();
    //     });
    // }
}