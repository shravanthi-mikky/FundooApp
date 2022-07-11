using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        UserContext userContext;
        private readonly IConfiguration config;
        public UserRL(UserContext userContext, IConfiguration config)
        {
            this.userContext = userContext;
            this.config = config;
        }
        public UserEntity Register(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;
                userContext.UsersTable.Add(userEntity);
                int result= userContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                    return null;
            }
            catch
            {
                throw;
            } 
        }
        public string JwtMethod(string email, long id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                        new Claim(ClaimTypes.Email, email),
                        new Claim("id", id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(
                tokenKey, SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var loginData = userContext.UsersTable.SingleOrDefault(x => x.Email == userLoginModel.Email && x.Password == userLoginModel.Password);
                if (loginData != null)
                {
                    var token = JwtMethod(loginData.Email, loginData.UserId);
                    return token;
                }
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
