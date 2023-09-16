using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.ViewModel
{
    public class ViewFormModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

       
        public IFormFile Image { get; set; }

        [Required]
        public double Price { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public  IEnumerable<SelectListItem>  categories{ get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Rent Or Sale")]
        public int RentOrSaleId { get; set; }

        public IEnumerable<RentOrSale> Stats { get; set; } 

    }
}
