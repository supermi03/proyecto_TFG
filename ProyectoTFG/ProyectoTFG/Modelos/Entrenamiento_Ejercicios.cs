using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Entrenamiento_Ejercicios")]
    public class Entrenamiento_Ejercicios
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
        public int Entrenamiento_Ejercicios_Id { get; set; }

        [ForeignKey("Entrenamiento")]
        public int Entrenamiento_Id { get; set; }

        [ForeignKey("Ejercicios")]
        public int Ejercicios_Id { get; set; }

        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public string Peso { get; set; }
        public string Tiempo { get; set; }

        // Navegación a las entidades relacionadas
        public virtual Entrenamiento Entrenamiento { get; set; }
        public virtual Ejercicios Ejercicios { get; set; }
    }
}
