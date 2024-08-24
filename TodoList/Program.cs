using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using TodoList.Endpoints;
using TodoList.Repositories;
using TodoList.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo List", Version = "v1" });
  var security = new OpenApiSecurityScheme
  {
    Name = HeaderNames.Authorization,
    Type = SecuritySchemeType.ApiKey,
    In = ParameterLocation.Header,
    Description = "JWT Auth header",
    Reference = new OpenApiReference
    {
      Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme
    }
  };
  options.AddSecurityDefinition(security.Reference.Id, security);
  options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, Array.Empty<string>() } }
  );
});

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connString));

builder.Services.AddScoped<TodoListService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.RegisterTodoListEndpoints();
app.RegisterUserEndpoints();

app.Run();
