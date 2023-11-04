using System.ComponentModel.DataAnnotations;

namespace TPLabo_4.Models
{
    public class Carpinteria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Esciba un nombre")]
        public string nombre { get; set; }
        public bool Stock {  get; set; }
        public string? fotografia { get; set; }
        public string? descripcion { get; set; }
        [Required(ErrorMessage = "Coloque el precio")]
        public int precio { get; set; }
        [Display(Name = "Calidad")]
        public int IdCalidad {  get; set; }
        public Calidad? Calidad { get; set; }
    }
}
