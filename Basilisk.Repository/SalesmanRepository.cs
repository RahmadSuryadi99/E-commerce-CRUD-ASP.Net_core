using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class SalesmanRepository : IRepository<Salesman>
    {
        public static SalesmanRepository instance = new SalesmanRepository();
        public static SalesmanRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var salesman = _context.Salesmen.SingleOrDefault(a => a.EmployeeNumber == (string)id);
                    _context.Salesmen.Remove(salesman);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Salesman> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.Salesmen;
            }
            catch
            {
                throw;
            }
        }

        public Salesman GetSingle(object id)
        {
            try
            {
                var salesman = new Salesman();
                using (var context = new BasiliskTFContext())
                {
                    salesman = context.Salesmen.SingleOrDefault(a => a.EmployeeNumber == (string)id);
                }
                return salesman;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Salesman model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Salesmen.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Salesman model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var salesmen = _context.Salesmen.SingleOrDefault(a => a.EmployeeNumber == model.EmployeeNumber);
                    salesmen.EmployeeNumber = model.EmployeeNumber;
                    salesmen.FirstName = model.FirstName;
                    salesmen.LastName = model.LastName;
                    salesmen.Level = model.Level;
                    salesmen.BirthDate = model.BirthDate;
                    salesmen.HiredDate = model.HiredDate;
                    salesmen.Address = model.Address;
                    salesmen.City = model.City;
                    salesmen.Phone = model.Phone;
                    salesmen.SuperiorEmployeeNumber = model.SuperiorEmployeeNumber;
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
