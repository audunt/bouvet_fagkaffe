using bouvet_fagkaffe_frontend.Components;
using bouvet_fagkaffe_repository.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sustainsys.Saml2;
using Sustainsys.Saml2.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Adding Database conenction.
string connection;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING")!;
}

builder.Services.AddDbContext<FagkaffeContext>(options =>
{
    options.UseSqlServer(connection);
});

// Adding Authentication with SAML
builder.Services.AddAuthentication(sharedOptions =>
{
    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    sharedOptions.DefaultChallengeScheme = "Saml2";
}).AddSaml2(options =>
{
    options.SPOptions.EntityId = new EntityId("705b4634-d459-4a4d-911d-e94fdc0d395a");
    options.IdentityProviders.Add(
        new IdentityProvider(
            new EntityId("https://sts.windows.net/27e71101-1d69-483a-91d0-34b1c6356c88/"), options.SPOptions)
        {
            MetadataLocation = "https://login.microsoftonline.com/27e71101-1d69-483a-91d0-34b1c6356c88/federationmetadata/2007-06/federationmetadata.xml"
        });
})
.AddCookie();

// Adding Authorization with SAML
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
