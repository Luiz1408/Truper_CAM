using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelProcessorApi.Data;
using ExcelProcessorApi.Models;
using System.Text;

namespace ExcelProcessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FoliosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FoliosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("crear-folio")]
        public async Task<IActionResult> CrearFolio([FromBody] CrearFolioDto dto)
        {
            try
            {
                // Obtener el coordinador en turno (usuario actual)
                var coordinadorEnTurno = User.Identity.Name ?? "Sistema";

                // Obtener o crear contador del día
                var contadorHoy = await ObtenerOCrearContadorHoy();

                // Generar folios
                var folios = await GenerarFolios(contadorHoy, dto.Tipo);

                if (dto.Tipo == "revision")
                {
                    var revision = new RevisionFolio
                    {
                        Folio1 = folios.Folio1,
                        Folio2 = folios.Folio2,
                        Acumulado = folios.Acumulado,
                        
                        // Campos del catálogo
                        Almacen = dto.Almacen,
                        Observaciones = dto.Observaciones,
                        Indicador = dto.Indicador,
                        Subindicador = dto.Subindicador,
                        SeDetectoIncidenciaReportada = dto.SeDetectoIncidenciaReportada,
                        AreaCargo = dto.AreaCargo,
                        AreaSolicita = dto.AreaSolicita,
                        Monitorista = dto.Monitorista,
                        Puesto = dto.Puesto,
                        ComentarioGeneral = dto.ComentarioGeneral,
                        CoordinadorEnTurno = coordinadorEnTurno,
                        
                        // Campos adicionales
                        FechaEnvio = dto.FechaEnvio,
                        Mes = dto.Mes,
                        FechaSolicitud = dto.FechaSolicitud,
                        FechaIncidente = dto.FechaIncidente,
                        Hora = dto.Hora,
                        Monto = dto.Monto,
                        Codigo = dto.Codigo,
                        Tiempo = dto.Tiempo,
                        Ticket = dto.Ticket,
                        FoliosAsignado1 = dto.FoliosAsignado1,
                        FoliosAsignado2 = dto.FoliosAsignado2,
                        PersonalInvolucrado = dto.PersonalInvolucrado,
                        No = dto.No,
                        Nomina = dto.Nomina,
                        LineaEmpresaPlacas = dto.LineaEmpresaPlacas,
                        Ubicacion2 = dto.Ubicacion2,
                        AreaEspecifica = dto.AreaEspecifica,
                        TurnoOperativo = dto.TurnoOperativo,
                        Situacion = dto.Situacion,
                        QuienEnvia = dto.QuienEnvia,
                        
                        CreadoPor = coordinadorEnTurno
                    };

                    _context.RevisionFolios.Add(revision);
                    contadorHoy.RevisionesCount++;
                }
                else if (dto.Tipo == "deteccion")
                {
                    var deteccion = new DeteccionFolio
                    {
                        Folio1 = folios.Folio1,
                        Folio2 = folios.Folio2,
                        Acumulado = folios.Acumulado,
                        
                        // Campos del catálogo
                        Sucursal = dto.Sucursal,
                        Codigo = dto.Codigo,
                        Indicador = dto.Indicador,
                        Subindicador = dto.Subindicador,
                        FolioAsignado1 = dto.FolioAsignado1,
                        UbicacionSucursal = dto.UbicacionSucursal,
                        Monitorista = dto.Monitorista,
                        Puesto = dto.Puesto,
                        PuestoColaborador = dto.PuestoColaborador,
                        CoordinadorEnTurno = coordinadorEnTurno,
                        
                        // Campos adicionales
                        FechaEnvio = dto.FechaEnvio,
                        Ubicacion = dto.Ubicacion,
                        Almacen = dto.Almacen,
                        Hora = dto.Hora,
                        GeneraImpacto = dto.GeneraImpacto,
                        FolioAsignado2 = dto.FolioAsignado2,
                        ColaboradorInvolucrado = dto.ColaboradorInvolucrado,
                        No = dto.No,
                        Nomina = dto.Nomina,
                        LineaEmpresa = dto.LineaEmpresa,
                        AreaEspecifica = dto.AreaEspecifica,
                        TurnoOperativo = dto.TurnoOperativo,
                        SupervisorJefeTurno = dto.SupervisorJefeTurno,
                        SituacionDescripcion = dto.SituacionDescripcion,
                        EnviaReporte = dto.EnviaReporte,
                        Retroalimentacion = dto.Retroalimentacion,
                        
                        CreadoPor = coordinadorEnTurno
                    };

                    _context.DeteccionFolios.Add(deteccion);
                    contadorHoy.DeteccionesCount++;
                }
                else
                {
                    return BadRequest(new { message = "Tipo de folio no válido" });
                }

                // Actualizar contadores
                contadorHoy.UltimaActualizacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { 
                    message = "Folio creado exitosamente",
                    folio = new
                    {
                        tipo = dto.Tipo,
                        folio1 = folios.Folio1,
                        folio2 = folios.Folio2,
                        acumulado = folios.Acumulado
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear folio", error = ex.Message });
            }
        }

        [HttpGet("revisiones")]
        public async Task<IActionResult> GetRevisiones()
        {
            try
            {
                var revisiones = await _context.RevisionFolios
                    .Where(r => r.Activo)
                    .OrderByDescending(r => r.FechaCreacion)
                    .ToListAsync();

                return Ok(revisiones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener revisiones", error = ex.Message });
            }
        }

        [HttpGet("detecciones")]
        public async Task<IActionResult> GetDetecciones()
        {
            try
            {
                var detecciones = await _context.DeteccionFolios
                    .Where(d => d.Activo)
                    .OrderByDescending(d => d.FechaCreacion)
                    .ToListAsync();

                return Ok(detecciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener detecciones", error = ex.Message });
            }
        }

        [HttpGet("siguiente-folio/{tipo}")]
        public async Task<IActionResult> GetSiguienteFolio(string tipo)
        {
            try
            {
                var contador = await ObtenerOCrearContadorHoy();
                
                // Simular el siguiente folio sin incrementar contadores
                var siguienteFolio1 = "001"; // Mañana se obtiene de catálogo
                var siguienteFolio2 = (contador.Folio2General + 1).ToString("D3"); // Continuo para ambos
                var siguienteAcumulado = (contador.AcumuladoDiario + 1).ToString("D3"); // Reinicia diario

                return Ok(new {
                    folio1 = siguienteFolio1,
                    folio2 = siguienteFolio2,
                    acumulado = siguienteAcumulado
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener siguiente folio", error = ex.Message });
            }
        }

        private async Task<ContadorFolios> ObtenerOCrearContadorHoy()
        {
            var hoy = DateTime.Today;
            var contador = await _context.ContadorFolios
                .FirstOrDefaultAsync(c => c.Fecha == hoy);

            if (contador == null)
            {
                // Es un nuevo día, reiniciar solo acumulado
                var ultimoContador = await _context.ContadorFolios
                    .OrderByDescending(c => c.Fecha)
                    .FirstOrDefaultAsync();

                contador = new ContadorFolios
                {
                    Fecha = hoy,
                    Folio2General = ultimoContador?.Folio2General ?? 0, // Continuo, no se reinicia
                    AcumuladoDiario = 0, // Se reinicia diario
                    RevisionesCount = 0, // No se usará más
                    DeteccionesCount = 0 // No se usará más
                };

                _context.ContadorFolios.Add(contador);
                await _context.SaveChangesAsync();
            }

            return contador;
        }

        private async Task<(string Folio1, string Folio2, string Acumulado)> GenerarFolios(ContadorFolios contador, string tipo)
        {
            // Incrementar ambos contadores (aplica para ambos tipos)
            contador.Folio2General++; // Continuo
            contador.AcumuladoDiario++; // Reinicia diario

            var folio1 = "001"; // Mañana se obtiene de catálogo
            var folio2 = contador.Folio2General.ToString("D3"); // Continuo para ambos tipos
            var acumulado = contador.AcumuladoDiario.ToString("D3"); // Reinicia diario para ambos tipos

            return (folio1, folio2, acumulado);
        }
    }

    // DTOs para crear folios
    public class CrearFolioDto
    {
        public string Tipo { get; set; } = string.Empty;

        // Campos comunes
        public DateTime? FechaEnvio { get; set; }
        public TimeSpan? Hora { get; set; }
        public string? Indicador { get; set; }
        public string? Subindicador { get; set; }
        public string? Monitorista { get; set; }
        public string? Puesto { get; set; }

        // Campos específicos de Revisiones
        public string? Almacen { get; set; }
        public string? Observaciones { get; set; }
        public string? SeDetectoIncidenciaReportada { get; set; }
        public string? AreaCargo { get; set; }
        public string? AreaSolicita { get; set; }
        public string? ComentarioGeneral { get; set; }
        public string? Mes { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaIncidente { get; set; }
        public string? Monto { get; set; }
        public string? Codigo { get; set; }
        public string? Tiempo { get; set; }
        public string? Ticket { get; set; }
        public string? FoliosAsignado1 { get; set; }
        public string? FoliosAsignado2 { get; set; }
        public string? PersonalInvolucrado { get; set; }
        public string? No { get; set; }
        public string? Nomina { get; set; }
        public string? LineaEmpresaPlacas { get; set; }
        public string? Ubicacion2 { get; set; }
        public string? AreaEspecifica { get; set; }
        public string? TurnoOperativo { get; set; }
        public string? Situacion { get; set; }
        public string? QuienEnvia { get; set; }

        // Campos específicos de Detecciones
        public string? Sucursal { get; set; }
        public string? FolioAsignado1 { get; set; }
        public string? UbicacionSucursal { get; set; }
        public string? PuestoColaborador { get; set; }
        public string? GeneraImpacto { get; set; }
        public string? FolioAsignado2 { get; set; }
        public string? ColaboradorInvolucrado { get; set; }
        public string? LineaEmpresa { get; set; }
        public string? SupervisorJefeTurno { get; set; }
        public string? SituacionDescripcion { get; set; }
        public string? EnviaReporte { get; set; }
        public string? Retroalimentacion { get; set; }
        public string? Ubicacion { get; set; }
        public string? AlmacenDeteccion { get; set; }
    }
}
