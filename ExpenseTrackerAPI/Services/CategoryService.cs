using AutoMapper;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Services.Dtos;


namespace ExpenseTrackerAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto); 
            var createdCategory = await _categoryRepository.AddCategoryAsync(categoryEntity);
            return _mapper.Map<CategoryDto>(createdCategory);  
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(categoryDto.Id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            var categoryEntity = _mapper.Map<Category>(categoryDto);
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(categoryEntity);
            return _mapper.Map<CategoryDto>(updatedCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
