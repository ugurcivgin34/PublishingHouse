using AutoMapper;
using PublishingHouse_Business.Abstract;
using PublishingHouse_DataAccess.Repositories.Abstract;
using PublishingHouse_DataTransferObjects.Request.Category;
using PublishingHouse_Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingHouse_Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper mapper;
        private List<Category> categories;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddCategory(AddCategoryRequest request)
        {
            var category = mapper.Map<Category>(request);
            await _categoryRepository.Add(category);
            return category.Id;
        }

        public async Task DeleteCategory(int id)
        {
            await _categoryRepository.Delete(id);
        }

        public async Task<AddCategoryRequest> GetCategory(int id)
        {

            var category = await _categoryRepository.GetById(id);
            var categoryDisplayResponse = mapper.Map<AddCategoryRequest>(category);
            return categoryDisplayResponse;
        }

        public async Task<IList<AddCategoryRequest>> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();
            var result = mapper.Map<IList<AddCategoryRequest>>(categories);
            return result;
        }



        public async Task<bool> IsCategoryExists(int id)
        {
            return await _categoryRepository.IsExists(id);

        }

        public async Task UpdateCategory(UpdateCategoryRequest request)
        {
            var category = mapper.Map<Category>(request);
            await _categoryRepository.Update(category);
        }

       
    }

}
