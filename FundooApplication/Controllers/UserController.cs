using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    //[Route("api/[controller]")]
   // [ApiController]

    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
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
        [HttpPost("Forget")]
        public IActionResult Forget(string Email)
        {
            try
            {
                string token = iUserBL.ForgetPassword(Email);
                if (token != null)
                {
                    return Ok(new { Success = true, Message = "Please check your Email.Token sent succesfully." });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Email not registered" });
                }
            }
            catch
            {
                throw;
            }
        }

        [Authorize]
        [HttpPost("Reset")]
        
        public IActionResult ResetPassword(string Password,string ConfirmPassword)
        {
            try
            {
                //var email = User.Claims.First(e => e.Type == "Email").Value;
                var email = User.FindFirst(ClaimTypes.Email).Value;
                var result = iUserBL.ResetPassword(email, Password, ConfirmPassword);
                if (result == null)
                {
                    return this.BadRequest(new { Success = false, Message = "Something went wrong! Please try again." });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Password is changed Succesfully" ,result = result});
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
