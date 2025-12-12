using System.Globalization;
using ExcelProcessorApi.Data;
using ExcelProcessorApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ExcelProcessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CapturaRevisionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CapturaRevisionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var revisiones = await _context.RevisionFolios
                .AsNoTracking()
                .Where(r => r.Activo)
                .OrderByDescending(r => r.FechaCreacion)
                .ToListAsync();

            var result = revisiones.Select(MapToResponse);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var revision = await _context.RevisionFolios
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id && r.Activo);

            if (revision == null)
            {
                return NotFound();
            }

            return Ok(MapToResponse(revision));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CapturaRevisionCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Datos inválidos.");
            }

            var contador = await ObtenerOCrearContadorHoy();
            var folios = GenerarFolios(contador);

            var revision = new RevisionFolio
            {
                Folio1 = folios.Folio1,
                Folio2 = folios.Folio2,
                Acumulado = folios.Acumulado,
                Almacen = dto.Almacen,
                Observaciones = dto.Titulo,
                ComentarioGeneral = dto.Titulo,
                QuienEnvia = dto.NombreCorreo,
                AreaSolicita = dto.AreaSolicita,
                Monitorista = dto.QuienRealiza,
                Situacion = string.IsNullOrWhiteSpace(dto.Estatus) ? "pendiente" : dto.Estatus,
                Ubicacion2 = dto.Ubicacion,
                FechaEnvio = ParseDate(dto.FechaRegistro),
                FechaIncidente = ParseDate(dto.FechaIncidente),
                FechaSolicitud = ParseDate(dto.FechaRegistro),
                CreadoPor = User.Identity?.Name ?? "captura-revisiones",
                FechaCreacion = DateTime.UtcNow
            };

            _context.RevisionFolios.Add(revision);
            contador.UltimaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var response = MapToResponse(revision);
            return CreatedAtAction(nameof(GetById), new { id = revision.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CapturaRevisionUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Datos inválidos.");
            }

            var revision = await _context.RevisionFolios.FirstOrDefaultAsync(r => r.Id == id && r.Activo);

            if (revision == null)
            {
                return NotFound();
            }

            bool hasChanges = false;

            if (dto.Titulo != null)
            {
                revision.Observaciones = dto.Titulo;
                revision.ComentarioGeneral = dto.Titulo;
                hasChanges = true;
            }

            if (dto.FechaRegistro != null)
            {
                revision.FechaEnvio = ParseDate(dto.FechaRegistro);
                revision.FechaSolicitud = revision.FechaEnvio;
                hasChanges = true;
            }

            if (dto.FechaIncidente != null)
            {
                revision.FechaIncidente = ParseDate(dto.FechaIncidente);
                hasChanges = true;
            }

            if (dto.Almacen != null)
            {
                revision.Almacen = dto.Almacen;
                hasChanges = true;
            }

            if (dto.Ubicacion != null)
            {
                revision.Ubicacion2 = dto.Ubicacion;
                hasChanges = true;
            }

            if (dto.NombreCorreo != null)
            {
                revision.QuienEnvia = dto.NombreCorreo;
                hasChanges = true;
            }

            if (dto.AreaSolicita != null)
            {
                revision.AreaSolicita = dto.AreaSolicita;
                hasChanges = true;
            }

            if (dto.QuienRealiza != null)
            {
                revision.Monitorista = dto.QuienRealiza;
                hasChanges = true;
            }

            if (dto.Estatus != null)
            {
                revision.Situacion = dto.Estatus;
                hasChanges = true;
            }

            if (!hasChanges)
            {
                return Ok(MapToResponse(revision));
            }

            revision.FechaActualizacion = DateTime.UtcNow;
            revision.ActualizadoPor = User.Identity?.Name ?? "captura-revisiones";

            await _context.SaveChangesAsync();
            return Ok(MapToResponse(revision));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var revision = await _context.RevisionFolios.FirstOrDefaultAsync(r => r.Id == id && r.Activo);

            if (revision == null)
            {
                return NotFound();
            }

            revision.Activo = false;
            revision.FechaActualizacion = DateTime.UtcNow;
            revision.ActualizadoPor = User.Identity?.Name ?? "captura-revisiones";

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private CapturaRevisionResponse MapToResponse(RevisionFolio revision)
        {
            var fechaRegistro = revision.FechaEnvio ?? revision.FechaCreacion;
            return new CapturaRevisionResponse
            {
                Id = revision.Id,
                Titulo = revision.Observaciones ?? revision.ComentarioGeneral ?? $"Folio {revision.Folio1}-{revision.Folio2}",
                FechaRegistro = fechaRegistro.ToString("yyyy-MM-dd"),
                FechaIncidente = revision.FechaIncidente?.ToString("yyyy-MM-dd"),
                Almacen = revision.Almacen ?? string.Empty,
                Ubicacion = revision.Ubicacion2 ?? revision.Almacen ?? string.Empty,
                NombreCorreo = revision.QuienEnvia ?? string.Empty,
                AreaSolicita = revision.AreaSolicita ?? string.Empty,
                QuienRealiza = revision.Monitorista ?? string.Empty,
                Estatus = string.IsNullOrWhiteSpace(revision.Situacion) ? "pendiente" : revision.Situacion
            };
        }

        private async Task<ContadorFolios> ObtenerOCrearContadorHoy()
        {
            var hoy = DateTime.Today;
            var contador = await _context.ContadorFolios.FirstOrDefaultAsync(c => c.Fecha == hoy);

            if (contador == null)
            {
                var ultimoContador = await _context.ContadorFolios
                    .OrderByDescending(c => c.Fecha)
                    .FirstOrDefaultAsync();

                contador = new ContadorFolios
                {
                    Fecha = hoy,
                    Folio2General = ultimoContador?.Folio2General ?? 0,
                    AcumuladoDiario = 0,
                    RevisionesCount = 0,
                    DeteccionesCount = 0,
                    UltimaActualizacion = DateTime.UtcNow
                };

                _context.ContadorFolios.Add(contador);
                await _context.SaveChangesAsync();
            }

            return contador;
        }

        private static (string Folio1, string Folio2, string Acumulado) GenerarFolios(ContadorFolios contador)
        {
            contador.Folio2General++;
            contador.AcumuladoDiario++;

            var folio1 = "001";
            var folio2 = contador.Folio2General.ToString("D3");
            var acumulado = contador.AcumuladoDiario.ToString("D3");

            return (folio1, folio2, acumulado);
        }

        private static DateTime? ParseDate(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var formats = new[]
            {
                "yyyy-MM-dd",
                "dd/MM/yyyy",
                "MM/dd/yyyy",
                "d/M/yyyy",
                "M/d/yyyy"
            };

            if (DateTime.TryParseExact(value.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed))
            {
                return parsed.Date;
            }

            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out parsed))
            {
                return parsed.Date;
            }

            return null;
        }
    }

    public sealed class CapturaRevisionResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [JsonPropertyName("fechaRegistro")]
        public string FechaRegistro { get; set; } = string.Empty;

        [JsonPropertyName("fechaIncidente")]
        public string? FechaIncidente { get; set; }

        [JsonPropertyName("almacen")]
        public string Almacen { get; set; } = string.Empty;

        [JsonPropertyName("ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;

        [JsonPropertyName("nombreCorreo")]
        public string NombreCorreo { get; set; } = string.Empty;

        [JsonPropertyName("areaSolicita")]
        public string AreaSolicita { get; set; } = string.Empty;

        [JsonPropertyName("quienRealiza")]
        public string QuienRealiza { get; set; } = string.Empty;

        [JsonPropertyName("estatus")]
        public string Estatus { get; set; } = "pendiente";
    }

    public sealed class CapturaRevisionCreateDto
    {
        [JsonPropertyName("titulo")]
        public string? Titulo { get; set; }

        [JsonPropertyName("fechaRegistro")]
        public string? FechaRegistro { get; set; }

        [JsonPropertyName("fechaIncidente")]
        public string? FechaIncidente { get; set; }

        [JsonPropertyName("almacen")]
        public string? Almacen { get; set; }

        [JsonPropertyName("ubicacion")]
        public string? Ubicacion { get; set; }

        [JsonPropertyName("nombreCorreo")]
        public string? NombreCorreo { get; set; }

        [JsonPropertyName("areaSolicita")]
        public string? AreaSolicita { get; set; }

        [JsonPropertyName("quienRealiza")]
        public string? QuienRealiza { get; set; }

        [JsonPropertyName("estatus")]
        public string? Estatus { get; set; }
    }

    public sealed class CapturaRevisionUpdateDto
    {
        [JsonPropertyName("titulo")]
        public string? Titulo { get; set; }

        [JsonPropertyName("fechaRegistro")]
        public string? FechaRegistro { get; set; }

        [JsonPropertyName("fechaIncidente")]
        public string? FechaIncidente { get; set; }

        [JsonPropertyName("almacen")]
        public string? Almacen { get; set; }

        [JsonPropertyName("ubicacion")]
        public string? Ubicacion { get; set; }

        [JsonPropertyName("nombreCorreo")]
        public string? NombreCorreo { get; set; }

        [JsonPropertyName("areaSolicita")]
        public string? AreaSolicita { get; set; }

        [JsonPropertyName("quienRealiza")]
        public string? QuienRealiza { get; set; }

        [JsonPropertyName("estatus")]
        public string? Estatus { get; set; }
    }
}
