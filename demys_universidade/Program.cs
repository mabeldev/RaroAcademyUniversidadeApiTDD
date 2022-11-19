#region BuilderServices


using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Interfaces.Services;
using demys_universidade.Domain.Services;
using demys_universidade.Domain.Settings;
using demys_universidade.Filters;
using demys_universidade.Handlers;
using demys_universidade.Infrastructure.Contexts;
using demys_universidade.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
    options.Filters.Add(typeof(ValidationFilter));
});

#endregion

#region Swagger

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1",
                        new Microsoft.OpenApi.Models.OpenApiInfo
                        {
                            Title = "demys_universidade",
                            Version = "v1",
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "Mabel" },
                            TermsOfService = new Uri("http://raroacademy.com")
                        });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    swagger.IncludeXmlComments(xmlPath);

});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


#endregion

#region AppSettings

var appSetting = builder.Configuration.GetSection(nameof(AppSetting)).Get<AppSetting>();
builder.Services.AddSingleton(appSetting);

#endregion

#region Mapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region Services

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();


#endregion

#region Repositories

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();


#endregion

#region Jwt

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSetting.JwtSecurityKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

#endregion

#region Authorization
builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = true;
}).AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandler>();

#endregion

#region Dbcontext

builder.Services.AddDbContext<UniversidadeContext>(options =>
{
    options.UseSqlServer(appSetting.SqlServerConnection);
    options.UseLazyLoadingProxies();
});

#endregion

#region HttpClient

builder.Services.AddHttpClient<IBrasilCepRepository, BrasilCepRepository>(options =>
{
    options.BaseAddress = new Uri(appSetting.ApiCep);
});

#endregion

#region AppConfiguration

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion