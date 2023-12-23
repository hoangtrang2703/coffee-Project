using Microsoft.AspNetCore.Mvc;
using startup.Models;
using startup.Models.products;
using startup.Utilities;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/admin/products/[action]/{id?}")]
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pr = _context.products.ToList();
            return View(pr);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel pr)
        {
            if (!ModelState.IsValid)
            {
                pr.Slug = Functions.GenerateSlug(pr.Name);
                pr.DateCreated = DateTime.Now;
                _context.products.Add(pr);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pr);

        }

        [HttpGet]
        public IActionResult Delete(int id) {
            if (id == null) return NotFound();
            var pr = _context.products.FirstOrDefault(x => x.Id == id);
            return View(pr);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var pr = _context.products.FirstOrDefault(x => x.Id == id);
            _context.products.Remove(pr);
            _context.SaveChanges();
                return RedirectToAction("Index");
           
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id == null) return NotFound();
            var pr = _context.products.FirstOrDefault(y => y.Id == id);
            return View(pr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductModel pr) {
            pr.Slug = Functions.GenerateSlug(pr.Name);
            pr.DateCreated = DateTime.Now;
                _context.products.Update(pr);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            
        }

    }
}
