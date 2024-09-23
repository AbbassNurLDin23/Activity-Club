﻿using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services.IServices
{
    public interface IEventService
    {
        Task<List<EventView>?> GetAllEvents();
        Task<Event?> GetEventWithId(int id);
        Task<bool> CreateEvent(EventView Event);
        Task<bool> UpdateEvent(int id, EventView view);
        Task<List<GuideView?>> AvailableGuides(int id);
        Task<bool> DeleteEvent(int id);
        Task<bool> UpcomingEvent(int id);
        Task<bool> HasMember(string email, int id);
        Task<bool> HasGuide(string email, int id);
        //Task<bool> AddAdminToEvent(string email, int id);
        Task<bool> AddGuideToEvent(string email, int id);
        Task<bool> AddMemberToEvent(string email, int id);
        //Task<bool> AddLookupToEvent(int order, int id);
    }
}
