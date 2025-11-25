using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelProcessorApi.Data;
using ExcelProcessorApi.Models;
using ExcelProcessorApi.Models.DTOs;
using ExcelProcessorApi.Services;
using BCrypt.Net;

namespace ExcelProcessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.IsActive);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                {
                    return Unauthorized(new { message = "Credenciales inválidas" });
                }

                // Update last login
                user.LastLogin = DateTime.Now;
                await _context.SaveChangesAsync();

                var token = _jwtService.GenerateToken(user.Username, user.Role?.Name ?? "User");
                var expiresAt = DateTime.UtcNow.AddHours(24);

                var response = new AuthResponseDto
                {
                    Token = token,
                    Username = user.Username,
                    FullName = $"{user.FirstName} {user.LastName}",
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role?.Name ?? "User",
                    ExpiresAt = expiresAt
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error en el login: {ex.Message}" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                // Check if username already exists
                if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                {
                    return BadRequest(new { message = "El nombre de usuario ya existe" });
                }

                // Validate role exists
                var role = await _context.Roles.FindAsync(registerDto.RoleId);
                if (role == null)
                {
                    return BadRequest(new { message = "Rol inválido" });
                }

                var user = new User
                {
                    Username = registerDto.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    RoleId = registerDto.RoleId,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Load the role for response
                await _context.Entry(user).Reference(u => u.Role).LoadAsync();

var token = _jwtService.GenerateToken(user.Username, user.Role?.Name ?? "User");
                var expiresAt = DateTime.UtcNow.AddHours(24);

                var response = new AuthResponseDto
                {
                    Token = token,
                    Username = user.Username,
                    FullName = $"{user.FirstName} {user.LastName}",
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role?.Name ?? "User",
                    ExpiresAt = expiresAt
                };

                return Ok(new { message = "Usuario registrado exitosamente", user = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error en el registro: {ex.Message}" });
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _context.Roles
                    .Select(r => new { r.Id, r.Name, r.Description })
                    .ToListAsync();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener roles: {ex.Message}" });
            }
        }
    }
}
