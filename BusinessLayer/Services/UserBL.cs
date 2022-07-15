using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL iUserRL;
        public UserBL(IUserRL iUserRL)
        {
            this.iUserRL = iUserRL;
        }

        public UserEntity Register(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iUserRL.Register(userRegistrationModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return iUserRL.Login(userLoginModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                return iUserRL.ForgetPassword(Email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ResetPassword(string Email, string Password, string confirmpassword)
        {
            try
            {
                return iUserRL.ResetPassword(Email,Password, confirmpassword);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
