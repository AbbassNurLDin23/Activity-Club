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
    
    public class EventController : Controller
    {
        private readonly IEventService _EventService;
        private readonly IMapper _mapper;

        public EventController(IEventService EventService, IMapper mapper)
        {
            _EventService = EventService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent(EventView view)
        {
            if (view == null)
                return BadRequest("Please fill the required information");
            //var newEvent = _mapper.Map<EventResource, Event>(view);
            await _EventService.CreateEvent(view);
            return Ok(view);
        }

        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            var results = await _EventService.GetAllEvents();
            return Ok(results);
        }

        [Authorize]
        [HttpGet("GetEventById")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var results = await _EventService.GetEventWithId(id);
            return Ok(results);
        }


        [Authorize]
        [HttpGet("GetAvailableGuides")]
        public async Task<IActionResult> GetAvailableGuides(int id)
        {
            var results = await _EventService.AvailableGuides(id);
            return Ok(results);
        }

        [Authorize]
        [HttpPut("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(int id, EventView resource)
        {
            var results = await _EventService.UpdateEvent(id, resource);
            return Ok(results);
        }

        [Authorize]
        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var results = await _EventService.DeleteEvent(id);
            return Ok("Event deleted successfully");
        }


        [HttpGet("UpcomingEvent")]
        public async Task<bool> UpcomingEvent (int id)
        {
            return await _EventService.UpcomingEvent(id);
        }

        [HttpGet("HasMember")]
        public async Task<bool> HasMember(string email, int id)
        {
            return await _EventService.HasMember(email, id);
        }


        [HttpGet("HasGuide")]
        public async Task<bool> HasGuide(string email, int id)
        {
            return await _EventService.HasGuide(email, id);

        }


        //[Authorize]
        //[HttpPut("AddAdmin")]
        //public async Task<IActionResult> AddAdmin(string email, int id)
        //{
        //    var results = await _EventService.AddAdminToEvent(email, id);
        //    return Ok(results);
        //}

        [Authorize]
        [HttpPut("AddGuide")]
        public async Task<IActionResult> AddGuide(string email, int id)
        {
            var results = await _EventService.AddGuideToEvent(email, id);
            return Ok(results);
        }

        [HttpPut("AddMember")]
        public async Task<IActionResult> AddMember(string email, int id)
        {
            var results = await _EventService.AddMemberToEvent(email, id);
            return Ok(results);
        }

        //[Authorize]
        //[HttpPut("AddLookup")]
        //public async Task<IActionResult> AddLookup(int order, int id)
        //{
        //    var results = await _EventService.AddLookupToEvent(order, id);
        //    return Ok(results);
        //}
    }
}
