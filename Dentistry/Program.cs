using Dentistry.Data;
using Dentistry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер.
var connectionIdentityString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Строка подключения 'IdentityConnection' не найдена.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionIdentityString));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Строка подключения 'DefaultConnection' не найдена.");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;               // минимальная длина
    options.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    options.Password.RequireLowercase = false;         // требуются ли символы в нижнем регистре
    options.Password.RequireUppercase = false;         // требуются ли символы в верхнем регистре
    options.Password.RequireDigit = false;             // требуются ли цифры
    options.User.RequireUniqueEmail = true;            // уникальный email
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.CreateRolesAsync(builder.Configuration);

app.Run();