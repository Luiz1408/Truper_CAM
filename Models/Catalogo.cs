using System.ComponentModel.DataAnnotations;

namespace ExcelProcessorApi.Models
{
    public class Catalogo
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public required string Tipo { get; set; } // "Monitorista", "Sucursal", "Almacen", etc.
        
        [Required]
        [StringLength(200)]
        public required string Valor { get; set; } // El valor del catálogo
        
        [StringLength(500)]
        public string? Descripcion { get; set; }
        
        public bool Activo { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public DateTime? FechaActualizacion { get; set; }
        
        public string? CreadoPor { get; set; }
        
        public string? ActualizadoPor { get; set; }
    }
}
