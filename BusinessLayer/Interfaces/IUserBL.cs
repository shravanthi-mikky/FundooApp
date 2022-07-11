using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserEntity Register(UserRegistrationModel userRegistrationModel);
        public string Login(UserLoginModel userLoginModel);
    }
}
