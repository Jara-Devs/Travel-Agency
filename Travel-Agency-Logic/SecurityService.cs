using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Travel_Agency_Core;

namespace Travel_Agency_Logic;

public static class MyClaims
{
    public const string Id = "id";
    public const string Name = "name";

    public const string Role = "role";
}

public class SecurityService
{
    private readonly IConfiguration _configuration;

    public SecurityService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string JwtAuth(Guid id, string name, string role)
    {
        var claims = new[]
        {
            new Claim(MyClaims.Id, id.ToString()),
            new Claim(MyClaims.Name, name),
            new Claim(MyClaims.Role, role)
        };

        var expires = DateTime.UtcNow.AddHours(2);

        return Jwt(expires, claims);
    }
    
    private string Jwt(DateTime expires, IEnumerable<Claim> claims)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }

    public static ApiResponse<UserBasic> DecodingAuth(string authHeader)
    {
        var response = DecodingToken(authHeader);
        if (!response.Ok) return response.ConvertApiResponse<UserBasic>();

        var jwtToken = response.Value!;

        var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == MyClaims.Id);
        var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == MyClaims.Name);
        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == MyClaims.Role);

        if (idClaim is null || nameClaim is null || roleClaim is null)
            return new ApiResponse<UserBasic>(HttpStatusCode.Unauthorized, "Unauthorized");

        try
        {
            return new ApiResponse<UserBasic>(new UserBasic(Guid.Parse(idClaim.Value), nameClaim.Value,
                roleClaim.Value));
        }
        catch
        {
            return new ApiResponse<UserBasic>(HttpStatusCode.Unauthorized, "Unauthorized");
        }
    }
    
    private static ApiResponse<JwtSecurityToken> DecodingToken(string authHeader, bool bearer = true)
    {
        string token;

        if (bearer)
        {
            if (!authHeader.StartsWith("Bearer "))
                return new ApiResponse<JwtSecurityToken>(HttpStatusCode.Unauthorized, "Unauthorized");

            token = authHeader.Substring("Bearer ".Length).Trim();
        }
        else
        {
            token = authHeader;
        }

        var handler = new JwtSecurityTokenHandler();
        return new ApiResponse<JwtSecurityToken>(handler.ReadJwtToken(token));
    }

    public (string user, string password) AdminCredentials()
    {
        var user = _configuration["Admin:User"];
        var password = _configuration["Admin:Password"];

        return (user!, password!);
    }
    
    public static string EncryptPassword(string password)
    {
        var sal = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(sal);
        }

        var pbkdf2 = new Rfc2898DeriveBytes(password, sal, 10000, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(20);

        var hashBytes = new byte[36];
        Array.Copy(sal, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }

    public static bool CheckPassword(string password, string current)
    {
        var hashBytes = Convert.FromBase64String(password);

        var sal = new byte[16];
        Array.Copy(hashBytes, 0, sal, 0, 16);
        var hash = new byte[20];
        Array.Copy(hashBytes, 16, hash, 0, 20);

        var pbkdf2 = new Rfc2898DeriveBytes(current, sal, 10000, HashAlgorithmName.SHA256);
        var hashPass = pbkdf2.GetBytes(20);

        for (var i = 0; i < 20; i++)
            if (hash[i] != hashPass[i])
                return false;

        return true;
    }

    public static string RandomPassword()
    {
        var alphabet = new List<string>(26 * 2 + 10);

        for (var i = 0; i < 25; i++)
        {
            alphabet.Add(((char)('A' + i)).ToString());
            alphabet.Add(((char)('a' + i)).ToString());
        }

        for (var i = 0; i < 10; i++) alphabet.Add(i.ToString());

        var result = "";
        var rnd = new Random();

        for (var i = 0; i < 8; i++) result += alphabet[rnd.Next(alphabet.Count)];

        return result;
    }
}