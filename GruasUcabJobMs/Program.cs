using GruasUcabJobMs.Controllers;
using Hangfire;
using Hangfire.PostgreSql;
using JobMs.Core.Services;
using JobMs.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var applicationAssembly = Assembly.Load("JobMs.Application");
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IOrdersMsService, OrdersMsService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7057");
});
var dbConnectionStringHangfire = builder.Configuration.GetValue<string>("DBConnectionStringsHangfire");

builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseDefaultTypeSerializer()
          .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(dbConnectionStringHangfire)));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<JobsController>("asignacion-vencida-grua", service => service.OrdenesExpiradas(), "*/30 * * * * *"); // Adjust the schedule as needed

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();