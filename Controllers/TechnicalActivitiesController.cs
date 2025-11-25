using System.Security.Claims;
using ExcelProcessorApi.Data;
using ExcelProcessorApi.Models;
using ExcelProcessorApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExcelProcessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TechnicalActivitiesController : ControllerBase
    {
        private static readonly HashSet<string> AllowedStatuses = new(StringComparer.OrdinalIgnoreCase)
        {
            "Pendiente",
            "Finalizada",
            "No realizada"
        };

        private readonly ApplicationDbContext _context;

        public TechnicalActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _context.TechnicalActivities
                .Include(a => a.CreatedByUser)
                .Include(a => a.UpdatedByUser)
                .OrderByDescending(a => a.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

            var response = activities.Select(MapToDto);
            return Ok(new { activities = response });
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var total = await _context.TechnicalActivities.CountAsync();
            var pending = await _context.TechnicalActivities.CountAsync(a => a.Status == "Pendiente");
            var completed = await _context.TechnicalActivities.CountAsync(a => a.Status == "Finalizada");
            var notCompleted = await _context.TechnicalActivities.CountAsync(a => a.Status == "No realizada");

            var summary = new TechnicalActivitySummaryDto
            {
                Total = total,
                Pending = pending,
                Completed = completed,
                NotCompleted = notCompleted,
            };

            return Ok(summary);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Administrator + "," + RoleNames.Technician)]
        public async Task<IActionResult> CreateActivity([FromBody] CreateTechnicalActivityDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado" });
            }

            var activity = new TechnicalActivity
            {
                Description = dto.Description.Trim(),
                Notes = dto.Notes?.Trim(),
                Status = "Pendiente",
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedByUserId = currentUser.Id,
                UpdatedByUserId = currentUser.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _context.TechnicalActivities.Add(activity);
            await _context.SaveChangesAsync();

            await _context.Entry(activity).Reference(a => a.CreatedByUser).LoadAsync();
            await _context.Entry(activity).Reference(a => a.UpdatedByUser).LoadAsync();

            return CreatedAtAction(nameof(GetActivities), new { id = activity.Id }, new { activity = MapToDto(activity) });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleNames.Administrator + "," + RoleNames.Technician)]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateTechnicalActivityDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = await _context.TechnicalActivities.FirstOrDefaultAsync(a => a.Id == id);
            if (activity == null)
            {
                return NotFound(new { message = "Actividad no encontrada" });
            }

            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado" });
            }

            if (!string.IsNullOrWhiteSpace(dto.Status))
            {
                var trimmedStatus = dto.Status.Trim();
                if (!AllowedStatuses.Contains(trimmedStatus))
                {
                    return BadRequest(new { message = "Estatus inválido" });
                }

                activity.Status = trimmedStatus;
                activity.CompletedAt = string.Equals(trimmedStatus, "Finalizada", StringComparison.OrdinalIgnoreCase)
                    ? DateTime.UtcNow
                    : null;
            }

            if (dto.Notes != null)
            {
                activity.Notes = dto.Notes.Trim();
            }

            activity.UpdatedAt = DateTime.UtcNow;
            activity.UpdatedByUserId = currentUser.Id;

            await _context.SaveChangesAsync();

            await _context.Entry(activity).Reference(a => a.CreatedByUser).LoadAsync();
            await _context.Entry(activity).Reference(a => a.UpdatedByUser).LoadAsync();

            return Ok(new { activity = MapToDto(activity) });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleNames.Administrator)]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.TechnicalActivities.FirstOrDefaultAsync(a => a.Id == id);
            if (activity == null)
            {
                return NotFound(new { message = "Actividad no encontrada" });
            }

            _context.TechnicalActivities.Remove(activity);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Actividad eliminada" });
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        }

        private TechnicalActivityDto MapToDto(TechnicalActivity activity)
        {
            return new TechnicalActivityDto
            {
                Id = activity.Id,
                Description = activity.Description,
                Status = activity.Status,
                Notes = activity.Notes,
                CreatedAt = activity.CreatedAt,
                UpdatedAt = activity.UpdatedAt,
                CompletedAt = activity.CompletedAt,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                CreatedBy = activity.CreatedByUser != null
                    ? new BasicUserDto
                    {
                        Id = activity.CreatedByUser.Id,
                        FullName = ($"{activity.CreatedByUser.FirstName} {activity.CreatedByUser.LastName}").Trim(),
                        Username = activity.CreatedByUser.Username
                    }
                    : null,
                UpdatedBy = activity.UpdatedByUser != null
                    ? new BasicUserDto
                    {
                        Id = activity.UpdatedByUser.Id,
                        FullName = ($"{activity.UpdatedByUser.FirstName} {activity.UpdatedByUser.LastName}").Trim(),
                        Username = activity.UpdatedByUser.Username
                    }
                    : null
            };
        }
    }
}
