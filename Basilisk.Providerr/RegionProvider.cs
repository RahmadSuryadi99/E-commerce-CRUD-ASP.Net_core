using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Region;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public class RegionProvider : BaseProvider
    {
        private const int _totalDataPerPage = 5;
        private static RegionProvider _instance = new RegionProvider();
        public static RegionProvider GetProvider()
        {
            return _instance;
        }

        public IndexRegionViewModel GetIndex(string searchName = "")
        {
            var regions = RegionRepository.GetRepository().GetAll();
            var entity = (from r in regions
                          where r.City.Contains(searchName)
                          select new RegionViewModel()
                          {
                              Id = r.Id,
                              City = r.City,
                              Remark = r.Remark
                          }).AsEnumerable().ToList();

            var model = new IndexRegionViewModel()
            {
                grid = entity,
            };
            return model;
        }

        public RegionViewModel GetEdit(long idRegion)
        {

            var entity = RegionRepository.GetRepository().GetSingle(idRegion);
            var model = new RegionViewModel();
            MappingModel(model, entity);


            return model;
        }
        public void PostSave(RegionViewModel viewModel)
        {
            try
            {
                if (viewModel.Id == 0)
                {
                    var entity = new Region();
                    MappingModel(entity, viewModel);
                    RegionRepository.GetRepository().Insert(entity);
                }
                else
                {
                    var oldRegion = RegionRepository.GetRepository().GetSingle(viewModel.Id);
                    MappingModel(oldRegion, viewModel);
                    RegionRepository.GetRepository().Update(oldRegion);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
