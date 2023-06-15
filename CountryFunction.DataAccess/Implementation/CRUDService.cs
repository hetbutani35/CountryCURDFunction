using CountryFunction.DataAccess.Context;
using CountryFunction.Domain.Model;
using System.Data.Entity;

namespace CountryFunction.DataAccess.Implementation
{
    public class CRUDService : ICRUDService
    {
        private readonly DataContext _dataContext;
        public CRUDService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Country> CreateAsync(Country country)
        {
            if (await CountryExists(country.CountryName))
                return null;
            _dataContext.country.Add(country);
            await _dataContext.SaveChangesAsync();
            return country;
        }

        public async Task<Country> ReadAsync(int id)
        {
            var country = await _dataContext.country.FindAsync(id);
            return country;
        }

        public async Task<Country> UpdateAsync(int id, Country country)
        {
            var countrytoupdate = await ReadAsync(id);
            if (countrytoupdate == null || country == null)
                return null;
            countrytoupdate.CountryName = country.CountryName;
            countrytoupdate.CountryCode = country.CountryCode;
            countrytoupdate.CreatedBy = country.CreatedBy;
            await _dataContext.SaveChangesAsync();
            return countrytoupdate;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var country = await ReadAsync(id);
            if(country == null) 
                return false;
            _dataContext.country.Remove(country);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            var countrylist = await _dataContext.country.ToListAsync();
            return countrylist;
        }

        public async Task<bool> CountryExists(string cname)
        {
            return await _dataContext.country.FirstOrDefaultAsync(x => x.CountryName == cname) != null;
        }
    }
}
