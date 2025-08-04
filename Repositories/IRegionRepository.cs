using System.Collections.Generic;
using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region>GetByIdAsync(int id);

        Task<Region>CreateRegionAsync(Region regin);

        Task<Region>UpdateReginAsync(Region regin, int id);

        Task<Region> DeleteReginAsync(int id);
    }
}
