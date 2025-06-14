using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // ✅ Bắt buộc để dùng MapControllers()
// Đăng ký CloudinarySettings từ appsettings.json
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

// Đăng ký Cloudinary để inject vào controller (nếu muốn dùng DI)
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
    return new Cloudinary(account);
});

// Đăng ký Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CloudinaryDemo API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseRouting();


app.MapControllers();


app.Run();

