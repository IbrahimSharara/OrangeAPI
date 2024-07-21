using Microsoft.EntityFrameworkCore;
using OrangeTask.BLL.Interfaces;
using OrangeTask.BLL.Services;
using OrangeTask.DAL.DataSeeding;
using OrangeTask.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddDbContext<OrangeTaskContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});
builder.Services.AddCors(
    op =>
    {
        op.AddPolicy("AllowAllOrigins", builder =>
        {
            builder.AllowAnyMethod()
                   .AllowAnyHeader()
            .AllowAnyOrigin();

        });
    });
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
app.UseCors("AllowAllOrigins");
using var scop = app.Services.CreateScope();
var services = scop.ServiceProvider;
var context = services.GetRequiredService<OrangeTaskContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await SeedingData.SeedAsync(context);
}
catch (Exception ex)
{

    logger.LogError(ex, "Error While Seeding");
}
app.Run();