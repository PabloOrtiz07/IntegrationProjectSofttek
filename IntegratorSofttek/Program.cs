using IntegratorSofttek.DataAccess;
using IntegratorSofttek.DataAccess.DatabaseSeeding;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autorizacion JWT",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type= ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new string[]{ }
                    }
                });

});
builder.Services.AddDbContext<ContextDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Add some registers and new features



//Apply  dependency injection
builder.Services.AddScoped<IUnitOfWork,UnitOfWorkService>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<WorkMapper>();
builder.Services.AddScoped<ProjectMapper>();
builder.Services.AddScoped<ServiceMapper>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
               ValidateIssuer = false,
               ValidateAudience = false
           }); ;

builder.Services.AddScoped<IUnitOfWork, UnitOfWorkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
