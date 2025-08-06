using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreatWalkAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sotyBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);

        Task<Walk> GetWalByIdAsync(int id);

        Task<Walk> UpdateWalkAsync(Walk walk, int id);

        Task<Walk> DeleteWalkAsync(int id);
    }
}
