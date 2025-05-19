using AccessControlSystem.Application.Commands;
using AccessControlSystem.Application.External;
using AccessControlSystem.Infrastructure.Persistence;
using AccessControlSystem.Infrastructure.Search;
using AccessControlSystem.Infrastructure.Messaging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Nest;
using Microsoft.Extensions.Options;
using AccessControlSystem.Application.Mappings;
using Confluent.Kafka;
using Elasticsearch.Net;
using AccessControlSystem.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));
builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));
builder.Services.AddScoped(typeof(AccessControlSystem.Domain.Repositories.IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(AccessControlSystem.Domain.Repositories.IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();


builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
{
    var bootstrapServers = builder.Configuration.GetValue<string>("KafkaSettings:BootstrapServers");

    var config = new ProducerConfig
    {
        BootstrapServers = bootstrapServers
    };
    return new ProducerBuilder<Null, string>(config).Build();
});
builder.Services.AddSingleton<IElasticsearchService, ElasticsearchService>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreatePermissionTypeHandler>();
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AccessControl API",
        Version = "v1"
    });
});

builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));

builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ElasticSettings>>().Value;

    var connectionSettings = new ConnectionSettings(new Uri(settings.Uri))
        .DefaultIndex(settings.DefaultIndex)
        .BasicAuthentication(settings.Username, settings.Password)
        .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
    return new ElasticClient(connectionSettings);
});

builder.Services.AddSingleton<IElasticsearchService, ElasticsearchService>();
builder.WebHost.UseUrls("http://0.0.0.0:5000");

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
