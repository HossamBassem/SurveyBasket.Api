using FluentValidation;
using MapsterMapper;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace SurveyBasket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger
            services.AddSwaggerServices();
            //Add Mapster Configurations
            services.AddMapsterServices();

            //Register Application Services
            services.AddScoped<IPollService, PollService>();

            //Add Fluent Validation
            services.AddValidationServices();

            return services;
        }



        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddMapsterServices(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(config));

            return services;
        }
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services
                 .AddFluentValidationAutoValidation()
                 .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}