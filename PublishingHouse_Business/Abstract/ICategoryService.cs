using PublishingHouse_DataTransferObjects.Request.Category;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Abstract
{
    public interface ICategoryService
    {
        Task<IList<AddCategoryRequest>> GetCategories();
        Task<AddCategoryRequest> GetCategory(int id);
        Task<int> AddCategory(AddCategoryRequest request);
        Task UpdateCategory(UpdateCategoryRequest request);
        Task DeleteCategory(int id);
        Task<bool> IsCategoryExists(int id);
    }
}
