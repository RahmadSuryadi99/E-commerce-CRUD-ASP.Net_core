using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Repository
{
    public class SalesmanRegionRepository
    {
        public static SalesmanRegionRepository instance = new SalesmanRegionRepository();
        public static SalesmanRegionRepository GetRepository() { return instance; }
        public IQueryable<SalesmenRegion> GetAll()
        {
            var context = new BasiliskTFContext();
            return context.SalesmenRegions;
        }
    }
}
