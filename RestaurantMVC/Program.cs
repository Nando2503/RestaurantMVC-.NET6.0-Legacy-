using LanchesMac.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Areas.Admin.Servicos;
using RestaurantMVC.Context;
using RestaurantMVC.Models;
using RestaurantMVC.Repositories.Interfaces;
using RestaurantMVC.Repositories;
using RestaurantMVC.Services;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@.";
});

builder.Services.Configure<ConfigurationImagens>(builder.Configuration.GetSection("ConfigurationPastaImagens"));

builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoryRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<GraficoVendasService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", politica =>
    {
        politica.RequireRole("Admin");
    });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));


builder.Services.AddControllersWithViews();

builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

CriarPirfisUsuarios(app);

//creates the profiles
//seedUserRoleInitial.SeedRoles();
//creates the users and link them to the profiles
//seedUserRoleInitial.SeedUsers();



app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");


    endpoints.MapControllerRoute(
        name: "categoriaFiltro",
        pattern: "Lanche/{action}/{categoria?}",
        defaults: new { controller = "Lanche", action = "List" }
        );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();

void CriarPirfisUsuarios(WebApplication app)
{
    var ScopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using(var scope = ScopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedRoles();
        service.SeedUsers();
    }
}