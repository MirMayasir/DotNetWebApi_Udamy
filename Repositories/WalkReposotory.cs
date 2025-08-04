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

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dataBaseContext.Walks.ToListAsync();
        }
    }
}
