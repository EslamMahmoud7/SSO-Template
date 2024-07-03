using SSO_Template.Models;

namespace SSO_Template.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
        string RefreshToken(string token);
    }
}
