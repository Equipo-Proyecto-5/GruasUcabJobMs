using GruasUcabJobMs.Controllers;
using Hangfire;
using Hangfire.PostgreSql;
using JobMs.Core.Services;
using JobMs.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var applicationAssembly = Assembly.Load("JobMs.Application");
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));


builder.WebHost.ConfigureKestrel(options =>
{
   options.ListenAnyIP(5235); // Puerto HTTP
    options.ListenAnyIP(7100); // Puerto HTTPS
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IOrdersMsService, OrdersMsService>(client =>
{
    client.BaseAddress = new Uri("http://ec2-18-216-27-227.us-east-2.compute.amazonaws.com:5101/swagger");
});
builder.Services.AddHttpClient<IProvidersMsService, ProvidersMsService>(client =>
{
    client.BaseAddress = new Uri("http://ec2-3-17-11-0.us-east-2.compute.amazonaws.com:5039/swagger");
});
builder.Services.AddHttpClient<IUserMsService, UsersMsService>(client =>
{
    client.BaseAddress = new Uri("http://ec2-3-145-211-144.us-east-2.compute.amazonaws.com:5163/swagger");
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
RecurringJob.AddOrUpdate<JobsController>("asignacion-vencida-localizacion-grua", service => service.GruasExpiradas(), "*/30 * * * * *"); // Adjust the schedule as needed
RecurringJob.AddOrUpdate<JobsController>("procesar-envio-notificacion", service => service.ProcesarEnvioNotificacion(), Cron.Minutely); // Adjust the schedule as needed

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();