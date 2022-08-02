using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;

// Create the builder and add the root components
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add additional needed classes
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IRecordsRepository, RecordsRepository>();
builder.Services.AddSingleton<IDatabaseFacade, DatabaseFacade>();

// Let the builder do its thing
await builder.Build().RunAsync();
