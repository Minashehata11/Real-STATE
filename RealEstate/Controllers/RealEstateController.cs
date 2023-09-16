using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NToastNotify;
using RealEstate.Data;
using RealEstate.Services;
using RealEstate.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstate.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IRealStateServices _IRealStateServices;

        public RealEstateController(ApplicationDbContext context, IToastNotification toastNotification, IRealStateServices iRealStateServices)
        {
            _context = context;
            _toastNotification = toastNotification;
            _IRealStateServices = iRealStateServices;
        }
        public IActionResult Index()
        {
           var data = _context.realEstates.Include(a=>a.category).Include(a=>a.RentOrSale).OrderByDescending(a=>a.Price).ToList();    
            return View(data);
        }

        
        //Get 
        public IActionResult Create()
        {
            var data = new ViewFormModel
            {
                categories=_context.categories.Select(x => new SelectListItem {Value=x.Id.ToString(),Text=x.Name }).ToList(),
                Stats=_context.rentOrSales.ToList(),
               
            };
            return View("ViewForm",data);
        }


        //Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ViewFormModel model)
        {
          model.categories = _context.categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.Stats = _context.rentOrSales.ToList();
           
             await  _IRealStateServices.Create(model);
                _toastNotification.AddSuccessToastMessage("ADDed suucsess");
              return RedirectToAction("Index");
           

           
        }

        public IActionResult Edit(int? id)
        {

            var data = _context.realEstates.Find(id);
            if(id == null)
            {
                return BadRequest(string.Empty);
            }
            if(data==null) {
                return NotFound();
              }
            var form = new ViewFormModel
            {
                categories = _context.categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                Stats = _context.rentOrSales.ToList(),
                Price=data.Price,
                Description=data.Description,
                Address=data.Address,
                CategoryId=data.CategoryId,
                RentOrSaleId=data.RentOrSaleId,
                Id=data.Id
                
            };
         
            return View("ViewForm", form);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ViewFormModel model)
        {
            model.categories = _context.categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.Stats = _context.rentOrSales.ToList();

           await _IRealStateServices.Edit(model);
            _toastNotification.AddSuccessToastMessage("Data WAS UPDADED suucsess");
            return RedirectToAction("Index");



        }
        

        public IActionResult Details(int? id)
        {
            var data = _context.realEstates.
                Include(c => c.category).
                Include(r => r.RentOrSale).
                OrderBy(p => p.Price).
                FirstOrDefault(i => i.Id == id);
            if (id == null)
            {
                return BadRequest(string.Empty);
            }
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }
        public IActionResult Delete(int? id)
        {
            var data = _context.realEstates.Find(id);
            if (id == null)
            {
                return BadRequest(string.Empty);
            }
            if (data == null)
            {
                return NotFound();
            }
            var form = new ViewFormModel
            {
                categories = _context.categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),
                Stats = _context.rentOrSales.ToList(),
                Price = data.Price,
                Description = data.Description,
                Address = data.Address,
                CategoryId = data.CategoryId,
                RentOrSaleId = data.RentOrSaleId,
                Id = data.Id

            };
            return View(form);
        }
        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletepost(int? id)
        {


          await  _IRealStateServices.Delete(id);

           
            _toastNotification.AddSuccessToastMessage("Data WAS UPDADED suucsess");
            return RedirectToAction("Index");



        }

        //TODO: 2.Improve front  of Index View,3.ADD ANIMATION,4.Use Auto Mapper
      
       
    }
}
