using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPLabo_4.Models
{
    public class Calidad
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Coloque un tipo de madera")]
        public string TipoMadera { get; set; }
        public bool Artesanal {  get; set; }
        public List<Carpinteria>? listaCarpinteria { get; set; }
    }
}
