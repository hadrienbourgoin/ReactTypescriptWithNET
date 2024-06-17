using Microsoft.EntityFrameworkCore;
using ReactTypescriptWithNET.Server.Data;
using ReactTypescriptWithNET.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    SeedData(context);
}

app.Run();

void SeedData(ApplicationDbContext context)
{
    context.Database.Migrate();

    // Check if the database is empty and seed data
    if (!context.Forecasts.Any())
    {
        context.Forecasts.AddRange(
            new Forecast { Date = DateTime.Now.AddDays(1), TemperatureC = 25, Summary = "Mild" },
            new Forecast { Date = DateTime.Now.AddDays(2), TemperatureC = 20, Summary = "Cool" },
            new Forecast { Date = DateTime.Now.AddDays(3), TemperatureC = 30, Summary = "Warm" }
        );
        context.SaveChanges();
    }
}