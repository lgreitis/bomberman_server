using DataAccess.DataAccess;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.Services;
using Services.Services.Lobby;

namespace Services
{
    public static class Registrator
    {
        public static void Register(IServiceCollection services)
        {
            var connectionString = "server=containers-us-west-65.railway.app;user=root;database=railway;password=CiURzce0I9ScJdalHpfj;port=6302";
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
            services.AddScoped(typeof(ILobbyDataAccess), typeof(LobbyDataAccess));
            services.AddScoped(typeof(ILobbyUserDataAccess), typeof(LobbyUserDataAccess));

            // =================== Services ===================
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(ILobbyService), typeof(LobbyService));
        }
    }
}
