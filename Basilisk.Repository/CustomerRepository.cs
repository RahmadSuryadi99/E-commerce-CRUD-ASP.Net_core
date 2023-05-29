using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class CustomerRepository : BaseRepository,IRepository<Customer>
    {
        public static CustomerRepository instance = new CustomerRepository();
        public static CustomerRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Customer = _context.Customers.SingleOrDefault(a => a.Id == (long)id);
                    _context.Customers.Remove(Customer);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Customer> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.Customers;
            }
            catch
            {
                throw;
            }
        }

        public Customer GetSingle(object id)
        {
            try
            {
                var Customer = new Customer();
                using (var context = new BasiliskTFContext())
                {
                    Customer = context.Customers.SingleOrDefault(a => a.Id == (long)id);
                }
                return Customer;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Customer model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Customers.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Customer model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Customer = _context.Customers.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(Customer, model);
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
