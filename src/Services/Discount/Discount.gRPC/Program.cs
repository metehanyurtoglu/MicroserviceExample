using Discount.gRPC.Data;
using Discount.gRPC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
    });

    builder.Services.AddGrpc().AddJsonTranscoding(); ;
    builder.Services.AddGrpcSwagger();
    builder.Services.AddSwaggerGen(options =>
    {
        var filePath = Path.Combine(System.AppContext.BaseDirectory, "Discount.gRPC.xml");

        options.SwaggerDoc("v1", new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
        options.IncludeXmlComments(filePath);
        options.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
    });
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.UseMigration();
    app.UseSwagger();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
    }

    app.MapGrpcService<DiscountService>();
    app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

    app.Run();
}