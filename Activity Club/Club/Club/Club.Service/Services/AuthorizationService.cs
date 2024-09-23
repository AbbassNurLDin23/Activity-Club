using Club.Core.Repositories.IRepositories;
using Club.Service.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class AuthorizationService : IAuthorizationService
{
    //private readonly IAuthorizationRepository _authorizationRepository;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork unitOfWork;


    public AuthorizationService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        //_authorizationRepository = authorizationRepository;
        _configuration = configuration;
        this.unitOfWork = unitOfWork;

    }

    public async Task<IActionResult> GetToken(string email, string password)
    {
        try
        {
            var userFound = await unitOfWork.Authorizations.Authorize(email, password);

            // Validate email and password
            if (userFound == false)
            {
                return new BadRequestObjectResult("Invalid account, or email or password is incorrect.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new OkObjectResult(tokenString); // Return the token string as an OK response
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new StatusCodeResult(500); // Return a 500 Internal Server Error
        }
    }

    public async Task<IActionResult> CheckAccount(string email, string password)
    {
        try
        {
            var userFound = await unitOfWork.Authorizations.CheckUserEmailAndPassword(email, password);
            // Validate email and password
            if (userFound == false)
            {
                return new BadRequestObjectResult("Email or password is incorrect.");
            }
            return new OkObjectResult(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new StatusCodeResult(500); // Return a 500 Internal Server Error
        }
    }
}
