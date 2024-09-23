using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories;
using Club.Core.Repositories.IRepositories;
using Club.Service.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MyDataContext _DataContext;

        public EventService(IUnitOfWork unitOfWork, MyDataContext dataContext)
        {
            this.unitOfWork = unitOfWork;
            _DataContext = dataContext;
        }

        public async Task<List<EventView>?> GetAllEvents()
        {
            try
            {
                return await unitOfWork.Events.GetAllEvents();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Event?> GetEventWithId(int id)
        {
            try
            {
                return await unitOfWork.Events.GetEventById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateEvent(EventView Event)
        {
            try
            {
                await unitOfWork.Events.AddEvent(Event);
                await unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<GuideView?>> AvailableGuides(int id)
        {
            try
            {
                return await unitOfWork.Events.AvailableGuides(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateEvent(int id, EventView view)
        {
            try
            {
                await unitOfWork.Events.UpdateEvent(id, view);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpcomingEvent(int id)
        {
            try
            {
                return await unitOfWork.Events.UpcomingEvent(id);
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteEvent(int id)
        {
            try
            {
                await unitOfWork.Events.DeleteEvent(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<bool> HasMember(string email, int id)
        {
            try
            {
                return await unitOfWork.Events.HasMember(email, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> HasGuide(string email, int id)
        {
            try
            {
                return await unitOfWork.Events.HasGuide(email, id);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //public async Task<bool> AddAdminToEvent(string email, int id)
        //{
        //    try
        //    {
        //        await unitOfWork.Events.AddAdminToEvent(email, id);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        public async Task<bool> AddGuideToEvent(string email, int id)
        {
            try
            {
                await unitOfWork.Events.AddGuideToEvent(email, id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddMemberToEvent(string email, int id)
        {
            try
            {
                await unitOfWork.Events.AddMemberToEvent(email, id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //public async Task<bool> AddLookupToEvent(int order, int id)
        //{
        //    try
        //    {
        //        await unitOfWork.Events.AddLookupToEvent(order, id);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
    }
}
