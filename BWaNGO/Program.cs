using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BWaNGO;
using BWaNGO.Generation;
using BWaNGO.Repositories;
using BWaNGO.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ILabelsRepository, LabelsRepository>();
builder.Services.AddScoped<IBingoBoardRepository, BingoBoardRepository>();
builder.Services.AddScoped<IBingoBoardGenerator, BingoBoardGenerator>();
builder.Services.AddScoped<ClipboardService>();
builder.Services.AddScoped<IShareService, ShareService>();

await builder.Build().RunAsync();