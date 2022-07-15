using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserEntity Register(UserRegistrationModel userRegistrationModel);
        public string Login(UserLoginModel userLoginModel);

        public string ForgetPassword(string Email);
        public bool ResetPassword(string email, string password, string confirmpassword);
    }
}
