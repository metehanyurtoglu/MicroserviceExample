using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            
            dbContext.Database.MigrateAsync();

            return app;
        }
    }
}
