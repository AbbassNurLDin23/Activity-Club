using AutoMapper;
using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Resources;
using Club.Service.Services;
using Club.Service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MemberController : Controller
    {
        private readonly IMemberService _MemberService;
        private readonly IMapper _mapper;

        public MemberController(IMemberService MemberService, IMapper mapper)
        {
            _MemberService = MemberService;
            _mapper = mapper;
        }


        [HttpPost("CreateMember")]
        public async Task<IActionResult> CreateMember(string email, MemberRes resource)
        {
            if (resource == null)
                return BadRequest("Please fill the required information");
            if (email == null || email == "")
                return BadRequest("Email should not be empty or null.");
            //var newMember = _mapper.Map<MemberResource, Member>(MemberResource);
            var creation = await _MemberService.CreateMember(email, resource);
            if (creation == false)
            {
                return BadRequest("Invalid Email.");
            }
            //await _MemberService.CreateMember(newMember);
            return Ok(resource);
        }

        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers()
        {
            var results = await _MemberService.GetAllMembers();
            return Ok(results);
        }

        [HttpGet("GetMemberByEmail")]
        public async Task<IActionResult> GetMemberByEmail(string email)
        {
            var results = await _MemberService.GetMemberWithEmail(email);
            return Ok(results);
        }

        [Authorize]
        [HttpPut("UpdateMember")]
        public async Task<IActionResult> UpdateMember(string email, MemberView resource)
        {
            var results = await _MemberService.UpdateMember(email, resource);
            return Ok(results);
        }

        [Authorize]
        [HttpDelete("DeleteMember")]
        public async Task<IActionResult> DeleteMember(string email)
        {
            var results = await _MemberService.DeleteMember(email);
            return Ok("Member deleted successfully");
        }

        
        [HttpPut("AddEvent")]
        public async Task<IActionResult> AddEvent(int id, string email)
        {
            var results = await _MemberService.AddEventToMember(id, email);
            return Ok(results);
        }


        [Authorize]
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents(string email)
        {
            var results = await _MemberService.GetEvents(email);
            return Ok(results);
        }

    }
}
