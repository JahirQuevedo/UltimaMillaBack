using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALOG.APICatalogosWMS.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionTipoTransporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SLO");

            migrationBuilder.EnsureSchema(
                name: "vacios");

            migrationBuilder.EnsureSchema(
                name: "WMS");

            migrationBuilder.CreateTable(
                name: "CatNavieras",
                columns: table => new
                {
                    IdCatNaviera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatNavieras", x => x.IdCatNaviera);
                });

            migrationBuilder.CreateTable(
                name: "CatPaises",
                columns: table => new
                {
                    IdCatPaises = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ClavePaisSAT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatPaises", x => x.IdCatPaises);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoEstados",
                columns: table => new
                {
                    IdCatTipoEstados = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoEstado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoEstados", x => x.IdCatTipoEstados);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoEventosCron",
                columns: table => new
                {
                    IdCatTipoEventoCron = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRegistroUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoEventosCron", x => x.IdCatTipoEventoCron);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoIncidenciaCron",
                columns: table => new
                {
                    IdCatTipoIncidenciaCron = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRegistroUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoIncidenciaCron", x => x.IdCatTipoIncidenciaCron);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoPuesto",
                columns: table => new
                {
                    IdCatTipoPuesto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Puesto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoPuesto", x => x.IdCatTipoPuesto);
                });

            migrationBuilder.CreateTable(
                name: "CatTiposConfig",
                columns: table => new
                {
                    IdCatTiposConfig = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTiposConfig", x => x.IdCatTiposConfig);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoTransporte",
                columns: table => new
                {
                    IdCatTipoTransporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoTransporte", x => x.IdCatTipoTransporte);
                });

            migrationBuilder.CreateTable(
                name: "WMS_005_PAQUETE",
                schema: "WMS",
                columns: table => new
                {
                    nIdPaquete005 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nTipoOperacion = table.Column<int>(type: "int", nullable: true),
                    nTipoIngreso = table.Column<int>(type: "int", nullable: true),
                    nIdServicio004 = table.Column<int>(type: "int", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_005_PAQUETE", x => x.nIdPaquete005);
                });

            migrationBuilder.CreateTable(
                name: "WMS_007_TIPO_CARGA_ALMACEN",
                schema: "WMS",
                columns: table => new
                {
                    nIdTipoCargaAlmacen007 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_007_TIPO_CARGA_ALMACEN", x => x.nIdTipoCargaAlmacen007);
                });

            migrationBuilder.CreateTable(
                name: "WMS_009_TIPO_ZONA_ALMACENAJE",
                schema: "WMS",
                columns: table => new
                {
                    nIdTipoZonaAlmacenaje009 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_009_TIPO_ZONA_ALMACENAJE", x => x.nIdTipoZonaAlmacenaje009);
                });

            migrationBuilder.CreateTable(
                name: "WMS_012_UNIDAD_MEDIDA",
                schema: "WMS",
                columns: table => new
                {
                    nIdUnidadMedidad012 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_012_UNIDAD_MEDIDA", x => x.nIdUnidadMedidad012);
                });

            migrationBuilder.CreateTable(
                name: "WMS_013_TIPO_EMBALAJE",
                schema: "WMS",
                columns: table => new
                {
                    nIdTipoEmbalaje013 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_013_TIPO_EMBALAJE", x => x.nIdTipoEmbalaje013);
                });

            migrationBuilder.CreateTable(
                name: "WMS_020_TIPO_TRANSPORTE",
                schema: "WMS",
                columns: table => new
                {
                    nIdTipoTransporte020 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nTipo = table.Column<int>(type: "int", nullable: false),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_020_TIPO_TRANSPORTE", x => x.nIdTipoTransporte020);
                });

            migrationBuilder.CreateTable(
                name: "WMS_023_MANIOBRISTA",
                schema: "WMS",
                columns: table => new
                {
                    nIdManiobrista023 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sRazonSocial = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    sRFC = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    sNombreCorto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    sResponsable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    sIdCamir = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    bRecintoFiscalizado = table.Column<bool>(type: "bit", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_023_MANIOBRISTA", x => x.nIdManiobrista023);
                });

            migrationBuilder.CreateTable(
                name: "WMS_029_CODIGO_DESPERFECTO",
                schema: "WMS",
                columns: table => new
                {
                    nIdCodigoDesperfecto029 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_029_CODIGO_DESPERFECTO", x => x.nIdCodigoDesperfecto029);
                });

            migrationBuilder.CreateTable(
                name: "WMS_030_TIPO_DESPERFECTO",
                schema: "WMS",
                columns: table => new
                {
                    nIdTipoDesperfecto030 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_030_TIPO_DESPERFECTO", x => x.nIdTipoDesperfecto030);
                });

            migrationBuilder.CreateTable(
                name: "WMS_031_TIPO_SEVERIDAD",
                schema: "WMS",
                columns: table => new
                {
                    nIdTipoSeveridad031 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_031_TIPO_SEVERIDAD", x => x.nIdTipoSeveridad031);
                });

            migrationBuilder.CreateTable(
                name: "WMS_033_AGENTE_ADUANAL",
                schema: "WMS",
                columns: table => new
                {
                    nIdAgenteAduanal033 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sPatente = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    sNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sRFC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sAlias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sCURP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    bEsAgenteDeCarga = table.Column<bool>(type: "bit", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_033_AGENTE_ADUANAL", x => x.nIdAgenteAduanal033);
                });

            migrationBuilder.CreateTable(
                name: "CatPaisEstados",
                columns: table => new
                {
                    IdCatPaisEstados = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatPais = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodEstadoSAT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatPaisEstados", x => x.IdCatPaisEstados);
                    table.ForeignKey(
                        name: "FK_CatPaisEstados_CatPaises_IdCatPais",
                        column: x => x.IdCatPais,
                        principalTable: "CatPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_003_BARCO",
                schema: "WMS",
                columns: table => new
                {
                    nIdBarco003 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nIdCatPais = table.Column<int>(type: "int", nullable: true),
                    nIdCatNaviera = table.Column<int>(type: "int", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_003_BARCO", x => x.nIdBarco003);
                    table.ForeignKey(
                        name: "FK_WMS_003_BARCO_CatNavieras_nIdCatNaviera",
                        column: x => x.nIdCatNaviera,
                        principalTable: "CatNavieras",
                        principalColumn: "IdCatNaviera");
                    table.ForeignKey(
                        name: "FK_WMS_003_BARCO_CatPaises_nIdCatPais",
                        column: x => x.nIdCatPais,
                        principalTable: "CatPaises",
                        principalColumn: "IdCatPaises");
                });

            migrationBuilder.CreateTable(
                name: "CatTipoIncidenciaEvento",
                columns: table => new
                {
                    IdCatTipoIncidenciaEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatTipoEventoCron = table.Column<int>(type: "int", nullable: false),
                    IdCatTipoIncidenciaCron = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoIncidenciaEvento", x => x.IdCatTipoIncidenciaEvento);
                    table.ForeignKey(
                        name: "FK_CatTipoIncidenciaEvento_CatTipoEventosCron_IdCatTipoEventoCron",
                        column: x => x.IdCatTipoEventoCron,
                        principalTable: "CatTipoEventosCron",
                        principalColumn: "IdCatTipoEventoCron",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatTipoIncidenciaEvento_CatTipoIncidenciaCron_IdCatTipoIncidenciaCron",
                        column: x => x.IdCatTipoIncidenciaCron,
                        principalTable: "CatTipoIncidenciaCron",
                        principalColumn: "IdCatTipoIncidenciaCron",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatUsuarios",
                columns: table => new
                {
                    IdCatUsuarios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CURP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    passSistema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    salt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdCatTipoPuesto = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatUsuarios", x => x.IdCatUsuarios);
                    table.ForeignKey(
                        name: "FK_CatUsuarios_CatTipoPuesto_IdCatTipoPuesto",
                        column: x => x.IdCatTipoPuesto,
                        principalTable: "CatTipoPuesto",
                        principalColumn: "IdCatTipoPuesto");
                });

            migrationBuilder.CreateTable(
                name: "WMS_008_ALMACEN",
                schema: "WMS",
                columns: table => new
                {
                    nIdAlmacen008 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    nIdTipoCargaAlmacen007 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_008_ALMACEN", x => x.nIdAlmacen008);
                    table.ForeignKey(
                        name: "FK_WMS_008_ALMACEN_WMS_007_TIPO_CARGA_ALMACEN_nIdTipoCargaAlmacen007",
                        column: x => x.nIdTipoCargaAlmacen007,
                        principalSchema: "WMS",
                        principalTable: "WMS_007_TIPO_CARGA_ALMACEN",
                        principalColumn: "nIdTipoCargaAlmacen007");
                });

            migrationBuilder.CreateTable(
                name: "CatAduana",
                columns: table => new
                {
                    IdCatAduana = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aduana = table.Column<int>(type: "int", nullable: false),
                    Seccion = table.Column<int>(type: "int", nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    IdCatPais = table.Column<int>(type: "int", nullable: false),
                    IdCatPaisEstados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatAduana", x => x.IdCatAduana);
                    table.ForeignKey(
                        name: "FK_CatAduana_CatPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "CatPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatAduana_CatPaises_IdCatPais",
                        column: x => x.IdCatPais,
                        principalTable: "CatPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatAduana_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatClientes",
                columns: table => new
                {
                    IdCatCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroExterior = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Colonia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroInterior = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Acronimo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdCatPaises = table.Column<int>(type: "int", nullable: false),
                    IdCatPaisEstados = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    RegimenFiscalSAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsoCFDISAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rfc1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatClientes", x => x.IdCatCliente);
                    table.ForeignKey(
                        name: "FK_CatClientes_CatPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "CatPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientes_CatPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "CatPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientes_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatContenedor",
                columns: table => new
                {
                    IdCatContenedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nomenclatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatContenedor", x => x.IdCatContenedor);
                    table.ForeignKey(
                        name: "FK_CatContenedor_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatDocumentos",
                columns: table => new
                {
                    IdCatDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatDocumentos", x => x.IdCatDocumento);
                    table.ForeignKey(
                        name: "FK_CatDocumentos_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatEmpresas",
                columns: table => new
                {
                    IdCatEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroExterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Colonia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroInterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatEmpresas", x => x.IdCatEmpresa);
                    table.ForeignKey(
                        name: "FK_CatEmpresas_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatLineaNegocio",
                columns: table => new
                {
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatLineaNegocio", x => x.IdCatLineaNegocio);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocio_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatPatios",
                columns: table => new
                {
                    IdCatPatios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatPatios", x => x.IdCatPatios);
                    table.ForeignKey(
                        name: "FK_CatPatios_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatPermisos",
                columns: table => new
                {
                    IdCatPermisos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatPermisos", x => x.IdCatPermisos);
                    table.ForeignKey(
                        name: "FK_CatPermisos_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatProveedores",
                columns: table => new
                {
                    IdCatProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroExterior = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Colonia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroInterior = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Clave1G = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdCatPaises = table.Column<int>(type: "int", nullable: false),
                    IdCatPaisEstados = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatProveedores", x => x.IdCatProveedor);
                    table.ForeignKey(
                        name: "FK_CatProveedores_CatPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "CatPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedores_CatPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "CatPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedores_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatReferenciaEstado",
                columns: table => new
                {
                    IdCatReferenciaEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatReferenciaEstado", x => x.IdCatReferenciaEstado);
                    table.ForeignKey(
                        name: "FK_CatReferenciaEstado_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatRoles",
                columns: table => new
                {
                    IdCatRoles = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatRoles", x => x.IdCatRoles);
                    table.ForeignKey(
                        name: "FK_CatRoles_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoContenedor",
                columns: table => new
                {
                    IdCatTipoContenedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nomenclatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoContenedor", x => x.IdCatTipoContenedor);
                    table.ForeignKey(
                        name: "FK_CatTipoContenedor_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_010_ZONA_ALMACENAJE",
                schema: "WMS",
                columns: table => new
                {
                    nIdZonaAlmacenaje010 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    nTotalFilas = table.Column<int>(type: "int", nullable: false),
                    nTotalColumnas = table.Column<int>(type: "int", nullable: false),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    nIdTipoZonaAlmacenaje009 = table.Column<int>(type: "int", nullable: false),
                    nIdAlmacen008 = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_010_ZONA_ALMACENAJE", x => x.nIdZonaAlmacenaje010);
                    table.ForeignKey(
                        name: "FK_WMS_010_ZONA_ALMACENAJE_WMS_008_ALMACEN_nIdAlmacen008",
                        column: x => x.nIdAlmacen008,
                        principalSchema: "WMS",
                        principalTable: "WMS_008_ALMACEN",
                        principalColumn: "nIdAlmacen008",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_010_ZONA_ALMACENAJE_WMS_009_TIPO_ZONA_ALMACENAJE_nIdTipoZonaAlmacenaje009",
                        column: x => x.nIdTipoZonaAlmacenaje009,
                        principalSchema: "WMS",
                        principalTable: "WMS_009_TIPO_ZONA_ALMACENAJE",
                        principalColumn: "nIdTipoZonaAlmacenaje009",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatUsuariosAduanas",
                columns: table => new
                {
                    IdCatUsuarioAduana = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCatUsuario = table.Column<int>(type: "int", nullable: false),
                    IdCatAduana = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatUsuariosAduanas", x => x.IdCatUsuarioAduana);
                    table.ForeignKey(
                        name: "FK_CatUsuariosAduanas_CatAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatUsuariosAduanas_CatUsuarios_IdCatUsuario",
                        column: x => x.IdCatUsuario,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatClientesConfig",
                columns: table => new
                {
                    IdCatClientesConfig = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatClientes = table.Column<int>(type: "int", nullable: false),
                    IdCatTipoConfig = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor1 = table.Column<double>(type: "float", nullable: false),
                    Valor2 = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatClientesConfig", x => x.IdCatClientesConfig);
                    table.ForeignKey(
                        name: "FK_CatClientesConfig_CatClientes_IdCatClientes",
                        column: x => x.IdCatClientes,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesConfig_CatTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "CatTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesConfig_CatUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatUsuariosClientes",
                columns: table => new
                {
                    IdCatUsuarioCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatUsuario = table.Column<int>(type: "int", nullable: false),
                    IdCatCliente = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatUsuariosClientes", x => x.IdCatUsuarioCliente);
                    table.ForeignKey(
                        name: "FK_CatUsuariosClientes_CatClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_CatUsuariosClientes_CatUsuarios_IdCatUsuario",
                        column: x => x.IdCatUsuario,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatServicios",
                columns: table => new
                {
                    IdCatServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCatEmpresas = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatServicios", x => x.IdCatServicio);
                    table.ForeignKey(
                        name: "FK_CatServicios_CatEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatServicios_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatSistemas",
                columns: table => new
                {
                    IdCatSistema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    userSistema = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    passSistema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    salt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdCatCliente = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresas = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatSistemas", x => x.IdCatSistema);
                    table.ForeignKey(
                        name: "FK_CatSistemas_CatClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatSistemas_CatEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatSucursales",
                columns: table => new
                {
                    IdCatSucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatSucursales", x => x.IdCatSucursal);
                    table.ForeignKey(
                        name: "FK_CatSucursales_CatEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatSucursales_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatTransportistas",
                columns: table => new
                {
                    IdCatTransportista = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroExterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Colonia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Ciudad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumeroInterior = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdCatPaises = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    IdCatPaisEstados = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTransportistas", x => x.IdCatTransportista);
                    table.ForeignKey(
                        name: "FK_CatTransportistas_CatEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatTransportistas_CatPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "CatPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatTransportistas_CatPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "CatPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatTransportistas_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_002_VIAJE",
                schema: "WMS",
                columns: table => new
                {
                    nIdViaje002 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sFolio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    nTipoOperacion = table.Column<int>(type: "int", nullable: false),
                    dFechaArriboSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaFondeo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaAtraque = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaInicioCargaDescarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaDeatraque = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaFinCargaDescarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nPedoBls = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    nIdBarco003 = table.Column<int>(type: "int", nullable: true),
                    bExterior = table.Column<bool>(type: "bit", nullable: false),
                    nReferenciaBuque = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_002_VIAJE", x => x.nIdViaje002);
                    table.ForeignKey(
                        name: "FK_WMS_002_VIAJE_CatEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_002_VIAJE_WMS_003_BARCO_nIdBarco003",
                        column: x => x.nIdBarco003,
                        principalSchema: "WMS",
                        principalTable: "WMS_003_BARCO",
                        principalColumn: "nIdBarco003");
                });

            migrationBuilder.CreateTable(
                name: "WMS_004_SERVICIO",
                schema: "WMS",
                columns: table => new
                {
                    nIdServicio004 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClaveServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nTipoOperacion = table.Column<int>(type: "int", nullable: false),
                    bEsAlmacenaje = table.Column<bool>(type: "bit", nullable: true),
                    bEsFlete = table.Column<bool>(type: "bit", nullable: true),
                    bExtraccionPatioExterno = table.Column<bool>(type: "bit", nullable: true),
                    bPaquete = table.Column<bool>(type: "bit", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_004_SERVICIO", x => x.nIdServicio004);
                    table.ForeignKey(
                        name: "FK_WMS_004_SERVICIO_CatEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatClientesLNegocio",
                columns: table => new
                {
                    IdCatClientesLNegocio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatClientesLNegocio", x => x.IdCatClientesLNegocio);
                    table.ForeignKey(
                        name: "FK_CatClientesLNegocio_CatClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesLNegocio_CatLineaNegocio_IdLineaNegocio",
                        column: x => x.IdLineaNegocio,
                        principalTable: "CatLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesLNegocio_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatProyectos",
                columns: table => new
                {
                    IdProyectos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresas = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatProyectos", x => x.IdProyectos);
                    table.ForeignKey(
                        name: "FK_CatProyectos_CatEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProyectos_CatLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "CatLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProyectos_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatPatiosConfig",
                columns: table => new
                {
                    IdCatPatiosConfig = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatPatios = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCatTipoConfig = table.Column<int>(type: "int", nullable: false),
                    Valor1 = table.Column<double>(type: "float", nullable: false),
                    Valor2 = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatPatiosConfig", x => x.IdCatPatiosConfig);
                    table.ForeignKey(
                        name: "FK_CatPatiosConfig_CatPatios_IdCatPatios",
                        column: x => x.IdCatPatios,
                        principalTable: "CatPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatPatiosConfig_CatTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "CatTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatPatiosConfig_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatPatiosNavieras",
                columns: table => new
                {
                    IdCatPatiosNavieras = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatNaviera = table.Column<int>(type: "int", nullable: false),
                    IdCatPatios = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatPatiosNavieras", x => x.IdCatPatiosNavieras);
                    table.ForeignKey(
                        name: "FK_CatPatiosNavieras_CatNavieras_IdCatNaviera",
                        column: x => x.IdCatNaviera,
                        principalTable: "CatNavieras",
                        principalColumn: "IdCatNaviera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatPatiosNavieras_CatPatios_IdCatPatios",
                        column: x => x.IdCatPatios,
                        principalTable: "CatPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatPatiosNavieras_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatUsuariosPermisos",
                columns: table => new
                {
                    IdCatUsuariosPermisos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatUsuarios = table.Column<int>(type: "int", nullable: false),
                    IdCatPermisos = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Crear = table.Column<bool>(type: "bit", nullable: false),
                    Guardar = table.Column<bool>(type: "bit", nullable: false),
                    Actualizar = table.Column<bool>(type: "bit", nullable: false),
                    Eliminar = table.Column<bool>(type: "bit", nullable: false),
                    Impresion = table.Column<bool>(type: "bit", nullable: false),
                    Exportar = table.Column<bool>(type: "bit", nullable: false),
                    Notificar = table.Column<bool>(type: "bit", nullable: false),
                    Autorizar = table.Column<bool>(type: "bit", nullable: false),
                    Enviar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatUsuariosPermisos", x => x.IdCatUsuariosPermisos);
                    table.ForeignKey(
                        name: "FK_CatUsuariosPermisos_CatPermisos_IdCatPermisos",
                        column: x => x.IdCatPermisos,
                        principalTable: "CatPermisos",
                        principalColumn: "IdCatPermisos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatUsuariosPermisos_CatUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatProveedoresConfig",
                columns: table => new
                {
                    IdCatProvConfig = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCatTipoConfig = table.Column<int>(type: "int", nullable: false),
                    Valor1 = table.Column<double>(type: "float", nullable: false),
                    Valor2 = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatProveedoresConfig", x => x.IdCatProvConfig);
                    table.ForeignKey(
                        name: "FK_CatProveedoresConfig_CatProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresConfig_CatTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "CatTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresConfig_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatProveedoresPatios",
                columns: table => new
                {
                    IdCatProveedorPatio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: false),
                    IdCatPatio = table.Column<int>(type: "int", nullable: false),
                    IdCatAduana = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatProveedoresPatios", x => x.IdCatProveedorPatio);
                    table.ForeignKey(
                        name: "FK_CatProveedoresPatios_CatAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresPatios_CatPatios_IdCatPatio",
                        column: x => x.IdCatPatio,
                        principalTable: "CatPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresPatios_CatProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresPatios_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatUsuariosEmpresa",
                columns: table => new
                {
                    IdCatUsuariosEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatUsuarios = table.Column<int>(type: "int", nullable: false),
                    IdCatCliente = table.Column<int>(type: "int", nullable: true),
                    idCatEmpresa = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatUsuariosEmpresa", x => x.IdCatUsuariosEmpresa);
                    table.ForeignKey(
                        name: "FK_CatUsuariosEmpresa_CatClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_CatUsuariosEmpresa_CatEmpresas_idCatEmpresa",
                        column: x => x.idCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa");
                    table.ForeignKey(
                        name: "FK_CatUsuariosEmpresa_CatProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor");
                    table.ForeignKey(
                        name: "FK_CatUsuariosEmpresa_CatUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatUsuarioRoles",
                columns: table => new
                {
                    IdCatUsuariosRoles = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatUsuarios = table.Column<int>(type: "int", nullable: false),
                    IdCatRoles = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatUsuarioRoles", x => x.IdCatUsuariosRoles);
                    table.ForeignKey(
                        name: "FK_CatUsuarioRoles_CatRoles_IdCatRoles",
                        column: x => x.IdCatRoles,
                        principalTable: "CatRoles",
                        principalColumn: "IdCatRoles",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatUsuarioRoles_CatUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_011_UBICACION",
                schema: "WMS",
                columns: table => new
                {
                    nIdUbicacion011 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sClave = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    sDescripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    nPosicion = table.Column<int>(type: "int", nullable: false),
                    nAltura = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    nCapacidad = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    nIdZonaAlmacenaje010 = table.Column<int>(type: "int", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_011_UBICACION", x => x.nIdUbicacion011);
                    table.ForeignKey(
                        name: "FK_WMS_011_UBICACION_WMS_010_ZONA_ALMACENAJE_nIdZonaAlmacenaje010",
                        column: x => x.nIdZonaAlmacenaje010,
                        principalSchema: "WMS",
                        principalTable: "WMS_010_ZONA_ALMACENAJE",
                        principalColumn: "nIdZonaAlmacenaje010");
                });

            migrationBuilder.CreateTable(
                name: "CatClientesServicioAduana",
                columns: table => new
                {
                    IdCatCteServAduana = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatClientes = table.Column<int>(type: "int", nullable: false),
                    IdCatAduana = table.Column<int>(type: "int", nullable: false),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatClientesServicioAduana", x => x.IdCatCteServAduana);
                    table.ForeignKey(
                        name: "FK_CatClientesServicioAduana_CatAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesServicioAduana_CatClientes_IdCatClientes",
                        column: x => x.IdCatClientes,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesServicioAduana_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesServicioAduana_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatLineaNegocioTarifa",
                columns: table => new
                {
                    IdCatLineaNegocioTarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatLineaNegocioTarifa", x => x.IdCatLineaNegocioTarifa);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocioTarifa_CatLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "CatLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocioTarifa_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocioTarifa_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatProveedoresTarifas",
                columns: table => new
                {
                    IdCatProvTarifas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatProveedores = table.Column<int>(type: "int", nullable: false),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false),
                    Tarifa = table.Column<double>(type: "float", nullable: false),
                    tarifa_cotizacion = table.Column<double>(type: "float", nullable: false),
                    Impuesto = table.Column<double>(type: "float", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    IdCatAduana = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatProveedoresTarifas", x => x.IdCatProvTarifas);
                    table.ForeignKey(
                        name: "FK_CatProveedoresTarifas_CatAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresTarifas_CatProveedores_IdCatProveedores",
                        column: x => x.IdCatProveedores,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresTarifas_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresTarifas_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_021_LINEA_TRANS_OPERADOR",
                schema: "WMS",
                columns: table => new
                {
                    nIdLineaTransOperador021 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sApellidoPaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sApellidoMaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sTelefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sLicencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    nIdCatTransportista = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_021_LINEA_TRANS_OPERADOR", x => x.nIdLineaTransOperador021);
                    table.ForeignKey(
                        name: "FK_WMS_021_LINEA_TRANS_OPERADOR_CatTransportistas_nIdCatTransportista",
                        column: x => x.nIdCatTransportista,
                        principalTable: "CatTransportistas",
                        principalColumn: "IdCatTransportista",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_022_LINEA_TRANS_TRANSPORTE",
                schema: "WMS",
                columns: table => new
                {
                    nIdLineaTransTransporte022 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sPlacas = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    sNumeroEconomico = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    sPlacasPlana1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    sPlacasPlana2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    nIdCatTransportista = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_022_LINEA_TRANS_TRANSPORTE", x => x.nIdLineaTransTransporte022);
                    table.ForeignKey(
                        name: "FK_WMS_022_LINEA_TRANS_TRANSPORTE_CatTransportistas_nIdCatTransportista",
                        column: x => x.nIdCatTransportista,
                        principalTable: "CatTransportistas",
                        principalColumn: "IdCatTransportista",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_006_PAQUETE_SERVICIO",
                schema: "WMS",
                columns: table => new
                {
                    PaquetesId = table.Column<int>(type: "int", nullable: false),
                    ServiciosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_006_PAQUETE_SERVICIO", x => new { x.PaquetesId, x.ServiciosId });
                    table.ForeignKey(
                        name: "FK_WMS_006_PAQUETE_SERVICIO_WMS_004_SERVICIO_ServiciosId",
                        column: x => x.ServiciosId,
                        principalSchema: "WMS",
                        principalTable: "WMS_004_SERVICIO",
                        principalColumn: "nIdServicio004",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_006_PAQUETE_SERVICIO_WMS_005_PAQUETE_PaquetesId",
                        column: x => x.PaquetesId,
                        principalSchema: "WMS",
                        principalTable: "WMS_005_PAQUETE",
                        principalColumn: "nIdPaquete005",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatClientesProyectos",
                columns: table => new
                {
                    IdCatClientesProyectos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatCliente = table.Column<int>(type: "int", nullable: false),
                    IdCatProyecto = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatClientesProyectos", x => x.IdCatClientesProyectos);
                    table.ForeignKey(
                        name: "FK_CatClientesProyectos_CatClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesProyectos_CatProyectos_IdCatProyecto",
                        column: x => x.IdCatProyecto,
                        principalTable: "CatProyectos",
                        principalColumn: "IdProyectos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClientesProyectos_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    IdOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatCliente = table.Column<int>(type: "int", nullable: false),
                    IdCatSistema = table.Column<int>(type: "int", nullable: true),
                    IdCatAduana = table.Column<int>(type: "int", nullable: true),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: true),
                    IdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdCatSucursal = table.Column<int>(type: "int", nullable: false),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    IdCatProyecto = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdEstadoOrden = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ReferenciaALO = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.IdOrden);
                    table.ForeignKey(
                        name: "FK_Ordenes_CatAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_Ordenes_CatClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_CatEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_CatLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "CatLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_CatProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor");
                    table.ForeignKey(
                        name: "FK_Ordenes_CatProyectos_IdCatProyecto",
                        column: x => x.IdCatProyecto,
                        principalTable: "CatProyectos",
                        principalColumn: "IdProyectos");
                    table.ForeignKey(
                        name: "FK_Ordenes_CatSistemas_IdCatSistema",
                        column: x => x.IdCatSistema,
                        principalTable: "CatSistemas",
                        principalColumn: "IdCatSistema");
                    table.ForeignKey(
                        name: "FK_Ordenes_CatSucursales_IdCatSucursal",
                        column: x => x.IdCatSucursal,
                        principalTable: "CatSucursales",
                        principalColumn: "IdCatSucursal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_CatUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios");
                });

            migrationBuilder.CreateTable(
                name: "WMS_014_INVENTARIO",
                schema: "WMS",
                columns: table => new
                {
                    nIdInventario014 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nEstado = table.Column<int>(type: "int", nullable: false),
                    sMercancia = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    nIdTipoEmbalaje013 = table.Column<int>(type: "int", nullable: true),
                    nIdUnidadMediad012 = table.Column<int>(type: "int", nullable: true),
                    nCantidadInicial = table.Column<int>(type: "int", nullable: true),
                    nPesoInicial = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    nCantidadFinal = table.Column<int>(type: "int", nullable: true),
                    nPesoFinal = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    bExistencia = table.Column<bool>(type: "bit", nullable: false),
                    sObservaciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    bProcesoRecepcionCompletado = table.Column<bool>(type: "bit", nullable: false),
                    dFechaRecoleccion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaEmbarque = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaVerificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nIdUbicacion011 = table.Column<int>(type: "int", nullable: true),
                    sGrupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_014_INVENTARIO", x => x.nIdInventario014);
                    table.ForeignKey(
                        name: "FK_WMS_014_INVENTARIO_WMS_011_UBICACION_nIdUbicacion011",
                        column: x => x.nIdUbicacion011,
                        principalSchema: "WMS",
                        principalTable: "WMS_011_UBICACION",
                        principalColumn: "nIdUbicacion011");
                    table.ForeignKey(
                        name: "FK_WMS_014_INVENTARIO_WMS_012_UNIDAD_MEDIDA_nIdUnidadMediad012",
                        column: x => x.nIdUnidadMediad012,
                        principalSchema: "WMS",
                        principalTable: "WMS_012_UNIDAD_MEDIDA",
                        principalColumn: "nIdUnidadMedidad012");
                    table.ForeignKey(
                        name: "FK_WMS_014_INVENTARIO_WMS_013_TIPO_EMBALAJE_nIdTipoEmbalaje013",
                        column: x => x.nIdTipoEmbalaje013,
                        principalSchema: "WMS",
                        principalTable: "WMS_013_TIPO_EMBALAJE",
                        principalColumn: "nIdTipoEmbalaje013");
                });

            migrationBuilder.CreateTable(
                name: "CatClienteTarifa",
                columns: table => new
                {
                    IdCatClienteTarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatCteServAduana = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Impuesto = table.Column<double>(type: "float", nullable: false),
                    IdCatContenedor = table.Column<int>(type: "int", nullable: true),
                    catContenedorIdCatContenedor = table.Column<int>(type: "int", nullable: true),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CatClientesIdCatCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatClienteTarifa", x => x.IdCatClienteTarifa);
                    table.ForeignKey(
                        name: "FK_CatClienteTarifa_CatClientesServicioAduana_IdCatCteServAduana",
                        column: x => x.IdCatCteServAduana,
                        principalTable: "CatClientesServicioAduana",
                        principalColumn: "IdCatCteServAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatClienteTarifa_CatClientes_CatClientesIdCatCliente",
                        column: x => x.CatClientesIdCatCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_CatClienteTarifa_CatContenedor_catContenedorIdCatContenedor",
                        column: x => x.catContenedorIdCatContenedor,
                        principalTable: "CatContenedor",
                        principalColumn: "IdCatContenedor");
                    table.ForeignKey(
                        name: "FK_CatClienteTarifa_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatLineaNegocioTariPrecio",
                columns: table => new
                {
                    IdCatLineNegocioTariPrecio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatLineaNegocioTarifa = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Impuesto = table.Column<double>(type: "float", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    IdCatAduana = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatLineaNegocioTariPrecio", x => x.IdCatLineNegocioTariPrecio);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocioTariPrecio_CatAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocioTariPrecio_CatEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocioTariPrecio_CatLineaNegocioTarifa_IdCatLineaNegocioTarifa",
                        column: x => x.IdCatLineaNegocioTarifa,
                        principalTable: "CatLineaNegocioTarifa",
                        principalColumn: "IdCatLineaNegocioTarifa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatLineaNegocioTariPrecio_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatProveedoresTarifaPatio",
                columns: table => new
                {
                    IdCatProvTarifaPatio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatProvTarifas = table.Column<int>(type: "int", nullable: false),
                    IdCatPatio = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatProveedoresTarifaPatio", x => x.IdCatProvTarifaPatio);
                    table.ForeignKey(
                        name: "FK_CatProveedoresTarifaPatio_CatPatios_IdCatPatio",
                        column: x => x.IdCatPatio,
                        principalTable: "CatPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresTarifaPatio_CatProveedoresTarifas_IdCatProvTarifas",
                        column: x => x.IdCatProvTarifas,
                        principalTable: "CatProveedoresTarifas",
                        principalColumn: "IdCatProvTarifas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatProveedoresTarifaPatio_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_024_CONTROL_TRANSPORTE",
                schema: "WMS",
                columns: table => new
                {
                    nIdControlTransporte024 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nFolio = table.Column<int>(type: "int", nullable: false),
                    nCantidad = table.Column<int>(type: "int", nullable: true),
                    nPeso = table.Column<int>(type: "int", nullable: true),
                    nTipoEntrada = table.Column<int>(type: "int", nullable: true),
                    nTipoViaje = table.Column<int>(type: "int", nullable: true),
                    bEsLocal = table.Column<bool>(type: "bit", maxLength: 250, nullable: true),
                    sViajes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sTerminalOrigen = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    sTerminalDestino = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    nIdCatTransportista = table.Column<int>(type: "int", nullable: true),
                    sNombreOperador = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    sPlacas = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    sPlacasPlana1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    sPlacasPlana2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    sNumeroEconomico = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    nTipoOperacion = table.Column<int>(type: "int", nullable: false),
                    sObservacion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    dFechaLlegada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bCargado = table.Column<bool>(type: "bit", nullable: true),
                    bSalidaAutorizada = table.Column<bool>(type: "bit", nullable: true),
                    dFechaAutorizacionEntrada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaInicioCargaDescarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinCargaDescarga = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaAutorizacionSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaCancelacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sMotivoCancelacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nIdTipoTransporte020 = table.Column<int>(type: "int", nullable: true),
                    nIdLineaTansOperador021 = table.Column<int>(type: "int", nullable: true),
                    nIdLineaTransTransporte022 = table.Column<int>(type: "int", nullable: true),
                    nTurno = table.Column<int>(type: "int", nullable: true),
                    nEstado = table.Column<int>(type: "int", nullable: true),
                    nIdManiobristaOrigen023 = table.Column<int>(type: "int", nullable: true),
                    nIdManiobristaDestino023 = table.Column<int>(type: "int", nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_024_CONTROL_TRANSPORTE", x => x.nIdControlTransporte024);
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_CatEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_CatTransportistas_nIdCatTransportista",
                        column: x => x.nIdCatTransportista,
                        principalTable: "CatTransportistas",
                        principalColumn: "IdCatTransportista");
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_WMS_020_TIPO_TRANSPORTE_nIdTipoTransporte020",
                        column: x => x.nIdTipoTransporte020,
                        principalSchema: "WMS",
                        principalTable: "WMS_020_TIPO_TRANSPORTE",
                        principalColumn: "nIdTipoTransporte020");
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_WMS_021_LINEA_TRANS_OPERADOR_nIdLineaTansOperador021",
                        column: x => x.nIdLineaTansOperador021,
                        principalSchema: "WMS",
                        principalTable: "WMS_021_LINEA_TRANS_OPERADOR",
                        principalColumn: "nIdLineaTransOperador021");
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_WMS_022_LINEA_TRANS_TRANSPORTE_nIdLineaTransTransporte022",
                        column: x => x.nIdLineaTransTransporte022,
                        principalSchema: "WMS",
                        principalTable: "WMS_022_LINEA_TRANS_TRANSPORTE",
                        principalColumn: "nIdLineaTransTransporte022");
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_WMS_023_MANIOBRISTA_nIdManiobristaDestino023",
                        column: x => x.nIdManiobristaDestino023,
                        principalSchema: "WMS",
                        principalTable: "WMS_023_MANIOBRISTA",
                        principalColumn: "nIdManiobrista023");
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_WMS_023_MANIOBRISTA_nIdManiobristaOrigen023",
                        column: x => x.nIdManiobristaOrigen023,
                        principalSchema: "WMS",
                        principalTable: "WMS_023_MANIOBRISTA",
                        principalColumn: "nIdManiobrista023");
                });

            migrationBuilder.CreateTable(
                name: "DtAcarreos",
                columns: table => new
                {
                    IdDtAcarreos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Servicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contenedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdCatTipoEstado = table.Column<int>(type: "int", nullable: false),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtAcarreos", x => x.IdDtAcarreos);
                    table.ForeignKey(
                        name: "FK_DtAcarreos_CatClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtAcarreos_CatEmpresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtAcarreos_CatProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtAcarreos_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtAcarreos_CatTipoEstados_IdCatTipoEstado",
                        column: x => x.IdCatTipoEstado,
                        principalTable: "CatTipoEstados",
                        principalColumn: "IdCatTipoEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtAcarreos_CatUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtAcarreos_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DtUltimaMillaEnc",
                columns: table => new
                {
                    IdDtUltMillaEnc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Viaje = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacturaCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bodega = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdTipoEstado = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCatTipoTransporte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtUltimaMillaEnc", x => x.IdDtUltMillaEnc);
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaEnc_CatClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaEnc_CatEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaEnc_CatProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaEnc_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaEnc_CatTipoEstados_IdTipoEstado",
                        column: x => x.IdTipoEstado,
                        principalTable: "CatTipoEstados",
                        principalColumn: "IdCatTipoEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaEnc_CatTipoTransporte_IdCatTipoTransporte",
                        column: x => x.IdCatTipoTransporte,
                        principalTable: "CatTipoTransporte",
                        principalColumn: "IdCatTipoTransporte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaEnc_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "integracionReferencia",
                schema: "SLO",
                columns: table => new
                {
                    IdIntReferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    IdCompaniaExterna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenciaALO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenciaClienteExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveClienteExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aduana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaWS1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionReferencia", x => x.IdIntReferencia);
                    table.ForeignKey(
                        name: "FK_integracionReferencia_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegraReferencia",
                columns: table => new
                {
                    IdIntReferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    IdCompaniaExterna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenciaALO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenciaClienteExterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveClienteExterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aduana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaWS1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegraReferencia", x => x.IdIntReferencia);
                    table.ForeignKey(
                        name: "FK_IntegraReferencia_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peticionesReferencias",
                schema: "SLO",
                columns: table => new
                {
                    IdReferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket = table.Column<int>(type: "int", nullable: true),
                    Transporte_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transporte_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrasporteId = table.Column<int>(type: "int", nullable: true),
                    Transporte_Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transporte_UsuarioEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoReferencia = table.Column<int>(type: "int", nullable: true),
                    Procesado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatReferenciaEstado = table.Column<int>(type: "int", nullable: true),
                    FechaProcesado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peticionesReferencias", x => x.IdReferencia);
                    table.ForeignKey(
                        name: "FK_peticionesReferencias_CatReferenciaEstado_IdCatReferenciaEstado",
                        column: x => x.IdCatReferenciaEstado,
                        principalTable: "CatReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado");
                    table.ForeignKey(
                        name: "FK_peticionesReferencias_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeticionesReferencias",
                columns: table => new
                {
                    IdReferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket = table.Column<int>(type: "int", nullable: true),
                    Transporte_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transporte_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrasporteId = table.Column<int>(type: "int", nullable: true),
                    Transporte_Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transporte_UsuarioEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoReferencia = table.Column<int>(type: "int", nullable: false),
                    Procesado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaProcesado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatReferenciaEstado = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeticionesReferencias", x => x.IdReferencia);
                    table.ForeignKey(
                        name: "FK_PeticionesReferencias_CatReferenciaEstado_IdCatReferenciaEstado",
                        column: x => x.IdCatReferenciaEstado,
                        principalTable: "CatReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeticionesReferencias_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_001_REFERENCIA",
                schema: "WMS",
                columns: table => new
                {
                    nIdReferencia001 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sFolio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nEstado = table.Column<int>(type: "int", nullable: false),
                    nTipoOperacion = table.Column<int>(type: "int", nullable: false),
                    nTipoMercancia = table.Column<int>(type: "int", nullable: false),
                    sReferenciaClienteExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sMercancia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nBultosInicial = table.Column<int>(type: "int", nullable: true),
                    nPesoInicial = table.Column<decimal>(type: "decimal(14,3)", nullable: true),
                    nBultosFinal = table.Column<int>(type: "int", nullable: true),
                    nPesoFinal = table.Column<decimal>(type: "decimal(14,3)", nullable: true),
                    sManifiestoBuque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dFechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sObservaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nIdCatAduana = table.Column<int>(type: "int", nullable: true),
                    nIdCatCliente = table.Column<int>(type: "int", nullable: true),
                    nIdCatProveedor = table.Column<int>(type: "int", nullable: true),
                    nIdCatClienteImpoExpo = table.Column<int>(type: "int", nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    nIdViaje002 = table.Column<int>(type: "int", nullable: true),
                    nIdAgenteAduanal033 = table.Column<int>(type: "int", nullable: true),
                    nIdOrdenServicio = table.Column<int>(type: "int", nullable: true),
                    nIdCatClienteFacturarA = table.Column<int>(type: "int", nullable: true),
                    nIdManiobristaOrigen023 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_001_REFERENCIA", x => x.nIdReferencia001);
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_CatAduana_nIdCatAduana",
                        column: x => x.nIdCatAduana,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_CatClientes_nIdCatCliente",
                        column: x => x.nIdCatCliente,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_CatClientes_nIdCatClienteFacturarA",
                        column: x => x.nIdCatClienteFacturarA,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_CatClientes_nIdCatClienteImpoExpo",
                        column: x => x.nIdCatClienteImpoExpo,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_CatEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_CatProveedores_nIdCatProveedor",
                        column: x => x.nIdCatProveedor,
                        principalTable: "CatProveedores",
                        principalColumn: "IdCatProveedor");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_Ordenes_nIdOrdenServicio",
                        column: x => x.nIdOrdenServicio,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_WMS_002_VIAJE_nIdViaje002",
                        column: x => x.nIdViaje002,
                        principalSchema: "WMS",
                        principalTable: "WMS_002_VIAJE",
                        principalColumn: "nIdViaje002");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_WMS_023_MANIOBRISTA_nIdManiobristaOrigen023",
                        column: x => x.nIdManiobristaOrigen023,
                        principalSchema: "WMS",
                        principalTable: "WMS_023_MANIOBRISTA",
                        principalColumn: "nIdManiobrista023");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_WMS_033_AGENTE_ADUANAL_nIdAgenteAduanal033",
                        column: x => x.nIdAgenteAduanal033,
                        principalSchema: "WMS",
                        principalTable: "WMS_033_AGENTE_ADUANAL",
                        principalColumn: "nIdAgenteAduanal033");
                });

            migrationBuilder.CreateTable(
                name: "WMS_032_BITACORA_AVERIA",
                schema: "WMS",
                columns: table => new
                {
                    nIdBitacoraAveria032 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdInventario014 = table.Column<int>(type: "int", nullable: false),
                    nIdCodigoDesperfecto029 = table.Column<int>(type: "int", nullable: false),
                    nIdTipoDesperfecto030 = table.Column<int>(type: "int", nullable: false),
                    nIdTipoSeveridad031 = table.Column<int>(type: "int", nullable: false),
                    sDescripcionAveria = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_032_BITACORA_AVERIA", x => x.nIdBitacoraAveria032);
                    table.ForeignKey(
                        name: "FK_WMS_032_BITACORA_AVERIA_WMS_014_INVENTARIO_nIdInventario014",
                        column: x => x.nIdInventario014,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_032_BITACORA_AVERIA_WMS_029_CODIGO_DESPERFECTO_nIdCodigoDesperfecto029",
                        column: x => x.nIdCodigoDesperfecto029,
                        principalSchema: "WMS",
                        principalTable: "WMS_029_CODIGO_DESPERFECTO",
                        principalColumn: "nIdCodigoDesperfecto029",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_032_BITACORA_AVERIA_WMS_030_TIPO_DESPERFECTO_nIdTipoDesperfecto030",
                        column: x => x.nIdTipoDesperfecto030,
                        principalSchema: "WMS",
                        principalTable: "WMS_030_TIPO_DESPERFECTO",
                        principalColumn: "nIdTipoDesperfecto030",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_032_BITACORA_AVERIA_WMS_031_TIPO_SEVERIDAD_nIdTipoSeveridad031",
                        column: x => x.nIdTipoSeveridad031,
                        principalSchema: "WMS",
                        principalTable: "WMS_031_TIPO_SEVERIDAD",
                        principalColumn: "nIdTipoSeveridad031",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DtUltimaMillaDet",
                columns: table => new
                {
                    IdDtUltimaMillaDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroParte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Piezas = table.Column<int>(type: "int", nullable: false),
                    Pallet = table.Column<int>(type: "int", nullable: true),
                    IdUltimaMilla = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtUltimaMillaDet", x => x.IdDtUltimaMillaDet);
                    table.ForeignKey(
                        name: "FK_DtUltimaMillaDet_DtUltimaMillaEnc_IdUltimaMilla",
                        column: x => x.IdUltimaMilla,
                        principalTable: "DtUltimaMillaEnc",
                        principalColumn: "IdDtUltMillaEnc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "integracionFacturaEnc",
                schema: "SLO",
                columns: table => new
                {
                    IdIntFacturaEnc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    IdPeticionesReferencia = table.Column<int>(type: "int", nullable: false),
                    IdDtAcarreos = table.Column<int>(type: "int", nullable: true),
                    IdDtUltimaMillaEnc = table.Column<int>(type: "int", nullable: true),
                    IdSolicitudFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCompaniaExterna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveClienteExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConceptoFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveSATMoneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveSATUsoCFDI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacturacionAutomatica = table.Column<bool>(type: "bit", nullable: false),
                    CierreReferencia = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaWS1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdSucursalExterna = table.Column<int>(type: "int", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdIntReferencia = table.Column<int>(type: "int", nullable: false),
                    EstaListo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionFacturaEnc", x => x.IdIntFacturaEnc);
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_DtAcarreos_IdDtAcarreos",
                        column: x => x.IdDtAcarreos,
                        principalTable: "DtAcarreos",
                        principalColumn: "IdDtAcarreos");
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_DtUltimaMillaEnc_IdDtUltimaMillaEnc",
                        column: x => x.IdDtUltimaMillaEnc,
                        principalTable: "DtUltimaMillaEnc",
                        principalColumn: "IdDtUltMillaEnc");
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_integracionReferencia_IdIntReferencia",
                        column: x => x.IdIntReferencia,
                        principalSchema: "SLO",
                        principalTable: "integracionReferencia",
                        principalColumn: "IdIntReferencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_peticionesReferencias_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalSchema: "SLO",
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peticionesContenedores",
                schema: "SLO",
                columns: table => new
                {
                    IdContenedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FolioManiobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefenciaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveTipoContenedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatioId = table.Column<int>(type: "int", nullable: true),
                    Patio_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patio_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaTocaPiso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Cliente_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente_Solicitante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignadoId = table.Column<int>(type: "int", nullable: true),
                    Consignado_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consignado_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FondoFinanciamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MontoSolicitud = table.Column<double>(type: "float", nullable: true),
                    MontoTotal = table.Column<double>(type: "float", nullable: true),
                    AduanaId = table.Column<int>(type: "int", nullable: true),
                    FechaSolDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaPagoGarantiaNav = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Naviera_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naviera_Id = table.Column<int>(type: "int", nullable: true),
                    Naviera_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aduana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdReferencia = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoContenedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstadoContenedor = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ReferenciaFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdClienteFacturarA = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peticionesContenedores", x => x.IdContenedor);
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_CatAduana_AduanaId",
                        column: x => x.AduanaId,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_CatClientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_CatClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_CatNavieras_Naviera_Id",
                        column: x => x.Naviera_Id,
                        principalTable: "CatNavieras",
                        principalColumn: "IdCatNaviera");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_CatPatios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "CatPatios",
                        principalColumn: "IdCatPatios");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_CatReferenciaEstado_IdEstadoContenedor",
                        column: x => x.IdEstadoContenedor,
                        principalTable: "CatReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_peticionesReferencias_IdReferencia",
                        column: x => x.IdReferencia,
                        principalSchema: "SLO",
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia");
                });

            migrationBuilder.CreateTable(
                name: "PeticionesContenedores",
                columns: table => new
                {
                    IdContenedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCatTipoContenedor = table.Column<int>(type: "int", nullable: false),
                    RefenciaCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveTipoContenedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatioId = table.Column<int>(type: "int", nullable: true),
                    Patio_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patio_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaTocaPiso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuqueViaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Cliente_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente_Solicitante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsignadoId = table.Column<int>(type: "int", nullable: true),
                    Consignado_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consignado_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FondoFinanciamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MontoSolicitud = table.Column<double>(type: "float", nullable: true),
                    MontoTotal = table.Column<double>(type: "float", nullable: true),
                    AduanaId = table.Column<int>(type: "int", nullable: true),
                    FechaSolDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaPagoGarantiaNav = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Naviera_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naviera_Id = table.Column<int>(type: "int", nullable: true),
                    Naviera_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aduana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdReferencia = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoContenedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstadoContenedor = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ReferenciaFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdClienteFacturarA = table.Column<int>(type: "int", nullable: true),
                    FolioManiobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatTransportista = table.Column<int>(type: "int", nullable: true),
                    NombreConductor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenciaConductor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroUnidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlacaUnidad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeticionesContenedores", x => x.IdContenedor);
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_CatAduana_AduanaId",
                        column: x => x.AduanaId,
                        principalTable: "CatAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_CatClientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_CatClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_CatNavieras_Naviera_Id",
                        column: x => x.Naviera_Id,
                        principalTable: "CatNavieras",
                        principalColumn: "IdCatNaviera");
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_CatPatios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "CatPatios",
                        principalColumn: "IdCatPatios");
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_CatReferenciaEstado_IdEstadoContenedor",
                        column: x => x.IdEstadoContenedor,
                        principalTable: "CatReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_CatTipoContenedor_IdCatTipoContenedor",
                        column: x => x.IdCatTipoContenedor,
                        principalTable: "CatTipoContenedor",
                        principalColumn: "IdCatTipoContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeticionesContenedores_PeticionesReferencias_IdReferencia",
                        column: x => x.IdReferencia,
                        principalTable: "PeticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_015_TARJA",
                schema: "WMS",
                columns: table => new
                {
                    nIdTarja015 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nFolio = table.Column<int>(type: "int", nullable: false),
                    nTipo = table.Column<int>(type: "int", nullable: false),
                    nEstado = table.Column<int>(type: "int", nullable: false),
                    sClaveReferencia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    sGrupoViaje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nConsecutivo = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    sObservaciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    nTipoServicio = table.Column<int>(type: "int", nullable: true),
                    nFolioIngreso = table.Column<int>(type: "int", nullable: true),
                    dFechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nMedioEntrada = table.Column<int>(type: "int", nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    nIdReferencia001 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_015_TARJA", x => x.nIdTarja015);
                    table.ForeignKey(
                        name: "FK_WMS_015_TARJA_CatEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_015_TARJA_WMS_001_REFERENCIA_nIdReferencia001",
                        column: x => x.nIdReferencia001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                });

            migrationBuilder.CreateTable(
                name: "WMS_017_INVENTARIO_ALO",
                schema: "WMS",
                columns: table => new
                {
                    nIdInventarioALO017 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nTipoMercancia = table.Column<int>(type: "int", nullable: true),
                    nIdInventario014 = table.Column<int>(type: "int", nullable: true),
                    nIdReferencia001 = table.Column<int>(type: "int", nullable: true),
                    nIdReferenciaOrigen001 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_017_INVENTARIO_ALO", x => x.nIdInventarioALO017);
                    table.ForeignKey(
                        name: "FK_WMS_017_INVENTARIO_ALO_WMS_001_REFERENCIA_nIdReferencia001",
                        column: x => x.nIdReferencia001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                    table.ForeignKey(
                        name: "FK_WMS_017_INVENTARIO_ALO_WMS_001_REFERENCIA_nIdReferenciaOrigen001",
                        column: x => x.nIdReferenciaOrigen001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                    table.ForeignKey(
                        name: "FK_WMS_017_INVENTARIO_ALO_WMS_014_INVENTARIO_nIdInventario014",
                        column: x => x.nIdInventario014,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014");
                });

            migrationBuilder.CreateTable(
                name: "peticionesServicios",
                schema: "SLO",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatServicio = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<double>(type: "float", nullable: true),
                    Monto = table.Column<double>(type: "float", nullable: true),
                    DescServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdContenedor = table.Column<int>(type: "int", nullable: true),
                    EstadoServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstadoServicio = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdClienteFacturarA = table.Column<int>(type: "int", nullable: true),
                    ReferenciaClienteFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peticionesServicios", x => x.IdServicio);
                    table.ForeignKey(
                        name: "FK_peticionesServicios_CatClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesServicios_CatReferenciaEstado_IdEstadoServicio",
                        column: x => x.IdEstadoServicio,
                        principalTable: "CatReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado");
                    table.ForeignKey(
                        name: "FK_peticionesServicios_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio");
                    table.ForeignKey(
                        name: "FK_peticionesServicios_peticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalSchema: "SLO",
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor");
                });

            migrationBuilder.CreateTable(
                name: "IntegraAnticipoSol",
                columns: table => new
                {
                    IdIntAnticipoSol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    IdPeticionesReferencia = table.Column<int>(type: "int", nullable: false),
                    IdPeticionesContenedor = table.Column<int>(type: "int", nullable: false),
                    IdCompaniaExterna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSolicitudAnticipoProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveProveedorExterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoTotal = table.Column<double>(type: "float", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaWS1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nota1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegraAnticipoSol", x => x.IdIntAnticipoSol);
                    table.ForeignKey(
                        name: "FK_IntegraAnticipoSol_IntegraReferencia_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalTable: "IntegraReferencia",
                        principalColumn: "IdIntReferencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegraAnticipoSol_Ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegraAnticipoSol_PeticionesContenedores_IdPeticionesContenedor",
                        column: x => x.IdPeticionesContenedor,
                        principalTable: "PeticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeticionesServicios",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false),
                    DescServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdContenedor = table.Column<int>(type: "int", nullable: false),
                    EstadoServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstadoServicio = table.Column<int>(type: "int", nullable: false),
                    ReferenciaClienteFacturar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdClienteFacturarA = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeticionesServicios", x => x.IdServicio);
                    table.ForeignKey(
                        name: "FK_PeticionesServicios_CatClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "CatClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_PeticionesServicios_CatReferenciaEstado_IdEstadoServicio",
                        column: x => x.IdEstadoServicio,
                        principalTable: "CatReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeticionesServicios_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeticionesServicios_PeticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "PeticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_016_PARTIDA",
                schema: "WMS",
                columns: table => new
                {
                    nIdPartida016 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nNumeroPartida = table.Column<int>(type: "int", nullable: false),
                    sMarcas = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sNumeros = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    sModelo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    bTieneAveria = table.Column<bool>(type: "bit", nullable: false),
                    sDescripcionAveria = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    sBLHouse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    bCargaMasiva = table.Column<bool>(type: "bit", nullable: false),
                    nIdInventario014 = table.Column<int>(type: "int", nullable: true),
                    nIdInventarioOrigen014 = table.Column<int>(type: "int", nullable: true),
                    nIdTarja015 = table.Column<int>(type: "int", nullable: true),
                    nIdTarjaOrigen015 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_016_PARTIDA", x => x.nIdPartida016);
                    table.ForeignKey(
                        name: "FK_WMS_016_PARTIDA_WMS_014_INVENTARIO_nIdInventario014",
                        column: x => x.nIdInventario014,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014");
                    table.ForeignKey(
                        name: "FK_WMS_016_PARTIDA_WMS_014_INVENTARIO_nIdInventarioOrigen014",
                        column: x => x.nIdInventarioOrigen014,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014");
                    table.ForeignKey(
                        name: "FK_WMS_016_PARTIDA_WMS_015_TARJA_nIdTarja015",
                        column: x => x.nIdTarja015,
                        principalSchema: "WMS",
                        principalTable: "WMS_015_TARJA",
                        principalColumn: "nIdTarja015");
                    table.ForeignKey(
                        name: "FK_WMS_016_PARTIDA_WMS_015_TARJA_nIdTarjaOrigen015",
                        column: x => x.nIdTarjaOrigen015,
                        principalSchema: "WMS",
                        principalTable: "WMS_015_TARJA",
                        principalColumn: "nIdTarja015");
                });

            migrationBuilder.CreateTable(
                name: "WMS_018_FOLIO_SERVICIO",
                schema: "WMS",
                columns: table => new
                {
                    nIdFolioServicio018 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nFolio = table.Column<int>(type: "int", nullable: false),
                    nEstado = table.Column<int>(type: "int", nullable: true),
                    nServicioPara = table.Column<int>(type: "int", nullable: true),
                    dFechaProgramacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sSolicitante = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    sInstrucciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    bFacturable = table.Column<bool>(type: "bit", nullable: false),
                    nIdPaquete005 = table.Column<int>(type: "int", nullable: true),
                    nIdReferencia001 = table.Column<int>(type: "int", nullable: true),
                    nIdInventario014 = table.Column<int>(type: "int", nullable: true),
                    nIdTarja015 = table.Column<int>(type: "int", nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_018_FOLIO_SERVICIO", x => x.nIdFolioServicio018);
                    table.ForeignKey(
                        name: "FK_WMS_018_FOLIO_SERVICIO_CatEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_018_FOLIO_SERVICIO_WMS_001_REFERENCIA_nIdReferencia001",
                        column: x => x.nIdReferencia001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                    table.ForeignKey(
                        name: "FK_WMS_018_FOLIO_SERVICIO_WMS_005_PAQUETE_nIdPaquete005",
                        column: x => x.nIdPaquete005,
                        principalSchema: "WMS",
                        principalTable: "WMS_005_PAQUETE",
                        principalColumn: "nIdPaquete005");
                    table.ForeignKey(
                        name: "FK_WMS_018_FOLIO_SERVICIO_WMS_014_INVENTARIO_nIdInventario014",
                        column: x => x.nIdInventario014,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014");
                    table.ForeignKey(
                        name: "FK_WMS_018_FOLIO_SERVICIO_WMS_015_TARJA_nIdTarja015",
                        column: x => x.nIdTarja015,
                        principalSchema: "WMS",
                        principalTable: "WMS_015_TARJA",
                        principalColumn: "nIdTarja015");
                });

            migrationBuilder.CreateTable(
                name: "WMS_025_FOLIO_SERVICIO_MASTER_DETALLE",
                schema: "WMS",
                columns: table => new
                {
                    nIdFolioServicioMasterDetalle025 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdReferencia001 = table.Column<int>(type: "int", nullable: true),
                    nIdTarja015 = table.Column<int>(type: "int", nullable: true),
                    nIdInventario014 = table.Column<int>(type: "int", nullable: true),
                    nServicioPara = table.Column<int>(type: "int", nullable: false),
                    IdTarja = table.Column<int>(type: "int", nullable: true),
                    IdInventario = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE", x => x.nIdFolioServicioMasterDetalle025);
                    table.ForeignKey(
                        name: "FK_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE_WMS_001_REFERENCIA_nIdReferencia001",
                        column: x => x.nIdReferencia001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                    table.ForeignKey(
                        name: "FK_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE_WMS_014_INVENTARIO_IdInventario",
                        column: x => x.IdInventario,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014");
                    table.ForeignKey(
                        name: "FK_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE_WMS_015_TARJA_IdTarja",
                        column: x => x.IdTarja,
                        principalSchema: "WMS",
                        principalTable: "WMS_015_TARJA",
                        principalColumn: "nIdTarja015");
                });

            migrationBuilder.CreateTable(
                name: "integracionFacturaDet",
                schema: "SLO",
                columns: table => new
                {
                    IdIntFacturaDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IddIntFacturaEnc = table.Column<int>(type: "int", nullable: false),
                    ClaveServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPeticionesReferencia = table.Column<int>(type: "int", nullable: false),
                    IdPeticionesContenedor = table.Column<int>(type: "int", nullable: false),
                    Contenedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CentroCostos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EIR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false),
                    Idpservicios = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionFacturaDet", x => x.IdIntFacturaDet);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_CatServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "CatServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_integracionFacturaEnc_IddIntFacturaEnc",
                        column: x => x.IddIntFacturaEnc,
                        principalSchema: "SLO",
                        principalTable: "integracionFacturaEnc",
                        principalColumn: "IdIntFacturaEnc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_peticionesContenedores_IdPeticionesContenedor",
                        column: x => x.IdPeticionesContenedor,
                        principalSchema: "SLO",
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_peticionesReferencias_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalSchema: "SLO",
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_peticionesServicios_Idpservicios",
                        column: x => x.Idpservicios,
                        principalSchema: "SLO",
                        principalTable: "peticionesServicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peticionesContenedorCron",
                schema: "vacios",
                columns: table => new
                {
                    IdContenedorCron = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatTipoIncidenciaEvento = table.Column<int>(type: "int", nullable: false),
                    IdRegistroUsuario = table.Column<int>(type: "int", nullable: false),
                    IdContenedor = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peticionesContenedorCron", x => x.IdContenedorCron);
                    table.ForeignKey(
                        name: "FK_peticionesContenedorCron_CatTipoIncidenciaEvento_IdCatTipoIncidenciaEvento",
                        column: x => x.IdCatTipoIncidenciaEvento,
                        principalTable: "CatTipoIncidenciaEvento",
                        principalColumn: "IdCatTipoIncidenciaEvento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedorCron_CatUsuarios_IdRegistroUsuario",
                        column: x => x.IdRegistroUsuario,
                        principalTable: "CatUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedorCron_PeticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "PeticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedorCron_PeticionesServicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "PeticionesServicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeticionesDocumentos",
                columns: table => new
                {
                    IdDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentoUUID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdContenedor = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeticionesDocumentos", x => x.IdDocumento);
                    table.ForeignKey(
                        name: "FK_PeticionesDocumentos_CatDocumentos_IdTipoDocumento",
                        column: x => x.IdTipoDocumento,
                        principalTable: "CatDocumentos",
                        principalColumn: "IdCatDocumento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeticionesDocumentos_PeticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "PeticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeticionesDocumentos_PeticionesServicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "PeticionesServicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_028_SERVICIO_FOTOGRAFIA",
                schema: "WMS",
                columns: table => new
                {
                    nIdServicioFotografia028 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdFolioServicio018 = table.Column<int>(type: "int", nullable: true),
                    nIdInventario014 = table.Column<int>(type: "int", nullable: true),
                    sRutaBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sRutaNombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nTipoFoto = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_028_SERVICIO_FOTOGRAFIA", x => x.nIdServicioFotografia028);
                    table.ForeignKey(
                        name: "FK_WMS_028_SERVICIO_FOTOGRAFIA_WMS_014_INVENTARIO_nIdInventario014",
                        column: x => x.nIdInventario014,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014");
                    table.ForeignKey(
                        name: "FK_WMS_028_SERVICIO_FOTOGRAFIA_WMS_018_FOLIO_SERVICIO_nIdFolioServicio018",
                        column: x => x.nIdFolioServicio018,
                        principalSchema: "WMS",
                        principalTable: "WMS_018_FOLIO_SERVICIO",
                        principalColumn: "nIdFolioServicio018");
                });

            migrationBuilder.CreateTable(
                name: "WMS_026_SOLICITUD_TRASLADO",
                schema: "WMS",
                columns: table => new
                {
                    nIdSolicitudTraslado026 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nFolio = table.Column<int>(type: "int", nullable: false),
                    sBoletaLiberacionOrigen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nEstado = table.Column<int>(type: "int", nullable: true),
                    nPrioridad = table.Column<int>(type: "int", nullable: true),
                    sObservaciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    dFechaSolicitudTraslado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaRecepcionBoleta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaVigenciaBoleta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nIdFolioServicio018 = table.Column<int>(type: "int", nullable: true),
                    nIdFolioServicioMasterDetalle025 = table.Column<int>(type: "int", nullable: true),
                    nIdManiobristaOrigen023 = table.Column<int>(type: "int", nullable: true),
                    nIdManiobristaDestino023 = table.Column<int>(type: "int", nullable: true),
                    nIdControlTransporte024 = table.Column<int>(type: "int", nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_026_SOLICITUD_TRASLADO", x => x.nIdSolicitudTraslado026);
                    table.ForeignKey(
                        name: "FK_WMS_026_SOLICITUD_TRASLADO_CatEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "CatEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_026_SOLICITUD_TRASLADO_WMS_018_FOLIO_SERVICIO_nIdFolioServicio018",
                        column: x => x.nIdFolioServicio018,
                        principalSchema: "WMS",
                        principalTable: "WMS_018_FOLIO_SERVICIO",
                        principalColumn: "nIdFolioServicio018");
                    table.ForeignKey(
                        name: "FK_WMS_026_SOLICITUD_TRASLADO_WMS_023_MANIOBRISTA_nIdManiobristaDestino023",
                        column: x => x.nIdManiobristaDestino023,
                        principalSchema: "WMS",
                        principalTable: "WMS_023_MANIOBRISTA",
                        principalColumn: "nIdManiobrista023");
                    table.ForeignKey(
                        name: "FK_WMS_026_SOLICITUD_TRASLADO_WMS_023_MANIOBRISTA_nIdManiobristaOrigen023",
                        column: x => x.nIdManiobristaOrigen023,
                        principalSchema: "WMS",
                        principalTable: "WMS_023_MANIOBRISTA",
                        principalColumn: "nIdManiobrista023");
                    table.ForeignKey(
                        name: "FK_WMS_026_SOLICITUD_TRASLADO_WMS_024_CONTROL_TRANSPORTE_nIdControlTransporte024",
                        column: x => x.nIdControlTransporte024,
                        principalSchema: "WMS",
                        principalTable: "WMS_024_CONTROL_TRANSPORTE",
                        principalColumn: "nIdControlTransporte024");
                    table.ForeignKey(
                        name: "FK_WMS_026_SOLICITUD_TRASLADO_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE_nIdFolioServicioMasterDetalle025",
                        column: x => x.nIdFolioServicioMasterDetalle025,
                        principalSchema: "WMS",
                        principalTable: "WMS_025_FOLIO_SERVICIO_MASTER_DETALLE",
                        principalColumn: "nIdFolioServicioMasterDetalle025");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatAduana_IdCatPais",
                table: "CatAduana",
                column: "IdCatPais");

            migrationBuilder.CreateIndex(
                name: "IX_CatAduana_IdCatPaisEstados",
                table: "CatAduana",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_CatAduana_IdUsuarioRegistro",
                table: "CatAduana",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientes_IdCatPaises",
                table: "CatClientes",
                column: "IdCatPaises");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientes_IdCatPaisEstados",
                table: "CatClientes",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientes_IdUsuarioRegistro",
                table: "CatClientes",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesConfig_IdCatClientes",
                table: "CatClientesConfig",
                column: "IdCatClientes");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesConfig_IdCatTipoConfig",
                table: "CatClientesConfig",
                column: "IdCatTipoConfig");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesConfig_IdUsuario",
                table: "CatClientesConfig",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesLNegocio_IdCliente",
                table: "CatClientesLNegocio",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesLNegocio_IdLineaNegocio",
                table: "CatClientesLNegocio",
                column: "IdLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesLNegocio_IdUsuarioRegistro",
                table: "CatClientesLNegocio",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesProyectos_IdCatCliente",
                table: "CatClientesProyectos",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesProyectos_IdCatProyecto",
                table: "CatClientesProyectos",
                column: "IdCatProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesProyectos_IdUsuarioRegistro",
                table: "CatClientesProyectos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesServicioAduana_IdCatAduana",
                table: "CatClientesServicioAduana",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesServicioAduana_IdCatClientes",
                table: "CatClientesServicioAduana",
                column: "IdCatClientes");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesServicioAduana_IdCatServicio",
                table: "CatClientesServicioAduana",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_CatClientesServicioAduana_IdUsuarioRegistro",
                table: "CatClientesServicioAduana",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatClienteTarifa_CatClientesIdCatCliente",
                table: "CatClienteTarifa",
                column: "CatClientesIdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_CatClienteTarifa_catContenedorIdCatContenedor",
                table: "CatClienteTarifa",
                column: "catContenedorIdCatContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_CatClienteTarifa_IdCatCteServAduana",
                table: "CatClienteTarifa",
                column: "IdCatCteServAduana");

            migrationBuilder.CreateIndex(
                name: "IX_CatClienteTarifa_IdUsuarioRegistro",
                table: "CatClienteTarifa",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatContenedor_IdUsuarioRegistro",
                table: "CatContenedor",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatDocumentos_IdUsuarioRegistro",
                table: "CatDocumentos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatEmpresas_IdUsuarioRegistro",
                table: "CatEmpresas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocio_IdUsuarioRegistro",
                table: "CatLineaNegocio",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocioTarifa_IdCatLineaNegocio",
                table: "CatLineaNegocioTarifa",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocioTarifa_IdCatServicio",
                table: "CatLineaNegocioTarifa",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocioTarifa_IdUsuarioRegistro",
                table: "CatLineaNegocioTarifa",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocioTariPrecio_IdCatAduana",
                table: "CatLineaNegocioTariPrecio",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocioTariPrecio_IdCatEmpresa",
                table: "CatLineaNegocioTariPrecio",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocioTariPrecio_IdCatLineaNegocioTarifa",
                table: "CatLineaNegocioTariPrecio",
                column: "IdCatLineaNegocioTarifa");

            migrationBuilder.CreateIndex(
                name: "IX_CatLineaNegocioTariPrecio_IdUsuarioRegistro",
                table: "CatLineaNegocioTariPrecio",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatPaisEstados_IdCatPais",
                table: "CatPaisEstados",
                column: "IdCatPais");

            migrationBuilder.CreateIndex(
                name: "IX_CatPatios_IdUsuarioRegistro",
                table: "CatPatios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatPatiosConfig_IdCatPatios",
                table: "CatPatiosConfig",
                column: "IdCatPatios");

            migrationBuilder.CreateIndex(
                name: "IX_CatPatiosConfig_IdCatTipoConfig",
                table: "CatPatiosConfig",
                column: "IdCatTipoConfig");

            migrationBuilder.CreateIndex(
                name: "IX_CatPatiosConfig_IdUsuarioRegistro",
                table: "CatPatiosConfig",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatPatiosNavieras_IdCatNaviera",
                table: "CatPatiosNavieras",
                column: "IdCatNaviera");

            migrationBuilder.CreateIndex(
                name: "IX_CatPatiosNavieras_IdCatPatios",
                table: "CatPatiosNavieras",
                column: "IdCatPatios");

            migrationBuilder.CreateIndex(
                name: "IX_CatPatiosNavieras_IdUsuarioRegistro",
                table: "CatPatiosNavieras",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatPermisos_IdUsuarioRegistro",
                table: "CatPermisos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedores_IdCatPaises",
                table: "CatProveedores",
                column: "IdCatPaises");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedores_IdCatPaisEstados",
                table: "CatProveedores",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedores_IdUsuarioRegistro",
                table: "CatProveedores",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresConfig_IdCatProveedor",
                table: "CatProveedoresConfig",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresConfig_IdCatTipoConfig",
                table: "CatProveedoresConfig",
                column: "IdCatTipoConfig");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresConfig_IdUsuarioRegistro",
                table: "CatProveedoresConfig",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresPatios_IdCatAduana",
                table: "CatProveedoresPatios",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresPatios_IdCatPatio",
                table: "CatProveedoresPatios",
                column: "IdCatPatio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresPatios_IdCatProveedor",
                table: "CatProveedoresPatios",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresPatios_IdUsuarioRegistro",
                table: "CatProveedoresPatios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresTarifaPatio_IdCatPatio",
                table: "CatProveedoresTarifaPatio",
                column: "IdCatPatio");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresTarifaPatio_IdCatProvTarifas",
                table: "CatProveedoresTarifaPatio",
                column: "IdCatProvTarifas");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresTarifaPatio_IdUsuarioRegistro",
                table: "CatProveedoresTarifaPatio",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresTarifas_IdCatAduana",
                table: "CatProveedoresTarifas",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresTarifas_IdCatProveedores",
                table: "CatProveedoresTarifas",
                column: "IdCatProveedores");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresTarifas_IdCatServicio",
                table: "CatProveedoresTarifas",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_CatProveedoresTarifas_IdUsuarioRegistro",
                table: "CatProveedoresTarifas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatProyectos_IdCatEmpresas",
                table: "CatProyectos",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_CatProyectos_IdCatLineaNegocio",
                table: "CatProyectos",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_CatProyectos_IdUsuarioRegistro",
                table: "CatProyectos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatReferenciaEstado_IdUsuarioRegistro",
                table: "CatReferenciaEstado",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatRoles_IdUsuarioRegistro",
                table: "CatRoles",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatServicios_IdCatEmpresas",
                table: "CatServicios",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_CatServicios_IdUsuarioRegistro",
                table: "CatServicios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatSistemas_IdCatCliente",
                table: "CatSistemas",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_CatSistemas_IdCatEmpresas",
                table: "CatSistemas",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_CatSucursales_IdCatEmpresas",
                table: "CatSucursales",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_CatSucursales_IdUsuarioRegistro",
                table: "CatSucursales",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatTipoContenedor_IdUsuarioRegistro",
                table: "CatTipoContenedor",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatTipoIncidenciaEvento_IdCatTipoEventoCron",
                table: "CatTipoIncidenciaEvento",
                column: "IdCatTipoEventoCron");

            migrationBuilder.CreateIndex(
                name: "IX_CatTipoIncidenciaEvento_IdCatTipoIncidenciaCron",
                table: "CatTipoIncidenciaEvento",
                column: "IdCatTipoIncidenciaCron");

            migrationBuilder.CreateIndex(
                name: "IX_CatTransportistas_IdCatEmpresas",
                table: "CatTransportistas",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_CatTransportistas_IdCatPaises",
                table: "CatTransportistas",
                column: "IdCatPaises");

            migrationBuilder.CreateIndex(
                name: "IX_CatTransportistas_IdCatPaisEstados",
                table: "CatTransportistas",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_CatTransportistas_IdUsuarioRegistro",
                table: "CatTransportistas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuarioRoles_IdCatRoles",
                table: "CatUsuarioRoles",
                column: "IdCatRoles");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuarioRoles_IdCatUsuarios",
                table: "CatUsuarioRoles",
                column: "IdCatUsuarios");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuarios_IdCatTipoPuesto",
                table: "CatUsuarios",
                column: "IdCatTipoPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosAduanas_IdCatAduana",
                table: "CatUsuariosAduanas",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosAduanas_IdCatUsuario",
                table: "CatUsuariosAduanas",
                column: "IdCatUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosClientes_IdCatCliente",
                table: "CatUsuariosClientes",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosClientes_IdCatUsuario",
                table: "CatUsuariosClientes",
                column: "IdCatUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosEmpresa_IdCatCliente",
                table: "CatUsuariosEmpresa",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosEmpresa_idCatEmpresa",
                table: "CatUsuariosEmpresa",
                column: "idCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosEmpresa_IdCatProveedor",
                table: "CatUsuariosEmpresa",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosEmpresa_IdCatUsuarios",
                table: "CatUsuariosEmpresa",
                column: "IdCatUsuarios");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosPermisos_IdCatPermisos",
                table: "CatUsuariosPermisos",
                column: "IdCatPermisos");

            migrationBuilder.CreateIndex(
                name: "IX_CatUsuariosPermisos_IdCatUsuarios",
                table: "CatUsuariosPermisos",
                column: "IdCatUsuarios");

            migrationBuilder.CreateIndex(
                name: "IX_DtAcarreos_IdCatProveedor",
                table: "DtAcarreos",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DtAcarreos_IdCatServicio",
                table: "DtAcarreos",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_DtAcarreos_IdCatTipoEstado",
                table: "DtAcarreos",
                column: "IdCatTipoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_DtAcarreos_IdCliente",
                table: "DtAcarreos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DtAcarreos_IdEmpresa",
                table: "DtAcarreos",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_DtAcarreos_IdOrden",
                table: "DtAcarreos",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_DtAcarreos_IdUsuarioRegistro",
                table: "DtAcarreos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaDet_IdUltimaMilla",
                table: "DtUltimaMillaDet",
                column: "IdUltimaMilla");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaEnc_IdCatEmpresa",
                table: "DtUltimaMillaEnc",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaEnc_IdCatProveedor",
                table: "DtUltimaMillaEnc",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaEnc_IdCatServicio",
                table: "DtUltimaMillaEnc",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaEnc_IdCatTipoTransporte",
                table: "DtUltimaMillaEnc",
                column: "IdCatTipoTransporte");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaEnc_IdCliente",
                table: "DtUltimaMillaEnc",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaEnc_IdOrden",
                table: "DtUltimaMillaEnc",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_DtUltimaMillaEnc_IdTipoEstado",
                table: "DtUltimaMillaEnc",
                column: "IdTipoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_IntegraAnticipoSol_IdOrden",
                table: "IntegraAnticipoSol",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_IntegraAnticipoSol_IdPeticionesContenedor",
                table: "IntegraAnticipoSol",
                column: "IdPeticionesContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_IntegraAnticipoSol_IdPeticionesReferencia",
                table: "IntegraAnticipoSol",
                column: "IdPeticionesReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_IdCatServicio",
                schema: "SLO",
                table: "integracionFacturaDet",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_IddIntFacturaEnc",
                schema: "SLO",
                table: "integracionFacturaDet",
                column: "IddIntFacturaEnc");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_IdPeticionesContenedor",
                schema: "SLO",
                table: "integracionFacturaDet",
                column: "IdPeticionesContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_IdPeticionesReferencia",
                schema: "SLO",
                table: "integracionFacturaDet",
                column: "IdPeticionesReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_Idpservicios",
                schema: "SLO",
                table: "integracionFacturaDet",
                column: "Idpservicios");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdDtAcarreos",
                schema: "SLO",
                table: "integracionFacturaEnc",
                column: "IdDtAcarreos");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdDtUltimaMillaEnc",
                schema: "SLO",
                table: "integracionFacturaEnc",
                column: "IdDtUltimaMillaEnc");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdIntReferencia",
                schema: "SLO",
                table: "integracionFacturaEnc",
                column: "IdIntReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdOrden",
                schema: "SLO",
                table: "integracionFacturaEnc",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdPeticionesReferencia",
                schema: "SLO",
                table: "integracionFacturaEnc",
                column: "IdPeticionesReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_integracionReferencia_IdOrden",
                schema: "SLO",
                table: "integracionReferencia",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_IntegraReferencia_IdOrden",
                table: "IntegraReferencia",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatAduana",
                table: "Ordenes",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatCliente",
                table: "Ordenes",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatEmpresa",
                table: "Ordenes",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatLineaNegocio",
                table: "Ordenes",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatProveedor",
                table: "Ordenes",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatProyecto",
                table: "Ordenes",
                column: "IdCatProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatSistema",
                table: "Ordenes",
                column: "IdCatSistema");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdCatSucursal",
                table: "Ordenes",
                column: "IdCatSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdUsuario",
                table: "Ordenes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedorCron_IdCatTipoIncidenciaEvento",
                schema: "vacios",
                table: "peticionesContenedorCron",
                column: "IdCatTipoIncidenciaEvento");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedorCron_IdContenedor",
                schema: "vacios",
                table: "peticionesContenedorCron",
                column: "IdContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedorCron_IdRegistroUsuario",
                schema: "vacios",
                table: "peticionesContenedorCron",
                column: "IdRegistroUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedorCron_IdServicio",
                schema: "vacios",
                table: "peticionesContenedorCron",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_AduanaId",
                schema: "SLO",
                table: "peticionesContenedores",
                column: "AduanaId");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_ClienteId",
                schema: "SLO",
                table: "peticionesContenedores",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_IdClienteFacturarA",
                schema: "SLO",
                table: "peticionesContenedores",
                column: "IdClienteFacturarA");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_IdEstadoContenedor",
                schema: "SLO",
                table: "peticionesContenedores",
                column: "IdEstadoContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_IdReferencia",
                schema: "SLO",
                table: "peticionesContenedores",
                column: "IdReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_Naviera_Id",
                schema: "SLO",
                table: "peticionesContenedores",
                column: "Naviera_Id");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_PatioId",
                schema: "SLO",
                table: "peticionesContenedores",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_AduanaId",
                table: "PeticionesContenedores",
                column: "AduanaId");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_ClienteId",
                table: "PeticionesContenedores",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_IdCatTipoContenedor",
                table: "PeticionesContenedores",
                column: "IdCatTipoContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_IdClienteFacturarA",
                table: "PeticionesContenedores",
                column: "IdClienteFacturarA");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_IdEstadoContenedor",
                table: "PeticionesContenedores",
                column: "IdEstadoContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_IdReferencia",
                table: "PeticionesContenedores",
                column: "IdReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_Naviera_Id",
                table: "PeticionesContenedores",
                column: "Naviera_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesContenedores_PatioId",
                table: "PeticionesContenedores",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesDocumentos_IdContenedor",
                table: "PeticionesDocumentos",
                column: "IdContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesDocumentos_IdServicio",
                table: "PeticionesDocumentos",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesDocumentos_IdTipoDocumento",
                table: "PeticionesDocumentos",
                column: "IdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesReferencias_IdCatReferenciaEstado",
                schema: "SLO",
                table: "peticionesReferencias",
                column: "IdCatReferenciaEstado");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesReferencias_IdOrden",
                schema: "SLO",
                table: "peticionesReferencias",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesReferencias_IdCatReferenciaEstado",
                table: "PeticionesReferencias",
                column: "IdCatReferenciaEstado");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesReferencias_IdOrden",
                table: "PeticionesReferencias",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdCatServicio",
                schema: "SLO",
                table: "peticionesServicios",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdClienteFacturarA",
                schema: "SLO",
                table: "peticionesServicios",
                column: "IdClienteFacturarA");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdContenedor",
                schema: "SLO",
                table: "peticionesServicios",
                column: "IdContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdEstadoServicio",
                schema: "SLO",
                table: "peticionesServicios",
                column: "IdEstadoServicio");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesServicios_IdCatServicio",
                table: "PeticionesServicios",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesServicios_IdClienteFacturarA",
                table: "PeticionesServicios",
                column: "IdClienteFacturarA");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesServicios_IdContenedor",
                table: "PeticionesServicios",
                column: "IdContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_PeticionesServicios_IdEstadoServicio",
                table: "PeticionesServicios",
                column: "IdEstadoServicio");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdAgenteAduanal033",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdAgenteAduanal033");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdCatAduana",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdCatCliente",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdCatClienteFacturarA",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdCatClienteFacturarA");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdCatClienteImpoExpo",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdCatClienteImpoExpo");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdCatProveedor",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdManiobristaOrigen023",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdManiobristaOrigen023");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdOrdenServicio",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdOrdenServicio");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_001_REFERENCIA_nIdViaje002",
                schema: "WMS",
                table: "WMS_001_REFERENCIA",
                column: "nIdViaje002");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_002_VIAJE_nIdBarco003",
                schema: "WMS",
                table: "WMS_002_VIAJE",
                column: "nIdBarco003");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_002_VIAJE_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_002_VIAJE",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_003_BARCO_nIdCatNaviera",
                schema: "WMS",
                table: "WMS_003_BARCO",
                column: "nIdCatNaviera");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_003_BARCO_nIdCatPais",
                schema: "WMS",
                table: "WMS_003_BARCO",
                column: "nIdCatPais");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_004_SERVICIO_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_004_SERVICIO",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_006_PAQUETE_SERVICIO_ServiciosId",
                schema: "WMS",
                table: "WMS_006_PAQUETE_SERVICIO",
                column: "ServiciosId");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_008_ALMACEN_nIdTipoCargaAlmacen007",
                schema: "WMS",
                table: "WMS_008_ALMACEN",
                column: "nIdTipoCargaAlmacen007");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_010_ZONA_ALMACENAJE_nIdAlmacen008",
                schema: "WMS",
                table: "WMS_010_ZONA_ALMACENAJE",
                column: "nIdAlmacen008");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_010_ZONA_ALMACENAJE_nIdTipoZonaAlmacenaje009",
                schema: "WMS",
                table: "WMS_010_ZONA_ALMACENAJE",
                column: "nIdTipoZonaAlmacenaje009");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_011_UBICACION_nIdZonaAlmacenaje010",
                schema: "WMS",
                table: "WMS_011_UBICACION",
                column: "nIdZonaAlmacenaje010");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_014_INVENTARIO_nIdTipoEmbalaje013",
                schema: "WMS",
                table: "WMS_014_INVENTARIO",
                column: "nIdTipoEmbalaje013");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_014_INVENTARIO_nIdUbicacion011",
                schema: "WMS",
                table: "WMS_014_INVENTARIO",
                column: "nIdUbicacion011");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_014_INVENTARIO_nIdUnidadMediad012",
                schema: "WMS",
                table: "WMS_014_INVENTARIO",
                column: "nIdUnidadMediad012");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_015_TARJA_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_015_TARJA",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_015_TARJA_nIdReferencia001",
                schema: "WMS",
                table: "WMS_015_TARJA",
                column: "nIdReferencia001");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_016_PARTIDA_nIdInventario014",
                schema: "WMS",
                table: "WMS_016_PARTIDA",
                column: "nIdInventario014");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_016_PARTIDA_nIdInventarioOrigen014",
                schema: "WMS",
                table: "WMS_016_PARTIDA",
                column: "nIdInventarioOrigen014");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_016_PARTIDA_nIdTarja015",
                schema: "WMS",
                table: "WMS_016_PARTIDA",
                column: "nIdTarja015");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_016_PARTIDA_nIdTarjaOrigen015",
                schema: "WMS",
                table: "WMS_016_PARTIDA",
                column: "nIdTarjaOrigen015");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_017_INVENTARIO_ALO_nIdInventario014",
                schema: "WMS",
                table: "WMS_017_INVENTARIO_ALO",
                column: "nIdInventario014");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_017_INVENTARIO_ALO_nIdReferencia001",
                schema: "WMS",
                table: "WMS_017_INVENTARIO_ALO",
                column: "nIdReferencia001");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_017_INVENTARIO_ALO_nIdReferenciaOrigen001",
                schema: "WMS",
                table: "WMS_017_INVENTARIO_ALO",
                column: "nIdReferenciaOrigen001");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_018_FOLIO_SERVICIO_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_018_FOLIO_SERVICIO",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_018_FOLIO_SERVICIO_nIdInventario014",
                schema: "WMS",
                table: "WMS_018_FOLIO_SERVICIO",
                column: "nIdInventario014");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_018_FOLIO_SERVICIO_nIdPaquete005",
                schema: "WMS",
                table: "WMS_018_FOLIO_SERVICIO",
                column: "nIdPaquete005");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_018_FOLIO_SERVICIO_nIdReferencia001",
                schema: "WMS",
                table: "WMS_018_FOLIO_SERVICIO",
                column: "nIdReferencia001");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_018_FOLIO_SERVICIO_nIdTarja015",
                schema: "WMS",
                table: "WMS_018_FOLIO_SERVICIO",
                column: "nIdTarja015");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_021_LINEA_TRANS_OPERADOR_nIdCatTransportista",
                schema: "WMS",
                table: "WMS_021_LINEA_TRANS_OPERADOR",
                column: "nIdCatTransportista");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_022_LINEA_TRANS_TRANSPORTE_nIdCatTransportista",
                schema: "WMS",
                table: "WMS_022_LINEA_TRANS_TRANSPORTE",
                column: "nIdCatTransportista");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_024_CONTROL_TRANSPORTE_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_024_CONTROL_TRANSPORTE",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_024_CONTROL_TRANSPORTE_nIdCatTransportista",
                schema: "WMS",
                table: "WMS_024_CONTROL_TRANSPORTE",
                column: "nIdCatTransportista");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_024_CONTROL_TRANSPORTE_nIdLineaTansOperador021",
                schema: "WMS",
                table: "WMS_024_CONTROL_TRANSPORTE",
                column: "nIdLineaTansOperador021");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_024_CONTROL_TRANSPORTE_nIdLineaTransTransporte022",
                schema: "WMS",
                table: "WMS_024_CONTROL_TRANSPORTE",
                column: "nIdLineaTransTransporte022");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_024_CONTROL_TRANSPORTE_nIdManiobristaDestino023",
                schema: "WMS",
                table: "WMS_024_CONTROL_TRANSPORTE",
                column: "nIdManiobristaDestino023");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_024_CONTROL_TRANSPORTE_nIdManiobristaOrigen023",
                schema: "WMS",
                table: "WMS_024_CONTROL_TRANSPORTE",
                column: "nIdManiobristaOrigen023");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_024_CONTROL_TRANSPORTE_nIdTipoTransporte020",
                schema: "WMS",
                table: "WMS_024_CONTROL_TRANSPORTE",
                column: "nIdTipoTransporte020");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE_IdInventario",
                schema: "WMS",
                table: "WMS_025_FOLIO_SERVICIO_MASTER_DETALLE",
                column: "IdInventario");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE_IdTarja",
                schema: "WMS",
                table: "WMS_025_FOLIO_SERVICIO_MASTER_DETALLE",
                column: "IdTarja");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_025_FOLIO_SERVICIO_MASTER_DETALLE_nIdReferencia001",
                schema: "WMS",
                table: "WMS_025_FOLIO_SERVICIO_MASTER_DETALLE",
                column: "nIdReferencia001");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_026_SOLICITUD_TRASLADO_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_026_SOLICITUD_TRASLADO",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_026_SOLICITUD_TRASLADO_nIdControlTransporte024",
                schema: "WMS",
                table: "WMS_026_SOLICITUD_TRASLADO",
                column: "nIdControlTransporte024");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_026_SOLICITUD_TRASLADO_nIdFolioServicio018",
                schema: "WMS",
                table: "WMS_026_SOLICITUD_TRASLADO",
                column: "nIdFolioServicio018");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_026_SOLICITUD_TRASLADO_nIdFolioServicioMasterDetalle025",
                schema: "WMS",
                table: "WMS_026_SOLICITUD_TRASLADO",
                column: "nIdFolioServicioMasterDetalle025");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_026_SOLICITUD_TRASLADO_nIdManiobristaDestino023",
                schema: "WMS",
                table: "WMS_026_SOLICITUD_TRASLADO",
                column: "nIdManiobristaDestino023");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_026_SOLICITUD_TRASLADO_nIdManiobristaOrigen023",
                schema: "WMS",
                table: "WMS_026_SOLICITUD_TRASLADO",
                column: "nIdManiobristaOrigen023");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_028_SERVICIO_FOTOGRAFIA_nIdFolioServicio018",
                schema: "WMS",
                table: "WMS_028_SERVICIO_FOTOGRAFIA",
                column: "nIdFolioServicio018");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_028_SERVICIO_FOTOGRAFIA_nIdInventario014",
                schema: "WMS",
                table: "WMS_028_SERVICIO_FOTOGRAFIA",
                column: "nIdInventario014");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_032_BITACORA_AVERIA_nIdCodigoDesperfecto029",
                schema: "WMS",
                table: "WMS_032_BITACORA_AVERIA",
                column: "nIdCodigoDesperfecto029");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_032_BITACORA_AVERIA_nIdInventario014",
                schema: "WMS",
                table: "WMS_032_BITACORA_AVERIA",
                column: "nIdInventario014");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_032_BITACORA_AVERIA_nIdTipoDesperfecto030",
                schema: "WMS",
                table: "WMS_032_BITACORA_AVERIA",
                column: "nIdTipoDesperfecto030");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_032_BITACORA_AVERIA_nIdTipoSeveridad031",
                schema: "WMS",
                table: "WMS_032_BITACORA_AVERIA",
                column: "nIdTipoSeveridad031");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatClientesConfig");

            migrationBuilder.DropTable(
                name: "CatClientesLNegocio");

            migrationBuilder.DropTable(
                name: "CatClientesProyectos");

            migrationBuilder.DropTable(
                name: "CatClienteTarifa");

            migrationBuilder.DropTable(
                name: "CatLineaNegocioTariPrecio");

            migrationBuilder.DropTable(
                name: "CatPatiosConfig");

            migrationBuilder.DropTable(
                name: "CatPatiosNavieras");

            migrationBuilder.DropTable(
                name: "CatProveedoresConfig");

            migrationBuilder.DropTable(
                name: "CatProveedoresPatios");

            migrationBuilder.DropTable(
                name: "CatProveedoresTarifaPatio");

            migrationBuilder.DropTable(
                name: "CatUsuarioRoles");

            migrationBuilder.DropTable(
                name: "CatUsuariosAduanas");

            migrationBuilder.DropTable(
                name: "CatUsuariosClientes");

            migrationBuilder.DropTable(
                name: "CatUsuariosEmpresa");

            migrationBuilder.DropTable(
                name: "CatUsuariosPermisos");

            migrationBuilder.DropTable(
                name: "DtUltimaMillaDet");

            migrationBuilder.DropTable(
                name: "IntegraAnticipoSol");

            migrationBuilder.DropTable(
                name: "integracionFacturaDet",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "peticionesContenedorCron",
                schema: "vacios");

            migrationBuilder.DropTable(
                name: "PeticionesDocumentos");

            migrationBuilder.DropTable(
                name: "WMS_006_PAQUETE_SERVICIO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_016_PARTIDA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_017_INVENTARIO_ALO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_026_SOLICITUD_TRASLADO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_028_SERVICIO_FOTOGRAFIA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_032_BITACORA_AVERIA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "CatClientesServicioAduana");

            migrationBuilder.DropTable(
                name: "CatContenedor");

            migrationBuilder.DropTable(
                name: "CatLineaNegocioTarifa");

            migrationBuilder.DropTable(
                name: "CatTiposConfig");

            migrationBuilder.DropTable(
                name: "CatProveedoresTarifas");

            migrationBuilder.DropTable(
                name: "CatRoles");

            migrationBuilder.DropTable(
                name: "CatPermisos");

            migrationBuilder.DropTable(
                name: "IntegraReferencia");

            migrationBuilder.DropTable(
                name: "integracionFacturaEnc",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "peticionesServicios",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "CatTipoIncidenciaEvento");

            migrationBuilder.DropTable(
                name: "CatDocumentos");

            migrationBuilder.DropTable(
                name: "PeticionesServicios");

            migrationBuilder.DropTable(
                name: "WMS_004_SERVICIO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_024_CONTROL_TRANSPORTE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_025_FOLIO_SERVICIO_MASTER_DETALLE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_018_FOLIO_SERVICIO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_029_CODIGO_DESPERFECTO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_030_TIPO_DESPERFECTO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_031_TIPO_SEVERIDAD",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "DtAcarreos");

            migrationBuilder.DropTable(
                name: "DtUltimaMillaEnc");

            migrationBuilder.DropTable(
                name: "integracionReferencia",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "peticionesContenedores",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "CatTipoEventosCron");

            migrationBuilder.DropTable(
                name: "CatTipoIncidenciaCron");

            migrationBuilder.DropTable(
                name: "PeticionesContenedores");

            migrationBuilder.DropTable(
                name: "WMS_020_TIPO_TRANSPORTE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_021_LINEA_TRANS_OPERADOR",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_022_LINEA_TRANS_TRANSPORTE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_005_PAQUETE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_014_INVENTARIO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_015_TARJA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "CatServicios");

            migrationBuilder.DropTable(
                name: "CatTipoEstados");

            migrationBuilder.DropTable(
                name: "CatTipoTransporte");

            migrationBuilder.DropTable(
                name: "peticionesReferencias",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "CatPatios");

            migrationBuilder.DropTable(
                name: "CatTipoContenedor");

            migrationBuilder.DropTable(
                name: "PeticionesReferencias");

            migrationBuilder.DropTable(
                name: "CatTransportistas");

            migrationBuilder.DropTable(
                name: "WMS_011_UBICACION",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_012_UNIDAD_MEDIDA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_013_TIPO_EMBALAJE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_001_REFERENCIA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "CatReferenciaEstado");

            migrationBuilder.DropTable(
                name: "WMS_010_ZONA_ALMACENAJE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "WMS_002_VIAJE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_023_MANIOBRISTA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_033_AGENTE_ADUANAL",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_008_ALMACEN",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_009_TIPO_ZONA_ALMACENAJE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "CatAduana");

            migrationBuilder.DropTable(
                name: "CatProveedores");

            migrationBuilder.DropTable(
                name: "CatProyectos");

            migrationBuilder.DropTable(
                name: "CatSistemas");

            migrationBuilder.DropTable(
                name: "CatSucursales");

            migrationBuilder.DropTable(
                name: "WMS_003_BARCO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_007_TIPO_CARGA_ALMACEN",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "CatLineaNegocio");

            migrationBuilder.DropTable(
                name: "CatClientes");

            migrationBuilder.DropTable(
                name: "CatEmpresas");

            migrationBuilder.DropTable(
                name: "CatNavieras");

            migrationBuilder.DropTable(
                name: "CatPaisEstados");

            migrationBuilder.DropTable(
                name: "CatUsuarios");

            migrationBuilder.DropTable(
                name: "CatPaises");

            migrationBuilder.DropTable(
                name: "CatTipoPuesto");
        }
    }
}
