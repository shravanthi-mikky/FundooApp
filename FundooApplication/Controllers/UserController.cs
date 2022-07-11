﻿using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL iUserBL;
        public UserController(IUserBL iUserBL)
        {
            this.iUserBL = iUserBL;
        }
        [HttpPost("Register")]
        public IActionResult Register(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result= iUserBL.Register(userRegistrationModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Registration Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessfull"});
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            try
            {
                var result = iUserBL.Login(userLoginModel);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Login Successfull",
                        token = result
                    }  
                        );
                }
                else
                {
                    return this.Unauthorized(new
                    {
                        Success = false,
                        message = "Invalid Email or Password",
                    } );
                }

            }
            catch
            {
                throw;
            }
        }

    }
}
