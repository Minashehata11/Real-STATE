using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.ViewModel;

namespace RealEstate.Services
{
    public class RealStateServices:IRealStateServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;


        public RealStateServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}/assets/images";
        }
        public async Task Create(ViewFormModel model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);
            using var stream = File.Create(path);
            await model.Image.CopyToAsync(stream);


            var realState = new RealState
            {
                Image=coverName,
                Description=model.Description,
                Address=model.Address,
                RentOrSaleId=model.RentOrSaleId,
                CategoryId=model.CategoryId,
                Price=model.Price,

              
            };
            _context.Add(realState);
            _context.SaveChanges();
        }

        public async Task Edit(ViewFormModel model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);
            using var stream = File.Create(path);
            await model.Image.CopyToAsync(stream);

            var row=_context.realEstates.Find(model.Id);

            row.Description=model.Description;
            row.Address=model.Address;
            row.RentOrSaleId=model.RentOrSaleId;
            row.CategoryId = model.CategoryId;
            row.Price=model.Price;
            row.Image = coverName;
          
            
            
            _context.SaveChanges();
        }

        public async Task Delete(int? id)
        {
            var tuple=_context.realEstates.Include(x=>x.category).Include(x=>x.RentOrSale).SingleOrDefault(x=>x.Id==id);
            var path = Path.Combine(_imagesPath, tuple.Image);
            File.Delete(path);

           

            _context.Remove(tuple);

            _context.SaveChanges();
            
        }
    }
}
