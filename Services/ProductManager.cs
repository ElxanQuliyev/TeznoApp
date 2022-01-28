using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager
    {
        private readonly ApplicationDbContext _context;

        public ProductManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> SearchProducts(string? q,int? categoryId)
        {
            var productList = _context.Products.Include(x=>x.Category).AsQueryable();
            
            if (categoryId != null)
                productList = productList.Where(x => x.CategoryId == categoryId);


            if (!string.IsNullOrWhiteSpace(q)) 
                productList = productList.Where(x => x.Name.Contains(q) || x.Category.CategoryName.Contains(q));

            return productList.OrderByDescending(x=>x.ModifiedOn).ToList();
         }

        public List<Product> GetProducts()
        {
         return   _context.Products.Include(x=>x.Category).Where(x=>!x.IsDeleted).ToList();
        }
        public List<Product> NewProducts(int count)
        {
            return _context.Products.Include(x => x.Category).Where(x => !x.IsDeleted)
                .OrderByDescending(x=>x.PublishDate).Take(count).ToList();
        }
        public List<Product> WeekProducts(int count)
        {
            return _context.Products.Include(x => x.Category).Where(x => !x.IsDeleted && x.IsWeek)
                .OrderByDescending(x => x.ModifiedOn).Take(count).ToList();
        }
        public List<Product> MonthProducts(int count)
        {
            return _context.Products.Include(x => x.Category).Where(x => !x.IsDeleted && x.IsMonth)
                .OrderByDescending(x => x.ModifiedOn).Take(count).ToList();
        }
        public Product? GetById(int? id)
        {
            var selectedProduct = _context.Products.Include(x => x.Category).Where(x => !x.IsDeleted).FirstOrDefault(x => x.ID == id);
           return selectedProduct;
        }

        public List<Product?> GetByIds(IEnumerable<int> ids)
        {
            var selectedProducts = _context.Products.Where(pr => ids.Contains(pr.ID)).ToList();
            return selectedProducts;

        }
        public List<Product>? GetSliders()
        {
            return _context.Products.Where(x => x.IsSlider && !x.IsDeleted).OrderByDescending(x=>x.ModifiedOn).Take(5).ToList();
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); 
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            product.IsDeleted = true;
            _context.SaveChanges();
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
