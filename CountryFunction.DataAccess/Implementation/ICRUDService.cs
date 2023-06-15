using CountryFunction.Domain.Model;

namespace CountryFunction.DataAccess.Implementation
{
    public interface ICRUDService
    {
        Task<Country> CreateAsync(Country country);
        Task<Country> ReadAsync(int id);
        Task<Country> UpdateAsync(int id, Country country);
        Task<bool> DeleteAsync(int id);
        Task<List<Country>> GetAllAsync();
    }
}
