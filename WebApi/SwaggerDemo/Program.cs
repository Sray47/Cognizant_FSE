using Microsoft.OpenApi.Models;
using SwaggerDemo.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Swagger Demo",
        Version = "v1",
        Description = "TBD",
        TermsOfService = new Uri("http://www.example.com"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact 
        { 
            Name = "John Doe", 
            Email = "john@xyzmail.com", 
            Url = new Uri("http://www.example.com") 
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense 
        { 
            Name = "License Terms", 
            Url = new Uri("http://www.example.com") 
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // specifying the Swagger JSON endpoint.
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
    });
}

app.UseHttpsRedirection();



// Add endpoint routing middleware
app.UseRouting();

app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();
