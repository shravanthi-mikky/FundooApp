using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        UserContext userContext;
        public UserRL(UserContext userContext)
        {
            this.userContext = userContext;

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
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
