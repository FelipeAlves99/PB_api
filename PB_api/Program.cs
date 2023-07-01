using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PB.Domain.Notifications;
using PB.Domain.Repositories;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;
using PB.Domain.Shared.UnitOfWork;
using PB.Infra.Data.Context;
using PB.Infra.Data.Repositories;
using PB.Infra.Data.UnitOfWork;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services
//    .AddDbContext<PbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

builder.Services
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), AppDomain.CurrentDomain.Load("PB.Domain")));

builder.Services
    .AddScoped<Notifications>()
    .AddScoped<IHandler, Handler>()
    .AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>()
    .AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddScoped<IClientRepository, ClientRepository>()
    .AddScoped<IPhoneRepository, PhoneRepository>();

builder.Services
    .AddScoped<PbContext>();

//TODO: add redis cache?

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services
    .AddSwaggerGenNewtonsoftSupport();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
