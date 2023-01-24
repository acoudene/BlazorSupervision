using BlazorSupervision.Client;
using BlazorSupervision.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
  .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services
  .AddHttpClient(Options.DefaultName, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services
  .AddScoped<ILogService, SnackbarService>();

builder.Services.AddLocalization();

builder.Services.AddMudServices();


await builder.Build().RunAsync();
