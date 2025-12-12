using System.ComponentModel.DataAnnotations;

namespace ExcelProcessorApi.Models
{
    public class ContadorFolios
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required]
        public int Folio2General { get; set; } = 0; // Contador general (no reinicia)

        [Required]
        public int AcumuladoDiario { get; set; } = 0; // Contador diario (reinicia cada día)

        [Required]
        public int RevisionesCount { get; set; } = 0; // Contador de revisiones del día

        [Required]
        public int DeteccionesCount { get; set; } = 0; // Contador de detecciones del día

        public DateTime UltimaActualizacion { get; set; } = DateTime.UtcNow;
    }
}
