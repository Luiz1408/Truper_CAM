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
    [Authorize(Roles = $"{RoleNames.Administrator},{RoleNames.Coordinator}")]
    public class ShiftHandOffController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShiftHandOffController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            var notes = await _context.ShiftHandOffNotes
                .Include(n => n.CreatedByUser)
                .Include(n => n.FinalizedByUser)
                .Include(n => n.Acknowledgements)
                    .ThenInclude(a => a.CoordinatorUser)
                .AsNoTracking()
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            var response = notes.Select(MapNoteToResponse);

            return Ok(new { notes = response });
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] UpsertShiftHandOffNoteDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Datos inválidos" });
            }

            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado" });
            }

            var note = new ShiftHandOffNote
            {
                Description = dto.Description?.Trim() ?? string.Empty,
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "Pendiente" : dto.Status.Trim(),
                AssignedCoordinatorId = dto.AssignedCoordinatorId,
                CreatedByUserId = currentUser.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (dto.Acknowledgements?.Count > 0)
            {
                foreach (var acknowledgementDto in dto.Acknowledgements)
                {
                    var coordinator = await _context.Users
                        .FirstOrDefaultAsync(u => u.Id == acknowledgementDto.CoordinatorUserId && u.IsActive);

                    if (coordinator == null)
                    {
                        continue;
                    }

                    note.Acknowledgements.Add(new ShiftHandOffAcknowledgement
                    {
                        CoordinatorUserId = acknowledgementDto.CoordinatorUserId,
                        IsAcknowledged = acknowledgementDto.IsAcknowledged,
                        AcknowledgedAt = acknowledgementDto.IsAcknowledged ? DateTime.UtcNow : null,
                        UpdatedByUserId = currentUser.Id
                    });
                }
            }

            _context.ShiftHandOffNotes.Add(note);
            await _context.SaveChangesAsync();

            var createdNote = await _context.ShiftHandOffNotes
                .Include(n => n.CreatedByUser)
                .Include(n => n.FinalizedByUser)
                .Include(n => n.Acknowledgements)
                    .ThenInclude(a => a.CoordinatorUser)
                .AsNoTracking()
                .FirstAsync(n => n.Id == note.Id);

            return Ok(new
            {
                note = MapNoteToResponse(createdNote)
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleNames.Administrator + "," + RoleNames.Coordinator)]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpsertShiftHandOffNoteDto dto)
        {
            var note = await _context.ShiftHandOffNotes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null)
            {
                return NotFound(new { message = "Nota no encontrada" });
            }

            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado" });
            }

            var isAdmin = string.Equals(currentUser.Role?.Name, RoleNames.Administrator, StringComparison.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                if (!isAdmin)
                {
                    return Forbid();
                }

                note.Description = dto.Description.Trim();
            }

            if (!string.IsNullOrWhiteSpace(dto.Status))
            {
                var trimmedStatus = dto.Status.Trim();
                note.Status = trimmedStatus;

                if (string.Equals(trimmedStatus, "Finalizado", StringComparison.OrdinalIgnoreCase))
                {
                    note.FinalizedByUserId = currentUser.Id;
                    note.FinalizedAt = DateTime.UtcNow;
                }
                else
                {
                    note.FinalizedByUserId = null;
                    note.FinalizedAt = null;
                }
            }

            if (dto.AssignedCoordinatorId.HasValue)
            {
                if (!isAdmin)
                {
                    return Forbid();
                }

                var coordinatorExists = await _context.Users.AnyAsync(u => u.Id == dto.AssignedCoordinatorId.Value && u.IsActive);
                if (!coordinatorExists)
                {
                    return BadRequest(new { message = "Coordinador inválido" });
                }

                note.AssignedCoordinatorId = dto.AssignedCoordinatorId;
            }

            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedNote = await _context.ShiftHandOffNotes
                .Include(n => n.CreatedByUser)
                .Include(n => n.FinalizedByUser)
                .Include(n => n.Acknowledgements)
                    .ThenInclude(a => a.CoordinatorUser)
                .AsNoTracking()
                .FirstAsync(n => n.Id == note.Id);

            return Ok(new { note = MapNoteToResponse(updatedNote) });
        }

        [HttpPut("{id}/acknowledgements")]
        public async Task<IActionResult> ToggleAcknowledgement(int id, [FromBody] ShiftHandOffAcknowledgementToggleDto dto)
        {
            var note = await _context.ShiftHandOffNotes
                .Include(n => n.Acknowledgements)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                return NotFound(new { message = "Nota no encontrada" });
            }

            var currentUser = await GetCurrentUserAsync();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Usuario no autenticado" });
            }

            var targetCoordinatorId = currentUser.Role?.Name == RoleNames.Administrator
                ? dto.CoordinatorUserId ?? currentUser.Id
                : currentUser.Id;

            if (currentUser.Role?.Name != RoleNames.Administrator)
            {
                var isCoordinator = await _context.Users.AnyAsync(u => u.Id == currentUser.Id && u.Role.Name == RoleNames.Coordinator);
                if (!isCoordinator)
                {
                    return Forbid();
                }
            }

            var acknowledgement = note.Acknowledgements.FirstOrDefault(a => a.CoordinatorUserId == targetCoordinatorId);
            if (acknowledgement == null)
            {
                acknowledgement = new ShiftHandOffAcknowledgement
                {
                    NoteId = note.Id,
                    CoordinatorUserId = targetCoordinatorId
                };

                note.Acknowledgements.Add(acknowledgement);
            }

            acknowledgement.IsAcknowledged = dto.IsAcknowledged;
            acknowledgement.AcknowledgedAt = dto.IsAcknowledged ? DateTime.UtcNow : null;
            acknowledgement.UpdatedByUserId = currentUser.Id;
            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedAcknowledgement = await _context.ShiftHandOffAcknowledgements
                .Include(a => a.CoordinatorUser)
                .AsNoTracking()
                .FirstAsync(a => a.Id == acknowledgement.Id);

            return Ok(new
            {
                acknowledgement = new
                {
                    updatedAcknowledgement.NoteId,
                    updatedAcknowledgement.CoordinatorUserId,
                    CoordinatorName = updatedAcknowledgement.CoordinatorUser != null
                        ? ($"{updatedAcknowledgement.CoordinatorUser.FirstName} {updatedAcknowledgement.CoordinatorUser.LastName}").Trim()
                        : string.Empty,
                    CoordinatorUsername = updatedAcknowledgement.CoordinatorUser?.Username ?? string.Empty,
                    updatedAcknowledgement.IsAcknowledged,
                    updatedAcknowledgement.AcknowledgedAt
                }
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleNames.Administrator)]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.ShiftHandOffNotes
                .Include(n => n.Acknowledgements)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                return NotFound(new { message = "Nota no encontrada" });
            }

            _context.ShiftHandOffNotes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Nota eliminada" });
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var usernameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrWhiteSpace(usernameClaim))
            {
                return null;
            }

            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == usernameClaim && u.IsActive);
        }

        private static object MapNoteToResponse(ShiftHandOffNote note)
        {
            return new
            {
                note.Id,
                note.Description,
                note.Status,
                note.AssignedCoordinatorId,
                CreatedAt = EnsureUtc(note.CreatedAt),
                UpdatedAt = EnsureUtc(note.UpdatedAt),
                FinalizedAt = EnsureUtc(note.FinalizedAt),
                FinalizedBy = note.FinalizedByUser != null
                    ? new
                    {
                        note.FinalizedByUser.Id,
                        FullName = ($"{note.FinalizedByUser.FirstName} {note.FinalizedByUser.LastName}").Trim(),
                        note.FinalizedByUser.Username
                    }
                    : null,
                CreatedBy = note.CreatedByUser != null
                    ? new
                    {
                        note.CreatedByUser.Id,
                        FullName = ($"{note.CreatedByUser.FirstName} {note.CreatedByUser.LastName}").Trim(),
                        note.CreatedByUser.Username
                    }
                    : null,
                acknowledgements = note.Acknowledgements.Select(a => new
                {
                    a.CoordinatorUserId,
                    CoordinatorName = a.CoordinatorUser != null
                        ? ($"{a.CoordinatorUser.FirstName} {a.CoordinatorUser.LastName}").Trim()
                        : string.Empty,
                    CoordinatorUsername = a.CoordinatorUser?.Username ?? string.Empty,
                    a.IsAcknowledged,
                    AcknowledgedAt = EnsureUtc(a.AcknowledgedAt)
                })
            };
        }

        private static DateTime EnsureUtc(DateTime value)
        {
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private static DateTime? EnsureUtc(DateTime? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            return DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
        }
    }
}
