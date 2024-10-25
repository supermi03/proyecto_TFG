using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Entrenamiento")]
    public class Entrenamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
        public int Entrenamiento_Id { get; set; } // ID del entrenamiento

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] // Formato de fecha
        public DateTime Fecha { get; set; } // Fecha del entrenamiento

        [Required]
        public int Persona_Id { get; set; } // Relación con Persona

        // Propiedades de navegación
        [ForeignKey("Persona_Id")]
        public virtual Persona Persona { get; set; } // Referencia a la entidad Persona

        public ICollection<Entrenamiento_Ejercicios> EntrenamientoEjercicios { get; set; } // Relación con Entrenamiento_Ejercicios

        public Entrenamiento()
        {
            EntrenamientoEjercicios = new HashSet<Entrenamiento_Ejercicios>();
        }
    }
}
