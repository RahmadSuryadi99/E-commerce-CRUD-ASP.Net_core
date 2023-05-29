using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel;
using Basilisk.ViewModel.IndexProduct;
using Basilisk.ViewModel.ProdSupCat;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public class ProductProvider : BaseProvider
    {
        private static ProductProvider _instance = new ProductProvider();
        public static ProductProvider GetProvider()
        {
            return _instance;
        }
        public IEnumerable<GridProdSupCatViewModel> GetDataIndex()
        {
            var products = ProductRepository.GetRepository().GetAll();
            IEnumerable<GridProdSupCatViewModel> prodSupCat = (from prod in products
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
            return prodSupCat.ToList();
        }
        public GridIndexProductViewModel GetIndex(int page = 1, string searchName = "", string searchCategory = "", string searchSupplier = "")
        {
            IEnumerable<GridProdSupCatViewModel> products = GetDataIndex();
            if (!string.IsNullOrEmpty(searchName))
            {
                products = products.Where(a => a.ProductName.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchCategory))
            {
                products = products.Where(a => a.Category.Contains(searchCategory));
            }
            if (!string.IsNullOrEmpty(searchSupplier))
            {
                products = products.Where(a => a.Supplier.Contains(searchSupplier));
            }

            int totalHalaman = TotalHalaman(products.Count());
            int skip = GetSkip(page);

            var model = new GridIndexProductViewModel
            {
                SearchName = searchName,
                SearchCategory = searchCategory,
                SearchSuppllier = searchSupplier,
                Grid = products.Skip(skip).Take(_totalDataPerPage),
                TotalData = totalHalaman
            };

            return model;
        }
        public List<SelectListItem> GetDataSupplier()
        {
            var suppliers = SupplierRepository.GetRepository().GetAll();
            var result = suppliers.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.CompanyName
            }).ToList();
            return result;
        }

        public List<SelectListItem> GetDataCategory()
        {
            var categories = CategoryRepository.GetRepository().GetAll();
            var result = categories.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            return result;

        }

    

        public List<DropDownViewModel> GetDataSuplierCustom()
        {
            var suppliers = SupplierRepository.GetRepository().GetAll();
            var result = suppliers.Select(a => new DropDownViewModel
            {
                LongValue = a.Id,
                Text = a.CompanyName
            }).ToList();
            return result;

        }


        public UpsertProductViewModel GetDropDownAdd()
        {
            var model = new UpsertProductViewModel()
            {
                DropdownCategory = GetDataCategory(),
                DropdownSupplier = GetDataSupplier(),
                //DropDownCategoryCustom = GetDataCategoryCustom(),
                DropdownSupplierCustom = GetDataSuplierCustom(),
            };
            return model;
        }
        public void PostDataAdd(UpsertProductViewModel viewModel)
        {
            try
            {
                var model = new Product();
                MappingModel<Product, UpsertProductViewModel>(model, viewModel);
                ProductRepository.GetRepository().Insert(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpsertProductViewModel GetEdit(long id)
        {
            var oldProduct = ProductRepository.GetRepository().GetSingle(id);
            var model = new UpsertProductViewModel();
            MappingModel(model, oldProduct);
            model.DropdownCategory = GetDataCategory();
            model.DropdownSupplier = GetDataSupplier();
            return model;
        }
        public void PostEdit(UpsertProductViewModel viewModel)
        {
            try
            {
                var oldProduct = ProductRepository.GetRepository().GetSingle(viewModel.Id);
                MappingModel(oldProduct, viewModel);
                ProductRepository.GetRepository().Update(oldProduct);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(long id)
        {
            try
            {
                ProductRepository.GetRepository().Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
