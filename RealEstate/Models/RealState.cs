using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class RealState
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }


        [Required]
        public double Price { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Category category { get; set; }

        public int RentOrSaleId { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public RentOrSale RentOrSale { get; set; }

    }
}
