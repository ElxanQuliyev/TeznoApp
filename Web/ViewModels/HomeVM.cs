using Entities;

namespace Web.ViewModels
{
    public class HomeVM
    {
        public List<Product>? SliderList { get; set; }
        public List<Product>? ProductsList { get; set; }
        public List<Product>? NewProducts { get; set; }
        public List<Product> MonthProducts { get; set; }


    }
}
