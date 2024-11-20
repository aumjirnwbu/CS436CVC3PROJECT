using CS436CVC3PROJECT.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// ตั้งค่า DbContext เพื่อเชื่อมต่อกับฐานข้อมูล
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ตั้งค่า Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();  // ใช้ ApplicationDbContext เป็นที่จัดเก็บข้อมูลของ Identity

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // สำหรับการจัดการข้อผิดพลาดในช่วงพัฒนา

builder.Services.AddControllersWithViews();

// ลงทะเบียน EmailService และ IEmailService ใน DI container


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // ใช้ Migration EndPoint ในระหว่างพัฒนา
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // ใช้ HSTS สำหรับการรักษาความปลอดภัยใน production
}

app.UseHttpsRedirection();  // ใช้การ redirect ไปที่ HTTPS
app.UseStaticFiles();  // ใช้ Static Files (เช่น CSS, JS, รูปภาพ)

app.UseRouting();  // เริ่มใช้ routing

app.UseAuthorization();  // เริ่มใช้ authorization สำหรับการตรวจสอบสิทธิ์

app.MapControllerRoute(
    name: "default",
     pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();  // ใช้ Razor Pages

app.Run();  // เริ่มแอปพลิเคชัน
