using Blazored.LocalStorage;
using HappyBookingClient.Components;
using HappyBookingClient.Middleware;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddServices();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

string baseUrl = builder.Configuration.GetSection("ApiSettings:BaseUrl").Value ?? string.Empty;
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
