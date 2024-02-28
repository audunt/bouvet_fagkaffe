using bouvet_fagkaffe_frontend;
using bouvet_fagkaffe_frontend.Components;
using bouvet_fagkaffe_repository;
using bouvet_fagkaffe_repository.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sustainsys.Saml2;
using Sustainsys.Saml2.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

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
    options.SPOptions.EntityId = new EntityId("bouvetfagkaffe.azurewebsites.net");
    options.IdentityProviders.Add(
        new IdentityProvider(
            new EntityId("https://sts.windows.net/27e71101-1d69-483a-91d0-34b1c6356c88/"), options.SPOptions)
        {
            MetadataLocation = "https://login.microsoftonline.com/27e71101-1d69-483a-91d0-34b1c6356c88/federationmetadata/2007-06/federationmetadata.xml?appid=27fc81bd-68b3-42e5-973d-c8fa34b31034"
        });
})
.AddCookie();

builder.Services.AddHttpContextAccessor();

// Adding Authorization with SAML
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddTransient<UserSession>();
builder.Services.AddTransient<Operations>();

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
