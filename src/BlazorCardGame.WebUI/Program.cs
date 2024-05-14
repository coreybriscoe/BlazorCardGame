global using Fluxor;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorCardGame.WebUI;
using BlazorCardGame.Domain.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));
builder.Services.AddScoped<GameEngine>();
builder.Services.AddScoped<GameEngineUIFacade>();

await builder.Build().RunAsync();
