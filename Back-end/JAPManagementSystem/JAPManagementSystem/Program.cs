using JAPManagementSystem.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterDBContext(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors(RegisterCorsExtension.origin);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
