using Microsoft.EntityFrameworkCore;
using ExcelProcessorApi.Data;
using ExcelProcessorApi.Models;

namespace ExcelProcessorApi.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            // Asegurar que la base de datos existe
            await context.Database.EnsureCreatedAsync();

            // Agregar catálogos si no existen
            await AddCatalogos(context);
        }

        private static async Task AddCatalogos(ApplicationDbContext context)
        {
            // Verificar si ya hay catálogos
            if (await context.Catalogos.AnyAsync())
            {
                return; // Ya hay datos, no agregar
            }

            var catalogos = new List<Catalogo>
            {
                // Almacenes
                new Catalogo { Tipo = "Almacen", Valor = "Almacén Central", Descripcion = "Principal warehouse" },
                new Catalogo { Tipo = "Almacen", Valor = "Almacén Norte", Descripcion = "North warehouse" },
                new Catalogo { Tipo = "Almacen", Valor = "Almacén Sur", Descripcion = "South warehouse" },
                new Catalogo { Tipo = "Almacen", Valor = "Almacén Este", Descripcion = "East warehouse" },
                new Catalogo { Tipo = "Almacen", Valor = "Almacén Oeste", Descripcion = "West warehouse" },

                // Indicadores
                new Catalogo { Tipo = "Indicador", Valor = "Calidad", Descripcion = "Quality indicators" },
                new Catalogo { Tipo = "Indicador", Valor = "Seguridad", Descripcion = "Safety indicators" },
                new Catalogo { Tipo = "Indicador", Valor = "Productividad", Descripcion = "Productivity indicators" },
                new Catalogo { Tipo = "Indicador", Valor = "Mantenimiento", Descripcion = "Maintenance indicators" },
                new Catalogo { Tipo = "Indicador", Valor = "Inventario", Descripcion = "Inventory indicators" },
                new Catalogo { Tipo = "Indicador", Valor = "Servicio al Cliente", Descripcion = "Customer service indicators" },
                new Catalogo { Tipo = "Indicador", Valor = "Costos", Descripcion = "Cost indicators" },
                new Catalogo { Tipo = "Indicador", Valor = "Tiempo de Entrega", Descripcion = "Delivery time indicators" },

                // Subindicadores
                new Catalogo { Tipo = "Subindicador", Valor = "Defectos", Descripcion = "Product defects" },
                new Catalogo { Tipo = "Subindicador", Valor = "Accidentes", Descripcion = "Work accidents" },
                new Catalogo { Tipo = "Subindicador", Valor = "Producción/Hora", Descripcion = "Units per hour" },
                new Catalogo { Tipo = "Subindicador", Valor = "Tiempo Muerto", Descripcion = "Downtime" },
                new Catalogo { Tipo = "Subindicador", Valor = "Rotación de Inventario", Descripcion = "Inventory turnover" },
                new Catalogo { Tipo = "Subindicador", Valor = "Satisfacción del Cliente", Descripcion = "Customer satisfaction" },
                new Catalogo { Tipo = "Subindicador", Valor = "Costos Operativos", Descripcion = "Operational costs" },
                new Catalogo { Tipo = "Subindicador", Valor = "Tiempo de Ciclo", Descripcion = "Cycle time" },

                // Áreas
                new Catalogo { Tipo = "Area", Valor = "Producción", Descripcion = "Production area" },
                new Catalogo { Tipo = "Area", Valor = "Calidad", Descripcion = "Quality area" },
                new Catalogo { Tipo = "Area", Valor = "Mantenimiento", Descripcion = "Maintenance area" },
                new Catalogo { Tipo = "Area", Valor = "Logística", Descripcion = "Logistics area" },
                new Catalogo { Tipo = "Area", Valor = "Compras", Descripcion = "Purchasing area" },
                new Catalogo { Tipo = "Area", Valor = "Ventas", Descripcion = "Sales area" },
                new Catalogo { Tipo = "Area", Valor = "Recursos Humanos", Descripcion = "HR area" },
                new Catalogo { Tipo = "Area", Valor = "Finanzas", Descripcion = "Finance area" },
                new Catalogo { Tipo = "Area", Valor = "TI", Descripcion = "IT area" },

                // Puestos
                new Catalogo { Tipo = "Puesto", Valor = "Operador", Descripcion = "Machine operator" },
                new Catalogo { Tipo = "Puesto", Valor = "Supervisor", Descripcion = "Production supervisor" },
                new Catalogo { Tipo = "Puesto", Valor = "Gerente", Descripcion = "Department manager" },
                new Catalogo { Tipo = "Puesto", Valor = "Técnico", Descripcion = "Maintenance technician" },
                new Catalogo { Tipo = "Puesto", Valor = "Analista", Descripcion = "Quality analyst" },
                new Catalogo { Tipo = "Puesto", Valor = "Coordinador", Descripcion = "Operations coordinator" },
                new Catalogo { Tipo = "Puesto", Valor = "Jefe de Turno", Descripcion = "Shift supervisor" },
                new Catalogo { Tipo = "Puesto", Valor = "Monitorista", Descripcion = "Monitoring operator" },

                // Monitoristas
                new Catalogo { Tipo = "Monitorista", Valor = "Juan Pérez", Descripcion = "Monitorista principal" },
                new Catalogo { Tipo = "Monitorista", Valor = "María González", Descripcion = "Monitorista turno matutino" },
                new Catalogo { Tipo = "Monitorista", Valor = "Carlos Rodríguez", Descripcion = "Monitorista turno vespertino" },
                new Catalogo { Tipo = "Monitorista", Valor = "Ana Martínez", Descripcion = "Monitorista turno nocturno" },
                new Catalogo { Tipo = "Monitorista", Valor = "Luis Sánchez", Descripcion = "Monitorista de respaldo" },

                // Sucursales
                new Catalogo { Tipo = "Sucursal", Valor = "Matriz", Descripcion = "Main office" },
                new Catalogo { Tipo = "Sucursal", Valor = "Sucursal Norte", Descripcion = "North branch" },
                new Catalogo { Tipo = "Sucursal", Valor = "Sucursal Sur", Descripcion = "South branch" },
                new Catalogo { Tipo = "Sucursal", Valor = "Sucursal Este", Descripcion = "East branch" },
                new Catalogo { Tipo = "Sucursal", Valor = "Sucursal Oeste", Descripcion = "West branch" },
                new Catalogo { Tipo = "Sucursal", Valor = "Planta 1", Descripcion = "Production plant 1" },
                new Catalogo { Tipo = "Sucursal", Valor = "Planta 2", Descripcion = "Production plant 2" },

                // Códigos
                new Catalogo { Tipo = "Codigo", Valor = "COD-001", Descripcion = "Código general" },
                new Catalogo { Tipo = "Codigo", Valor = "COD-002", Descripcion = "Código de calidad" },
                new Catalogo { Tipo = "Codigo", Valor = "COD-003", Descripcion = "Código de seguridad" },
                new Catalogo { Tipo = "Codigo", Valor = "COD-004", Descripcion = "Código de producción" },
                new Catalogo { Tipo = "Codigo", Valor = "COD-005", Descripcion = "Código de mantenimiento" },
                new Catalogo { Tipo = "Codigo", Valor = "COD-006", Descripcion = "Código de logística" },
                new Catalogo { Tipo = "Codigo", Valor = "COD-007", Descripcion = "Código de inventario" },
                new Catalogo { Tipo = "Codigo", Valor = "COD-008", Descripcion = "Código de servicio" },

                // Catálogos para Revisiones (REV_)
                new Catalogo { Tipo = "REV_OBSERVACIONES", Valor = "Falla en equipo", Descripcion = "Observación de falla de equipo" },
                new Catalogo { Tipo = "REV_OBSERVACIONES", Valor = "Error humano", Descripcion = "Observación de error humano" },
                new Catalogo { Tipo = "REV_OBSERVACIONES", Valor = "Mantenimiento preventivo", Descripcion = "Observación de mantenimiento" },
                new Catalogo { Tipo = "REV_OBSERVACIONES", Valor = "Procedimiento incorrecto", Descripcion = "Observación de procedimiento" },
                new Catalogo { Tipo = "REV_OBSERVACIONES", Valor = "Condición ambiental", Descripcion = "Observación ambiental" },

                new Catalogo { Tipo = "REV_SE_DETECTO_INCIDENCIA", Valor = "Sí", Descripcion = "Incidencia detectada" },
                new Catalogo { Tipo = "REV_SE_DETECTO_INCIDENCIA", Valor = "No", Descripcion = "Incidencia no detectada" },
                new Catalogo { Tipo = "REV_SE_DETECTO_INCIDENCIA", Valor = "Parcial", Descripcion = "Incidencia parcialmente detectada" },

                new Catalogo { Tipo = "REV_AREA_CARGO", Valor = "Producción", Descripcion = "Area de producción" },
                new Catalogo { Tipo = "REV_AREA_CARGO", Valor = "Calidad", Descripcion = "Area de calidad" },
                new Catalogo { Tipo = "REV_AREA_CARGO", Valor = "Logística", Descripcion = "Area de logística" },
                new Catalogo { Tipo = "REV_AREA_CARGO", Valor = "Mantenimiento", Descripcion = "Area de mantenimiento" },
                new Catalogo { Tipo = "REV_AREA_CARGO", Valor = "Compras", Descripcion = "Area de compras" },

                new Catalogo { Tipo = "REV_FOLIO_ASIGNADO1", Valor = "F001", Descripcion = "Folio asignado 001" },
                new Catalogo { Tipo = "REV_FOLIO_ASIGNADO1", Valor = "F002", Descripcion = "Folio asignado 002" },
                new Catalogo { Tipo = "REV_FOLIO_ASIGNADO1", Valor = "F003", Descripcion = "Folio asignado 003" },
                new Catalogo { Tipo = "REV_FOLIO_ASIGNADO1", Valor = "F004", Descripcion = "Folio asignado 004" },
                new Catalogo { Tipo = "REV_FOLIO_ASIGNADO1", Valor = "F005", Descripcion = "Folio asignado 005" },

                new Catalogo { Tipo = "REV_AREA_SOLICITA", Valor = "Producción", Descripcion = "Area solicitante producción" },
                new Catalogo { Tipo = "REV_AREA_SOLICITA", Valor = "Mantenimiento", Descripcion = "Area solicitante mantenimiento" },
                new Catalogo { Tipo = "REV_AREA_SOLICITA", Valor = "TI", Descripcion = "Area solicitante TI" },
                new Catalogo { Tipo = "REV_AREA_SOLICITA", Valor = "Recursos Humanos", Descripcion = "Area solicitante RH" },
                new Catalogo { Tipo = "REV_AREA_SOLICITA", Valor = "Finanzas", Descripcion = "Area solicitante finanzas" },

                new Catalogo { Tipo = "REV_COMENTARIO_GENERAL", Valor = "Urgente", Descripcion = "Comentario urgente" },
                new Catalogo { Tipo = "REV_COMENTARIO_GENERAL", Valor = "Normal", Descripcion = "Comentario normal" },
                new Catalogo { Tipo = "REV_COMENTARIO_GENERAL", Valor = "Baja prioridad", Descripcion = "Comentario baja prioridad" },
                new Catalogo { Tipo = "REV_COMENTARIO_GENERAL", Valor = "Requiere seguimiento", Descripcion = "Comentario requiere seguimiento" },
                new Catalogo { Tipo = "REV_COMENTARIO_GENERAL", Valor = "Cerrado", Descripcion = "Comentario cerrado" },

                // Catálogos para Detecciones (DET_)
                new Catalogo { Tipo = "DET_FOLIO_ASIGNADO1", Valor = "D001", Descripcion = "Folio detección 001" },
                new Catalogo { Tipo = "DET_FOLIO_ASIGNADO1", Valor = "D002", Descripcion = "Folio detección 002" },
                new Catalogo { Tipo = "DET_FOLIO_ASIGNADO1", Valor = "D003", Descripcion = "Folio detección 003" },
                new Catalogo { Tipo = "DET_FOLIO_ASIGNADO1", Valor = "D004", Descripcion = "Folio detección 004" },
                new Catalogo { Tipo = "DET_FOLIO_ASIGNADO1", Valor = "D005", Descripcion = "Folio detección 005" },

                new Catalogo { Tipo = "DET_UBICACION_SUCURSAL", Valor = "Almacén A", Descripcion = "Ubicación almacén A" },
                new Catalogo { Tipo = "DET_UBICACION_SUCURSAL", Valor = "Oficina B", Descripcion = "Ubicación oficina B" },
                new Catalogo { Tipo = "DET_UBICACION_SUCURSAL", Valor = "Planta C", Descripcion = "Ubicación planta C" },
                new Catalogo { Tipo = "DET_UBICACION_SUCURSAL", Valor = "Taller D", Descripcion = "Ubicación taller D" },
                new Catalogo { Tipo = "DET_UBICACION_SUCURSAL", Valor = "Bodega E", Descripcion = "Ubicación bodega E" },

                new Catalogo { Tipo = "DET_PUESTO_COLABORADOR", Valor = "Operario", Descripcion = "Puesto operario" },
                new Catalogo { Tipo = "DET_PUESTO_COLABORADOR", Valor = "Supervisor", Descripcion = "Puesto supervisor" },
                new Catalogo { Tipo = "DET_PUESTO_COLABORADOR", Valor = "Técnico", Descripcion = "Puesto técnico" },
                new Catalogo { Tipo = "DET_PUESTO_COLABORADOR", Valor = "Analista", Descripcion = "Puesto analista" },
                new Catalogo { Tipo = "DET_PUESTO_COLABORADOR", Valor = "Coordinador", Descripcion = "Puesto coordinador" },

                // Catálogos adicionales para Detecciones
                new Catalogo { Tipo = "DET_LINEA_EMPRESA", Valor = "Línea 1", Descripcion = "Línea de producción 1" },
                new Catalogo { Tipo = "DET_LINEA_EMPRESA", Valor = "Línea 2", Descripcion = "Línea de producción 2" },
                new Catalogo { Tipo = "DET_LINEA_EMPRESA", Valor = "Línea 3", Descripcion = "Línea de producción 3" },
                new Catalogo { Tipo = "DET_LINEA_EMPRESA", Valor = "Empresa A", Descripcion = "Empresa asociada A" },
                new Catalogo { Tipo = "DET_LINEA_EMPRESA", Valor = "Empresa B", Descripcion = "Empresa asociada B" },

                new Catalogo { Tipo = "DET_AREA_ESPECIFICA", Valor = "Producción A", Descripcion = "Área específica producción A" },
                new Catalogo { Tipo = "DET_AREA_ESPECIFICA", Valor = "Producción B", Descripcion = "Área específica producción B" },
                new Catalogo { Tipo = "DET_AREA_ESPECIFICA", Valor = "Almacén Principal", Descripcion = "Área específica almacén" },
                new Catalogo { Tipo = "DET_AREA_ESPECIFICA", Valor = "Oficina Administrativa", Descripcion = "Área específica oficina" },
                new Catalogo { Tipo = "DET_AREA_ESPECIFICA", Valor = "Taller Mecánico", Descripcion = "Área específica taller" },

                new Catalogo { Tipo = "DET_TURNO_OPERATIVO", Valor = "Matutino", Descripcion = "Turno matutino" },
                new Catalogo { Tipo = "DET_TURNO_OPERATIVO", Valor = "Vespertino", Descripcion = "Turno vespertino" },
                new Catalogo { Tipo = "DET_TURNO_OPERATIVO", Valor = "Nocturno", Descripcion = "Turno nocturno" },
                new Catalogo { Tipo = "DET_TURNO_OPERATIVO", Valor = "Mixto", Descripcion = "Turno mixto" },
                new Catalogo { Tipo = "DET_TURNO_OPERATIVO", Valor = "Fin de Semana", Descripcion = "Turno fin de semana" },

                // Catálogos generales de ubicación
                new Catalogo { Tipo = "Ubicacion", Valor = "Almacén Central", Descripcion = "Ubicación almacén central" },
                new Catalogo { Tipo = "Ubicacion", Valor = "Andén carga", Descripcion = "Ubicación andén de carga" },
                new Catalogo { Tipo = "Ubicacion", Valor = "Andén descarga", Descripcion = "Ubicación andén de descarga" },
                new Catalogo { Tipo = "Ubicacion", Valor = "Estacionamiento", Descripcion = "Ubicación estacionamiento" },
                new Catalogo { Tipo = "Ubicacion", Valor = "Oficinas", Descripcion = "Ubicación oficinas" },

                // Catálogos para coordinadores de turno (detecciones)
                new Catalogo { Tipo = "DET_COORDINADOR_TURNO", Valor = "Coordinador 1", Descripcion = "Coordinador de turno 1" },
                new Catalogo { Tipo = "DET_COORDINADOR_TURNO", Valor = "Coordinador 2", Descripcion = "Coordinador de turno 2" },
                new Catalogo { Tipo = "DET_COORDINADOR_TURNO", Valor = "Coordinador 3", Descripcion = "Coordinador de turno 3" }
            };

            await context.Catalogos.AddRangeAsync(catalogos);
            await context.SaveChangesAsync();
        }
    }
}
