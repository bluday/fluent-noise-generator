using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator;
using FluentNoiseGenerator.Configuration;
using FluentNoiseGenerator.UI.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;
using System;

using IHost host = Host.CreateDefaultBuilder()
    .UseContentRoot(AppContext.BaseDirectory)
    .ConfigureLogging(LoggingConfiguration.Configure)
    .ConfigureServices(ServiceConfiguration.Configure)
    .UseWinUI3Application<App>()
    .Build();

Ioc.Default.ConfigureServices(host.Services);

await host.RunAsync();