using Catalog.API.Data;
using Core.Application.Behaviors;
using Core.Application.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    var assembly = typeof(Program).Assembly;

    builder.Services.AddCarter();
    builder.Services.AddMediatR(options =>
    {
        options.RegisterServicesFromAssemblies(assembly);
        options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        options.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    builder.Services.AddMarten(options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("Database")!);
    }).UseLightweightSessions();

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.InitializeMartenWith<CatalogInitialData>();
    }

    builder.Services.AddValidatorsFromAssembly(assembly);
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    builder.Services.AddHealthChecks()
        .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.MapCarter();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler(options =>
    {

    });

    app.UseHealthChecks("/health", 
        new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }
    );

    app.Run();
}