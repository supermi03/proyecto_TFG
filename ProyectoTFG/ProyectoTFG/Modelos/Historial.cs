using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Historial")]
    public class Historial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
        public int Historial_Id { get; set; }

        [ForeignKey("Persona")]
        public int Persona_Id { get; set; }  // Propiedad de clave externa

        public string Tipo_Evento { get; set; }

        [MaxLength(50)]
        public string Descripcion { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] // Formato de fecha
        public DateTime Fecha_Evento { get; set; }

        // Propiedad de navegación
        public virtual Persona Persona { get; set; } // Relación con Persona
    }
}
