using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelProcessorApi.Models
{
    public class DeteccionFolio
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
        public string? Sucursal { get; set; }

        [StringLength(200)]
        public string? Codigo { get; set; }

        [StringLength(200)]
        public string? Indicador { get; set; }

        [StringLength(200)]
        public string? Subindicador { get; set; }

        [StringLength(200)]
        public string? FolioAsignado1 { get; set; }

        [StringLength(200)]
        public string? UbicacionSucursal { get; set; }

        [StringLength(200)]
        public string? Monitorista { get; set; }

        [StringLength(200)]
        public string? Puesto { get; set; }

        [StringLength(200)]
        public string? PuestoColaborador { get; set; }

        // Coordinador en turno (sesión activa)
        [StringLength(200)]
        public string? CoordinadorEnTurno { get; set; }

        // Campos adicionales del formulario
        public DateTime? FechaEnvio { get; set; }

        [StringLength(200)]
        public string? Ubicacion { get; set; }

        [StringLength(200)]
        public string? Almacen { get; set; }

        public TimeSpan? Hora { get; set; }

        [StringLength(500)]
        public string? GeneraImpacto { get; set; }

        [StringLength(200)]
        public string? FolioAsignado2 { get; set; }

        [StringLength(200)]
        public string? ColaboradorInvolucrado { get; set; }

        [StringLength(100)]
        public string? No { get; set; }

        [StringLength(100)]
        public string? Nomina { get; set; }

        [StringLength(200)]
        public string? LineaEmpresa { get; set; }

        [StringLength(200)]
        public string? AreaEspecifica { get; set; }

        [StringLength(100)]
        public string? TurnoOperativo { get; set; }

        [StringLength(200)]
        public string? SupervisorJefeTurno { get; set; }

        [StringLength(1000)]
        public string? SituacionDescripcion { get; set; }

        [StringLength(200)]
        public string? EnviaReporte { get; set; }

        [StringLength(1000)]
        public string? Retroalimentacion { get; set; }

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
