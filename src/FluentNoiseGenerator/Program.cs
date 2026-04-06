using CommunityToolkit.Mvvm.DependencyInjection;
using FluentNoiseGenerator;
using FluentNoiseGenerator.Configuration;
using FluentNoiseGenerator.UI.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;
using System;

using IHost host = Host.CreateDefaultBuilder()
    .UseWinUI3Application<App>()
    .UseContentRoot(AppContext.BaseDirectory)
    .ConfigureLogging(LoggingConfiguration.Configure)
    .ConfigureServices(ServiceConfiguration.Configure)
    .Build();

Ioc.Default.ConfigureServices(host.Services);

await host.RunAsync();