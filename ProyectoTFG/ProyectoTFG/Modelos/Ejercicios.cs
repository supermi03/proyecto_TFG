using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Ejercicios")]
    public class Ejercicios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ejercicios_Id { get; set; } // Esta es la única columna de identidad

        [Required]
        [StringLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La descripcion no puede superar los 100 caracteres.")]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(100)]
        public string Imagen_Ejercicio { get; set; }

        public virtual ICollection<Entrenamiento_Ejercicios> EntrenamientoEjercicios { get; set; }
    }
}
