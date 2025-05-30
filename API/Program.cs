using System.Text;
using API.helper;
using Application.helper;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SMALLERP_API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter token",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "",
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAutoMapper(typeof(MappingConfig));
// DbContext
builder.Services.AddDbContext<SmootieerpDbConText>(options =>
     options.UseMySQL(builder.Configuration.GetConnectionString("SmootieerpContext_mysql")));

// DI สำหรับ Application และ Infrastructure
builder.Services.AddScoped<ControllerHelper>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuCategoryService, MenuCategoryService>();
builder.Services.AddScoped<IServiceFactory, ServiceFactory>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IMenuRecipeService, MenuRecipeService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();

// Services
builder.Services.AddScoped(typeof(IGenericCrudService<>), typeof(GenericCrudService<>));

// Register
builder.Services.AddSingleton<PathFile>();
builder.Services.AddScoped<UploadFile>();

//core 
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
