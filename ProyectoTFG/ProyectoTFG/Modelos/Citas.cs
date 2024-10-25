using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Citas")]
    public class Citas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Citas_Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required]
        public string Motivo { get; set; }

        // Clave foránea hacia Persona (Cliente)
        public int Persona_Id { get; set; }
        [ForeignKey("Persona_Id")]
        public Persona Persona { get; set; }
        // Clave foránea hacia Persona (Cliente)
    }
}
