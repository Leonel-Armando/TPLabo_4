using System.ComponentModel.DataAnnotations;

namespace TPLabo_4.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Escriba un nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingrese una edad")]
        public int edad { get; set; }
        [Display (Name = "Años trabajando")]
        [Required(ErrorMessage = "Ingrese los años de trabajo")]
        public int experiencia { get; set; }
        public string? fotografia { get; set; }
    }
}
