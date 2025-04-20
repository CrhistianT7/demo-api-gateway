using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginRequest req)
  {
    if (req.Username == "admin" && req.Password == "password")
    {
      var claims = new[] {
                new Claim(ClaimTypes.Name, req.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Secret));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          claims: claims,
          expires: DateTime.UtcNow.AddHours(1),
          signingCredentials: creds);

      return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    return Unauthorized();
  }

  public record LoginRequest(string Username, string Password);
}
