using RealEstate_Dapper__Api.Dtos.CategoryDtos;

namespace RealEstate_Dapper__Api.Models.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        void CreateCategory(CreateCategoryDto categoryDto);
        void DeleteCategory(int id);
        void UpdateCategory(UpdateCategoryDto categoryDto);

        Task<GetByIdCategoryDto> GetCategory(int id); //Category Idleri getiren method.


        //Task veya Task<T> döndüren yapılar asenkron bir yöntem await işlem kullanılır.
        // void yapılarında kullanılmaz.
    }
}
