using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class DeliveryRepository:BaseRepository,IRepository<Delivery>
    {
        public static DeliveryRepository instance = new DeliveryRepository();
        public static DeliveryRepository GetRepository() { return instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Delivery = _context.Deliveries.SingleOrDefault(a => a.Id == (long)id);
                    _context.Deliveries.Remove(Delivery);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Delivery> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.Deliveries;
            }
            catch
            {
                throw;
            }
        }

        public Delivery GetSingle(object id)
        {
            try
            {
                var Delivery = new Delivery();
                using (var context = new BasiliskTFContext())
                {
                    Delivery = context.Deliveries.SingleOrDefault(a => a.Id == (long)id);
                }
                return Delivery;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Delivery model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Deliveries.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Delivery model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Delivery = _context.Deliveries.SingleOrDefault(a => a.Id == model.Id);
                    MappingModel(Delivery, model);
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
