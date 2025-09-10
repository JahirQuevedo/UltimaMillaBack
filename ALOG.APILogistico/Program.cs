using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Catalogos;
using ALOG.Repositorios.Repositorio.Catalogos.ICatalogo;
using ALOG.Repositorios.Repositorio.Control;
using ALOG.Repositorios.Repositorio.Control.IControl;
using ALOG.Repositorios.Repositorio.DtLogistica;
using ALOG.Repositorios.Repositorio.DtLogistica.IDtLogistica;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.IRepoOrdenes;
using ALOG.Repositorios.Repositorio.Logistica;
using ALOG.Repositorios.Repositorio.Logistica.ILogisticosRepositorio;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using ApiVacios.VaciosMapper;
//using ALOG.Repositorios.Repositorio.IRepositorio;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using ALOG.Repositorios.Repositorio.RutasArchivos;
using ALOG.Repositorios.Utilerias.IUtileria;
using ALOG.Repositorios.Utilerias;
using ApiVacios.Controllers.CtrllIntegrar1G;
using ALOG.Repositorios.Repositorio.Integración1G.IIntegracion1G;
using ALOG.Repositorios.Repositorio.Integración1G;
using ALOG.Repositorios.Repositorio.Integracion1G.IIntegracion1G;
using ALOG.Repositorios.Repositorio.Integracion1G;

var builder = WebApplication.CreateBuilder(args);
//Configrar conexi�n SQLServer
builder.Services.AddDbContext<ApplicationDbContext>(
    opciones =>
    {
        //LOCAL
        opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));

        //LOCALIIS
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlIIS"));

        //PROD
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlProd"));

        //PRODQA
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlProdQA"));
    }
    );
//Agregamos los repositorios
//builder.Services.AddScoped<IServicioRepositorio, ServiciosRepository>();
builder.Services.AddScoped<IOrdenesRepositorio, OrdenesRepositorio>();
builder.Services.AddScoped<IVaciosRepositorio, VaciosRepository>();
builder.Services.AddScoped<ICatSistemasRepositorio, CatSistemasRepositorio>();
builder.Services.AddScoped<ICatUsuariosRepositorio, CatUsuariosRepositorio>();
builder.Services.AddScoped<IDtUltimaMillaEncRepositorio, DtUltimaMillaEncRepositorio>();
builder.Services.AddScoped<IDtUltimaMillaDetRepositorio, DtUltimaMillaDetRepositorio>();
builder.Services.AddScoped<IDtAcarreoRepositorio, DtAcarreoRepositorio>();
builder.Services.AddScoped<IPeticionesContenedoresRepo, PeticionesContenedoresRepo>();
builder.Services.AddScoped(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>));
builder.Services.AddScoped<IControlAccesoRepo, ControlAccesoRepo>();
builder.Services.AddScoped<IIntegraReferenciaRepo, IntegraReferenciaRepo>();
builder.Services.AddScoped<IIntegraFacturaEncRepo, IntegraFacturaEncRepo>();
builder.Services.AddScoped<IIntegraFacturaDetRepo, IntegraFacturaDetRepo>();
builder.Services.AddScoped<IIntegraFacturaEstRepo, IntegraFacturaEstRepo>();
builder.Services.AddScoped<IIntegraAnticiposSolRepo, IntegraAnticiposSolRepo>();
builder.Services.AddScoped<IIntegracion1GRepo, Integracion1GRepo>();
builder.Services.Configure<RutasArchivosRepositorio>(builder.Configuration.GetSection("RutasArchivos"));
builder.Services.AddScoped<IControlSistemaRepositorio, ControlSistemaRepositorio>();
builder.Services.AddScoped<IPeticionesContenedoresRepo, PeticionesContenedoresRepo>();
builder.Services.AddScoped<IPeticionesContenedorCronRepo, PeticionesContenedoresCronRepo>();
builder.Services.AddScoped<IUtileriaExportacionExcel, UtileriaExportacionExcel>();
builder.Services.AddScoped<IPeticionesReferenciasSLORepo, PeticionesReferenciasSLORepo>();
builder.Services.AddScoped<ISLOSolicitudesRepositorio, SLOSolicitudesRepositorio>();


var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");
//Agregar el automapper
builder.Services.AddAutoMapper(typeof(VaciosMapper));
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
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
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
    //build.WithOrigins("http://localhost", "https://localhost", "*") // Permitir el cliente Blazor
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
