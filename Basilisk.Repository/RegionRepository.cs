using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class RegionRepository : BaseRepository, IRepository<Region>
    {
        public static RegionRepository Instance = new RegionRepository(); 
        public static RegionRepository GetRepository() { return Instance; }
        public bool Delete(object id)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Region = _context.Regions.SingleOrDefault(a => a.Id == (long)id);
                    _context.Regions.Remove(Region);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Region> GetAll()
        {
            try
            {
                var context = new BasiliskTFContext();
                return context.Regions;
            }
            catch
            {
                throw;
            }
        }

        public Region GetSingle(object id)
        {
            try
            {
                var Region = new Region();
                using (var context = new BasiliskTFContext())
                {
                    Region = context.Regions.SingleOrDefault(a => a.Id == (long)id);
                }
                return Region;
            }
            catch
            {
                throw;
            }
        }

        public bool Insert(Region model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    _context.Regions.Add(model);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Region model)
        {
            try
            {
                using (var _context = new BasiliskTFContext())
                {
                    var Region = _context.Regions.SingleOrDefault(a => a.Id == model.Id);
                    Region.Id = model.Id;
                    MappingModel(Region, model);
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
