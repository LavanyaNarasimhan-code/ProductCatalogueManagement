using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Application.Category.Commands;
using ProductCatalogue.Application.MappingProfile;
using ProductCatalogue.Application.Product.Commands;
using ProductCatalogue.Contracts;
using ProductCatalogue.Domain.Events.ProductEvents;
using ProductCatalogue.Persistence.Repository;
using System.Net;
using System.Reflection;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        if (_environment.EnvironmentName != "IntegrationTesting")
        {
            services.AddDbContext<ProductCatalogueDbContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));
        }

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(
                typeof(CreateProductCommand).Assembly,
                typeof(ProductCreatedEvent).Assembly                
            );
        });

        // AutoMapper
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(ProductMappingProfile).Assembly);
        });

        // FluentValidation
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.Scan(scan => scan
            .FromAssemblies(Assembly.Load("ProductCatalogue.Application"))
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        // Run EF Core migration
        if (_environment.IsDevelopment())
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductCatalogueDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is ValidationException validationException)
                {
                    var errors = validationException.Errors.Select(e => new
                    {
                        PropertyName = e.PropertyName,
                        ErrorMessage = e.ErrorMessage
                    });

                    await context.Response.WriteAsJsonAsync(errors);
                }
                else
                {
                    // fallback for unexpected errors
                    var result = new
                    {
                        Message = exceptionHandlerPathFeature?.Error?.Message ?? "An error occurred."
                    };

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(result);
                }
            });
        });

        // Middleware
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI();

        // Exception handling
        
    }
}
