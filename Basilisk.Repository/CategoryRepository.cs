using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class CategoryRepository : IRepository<Category>
    { 
        //private static IRepository<Category> instance = new CategoryRepository();
        //public static IRepository<Category> GetRepository()
        //{
        //    return instance;
        //}
        private static CategoryRepository instance = new CategoryRepository();

        public static CategoryRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var category = _context.Categories.SingleOrDefault(a => a.Id == (long)id);
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Category> GetAll()
        {
            var context = new BasiliskTFContext();

            return context.Categories;
        }

        public Category GetSingle(object id)
        {
            var category = new Category();
            using (var context = new BasiliskTFContext())
            {
                category = context.Categories.SingleOrDefault(a => a.Id == (long)id);
            }
            return category;
        }

        public bool Insert(Category model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Categories.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Category model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var catagory = _context.Categories.SingleOrDefault(a => a.Id == model.Id);
                    catagory.Name = model.Name;
                    catagory.Description = model.Description;
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateDescription(long id , string description)
        {
            using(var context = new BasiliskTFContext())
            {
                var cat = context.Categories.SingleOrDefault(a => a.Id == id);
                cat.Description = description;
                context.SaveChanges();
                  
            }
        }
    }
}
