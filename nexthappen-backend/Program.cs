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
using nexthappen_backend.EventDiscovery.Application.Services;
using nexthappen_backend.EventDiscovery.Application.Usecases;
using nexthappen_backend.EventDiscovery.Domain.Entities;
using nexthappen_backend.EventDiscovery.Infrastructure.Persistence.Repositories;
using nexthappen_backend.SavedEvents.Domain.Entities;
using nexthappen_backend.SavedEvents.Infrastructure.Persistence.Repositories;
using nexthappen_backend.SavedEvents.Application.Services;
using nexthappen_backend.SavedEvents.Application.UseCases;
using nexthappen_backend.Tickets.Domain.Entities;
using nexthappen_backend.Tickets.Infrastructure.Persistence.Repositories;
using nexthappen_backend.Tickets.Application.Services;
using nexthappen_backend.Tickets.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Controllers
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Swagger SIEMPRE, también en Production
builder.Services.AddSwaggerGen(options => { options.EnableAnnotations(); });

// Application Services & Handlers
builder.Services.AddScoped<IManageEventRepository, ManageEventRepository>();
builder.Services.AddScoped<ManageEventService>();
builder.Services.AddScoped<GetAllEventsHandler>();
builder.Services.AddScoped<UpdateEventHandler>();
builder.Services.AddScoped<DeleteEventHandler>();

builder.Services.AddScoped<IDiscoveryEventRepository, DiscoveryEventRepository>();
builder.Services.AddScoped<EventDiscoveryService>();
builder.Services.AddScoped<GetPublicEventsHandler>();

builder.Services.AddScoped<ISavedEventRepository, SavedEventRepository>();
builder.Services.AddScoped<SavedEventsService>();
builder.Services.AddScoped<GetSavedEventsHandler>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<TicketsService>();
builder.Services.AddScoped<PurchaseTicketHandler>();
builder.Services.AddScoped<GetUserTicketsHandler>();
builder.Services.AddScoped<GetTicketByIdHandler>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<CreateEventService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// MySQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 32)),
        mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)
    );
});

var app = builder.Build();

// ENDPOINT raíz para Render (evita 404)
app.MapGet("/", () => Results.Ok("NextHappen API is running"));

// CORS antes de controllers
app.UseCors("AllowFrontend");

// Swagger SIEMPRE visible
app.UseSwagger();
app.UseSwaggerUI();

// Quita HTTPS redirection (Render no lo usa internamente)
app.UseAuthorization();

app.MapControllers();

app.Run();
