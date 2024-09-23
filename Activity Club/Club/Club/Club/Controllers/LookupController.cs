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
    [Authorize]

    public class LookupController : Controller
    {
        private readonly ILookupService _lookupService;
        private readonly IMapper _mapper;

        public LookupController(ILookupService lookupService, IMapper mapper)
        {
            _lookupService = lookupService;
            _mapper = mapper;
        }


        [HttpPost("CreateLookup")]
        public async Task<IActionResult> CreateLookup(LookupResource lookupResource)
        {
            if (lookupResource == null)
                return BadRequest("Please fill the required information");
            var newLookup = _mapper.Map<LookupResource, Lookup>(lookupResource);
            await _lookupService.CreateLookup(newLookup);
            return Ok(newLookup);
        }

        [HttpGet("GetAllLookups")]
        public async Task<IActionResult> GetAllLookups()
        {
            var results = await _lookupService.GetAllLookups();
            return Ok(results);
        }

        [HttpGet("GetLookupByOrder")]
        public async Task<IActionResult> GetLookupByOrder(int order)
        {
            var results = await _lookupService.GetLookupWithOrder(order);
            return Ok(results);
        }

        [HttpPut("UpdateLookup")]
        public async Task<IActionResult> UpdateLookup(int order, LookupView resource)
        {
            var results = await _lookupService.UpdateLookup(order, resource);
            return Ok(results);
        }

        [HttpDelete("DeleteLookup")]
        public async Task<IActionResult> DeleteLookup(int order)
        {
            var results = await _lookupService.DeleteLookup(order);
            return Ok("Lookup deleted successfully");
        }

        [HttpPut("AddAdmin")]
        public async Task<IActionResult> AddAdmin(string email, int  order)
        {
            var results = await _lookupService.AddAdminToLookup(email, order);
            return Ok(results);
        }

        [HttpPut("AddEvent")]
        public async Task<IActionResult> AddEvent(int id, int order)
        {
            var results = await _lookupService.AddEventToLookup(id, order);
            return Ok(results);
        }
    }
}
