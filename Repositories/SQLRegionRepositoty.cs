using Microsoft.EntityFrameworkCore;
using UdamyCourse.Data;
using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public class SQLRegionRepositoty : IRegionRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public SQLRegionRepositoty(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task<Region> CreateRegionAsync(Region regin)
        {
            await dataBaseContext.AddAsync(regin);
            await dataBaseContext.SaveChangesAsync();
            return regin;
        }

        public async Task<Region> DeleteReginAsync(int id)
        {
            var existingRegion = await dataBaseContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            dataBaseContext.Regions.Remove(existingRegion);
            await dataBaseContext.SaveChangesAsync();
            return existingRegion;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dataBaseContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(int id)
        {
            var region = await dataBaseContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region> UpdateReginAsync(Region regin, int id)
        {
            var Currentregion = await dataBaseContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
            if (Currentregion == null)
            {
                return null;
            }
            Currentregion.Code = regin.Code;
            Currentregion.Name = regin.Name;
            Currentregion.RegionImageUrl = regin.RegionImageUrl;

            await dataBaseContext.SaveChangesAsync();
            return Currentregion;
            
        }
    }
}
