using Microsoft.EntityFrameworkCore;
using RealEstate.Models;

namespace RealEstate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category{Id=1,Name="Department"},
                new Category{Id=2,Name="house"},
                new Category{Id=3,Name="villa"},


            });

            modelBuilder.Entity<RentOrSale>().HasData(new RentOrSale[]
            {
                new RentOrSale{Id=1,Name="Rent"},
                new RentOrSale{Id=2,Name="Sale"},
                
            });
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<RealState> realEstates { get; set; }
        public DbSet<RentOrSale> rentOrSales { get; set; }



    }
}
