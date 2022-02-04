using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly UserManager<ECommerceUser> _userManager;
        private readonly OrderManager _orderManager;
        public OrdersController(ProductManager productManager, UserManager<ECommerceUser> userManager, OrderManager orderManager)
        {
            _productManager = productManager;
            _userManager = userManager;
            _orderManager = orderManager;
        }

        public async Task<IActionResult> Checkout()
        {
            var productIdList = Request.Cookies["cartItem"];
            List<Product> productList = null;
            CheckoutVM vm = new();

            if (productIdList != null && productIdList != "")
            {
                List<int> productIds = productIdList.Split('-').Select(c => int.Parse(c)).ToList();
                productList = _productManager.GetByIds(productIds.Distinct());
                vm.ProductIds = productIds;
                vm.Products = productList;
                var selectedUser = await _userManager.GetUserAsync(User);
                if (selectedUser != null)
                {
                    vm.CustomerID = selectedUser.Id;
                    vm.CustomerEmail = selectedUser.Email;
                    vm.CustomerPhone = selectedUser.PhoneNumber;
                    vm.CustomerAddress=selectedUser.Address;
                    vm.CustomerFirstname = selectedUser.Firstname;
                    vm.CustomerLastname = selectedUser.Lastname;
                   
                }

                return View(vm);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutAsync(CheckoutVM checkout)
        {
            Order newOrder = new();
            var productIdList = Request.Cookies["cartItem"];
            List<Product> productList = null;
            if (productIdList != null && productIdList != "")
            {
                List<int> productIds = productIdList.Split('-').Select(c => int.Parse(c)).ToList();
                productList = _productManager.GetByIds(productIds.Distinct());
                var selectedUser = await _userManager.GetUserAsync(User);

                newOrder.CustomerName = selectedUser.UserName;
                newOrder.CustomerPhone = checkout.CustomerPhone;
                newOrder.CustomerAddress = checkout.CustomerAddress;

                newOrder.CustomerEmail = selectedUser.Email;
                newOrder.CustomerID = selectedUser.Id;
                newOrder.OrderCode = Guid.NewGuid().ToString();
                newOrder.PlacedOn = DateTime.Now;
                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(productList.Select(c =>
                new OrderItem() { 
                    ProductId = c.ID, 
                    Quantiy = (ushort)productIds.Where(prId => prId == c.ID).Count(), 
                    itemPrice=c.Price,
                    ID = newOrder.ID }));

                newOrder.TotalAmount=newOrder.OrderItems.Select(c=>c.Quantiy*c.itemPrice).Sum();
            }
            _orderManager.Add(newOrder);

            


            return RedirectToAction("Index","Home");
        }
    }
}
