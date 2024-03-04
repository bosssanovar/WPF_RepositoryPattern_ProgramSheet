using Microsoft.Extensions.DependencyInjection;
using Repository;
using RepositoryMonitor;
using System.Windows;
using Usecase;
using WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            // DI
            var services = new ServiceCollection();
            services.AddSingleton<IXXRepository, InMemoryXXRepository>();
            services.AddTransient<SaveLoadUsecase>();
            services.AddTransient<InitUsecase>();
            services.AddTransient<Model>();
            services.AddTransient<RepositoryMonitorView>();
            var provider = services.BuildServiceProvider();






            var model = provider.GetRequiredService<Model>();
            var repositoryMonitorView = provider.GetRequiredService<RepositoryMonitorView>();
            var mainWindow = new MainWindowView(model, repositoryMonitorView);
            MainWindow = mainWindow;
            mainWindow.Show();
        }
    }

}
