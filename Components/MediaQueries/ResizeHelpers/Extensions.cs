using Microsoft.Extensions.DependencyInjection;
namespace BasicBlazorLibrary.Components.MediaQueries.ResizeHelpers
{
    public class Extensions
    {
        public static IServiceCollection RegisterResizeListener(IServiceCollection services)
        {
            services.AddScoped<IResizeListener, ResizeListener>();
            return services; //so i can chain things together.  i like that idea.
        }
    }
}
