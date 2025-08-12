using Microsoft.AspNetCore.Identity;

namespace UdamyCourse.Repositories
{
    public interface ITokenRepository
    {
        string createJWTToken(IdentityUser user, List<string> roles);
    }
}
