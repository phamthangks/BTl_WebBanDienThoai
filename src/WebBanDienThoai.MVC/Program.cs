using BTLW_BDT.Helpers;
using BTLW_BDT.Models;
using BTLW_BDT.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using BTLW_BDT.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký các dịch vụ cần thiết
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSignalR();

// Đăng ký DbContext trong DI container
builder.Services.AddDbContext<BtlLtwQlbdtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BtlLtwQlbdtContext"))
    .EnableDetailedErrors());

// Đăng ký Repository và Service trong DI container
builder.Services.AddScoped<IVnPayService, VnPayService>();

// Đăng ký IHttpClientFactory
builder.Services.AddHttpClient();  // Thêm dòng này để đăng ký IHttpClientFactory

// Cấu hình Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của session
    options.Cookie.HttpOnly = true;                 // Cookie chỉ có thể truy cập qua HTTP (bảo mật)
    options.Cookie.IsEssential = true;              // Cookie là cần thiết
});



// Đăng ký IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddMemoryCache();
builder.Services.AddControllers()
   .AddJsonOptions(options =>
   {
       options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
       options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
   });
var app = builder.Build();



builder.Services.AddAuthorization();
// tao router cho chi tiet san pham
app.MapControllerRoute(
    name: "productDetail",
    pattern: "san-pham/{id}",
    defaults: new { controller = "Home", action = "ProductDetail" }
);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "productDetail",
        pattern: "san-pham/{id}",
        defaults: new { controller = "Home", action = "ProductDetail" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=access}/{action=login}/{id?}");
        
    endpoints.MapHub<ChatHub>("/chatHub");
});


app.Run();
