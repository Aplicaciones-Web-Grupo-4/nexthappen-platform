using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using nexthappen_backend.Shared.Infrastructure.Interfaces.ASP.Configuration;
using nexthappen_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using nexthappen_backend.CreateEvent.Application.Services;
using nexthappen_backend.CreateEvent.Domain.Entities;
using nexthappen_backend.CreateEvent.Infrastructure.Persistence.Repositories;
using nexthappen_backend.ManageEvent.Application.Services;
using nexthappen_backend.ManageEvent.Application.UseCases;
using nexthappen_backend.ManageEvent.Domain;
using nexthappen_backend.ManageEvent.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddSwaggerGen(options => { options.EnableAnnotations(); });

builder.Services.AddScoped<IManageEventRepository, ManageEventRepository>();
builder.Services.AddScoped<ManageEventService>();
builder.Services.AddScoped<GetAllEventsHandler>();
builder.Services.AddScoped<UpdateEventHandler>();
builder.Services.AddScoped<DeleteEventHandler>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(9, 0, 0)), // versión de MySQL de tu entorno (ajústala si es diferente)
        mySqlOptions =>
        {
            mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore); // evita errores de esquema
        });
});

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<CreateEventService>();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();