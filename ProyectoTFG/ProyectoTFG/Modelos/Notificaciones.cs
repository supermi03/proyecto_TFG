using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Notificaciones")]
    public class Notificaciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
        public int Notificaciones_Id { get; set; } // ID de la notificación

        [Required]
        public string Mensaje { get; set; } // Mensaje de la notificación

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] // Formato de fecha
        public DateTime Fecha { get; set; } // Fecha de la notificación

        // Foreign Key para Persona
        [ForeignKey("Persona")] // Cambia aquí para referirse a la propiedad de navegación
        public int Persona_Id { get; set; }

        // Navegación
        public virtual Persona Persona { get; set; } // Relación con la clase Persona

    }
}
