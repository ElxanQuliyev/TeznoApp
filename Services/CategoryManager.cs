using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    public class CategoryManager
    {
        private readonly ApplicationDbContext _context;

        public CategoryManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.Where(x => !x.IsDeleted).ToList();
        }
        public Category GetById(int? id)
        {
            var selectedCategory = _context.Categories.Where(x=>!x.IsDeleted).FirstOrDefault(x => x.ID == id);
           return selectedCategory;
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void Delete(Category category)
        {
           category.IsDeleted = true;   
            _context.SaveChanges();
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
