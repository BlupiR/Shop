// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeanutButter.TinyEventAggregator;
using SampleHierarchies.Data;
using SampleHierarchies.Gui;
using SampleHierarchies.Interfaces;
using SampleHierarchies.Services;

namespace ImageTagger.FrontEnd.WinForms;

/// <summary>
/// Main class for starting up program.
/// </summary>
internal static class Program
{
    #region Main Method

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    /// <param name="args">Arguments</param>
    [STAThread]
    static void Main(string[] args)
    {
        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

        var mainScreen = ServiceProvider.GetRequiredService<MainScreen>();
        mainScreen.Show();
    }

    #endregion // Main Method

    #region Properties And Methods

    /// <summary>
    /// Service provider.
    /// </summary>
    public static IServiceProvider? ServiceProvider { get; private set; } = null;

    /// <summary>
    /// Creates a host builder.
    /// </summary>
    /// <returns></returns>
    static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => 
            {
                services.AddSingleton<IEventAggregator, EventAggregator>();
                services.AddSingleton<IShopService, ShopService>();
                services.AddSingleton<IShop, SampleHierarchies.Data.Shop>();
                services.AddSingleton<ICustomer, Customer>();
                services.AddSingleton<SampleHierarchies.Data.Shop, SampleHierarchies.Data.Shop>();
                services.AddSingleton<MainScreen, MainScreen>();
                services.AddSingleton<SampleHierarchies.Gui.ShopScreen, SampleHierarchies.Gui.ShopScreen>();

            });
    }

    #endregion // Properties And Methods
}

