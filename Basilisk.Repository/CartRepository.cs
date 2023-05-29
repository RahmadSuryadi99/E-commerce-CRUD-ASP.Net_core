using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class CartRepository : BaseRepository, IRepository<Cart>
    {
        public static CartRepository instance = new CartRepository();
        public static CartRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Cart = _context.Carts.SingleOrDefault(a => a.Id == (long)id);
                    _context.Carts.Remove(Cart);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Cart> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.Carts;
            }
            catch
            {
                throw;
            }
        }

        public Cart GetSingle(object id)
        {
            try
            {
                var Cart = new Cart();
                using (var context = new BasiliskTFContext())
                {
                    Cart = context.Carts.SingleOrDefault(a => a.Id == (long)id);
                }
                return Cart;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Cart model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Carts.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Cart model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Cart = _context.Carts.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel<Cart, Cart>(Cart, model);
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
