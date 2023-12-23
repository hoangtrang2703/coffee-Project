using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using startup.Areas.Admin.Models;
using startup.Models;
using startup.Utilities;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManageController : Controller
    {
        private readonly DataContext _context;

        public UserManageController(DataContext context)
        {
            _context = context;
        }

        public IActionResult index(){
            var user = _context.AdminUsers.ToList();
            return View(user);
        }


        public IActionResult DeleteU(int? id){
            if(id == null){
                return NotFound();
            }
            var user = _context.AdminUsers.FirstOrDefault(u => u.UserID == id);
            return View(user);
        }
        
        [HttpPost]
        public IActionResult DeleteConfirm(AdminUser id){
            var user = _context.AdminUsers.Find(id.UserID);
            if(user == null){
                return NotFound();
            }
            _context.AdminUsers.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

       
    }
}
