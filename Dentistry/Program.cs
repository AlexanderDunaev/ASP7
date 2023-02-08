using Dentistry.Data;
using Dentistry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ���������.
var connectionIdentityString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("������ ����������� 'IdentityConnection' �� �������.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionIdentityString));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("������ ����������� 'DefaultConnection' �� �������.");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;               // ����������� �����
    options.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
    options.Password.RequireLowercase = false;         // ��������� �� ������� � ������ ��������
    options.Password.RequireUppercase = false;         // ��������� �� ������� � ������� ��������
    options.Password.RequireDigit = false;             // ��������� �� �����
    options.User.RequireUniqueEmail = true;            // ���������� email
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