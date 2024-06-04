using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Users;

namespace WebApp.Authentication;

public class JwtService(JwtSettings jwtSettings) {
    private readonly SymmetricSecurityKey _signingKey = 
        new(Encoding.UTF8.GetBytes(jwtSettings.Key));

    public JwtSettings JwtSettings { get; } = jwtSettings;

    public string GenerateToken(int userId, string username, string name, UserType userRole) {
        var claims = new[] {
            new Claim("user_id", userId.ToString()),
            new Claim("username", username),
            new Claim("name", name),
            new Claim("user_type", ((int)userRole).ToString())
        };

        var signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(JwtSettings.TokenLifetime),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}