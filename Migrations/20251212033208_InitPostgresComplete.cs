using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExcelProcessorApi.Migrations
{
    /// <inheritdoc />
    public partial class InitPostgresComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlmacenUbicacionFolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Almacen = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FolioAsignado1 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Ubicacion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreadoPor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ActualizadoPor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlmacenUbicacionFolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreadoPor = table.Column<string>(type: "text", nullable: true),
                    ActualizadoPor = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContadorFolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Folio2General = table.Column<int>(type: "integer", nullable: false),
                    AcumuladoDiario = table.Column<int>(type: "integer", nullable: false),
                    RevisionesCount = table.Column<int>(type: "integer", nullable: false),
                    DeteccionesCount = table.Column<int>(type: "integer", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContadorFolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeteccionFolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Folio1 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Folio2 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Acumulado = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Sucursal = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Indicador = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Subindicador = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FolioAsignado1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UbicacionSucursal = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Monitorista = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Puesto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PuestoColaborador = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CoordinadorEnTurno = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ubicacion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Almacen = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Hora = table.Column<TimeSpan>(type: "interval", nullable: true),
                    GeneraImpacto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FolioAsignado2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ColaboradorInvolucrado = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    No = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Nomina = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LineaEmpresa = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AreaEspecifica = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TurnoOperativo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SupervisorJefeTurno = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SituacionDescripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    EnviaReporte = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Retroalimentacion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreadoPor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ActualizadoPor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeteccionFolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RevisionFolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Folio1 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Folio2 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Acumulado = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Almacen = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Observaciones = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Indicador = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Subindicador = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SeDetectoIncidenciaReportada = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AreaCargo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AreaSolicita = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Monitorista = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Puesto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ComentarioGeneral = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CoordinadorEnTurno = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Mes = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaIncidente = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Hora = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Monto = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Tiempo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Ticket = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FoliosAsignado1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FoliosAsignado2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PersonalInvolucrado = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    No = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Nomina = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LineaEmpresaPlacas = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Ubicacion2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AreaEspecifica = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TurnoOperativo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Situacion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    QuienEnvia = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreadoPor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ActualizadoPor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevisionFolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExcelUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UploadType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SheetName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    HeadersJson = table.Column<string>(type: "text", nullable: true),
                    TotalRows = table.Column<int>(type: "integer", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UploadedByUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelUploads_Users_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShiftHandOffNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pendiente"),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AssignedCoordinatorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    FinalizedByUserId = table.Column<int>(type: "integer", nullable: true),
                    FinalizedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Turno = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Area = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nota = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    AcknowledgedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsAcknowledged = table.Column<bool>(type: "boolean", nullable: false),
                    AcknowledgedByUserId = table.Column<int>(type: "integer", nullable: true),
                    IsFinalized = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftHandOffNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftHandOffNotes_Users_AcknowledgedByUserId",
                        column: x => x.AcknowledgedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShiftHandOffNotes_Users_AssignedCoordinatorId",
                        column: x => x.AssignedCoordinatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ShiftHandOffNotes_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftHandOffNotes_Users_FinalizedByUserId",
                        column: x => x.FinalizedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Pendiente"),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedByUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalActivities_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TechnicalActivities_Users_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UploadId = table.Column<int>(type: "integer", nullable: false),
                    RowIndex = table.Column<int>(type: "integer", nullable: false),
                    DataJson = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detecciones_ExcelUploads_UploadId",
                        column: x => x.UploadId,
                        principalTable: "ExcelUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExcelData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SheetName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Columna1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Columna2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Columna3 = table.Column<int>(type: "integer", nullable: true),
                    RowIndex = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Mes = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MesTexto = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Almacen = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MonitoristaReporta = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CoordinadorTurno = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FechaEnvio = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IncidenceMetadata = table.Column<string>(type: "text", nullable: true),
                    UploadedByUserId = table.Column<int>(type: "integer", nullable: false),
                    UploadId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelData_ExcelUploads_UploadId",
                        column: x => x.UploadId,
                        principalTable: "ExcelUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExcelData_Users_UploadedByUserId",
                        column: x => x.UploadedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Revisiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UploadId = table.Column<int>(type: "integer", nullable: false),
                    RowIndex = table.Column<int>(type: "integer", nullable: false),
                    DataJson = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revisiones_ExcelUploads_UploadId",
                        column: x => x.UploadId,
                        principalTable: "ExcelUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftHandOffAcknowledgements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NoteId = table.Column<int>(type: "integer", nullable: false),
                    CoordinatorUserId = table.Column<int>(type: "integer", nullable: false),
                    IsAcknowledged = table.Column<bool>(type: "boolean", nullable: false),
                    AcknowledgedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftHandOffAcknowledgements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftHandOffAcknowledgements_ShiftHandOffNotes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "ShiftHandOffNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftHandOffAcknowledgements_Users_CoordinatorUserId",
                        column: x => x.CoordinatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftHandOffAcknowledgements_Users_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalActivityImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TechnicalActivityId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FileName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileExtension = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalActivityImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalActivityImages_TechnicalActivities_TechnicalActivi~",
                        column: x => x.TechnicalActivityId,
                        principalTable: "TechnicalActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicalActivityImages_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Acceso completo al sistema", "Administrador" },
                    { 2, "Puede subir archivos Excel y crear gráficas", "Coordinador" },
                    { 3, "Puede consultar información y dar seguimiento", "Monitorista" },
                    { 4, "Puede registrar y dar seguimiento a actividades técnicas", "Técnico" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "FirstName", "IsActive", "LastLogin", "LastName", "PasswordHash", "RoleId", "Username" },
                values: new object[] { 1, new DateTime(2025, 11, 10, 9, 45, 3, 932, DateTimeKind.Utc).AddTicks(3187), "Administrador", true, null, "Sistema", "$2a$11$/MkrViP.Yr1L.J6JmTYPNOk.etb8CjytC3bm/7iXj37.Y/FAWshXC", 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Catalogos_Tipo_Valor",
                table: "Catalogos",
                columns: new[] { "Tipo", "Valor" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContadorFolios_Fecha",
                table: "ContadorFolios",
                column: "Fecha",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detecciones_UploadId",
                table: "Detecciones",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelData_UploadedByUserId",
                table: "ExcelData",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelData_UploadId",
                table: "ExcelData",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelUploads_UploadedByUserId",
                table: "ExcelUploads",
                column: "UploadedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisiones_UploadId",
                table: "Revisiones",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandOffAcknowledgements_CoordinatorUserId",
                table: "ShiftHandOffAcknowledgements",
                column: "CoordinatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandOffAcknowledgements_NoteId_CoordinatorUserId",
                table: "ShiftHandOffAcknowledgements",
                columns: new[] { "NoteId", "CoordinatorUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandOffAcknowledgements_UpdatedByUserId",
                table: "ShiftHandOffAcknowledgements",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandOffNotes_AcknowledgedByUserId",
                table: "ShiftHandOffNotes",
                column: "AcknowledgedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandOffNotes_AssignedCoordinatorId",
                table: "ShiftHandOffNotes",
                column: "AssignedCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandOffNotes_CreatedByUserId",
                table: "ShiftHandOffNotes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftHandOffNotes_FinalizedByUserId",
                table: "ShiftHandOffNotes",
                column: "FinalizedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalActivities_CreatedByUserId",
                table: "TechnicalActivities",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalActivities_UpdatedByUserId",
                table: "TechnicalActivities",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalActivityImages_CreatedByUserId",
                table: "TechnicalActivityImages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalActivityImages_TechnicalActivityId",
                table: "TechnicalActivityImages",
                column: "TechnicalActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlmacenUbicacionFolios");

            migrationBuilder.DropTable(
                name: "Catalogos");

            migrationBuilder.DropTable(
                name: "ContadorFolios");

            migrationBuilder.DropTable(
                name: "Detecciones");

            migrationBuilder.DropTable(
                name: "DeteccionFolios");

            migrationBuilder.DropTable(
                name: "ExcelData");

            migrationBuilder.DropTable(
                name: "Revisiones");

            migrationBuilder.DropTable(
                name: "RevisionFolios");

            migrationBuilder.DropTable(
                name: "ShiftHandOffAcknowledgements");

            migrationBuilder.DropTable(
                name: "TechnicalActivityImages");

            migrationBuilder.DropTable(
                name: "ExcelUploads");

            migrationBuilder.DropTable(
                name: "ShiftHandOffNotes");

            migrationBuilder.DropTable(
                name: "TechnicalActivities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
