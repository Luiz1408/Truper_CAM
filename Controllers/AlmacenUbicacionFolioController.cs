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
    public class AlmacenUbicacionFolioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlmacenUbicacionFolioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var almacenes = await _context.AlmacenUbicacionFolios
                    .Where(a => a.Activo)
                    .OrderBy(a => a.Almacen)
                    .ToListAsync();

                return Ok(almacenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener almacenes", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var almacen = await _context.AlmacenUbicacionFolios
                    .FirstOrDefaultAsync(a => a.Id == id && a.Activo);

                if (almacen == null)
                {
                    return NotFound(new { message = "Almacén no encontrado" });
                }

                return Ok(almacen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener almacén", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAlmacenUbicacionFolioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar si ya existe el almacén
                var existente = await _context.AlmacenUbicacionFolios
                    .FirstOrDefaultAsync(a => a.Almacen.ToLower() == dto.Almacen.ToLower() && a.Activo);

                if (existente != null)
                {
                    return BadRequest(new { message = "Ya existe un almacén con ese nombre" });
                }

                var almacen = new AlmacenUbicacionFolio
                {
                    Almacen = dto.Almacen,
                    FolioAsignado1 = dto.FolioAsignado1,
                    Ubicacion = dto.Ubicacion,
                    Activo = true,
                    CreadoPor = User.Identity.Name
                };

                _context.AlmacenUbicacionFolios.Add(almacen);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = almacen.Id }, almacen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear almacén", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAlmacenUbicacionFolioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var almacen = await _context.AlmacenUbicacionFolios
                    .FirstOrDefaultAsync(a => a.Id == id && a.Activo);

                if (almacen == null)
                {
                    return NotFound(new { message = "Almacén no encontrado" });
                }

                // Verificar si ya existe otro almacén con el mismo nombre
                var existente = await _context.AlmacenUbicacionFolios
                    .FirstOrDefaultAsync(a => a.Almacen.ToLower() == dto.Almacen.ToLower() && 
                                           a.Id != id && a.Activo);

                if (existente != null)
                {
                    return BadRequest(new { message = "Ya existe otro almacén con ese nombre" });
                }

                almacen.Almacen = dto.Almacen;
                almacen.FolioAsignado1 = dto.FolioAsignado1;
                almacen.Ubicacion = dto.Ubicacion;
                almacen.FechaActualizacion = DateTime.UtcNow;
                almacen.ActualizadoPor = User.Identity.Name;

                await _context.SaveChangesAsync();

                return Ok(almacen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar almacén", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacen = await _context.AlmacenUbicacionFolios
                    .FirstOrDefaultAsync(a => a.Id == id && a.Activo);

                if (almacen == null)
                {
                    return NotFound(new { message = "Almacén no encontrado" });
                }

                almacen.Activo = false;
                almacen.FechaActualizacion = DateTime.UtcNow;
                almacen.ActualizadoPor = User.Identity.Name;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Almacén eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar almacén", error = ex.Message });
            }
        }

        [HttpPost("seed-data")]
        [AllowAnonymous]
        public async Task<IActionResult> SeedData()
        {
            try
            {
                // Verificar si ya existen datos
                if (await _context.AlmacenUbicacionFolios.AnyAsync())
                {
                    return Ok(new { message = "Los datos de ejemplo ya existen" });
                }

                var almacenes = new List<AlmacenUbicacionFolio>
                {
                    new AlmacenUbicacionFolio
                    {
                        Almacen = "Tuxtla",
                        FolioAsignado1 = "STUX",
                        Ubicacion = "Sucursal",
                        Activo = true,
                        CreadoPor = "System"
                    },
                    new AlmacenUbicacionFolio
                    {
                        Almacen = "Tapachula",
                        FolioAsignado1 = "STAP",
                        Ubicacion = "Sucursal Tapachula",
                        Activo = true,
                        CreadoPor = "System"
                    },
                    new AlmacenUbicacionFolio
                    {
                        Almacen = "San Cristóbal",
                        FolioAsignado1 = "STSC",
                        Ubicacion = "Sucursal San Cristóbal",
                        Activo = true,
                        CreadoPor = "System"
                    },
                    new AlmacenUbicacionFolio
                    {
                        Almacen = "Comitán",
                        FolioAsignado1 = "STCM",
                        Ubicacion = "Sucursal Comitán",
                        Activo = true,
                        CreadoPor = "System"
                    }
                };

                await _context.AlmacenUbicacionFolios.AddRangeAsync(almacenes);
                await _context.SaveChangesAsync();

                return Ok(new { 
                    message = "Datos de ejemplo creados correctamente",
                    cantidad = almacenes.Count,
                    datos = almacenes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creando datos de ejemplo", error = ex.Message });
            }
        }
    }

    // DTOs
    public class CreateAlmacenUbicacionFolioDto
    {
        public string Almacen { get; set; } = string.Empty;
        public string FolioAsignado1 { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
    }

    public class UpdateAlmacenUbicacionFolioDto
    {
        public string Almacen { get; set; } = string.Empty;
        public string FolioAsignado1 { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
    }
}
