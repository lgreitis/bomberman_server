using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public class Resolver
    {
        internal static IServiceProvider? _serviceProvider = null;
        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public static IServiceScope GetScope(IServiceProvider? serviceProvider = null)
        {
            var provider = serviceProvider ?? _serviceProvider;

            return provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }
    }
}
