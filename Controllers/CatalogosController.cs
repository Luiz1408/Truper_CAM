using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelProcessorApi.Data;
using ExcelProcessorApi.Models;

namespace ExcelProcessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CatalogosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatalogosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{tipo}")]
        public async Task<IActionResult> GetCatalogoByTipo(string tipo)
        {
            try
            {
                var catalogos = await _context.Catalogos
                    .Where(c => c.Tipo == tipo && c.Activo)
                    .OrderBy(c => c.Valor)
                    .Select(c => new
                    {
                        c.Id,
                        c.Valor,
                        c.Descripcion
                    })
                    .ToListAsync();

                return Ok(catalogos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener catálogo", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTipos()
        {
            try
            {
                var tipos = await _context.Catalogos
                    .Where(c => c.Activo)
                    .GroupBy(c => c.Tipo)
                    .Select(g => new
                    {
                        Tipo = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(t => t.Tipo)
                    .ToListAsync();

                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener tipos de catálogos", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatalogo([FromBody] CatalogoCreateDto dto)
        {
            try
            {
                // Verificar si ya existe
                var existe = await _context.Catalogos
                    .AnyAsync(c => c.Tipo == dto.Tipo && c.Valor == dto.Valor);

                if (existe)
                {
                    return BadRequest(new { message = "Ya existe este valor en este catálogo" });
                }

                var catalogo = new Catalogo
                {
                    Tipo = dto.Tipo,
                    Valor = dto.Valor,
                    Descripcion = dto.Descripcion,
                    Activo = true,
                    CreadoPor = User.Identity.Name
                };

                _context.Catalogos.Add(catalogo);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Catálogo creado exitosamente", id = catalogo.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear catálogo", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatalogo(int id, [FromBody] CatalogoUpdateDto dto)
        {
            try
            {
                var catalogo = await _context.Catalogos.FindAsync(id);
                if (catalogo == null)
                {
                    return NotFound(new { message = "Catálogo no encontrado" });
                }

                // Verificar si ya existe otro con el mismo valor
                var existe = await _context.Catalogos
                    .AnyAsync(c => c.Tipo == catalogo.Tipo && c.Valor == dto.Valor && c.Id != id);

                if (existe)
                {
                    return BadRequest(new { message = "Ya existe este valor en este catálogo" });
                }

                catalogo.Valor = dto.Valor;
                catalogo.Descripcion = dto.Descripcion;
                catalogo.ActualizadoPor = User.Identity.Name;
                catalogo.FechaActualizacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Catálogo actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar catálogo", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(int id)
        {
            try
            {
                var catalogo = await _context.Catalogos.FindAsync(id);
                if (catalogo == null)
                {
                    return NotFound(new { message = "Catálogo no encontrado" });
                }

                catalogo.Activo = false;
                catalogo.ActualizadoPor = User.Identity.Name;
                catalogo.FechaActualizacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Catálogo desactivado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al desactivar catálogo", error = ex.Message });
            }
        }
    }

    public class CatalogoCreateDto
    {
        public string Tipo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }

    public class CatalogoUpdateDto
    {
        public string Valor { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}
