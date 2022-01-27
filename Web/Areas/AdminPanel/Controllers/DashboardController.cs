using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
      
        private readonly ProductManager _productManager;

        public DashboardController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
