using System.ComponentModel.DataAnnotations;

namespace TPLabo_4.Models
{
    public class Ferreteria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre")]
        public string nombre { get; set; }
        public bool Stock { get; set; }
        public string? fotografia { get; set; }
        [Required(ErrorMessage = "Coloque un precio")]
        public int precio { get; set; }
    }
}
