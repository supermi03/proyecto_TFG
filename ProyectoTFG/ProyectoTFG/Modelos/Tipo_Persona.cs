using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Tipo_Persona")]
    public class Tipo_Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
        public int Tipo_Persona_Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; } // "Cliente", "Monitor", "Administrador"
    }
}
