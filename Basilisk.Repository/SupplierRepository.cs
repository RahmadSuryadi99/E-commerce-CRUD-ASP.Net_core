using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class SupplierRepository : IRepository<Supplier>
    {
        public static SupplierRepository Instance = new SupplierRepository();
        public static SupplierRepository GetRepository() { return Instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var supplier = _context.Suppliers.SingleOrDefault(a => a.Id == (long)id);
                    _context.Suppliers.Remove(supplier);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Supplier> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.Suppliers;
            }
            catch
            {
                throw;
            }
        }

        public Supplier GetSingle(object id)
        {
            try
            {
                var supplier = new Supplier();
                using (var context = new BasiliskTFContext())
                {
                    supplier = context.Suppliers.SingleOrDefault(a => a.Id == (long)id);
                }
                return supplier;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Supplier model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Suppliers.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Supplier model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var supplier = _context.Suppliers.SingleOrDefault(a => a.Id == model.Id);
                    supplier.Id = model.Id;
                    supplier.CompanyName = model.CompanyName;
                    supplier.ContactPerson = model.ContactPerson;
                    supplier.JobTitle = model.JobTitle;
                    supplier.Address = model.Address;
                    supplier.City = model.City;
                    supplier.Phone = model.Phone;
                    supplier.Email = model.Email;
                    supplier.DeleteDate = model.DeleteDate;
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
