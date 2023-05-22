using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Data;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensious;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPaymentService, PaymentService>();

        services.AddControllers();

        services.AddAutoMapper(typeof(MappingProfiles));

        services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")!);
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddSingleton<IConnectionMultiplexer>(c =>
        {
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis")!);
            return ConnectionMultiplexer.Connect(options);

        });
        services.AddScoped<IBasketRepository, BasketRepository>();

        services.AddScoped<IOrderService, OrderService>();

        //services.AddScoped<IProductsService, ProductsService>();

        //services.AddScoped<ITypesService, TypesService>();

        services.AddScoped<ITokenService, TokenService>();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value!.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}
