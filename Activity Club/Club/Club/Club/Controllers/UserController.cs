using AutoMapper;
using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Resources;
using Club.Service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService UserService, IMapper mapper)
        {
            _userService = UserService;
            _mapper = mapper;
        }

        //Post method to create a new user
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserResource UserResource)
        {
            if (UserResource == null)
                return BadRequest("Please fill the required information");
            if(UserResource.Email == null || UserResource.Email == "")
                return BadRequest("Email should not be empty or null.");
            var newUser = _mapper.Map<UserResource, User>(UserResource);
            var creation = await _userService.CreateUser(newUser);
            if(creation == false)
            {
                return BadRequest("Invalid Email.");
            }
            return Ok(newUser);
        }

        //Get methid to get all existing users
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var results = await _userService.GetAllUsers();
            return Ok(results);
        }

        //Get method to get all the admins
        [HttpGet("GetAllAdmins")]
        [Authorize]
        public async Task<IActionResult> GetAllAdmins()
        {
            var results = await _userService.GetAllAdmins();
            return Ok(results);
        }
        
        //Get method to get all users who are not admin
        [HttpGet("GetAllNonAdmins")]
        [Authorize]
        public async Task<IActionResult> GetAllNonAdmins()
        {
            var results = await _userService.GetAllNonAdmins();
            return Ok(results);
        }

        [HttpGet("GetUserType")]
        public async Task<IActionResult> GetUserType(string email)
        {
            var results = await _userService.GetUserType(email);
            return Ok(results);
        }

        //Get methid to get all the users who have not joined yet any event
        [HttpGet("GetAllNonMembers")]
        [Authorize]
        public async Task<IActionResult> GetAllNonMembers()
        {
            var results = await _userService.GetAllNonMembers();
            return Ok(results);
        }

        //get method to get all the users that are guides in an event
        [HttpGet("GetAllNonGuides")]
        [Authorize]
        public async Task<IActionResult> GetAllNonGuides()
        {
            var results = await _userService.GetAllNonGuides();
            return Ok(results);
        }

        //Put method to let a user to become admin
        [HttpPut("AddAdmins")]
        [Authorize]
        public async Task<bool?> AddAdmin(string email)
        {
            return await _userService.AddAdmin(email);
        }

        //Delete  method to let an admin to become just a user
        [HttpPut("DeleteAdmins")]
        [Authorize]
        public async Task<bool?> DeleteAdmin(string email)
        {
            return await _userService.DeleteAdmin(email);
        }


        //Get method to get any user in the database using his/her email
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var results = await _userService.GetUserWithEmail(email);
            return Ok(results);
        }

        //Put method to update the information of any user 
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string email, UserRes resource)
        {
            var results = await _userService.UpdateUser(email, resource);
            return Ok(results);
        }

        //Delete methid to delete a user from the database using his/her email
        [HttpDelete("DeleteUser")]
        [Authorize]

        public async Task<IActionResult> DeleteUser(string email)
        {
            var results = await _userService.DeleteUser(email);
            return Ok("User deleted successfully");
        }

        //Get method to check a specific user if he/she has joined a specific event
        [HttpGet("BelongToEvent")]
        public async Task<bool> BelongToEvent(string email, int id)
        {
            return await _userService.BelonngToEvent(email, id);
        }

        //Put method to add a role to a specific user
        [HttpPut("AddUserRole")]
        public async Task<List<string>> AddUserRole(string email, string role)
        {
            return await _userService.AddUserRole(email, role);
        }

        
        //Put method to add an event to a user to manage it
        [HttpPut("AddEvent")]
        [Authorize]

        public async Task<IActionResult> AddEvent(int id, string email)
        {
            var results = await _userService.AddEventToUser(id, email);
            return Ok(results);
        }

        //Put method to add lookup to a user to manage it
        [HttpPut("AddLookup")]
        [Authorize]

        public async Task<IActionResult> AddLookup(int order, string email)
        {
            var results = await _userService.AddEventToUser(order, email);
            return Ok(results);
        }
    }
}
