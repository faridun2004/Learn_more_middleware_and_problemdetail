using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RegisterService.Common.Behaviors;
using RegisterService.Data;
using RegisterService.Exceptions;
using RegisterService.UseCases.Users.V2.Commands.CreateUser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging(); 
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandV2Validator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Формат: v1, v2, v1.0, v2.1 и т.д.
    options.SubstituteApiVersionInUrl = true; // Автоматически подставляет версию в URL
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User API v1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "User API v2", Version = "v2" });
});

builder.Services.AddDbContext<AppDbContext>(con => con.UseNpgsql(builder.Configuration["ConnectionString"]));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<AppDbContext>();
    if (context != null)
    {
        context.Database.Migrate();
    }
    else
    {
        throw new InvalidOperationException("PersonContext could not be resolved from the service provider.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
}
app.UseExceptionHandler();

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
