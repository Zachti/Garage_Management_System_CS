using Microsoft.Extensions.DependencyInjection;

namespace Garage {
    internal class Program
    {
        public static void Main()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<Garage>()
                .AddTransient<UIManager>()
                .BuildServiceProvider();

            UIManager uiManager = serviceProvider.GetRequiredService<UIManager>();

            uiManager.Start();
        }
    }
}