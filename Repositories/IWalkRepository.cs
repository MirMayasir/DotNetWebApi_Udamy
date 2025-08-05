using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreatWalkAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();

        Task<Walk> GetWalByIdAsync(int id);

        Task<Walk> UpdateWalkAsync(Walk walk, int id);

        Task<Walk> DeleteWalkAsync(int id);
    }
}
