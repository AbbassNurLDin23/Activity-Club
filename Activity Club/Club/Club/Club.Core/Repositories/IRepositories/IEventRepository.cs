using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories.IRepositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<List<EventView>> GetAllEvents();
        Task<bool> UpdateEvent(int email, EventView view);
        Task<Event?> GetEventById(int id);
        Task<EventView?> AddEvent( EventView EventView);
        Task<bool> DeleteEvent(int id);
        Task<bool> UpcomingEvent(int id);
        Task<bool> HasMember(string email, int id);
        Task<bool> HasGuide(string email, int id);
        Task<List<GuideView?>> AvailableGuides (int id);
        //Task<bool> AddAdminToEvent(string email, int id);
        Task<bool> AddGuideToEvent(string email, int id);
        Task<bool> AddMemberToEvent(string email, int id);
        //Task<bool> AddLookupToEvent(int order, int id);
    }
}
