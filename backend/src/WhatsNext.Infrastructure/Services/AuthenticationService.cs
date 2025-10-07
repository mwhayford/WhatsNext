// <copyright file="AuthenticationService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WhatsNext.Application.Common.Interfaces;
using WhatsNext.Application.Common.Models;
using WhatsNext.Domain.Entities;

namespace WhatsNext.Infrastructure.Services;

/// <summary>
/// Service for handling authentication operations.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration configuration;
    private readonly IApplicationDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="context">The database context.</param>
    public AuthenticationService(IConfiguration configuration, IApplicationDbContext context)
    {
        this.configuration = configuration;
        this.context = context;
    }

    /// <inheritdoc/>
    public AuthenticationResult GenerateToken(User user)
    {
        var jwtSettings = this.configuration.GetSection("JwtSettings");
        var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret is not configured.");
        var issuer = jwtSettings["Issuer"] ?? "WhatsNext";
        var audience = jwtSettings["Audience"] ?? "WhatsNext";
        var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"] ?? "60");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("username", user.Username),
        };

        // Add roles as claims
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Generate refresh token (simplified - in production, store this securely)
        var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        return new AuthenticationResult
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            Token = tokenString,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            Roles = user.Roles.ToList(),
        };
    }

    /// <inheritdoc/>
    public async Task<AuthenticationResult?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        // In a production app, you would:
        // 1. Validate the refresh token against stored tokens in the database
        // 2. Check if it's expired
        // 3. Generate a new access token
        // 4. Optionally rotate the refresh token

        // For now, returning null (simplified implementation)
        await Task.CompletedTask;
        return null;
    }

    /// <inheritdoc/>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
