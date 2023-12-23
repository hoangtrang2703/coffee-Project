
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using startup.Models;

[Area("admin")]
[Route("/admin/menuclient/[action]/{id?}")]
public class MenuClientController : Controller
{
    private readonly DataContext _context;
    public MenuClientController(DataContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var mnList = _context.Menus.OrderBy(m => m.MenuID).ToList();
        return View(mnList);
    }

    [HttpGet]
    public IActionResult create()
    {
        return View();
    }
    [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu mn)
        {
            if (ModelState.IsValid)
            {
                // Lưu menu vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
                _context.Menus.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng sau khi thêm thành công
            }
            return View(mn);
        }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var mn = _context.Menus.Find(id);
        if (mn == null)
        {
            return NotFound();
        }
        return View(mn);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var deleMenu = _context.Menus.Find(id);
        if (deleMenu == null)
        {
            return NotFound();
        }
        _context.Menus.Remove(deleMenu);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var mn = _context.Menus.Find(id);
        if (mn == null)
        {
            return NotFound();
        }
        return View(mn);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Menu mn)
    {
        if (ModelState.IsValid)
        {
            // Cập nhật dữ liệu menu trong cơ sở dữ liệu (sử dụng Entity Framework hoặc phương thức truy cập dữ liệu của bạn)
            _context.Menus.Update(mn);
            _context.SaveChanges();
            return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách menu
        }
        return View(mn); // Hiển thị biểu mẫu chỉnh sửa với lỗi kiểm tra
    }
}