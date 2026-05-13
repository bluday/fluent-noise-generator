using FluentNoiseGenerator.Configuration;
using FluentNoiseGenerator.UI.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;
using System;

using IHost host = Host.CreateDefaultBuilder()
    .ConfigureLogging(LoggingConfiguration.Configure)
    .ConfigureServices(ServiceConfiguration.Configure)
    .UseContentRoot(AppContext.BaseDirectory)
    .Build();

host.ConfigureCommunityToolkitIoc();

await host.RunAsync();