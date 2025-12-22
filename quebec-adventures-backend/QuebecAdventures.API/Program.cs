using Microsoft.EntityFrameworkCore;
using QuebecAdventures.Infrastructure.Data;
using QuebecAdventures.Infrastructure.Data.DbContext;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QuebecAdventuresDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("QuebecAdventuresDb")));


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
								.AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();
app.Run();
