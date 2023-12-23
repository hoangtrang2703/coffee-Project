using Microsoft.AspNetCore.Mvc;
using startup.Models;

namespace startup.Components
{
        [ViewComponent(Name = "Contact")]
    public class ContactViewComponent : ViewComponent
    {
       
            public async Task<IViewComponentResult> InvokeAsync()
            {
                
                return await Task.FromResult((IViewComponentResult)View("Default"));
            }
        
    }
}
