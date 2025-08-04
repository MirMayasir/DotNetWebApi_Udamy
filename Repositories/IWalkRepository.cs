using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreatWalkAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
    }
}
