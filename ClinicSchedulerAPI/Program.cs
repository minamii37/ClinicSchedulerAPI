using ClinicScheduler.Application.IServices;
using ClinicScheduler.Domain.IRepositories;
using ClinicScheduler.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Transient：使い捨て
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IPrivateScheduleService, PrivateScheduleService>();
builder.Services.AddTransient<IPublicScheduleService, PublicScheduleService>();
builder.Services.AddTransient<IReservateService, ReservationService>();

builder.Services.AddTransient<IPatientRepository, PatientRepository>();
builder.Services.AddTransient<IPrivateScheduleRepository, PrivateScheduleRepository>();
builder.Services.AddTransient<IPublicScheduleRepository, PublicScheduleRepository>();
builder.Services.AddTransient<IReservateRepository, ReservateRepository>();

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

