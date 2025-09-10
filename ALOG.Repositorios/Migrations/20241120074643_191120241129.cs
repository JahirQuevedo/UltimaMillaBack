using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALOG.Repositorios.Migrations
{
    /// <inheritdoc />
    public partial class _191120241129 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        onDelete: ReferentialAction.NoAction);
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
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CURP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    passSistema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    salt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_anticiposEnc_catUsuarios_IdUsuarioSolicita",
                        column: x => x.IdUsuarioSolicita,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catAduana_catPaises_IdCatPais",
                        column: x => x.IdCatPais,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catAduana_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                    UsoCFDISAT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catClientes", x => x.IdCatCliente);
                    table.ForeignKey(
                        name: "FK_catClientes_catPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientes_catPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientes_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedores_catPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedores_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catRecintos_catPaises_IdCatPais",
                        column: x => x.IdCatPais,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catRecintos_catUsuarios_IdCatUsuarioRegistro",
                        column: x => x.IdCatUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catUsuariosAduanas_catUsuarios_IdCatUsuario",
                        column: x => x.IdCatUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesClasificacion_catTipoClasificacion_IdCatClasificacion",
                        column: x => x.IdCatClasificacion,
                        principalTable: "catTipoClasificacion",
                        principalColumn: "IdCatTipoClasificacion",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesConfig_catTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "catTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesConfig_catUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesExternos_catClientes_IdCatClientesAsociado",
                        column: x => x.IdCatClientesAsociado,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesExternos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catServicios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "catSistemas",
                columns: table => new
                {
                    IdCatSistema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false),
                    userSistema = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    passSistema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    salt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdCatCliente = table.Column<int>(type: "int", nullable: false),
                    IdCatEmpresas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catSistemas", x => x.IdCatSistema);
                    table.ForeignKey(
                        name: "FK_catSistemas_catClientes_IdCatCliente",
                        column: x => x.IdCatCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catSistemas_catEmpresas_IdCatEmpresas",
                        column: x => x.IdCatEmpresas,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catSucursales_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catTransportistas_catPaisEstados_IdCatPaisEstados",
                        column: x => x.IdCatPaisEstados,
                        principalTable: "catPaisEstados",
                        principalColumn: "IdCatPaisEstados",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catTransportistas_catPaises_IdCatPaises",
                        column: x => x.IdCatPaises,
                        principalTable: "catPaises",
                        principalColumn: "IdCatPaises",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catTransportistas_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                    catEmpresasIdCatEmpresa = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        name: "FK_catUsuariosEmpresa_catEmpresas_catEmpresasIdCatEmpresa",
                        column: x => x.catEmpresasIdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa");
                    table.ForeignKey(
                        name: "FK_catUsuariosEmpresa_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesLNegocio_catLineaNegocio_IdLineaNegocio",
                        column: x => x.IdLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesLNegocio_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catFormatoReferencias_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProyectos_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProyectos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catUsuariosPermisos_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "catPatios",
                columns: table => new
                {
                    IdCatPatios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdAduana = table.Column<int>(type: "int", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catPatios", x => x.IdCatPatios);
                    table.ForeignKey(
                        name: "FK_catPatios_catAduana_IdAduana",
                        column: x => x.IdAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.NoAction);
                    //table.ForeignKey(
                    //    name: "FK_catPatios_catProveedores_IdProveedor",
                    //    column: x => x.IdProveedor,
                    //    principalTable: "catProveedores",
                    //    principalColumn: "IdCatProveedor",
                    //    onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catPatios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresClasif_catTipoClasificacion_IdCatTipoClasificacion",
                        column: x => x.IdCatTipoClasificacion,
                        principalTable: "catTipoClasificacion",
                        principalColumn: "IdCatTipoClasificacion",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresConfigs_catTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "catTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresConfigs_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresContactos_catTipoContactos_catTipoContactoIdCatTipoContacto",
                        column: x => x.catTipoContactoIdCatTipoContacto,
                        principalTable: "catTipoContactos",
                        principalColumn: "IdCatTipoContacto");
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catRolesPermisos_catRoles_IdCatRoles",
                        column: x => x.IdCatRoles,
                        principalTable: "catRoles",
                        principalColumn: "IdCatRoles",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catUsuarioRoles_catUsuarios_IdCatUsuarios",
                        column: x => x.IdCatUsuarios,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesServicioAduana_catClientes_IdCatClientes",
                        column: x => x.IdCatClientes,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesServicioAduana_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesServicioAduana_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTarifas_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTarifas_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifas_catProveedores_IdCatProveedores",
                        column: x => x.IdCatProveedores,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifas_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifas_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesProyectos_catProyectos_IdCatProyecto",
                        column: x => x.IdCatProyecto,
                        principalTable: "catProyectos",
                        principalColumn: "IdProyectos",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catClientesProyectos_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ordenes",
                columns: table => new
                {
                    IdOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatCliente = table.Column<int>(type: "int", nullable: false),
                    IdCatSistema = table.Column<int>(type: "int", nullable: false),
                    IdCatAduana = table.Column<int>(type: "int", nullable: true),
                    IdCatEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdCatSucursal = table.Column<int>(type: "int", nullable: false),
                    IdCatLineaNegocio = table.Column<int>(type: "int", nullable: false),
                    IdCatProyecto = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ordenes_catEmpresas_IdCatEmpresa",
                        column: x => x.IdCatEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ordenes_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ordenes_catProyectos_IdCatProyecto",
                        column: x => x.IdCatProyecto,
                        principalTable: "catProyectos",
                        principalColumn: "IdProyectos");
                    table.ForeignKey(
                        name: "FK_ordenes_catSistemas_IdCatSistema",
                        column: x => x.IdCatSistema,
                        principalTable: "catSistemas",
                        principalColumn: "IdCatSistema",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ordenes_catSucursales_IdCatSucursal",
                        column: x => x.IdCatSucursal,
                        principalTable: "catSucursales",
                        principalColumn: "IdCatSucursal",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ordenes_catUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios");
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catPatiosConfig_catTiposConfig_IdCatTipoConfig",
                        column: x => x.IdCatTipoConfig,
                        principalTable: "catTiposConfig",
                        principalColumn: "IdCatTiposConfig",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catPatiosConfig_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catPatiosNavieras_catPatios_IdCatPatios",
                        column: x => x.IdCatPatios,
                        principalTable: "catPatios",
                        principalColumn: "IdCatPatios",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catPatiosNavieras_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                    IdCatAduana = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catLineaNegocioTariPrecios", x => x.IdCatLineNegocioTariPrecio);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTariPrecios_catAduana_IdCatAduana",
                        column: x => x.IdCatAduana,
                        principalTable: "catAduana",
                        principalColumn: "IdCatAduana",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTariPrecios_catLineaNegocioTarifas_IdCatLineaNegocioTarifa",
                        column: x => x.IdCatLineaNegocioTarifa,
                        principalTable: "catLineaNegocioTarifas",
                        principalColumn: "IdCatLineaNegocioTarifa",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catLineaNegocioTariPrecios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifaPatios_catProveedoresTarifas_IdCatProvTarifas",
                        column: x => x.IdCatProvTarifas,
                        principalTable: "catProveedoresTarifas",
                        principalColumn: "IdCatProvTarifas",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_catProveedoresTarifaPatios_catUsuarios_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_anticiposDet_catLineaNegocio_IdLineaNegocio",
                        column: x => x.IdLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_anticiposDet_catProveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "catProveedores",
                        principalColumn: "IdCatProveedor",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_anticiposDet_catServicios_IdCatServicios",
                        column: x => x.IdCatServicios,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_anticiposDet_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.NoAction);
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
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdOrden = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdCatTipoEstado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dtAcarreos", x => x.IdDtAcarreos);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente");
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catEmpresas_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "catEmpresas",
                        principalColumn: "IdCatEmpresa",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_dtAcarreos_catTipoEstados_IdCatTipoEstado",
                        column: x => x.IdCatTipoEstado,
                        principalTable: "catTipoEstados",
                        principalColumn: "IdCatTipoEstados");
                    table.ForeignKey(
                        name: "FK_dtAcarreos_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden");
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
                    IdCatServicio = table.Column<int>(type: "int", nullable: false)
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_catTipoEstados_IdTipoEstado",
                        column: x => x.IdTipoEstado,
                        principalTable: "catTipoEstados",
                        principalColumn: "IdCatTipoEstados",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaEnc_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.NoAction);
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
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "peticionesReferencias",
                columns: table => new
                {
                    IdReferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket = table.Column<int>(type: "int", nullable: false),
                    Transporte_RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transporte_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrasporteId = table.Column<int>(type: "int", nullable: false),
                    Transporte_Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transporte_UsuarioEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoReferencia = table.Column<int>(type: "int", nullable: false),
                    Procesado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaProcesado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCatReferenciaEstado = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "IdCatReferenciaEstado",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_peticionesReferencias_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_catClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "catClientes",
                        principalColumn: "IdCatCliente",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_catLineaNegocio_IdCatLineaNegocio",
                        column: x => x.IdCatLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_catUsuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "catUsuarios",
                        principalColumn: "IdCatUsuarios",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_prefacturaEnc_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.NoAction);
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
                    IdUltimaMilla = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dtUltimaMillaDet", x => x.IdDtUltimaMillaDet);
                    table.ForeignKey(
                        name: "FK_dtUltimaMillaDet_dtUltimaMillaEnc_IdUltimaMilla",
                        column: x => x.IdUltimaMilla,
                        principalTable: "dtUltimaMillaEnc",
                        principalColumn: "IdDtUltMillaEnc",
                        onDelete: ReferentialAction.NoAction);
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
                    RespuestaWS1G = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        onDelete: ReferentialAction.NoAction);
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
                    Contenedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefenciaCliente = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaveTipoContenedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatioId = table.Column<int>(type: "int", nullable: true),
                    Patio_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patio_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaTocaPiso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Cliente_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cliente_RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cliente_Solicitante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsignadoId = table.Column<int>(type: "int", nullable: false),
                    Consignado_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consignado_RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FondoFinanciamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MontoSolicitud = table.Column<double>(type: "float", nullable: false),
                    MontoTotal = table.Column<double>(type: "float", nullable: false),
                    AduanaId = table.Column<int>(type: "int", nullable: true),
                    catAduanaIdCatAduana = table.Column<int>(type: "int", nullable: true),
                    FechaSolDevolucion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPagoGarantiaNav = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Naviera_RFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naviera_Id = table.Column<int>(type: "int", nullable: true),
                    Naviera_RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aduana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdReferencia = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        name: "FK_peticionesContenedores_catAduana_catAduanaIdCatAduana",
                        column: x => x.catAduanaIdCatAduana,
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
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_catLineaNegocio_IdLineaNegocio",
                        column: x => x.IdLineaNegocio,
                        principalTable: "catLineaNegocio",
                        principalColumn: "IdCatLineaNegocio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_catProyectos_IdProyectos",
                        column: x => x.IdProyectos,
                        principalTable: "catProyectos",
                        principalColumn: "IdProyectos",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_prefacturaDet_catServicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                        name: "FK_integracionAnticipoSol_ordenes_IdOrden",
                        column: x => x.IdOrden,
                        principalTable: "ordenes",
                        principalColumn: "IdOrden",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_integracionAnticipoSol_peticionesContenedores_IdPeticionesContenedor",
                        column: x => x.IdPeticionesContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_integracionAnticipoSol_peticionesReferencias_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.NoAction);
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
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integracionFacturaDet", x => x.IdIntFacturaDet);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_integracionFacturaEnc_IddIntFacturaEnc",
                        column: x => x.IddIntFacturaEnc,
                        principalTable: "integracionFacturaEnc",
                        principalColumn: "IdIntFacturaEnc",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_peticionesContenedores_IdPeticionesContenedor",
                        column: x => x.IdPeticionesContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_integracionFacturaDet_peticionesReferencias_IdPeticionesReferencia",
                        column: x => x.IdPeticionesReferencia,
                        principalTable: "peticionesReferencias",
                        principalColumn: "IdReferencia",
                        onDelete: ReferentialAction.NoAction);
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
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdContenedor = table.Column<int>(type: "int", nullable: false),
                    EstadoServicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEstadoServicio = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_peticionesServicios_catServicios_IdCatServicio",
                        column: x => x.IdCatServicio,
                        principalTable: "catServicios",
                        principalColumn: "IdCatServicio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_peticionesServicios_peticionesContenedores_IdContenedor",
                        column: x => x.IdContenedor,
                        principalTable: "peticionesContenedores",
                        principalColumn: "IdContenedor",
                        onDelete: ReferentialAction.NoAction);
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
                    CatDocumentoIdCatDocumento = table.Column<int>(type: "int", nullable: true),
                    IdServicio = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peticionesDocumentos", x => x.IdDocumento);
                    table.ForeignKey(
                        name: "FK_peticionesDocumentos_catDocumento_CatDocumentoIdCatDocumento",
                        column: x => x.CatDocumentoIdCatDocumento,
                        principalTable: "catDocumento",
                        principalColumn: "IdCatDocumento");
                    table.ForeignKey(
                        name: "FK_peticionesDocumentos_peticionesServicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "peticionesServicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.NoAction);
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
                name: "IX_catPatios_IdAduana",
                table: "catPatios",
                column: "IdAduana");

            migrationBuilder.CreateIndex(
                name: "IX_catPatios_IdProveedor",
                table: "catPatios",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_catPatios_IdUsuarioRegistro",
                table: "catPatios",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_catPatios_RazonSocial_IdAduana",
                table: "catPatios",
                columns: new[] { "RazonSocial", "IdAduana" },
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
                unique: true);

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
                name: "IX_catUsuariosEmpresa_catEmpresasIdCatEmpresa",
                table: "catUsuariosEmpresa",
                column: "catEmpresasIdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_catUsuariosEmpresa_IdCatCliente_IdCatUsuarios",
                table: "catUsuariosEmpresa",
                columns: new[] { "IdCatCliente", "IdCatUsuarios" },
                unique: true,
                filter: "[IdCatCliente] IS NOT NULL");

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
                name: "IX_dtUltimaMillaDet_IdUltimaMilla",
                table: "dtUltimaMillaDet",
                column: "IdUltimaMilla");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdCatEmpresa",
                table: "dtUltimaMillaEnc",
                column: "IdCatEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_dtUltimaMillaEnc_IdCatServicio",
                table: "dtUltimaMillaEnc",
                column: "IdCatServicio");

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
                name: "IX_integracionFacturaDet_IdPeticionesReferencia_IdPeticionesContenedor",
                table: "integracionFacturaDet",
                columns: new[] { "IdPeticionesReferencia", "IdPeticionesContenedor" },
                unique: true);

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
                name: "IX_integracionFacturaEst_IdIntFacturaEnc",
                table: "integracionFacturaEst",
                column: "IdIntFacturaEnc");

            migrationBuilder.CreateIndex(
                name: "IX_integracionReferencia_IdOrden",
                table: "integracionReferencia",
                column: "IdOrden",
                unique: true);

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
                name: "IX_peticionesContenedores_catAduanaIdCatAduana",
                table: "peticionesContenedores",
                column: "catAduanaIdCatAduana");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_ClienteId",
                table: "peticionesContenedores",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesContenedores_Contenedor_RefenciaCliente_IdReferencia",
                table: "peticionesContenedores",
                columns: new[] { "Contenedor", "RefenciaCliente", "IdReferencia" },
                unique: true);

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
                name: "IX_peticionesDocumentos_CatDocumentoIdCatDocumento",
                table: "peticionesDocumentos",
                column: "CatDocumentoIdCatDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_peticionesDocumentos_IdServicio",
                table: "peticionesDocumentos",
                column: "IdServicio");

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
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anticiposDet");

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
                name: "catProveedoresTarifaPatios");

            migrationBuilder.DropTable(
                name: "catRecintos");

            migrationBuilder.DropTable(
                name: "catRolesPermisos");

            migrationBuilder.DropTable(
                name: "catTransportistas");

            migrationBuilder.DropTable(
                name: "catUsuarioRoles");

            migrationBuilder.DropTable(
                name: "catUsuariosAduanas");

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
                name: "integracionFacturaEst");

            migrationBuilder.DropTable(
                name: "integracionReferencia");

            migrationBuilder.DropTable(
                name: "peticionesDocumentos");

            migrationBuilder.DropTable(
                name: "prefacturaDet");

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
                name: "integracionFacturaEnc");

            migrationBuilder.DropTable(
                name: "catDocumento");

            migrationBuilder.DropTable(
                name: "peticionesServicios");

            migrationBuilder.DropTable(
                name: "prefacturaEnc");

            migrationBuilder.DropTable(
                name: "dtAcarreos");

            migrationBuilder.DropTable(
                name: "dtUltimaMillaEnc");

            migrationBuilder.DropTable(
                name: "peticionesContenedores");

            migrationBuilder.DropTable(
                name: "catServicios");

            migrationBuilder.DropTable(
                name: "catTipoEstados");

            migrationBuilder.DropTable(
                name: "catNavieras");

            migrationBuilder.DropTable(
                name: "catPatios");

            migrationBuilder.DropTable(
                name: "peticionesReferencias");

            migrationBuilder.DropTable(
                name: "catProveedores");

            migrationBuilder.DropTable(
                name: "catReferenciaEstado");

            migrationBuilder.DropTable(
                name: "ordenes");

            migrationBuilder.DropTable(
                name: "catAduana");

            migrationBuilder.DropTable(
                name: "catProyectos");

            migrationBuilder.DropTable(
                name: "catSistemas");

            migrationBuilder.DropTable(
                name: "catSucursales");

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
