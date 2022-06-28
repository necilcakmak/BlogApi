using Blazored.LocalStorage;
using Blazored.Modal;
using Blog.Ui;
using Blog.Ui.Extensions;
using Blog.Ui.Services;
using Blog.Ui.Services.Auth;
using Blog.Ui.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44322/api/") });

builder.Services.UiServiceRegistration();

builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ModalManager>();
await builder.Build().RunAsync();
