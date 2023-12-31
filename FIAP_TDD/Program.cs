using FIAP_TDD.Data.Data;
using FIAP_TDD.Data.DbAccess;
using FIAP_TDD.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

ConfigurarDependecias(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Aluno}/{action=Index}/{id?}");

app.Run();

static void ConfigurarDependecias(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
    builder.Services.AddScoped<IAlunoService, AlunoService>();
    builder.Services.AddScoped<IAlunoData, AlunoData>();
    builder.Services.AddScoped<ITurmaService, TurmaService>();
    builder.Services.AddScoped<ITurmaData, TurmaData>();
}