namespace ProjectApi.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Data;

    public static class ApplicationBuildExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder appBuilder)
        {
            using var services = appBuilder.ApplicationServices.CreateScope();

            var db = services.ServiceProvider.GetService<ProjectDbContext>();

            db.Database.Migrate();
        }

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
            => app.UseSwagger()
                    .UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Project API");
                        options.RoutePrefix = string.Empty;
                    });

    }
}
