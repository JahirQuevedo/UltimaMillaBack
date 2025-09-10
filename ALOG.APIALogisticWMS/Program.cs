
using ALOG.Repositorios;
using ALOG.Repositorios.Data;
using ALOG.Repositorios.Repositorio.Control;
using ALOG.Repositorios.Repositorio.Control.IControl;
using ALOG.Repositorios.Repositorio.Generico;
using ALOG.Repositorios.Repositorio.Generico.IGenerico;
using ALOG.Repositorios.Repositorio.IRepoOrdenes;
using ALOG.Repositorios.Repositorio.RepoOrdenes.IRepoOrdenes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

//Configrar conexi�n SQLServer
builder.Services.AddDbContext<ApplicationDbContext>(
    opciones =>
    {
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlDesa"));
        //PRODQA
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlProdQA"));
        //PROD
        opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlQAntonio"));
        //LOCALIIS
        //opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlIIS"));
    }
);

builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddScoped<IReporteadorInventarioRepositorio, ReporteadorInventarioRepositorio>();
builder.Services.AddScoped<IReporteadorLiberacionRepositorio, ReporteadorLiberacionRepositorio>();
builder.Services.AddScoped<ILiberacionRepositorio, LiberacionRepositorio>();
builder.Services.AddScoped<IReferenciaBookingBLRepositorio, ReferenciaBookingBLRepositorio>();
builder.Services.AddScoped<IInventararioAlmacenRepositorio, InventarioAlmacenRepositorio>();
builder.Services.AddScoped<IBitacoraAveriaRepositorio, BitacoraAveriaRepositorio>();
builder.Services.AddScoped<ITipoSeveridadRepositorio, TipoSeveridadRepositorio>();
builder.Services.AddScoped<ITipoDesperfectoRepositorio, TipoDesperfectoRepositorio>();
builder.Services.AddScoped<ICodigoDesperfectoRepositorio, CodigoDesperfectoRepositorio>();
builder.Services.AddScoped<IReporteadorTarjaRepositorio, ReporteadorTarjaRepositorio>();
builder.Services.AddScoped<IServicioFotograficoRepositorio, ServicioFotograficoRepositorio>();
builder.Services.AddScoped<IRecepcionRepositorio, RecepcionRepositorio>();
builder.Services.AddScoped<IFolioServicioRepositorio, FolioServicioRepositorio>();
builder.Services.AddScoped<ISolicitudIngresoRepositorio, SolicitudIngresoRepositorio>();
builder.Services.AddScoped<ISolicitudTrasladoRepositorio, SolicitudTrasladoRepositorio>();
builder.Services.AddScoped<IManiobristaRepositorio, ManiobristaRepositorio>();
builder.Services.AddScoped<ILineaOperadorRepositorio, LineaOperadorRepositorio>();
builder.Services.AddScoped<ILineaTransportistaRepositorio, LineaTransportistaRepositorio>();
builder.Services.AddScoped<ITipoTransporteRepositorio, TipoTransporteRepositorio>();
builder.Services.AddScoped<IControlTransporteRepositorio, ControlTransporteRepositorio>();
builder.Services.AddScoped<IUbicacionAlmacenRepositorio, UbicacionAlmacenRepositorio>();
builder.Services.AddScoped<IZonaAlmacenRepositorio, ZonaAlmacenajeRepositorio>();
builder.Services.AddScoped<IPartidaRepositorio, PartidaRepositorio>();
builder.Services.AddScoped<ITarjaRepositorio, TarjaRepositorio>();
builder.Services.AddScoped<IReferenciaRepositorio, ReferenciaRepositorio>();
builder.Services.AddScoped<IServicioWMSRepositorio, ServicioWMSRepositorio>();
builder.Services.AddScoped<IPaqueteRepositorio, PaqueteRepositorio>();
builder.Services.AddScoped<IViajeRepositorio, ViajeRepositorio>();
builder.Services.AddScoped<IBarcoRepositorio, BarcoRepositorio>();
builder.Services.AddScoped<IOrdenesRepositorio, OrdenesRepositorio>();
builder.Services.AddScoped(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>));
builder.Services.AddScoped<UnitOfWorkWMS>();
builder.Services.AddScoped<IControlSistemaRepositorio, ControlSistemaRepositorio>();
builder.Services.AddScoped<IOrdenesRepositorio, OrdenesRepositorio>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//Agregar el automapper
//builder.Services.AddAutoMapper(typeof(VaciosMapper));

//Autenticacion
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
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer. \r\n\r\n " +
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
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
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

