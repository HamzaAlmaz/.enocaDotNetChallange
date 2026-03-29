using Core.Application.Interfaces.BackgroundJobs;
using Infrastructure.Persistence.BackgroundJobs;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Services;
using FluentValidation;
using Hangfire;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Core.Application"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

IServiceCollection serviceCollection = builder.Services.AddScoped<IReportJob, ReportJob>();
builder.Services.AddScoped<ICarrierRepository, CarrierRepository>();
builder.Services.AddScoped<ICarrierService, CarrierService>();
builder.Services.AddScoped<ICarrierConfigurationRepository, CarrierConfigurationRepository>();
builder.Services.AddScoped<ICarrierConfigurationService, CarrierConfigurationService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHangfireDashboard();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<IReportJob>(
        "hourly-carrier-report",
        job => job.ExecuteReportAsync(),
        Cron.Hourly);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();