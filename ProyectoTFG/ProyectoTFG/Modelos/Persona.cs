using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoTFG.Modelos
{
    [Table("Persona")]
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremento
        public int Persona_Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El nombre no puede superar los 20 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El apellido no puede superar los 20 caracteres.")]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string Telefono { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] // Formato de fecha
        public DateTime Fecha_nacimiento { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] // Formato de fecha
        public DateTime Fecha_Registro { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Clave foránea hacia Tipo_Persona
        [ForeignKey("TipoPersona")]
        public int Tipo_Persona { get; set; } // Relación con Tipo_Persona

        public virtual Tipo_Persona TipoPersona { get; set; } // Navegación
    }
}
