using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class ProductRepository : IRepository<Product>
    {

        public static ProductRepository instance = new ProductRepository();
        public static ProductRepository GetRepository()
        {
            return instance;
        }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var transaction = _context.Database.BeginTransaction();
                    try
                    {
                        var product = _context.Products.SingleOrDefault(a => a.Id == (long)id);
                        _context.Products.Remove(product);
                        _context.SaveChanges();

                        //throw new Exception();
                        //transaction.Commit();

                        transaction.RollbackToSavepoint("BeforeMoreBlogs");
                        return true;
                    }
                    catch
                    {
                        transaction.RollbackToSavepoint("BeforeMoreBlogs");
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Product> GetAll()
        {
            try
            {
                var products = new BasiliskTFContext().Products;
                return products;
            }
            catch
            {
                throw;
            }
        }

        public Product GetSingle(object id)
        {
            try
            {
                var products = new Product();
                using (var context = new BasiliskTFContext())
                {
                    products = context.Products.SingleOrDefault(a => a.Id == (long)id);
                }
                return products;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Product model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Products.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Product model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var product = _context.Products.SingleOrDefault(a => a.Id == model.Id);
                    product.Id = model.Id;
                    product.Name = model.Name;
                    product.SupplierId = model.SupplierId;
                    product.CategoryId = model.CategoryId;
                    product.Price = model.Price;
                    product.Stock = model.Stock;
                    product.Category = model.Category;
                    product.Description = model.Description;
                    product.OnOrder = model.OnOrder; ;
                    product.Discontinue = model.Discontinue;

                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
