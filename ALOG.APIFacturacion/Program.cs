using System.Text;
using System.Text.Json.Serialization;
using ALOG.APIFacturacion.Mapper;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Control;
using ALOG.Repositorios.Repositorio.Control.IControl;
using ALOG.Repositorios.Repositorio.IRepoOrdenes;
using ALOG.Repositorios.Repositorio.Provision;
using ALOG.Repositorios.Repositorio.Provision.IProvision;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Configrar conexi�n SQLServer
builder.Services.AddDbContext<ApplicationDbContext>(
    opciones =>
    {
        //LOCAL
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
        //LOCALIIS
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlIIS"));
        //PROD
        opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlQAntonio"));

        //PRODQA
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlProdQA"));
    }
);

//Agregamos los repositorios
//builder.Services.AddScoped<IServicioRepositorio, ServiciosRepository>();
builder.Services.AddScoped<IProvisionesEncRepositorio, ProvisionesEncRepositorio>();
builder.Services.AddScoped<ICatSistemasRepositorio, CatSistemasRepositorio>();
builder.Services.AddScoped<IControlSistemaRepositorio, ControlSistemaRepositorio>();
builder.Services.AddScoped<ICatUsuariosRepositorio, CatUsuariosRepositorio>();
builder.Services.AddScoped<IOrdenesRepositorio, OrdenesRepositorio>();
builder.Services.AddScoped<IControlSistemaRepositorio, ControlSistemaRepositorio>();
var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");
//Agregar el automapper
builder.Services.AddAutoMapper(typeof(Mapper));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication
    (
        x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Add services.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticaci�n JWT usando el esquema Bearer. \r\n\r\n " +
        "Ingresa la palabra 'Bearer' seguido de un [espacio] y despu�s su token en el campo de abajo.\r\n\r\n" +
        "Ejemplo: \"Bearer tkljk125jhhk\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
});

//Soporte para CORS
//Se pueden habilitar: 1-Un dominio, 2-multiples dominios,
//3-cualquier dominio (Tener en cuenta seguridad)
//Usamos de ejemplo el dominio: http://localhost:3223, se debe cambiar por el correcto
//Se usa (*) para todos los dominios
builder.Services.AddCors(p => p.AddPolicy("PoliticaCors", build =>
{
    //build.WithOrigins("http://localhost:3223").AllowAnyMethod().AllowAnyHeader();
    //build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    build.WithOrigins("*") // Permitir el cliente Blazor
             .AllowAnyMethod()
             .AllowAnyHeader()
             ;
}));


// Agrega soporte para referencias c�clicas en JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull; // opcional, para ignorar valores nulos

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSwagger();
//app.UseSwaggerUI();
//Soporte para CORS
app.UseCors("PoliticaCors");

//Soporte para Autenticaci�n
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();