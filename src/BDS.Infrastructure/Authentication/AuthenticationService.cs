using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BDS.Infrastructure.Authentication;

public class AuthenticationService
{
    public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    private const string queueName = "UserRegistered";
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;


    public async Task Register()
    {
        throw new NotImplementedException();
        // var identityUser = new IdentityUser
        // {
        //     UserName = user.Email,
        //     Email = user.Email,
        //     EmailConfirmed = true
        // };
        //
        // var result = await _userManager.CreateAsync(identityUser, user.Password);
        //
        // if (result.Succeeded)
        // {
        //     // var registerClient =
        //     //     new RegisterUserIntegrationEvent(Guid.Parse(identityUser.Id), user.FullName(),
        //     //         user.Email.Value, new Cpf("20612367070"));
        //     // var clientJSon = JsonSerializer.Serialize(registerClient);
        //     // var clientInfoBytes = Encoding.UTF8.GetBytes(clientJSon);
        //
        //     // _messageBusService.Publish(queueName, clientInfoBytes);
        //     var token = await GenerateToken(identityUser);
        //     return BaseResponse<string>.SuccessResponse(token, "Usuário registrado com sucesso.");
    }


    public string GenerateJWTToken(string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        ;

        var claims = new List<Claim>
        {
            new Claim("userName", email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials,
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }

    public string ComputeSha256Hash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}