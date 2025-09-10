using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALOG.Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoTransporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SLO");

            migrationBuilder.EnsureSchema(
                name: "WMS");

            migrationBuilder.EnsureSchema(
                name: "vacios");

            migrationBuilder.CreateTable(
                name: "catNavieras",
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
                    table.PrimaryKey("PK_catNavieras", x => x.IdCatNaviera);
                });

            migrationBuilder.CreateTable(
                name: "catPaisCP",
                columns: table => new
                {
                    IdCodigoPostal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPostal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClaveEstado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClaveMunicipio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClaveMunDel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catPaisCP", x => x.IdCodigoPostal);
                });

            migrationBuilder.CreateTable(
                name: "catPaises",
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
                    table.PrimaryKey("PK_catPaises", x => x.IdCatPaises);
                });

            migrationBuilder.CreateTable(
                name: "catTipoClasificacion",
                columns: table => new
                {
                    IdCatTipoClasificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catTipoClasificacion", x => x.IdCatTipoClasificacion);
                });

            migrationBuilder.CreateTable(
                name: "catTipoContactos",
                columns: table => new
                {
                    IdCatTipoContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catTipoContactos", x => x.IdCatTipoContacto);
                });

            migrationBuilder.CreateTable(
                name: "catTipoEstados",
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
                    table.PrimaryKey("PK_catTipoEstados", x => x.IdCatTipoEstados);
                });

            migrationBuilder.CreateTable(
                name: "catTipoEventosCron",
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
                    table.PrimaryKey("PK_catTipoEventosCron", x => x.IdCatTipoEventoCron);
                });

            migrationBuilder.CreateTable(
                name: "catTipoIncidenciasCron",
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
                    table.PrimaryKey("PK_catTipoIncidenciasCron", x => x.IdCatTipoIncidenciaCron);
                });

            migrationBuilder.CreateTable(
                name: "catTipoPuesto",
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
                    table.PrimaryKey("PK_catTipoPuesto", x => x.IdCatTipoPuesto);
                });

            migrationBuilder.CreateTable(
                name: "catTiposConfig",
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
                    table.PrimaryKey("PK_catTiposConfig", x => x.IdCatTiposConfig);
                });

            migrationBuilder.CreateTable(
                name: "catTipoTransporte",
                columns: table => new
                {
                    IdCatTipoTransporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catTipoTransporte", x => x.IdCatTipoTransporte);
                });

            migrationBuilder.CreateTable(
                name: "integracionConceptosFactura",
                columns: table => new
                {
                    IdConceptoFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdConcepto1G = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionConceptosFactura", x => x.IdConceptoFactura);
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
                name: "catPaisEstados",
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
                    table.PrimaryKey("PK_catPaisEstados", x => x.IdCatPaisEstados);
                    table.ForeignKey(
                        name: "FK_catPaisEstados_catPaises_IdCatPais",
                        column: x => x.IdCatPais,
                        principalTable: "catPaises",
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
                        name: "FK_WMS_003_BARCO_catNavieras_nIdCatNaviera",
                        column: x => x.nIdCatNaviera,
                        principalTable: "catNavieras",
                        principalColumn: "IdCatNaviera");
                    table.ForeignKey(
                        name: "FK_WMS_003_BARCO_catPaises_nIdCatPais",
                        column: x => x.nIdCatPais,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises");
                });

            migrationBuilder.CreateTable(
                name: "catTipoIncidenciaEvento",
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
                    table.PrimaryKey("PK_catTipoIncidenciaEvento", x => x.IdCatTipoIncidenciaEvento);
                    table.ForeignKey(
                        name: "FK_catTipoIncidenciaEvento_catTipoEventosCron_IdCatTipoEventoCron",
                        column: x => x.IdCatTipoEventoCron,
                        principalTable: "catTipoEventosCron",
                        principalColumn: "IdCatTipoEventoCron",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catTipoIncidenciaEvento_catTipoIncidenciasCron_IdCatTipoIncidenciaCron",
                        column: x => x.IdCatTipoIncidenciaCron,
                        principalTable: "catTipoIncidenciasCron",
                        principalColumn: "IdCatTipoIncidenciaCron",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catUsuarios",
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
                    table.PrimaryKey("PK_catUsuarios", x => x.IdCatUsuarios);
                    table.ForeignKey(
                        name: "FK_catUsuarios_catTipoPuesto_IdCatTipoPuesto",
                        column: x => x.IdCatTipoPuesto,
                        principalTable: "catTipoPuesto",
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
                name: "catPaisMunicipios",
                columns: table => new
                {
                    IdCatMunicipios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaisEstado = table.Column<int>(type: "int", nullable: false),
                    ClaveEntidadSAT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ClaveMunicipioSAT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ClaveMunDel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catPaisMunicipios", x => x.IdCatMunicipios);
                    table.ForeignKey(
                        name: "FK_catPaisMunicipios_catPaisEstados_IdPaisEstado",
                        column: x => x.IdPaisEstado,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anticiposEnc",
                columns: table => new
                {
                    IdAnticiposEnc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioSolicita = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioAutoriza = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaAutorizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    Autorizado = table.Column<bool>(type: "bit", nullable: false),
                    IdTipoAnticipo = table.Column<int>(type: "int", nullable: false),
                    EstadoAnticipo = table.Column<int>(type: "int", nullable: false),
                    IdTipoMoneda = table.Column<int>(type: "int", nullable: false),
                    TipoCambio = table.Column<double>(type: "float", nullable: false),
                    Clave1G = table.Column<int>(type: "int", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anticiposEnc", x => x.IdAnticiposEnc);
                    table.ForeignKey(
                        name: "FK_anticiposEnc_catUsuarios_IdUsuarioAutoriza",
                        column: x => x.IdUsuarioAutoriza,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_anticiposEnc_catUsuarios_IdUsuarioSolicita",
                        column: x => x.IdUsuarioSolicita,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catAduana",
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
                    table.PrimaryKey("PK_catAduana", x => x.IdCatAduana);
                    table.ForeignKey(
                        name: "FK_catAduana_catPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catAduana_catPaises_IdCatPais",
                        column: x => x.IdCatPais,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catAduana_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catAgentesAduanales",
                columns: table => new
                {
                    IdCatAgenteAduanal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catAgentesAduanales", x => x.IdCatAgenteAduanal);
                    table.ForeignKey(
                        name: "FK_catAgentesAduanales_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catClientes",
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
                    table.PrimaryKey("PK_catClientes", x => x.IdCatCliente);
                    table.ForeignKey(
                        name: "FK_catClientes_catPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientes_catPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientes_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catContenedor",
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
                    table.PrimaryKey("PK_catContenedor", x => x.IdCatContenedor);
                    table.ForeignKey(
                        name: "FK_catContenedor_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catDocumento",
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
                    table.PrimaryKey("PK_catDocumento", x => x.IdCatDocumento);
                    table.ForeignKey(
                        name: "FK_catDocumento_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catEmpresas",
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
                    table.PrimaryKey("PK_catEmpresas", x => x.IdCatEmpresa);
                    table.ForeignKey(
                        name: "FK_catEmpresas_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catLineaNegocio",
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
                    table.PrimaryKey("PK_catLineaNegocio", x => x.IdCatLineaNegocio);
                    table.ForeignKey(
                        name: "FK_catLineaNegocio_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catPatios",
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
                    table.PrimaryKey("PK_catPatios", x => x.IdCatPatios);
                    table.ForeignKey(
                        name: "FK_catPatios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catPermisos",
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
                    table.PrimaryKey("PK_catPermisos", x => x.IdCatPermisos);
                    table.ForeignKey(
                        name: "FK_catPermisos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catProveedores",
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
                    table.PrimaryKey("PK_catProveedores", x => x.IdCatProveedor);
                    table.ForeignKey(
                        name: "FK_catProveedores_catPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedores_catPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedores_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catReferenciaEstado",
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
                    table.PrimaryKey("PK_catReferenciaEstado", x => x.IdCatReferenciaEstado);
                    table.ForeignKey(
                        name: "FK_catReferenciaEstado_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catRoles",
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
                    table.PrimaryKey("PK_catRoles", x => x.IdCatRoles);
                    table.ForeignKey(
                        name: "FK_catRoles_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatTipoMoneda",
                columns: table => new
                {
                    IdCatTipoMoneda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    ClaveSAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatTipoMoneda", x => x.IdCatTipoMoneda);
                    table.ForeignKey(
                        name: "FK_CatTipoMoneda_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catTiposContenedor",
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
                    table.PrimaryKey("PK_catTiposContenedor", x => x.IdCatTipoContenedor);
                    table.ForeignKey(
                        name: "FK_catTiposContenedor_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
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
                name: "catRecintos",
                columns: table => new
                {
                    IdCatRecinto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClaveRecinto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdAduana = table.Column<int>(type: "int", nullable: false),
                    CatAduanaIdCatAduana = table.Column<int>(type: "int", nullable: true),
                    IdCatPais = table.Column<int>(type: "int", nullable: false),
                    IdCatPaisEstados = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCatUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catRecintos", x => x.IdCatRecinto);
                    table.ForeignKey(
                        name: "FK_catRecintos_catAduana_CatAduanaIdCatAduana",
                        column: x => x.CatAduanaIdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_catRecintos_catPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catRecintos_catPaises_IdCatPais",
                        column: x => x.IdCatPais,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catRecintos_catUsuarios_IdCatUsuarioRegistro",
                        column: x => x.IdCatUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catUsuariosAduanas",
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
                    table.PrimaryKey("PK_catUsuariosAduanas", x => x.IdCatUsuarioAduana);
                    table.ForeignKey(
                        name: "FK_catUsuariosAduanas_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catUsuariosAduanas_catUsuarios_IdCatUsuario",
                        column: x => x.IdCatUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catClientesClasificacion",
                columns: table => new
                {
                    IdCatClientesClasif = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatclientes = table.Column<int>(type: "int", nullable: false),
                    IdCatClasificacion = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catClientesClasificacion", x => x.IdCatClientesClasif);
                    table.ForeignKey(
                        name: "FK_catClientesClasificacion_catClientes_IdCatclientes",
                        column: x => x.IdCatclientes,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesClasificacion_catTipoClasificacion_IdCatClasificacion",
                        column: x => x.IdCatClasificacion,
                        principalTable: "catTipoClasificacion",
                        principalColumn: "IdCatTipoClasificacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catClientesConfig",
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
                    table.PrimaryKey("PK_catClientesConfig", x => x.IdCatClientesConfig);
                    table.ForeignKey(
                        name: "FK_catClientesConfig_catClientes_IdCatClientes",
                        column: x => x.IdCatClientes,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesConfig_catTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "catTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesConfig_catUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catClientesContactos",
                columns: table => new
                {
                    IdCatCteContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatCliente = table.Column<int>(type: "int", nullable: false),
                    IdCatTipoContacto = table.Column<int>(type: "int", nullable: false),
                    catTipoContactoIdCatTipoContacto = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catClientesContactos", x => x.IdCatCteContacto);
                    table.ForeignKey(
                        name: "FK_catClientesContactos_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesContactos_catTipoContactos_catTipoContactoIdCatTipoContacto",
                        column: x => x.catTipoContactoIdCatTipoContacto,
                        principalTable: "catTipoContactos",
                        principalColumn: "IdCatTipoContacto");
                });

            migrationBuilder.CreateTable(
                name: "catClientesExternos",
                columns: table => new
                {
                    IdCatClienteExterno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatCliente = table.Column<int>(type: "int", nullable: false),
                    IdCatClientesAsociado = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catClientesExternos", x => x.IdCatClienteExterno);
                    table.ForeignKey(
                        name: "FK_catClientesExternos_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesExternos_catClientes_IdCatClientesAsociado",
                        column: x => x.IdCatClientesAsociado,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesExternos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catUsuariosClientes",
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
                    table.PrimaryKey("PK_catUsuariosClientes", x => x.IdCatUsuarioCliente);
                    table.ForeignKey(
                        name: "FK_catUsuariosClientes_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_catUsuariosClientes_catUsuarios_IdCatUsuario",
                        column: x => x.IdCatUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catServicios",
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
                    table.PrimaryKey("PK_catServicios", x => x.IdCatServicio);
                    table.ForeignKey(
                        name: "FK_catServicios_catEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catServicios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catSistemas",
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
                    table.PrimaryKey("PK_catSistemas", x => x.IdCatSistema);
                    table.ForeignKey(
                        name: "FK_catSistemas_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catSistemas_catEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catSucursales",
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
                    table.PrimaryKey("PK_catSucursales", x => x.IdCatSucursal);
                    table.ForeignKey(
                        name: "FK_catSucursales_catEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catSucursales_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catTransportistas",
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
                    table.PrimaryKey("PK_catTransportistas", x => x.IdCatTransportista);
                    table.ForeignKey(
                        name: "FK_catTransportistas_catEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catTransportistas_catPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catTransportistas_catPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catTransportistas_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
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
                    nPedoBls = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                        name: "FK_WMS_002_VIAJE_WMS_003_BARCO_nIdBarco003",
                        column: x => x.nIdBarco003,
                        principalSchema: "WMS",
                        principalTable: "WMS_003_BARCO",
                        principalColumn: "nIdBarco003");
                    table.ForeignKey(
                        name: "FK_WMS_002_VIAJE_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_WMS_004_SERVICIO_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_035_ORDEN_SALIDA",
                schema: "WMS",
                columns: table => new
                {
                    nIdOrdenSalida035 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nFolio = table.Column<int>(type: "int", nullable: false),
                    nTipo = table.Column<int>(type: "int", nullable: false),
                    nEstado = table.Column<int>(type: "int", nullable: false),
                    sObservaciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    dFechaSalidaProgramada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaAutorizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaLiberacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sMotivoCancelacion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_035_ORDEN_SALIDA", x => x.nIdOrdenSalida035);
                    table.ForeignKey(
                        name: "FK_WMS_035_ORDEN_SALIDA_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catClientesLNegocio",
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
                    table.PrimaryKey("PK_catClientesLNegocio", x => x.IdCatClientesLNegocio);
                    table.ForeignKey(
                        name: "FK_catClientesLNegocio_catClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesLNegocio_catLineaNegocio_IdLineaNegocio",
                        column: x => x.IdLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesLNegocio_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catFormatoReferencias",
                columns: table => new
                {
                    IdCatFormatoRef = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catFormatoReferencias", x => x.IdCatFormatoRef);
                    table.ForeignKey(
                        name: "FK_catFormatoReferencias_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catFormatoReferencias_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catProyectos",
                columns: table => new
                {
                    IdProyectos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Acronimo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresas = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catProyectos", x => x.IdProyectos);
                    table.ForeignKey(
                        name: "FK_catProyectos_catEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProyectos_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProyectos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catPatiosConfig",
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
                    table.PrimaryKey("PK_catPatiosConfig", x => x.IdCatPatiosConfig);
                    table.ForeignKey(
                        name: "FK_catPatiosConfig_catPatios_IdCatPatios",
                        column: x => x.IdCatPatios,
                        principalTable: "catPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catPatiosConfig_catTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "catTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catPatiosConfig_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catPatiosNavieras",
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
                    table.PrimaryKey("PK_catPatiosNavieras", x => x.IdCatPatiosNavieras);
                    table.ForeignKey(
                        name: "FK_catPatiosNavieras_catNavieras_IdCatNaviera",
                        column: x => x.IdCatNaviera,
                        principalTable: "catNavieras",
                        principalColumn: "IdCatNaviera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catPatiosNavieras_catPatios_IdCatPatios",
                        column: x => x.IdCatPatios,
                        principalTable: "catPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catPatiosNavieras_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catUsuariosPermisos",
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
                    table.PrimaryKey("PK_catUsuariosPermisos", x => x.IdCatUsuariosPermisos);
                    table.ForeignKey(
                        name: "FK_catUsuariosPermisos_catPermisos_IdCatPermisos",
                        column: x => x.IdCatPermisos,
                        principalTable: "catPermisos",
                        principalColumn: "IdCatPermisos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catUsuariosPermisos_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catProveedoresClasif",
                columns: table => new
                {
                    IdCatProvClasif = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: false),
                    IdCatTipoClasificacion = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catProveedoresClasif", x => x.IdCatProvClasif);
                    table.ForeignKey(
                        name: "FK_catProveedoresClasif_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresClasif_catTipoClasificacion_IdCatTipoClasificacion",
                        column: x => x.IdCatTipoClasificacion,
                        principalTable: "catTipoClasificacion",
                        principalColumn: "IdCatTipoClasificacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catProveedoresConfigs",
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
                    table.PrimaryKey("PK_catProveedoresConfigs", x => x.IdCatProvConfig);
                    table.ForeignKey(
                        name: "FK_catProveedoresConfigs_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresConfigs_catTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "catTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresConfigs_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catProveedoresContactos",
                columns: table => new
                {
                    IdCatProvContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatProveedor = table.Column<int>(type: "int", nullable: false),
                    IdCatTipoContacto = table.Column<int>(type: "int", nullable: false),
                    catTipoContactoIdCatTipoContacto = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catProveedoresContactos", x => x.IdCatProvContacto);
                    table.ForeignKey(
                        name: "FK_catProveedoresContactos_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresContactos_catTipoContactos_catTipoContactoIdCatTipoContacto",
                        column: x => x.catTipoContactoIdCatTipoContacto,
                        principalTable: "catTipoContactos",
                        principalColumn: "IdCatTipoContacto");
                });

            migrationBuilder.CreateTable(
                name: "catProveedoresPatios",
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
                    table.PrimaryKey("PK_catProveedoresPatios", x => x.IdCatProveedorPatio);
                    table.ForeignKey(
                        name: "FK_catProveedoresPatios_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresPatios_catPatios_IdCatPatio",
                        column: x => x.IdCatPatio,
                        principalTable: "catPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresPatios_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresPatios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catUsuariosEmpresa",
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
                    table.PrimaryKey("PK_catUsuariosEmpresa", x => x.IdCatUsuariosEmpresa);
                    table.ForeignKey(
                        name: "FK_catUsuariosEmpresa_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_catUsuariosEmpresa_catEmpresas_idCatEmpresa",
                        column: x => x.idCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa");
                    table.ForeignKey(
                        name: "FK_catUsuariosEmpresa_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor");
                    table.ForeignKey(
                        name: "FK_catUsuariosEmpresa_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catRolesPermisos",
                columns: table => new
                {
                    IdCatRolesPermisos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatRoles = table.Column<int>(type: "int", nullable: false),
                    IdCatPermisos = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catRolesPermisos", x => x.IdCatRolesPermisos);
                    table.ForeignKey(
                        name: "FK_catRolesPermisos_catPermisos_IdCatPermisos",
                        column: x => x.IdCatPermisos,
                        principalTable: "catPermisos",
                        principalColumn: "IdCatPermisos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catRolesPermisos_catRoles_IdCatRoles",
                        column: x => x.IdCatRoles,
                        principalTable: "catRoles",
                        principalColumn: "IdCatRoles",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catUsuarioRoles",
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
                    table.PrimaryKey("PK_catUsuarioRoles", x => x.IdCatUsuariosRoles);
                    table.ForeignKey(
                        name: "FK_catUsuarioRoles_catRoles_IdCatRoles",
                        column: x => x.IdCatRoles,
                        principalTable: "catRoles",
                        principalColumn: "IdCatRoles",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catUsuarioRoles_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "provisionesEnc",
                columns: table => new
                {
                    IdProvisionesEnc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdCatTipoMoneda = table.Column<int>(type: "int", nullable: false),
                    TipoCambio = table.Column<double>(type: "float", nullable: false),
                    ImporteTotal = table.Column<double>(type: "float", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    IdCatUsuarios = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaTimbrado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provisionesEnc", x => x.IdProvisionesEnc);
                    table.ForeignKey(
                        name: "FK_provisionesEnc_CatTipoMoneda_IdCatTipoMoneda",
                        column: x => x.IdCatTipoMoneda,
                        principalTable: "CatTipoMoneda",
                        principalColumn: "IdCatTipoMoneda",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_provisionesEnc_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
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
                    nAltura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nCapacidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "catClientesServicioAduana",
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
                    table.PrimaryKey("PK_catClientesServicioAduana", x => x.IdCatCteServAduana);
                    table.ForeignKey(
                        name: "FK_catClientesServicioAduana_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesServicioAduana_catClientes_IdCatClientes",
                        column: x => x.IdCatClientes,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesServicioAduana_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesServicioAduana_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catLineaNegocioTarifas",
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
                    table.PrimaryKey("PK_catLineaNegocioTarifas", x => x.IdCatLineaNegocioTarifa);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTarifas_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTarifas_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTarifas_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catProveedoresTarifas",
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
                    table.PrimaryKey("PK_catProveedoresTarifas", x => x.IdCatProvTarifas);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifas_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifas_catProveedores_IdCatProveedores",
                        column: x => x.IdCatProveedores,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifas_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifas_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catSeriesFacturas",
                columns: table => new
                {
                    IdCatSeriesFact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    IdCatAduana = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdCatSucursal = table.Column<int>(type: "int", nullable: false),
                    catSucursalesIdCatSucursal = table.Column<int>(type: "int", nullable: true),
                    Serie = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catSeriesFacturas", x => x.IdCatSeriesFact);
                    table.ForeignKey(
                        name: "FK_catSeriesFacturas_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catSeriesFacturas_catEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catSeriesFacturas_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catSeriesFacturas_catSucursales_catSucursalesIdCatSucursal",
                        column: x => x.catSucursalesIdCatSucursal,
                        principalTable: "catSucursales",
                        principalColumn: "IdCatSucursal");
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
                        name: "FK_WMS_021_LINEA_TRANS_OPERADOR_catTransportistas_nIdCatTransportista",
                        column: x => x.nIdCatTransportista,
                        principalTable: "catTransportistas",
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
                        name: "FK_WMS_022_LINEA_TRANS_TRANSPORTE_catTransportistas_nIdCatTransportista",
                        column: x => x.nIdCatTransportista,
                        principalTable: "catTransportistas",
                        principalColumn: "IdCatTransportista",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaqueteServicio",
                schema: "WMS",
                columns: table => new
                {
                    PaquetesId = table.Column<int>(type: "int", nullable: false),
                    ServiciosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaqueteServicio", x => new { x.PaquetesId, x.ServiciosId });
                    table.ForeignKey(
                        name: "FK_PaqueteServicio_WMS_004_SERVICIO_ServiciosId",
                        column: x => x.ServiciosId,
                        principalSchema: "WMS",
                        principalTable: "WMS_004_SERVICIO",
                        principalColumn: "nIdServicio004",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaqueteServicio_WMS_005_PAQUETE_PaquetesId",
                        column: x => x.PaquetesId,
                        principalSchema: "WMS",
                        principalTable: "WMS_005_PAQUETE",
                        principalColumn: "nIdPaquete005",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_039_SALIDA",
                schema: "WMS",
                columns: table => new
                {
                    nIdSalida039 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdOrdenSalida035 = table.Column<int>(type: "int", nullable: true),
                    nFolio = table.Column<int>(type: "int", nullable: false),
                    nEstado = table.Column<int>(type: "int", nullable: false),
                    dFechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dFechaSalidaManual = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sMotivoCancelacion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    nIdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_039_SALIDA", x => x.nIdSalida039);
                    table.ForeignKey(
                        name: "FK_WMS_039_SALIDA_WMS_035_ORDEN_SALIDA_nIdOrdenSalida035",
                        column: x => x.nIdOrdenSalida035,
                        principalSchema: "WMS",
                        principalTable: "WMS_035_ORDEN_SALIDA",
                        principalColumn: "nIdOrdenSalida035");
                    table.ForeignKey(
                        name: "FK_WMS_039_SALIDA_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catFormatoRefDets",
                columns: table => new
                {
                    IdCatFormatoRefDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatFormatoRef = table.Column<int>(type: "int", nullable: false),
                    OrdenCampo = table.Column<int>(type: "int", nullable: false),
                    TipoDato = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Especificacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Valor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catFormatoRefDets", x => x.IdCatFormatoRefDet);
                    table.ForeignKey(
                        name: "FK_catFormatoRefDets_catFormatoReferencias_IdCatFormatoRef",
                        column: x => x.IdCatFormatoRef,
                        principalTable: "catFormatoReferencias",
                        principalColumn: "IdCatFormatoRef",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catClientesProyectos",
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
                    table.PrimaryKey("PK_catClientesProyectos", x => x.IdCatClientesProyectos);
                    table.ForeignKey(
                        name: "FK_catClientesProyectos_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesProyectos_catProyectos_IdCatProyecto",
                        column: x => x.IdCatProyecto,
                        principalTable: "catProyectos",
                        principalColumn: "IdProyectos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClientesProyectos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ordenes",
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
                    table.PrimaryKey("PK_ordenes", x => x.IdOrden);
                    table.ForeignKey(
                        name: "FK_ordenes_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_ordenes_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordenes_catEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordenes_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordenes_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor");
                    table.ForeignKey(
                        name: "FK_ordenes_catProyectos_IdCatProyecto",
                        column: x => x.IdCatProyecto,
                        principalTable: "catProyectos",
                        principalColumn: "IdProyectos");
                    table.ForeignKey(
                        name: "FK_ordenes_catSistemas_IdCatSistema",
                        column: x => x.IdCatSistema,
                        principalTable: "catSistemas",
                        principalColumn: "IdCatSistema");
                    table.ForeignKey(
                        name: "FK_ordenes_catSucursales_IdCatSucursal",
                        column: x => x.IdCatSucursal,
                        principalTable: "catSucursales",
                        principalColumn: "IdCatSucursal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordenes_catUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "catUsuarios",
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
                    nPesoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nCantidadFinal = table.Column<int>(type: "int", nullable: true),
                    nPesoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                name: "catClienteTarifa",
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
                    table.PrimaryKey("PK_catClienteTarifa", x => x.IdCatClienteTarifa);
                    table.ForeignKey(
                        name: "FK_catClienteTarifa_catClientesServicioAduana_IdCatCteServAduana",
                        column: x => x.IdCatCteServAduana,
                        principalTable: "catClientesServicioAduana",
                        principalColumn: "IdCatCteServAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catClienteTarifa_catClientes_CatClientesIdCatCliente",
                        column: x => x.CatClientesIdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_catClienteTarifa_catContenedor_catContenedorIdCatContenedor",
                        column: x => x.catContenedorIdCatContenedor,
                        principalTable: "catContenedor",
                        principalColumn: "IdCatContenedor");
                    table.ForeignKey(
                        name: "FK_catClienteTarifa_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catLineaNegocioTariPrecios",
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
                    table.PrimaryKey("PK_catLineaNegocioTariPrecios", x => x.IdCatLineNegocioTariPrecio);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTariPrecios_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTariPrecios_catEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTariPrecios_catLineaNegocioTarifas_IdCatLineaNegocioTarifa",
                        column: x => x.IdCatLineaNegocioTarifa,
                        principalTable: "catLineaNegocioTarifas",
                        principalColumn: "IdCatLineaNegocioTarifa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTariPrecios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "catProveedoresTarifaPatios",
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
                    table.PrimaryKey("PK_catProveedoresTarifaPatios", x => x.IdCatProvTarifaPatio);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifaPatios_catPatios_IdCatPatio",
                        column: x => x.IdCatPatio,
                        principalTable: "catPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifaPatios_catProveedoresTarifas_IdCatProvTarifas",
                        column: x => x.IdCatProvTarifas,
                        principalTable: "catProveedoresTarifas",
                        principalColumn: "IdCatProvTarifas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifaPatios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
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
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_024_CONTROL_TRANSPORTE_catTransportistas_nIdCatTransportista",
                        column: x => x.nIdCatTransportista,
                        principalTable: "catTransportistas",
                        principalColumn: "IdCatTransportista");
                });

            migrationBuilder.CreateTable(
                name: "anticiposDet",
                columns: table => new
                {
                    IdAnticiposDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAnticiposEnc = table.Column<int>(type: "int", nullable: false),
                    IdLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    RefTransferencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuentaClabe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefProveedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatServicios = table.Column<int>(type: "int", nullable: false),
                    CveServicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCarga = table.Column<int>(type: "int", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoProv = table.Column<int>(type: "int", nullable: false),
                    refNumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaBancaria = table.Column<int>(type: "int", nullable: false),
                    SaldoAplicado = table.Column<double>(type: "float", nullable: false),
                    txt_app_monex_descargado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clave1G = table.Column<int>(type: "int", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anticiposDet", x => x.IdAnticiposDet);
                    table.ForeignKey(
                        name: "FK_anticiposDet_anticiposEnc_IdAnticiposEnc",
                        column: x => x.IdAnticiposEnc,
                        principalTable: "anticiposEnc",
                        principalColumn: "IdAnticiposEnc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_anticiposDet_catLineaNegocio_IdLineaNegocio",
                        column: x => x.IdLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_anticiposDet_catProveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_anticiposDet_catServicios_IdCatServicios",
                        column: x => x.IdCatServicios,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_anticiposDet_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dtAcarreos",
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
                    table.PrimaryKey("PK_dtAcarreos", x => x.IdDtAcarreos);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catEmpresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catTipoEstados_IdCatTipoEstado",
                        column: x => x.IdCatTipoEstado,
                        principalTable: "catTipoEstados",
                        principalColumn: "IdCatTipoEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dtUltimaMillaEnc",
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
                    table.PrimaryKey("PK_dtUltimaMillaEnc", x => x.IdDtUltMillaEnc);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catProveedores_IdCatProveedor",
                        column: x => x.IdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catTipoEstados_IdTipoEstado",
                        column: x => x.IdTipoEstado,
                        principalTable: "catTipoEstados",
                        principalColumn: "IdCatTipoEstados",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catTipoTransporte_IdCatTipoTransporte",
                        column: x => x.IdCatTipoTransporte,
                        principalTable: "catTipoTransporte",
                        principalColumn: "IdCatTipoTransporte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "integracionReferencia",
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
                    table.PrimaryKey("PK_integracionReferencia", x => x.IdIntReferencia);
                    table.ForeignKey(
                        name: "FK_integracionReferencia_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
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
                        name: "FK_integracionReferencia_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peticionesReferencias",
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
                    table.PrimaryKey("PK_peticionesReferencias", x => x.IdReferencia);
                    table.ForeignKey(
                        name: "FK_peticionesReferencias_catReferenciaEstado_IdCatReferenciaEstado",
                        column: x => x.IdCatReferenciaEstado,
                        principalTable: "catReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesReferencias_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
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
                        name: "FK_peticionesReferencias_catReferenciaEstado_IdCatReferenciaEstado",
                        column: x => x.IdCatReferenciaEstado,
                        principalTable: "catReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado");
                    table.ForeignKey(
                        name: "FK_peticionesReferencias_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prefacturaEnc",
                columns: table => new
                {
                    IdPrefacturaEnc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    FechaPrefactura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    ReferenciaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    TipoCambio = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    IdAduana = table.Column<int>(type: "int", nullable: false),
                    Aduana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    EstadoPrefactra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Clave1G = table.Column<int>(type: "int", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prefacturaEnc", x => x.IdPrefacturaEnc);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_catAduana_IdAduana",
                        column: x => x.IdAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_catClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_catUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
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
                    nPesoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nBultosFinal = table.Column<int>(type: "int", nullable: true),
                    nPesoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_catAduana_nIdCatAduana",
                        column: x => x.nIdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_catClientes_nIdCatCliente",
                        column: x => x.nIdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_catClientes_nIdCatClienteFacturarA",
                        column: x => x.nIdCatClienteFacturarA,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_catClientes_nIdCatClienteImpoExpo",
                        column: x => x.nIdCatClienteImpoExpo,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_catProveedores_nIdCatProveedor",
                        column: x => x.nIdCatProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor");
                    table.ForeignKey(
                        name: "FK_WMS_001_REFERENCIA_ordenes_nIdOrdenServicio",
                        column: x => x.nIdOrdenServicio,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden");
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
                name: "WMS_037_ORDEN_SALIDA_INVENTARIO",
                schema: "WMS",
                columns: table => new
                {
                    nIdOrdenSalidaInventario037 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdOrdenSalida035 = table.Column<int>(type: "int", nullable: true),
                    nIdInventario014 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_037_ORDEN_SALIDA_INVENTARIO", x => x.nIdOrdenSalidaInventario037);
                    table.ForeignKey(
                        name: "FK_WMS_037_ORDEN_SALIDA_INVENTARIO_WMS_014_INVENTARIO_nIdInventario014",
                        column: x => x.nIdInventario014,
                        principalSchema: "WMS",
                        principalTable: "WMS_014_INVENTARIO",
                        principalColumn: "nIdInventario014");
                    table.ForeignKey(
                        name: "FK_WMS_037_ORDEN_SALIDA_INVENTARIO_WMS_035_ORDEN_SALIDA_nIdOrdenSalida035",
                        column: x => x.nIdOrdenSalida035,
                        principalSchema: "WMS",
                        principalTable: "WMS_035_ORDEN_SALIDA",
                        principalColumn: "nIdOrdenSalida035");
                });

            migrationBuilder.CreateTable(
                name: "WMS_040_SALIDA_CONTROL_TRANSPORTE",
                schema: "WMS",
                columns: table => new
                {
                    nIdSalidaControlTransporte040 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdOrdenSalida035 = table.Column<int>(type: "int", nullable: true),
                    nIdSalida039 = table.Column<int>(type: "int", nullable: true),
                    nIdControlTransporte024 = table.Column<int>(type: "int", nullable: true),
                    bActivo = table.Column<bool>(type: "bit", nullable: false),
                    dFechaCancelacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sMotivoCancelacion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    nIdCatUsuarioCancelacion = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_040_SALIDA_CONTROL_TRANSPORTE", x => x.nIdSalidaControlTransporte040);
                    table.ForeignKey(
                        name: "FK_WMS_040_SALIDA_CONTROL_TRANSPORTE_WMS_024_CONTROL_TRANSPORTE_nIdControlTransporte024",
                        column: x => x.nIdControlTransporte024,
                        principalSchema: "WMS",
                        principalTable: "WMS_024_CONTROL_TRANSPORTE",
                        principalColumn: "nIdControlTransporte024");
                    table.ForeignKey(
                        name: "FK_WMS_040_SALIDA_CONTROL_TRANSPORTE_WMS_035_ORDEN_SALIDA_nIdOrdenSalida035",
                        column: x => x.nIdOrdenSalida035,
                        principalSchema: "WMS",
                        principalTable: "WMS_035_ORDEN_SALIDA",
                        principalColumn: "nIdOrdenSalida035");
                    table.ForeignKey(
                        name: "FK_WMS_040_SALIDA_CONTROL_TRANSPORTE_WMS_039_SALIDA_nIdSalida039",
                        column: x => x.nIdSalida039,
                        principalSchema: "WMS",
                        principalTable: "WMS_039_SALIDA",
                        principalColumn: "nIdSalida039");
                    table.ForeignKey(
                        name: "FK_WMS_040_SALIDA_CONTROL_TRANSPORTE_catUsuarios_nIdCatUsuarioCancelacion",
                        column: x => x.nIdCatUsuarioCancelacion,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios");
                });

            migrationBuilder.CreateTable(
                name: "dtUltimaMillaDet",
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
                    table.PrimaryKey("PK_dtUltimaMillaDet", x => x.IdDtUltimaMillaDet);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaDet_dtUltimaMillaEnc_IdUltimaMilla",
                        column: x => x.IdUltimaMilla,
                        principalTable: "dtUltimaMillaEnc",
                        principalColumn: "IdDtUltMillaEnc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "integracionFacturaEnc",
                columns: table => new
                {
                    IdIntFacturaEnc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    IdPeticionesReferencia = table.Column<int>(type: "int", nullable: true),
                    IdDtAcarreos = table.Column<int>(type: "int", nullable: true),
                    IdDtUltimaMillaEnc = table.Column<int>(type: "int", nullable: true),
                    IdSolicitudFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCompaniaExterna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveClienteExterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConceptoFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveSATMoneda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveSATUsoCFDI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacturacionAutomatica = table.Column<bool>(type: "bit", nullable: false),
                    CierreReferencia = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    Estado1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaWS1G = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdSucursalExterna = table.Column<int>(type: "int", nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdIntReferencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionFacturaEnc", x => x.IdIntFacturaEnc);
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_dtAcarreos_IdDtAcarreos",
                        column: x => x.IdDtAcarreos,
                        principalTable: "dtAcarreos",
                        principalColumn: "IdDtAcarreos");
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_dtUltimaMillaEnc_IdDtUltimaMillaEnc",
                        column: x => x.IdDtUltimaMillaEnc,
                        principalTable: "dtUltimaMillaEnc",
                        principalColumn: "IdDtUltMillaEnc");
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_peticionesReferencias_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia");
                });

            migrationBuilder.CreateTable(
                name: "peticionesContenedores",
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
                    table.PrimaryKey("PK_peticionesContenedores", x => x.IdContenedor);
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catAduana_AduanaId",
                        column: x => x.AduanaId,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catClientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catNavieras_Naviera_Id",
                        column: x => x.Naviera_Id,
                        principalTable: "catNavieras",
                        principalColumn: "IdCatNaviera");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catPatios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "catPatios",
                        principalColumn: "IdCatPatios");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catReferenciaEstado_IdEstadoContenedor",
                        column: x => x.IdEstadoContenedor,
                        principalTable: "catReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catTiposContenedor_IdCatTipoContenedor",
                        column: x => x.IdCatTipoContenedor,
                        principalTable: "catTiposContenedor",
                        principalColumn: "IdCatTipoContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_peticionesReferencias_IdReferencia",
                        column: x => x.IdReferencia,
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
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
                        name: "FK_integracionFacturaEnc_dtAcarreos_IdDtAcarreos",
                        column: x => x.IdDtAcarreos,
                        principalTable: "dtAcarreos",
                        principalColumn: "IdDtAcarreos");
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_dtUltimaMillaEnc_IdDtUltimaMillaEnc",
                        column: x => x.IdDtUltimaMillaEnc,
                        principalTable: "dtUltimaMillaEnc",
                        principalColumn: "IdDtUltMillaEnc");
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_integracionReferencia_IdIntReferencia",
                        column: x => x.IdIntReferencia,
                        principalSchema: "SLO",
                        principalTable: "integracionReferencia",
                        principalColumn: "IdIntReferencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaEnc_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
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
                        name: "FK_peticionesContenedores_catAduana_AduanaId",
                        column: x => x.AduanaId,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catClientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catNavieras_Naviera_Id",
                        column: x => x.Naviera_Id,
                        principalTable: "catNavieras",
                        principalColumn: "IdCatNaviera");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catPatios_PatioId",
                        column: x => x.PatioId,
                        principalTable: "catPatios",
                        principalColumn: "IdCatPatios");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_catReferenciaEstado_IdEstadoContenedor",
                        column: x => x.IdEstadoContenedor,
                        principalTable: "catReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado");
                    table.ForeignKey(
                        name: "FK_peticionesContenedores_peticionesReferencias_IdReferencia",
                        column: x => x.IdReferencia,
                        principalSchema: "SLO",
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia");
                });

            migrationBuilder.CreateTable(
                name: "prefacturaDet",
                columns: table => new
                {
                    IdPrefacturaDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrefacturaEnc = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    OrdenesIdOrden = table.Column<int>(type: "int", nullable: true),
                    IdLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    numeroPartida = table.Column<int>(type: "int", nullable: false),
                    ReferenciaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdServicio = table.Column<int>(type: "int", nullable: false),
                    IdServicio1G = table.Column<int>(type: "int", nullable: false),
                    DescripcionServicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<float>(type: "real", nullable: false),
                    Factconv = table.Column<double>(type: "float", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Descuento1 = table.Column<double>(type: "float", nullable: false),
                    Cve_esq = table.Column<int>(type: "int", nullable: false),
                    Impuesto4 = table.Column<double>(type: "float", nullable: false),
                    TotalImpuesto4 = table.Column<double>(type: "float", nullable: false),
                    TipoCambio = table.Column<double>(type: "float", nullable: false),
                    TotalPartida = table.Column<double>(type: "float", nullable: false),
                    Contenedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sello = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Factura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroParte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProyectos = table.Column<int>(type: "int", nullable: false),
                    IdAduana = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Folio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enviado = table.Column<bool>(type: "bit", nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prefacturaDet", x => x.IdPrefacturaDet);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_catAduana_IdAduana",
                        column: x => x.IdAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_catLineaNegocio_IdLineaNegocio",
                        column: x => x.IdLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_catProyectos_IdProyectos",
                        column: x => x.IdProyectos,
                        principalTable: "catProyectos",
                        principalColumn: "IdProyectos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_catServicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_ordenes_OrdenesIdOrden",
                        column: x => x.OrdenesIdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden");
                    table.ForeignKey(
                        name: "FK_prefacturaDet_prefacturaEnc_IdPrefacturaEnc",
                        column: x => x.IdPrefacturaEnc,
                        principalTable: "prefacturaEnc",
                        principalColumn: "IdPrefacturaEnc",
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
                        name: "FK_WMS_015_TARJA_WMS_001_REFERENCIA_nIdReferencia001",
                        column: x => x.nIdReferencia001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                    table.ForeignKey(
                        name: "FK_WMS_015_TARJA_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
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
                name: "WMS_034_REFERENCIA_BOOKING_BL",
                schema: "WMS",
                columns: table => new
                {
                    nIdReferenciaBookingBl034 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sBookingBl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nIdReferencia001 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_034_REFERENCIA_BOOKING_BL", x => x.nIdReferenciaBookingBl034);
                    table.ForeignKey(
                        name: "FK_WMS_034_REFERENCIA_BOOKING_BL_WMS_001_REFERENCIA_nIdReferencia001",
                        column: x => x.nIdReferencia001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                });

            migrationBuilder.CreateTable(
                name: "WMS_036_LIBERACION",
                schema: "WMS",
                columns: table => new
                {
                    nIdLiberacion036 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdReferencia001 = table.Column<int>(type: "int", nullable: true),
                    nIdOrdenSalida035 = table.Column<int>(type: "int", nullable: true),
                    nIdCatCliente = table.Column<int>(type: "int", nullable: true),
                    nIdAgenteAduanal033 = table.Column<int>(type: "int", nullable: true),
                    nIdManiobristaDestino023 = table.Column<int>(type: "int", nullable: true),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_036_LIBERACION", x => x.nIdLiberacion036);
                    table.ForeignKey(
                        name: "FK_WMS_036_LIBERACION_WMS_001_REFERENCIA_nIdReferencia001",
                        column: x => x.nIdReferencia001,
                        principalSchema: "WMS",
                        principalTable: "WMS_001_REFERENCIA",
                        principalColumn: "nIdReferencia001");
                    table.ForeignKey(
                        name: "FK_WMS_036_LIBERACION_WMS_023_MANIOBRISTA_nIdManiobristaDestino023",
                        column: x => x.nIdManiobristaDestino023,
                        principalSchema: "WMS",
                        principalTable: "WMS_023_MANIOBRISTA",
                        principalColumn: "nIdManiobrista023");
                    table.ForeignKey(
                        name: "FK_WMS_036_LIBERACION_WMS_033_AGENTE_ADUANAL_nIdAgenteAduanal033",
                        column: x => x.nIdAgenteAduanal033,
                        principalSchema: "WMS",
                        principalTable: "WMS_033_AGENTE_ADUANAL",
                        principalColumn: "nIdAgenteAduanal033");
                    table.ForeignKey(
                        name: "FK_WMS_036_LIBERACION_WMS_035_ORDEN_SALIDA_nIdOrdenSalida035",
                        column: x => x.nIdOrdenSalida035,
                        principalSchema: "WMS",
                        principalTable: "WMS_035_ORDEN_SALIDA",
                        principalColumn: "nIdOrdenSalida035");
                    table.ForeignKey(
                        name: "FK_WMS_036_LIBERACION_catClientes_nIdCatCliente",
                        column: x => x.nIdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                });

            migrationBuilder.CreateTable(
                name: "integracionFacturaEst",
                columns: table => new
                {
                    IdIntFacturaEst = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdIntFacturaEnc = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSolicitudFacturacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FolioFactura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoTotal = table.Column<double>(type: "float", nullable: false),
                    MontoPagado = table.Column<double>(type: "float", nullable: false),
                    MontoNotaCredito = table.Column<double>(type: "float", nullable: false),
                    SaldoFactura = table.Column<double>(type: "float", nullable: false),
                    FechaUltimoPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstatusFactura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstatusSolicitud = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionFacturaEst", x => x.IdIntFacturaEst);
                    table.ForeignKey(
                        name: "FK_integracionFacturaEst_integracionFacturaEnc_IdIntFacturaEnc",
                        column: x => x.IdIntFacturaEnc,
                        principalTable: "integracionFacturaEnc",
                        principalColumn: "IdIntFacturaEnc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "integracionAnticipoSol",
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
                    table.PrimaryKey("PK_integracionAnticipoSol", x => x.IdIntAnticipoSol);
                    table.ForeignKey(
                        name: "FK_integracionAnticipoSol_integracionReferencia_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalTable: "integracionReferencia",
                        principalColumn: "IdIntReferencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionAnticipoSol_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionAnticipoSol_peticionesContenedores_IdPeticionesContenedor",
                        column: x => x.IdPeticionesContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "integracionFacturaDet",
                columns: table => new
                {
                    IdIntFacturaDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IddIntFacturaEnc = table.Column<int>(type: "int", nullable: false),
                    ClaveServicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPeticionesReferencia = table.Column<int>(type: "int", nullable: false),
                    IdPeticionesContenedor = table.Column<int>(type: "int", nullable: false),
                    Contenedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CentroCostos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EIR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCatServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionFacturaDet", x => x.IdIntFacturaDet);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_integracionFacturaEnc_IddIntFacturaEnc",
                        column: x => x.IddIntFacturaEnc,
                        principalTable: "integracionFacturaEnc",
                        principalColumn: "IdIntFacturaEnc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_peticionesContenedores_IdPeticionesContenedor",
                        column: x => x.IdPeticionesContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_peticionesReferencias_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peticionesServicios",
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
                    table.PrimaryKey("PK_peticionesServicios", x => x.IdServicio);
                    table.ForeignKey(
                        name: "FK_peticionesServicios_catClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesServicios_catReferenciaEstado_IdEstadoServicio",
                        column: x => x.IdEstadoServicio,
                        principalTable: "catReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesServicios_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesServicios_peticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "provisionesDet",
                columns: table => new
                {
                    IdProvisionesDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProvisionesEnc = table.Column<int>(type: "int", nullable: false),
                    IdReferencia = table.Column<int>(type: "int", nullable: false),
                    IdContenedor = table.Column<int>(type: "int", nullable: false),
                    IdCatUsuarios = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Partida = table.Column<int>(type: "int", nullable: false),
                    Servicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    Impuesto = table.Column<double>(type: "float", nullable: false),
                    TotalPartida = table.Column<double>(type: "float", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provisionesDet", x => x.IdProvisionesDet);
                    table.ForeignKey(
                        name: "FK_provisionesDet_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_provisionesDet_peticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_provisionesDet_peticionesReferencias_IdReferencia",
                        column: x => x.IdReferencia,
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_provisionesDet_provisionesEnc_IdProvisionesEnc",
                        column: x => x.IdProvisionesEnc,
                        principalTable: "provisionesEnc",
                        principalColumn: "IdProvisionesEnc",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_peticionesServicios_catClientes_IdClienteFacturarA",
                        column: x => x.IdClienteFacturarA,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_peticionesServicios_catReferenciaEstado_IdEstadoServicio",
                        column: x => x.IdEstadoServicio,
                        principalTable: "catReferenciaEstado",
                        principalColumn: "IdCatReferenciaEstado");
                    table.ForeignKey(
                        name: "FK_peticionesServicios_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio");
                    table.ForeignKey(
                        name: "FK_peticionesServicios_peticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalSchema: "SLO",
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor");
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
                    table.ForeignKey(
                        name: "FK_WMS_018_FOLIO_SERVICIO_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
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
                name: "WMS_038_LIBERACION_INVENTARIO",
                schema: "WMS",
                columns: table => new
                {
                    nIdLiberacionInventario038 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nIdOrdenSalidaInventario037 = table.Column<int>(type: "int", nullable: true),
                    nIdTarja015 = table.Column<int>(type: "int", nullable: true),
                    nNumeroPartida = table.Column<int>(type: "int", nullable: false),
                    nPeso = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nCantidad = table.Column<int>(type: "int", nullable: true),
                    bLiberacionParcial = table.Column<bool>(type: "bit", nullable: false),
                    dFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_038_LIBERACION_INVENTARIO", x => x.nIdLiberacionInventario038);
                    table.ForeignKey(
                        name: "FK_WMS_038_LIBERACION_INVENTARIO_WMS_015_TARJA_nIdTarja015",
                        column: x => x.nIdTarja015,
                        principalSchema: "WMS",
                        principalTable: "WMS_015_TARJA",
                        principalColumn: "nIdTarja015");
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
                        name: "FK_peticionesContenedorCron_catTipoIncidenciaEvento_IdCatTipoIncidenciaEvento",
                        column: x => x.IdCatTipoIncidenciaEvento,
                        principalTable: "catTipoIncidenciaEvento",
                        principalColumn: "IdCatTipoIncidenciaEvento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedorCron_catUsuarios_IdRegistroUsuario",
                        column: x => x.IdRegistroUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedorCron_peticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesContenedorCron_peticionesServicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "peticionesServicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "peticionesDocumentos",
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
                    table.PrimaryKey("PK_peticionesDocumentos", x => x.IdDocumento);
                    table.ForeignKey(
                        name: "FK_peticionesDocumentos_catDocumento_IdTipoDocumento",
                        column: x => x.IdTipoDocumento,
                        principalTable: "catDocumento",
                        principalColumn: "IdCatDocumento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesDocumentos_peticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_peticionesDocumentos_peticionesServicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "peticionesServicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_integracionFacturaDet_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
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
                    table.ForeignKey(
                        name: "FK_WMS_026_SOLICITUD_TRASLADO_catEmpresas_nIdCatEmpresa",
                        column: x => x.nIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anticiposDet_IdAnticiposEnc",
                table: "anticiposDet",
                column: "IdAnticiposEnc");

            migrationBuilder.CreateIndex(
                name: "IX_anticiposDet_IdCatServicios",
                table: "anticiposDet",
                column: "IdCatServicios");

            migrationBuilder.CreateIndex(
                name: "IX_anticiposDet_IdLineaNegocio",
                table: "anticiposDet",
                column: "IdLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_anticiposDet_IdOrden",
                table: "anticiposDet",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_anticiposDet_IdProveedor",
                table: "anticiposDet",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_anticiposEnc_IdUsuarioAutoriza",
                table: "anticiposEnc",
                column: "IdUsuarioAutoriza");

            migrationBuilder.CreateIndex(
                name: "IX_anticiposEnc_IdUsuarioSolicita",
                table: "anticiposEnc",
                column: "IdUsuarioSolicita");

            migrationBuilder.CreateIndex(
                name: "IX_catAduana_IdCatPais",
                table: "catAduana",
                column: "IdCatPais");

            migrationBuilder.CreateIndex(
                name: "IX_catAduana_IdCatPaisEstados",
                table: "catAduana",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_catAduana_IdUsuarioRegistro",
                table: "catAduana",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catAgentesAduanales_IdUsuarioRegistro",
                table: "catAgentesAduanales",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catClientes_IdCatPaises",
                table: "catClientes",
                column: "IdCatPaises");

            migrationBuilder.CreateIndex(
                name: "IX_catClientes_IdCatPaisEstados",
                table: "catClientes",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_catClientes_IdUsuarioRegistro",
                table: "catClientes",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catClientes_RFC",
                table: "catClientes",
                column: "RFC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesClasificacion_IdCatClasificacion",
                table: "catClientesClasificacion",
                column: "IdCatClasificacion");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesClasificacion_IdCatclientes_IdCatClasificacion",
                table: "catClientesClasificacion",
                columns: new[] { "IdCatclientes", "IdCatClasificacion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesConfig_IdCatClientes_IdCatTipoConfig",
                table: "catClientesConfig",
                columns: new[] { "IdCatClientes", "IdCatTipoConfig" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesConfig_IdCatTipoConfig",
                table: "catClientesConfig",
                column: "IdCatTipoConfig");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesConfig_IdUsuario",
                table: "catClientesConfig",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesContactos_catTipoContactoIdCatTipoContacto",
                table: "catClientesContactos",
                column: "catTipoContactoIdCatTipoContacto");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesContactos_IdCatCliente",
                table: "catClientesContactos",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesExternos_IdCatCliente_IdCatClientesAsociado",
                table: "catClientesExternos",
                columns: new[] { "IdCatCliente", "IdCatClientesAsociado" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesExternos_IdCatClientesAsociado",
                table: "catClientesExternos",
                column: "IdCatClientesAsociado");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesExternos_IdUsuarioRegistro",
                table: "catClientesExternos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesLNegocio_IdCliente_IdLineaNegocio",
                table: "catClientesLNegocio",
                columns: new[] { "IdCliente", "IdLineaNegocio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesLNegocio_IdLineaNegocio",
                table: "catClientesLNegocio",
                column: "IdLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesLNegocio_IdUsuarioRegistro",
                table: "catClientesLNegocio",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesProyectos_IdCatCliente_IdCatProyecto",
                table: "catClientesProyectos",
                columns: new[] { "IdCatCliente", "IdCatProyecto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesProyectos_IdCatProyecto",
                table: "catClientesProyectos",
                column: "IdCatProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesProyectos_IdUsuarioRegistro",
                table: "catClientesProyectos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesServicioAduana_IdCatAduana",
                table: "catClientesServicioAduana",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesServicioAduana_IdCatClientes_IdCatAduana_IdCatServicio",
                table: "catClientesServicioAduana",
                columns: new[] { "IdCatClientes", "IdCatAduana", "IdCatServicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesServicioAduana_IdCatClientes_IdCatServicio_IdCatAduana",
                table: "catClientesServicioAduana",
                columns: new[] { "IdCatClientes", "IdCatServicio", "IdCatAduana" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catClientesServicioAduana_IdCatServicio",
                table: "catClientesServicioAduana",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_catClientesServicioAduana_IdUsuarioRegistro",
                table: "catClientesServicioAduana",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catClienteTarifa_CatClientesIdCatCliente",
                table: "catClienteTarifa",
                column: "CatClientesIdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_catClienteTarifa_catContenedorIdCatContenedor",
                table: "catClienteTarifa",
                column: "catContenedorIdCatContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_catClienteTarifa_IdCatCteServAduana_IdCatContenedor",
                table: "catClienteTarifa",
                columns: new[] { "IdCatCteServAduana", "IdCatContenedor" },
                unique: true,
                filter: "[IdCatContenedor] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catClienteTarifa_IdUsuarioRegistro",
                table: "catClienteTarifa",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catContenedor_IdUsuarioRegistro",
                table: "catContenedor",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catDocumento_IdUsuarioRegistro",
                table: "catDocumento",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catEmpresas_IdUsuarioRegistro",
                table: "catEmpresas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catEmpresas_RFC",
                table: "catEmpresas",
                column: "RFC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catFormatoRefDets_IdCatFormatoRef",
                table: "catFormatoRefDets",
                column: "IdCatFormatoRef");

            migrationBuilder.CreateIndex(
                name: "IX_catFormatoReferencias_IdCatLineaNegocio",
                table: "catFormatoReferencias",
                column: "IdCatLineaNegocio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catFormatoReferencias_IdUsuarioRegistro",
                table: "catFormatoReferencias",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocio_Acronimo",
                table: "catLineaNegocio",
                column: "Acronimo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocio_IdUsuarioRegistro",
                table: "catLineaNegocio",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocio_Nombre",
                table: "catLineaNegocio",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocioTarifas_IdCatLineaNegocio",
                table: "catLineaNegocioTarifas",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocioTarifas_IdCatServicio_IdCatLineaNegocio",
                table: "catLineaNegocioTarifas",
                columns: new[] { "IdCatServicio", "IdCatLineaNegocio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocioTarifas_IdUsuarioRegistro",
                table: "catLineaNegocioTarifas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocioTariPrecios_IdCatAduana",
                table: "catLineaNegocioTariPrecios",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocioTariPrecios_IdCatEmpresa",
                table: "catLineaNegocioTariPrecios",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocioTariPrecios_IdCatLineaNegocioTarifa_IdCatAduana",
                table: "catLineaNegocioTariPrecios",
                columns: new[] { "IdCatLineaNegocioTarifa", "IdCatAduana" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catLineaNegocioTariPrecios_IdUsuarioRegistro",
                table: "catLineaNegocioTariPrecios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catNavieras_Acronimo",
                table: "catNavieras",
                column: "Acronimo",
                unique: true,
                filter: "[Acronimo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catNavieras_RazonSocial",
                table: "catNavieras",
                column: "RazonSocial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catNavieras_RFC",
                table: "catNavieras",
                column: "RFC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catPaises_Nombre",
                table: "catPaises",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catPaisEstados_IdCatPais_Nombre",
                table: "catPaisEstados",
                columns: new[] { "IdCatPais", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catPaisMunicipios_IdPaisEstado",
                table: "catPaisMunicipios",
                column: "IdPaisEstado");

            migrationBuilder.CreateIndex(
                name: "IX_catPatios_IdUsuarioRegistro",
                table: "catPatios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catPatios_RazonSocial",
                table: "catPatios",
                column: "RazonSocial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catPatiosConfig_IdCatPatios_IdCatPatiosConfig",
                table: "catPatiosConfig",
                columns: new[] { "IdCatPatios", "IdCatPatiosConfig" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catPatiosConfig_IdCatTipoConfig",
                table: "catPatiosConfig",
                column: "IdCatTipoConfig");

            migrationBuilder.CreateIndex(
                name: "IX_catPatiosConfig_IdUsuarioRegistro",
                table: "catPatiosConfig",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catPatiosNavieras_IdCatNaviera",
                table: "catPatiosNavieras",
                column: "IdCatNaviera");

            migrationBuilder.CreateIndex(
                name: "IX_catPatiosNavieras_IdCatPatios",
                table: "catPatiosNavieras",
                column: "IdCatPatios");

            migrationBuilder.CreateIndex(
                name: "IX_catPatiosNavieras_IdUsuarioRegistro",
                table: "catPatiosNavieras",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catPermisos_IdUsuarioRegistro",
                table: "catPermisos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catPermisos_Nombre",
                table: "catPermisos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedores_Acronimo",
                table: "catProveedores",
                column: "Acronimo",
                unique: true,
                filter: "[Acronimo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedores_Clave1G",
                table: "catProveedores",
                column: "Clave1G",
                unique: true,
                filter: "[Clave1G] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedores_IdCatPaises",
                table: "catProveedores",
                column: "IdCatPaises");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedores_IdCatPaisEstados",
                table: "catProveedores",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedores_IdUsuarioRegistro",
                table: "catProveedores",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedores_RazonSocial",
                table: "catProveedores",
                column: "RazonSocial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedores_RFC",
                table: "catProveedores",
                column: "RFC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresClasif_IdCatProveedor_IdCatTipoClasificacion",
                table: "catProveedoresClasif",
                columns: new[] { "IdCatProveedor", "IdCatTipoClasificacion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresClasif_IdCatTipoClasificacion",
                table: "catProveedoresClasif",
                column: "IdCatTipoClasificacion");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresConfigs_IdCatProveedor_IdCatTipoConfig",
                table: "catProveedoresConfigs",
                columns: new[] { "IdCatProveedor", "IdCatTipoConfig" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresConfigs_IdCatTipoConfig",
                table: "catProveedoresConfigs",
                column: "IdCatTipoConfig");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresConfigs_IdUsuarioRegistro",
                table: "catProveedoresConfigs",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresContactos_catTipoContactoIdCatTipoContacto",
                table: "catProveedoresContactos",
                column: "catTipoContactoIdCatTipoContacto");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresContactos_IdCatProveedor_IdCatTipoContacto",
                table: "catProveedoresContactos",
                columns: new[] { "IdCatProveedor", "IdCatTipoContacto" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresPatios_IdCatAduana",
                table: "catProveedoresPatios",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresPatios_IdCatPatio",
                table: "catProveedoresPatios",
                column: "IdCatPatio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresPatios_IdCatProveedor",
                table: "catProveedoresPatios",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresPatios_IdUsuarioRegistro",
                table: "catProveedoresPatios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifaPatios_IdCatPatio",
                table: "catProveedoresTarifaPatios",
                column: "IdCatPatio");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifaPatios_IdCatProvTarifas_IdCatPatio",
                table: "catProveedoresTarifaPatios",
                columns: new[] { "IdCatProvTarifas", "IdCatPatio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifaPatios_IdUsuarioRegistro",
                table: "catProveedoresTarifaPatios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifas_IdCatAduana",
                table: "catProveedoresTarifas",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifas_IdCatProveedores",
                table: "catProveedoresTarifas",
                column: "IdCatProveedores");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifas_IdCatProvTarifas_IdCatServicio_IdCatAduana",
                table: "catProveedoresTarifas",
                columns: new[] { "IdCatProvTarifas", "IdCatServicio", "IdCatAduana" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifas_IdCatServicio",
                table: "catProveedoresTarifas",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_catProveedoresTarifas_IdUsuarioRegistro",
                table: "catProveedoresTarifas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catProyectos_Acronimo",
                table: "catProyectos",
                column: "Acronimo",
                unique: true,
                filter: "[Acronimo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catProyectos_IdCatEmpresas",
                table: "catProyectos",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_catProyectos_IdCatLineaNegocio",
                table: "catProyectos",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_catProyectos_IdUsuarioRegistro",
                table: "catProyectos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catProyectos_Nombre",
                table: "catProyectos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catRecintos_CatAduanaIdCatAduana",
                table: "catRecintos",
                column: "CatAduanaIdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catRecintos_ClaveRecinto",
                table: "catRecintos",
                column: "ClaveRecinto",
                unique: true,
                filter: "[ClaveRecinto] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catRecintos_IdCatPais",
                table: "catRecintos",
                column: "IdCatPais");

            migrationBuilder.CreateIndex(
                name: "IX_catRecintos_IdCatPaisEstados",
                table: "catRecintos",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_catRecintos_IdCatUsuarioRegistro",
                table: "catRecintos",
                column: "IdCatUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catRecintos_Nombre",
                table: "catRecintos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catReferenciaEstado_IdUsuarioRegistro",
                table: "catReferenciaEstado",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catReferenciaEstado_Nombre",
                table: "catReferenciaEstado",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catRoles_IdUsuarioRegistro",
                table: "catRoles",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catRoles_Nombre",
                table: "catRoles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catRolesPermisos_IdCatPermisos",
                table: "catRolesPermisos",
                column: "IdCatPermisos");

            migrationBuilder.CreateIndex(
                name: "IX_catRolesPermisos_IdCatRoles_IdCatPermisos",
                table: "catRolesPermisos",
                columns: new[] { "IdCatRoles", "IdCatPermisos" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catSeriesFacturas_catSucursalesIdCatSucursal",
                table: "catSeriesFacturas",
                column: "catSucursalesIdCatSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_catSeriesFacturas_IdCatAduana",
                table: "catSeriesFacturas",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catSeriesFacturas_IdCatEmpresa",
                table: "catSeriesFacturas",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_catSeriesFacturas_IdCatLineaNegocio",
                table: "catSeriesFacturas",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_catServicios_IdCatEmpresas",
                table: "catServicios",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_catServicios_IdUsuarioRegistro",
                table: "catServicios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catServicios_Nombre",
                table: "catServicios",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catSistemas_IdCatCliente",
                table: "catSistemas",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_catSistemas_IdCatEmpresas",
                table: "catSistemas",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_catSistemas_nombre",
                table: "catSistemas",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catSistemas_userSistema",
                table: "catSistemas",
                column: "userSistema",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catSucursales_IdCatEmpresas",
                table: "catSucursales",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_catSucursales_IdUsuarioRegistro",
                table: "catSucursales",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catSucursales_Nombre",
                table: "catSucursales",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catSucursales_RFC_IdCatEmpresas",
                table: "catSucursales",
                columns: new[] { "RFC", "IdCatEmpresas" },
                unique: true,
                filter: "[RFC] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catTipoClasificacion_Nombre",
                table: "catTipoClasificacion",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catTipoContactos_Nombre",
                table: "catTipoContactos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catTipoEstados_Nombre",
                table: "catTipoEstados",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catTipoEstados_TipoEstado",
                table: "catTipoEstados",
                column: "TipoEstado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catTipoIncidenciaEvento_IdCatTipoEventoCron",
                table: "catTipoIncidenciaEvento",
                column: "IdCatTipoEventoCron");

            migrationBuilder.CreateIndex(
                name: "IX_catTipoIncidenciaEvento_IdCatTipoIncidenciaCron",
                table: "catTipoIncidenciaEvento",
                column: "IdCatTipoIncidenciaCron");

            migrationBuilder.CreateIndex(
                name: "IX_CatTipoMoneda_IdUsuarioRegistro",
                table: "CatTipoMoneda",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catTiposConfig_Clave",
                table: "catTiposConfig",
                column: "Clave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catTiposConfig_Nombre",
                table: "catTiposConfig",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catTiposContenedor_IdUsuarioRegistro",
                table: "catTiposContenedor",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catTransportistas_IdCatEmpresas",
                table: "catTransportistas",
                column: "IdCatEmpresas");

            migrationBuilder.CreateIndex(
                name: "IX_catTransportistas_IdCatPaises",
                table: "catTransportistas",
                column: "IdCatPaises");

            migrationBuilder.CreateIndex(
                name: "IX_catTransportistas_IdCatPaisEstados",
                table: "catTransportistas",
                column: "IdCatPaisEstados");

            migrationBuilder.CreateIndex(
                name: "IX_catTransportistas_IdUsuarioRegistro",
                table: "catTransportistas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catTransportistas_RazonSocial_IdCatEmpresas",
                table: "catTransportistas",
                columns: new[] { "RazonSocial", "IdCatEmpresas" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catTransportistas_RFC_IdCatEmpresas",
                table: "catTransportistas",
                columns: new[] { "RFC", "IdCatEmpresas" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catUsuarioRoles_IdCatRoles",
                table: "catUsuarioRoles",
                column: "IdCatRoles");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuarioRoles_IdCatUsuarios_IdCatRoles",
                table: "catUsuarioRoles",
                columns: new[] { "IdCatUsuarios", "IdCatRoles" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catUsuarios_IdCatTipoPuesto",
                table: "catUsuarios",
                column: "IdCatTipoPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuarios_Nombre_ApellidoPaterno_ApellidoMaterno",
                table: "catUsuarios",
                columns: new[] { "Nombre", "ApellidoPaterno", "ApellidoMaterno" },
                unique: true,
                filter: "[ApellidoMaterno] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuarios_RFC",
                table: "catUsuarios",
                column: "RFC",
                unique: true,
                filter: "[RFC] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuarios_Usuario",
                table: "catUsuarios",
                column: "Usuario",
                unique: true,
                filter: "[Usuario] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosAduanas_IdCatAduana",
                table: "catUsuariosAduanas",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosAduanas_IdCatUsuario_IdCatAduana",
                table: "catUsuariosAduanas",
                columns: new[] { "IdCatUsuario", "IdCatAduana" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosClientes_IdCatCliente",
                table: "catUsuariosClientes",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosClientes_IdCatUsuario",
                table: "catUsuariosClientes",
                column: "IdCatUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosEmpresa_IdCatCliente_IdCatUsuarios",
                table: "catUsuariosEmpresa",
                columns: new[] { "IdCatCliente", "IdCatUsuarios" },
                unique: true,
                filter: "[IdCatCliente] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosEmpresa_idCatEmpresa",
                table: "catUsuariosEmpresa",
                column: "idCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosEmpresa_IdCatProveedor",
                table: "catUsuariosEmpresa",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosEmpresa_IdCatUsuarios",
                table: "catUsuariosEmpresa",
                column: "IdCatUsuarios");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosPermisos_IdCatPermisos_IdCatUsuarios",
                table: "catUsuariosPermisos",
                columns: new[] { "IdCatPermisos", "IdCatUsuarios" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosPermisos_IdCatUsuarios",
                table: "catUsuariosPermisos",
                column: "IdCatUsuarios");

            migrationBuilder.CreateIndex(
                name: "IX_dtAcarreos_IdCatProveedor",
                table: "dtAcarreos",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_dtAcarreos_IdCatServicio",
                table: "dtAcarreos",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_dtAcarreos_IdCatTipoEstado",
                table: "dtAcarreos",
                column: "IdCatTipoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_dtAcarreos_IdCliente",
                table: "dtAcarreos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_dtAcarreos_IdEmpresa",
                table: "dtAcarreos",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_dtAcarreos_IdOrden",
                table: "dtAcarreos",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_dtAcarreos_IdUsuarioRegistro",
                table: "dtAcarreos",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaDet_IdUltimaMilla",
                table: "dtUltimaMillaDet",
                column: "IdUltimaMilla");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdCatEmpresa",
                table: "dtUltimaMillaEnc",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdCatProveedor",
                table: "dtUltimaMillaEnc",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdCatServicio",
                table: "dtUltimaMillaEnc",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdCatTipoTransporte",
                table: "dtUltimaMillaEnc",
                column: "IdCatTipoTransporte");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdCliente",
                table: "dtUltimaMillaEnc",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdOrden",
                table: "dtUltimaMillaEnc",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdTipoEstado",
                table: "dtUltimaMillaEnc",
                column: "IdTipoEstado");

            migrationBuilder.CreateIndex(
                name: "IX_integracionAnticipoSol_IdOrden_IdPeticionesReferencia_IdPeticionesContenedor",
                table: "integracionAnticipoSol",
                columns: new[] { "IdOrden", "IdPeticionesReferencia", "IdPeticionesContenedor" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_integracionAnticipoSol_IdPeticionesContenedor",
                table: "integracionAnticipoSol",
                column: "IdPeticionesContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_integracionAnticipoSol_IdPeticionesReferencia",
                table: "integracionAnticipoSol",
                column: "IdPeticionesReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_IddIntFacturaEnc",
                table: "integracionFacturaDet",
                column: "IddIntFacturaEnc");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_IdPeticionesContenedor",
                table: "integracionFacturaDet",
                column: "IdPeticionesContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaDet_IdPeticionesReferencia_IdPeticionesContenedor_IdCatServicio",
                table: "integracionFacturaDet",
                columns: new[] { "IdPeticionesReferencia", "IdPeticionesContenedor", "IdCatServicio" },
                unique: true);

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
                table: "integracionFacturaEnc",
                column: "IdDtAcarreos");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdDtUltimaMillaEnc",
                table: "integracionFacturaEnc",
                column: "IdDtUltimaMillaEnc");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdOrden_IdPeticionesReferencia",
                table: "integracionFacturaEnc",
                columns: new[] { "IdOrden", "IdPeticionesReferencia" },
                unique: true,
                filter: "[IdPeticionesReferencia] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_integracionFacturaEnc_IdPeticionesReferencia",
                table: "integracionFacturaEnc",
                column: "IdPeticionesReferencia");

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
                name: "IX_integracionFacturaEst_IdIntFacturaEnc",
                table: "integracionFacturaEst",
                column: "IdIntFacturaEnc");

            migrationBuilder.CreateIndex(
                name: "IX_integracionReferencia_IdOrden",
                table: "integracionReferencia",
                column: "IdOrden",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_integracionReferencia_IdOrden",
                schema: "SLO",
                table: "integracionReferencia",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatAduana",
                table: "ordenes",
                column: "IdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatCliente",
                table: "ordenes",
                column: "IdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatEmpresa",
                table: "ordenes",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatLineaNegocio",
                table: "ordenes",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatProveedor",
                table: "ordenes",
                column: "IdCatProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatProyecto",
                table: "ordenes",
                column: "IdCatProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatSistema",
                table: "ordenes",
                column: "IdCatSistema");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdCatSucursal",
                table: "ordenes",
                column: "IdCatSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_ordenes_IdUsuario",
                table: "ordenes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteServicio_ServiciosId",
                schema: "WMS",
                table: "PaqueteServicio",
                column: "ServiciosId");

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
                table: "peticionesContenedores",
                column: "AduanaId");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_ClienteId",
                table: "peticionesContenedores",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_IdCatTipoContenedor",
                table: "peticionesContenedores",
                column: "IdCatTipoContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_IdClienteFacturarA",
                table: "peticionesContenedores",
                column: "IdClienteFacturarA");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_IdEstadoContenedor",
                table: "peticionesContenedores",
                column: "IdEstadoContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_IdReferencia",
                table: "peticionesContenedores",
                column: "IdReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_Naviera_Id",
                table: "peticionesContenedores",
                column: "Naviera_Id");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_PatioId",
                table: "peticionesContenedores",
                column: "PatioId");

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
                name: "IX_peticionesDocumentos_IdContenedor",
                table: "peticionesDocumentos",
                column: "IdContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesDocumentos_IdServicio",
                table: "peticionesDocumentos",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesDocumentos_IdTipoDocumento",
                table: "peticionesDocumentos",
                column: "IdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesReferencias_IdCatReferenciaEstado",
                table: "peticionesReferencias",
                column: "IdCatReferenciaEstado");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesReferencias_IdOrden",
                table: "peticionesReferencias",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesReferencias_Ticket",
                table: "peticionesReferencias",
                column: "Ticket",
                unique: true,
                filter: "[Ticket] IS NOT NULL");

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
                name: "IX_peticionesServicios_IdCatServicio",
                table: "peticionesServicios",
                column: "IdCatServicio");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdClienteFacturarA",
                table: "peticionesServicios",
                column: "IdClienteFacturarA");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdContenedor_IdCatServicio",
                table: "peticionesServicios",
                columns: new[] { "IdContenedor", "IdCatServicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdEstadoServicio",
                table: "peticionesServicios",
                column: "IdEstadoServicio");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesServicios_IdServicio_IdContenedor_IdCatServicio",
                table: "peticionesServicios",
                columns: new[] { "IdServicio", "IdContenedor", "IdCatServicio" },
                unique: true);

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
                name: "IX_prefacturaDet_IdAduana",
                table: "prefacturaDet",
                column: "IdAduana");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaDet_IdLineaNegocio",
                table: "prefacturaDet",
                column: "IdLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaDet_IdPrefacturaEnc",
                table: "prefacturaDet",
                column: "IdPrefacturaEnc");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaDet_IdProyectos",
                table: "prefacturaDet",
                column: "IdProyectos");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaDet_IdServicio",
                table: "prefacturaDet",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaDet_OrdenesIdOrden",
                table: "prefacturaDet",
                column: "OrdenesIdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaEnc_IdAduana",
                table: "prefacturaEnc",
                column: "IdAduana");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaEnc_IdCatLineaNegocio",
                table: "prefacturaEnc",
                column: "IdCatLineaNegocio");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaEnc_IdCliente",
                table: "prefacturaEnc",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaEnc_IdOrden",
                table: "prefacturaEnc",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_prefacturaEnc_IdUsuario",
                table: "prefacturaEnc",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_provisionesDet_IdCatUsuarios",
                table: "provisionesDet",
                column: "IdCatUsuarios");

            migrationBuilder.CreateIndex(
                name: "IX_provisionesDet_IdContenedor",
                table: "provisionesDet",
                column: "IdContenedor");

            migrationBuilder.CreateIndex(
                name: "IX_provisionesDet_IdProvisionesEnc",
                table: "provisionesDet",
                column: "IdProvisionesEnc");

            migrationBuilder.CreateIndex(
                name: "IX_provisionesDet_IdReferencia",
                table: "provisionesDet",
                column: "IdReferencia");

            migrationBuilder.CreateIndex(
                name: "IX_provisionesEnc_IdCatTipoMoneda",
                table: "provisionesEnc",
                column: "IdCatTipoMoneda");

            migrationBuilder.CreateIndex(
                name: "IX_provisionesEnc_IdCatUsuarios",
                table: "provisionesEnc",
                column: "IdCatUsuarios");

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

            migrationBuilder.CreateIndex(
                name: "IX_WMS_034_REFERENCIA_BOOKING_BL_nIdReferencia001",
                schema: "WMS",
                table: "WMS_034_REFERENCIA_BOOKING_BL",
                column: "nIdReferencia001");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_035_ORDEN_SALIDA_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_035_ORDEN_SALIDA",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_036_LIBERACION_nIdAgenteAduanal033",
                schema: "WMS",
                table: "WMS_036_LIBERACION",
                column: "nIdAgenteAduanal033");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_036_LIBERACION_nIdCatCliente",
                schema: "WMS",
                table: "WMS_036_LIBERACION",
                column: "nIdCatCliente");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_036_LIBERACION_nIdManiobristaDestino023",
                schema: "WMS",
                table: "WMS_036_LIBERACION",
                column: "nIdManiobristaDestino023");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_036_LIBERACION_nIdOrdenSalida035",
                schema: "WMS",
                table: "WMS_036_LIBERACION",
                column: "nIdOrdenSalida035");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_036_LIBERACION_nIdReferencia001",
                schema: "WMS",
                table: "WMS_036_LIBERACION",
                column: "nIdReferencia001");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_037_ORDEN_SALIDA_INVENTARIO_nIdInventario014",
                schema: "WMS",
                table: "WMS_037_ORDEN_SALIDA_INVENTARIO",
                column: "nIdInventario014");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_037_ORDEN_SALIDA_INVENTARIO_nIdOrdenSalida035",
                schema: "WMS",
                table: "WMS_037_ORDEN_SALIDA_INVENTARIO",
                column: "nIdOrdenSalida035");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_038_LIBERACION_INVENTARIO_nIdTarja015",
                schema: "WMS",
                table: "WMS_038_LIBERACION_INVENTARIO",
                column: "nIdTarja015");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_039_SALIDA_nIdCatEmpresa",
                schema: "WMS",
                table: "WMS_039_SALIDA",
                column: "nIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_039_SALIDA_nIdOrdenSalida035",
                schema: "WMS",
                table: "WMS_039_SALIDA",
                column: "nIdOrdenSalida035");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_040_SALIDA_CONTROL_TRANSPORTE_nIdCatUsuarioCancelacion",
                schema: "WMS",
                table: "WMS_040_SALIDA_CONTROL_TRANSPORTE",
                column: "nIdCatUsuarioCancelacion");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_040_SALIDA_CONTROL_TRANSPORTE_nIdControlTransporte024",
                schema: "WMS",
                table: "WMS_040_SALIDA_CONTROL_TRANSPORTE",
                column: "nIdControlTransporte024");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_040_SALIDA_CONTROL_TRANSPORTE_nIdOrdenSalida035",
                schema: "WMS",
                table: "WMS_040_SALIDA_CONTROL_TRANSPORTE",
                column: "nIdOrdenSalida035");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_040_SALIDA_CONTROL_TRANSPORTE_nIdSalida039",
                schema: "WMS",
                table: "WMS_040_SALIDA_CONTROL_TRANSPORTE",
                column: "nIdSalida039");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anticiposDet");

            migrationBuilder.DropTable(
                name: "catAgentesAduanales");

            migrationBuilder.DropTable(
                name: "catClientesClasificacion");

            migrationBuilder.DropTable(
                name: "catClientesConfig");

            migrationBuilder.DropTable(
                name: "catClientesContactos");

            migrationBuilder.DropTable(
                name: "catClientesExternos");

            migrationBuilder.DropTable(
                name: "catClientesLNegocio");

            migrationBuilder.DropTable(
                name: "catClientesProyectos");

            migrationBuilder.DropTable(
                name: "catClienteTarifa");

            migrationBuilder.DropTable(
                name: "catFormatoRefDets");

            migrationBuilder.DropTable(
                name: "catLineaNegocioTariPrecios");

            migrationBuilder.DropTable(
                name: "catPaisCP");

            migrationBuilder.DropTable(
                name: "catPaisMunicipios");

            migrationBuilder.DropTable(
                name: "catPatiosConfig");

            migrationBuilder.DropTable(
                name: "catPatiosNavieras");

            migrationBuilder.DropTable(
                name: "catProveedoresClasif");

            migrationBuilder.DropTable(
                name: "catProveedoresConfigs");

            migrationBuilder.DropTable(
                name: "catProveedoresContactos");

            migrationBuilder.DropTable(
                name: "catProveedoresPatios");

            migrationBuilder.DropTable(
                name: "catProveedoresTarifaPatios");

            migrationBuilder.DropTable(
                name: "catRecintos");

            migrationBuilder.DropTable(
                name: "catRolesPermisos");

            migrationBuilder.DropTable(
                name: "catSeriesFacturas");

            migrationBuilder.DropTable(
                name: "catUsuarioRoles");

            migrationBuilder.DropTable(
                name: "catUsuariosAduanas");

            migrationBuilder.DropTable(
                name: "catUsuariosClientes");

            migrationBuilder.DropTable(
                name: "catUsuariosEmpresa");

            migrationBuilder.DropTable(
                name: "catUsuariosPermisos");

            migrationBuilder.DropTable(
                name: "dtUltimaMillaDet");

            migrationBuilder.DropTable(
                name: "integracionAnticipoSol");

            migrationBuilder.DropTable(
                name: "integracionConceptosFactura");

            migrationBuilder.DropTable(
                name: "integracionFacturaDet");

            migrationBuilder.DropTable(
                name: "integracionFacturaDet",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "integracionFacturaEst");

            migrationBuilder.DropTable(
                name: "PaqueteServicio",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "peticionesContenedorCron",
                schema: "vacios");

            migrationBuilder.DropTable(
                name: "peticionesDocumentos");

            migrationBuilder.DropTable(
                name: "prefacturaDet");

            migrationBuilder.DropTable(
                name: "provisionesDet");

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
                name: "WMS_034_REFERENCIA_BOOKING_BL",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_036_LIBERACION",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_037_ORDEN_SALIDA_INVENTARIO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_038_LIBERACION_INVENTARIO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_040_SALIDA_CONTROL_TRANSPORTE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "anticiposEnc");

            migrationBuilder.DropTable(
                name: "catClientesServicioAduana");

            migrationBuilder.DropTable(
                name: "catContenedor");

            migrationBuilder.DropTable(
                name: "catFormatoReferencias");

            migrationBuilder.DropTable(
                name: "catLineaNegocioTarifas");

            migrationBuilder.DropTable(
                name: "catTipoClasificacion");

            migrationBuilder.DropTable(
                name: "catTiposConfig");

            migrationBuilder.DropTable(
                name: "catTipoContactos");

            migrationBuilder.DropTable(
                name: "catProveedoresTarifas");

            migrationBuilder.DropTable(
                name: "catRoles");

            migrationBuilder.DropTable(
                name: "catPermisos");

            migrationBuilder.DropTable(
                name: "integracionReferencia");

            migrationBuilder.DropTable(
                name: "integracionFacturaEnc",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "peticionesServicios",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "integracionFacturaEnc");

            migrationBuilder.DropTable(
                name: "WMS_004_SERVICIO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "catTipoIncidenciaEvento");

            migrationBuilder.DropTable(
                name: "catDocumento");

            migrationBuilder.DropTable(
                name: "peticionesServicios");

            migrationBuilder.DropTable(
                name: "prefacturaEnc");

            migrationBuilder.DropTable(
                name: "provisionesEnc");

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
                name: "WMS_024_CONTROL_TRANSPORTE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_039_SALIDA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "integracionReferencia",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "peticionesContenedores",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "dtAcarreos");

            migrationBuilder.DropTable(
                name: "dtUltimaMillaEnc");

            migrationBuilder.DropTable(
                name: "catTipoEventosCron");

            migrationBuilder.DropTable(
                name: "catTipoIncidenciasCron");

            migrationBuilder.DropTable(
                name: "peticionesContenedores");

            migrationBuilder.DropTable(
                name: "CatTipoMoneda");

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
                name: "WMS_020_TIPO_TRANSPORTE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_021_LINEA_TRANS_OPERADOR",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_022_LINEA_TRANS_TRANSPORTE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_035_ORDEN_SALIDA",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "peticionesReferencias",
                schema: "SLO");

            migrationBuilder.DropTable(
                name: "catServicios");

            migrationBuilder.DropTable(
                name: "catTipoEstados");

            migrationBuilder.DropTable(
                name: "catTipoTransporte");

            migrationBuilder.DropTable(
                name: "catPatios");

            migrationBuilder.DropTable(
                name: "catTiposContenedor");

            migrationBuilder.DropTable(
                name: "peticionesReferencias");

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
                name: "catTransportistas");

            migrationBuilder.DropTable(
                name: "catReferenciaEstado");

            migrationBuilder.DropTable(
                name: "WMS_010_ZONA_ALMACENAJE",
                schema: "WMS");

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
                name: "ordenes");

            migrationBuilder.DropTable(
                name: "WMS_008_ALMACEN",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_009_TIPO_ZONA_ALMACENAJE",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "WMS_003_BARCO",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "catAduana");

            migrationBuilder.DropTable(
                name: "catProveedores");

            migrationBuilder.DropTable(
                name: "catProyectos");

            migrationBuilder.DropTable(
                name: "catSistemas");

            migrationBuilder.DropTable(
                name: "catSucursales");

            migrationBuilder.DropTable(
                name: "WMS_007_TIPO_CARGA_ALMACEN",
                schema: "WMS");

            migrationBuilder.DropTable(
                name: "catNavieras");

            migrationBuilder.DropTable(
                name: "catLineaNegocio");

            migrationBuilder.DropTable(
                name: "catClientes");

            migrationBuilder.DropTable(
                name: "catEmpresas");

            migrationBuilder.DropTable(
                name: "catPaisEstados");

            migrationBuilder.DropTable(
                name: "catUsuarios");

            migrationBuilder.DropTable(
                name: "catPaises");

            migrationBuilder.DropTable(
                name: "catTipoPuesto");
        }
    }
}
