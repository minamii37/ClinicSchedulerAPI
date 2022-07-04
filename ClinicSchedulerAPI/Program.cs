using ClinicScheduler.Application.IServices;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Transient：使い捨て
builder.Services.AddTransient<IPublicScheduleService, PublicScheduleService>();
builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();

builder.Services.AddControllers();
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

