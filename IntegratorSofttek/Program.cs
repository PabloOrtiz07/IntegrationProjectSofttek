using IntegratorSofttek.DataAccess;
using IntegratorSofttek.DataAccess.DatabaseSeeding;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Add some registers 



//Apply  dependency injection
builder.Services.AddScoped<IUnitOfWork,UnitOfWorkService>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<WorkMapper>();
builder.Services.AddScoped<ProjectMapper>();
builder.Services.AddScoped<ServiceMapper>();
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
