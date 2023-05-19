using NET.Bussiness.Base;
using NET.Bussiness.Dictionary;
using NET.Bussiness.Interfaces;
using NET.DataLayer.Base;
using NET.DataLayer.Dictionary;
using NET.DataLayer.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var service = builder.Services;

service.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
service.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
service.AddScoped(typeof(IDbContext<>), typeof(NET.DataLayer.DbContexts.DbContext<>));

service.AddScoped<IProductDL, ProductDL>();
service.AddScoped<IProductBL, ProductBL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
