using Microsoft.Extensions.DependencyInjection;
namespace BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers
{
    public static class Extensions
    {
        public static IServiceCollection RegisterResizeListener(this IServiceCollection services)
        {
            services.AddScoped<IResizeListener, ResizeListener>();
            return services; //so i can chain things together.  i like that idea.
        }
    }
}
