using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using startup.Areas.Admin.Models;
using startup.Models;
using startup.Utilities;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginClientController : Controller
    {
        private readonly DataContext _context;
        public LoginClientController(DataContext context)
        {
            _context = context;
        }
        [Route("/Login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginClient(AdminUser user)
        {
            if (user == null)
            {
                return NotFound();
            }
            string pw = Functions.MD5Password(user.Password);

            var check = _context.AdminUsers.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Functions._Message = "Invalid UserName or Password";

                return RedirectToAction("Index", "Login");
            }
            Functions._Message = string.Empty;
            Functions._UserID = check.UserID;
            Functions._UserName = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
            Functions._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            return Redirect("~/");
        }

        

        public IActionResult Logoutclient()
        {
            Functions._UserID = 0;
            Functions._UserName = string.Empty;
            Functions._Email = string.Empty;
            Functions._Message = string.Empty;
            Functions._MessageEmail = string.Empty;

            return Redirect("~/");
        }
    }
}
