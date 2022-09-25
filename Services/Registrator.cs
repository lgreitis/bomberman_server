using DataAccess.DataAccess;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.Services;

namespace Services
{
    public static class Registrator
    {
        public static void Register(IServiceCollection services)
        {
            var connectionString = "server=containers-us-west-65.railway.app;user=root;database=railway;password=Qjyi4BqOqDtbBX3YAV8w;port=6302";
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<Context>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion, options => options.MigrationsAssembly("DataAccess"))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            // =================== DataAccess ===================
            services.AddScoped(typeof(IGenericDataAccess<>), typeof(GenericDataAccess<>));
            services.AddScoped(typeof(IUserDataAccess), typeof(UserDataAccess));

            // =================== Services ===================
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped(typeof(IUserService), typeof(UserService));
        }
    }
}
