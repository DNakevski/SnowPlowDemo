﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnowplowShoppingApp.Models;
using SnowplowShoppingApp.Repositories;
using SnowplowShoppingApp.Services;

namespace SnowplowShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ITrackingService _trackingService;

        public UsersController(IUserRepo userRepo, ITrackingService trackingService)
        {
            _userRepo = userRepo;
            _trackingService = trackingService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepo.GetAllUsersAsync();
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userRepo.LoginAsync(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                //invalid login, track the unsuccessful login event
                _trackingService.TrackUserUnsuccessfulLoginEvent(loginModel.Email);
                return BadRequest("Invalid email or password");
            }
            
            //track the user successful login event
            _trackingService.TrackUserLoginEvent(loginModel.Email);
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutModel logoutModel)
        {
            //nothing in particular happens here, we just track the user logout event
            _trackingService.TrackUserLogoutEvent(logoutModel.Email);
            return Ok();
        }
    }
}
