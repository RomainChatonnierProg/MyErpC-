using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MyErp.Base;
using MyErp.Converters;
using MyErp.Metier;
using MyErp.Repository;
using MyErp.Translation;
using MyErp.Views;

namespace MyErp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceProvider = ConfigureServices();
            this.InitializeComponent();
        }

        public static IServiceProvider Services => ((App)Current).ServiceProvider;

        public IServiceProvider ServiceProvider { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<MainViewModel>();
            services.AddTransient<UserService>();
            services.AddTransient<IUserRepository, JsonFileUserRepository>();
            services.AddTransient<Dico>();
            services.AddTransient<ViewModelBase>();
            services.AddTransient<NullToCollapseConverter>();
            return services.BuildServiceProvider();
        }

    }
}
