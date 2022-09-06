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
using System.Security.Cryptography;

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
                userEntity.Password = EncryptPass(userRegistrationModel.Password);
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
        //Encryt/decrypt methods

        public string Decrpt(string encodedData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string EncryptPass(string password)
        {
            try
            {
                string msg = "";
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                msg = Convert.ToBase64String(encode);
                return msg;
            }
            catch (Exception)
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
                Expires = DateTime.UtcNow.AddMinutes(200),
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
                var loginData = userContext.UsersTable.SingleOrDefault(x => x.Email == userLoginModel.Email );
                string pswd = Decrpt(loginData.Password);
                if (loginData != null && pswd == userLoginModel.Password)
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
        public string ForgetPassword(string Email) 
        {
            try
            {
                var emailCheck = userContext.UsersTable.FirstOrDefault(x => x.Email == Email);
                if(emailCheck != null)
                {
                    var token = JwtMethod(emailCheck.Email, emailCheck.UserId);
                    MSMQ_Model msmq_Model = new MSMQ_Model();
                    msmq_Model.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex){
                throw;
            }
        }
        //reset
        public bool ResetPassword(string email, string password, string confirmpassword)
        {
            try
            {
                if (password.Equals(confirmpassword))
                {
                    UserEntity user = userContext.UsersTable.Where(e => e.Email == email).FirstOrDefault();
                    user.Password = confirmpassword;
                    userContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
} 
