using System.ComponentModel.DataAnnotations;

namespace ExcelProcessorApi.Models
{
    public class AlmacenUbicacionFolio
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Almacen { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string FolioAsignado1 { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Ubicacion { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaActualizacion { get; set; }

        [StringLength(100)]
        public string? CreadoPor { get; set; }

        [StringLength(100)]
        public string? ActualizadoPor { get; set; }
    }
}
