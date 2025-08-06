using Microsoft.EntityFrameworkCore;
using UdamyCourse.Data;
using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public class WalkReposotory : IWalkRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public WalkReposotory(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        public async Task<Walk> CreatWalkAsync(Walk walk)
        {
            await _dataBaseContext.AddAsync(walk);
            await _dataBaseContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(int id)
        {
            var currWalk = await _dataBaseContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(currWalk == null)
            {
                return null;
            }

            _dataBaseContext.Walks.Remove(currWalk);
            await _dataBaseContext.SaveChangesAsync();
            return currWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn=null, string? filterQuery=null, string sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _dataBaseContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterOn) == false ){
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x=> x.Name.Contains(filterQuery));
                }
            }
            //Sorting

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase) )
                {
                    walks = isAscending ? walks.OrderBy(x=>x.Name) : walks.OrderByDescending(x=>x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x=>x.LengthInKm) : walks.OrderByDescending(x=> x.LengthInKm);
                }
            }
            //Pagination
            var skipResults = (pageNumber -1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            
            //return await _dataBaseContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetWalByIdAsync(int id)
        {
            var walk = await _dataBaseContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }

            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Walk walk, int id)
        {
            var currentWalk = await _dataBaseContext.Walks.FirstOrDefaultAsync(x=> x.Id == id);
            if(currentWalk == null)
            {
                return null;
            }

            currentWalk.Name = walk.Name;
            currentWalk.Description = walk.Description;
            currentWalk.WalkImageUrl = walk.WalkImageUrl;
            currentWalk.DifficultyId = walk.DifficultyId;
            currentWalk.LengthInKm = walk.LengthInKm;
            currentWalk.RegionId = walk.RegionId;

            await _dataBaseContext.SaveChangesAsync();
            return currentWalk;


        }
    }
}
