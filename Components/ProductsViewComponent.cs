using Microsoft.AspNetCore.Mvc;
using startup.Models;
namespace startup.Components
{
    [ViewComponent(Name = "ProductsView")]
    public class ProductsViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public ProductsViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pr = (from p in _context.products
                     select p).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default" , pr));
        }
    }
}
