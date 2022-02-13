using Microsoft.Extensions.DependencyInjection;
using Sudoku.Models.GameModels;
using System.Windows;

namespace Sudoku.UI
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<SudokuBoard>();
        }
    }
}