using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using Task_Management_Api.Data;
using Task_Management_Api.Mappings;
using Task_Management_Api.Middleware;
using Task_Management_Api.Repositories;
using Task_Management_Api.Repositories.Interfaces;
using Task_Management_Api.Services;
using Task_Management_Api.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetValue<string>("AllwedOrigins");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
//
builder.Services.AddSwaggerGen(options =>
{
    // Creates the "Authorize" button in Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    // Applies Bearer authentication to Swagger requests
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddDbContext<AppDbContext>(option =>
 option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// add mapping Service
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

// add Cors for Vuejs project
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyVueAppPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
    });
});

// Add Authentiction
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme
    ).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

var app = builder.Build();
// CORS
app.UseCors("MyVueAppPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();

    app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AppMiddleware>();
app.MapControllers();

app.Run();