using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel;
using Basilisk.ViewModel.ProdSupCat;
using Basilisk.ViewModel.ProductSupplier;
using Basilisk.ViewModel.Supplier;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public class SupplierProvider : BaseProvider
    {
        public static SupplierProvider Instance = new SupplierProvider();
        public static SupplierProvider GetProvider()
        {
            return Instance;
        }

        public IEnumerable<GridSupplierViewModel> GetDataIndex()
        {
            var supplier = SupplierRepository.GetRepository().GetAll();
            IEnumerable<GridSupplierViewModel> suppliers = (from sup in supplier
                                                            where sup.DeleteDate == null
                                                            select new GridSupplierViewModel
                                                            {
                                                                ID = sup.Id,
                                                                CompanyName = sup.CompanyName,
                                                                ContactPerson = sup.ContactPerson,
                                                                JobTitle = sup.JobTitle,
                                                                Address = sup.Address,
                                                                City = sup.City,
                                                                Phone = sup.Phone,
                                                                Email = sup.Email == null ? "N/A" : sup.Email

                                                            }).ToList();
            return suppliers;

        }
        public IndexSupplier GetIndex(int page = 1, string searchName = "")
        {
            var suppliers = GetDataIndex();
            if (!string.IsNullOrEmpty(searchName))
            {
                suppliers = suppliers.Where(a => a.CompanyName.Contains(searchName));
            }

            var data = GetDataIndex();
            var skip = GetSkip(page);
            var model = new IndexSupplier
            {
                SearchName = searchName,
                Grid = data.Skip(skip).Take(_totalDataPerPage).ToList(),
                TotalHalaman = TotalHalaman(data.Count())
            };

            return model;
        }

        public ProdSupViewModel GetDetail(int id)
        {
            var Allsupplier = SupplierRepository.GetRepository().GetAll();
            var AllProduct = ProductRepository.GetRepository().GetAll();
            var suppliers = (from sup in Allsupplier
                             where sup.Id == id
                             select new GridSupplierViewModel
                             {
                                 ID = sup.Id,
                                 CompanyName = sup.CompanyName,
                                 ContactPerson = sup.ContactPerson,
                                 JobTitle = sup.JobTitle,
                                 Address = sup.Address,
                                 City = sup.City,
                                 Phone = sup.Phone,
                                 Email = sup.Email

                             }).AsEnumerable();

            var product = (from prod in AllProduct
                           where prod.Supplier.Id == id
                           select new GridProdSupCatViewModel
                           {
                               ID = prod.Id,
                               ProductName = prod.Name,
                               Supplier = prod.Supplier.CompanyName,
                               Category = prod.Category.Name,
                               Description = prod.Description,
                               Price = prod.Price,
                               Stock = prod.Stock,
                               OnOrder = prod.OnOrder,
                               Discontinue = prod.Discontinue
                           }).AsEnumerable();


            ProdSupViewModel prodSup = new ProdSupViewModel();
            prodSup.GridSupplier = suppliers.SingleOrDefault();
            prodSup.Grip = product;
            return prodSup;
        }

        public void PostAdd(CreateUpdateSupViewModel model)
        {
            try
            {
                Supplier newSup = new Supplier();
                MappingModel<Supplier, CreateUpdateSupViewModel>(newSup, model);
                SupplierRepository.GetRepository().Insert(newSup);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CreateUpdateSupViewModel GetEdit(long id)
        {
            var oldSup = SupplierRepository.GetRepository().GetSingle(id);
            var entity = new CreateUpdateSupViewModel();
            MappingModel<CreateUpdateSupViewModel, Supplier>(entity, oldSup);
            return entity;
        }
        public void PostEdit(CreateUpdateSupViewModel model)
        {
            var entity = SupplierRepository.GetRepository().GetSingle(model.ID);
            MappingModel<Supplier, CreateUpdateSupViewModel>(entity, model);
        }
    }
}
