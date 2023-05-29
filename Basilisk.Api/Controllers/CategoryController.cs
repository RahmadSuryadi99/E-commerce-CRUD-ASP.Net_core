using Basilisk.Providerr;
using Basilisk.Repository;
using Basilisk.ViewModel;
using Basilisk.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<GridCategoryViewModel> Get()
        {
            var result = CategoryProvider.GetProvider().GetDataIndex("");
            return result;
        }

        [HttpGet]
        [Route("{name}")]
        public IEnumerable<GridCategoryViewModel> Get(string name, string tes)
        {
            var result = CategoryProvider.GetProvider().GetDataIndex(name);
            return result;
        }
        //[HttpGet("id={id}")]
        [HttpGet("GetEdit/{id}")]
        public CreateUpdateViewModel Get(long id)
        {
            var model = CategoryProvider.GetProvider().GetDataEditCategory(id);

            return model;
        }

        [HttpPost]
        public string Insert(CreateUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try

                {
                    CategoryProvider.GetProvider().AddCategory(model);
                    return "Berhasil tambah data";
                }
                catch (Exception ex)
                {
                    return "Gagal tambah data";
                }
            }
            return "Gagal tambah data";

        } 
        [HttpPut("PutEdit/{model}")]
        public string Edit(CreateUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try

                {
                    CategoryProvider.GetProvider().PostDataEditCategory(model);
                    return "Berhasil tambah data";
                }
                catch (Exception ex)
                {
                    return "Gagal tambah data";
                }
            }
            return "Gagal tambah data";

        }
        [HttpPatch("{id}")]
        public string Patch(long id, [FromBody] string description)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CategoryProvider.GetProvider().UpdateDescription(id,description);
                    return "data berhasil di update";
                }
                catch
                {
                    return "data gagal di update";

                }
            }
                    return "data gagal di update";
        }

        [HttpDelete("{id}")]
        public string Delete(long id)
        {
            if (CategoryProvider.GetProvider().DeleteCategory(id))
            {
                return "delete berhasil";
            }
            else
            {
                return "delete Gagal";
            }
        }
        [HttpGet("GetEditbyId/{id}")]
        public JsonResultViewModel GetEdit(long id)
        {
            var result = CategoryProvider.GetProvider().GetEditCategory(id);
            return result;
        }
      

    }
}
