using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelProcessorApi.Models
{
    public class RevisionFolio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Folio1 { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Folio2 { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Acumulado { get; set; } = string.Empty;

        // Campos del catálogo
        [StringLength(200)]
        public string? Almacen { get; set; }

        [StringLength(1000)]
        public string? Observaciones { get; set; }

        [StringLength(200)]
        public string? Indicador { get; set; }

        [StringLength(200)]
        public string? Subindicador { get; set; }

        [StringLength(500)]
        public string? SeDetectoIncidenciaReportada { get; set; }

        [StringLength(200)]
        public string? AreaCargo { get; set; }

        [StringLength(200)]
        public string? AreaSolicita { get; set; }

        [StringLength(200)]
        public string? Monitorista { get; set; }

        [StringLength(200)]
        public string? Puesto { get; set; }

        [StringLength(1000)]
        public string? ComentarioGeneral { get; set; }

        // Coordinador en turno (sesión activa)
        [StringLength(200)]
        public string? CoordinadorEnTurno { get; set; }

        // Campos adicionales del formulario
        public DateTime? FechaEnvio { get; set; }

        [StringLength(50)]
        public string? Mes { get; set; }

        public DateTime? FechaSolicitud { get; set; }

        public DateTime? FechaIncidente { get; set; }

        public TimeSpan? Hora { get; set; }

        [StringLength(20)]
        public string? Monto { get; set; }

        [StringLength(200)]
        public string? Codigo { get; set; }

        [StringLength(200)]
        public string? Tiempo { get; set; }

        [StringLength(200)]
        public string? Ticket { get; set; }

        [StringLength(200)]
        public string? FoliosAsignado1 { get; set; }

        [StringLength(200)]
        public string? FoliosAsignado2 { get; set; }

        [StringLength(200)]
        public string? PersonalInvolucrado { get; set; }

        [StringLength(100)]
        public string? No { get; set; }

        [StringLength(100)]
        public string? Nomina { get; set; }

        [StringLength(200)]
        public string? LineaEmpresaPlacas { get; set; }

        [StringLength(200)]
        public string? Ubicacion2 { get; set; }

        [StringLength(200)]
        public string? AreaEspecifica { get; set; }

        [StringLength(100)]
        public string? TurnoOperativo { get; set; }

        [StringLength(1000)]
        public string? Situacion { get; set; }

        [StringLength(200)]
        public string? QuienEnvia { get; set; }

        // Metadatos
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaActualizacion { get; set; }

        [StringLength(200)]
        public string? CreadoPor { get; set; }

        [StringLength(200)]
        public string? ActualizadoPor { get; set; }

        public bool Activo { get; set; } = true;
    }
}
