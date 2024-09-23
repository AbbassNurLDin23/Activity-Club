using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Club.Core.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly MyDataContext _DataContext;
        public EventRepository(MyDataContext dataContext) : base(dataContext)
        {
            _DataContext = dataContext;
        }

        public async Task<List<EventView>> GetAllEvents()
        {
            return await _DataContext.Events
                .OrderByDescending(l => l.DateFrom) // Order by DateFrom in descending order
                .Select(l => new EventView
                {
                    id = l.id,
                    Name = l.Name,
                    Description = l.Description,
                    Cost = l.Cost,
                    Status = l.Status,
                    Category = l.Category,
                    Destination = l.Destination,
                    DateFrom = l.DateFrom,
                    DateTo = l.DateTo,                
                })
                .ToListAsync();
        }


        public async Task<bool> UpdateEvent(int id, EventView view)
        {
            var eventToUpdate = await _DataContext.Events
                .Where(l => l.id == id)
                .FirstOrDefaultAsync();

            if (eventToUpdate == null)
            {
                return false; // Event not found
            }
            if (view.DateFrom >= view.DateTo)
            {
                return false;
            }
            // Update properties only if they have been provided
            if (!string.IsNullOrEmpty(view.Name))
            {
                eventToUpdate.Name = view.Name;
            }
            if (!string.IsNullOrEmpty(view.Description))
            {
                eventToUpdate.Description = view.Description;
            }
            if (view.Cost != default(decimal)) // Assuming cost is a value type and cannot be null
            {
                eventToUpdate.Cost = view.Cost;
            }
            if (!string.IsNullOrEmpty(view.Status))
            {
                eventToUpdate.Status = view.Status;
            }
            if (view.Category != null)
            {
                if (!_DataContext.Lookups.Where(l => l.Order == view.Category).Any()) return false;
                eventToUpdate.Category = view.Category;
                var lookup = _DataContext.Lookups.FirstOrDefault(l => l.Order == view.Category);
                eventToUpdate.lookup = lookup;
            }
            if (!string.IsNullOrEmpty(view.Destination))
            {
                eventToUpdate.Destination = view.Destination;
            }
            if (view.DateFrom != default(DateTime)) // Assuming DateFrom is a value type
            {
                eventToUpdate.DateFrom = view.DateFrom;
            }
            if (view.DateTo != default(DateTime)) // Assuming DateTo is a value type
            {
                eventToUpdate.DateTo = view.DateTo;
            }

            // Save changes to the database
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GuideView?>> AvailableGuides(int id)
        {
            var ev = await _DataContext.Events
                        .Include(e => e.Guides) // Include the Guides to ensure they are loaded
                        .FirstOrDefaultAsync(e => e.id == id);

            // If the event is null or has no guides, return an empty list
            if (ev == null || ev.Guides == null)
            {
                return new List<GuideView>();
            }

            var existingGuideEmails = ev.Guides.Select(g => g.Email).ToList();  // Get the list of emails of current guides
            var availableGuides = await _DataContext.Guides
                                    .Where(g => !existingGuideEmails.Contains(g.Email)) // Compare by Email (or another unique property)
                                    .Select(g => new GuideView
                                    {
                                        Name = g.Name,
                                        Email = g.Email,
                                        Profession = g.Profession
                                        // Map other necessary fields to GuideView
                                    })
                                    .ToListAsync();

            return availableGuides;
        }


        public async Task<bool> UpcomingEvent(int id)
        {
            var Event = await _DataContext.Events.Where(l => l.id == id).FirstOrDefaultAsync();
            if (Event == null || Event.DateFrom < DateTime.Today)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteEvent(int id)
        {
            var Event = await _DataContext.Events.Where(l => l.id == id).FirstOrDefaultAsync();
            if (Event == null)
            {
                return false;
            }
            _DataContext.Remove(Event);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Event?> GetEventById(int id)
        {
            return await _DataContext.Events.Where(l => l.id == id)
                .Select(l => new Event
                {
                    id = l.id,
                    Name = l.Name,
                    Description = l.Description,
                    Cost = l.Cost,
                    Status = l.Status,
                    Category = l.Category,
                    Destination = l.Destination,
                    DateFrom = l.DateFrom,
                    DateTo = l.DateTo,
                    Guides = l.Guides,
                    Members = l.Members,
                }).FirstOrDefaultAsync();
        }

        public async Task<EventView?> AddEvent(EventView eventView)
        {
            // Check if the lookup exists based on the eventView.Category
            var lookup = _DataContext.Lookups.FirstOrDefault(l => l.Order == eventView.Category);
            if (lookup == null)
            {
                // If lookup doesn't exist, return null (or throw an exception)
                return null;
            }

            // Check if DateFrom is greater than or equal to DateTo
            if (eventView.DateFrom >= eventView.DateTo)
            {
                // Return null to indicate invalid date range
                return null;
            }

            // Check if DateFrom is before today's date
            if (eventView.DateFrom < DateTime.Now.Date)
            {
                // Return null to indicate invalid DateFrom
                return null;
            }

            // Create event entity and assign lookup
            var eventEntity = new Event
            {
                Name = eventView.Name,
                Description = eventView.Description,
                Cost = eventView.Cost,
                Status = eventView.Status,
                Category = eventView.Category,
                Destination = eventView.Destination,
                DateFrom = eventView.DateFrom,
                DateTo = eventView.DateTo,
                lookup = lookup,  // Assign lookup properly
            };

            // Add the event entity to the context and save changes
            _DataContext.Events.Add(eventEntity);
            await _DataContext.SaveChangesAsync();

            return eventView;
        }



        public async Task<bool> HasMember(string email, int id)
        {
            // Get the event with the specific ID and include its members
            var ev = await _DataContext.Events
                .Include(e => e.Members) // Ensure members are loaded
                .FirstOrDefaultAsync(e => e.id == id);

            if (ev == null) return false; // Return false if the event doesn't exist

            // Check if the event contains the member with the specified email
            var isMemberInEvent = ev.Members.Any(m => m.Email == email);

            return isMemberInEvent;
        }



        public async Task<bool> HasGuide(string email, int id)
        {
            // Get the event with the specific ID and include its members
            var ev = await _DataContext.Events
                .Include(e => e.Members) // Ensure members are loaded
                .FirstOrDefaultAsync(e => e.id == id);

            if (ev == null) return false; // Return false if the event doesn't exist

            // Check if the event contains the member with the specified email
            var isGuideInEvent = ev.Guides.Any(g => g.Email == email);

            return isGuideInEvent;
        }

        //public async Task<bool> AddAdminToEvent(string email, int id)
        //{
        //    User? admin = await _DataContext.Users.Where(l => l.Email == email).FirstOrDefaultAsync();
        //    if (admin == null)
        //    {
        //        return false;
        //    }
        //    Event? Event = await _DataContext.Events.Where(u => u.id == id).FirstOrDefaultAsync();
        //    if (Event == null)
        //    {
        //        return false; ;
        //    }
        //    if (Event.DateFrom <= DateTime.Today || Event.DateFrom >= Event.DateTo)
        //    {
        //        return false;
        //    }
        //    Event.Users ??= new List<User>();
        //    Event.Users.Add(admin);
        //    await _DataContext.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> AddGuideToEvent(string email, int id)
        {
            Guide? guide = await _DataContext.Guides.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (guide == null)
            {
                return false;
            }
            Event? Event = await _DataContext.Events.Where(u => u.id == id).FirstOrDefaultAsync();
            if (Event == null)
            {
                return false; ;
            }
            if (Event.DateFrom <= DateTime.Today || Event.DateFrom >= Event.DateTo)
            {
                return false;
            }
            Event.Guides ??= new List<Guide>();
            Event.Guides.Add(guide);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddMemberToEvent(string email, int id)
        {
            Member? member = await _DataContext.Members.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (member == null)
            {
                return false;
            }
            Event? ev = await _DataContext.Events.Where(u => u.id == id).FirstOrDefaultAsync();
            if (ev == null)
            {
                return false; ;
            }
            if (ev.DateFrom < DateTime.Today)
            {
                return false;
            }

            ev.Members ??= new List<Member>();
            ev.Members.Add(member);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        //    public async Task<bool> AddLookupToEvent(int order, int id)
        //    {
        //        Lookup? lookup = await _DataContext.Lookups.Where(l => l.Order == order).FirstOrDefaultAsync();
        //        if (lookup == null)
        //        {
        //            return false;
        //        }
        //        Event? Event = await _DataContext.Events.Where(u => u.id == id).FirstOrDefaultAsync();
        //        if (Event == null)
        //        {
        //            return false; ;
        //        }
        //        if (Event.DateFrom <= DateTime.Today || Event.DateFrom >= Event.DateTo)
        //        {
        //            return false;
        //        }
        //        Event.lookup = lookup;
        //        Event.lookupOrder = order;
        //        await _DataContext.SaveChangesAsync();
        //        return true;
        //    }
    }
}
