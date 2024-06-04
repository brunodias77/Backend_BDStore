using BDS.Application.Commands.Users.RegisterUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace BDS.Application.Configurations;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddPasswordEncrypter(services, configuration);
        AddUseCases(services);
        AddMediatR(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        // services.AddScoped(option =>
        //     new AutoMapper.MapperConfiguration(opt => { opt.AddProfile(new AutoMapping()); }).CreateMapper());
    }

    private static void AddUseCases(IServiceCollection services)
    {
        //  services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
    {
        //var additionalKey = configuration.GetSection("Settings:Password:AdditionalKey").Value;
        // var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        // services.AddScoped(options => new PasswordEncripter(additionalKey));
    }

    private static void AddMediatR(IServiceCollection services)
    {
        services
            .AddMediatR(config => config.RegisterServicesFromAssemblyContaining<RegisterUserCommand>());
    }
}