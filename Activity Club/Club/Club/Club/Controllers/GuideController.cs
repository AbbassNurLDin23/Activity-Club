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
    public class GuideController : Controller
    {
        private readonly IGuideService _GuideService;
        private readonly IMapper _mapper;

        public GuideController(IGuideService GuideService, IMapper mapper)
        {
            _GuideService = GuideService;
            _mapper = mapper;
        }


        [HttpPost("CreateGuide")]
        public async Task<IActionResult> CreateGuide(string email, GuideRes resource)
        {
            if (resource == null)
                return BadRequest("Please fill the required information");
            if (email == null || email == "")
                return BadRequest("Email should not be empty or null.");
            //var newGuide = _mapper.Map<GuideResource, Guide>(GuideResource);
            var creation = await _GuideService.CreateGuide(email, resource);
            if (creation == false)
            {
                return BadRequest("Invalid Email.");
            }
            //await _GuideService.CreateGuide(newGuide);
            return Ok(resource);
        }

        [HttpGet("GetAllGuides")]
        public async Task<IActionResult> GetAllGuides()
        {
            var results = await _GuideService.GetAllGuides();
            return Ok(results);
        }

        [HttpGet("GetGuideByEmail")]
        public async Task<IActionResult> GetGuideByEmail(string email)
        {
            var results = await _GuideService.GetGuideWithEmail(email);
            return Ok(results);
        }

        [Authorize]
        [HttpPut("UpdateGuide")]
        public async Task<IActionResult> UpdateGuide(string email, GuideView resource)
        {
            var results = await _GuideService.UpdateGuide(email, resource);
            return Ok(results);
        }

        [Authorize]
        [HttpDelete("DeleteGuide")]
        public async Task<IActionResult> DeleteGuide(string email)
        {
            var results = await _GuideService.DeleteGuide(email);
            return Ok("Guide deleted successfully");
        }

        [Authorize]
        [HttpPut("AddEvent")]
        public async Task<IActionResult> AddEvent(int id, string email)
        {
            var results = await _GuideService.AddEventToGuide(id, email);
            return Ok(results);
        }


        [Authorize]
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents( string email)
        {
            var results = await _GuideService.GetEvents( email);
            return Ok(results);
        }
    }


}
