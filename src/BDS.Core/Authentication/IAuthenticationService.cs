namespace BDS.Core.Authentication;

public interface IAuthenticationService
{
    string GenerateJWTToken(string email, string role);
    string ComputeSha256Hash(string password);
}