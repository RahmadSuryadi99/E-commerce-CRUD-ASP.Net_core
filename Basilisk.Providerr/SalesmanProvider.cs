using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel.Salesman;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public class SalesmanProvider : BaseProvider
    {
        private static SalesmanProvider _instance = new SalesmanProvider();
        public static SalesmanProvider GetProvider()
        {
            return _instance;
        }

        public IQueryable GetJumlahRegion(string id)
        {
            var SalesmenRegions = SalesmanRegionRepository.GetRepository().GetAll();
            var region = (from s in SalesmenRegions
                          where s.SalesmanEmployeeNumber == id
                          group s by s.SalesmanEmployeeNumber into jumlahRegion
                          select new
                          {
                              jumlahRegion = jumlahRegion,
                          }).AsQueryable();
            return region;
        }
        public IndexSalesmanViewModel GetIndex(int page = 1)
        {
            var salesmen = SalesmanRepository.GetRepository().GetAll();

            IEnumerable<SalesmanViewModel> data = (from sal in salesmen
                                                   join s in salesmen on sal.SuperiorEmployeeNumber equals s.EmployeeNumber into newsales
                                                   from subsales in newsales.DefaultIfEmpty()
                                                   select new SalesmanViewModel()
                                                   {
                                                       ID = sal.EmployeeNumber,
                                                       FirstName = sal.FirstName,
                                                       LastName = sal.LastName,
                                                       Level = sal.Level,
                                                       BirthDate = sal.BirthDate,
                                                       HireDate = sal.HiredDate,
                                                       Address = sal.Address,
                                                       City = sal.City,
                                                       Phone = sal.Phone,
                                                       SuperiorEmpID = sal.SuperiorEmployeeNumber,
                                                       SuperiorEmpName = $"{subsales.FirstName} {subsales.LastName}"
                                                   }).ToList();
            var skip = GetSkip(page);
            var model = new IndexSalesmanViewModel()
            {
                TotalHalaman = TotalHalaman(data.Count()),
                grid = data.Skip(skip).Take(_totalDataPerPage).ToList()
            };

            return model;
        }

        public SalesmanViewModel GetAdd()
        {

            var salesmen = SalesmanRepository.GetRepository().GetAll();

            var result = salesmen.Select(a => new SelectListItem()
            {
                Value = a.EmployeeNumber,
                Text = $"{a.FirstName} {a.LastName}"
            }).ToList();

            var model = new SalesmanViewModel()
            {
                SuperiorEmpName = "kosong",
                DropDownSales = result
            };
            return model;
        }
        public void PostAdd(SalesmanViewModel viewModel)
        {

            try
            {
                var salesman = new Salesman();
                MappingModel<Salesman, SalesmanViewModel>(salesman, viewModel);
                SalesmanRepository.GetRepository().Insert(salesman);
            }
            catch
            {
                throw;
            }

        }
        public List<SelectListItem> GetDataSuperiorEmployee()
        {
            var salesmen = SalesmanRepository.GetRepository().GetAll();

            var result = salesmen.Select(a => new SelectListItem()
            {
                Value = a.EmployeeNumber,
                Text = $"{a.FirstName} {a.LastName}"
            }).ToList();
            return result;
        }
        public SalesmanViewModel GetEdit(string id)
        {

            var oldSales = SalesmanRepository.GetRepository().GetSingle(id);
            var model = new SalesmanViewModel();
            MappingModel<SalesmanViewModel, Salesman>(model, oldSales);
            model.DropDownSales = GetDataSuperiorEmployee();
            return model;

        }
        public void PostEdit(SalesmanViewModel viewModel)
        {
            try
            {
                var salesman = new Salesman();
                MappingModel<Salesman, SalesmanViewModel>(salesman, viewModel);
                SalesmanRepository.GetRepository().Update(salesman);
            }
            catch
            {
                throw;
            }

        }

        public DetailSalesViewModel GetDetail(string id)
        {
            var AllSalesman = SalesmanRepository.GetRepository().GetAll();
            var AllSalesmanRegion = SalesmanRegionRepository.GetRepository().GetAll();
            var AllRegion = RegionRepository.GetRepository().GetAll();
            var salesmen = SalesmanRepository.GetRepository().GetSingle(id);
            var saldata = new SalesmanViewModel();
            MappingModel<SalesmanViewModel, Salesman>(saldata, salesmen);

            var region = (from s in AllSalesman.ToList()
                          join sr in AllSalesmanRegion.ToList() on s.EmployeeNumber equals sr.SalesmanEmployeeNumber
                          join r in AllRegion.ToList() on sr.RegionId equals r.Id
                          where s.EmployeeNumber == id
                          select new salesmanRegionViewModel()
                          {
                              City = r.City,
                              Remark = r.Remark
                          }).ToList();

            var model = new DetailSalesViewModel()
            {
                Sales = saldata,
                Grid = region

            };
            return model;

        }
        public void PostDelete(string empNumber)
        {
            var salesmen = SalesmanRepository.GetRepository().GetSingle(empNumber);
            var salesmens = SalesmanRepository.GetRepository().GetAll();
            var salesmenRegions = SalesmanRegionRepository.GetRepository().GetAll();
            var orders = OrderRepository.GetRepository().GetAll();

            var checkSales = salesmens.Any(s => s.SuperiorEmployeeNumber == empNumber);
            var checkRegion = salesmenRegions.Any(r => r.SalesmanEmployeeNumber == empNumber);
            var checkOrder = orders.Any(o => o.SalesEmployeeNumber == empNumber);

            if (checkOrder==false&&checkRegion==false&&checkSales==false)
            {
                SalesmanRepository.GetRepository().Delete(empNumber);
            }

        }
    }
}
