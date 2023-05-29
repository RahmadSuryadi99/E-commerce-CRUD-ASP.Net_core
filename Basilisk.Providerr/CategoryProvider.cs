using Basilisk.DataAccess.Models;
using Basilisk.Repository;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Category;
using Basilisk.ViewModel.IndexCategory;
using Basilisk.ViewModel.Order;
using Basilisk.ViewModel.ProdSupCat;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Basilisk.Providerr
{
    public class CategoryProvider : BaseProvider
    {
        private const int _totalDataPerPage = 5;
        private static CategoryProvider _instance = new CategoryProvider();
        public static CategoryProvider GetProvider()
        {
            return _instance;
        }

        public IEnumerable<GridCategoryViewModel> GetDataIndex(string searchName)
        {
            var allCategory = CategoryRepository.GetRepository().GetAll();

            var categories = (from cat in allCategory
                              where cat.Name.Contains(searchName)
                              select new GridCategoryViewModel
                              {
                                  Id = cat.Id,
                                  Name = cat.Name,
                                  Description = cat.Description,
                              }).AsEnumerable().ToList();
            return categories;

        }
        public IndexCategoryViewModel GetIndex(int page, string searchName)
        {
            searchName = string.IsNullOrEmpty(searchName) ? "" : searchName;
            IEnumerable<GridCategoryViewModel> data = GetDataIndex(searchName);


            var model = new IndexCategoryViewModel
            {
                TotalData = data.Count(),
                TotalHalaman = TotalHalaman(data.Count()),
                GridCategoryIndex = data.Skip(GetSkip(page)).Take(_totalDataPerPage),
                SearchName = searchName
            };
            return model;
        }
        public IEnumerable<GridProdSupCatViewModel> GetDataDetail(int catId)
        {
            var product = ProductRepository.GetRepository().GetAll();
            var query = (from prod in product
                         where prod.Category.Id == catId
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
            return query;
        }
        public IEnumerable<GridProdSupCatViewModel> GetDetail(int catId)
        {
            var data = GetDataDetail(catId);
            return data;
        }

        public void AddCategory(CreateUpdateViewModel model)
        {

            try
            {
                var entity = new Category();
                MappingModel<Category, CreateUpdateViewModel>(entity, model);
                CategoryRepository.GetRepository().Insert(entity);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public CreateUpdateViewModel GetDataEditCategory(long id)
        {

            try
            {
                var oldCatagory = CategoryRepository.GetRepository().GetSingle(id);
                var model = new CreateUpdateViewModel();
                MappingModel<CreateUpdateViewModel, Category>(model, oldCatagory);
                return model;
            }

            catch (Exception)
            {
                throw;
            }

        }
        public void PostDataEditCategory(CreateUpdateViewModel model)
        {

            try
            {
                var oldCatagory = CategoryRepository.GetRepository().GetSingle(model.Id);
                var prod = new Product();
                MappingModel<Category, CreateUpdateViewModel>(oldCatagory, model);

                CategoryRepository.GetRepository().Update(oldCatagory);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void UpdateDescription(long id,string description)
        {
            try
            {
               CategoryRepository.GetRepository().UpdateDescription(id,description);
             
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteCategory(long id)
        {
            try
            {
                var category = CategoryRepository.GetRepository().GetSingle(id);
                return CategoryRepository.GetRepository().Delete(category.Id);
                 
            }
            catch (Exception)
            {
                return false;
            }

        }
        public JsonResultViewModel GetEditCategory(long id)
        {
            var result = new JsonResultViewModel();
            var oldCategory = CategoryRepository.GetRepository().GetSingle(id);
            var model = new CreateUpdateViewModel();
            try
            {
                MappingModel<CreateUpdateViewModel, Category>(model, oldCategory);
                result.ReturnObject = model;
                result.Success = true;
                result.Message = "Ada data";
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Tidak ada data";
            }
            return result;
        }
        public GridCategoryViewModel GetDetailCategory(long id)
        {
            //var product = ((CategoryRepository)CategoryRepository.GetRepository()).DetailProduct(id);
            var category = CategoryRepository.GetRepository().GetAll();
            var indoFormat = CultureInfo.CreateSpecificCulture("id-ID");
            var query = (from cat in category
                         where cat.Id == id
                         select new GridCategoryViewModel
                         {
                             Id = cat.Id,
                             Name = cat.Name,
                             Description = cat.Description
                         }).SingleOrDefault();

            return query;
        }
        public bool SaveBtnModal(CreateUpdateViewModel model)
        {
            if(model.Id == 0)
            {
                AddCategory(model);
                return true;
            }
            else
            {
                PostDataEditCategory(model);
                return true;
            }
            return false;
        }

    }

}