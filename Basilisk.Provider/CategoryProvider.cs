using Basilisk.DataAccess.Models;
using Basilisk.ViewModel.Category;
using Basilisk.ViewModel.IndexCategory;
using Microsoft.EntityFrameworkCore;

namespace Basilisk.Provider
{
    public class CategoryProvider
    {
        private const int _totalDataPerPage = 5;
        private static CategoryProvider _instance  = new CategoryProvider();
        public static CategoryProvider GetProvider() {
            return _instance;
                }

        private IEnumerable<GridCategoryViewModel> GetDataIndex(string searchName)
        {
            using (var _context = new BasiliskTFContext())
            {

                var categories = (from cat in _context.Categories
                                  select new GridCategoryViewModel
                                  {
                                      Id = cat.Id,
                                      Name = cat.Name,
                                      Description = cat.Description,
                                  }).AsEnumerable();
            return categories;
            }
        }
        public IndexCategoryViewModel GetIndex(int page ,string searchName) {

            //if (!string.IsNullOrEmpty(searchName))
            //{
            //    categories = categories.Where(a => a.Name.Contains(searchName));
            //}
            searchName = string.IsNullOrEmpty(searchName) ? "" : searchName;
            var data = GetDataIndex(searchName);
            int totalHalaman = (int)Math.Ceiling(data.Count() / (decimal)_totalDataPerPage);
            int skip = (page - 1) * _totalDataPerPage;


            var model = new IndexCategoryViewModel
            {
                
                GridCategoryIndex = data,
                SearchName = searchName
            };
            return model;
        }
    }
}