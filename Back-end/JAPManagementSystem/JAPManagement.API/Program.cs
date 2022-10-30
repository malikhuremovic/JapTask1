using Hangfire;
using JAPManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDBContext(builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterHangfire(builder.Configuration);
builder.Services.RegisterAutoMapper();
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterCors(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors(RegisterCorsExtension.origin);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

await app.StartAsync();

await app.WaitForShutdownAsync();