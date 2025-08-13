using UdamyCourse.Model.Domain;

namespace UdamyCourse.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
