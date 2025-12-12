-- Script SQL Server convertido a PostgreSQL
-- Base de datos: trupercam

-- Crear base de datos (ya existe)
-- CREATE DATABASE trupercam;

-- Tablas
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" VARCHAR(150) NOT NULL,
    "ProductVersion" VARCHAR(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE IF NOT EXISTS "AlmacenUbicacionFolios" (
    "Id" SERIAL PRIMARY KEY,
    "Almacen" VARCHAR(100) NOT NULL,
    "FolioAsignado1" VARCHAR(20) NOT NULL,
    "Ubicacion" VARCHAR(200) NOT NULL,
    "Activo" BOOLEAN NOT NULL,
    "FechaCreacion" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "FechaActualizacion" TIMESTAMP WITH TIME ZONE,
    "CreadoPor" VARCHAR(100),
    "ActualizadoPor" VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS "Catalogos" (
    "Id" SERIAL PRIMARY KEY,
    "Tipo" VARCHAR(100) NOT NULL,
    "Valor" VARCHAR(200) NOT NULL,
    "Descripcion" VARCHAR(500),
    "Activo" BOOLEAN NOT NULL,
    "FechaCreacion" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "FechaActualizacion" TIMESTAMP WITH TIME ZONE,
    "CreadoPor" TEXT,
    "ActualizadoPor" TEXT
);

CREATE TABLE IF NOT EXISTS "ContadorFolios" (
    "Id" SERIAL PRIMARY KEY,
    "Fecha" TIMESTAMP WITH TIME ZONE NOT NULL,
    "Folio2General" INTEGER NOT NULL,
    "AcumuladoDiario" INTEGER NOT NULL,
    "RevisionesCount" INTEGER NOT NULL,
    "DeteccionesCount" INTEGER NOT NULL,
    "UltimaActualizacion" TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS "Detecciones" (
    "Id" SERIAL PRIMARY KEY,
    "UploadId" INTEGER NOT NULL,
    "RowIndex" INTEGER NOT NULL,
    "DataJson" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "DeteccionFolios" (
    "Id" SERIAL PRIMARY KEY,
    "Folio1" VARCHAR(10) NOT NULL,
    "Folio2" VARCHAR(10) NOT NULL,
    "Acumulado" VARCHAR(10) NOT NULL,
    "Sucursal" VARCHAR(200),
    "Codigo" VARCHAR(200),
    "Indicador" VARCHAR(200),
    "Subindicador" VARCHAR(200),
    "FolioAsignado1" VARCHAR(200),
    "UbicacionSucursal" VARCHAR(200),
    "Monitorista" VARCHAR(200),
    "Puesto" VARCHAR(200),
    "PuestoColaborador" VARCHAR(200),
    "CoordinadorEnTurno" VARCHAR(200),
    "FechaEnvio" TIMESTAMP WITH TIME ZONE,
    "Ubicacion" VARCHAR(200),
    "Almacen" VARCHAR(200),
    "Hora" INTERVAL,
    "GeneraImpacto" VARCHAR(500),
    "FolioAsignado2" VARCHAR(200),
    "ColaboradorInvolucrado" VARCHAR(200),
    "No" VARCHAR(100),
    "Nomina" VARCHAR(100),
    "LineaEmpresa" VARCHAR(200),
    "AreaEspecifica" VARCHAR(200),
    "TurnoOperativo" VARCHAR(100),
    "SupervisorJefeTurno" VARCHAR(200),
    "SituacionDescripcion" VARCHAR(1000),
    "EnviaReporte" VARCHAR(200),
    "Retroalimentacion" VARCHAR(1000),
    "FechaCreacion" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "FechaActualizacion" TIMESTAMP WITH TIME ZONE,
    "CreadoPor" VARCHAR(200),
    "ActualizadoPor" VARCHAR(200),
    "Activo" BOOLEAN NOT NULL
);

CREATE TABLE IF NOT EXISTS "ExcelData" (
    "Id" SERIAL PRIMARY KEY,
    "SheetName" VARCHAR(100) NOT NULL,
    "Columna1" VARCHAR(255),
    "Columna2" VARCHAR(255),
    "Columna3" INTEGER,
    "Mes" TIMESTAMP WITH TIME ZONE NOT NULL,
    "FechaCreacion" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "UploadedByUserId" INTEGER NOT NULL DEFAULT 0,
    "UploadId" INTEGER,
    "Almacen" VARCHAR(255),
    "CoordinadorTurno" VARCHAR(255),
    "MesTexto" VARCHAR(255),
    "MonitoristaReporta" VARCHAR(255),
    "RowIndex" INTEGER NOT NULL DEFAULT 0,
    "FechaEnvio" VARCHAR(64),
    "IncidenceMetadata" TEXT
);

CREATE TABLE IF NOT EXISTS "ExcelUploads" (
    "Id" SERIAL PRIMARY KEY,
    "UploadType" VARCHAR(50) NOT NULL,
    "FileName" VARCHAR(255) NOT NULL,
    "SheetName" VARCHAR(255) NOT NULL,
    "HeadersJson" TEXT,
    "TotalRows" INTEGER NOT NULL,
    "UploadedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "UploadedByUserId" INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS "Revisiones" (
    "Id" SERIAL PRIMARY KEY,
    "UploadId" INTEGER NOT NULL,
    "RowIndex" INTEGER NOT NULL,
    "DataJson" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "RevisionFolios" (
    "Id" SERIAL PRIMARY KEY,
    "Folio1" VARCHAR(10) NOT NULL,
    "Folio2" VARCHAR(10) NOT NULL,
    "Acumulado" VARCHAR(10) NOT NULL,
    "Almacen" VARCHAR(200),
    "Observaciones" VARCHAR(1000),
    "Indicador" VARCHAR(200),
    "Subindicador" VARCHAR(200),
    "SeDetectoIncidenciaReportada" VARCHAR(500),
    "AreaCargo" VARCHAR(200),
    "AreaSolicita" VARCHAR(200),
    "Monitorista" VARCHAR(200),
    "Puesto" VARCHAR(200),
    "ComentarioGeneral" VARCHAR(1000),
    "CoordinadorEnTurno" VARCHAR(200),
    "FechaEnvio" TIMESTAMP WITH TIME ZONE,
    "Mes" VARCHAR(50),
    "FechaSolicitud" TIMESTAMP WITH TIME ZONE,
    "FechaIncidente" TIMESTAMP WITH TIME ZONE,
    "Hora" INTERVAL,
    "Monto" VARCHAR(20),
    "Codigo" VARCHAR(200),
    "Tiempo" VARCHAR(200),
    "Ticket" VARCHAR(200),
    "FoliosAsignado1" VARCHAR(200),
    "FoliosAsignado2" VARCHAR(200),
    "PersonalInvolucrado" VARCHAR(200),
    "No" VARCHAR(100),
    "Nomina" VARCHAR(100),
    "LineaEmpresaPlacas" VARCHAR(200),
    "Ubicacion2" VARCHAR(200),
    "AreaEspecifica" VARCHAR(200),
    "TurnoOperativo" VARCHAR(100),
    "Situacion" VARCHAR(1000),
    "QuienEnvia" VARCHAR(200),
    "FechaCreacion" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "FechaActualizacion" TIMESTAMP WITH TIME ZONE,
    "CreadoPor" VARCHAR(200),
    "ActualizadoPor" VARCHAR(200),
    "Activo" BOOLEAN NOT NULL
);

CREATE TABLE IF NOT EXISTS "Roles" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL,
    "Description" VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS "ShiftHandOffAcknowledgements" (
    "Id" SERIAL PRIMARY KEY,
    "NoteId" INTEGER NOT NULL,
    "CoordinatorUserId" INTEGER NOT NULL,
    "IsAcknowledged" BOOLEAN NOT NULL,
    "AcknowledgedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedByUserId" INTEGER
);

CREATE TABLE IF NOT EXISTS "ShiftHandOffNotes" (
    "Id" SERIAL PRIMARY KEY,
    "Description" VARCHAR(500),
    "Status" VARCHAR(50) NOT NULL DEFAULT 'Pendiente',
    "AssignedCoordinatorId" INTEGER,
    "CreatedByUserId" INTEGER NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "FinalizedAt" TIMESTAMP WITH TIME ZONE,
    "FinalizedByUserId" INTEGER,
    "AcknowledgedAt" TIMESTAMP WITH TIME ZONE,
    "AcknowledgedByUserId" INTEGER,
    "Area" VARCHAR(50) NOT NULL DEFAULT '',
    "IsAcknowledged" BOOLEAN NOT NULL DEFAULT FALSE,
    "IsFinalized" BOOLEAN NOT NULL DEFAULT FALSE,
    "Nota" VARCHAR(1000) NOT NULL DEFAULT '',
    "Turno" VARCHAR(50) NOT NULL DEFAULT '',
    "Type" VARCHAR(20) NOT NULL DEFAULT '',
    "Title" VARCHAR(200) NOT NULL DEFAULT '',
    "Priority" VARCHAR(20) NOT NULL DEFAULT ''
);

CREATE TABLE IF NOT EXISTS "TechnicalActivities" (
    "Id" SERIAL PRIMARY KEY,
    "Description" VARCHAR(500) NOT NULL,
    "Status" VARCHAR(50) NOT NULL DEFAULT 'Pendiente',
    "Notes" VARCHAR(1000),
    "CreatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "CompletedAt" TIMESTAMP WITH TIME ZONE,
    "CreatedByUserId" INTEGER NOT NULL,
    "UpdatedByUserId" INTEGER,
    "EndDate" TIMESTAMP WITH TIME ZONE,
    "StartDate" TIMESTAMP WITH TIME ZONE
);

CREATE TABLE IF NOT EXISTS "TechnicalActivityImages" (
    "Id" SERIAL PRIMARY KEY,
    "TechnicalActivityId" INTEGER NOT NULL,
    "Type" VARCHAR(20) NOT NULL,
    "FileName" VARCHAR(500) NOT NULL,
    "OriginalFileName" VARCHAR(500) NOT NULL,
    "FileExtension" VARCHAR(10) NOT NULL,
    "FileSize" BIGINT NOT NULL,
    "FilePath" VARCHAR(1000) NOT NULL,
    "Url" VARCHAR(2000) NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "CreatedByUserId" INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Username" VARCHAR(100) NOT NULL,
    "PasswordHash" VARCHAR(255) NOT NULL,
    "FirstName" VARCHAR(100) NOT NULL,
    "LastName" VARCHAR(100) NOT NULL,
    "RoleId" INTEGER NOT NULL,
    "IsActive" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    "LastLogin" TIMESTAMP WITH TIME ZONE
);

-- Índices únicos
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Catalogos_Tipo_Valor" ON "Catalogos" ("Tipo", "Valor");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_ContadorFolios_Fecha" ON "ContadorFolios" ("Fecha");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Roles_Name" ON "Roles" ("Name");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Users_Username" ON "Users" ("Username");
CREATE UNIQUE INDEX IF NOT EXISTS "IX_ShiftHandOffAcknowledgements_NoteId_CoordinatorUserId" ON "ShiftHandOffAcknowledgements" ("NoteId", "CoordinatorUserId");

-- Índices regulares
CREATE INDEX IF NOT EXISTS "IX_Detecciones_UploadId" ON "Detecciones" ("UploadId");
CREATE INDEX IF NOT EXISTS "IX_ExcelData_UploadedByUserId" ON "ExcelData" ("UploadedByUserId");
CREATE INDEX IF NOT EXISTS "IX_ExcelData_UploadId" ON "ExcelData" ("UploadId");
CREATE INDEX IF NOT EXISTS "IX_ExcelUploads_UploadedByUserId" ON "ExcelUploads" ("UploadedByUserId");
CREATE INDEX IF NOT EXISTS "IX_Revisiones_UploadId" ON "Revisiones" ("UploadId");
CREATE INDEX IF NOT EXISTS "IX_ShiftHandOffAcknowledgements_CoordinatorUserId" ON "ShiftHandOffAcknowledgements" ("CoordinatorUserId");
CREATE INDEX IF NOT EXISTS "IX_ShiftHandOffAcknowledgements_UpdatedByUserId" ON "ShiftHandOffAcknowledgements" ("UpdatedByUserId");
CREATE INDEX IF NOT EXISTS "IX_ShiftHandOffNotes_AcknowledgedByUserId" ON "ShiftHandOffNotes" ("AcknowledgedByUserId");
CREATE INDEX IF NOT EXISTS "IX_ShiftHandOffNotes_AssignedCoordinatorId" ON "ShiftHandOffNotes" ("AssignedCoordinatorId");
CREATE INDEX IF NOT EXISTS "IX_ShiftHandOffNotes_CreatedByUserId" ON "ShiftHandOffNotes" ("CreatedByUserId");
CREATE INDEX IF NOT EXISTS "IX_ShiftHandOffNotes_FinalizedByUserId" ON "ShiftHandOffNotes" ("FinalizedByUserId");
CREATE INDEX IF NOT EXISTS "IX_TechnicalActivities_CreatedByUserId" ON "TechnicalActivities" ("CreatedByUserId");
CREATE INDEX IF NOT EXISTS "IX_TechnicalActivities_UpdatedByUserId" ON "TechnicalActivities" ("UpdatedByUserId");
CREATE INDEX IF NOT EXISTS "IX_TechnicalActivityImages_CreatedByUserId" ON "TechnicalActivityImages" ("CreatedByUserId");
CREATE INDEX IF NOT EXISTS "IX_TechnicalActivityImages_TechnicalActivityId" ON "TechnicalActivityImages" ("TechnicalActivityId");
CREATE INDEX IF NOT EXISTS "IX_Users_RoleId" ON "Users" ("RoleId");

-- Restricciones FOREIGN KEY
ALTER TABLE "Detecciones" ADD CONSTRAINT "FK_Detecciones_ExcelUploads_UploadId" 
    FOREIGN KEY ("UploadId") REFERENCES "ExcelUploads" ("Id") ON DELETE CASCADE;

ALTER TABLE "ExcelData" ADD CONSTRAINT "FK_ExcelData_ExcelUploads_UploadId" 
    FOREIGN KEY ("UploadId") REFERENCES "ExcelUploads" ("Id") ON DELETE SET NULL;

ALTER TABLE "ExcelData" ADD CONSTRAINT "FK_ExcelData_Users_UploadedByUserId" 
    FOREIGN KEY ("UploadedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "ExcelUploads" ADD CONSTRAINT "FK_ExcelUploads_Users_UploadedByUserId" 
    FOREIGN KEY ("UploadedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "Revisiones" ADD CONSTRAINT "FK_Revisiones_ExcelUploads_UploadId" 
    FOREIGN KEY ("UploadId") REFERENCES "ExcelUploads" ("Id") ON DELETE CASCADE;

ALTER TABLE "ShiftHandOffAcknowledgements" ADD CONSTRAINT "FK_ShiftHandOffAcknowledgements_ShiftHandOffNotes_NoteId" 
    FOREIGN KEY ("NoteId") REFERENCES "ShiftHandOffNotes" ("Id") ON DELETE CASCADE;

ALTER TABLE "ShiftHandOffAcknowledgements" ADD CONSTRAINT "FK_ShiftHandOffAcknowledgements_Users_CoordinatorUserId" 
    FOREIGN KEY ("CoordinatorUserId") REFERENCES "Users" ("Id") ON DELETE CASCADE;

ALTER TABLE "ShiftHandOffAcknowledgements" ADD CONSTRAINT "FK_ShiftHandOffAcknowledgements_Users_UpdatedByUserId" 
    FOREIGN KEY ("UpdatedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "ShiftHandOffNotes" ADD CONSTRAINT "FK_ShiftHandOffNotes_Users_AcknowledgedByUserId" 
    FOREIGN KEY ("AcknowledgedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "ShiftHandOffNotes" ADD CONSTRAINT "FK_ShiftHandOffNotes_Users_AssignedCoordinatorId" 
    FOREIGN KEY ("AssignedCoordinatorId") REFERENCES "Users" ("Id") ON DELETE SET NULL;

ALTER TABLE "ShiftHandOffNotes" ADD CONSTRAINT "FK_ShiftHandOffNotes_Users_CreatedByUserId" 
    FOREIGN KEY ("CreatedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "ShiftHandOffNotes" ADD CONSTRAINT "FK_ShiftHandOffNotes_Users_FinalizedByUserId" 
    FOREIGN KEY ("FinalizedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "TechnicalActivities" ADD CONSTRAINT "FK_TechnicalActivities_Users_CreatedByUserId" 
    FOREIGN KEY ("CreatedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "TechnicalActivities" ADD CONSTRAINT "FK_TechnicalActivities_Users_UpdatedByUserId" 
    FOREIGN KEY ("UpdatedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "TechnicalActivityImages" ADD CONSTRAINT "FK_TechnicalActivityImages_TechnicalActivities_TechnicalActivityId" 
    FOREIGN KEY ("TechnicalActivityId") REFERENCES "TechnicalActivities" ("Id") ON DELETE CASCADE;

ALTER TABLE "TechnicalActivityImages" ADD CONSTRAINT "FK_TechnicalActivityImages_Users_CreatedByUserId" 
    FOREIGN KEY ("CreatedByUserId") REFERENCES "Users" ("Id");

ALTER TABLE "Users" ADD CONSTRAINT "FK_Users_Roles_RoleId" 
    FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id");
