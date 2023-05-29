using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class OrderRepository : BaseRepository, IRepository<Order>
    {
        public static OrderRepository instance = new OrderRepository();
        public static OrderRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Order = _context.Orders.SingleOrDefault(a => a.InvoiceNumber == (string)id);
                    _context.Orders.Remove(Order);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Order> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.Orders;
            }
            catch
            {
                throw;
            }
        }

        public Order GetSingle(object id)
        {
            try
            {
                var Order = new Order();
                using (var context = new BasiliskTFContext())
                {
                    Order = context.Orders.SingleOrDefault(a => a.InvoiceNumber == (string)id);
                }
                return Order;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Order model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    try
                    {
                        //transaction.RollbackToSavepoint("BeforeMoreBlogss");
                        _context.Orders.Add(model);
                        _context.SaveChanges();
                    }
                    catch
                    {
                        //transaction.RollbackToSavepoint("BeforeMoreBlogss");
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Order model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Order = _context.Orders.SingleOrDefault(a => a.InvoiceNumber == model.InvoiceNumber);
                    MappingModel(Order, model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public bool Tran
    }
}
