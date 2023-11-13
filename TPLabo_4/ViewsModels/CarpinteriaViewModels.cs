using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using TPLabo_4.Models;

namespace TPLabo_4.ViewsModels
{
    public class CarpinteriaViewModels
    {
        public List<Carpinteria> listaCarpinteria { get; set; }
        public string busquedaNombre { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / PageSize);
            }
        }
    }
}
