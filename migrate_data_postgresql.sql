-- Script para migrar datos de SQL Server a PostgreSQL
-- Ejecutar después de crear la estructura de tablas

-- IMPORTANTE: Reemplaza los valores reales en cada INSERT

-- Migración de Roles
INSERT INTO "Roles" ("Name", "Description") VALUES 
('Técnico', 'Usuario con permisos de técnico'),
('Administrador', 'Usuario con todos los permisos'),
('Coordinador', 'Usuario con permisos de coordinador'),
('Monitorista', 'Usuario con permisos de monitorista')
ON CONFLICT ("Name") DO NOTHING;

-- Migración de Usuarios (ejemplo - reemplazar con datos reales)
INSERT INTO "Users" ("Username", "PasswordHash", "FirstName", "LastName", "RoleId", "IsActive", "CreatedAt", "LastLogin") VALUES 
('admin', 'hashed_password_here', 'Admin', 'User', 2, TRUE, NOW(), NULL)
ON CONFLICT ("Username") DO NOTHING;

-- Migración de Catálogos (ejemplo - reemplazar con datos reales)
INSERT INTO "Catalogos" ("Tipo", "Valor", "Descripcion", "Activo", "FechaCreacion", "FechaActualizacion", "CreadoPor", "ActualizadoPor") VALUES 
('Almacen', 'ALM001', 'Almacén Principal', TRUE, NOW(), NULL, 'admin', NULL),
('Almacen', 'ALM002', 'Almacén Secundario', TRUE, NOW(), NULL, 'admin', NULL),
('Indicador', 'IND001', 'Indicador de Calidad', TRUE, NOW(), NULL, 'admin', NULL)
ON CONFLICT ("Tipo", "Valor") DO NOTHING;

-- Migración de AlmacenUbicacionFolios (ejemplo)
INSERT INTO "AlmacenUbicacionFolios" ("Almacen", "FolioAsignado1", "Ubicacion", "Activo", "FechaCreacion", "FechaActualizacion", "CreadoPor", "ActualizadoPor") VALUES 
('ALM001', 'F001', 'A1-B2-C3', TRUE, NOW(), NULL, 'admin', NULL)
ON CONFLICT DO NOTHING;

-- Migración de ContadorFolios (ejemplo)
INSERT INTO "ContadorFolios" ("Fecha", "Folio2General", "AcumuladoDiario", "RevisionesCount", "DeteccionesCount", "UltimaActualizacion") VALUES 
(NOW()::DATE, 1000, 50, 10, 5, NOW())
ON CONFLICT ("Fecha") DO NOTHING;

-- Migración de ShiftHandOffNotes (ejemplo)
INSERT INTO "ShiftHandOffNotes" ("Description", "Status", "AssignedCoordinatorId", "CreatedByUserId", "CreatedAt", "UpdatedAt", "Area", "IsAcknowledged", "IsFinalized", "Nota", "Turno", "Type", "Title", "Priority") VALUES 
('Nota de prueba', 'Pendiente', 1, 1, NOW(), NOW(), 'Producción', FALSE, FALSE, 'Detalles de la nota', 'Matutino', 'General', 'Título de nota', 'Media')
ON CONFLICT DO NOTHING;

-- Migración de TechnicalActivities (ejemplo)
INSERT INTO "TechnicalActivities" ("Description", "Status", "Notes", "CreatedAt", "UpdatedAt", "CreatedByUserId", "EndDate", "StartDate") VALUES 
('Mantenimiento preventivo', 'Pendiente', 'Revisar equipos', NOW(), NOW(), 1, NOW() + INTERVAL '1 day', NOW())
ON CONFLICT DO NOTHING;

-- Migración de ExcelUploads (ejemplo)
INSERT INTO "ExcelUploads" ("UploadType", "FileName", "SheetName", "HeadersJson", "TotalRows", "UploadedAt", "UploadedByUserId") VALUES 
('Revision', 'archivo.xlsx', 'Hoja1', '{"headers": ["col1", "col2"]}', 100, NOW(), 1)
ON CONFLICT DO NOTHING;

-- Migración de ExcelData (ejemplo)
INSERT INTO "ExcelData" ("SheetName", "Columna1", "Columna2", "Columna3", "Mes", "FechaCreacion", "UploadedByUserId", "UploadId", "Almacen", "CoordinadorTurno", "MesTexto", "MonitoristaReporta", "RowIndex", "FechaEnvio", "IncidenceMetadata") VALUES 
('Hoja1', 'Dato1', 'Dato2', 123, NOW()::DATE, NOW(), 1, 1, 'ALM001', 'Juan Pérez', 'Diciembre', 'Monitor A', 1, '2025-12-12', '{"metadata": "test"}')
ON CONFLICT DO NOTHING;

-- Migración de Revisiones (ejemplo)
INSERT INTO "Revisiones" ("UploadId", "RowIndex", "DataJson", "CreatedAt") VALUES 
(1, 1, '{"revision": "data"}', NOW())
ON CONFLICT DO NOTHING;

-- Migración de Detecciones (ejemplo)
INSERT INTO "Detecciones" ("UploadId", "RowIndex", "DataJson", "CreatedAt") VALUES 
(1, 1, '{"deteccion": "data"}', NOW())
ON CONFLICT DO NOTHING;

-- Migración de RevisionFolios (ejemplo)
INSERT INTO "RevisionFolios" ("Folio1", "Folio2", "Acumulado", "Almacen", "Observaciones", "Indicador", "Subindicador", "SeDetectoIncidenciaReportada", "AreaCargo", "AreaSolicita", "Monitorista", "Puesto", "ComentarioGeneral", "CoordinadorEnTurno", "FechaEnvio", "Mes", "FechaSolicitud", "FechaIncidente", "Hora", "Monto", "Codigo", "Tiempo", "Ticket", "FoliosAsignado1", "FoliosAsignado2", "PersonalInvolucrado", "No", "Nomina", "LineaEmpresaPlacas", "Ubicacion2", "AreaEspecifica", "TurnoOperativo", "Situacion", "QuienEnvia", "FechaCreacion", "FechaActualizacion", "CreadoPor", "ActualizadoPor", "Activo") VALUES 
('F001', 'F002', 'A001', 'ALM001', 'Observación de prueba', 'IND001', 'SUB001', 'Sí', 'Producción', 'Mantenimiento', 'Monitor A', 'Técnico', 'Comentario general', 'Coordinador A', NOW(), 'Diciembre', NOW()::DATE, NOW()::DATE, '14:30:00', '1000', 'COD001', '2 horas', 'TKT001', 'FA001', 'FA002', 'Juan Pérez', '123', 'N001', 'EMP001', 'UB001', 'Producción', 'Matutino', 'Situación normal', 'Admin', NOW(), NULL, 'admin', NULL, TRUE)
ON CONFLICT DO NOTHING;

-- Migración de DeteccionFolios (ejemplo)
INSERT INTO "DeteccionFolios" ("Folio1", "Folio2", "Acumulado", "Sucursal", "Codigo", "Indicador", "Subindicador", "FolioAsignado1", "UbicacionSucursal", "Monitorista", "Puesto", "PuestoColaborador", "CoordinadorEnTurno", "FechaEnvio", "Ubicacion", "Almacen", "Hora", "GeneraImpacto", "FolioAsignado2", "ColaboradorInvolucrado", "No", "Nomina", "LineaEmpresa", "AreaEspecifica", "TurnoOperativo", "SupervisorJefeTurno", "SituacionDescripcion", "EnviaReporte", "Retroalimentacion", "FechaCreacion", "FechaActualizacion", "CreadoPor", "ActualizadoPor", "Activo") VALUES 
('F001', 'F002', 'A001', 'Sucursal A', 'COD001', 'IND001', 'SUB001', 'FA001', 'UB001', 'Monitor A', 'Técnico', 'Operador', 'Coordinador A', NOW(), 'Ubicación A', 'ALM001', '14:30:00', 'Sí', 'FA002', 'Juan Pérez', '123', 'N001', 'Línea 1', 'Producción', 'Matutino', 'Supervisor A', 'Situación detallada', 'Monitor A', 'Retroalimentación', NOW(), NULL, 'admin', NULL, TRUE)
ON CONFLICT DO NOTHING;

-- Migración de TechnicalActivityImages (ejemplo)
INSERT INTO "TechnicalActivityImages" ("TechnicalActivityId", "Type", "FileName", "OriginalFileName", "FileExtension", "FileSize", "FilePath", "Url", "CreatedAt", "CreatedByUserId") VALUES 
(1, 'Before', 'image1.jpg', 'original_image1.jpg', '.jpg', 1024000, '/path/to/image1.jpg', 'http://example.com/image1.jpg', NOW(), 1)
ON CONFLICT DO NOTHING;

-- Migración de ShiftHandOffAcknowledgements (ejemplo)
INSERT INTO "ShiftHandOffAcknowledgements" ("NoteId", "CoordinatorUserId", "IsAcknowledged", "AcknowledgedAt", "UpdatedByUserId") VALUES 
(1, 1, TRUE, NOW(), NULL)
ON CONFLICT ("NoteId", "CoordinatorUserId") DO NOTHING;

-- Resetear secuencias de auto-incremento
SELECT setval(pg_get_serial_sequence('"Roles"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "Roles";
SELECT setval(pg_get_serial_sequence('"Users"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "Users";
SELECT setval(pg_get_serial_sequence('"Catalogos"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "Catalogos";
SELECT setval(pg_get_serial_sequence('"AlmacenUbicacionFolios"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "AlmacenUbicacionFolios";
SELECT setval(pg_get_serial_sequence('"ContadorFolios"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "ContadorFolios";
SELECT setval(pg_get_serial_sequence('"ShiftHandOffNotes"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "ShiftHandOffNotes";
SELECT setval(pg_get_serial_sequence('"TechnicalActivities"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "TechnicalActivities";
SELECT setval(pg_get_serial_sequence('"ExcelUploads"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "ExcelUploads";
SELECT setval(pg_get_serial_sequence('"ExcelData"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "ExcelData";
SELECT setval(pg_get_serial_sequence('"Revisiones"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "Revisiones";
SELECT setval(pg_get_serial_sequence('"Detecciones"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "Detecciones";
SELECT setval(pg_get_serial_sequence('"RevisionFolios"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "RevisionFolios";
SELECT setval(pg_get_serial_sequence('"DeteccionFolios"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "DeteccionFolios";
SELECT setval(pg_get_serial_sequence('"TechnicalActivityImages"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "TechnicalActivityImages";
SELECT setval(pg_get_serial_sequence('"ShiftHandOffAcknowledgements"', 'Id'), coalesce(max("Id"), 1), max("Id") IS NOT null) FROM "ShiftHandOffAcknowledgements";
