using Dapper;
using RealEstate_Dapper__Api.Dtos.CategoryDtos;
using RealEstate_Dapper__Api.Models.DapperContext;

namespace RealEstate_Dapper__Api.Models.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly Context _context; //ilgili context sınıfını çağırdık.

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        //Ekleme
        public async void CreateCategory(CreateCategoryDto categoryDto)
        {
            string query = "insert into Category(CategoryName,CategoryStatus) values (@categoryName,@categoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", true); // CategoryStatus atamasını burda gerçekleştirdik.
            using (var connection = _context.CreateConnection())
            {
                //ExecuteAsync - veritabanında değişiklik yapmak (INSERT, UPDATE, DELETE gibi) için kullanılır 
                await connection.ExecuteAsync(query, parameters);
            }

        }
        
        //Silme
        public async  void DeleteCategory(int id)
        {
            string query = "Delete From Category Where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            string query = "Select * From Category"; //sorgu gönderdik.
            using (var connection = _context.CreateConnection()) // Disposable pattern  uygulayarak connection kullanımı
            {
                //Dapper -  
                //QueryAsync -  veri sorgulamak ve sonuç seti almak için kullanılır.
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdCategoryDto> GetCategory(int id)
        {
            string query = "Select * From Category Where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                //QueryFirstOrDefaultAsync - ilk kaydı döner. Eğer sonuç setinde hiç kayıt yoksa, varsayılan değeri (genellikle null) döner

                var values = await connection.QueryFirstOrDefaultAsync<GetByIdCategoryDto>(query,parameters);
                return values;
            }
        }

        //Güncelleme
        public async void UpdateCategory(UpdateCategoryDto categoryDto)
        {
            string query= "Update Category Set CategoryName=@categoryName,CategoryStatus=@categoryStatus Where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", categoryDto.CategoryStatus);
            parameters.Add("@categoryID", categoryDto.CategoryID);

            using (var connection= _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }
    }
}
