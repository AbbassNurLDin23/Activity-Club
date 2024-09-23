using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories
{
    internal class GuideRepository : Repository<Guide>, IGuideRepository
    {
        private readonly MyDataContext _DataContext;
        public GuideRepository(MyDataContext dataContext) : base(dataContext)
        {
            _DataContext = dataContext;
        }

        public async Task<List<GuideView>> GetAllGuides()
        {

            return await _DataContext.Guides.Select(l => new GuideView
            {
                Email = l.Email,
                Name = l.Name,
                Password = l.Password,
                Roles = l.Roles,
                DOB = l.DOB,
                Gender = l.Gender,
                //Photo = l.Photo,
                Profession = l.Profession,
            }).ToListAsync();
        }

        public async Task<bool> UpdateGuide(string email, GuideView view)
        {
            var guide = await _DataContext.Guides
                .Where(l => l.Email == email)
                .FirstOrDefaultAsync();

            if (guide == null)
            {
                return false; // Guide not found
            }

            // Update fields only if the view property is not null or empty
            if (!string.IsNullOrEmpty(view.Password))
            {
                guide.Password = view.Password;
            }
            if (!string.IsNullOrEmpty(view.Name))
            {
                guide.Name = view.Name;
            }
            if (!string.IsNullOrEmpty(view.Gender))
            {
                guide.Gender = view.Gender;
            }
            if (view.DOB.HasValue) // Assuming DOB is a DateTime? (nullable DateTime)
            {
                guide.DOB = view.DOB.Value;
            }
            //if (!string.IsNullOrEmpty(view.Photo))
            //{
            //    guide.Photo = view.Photo;
            //}
            if (!string.IsNullOrEmpty(view.Profession))
            {
                guide.Profession = view.Profession;
            }
            if (view.Roles != null && view.Roles.Count > 0)
            {
                foreach (var role in view.Roles)
                {
                    if (!guide.Roles.Contains(role)) // Prevent duplicate roles
                    {
                        guide.Roles.Add(role);
                    }
                }
            }

            await _DataContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteGuide(string email)
        {
            var Guide = await _DataContext.Guides.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (Guide == null)
            {
                return false;
            }
            _DataContext.Remove(Guide);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Guide?> GetGuideByEmail(string email)
        {
            return await _DataContext.Guides.Where(l => l.Email == email)
                .Select(l => new Guide
                {

                    Email = email,
                    Name = l.Name,
                    Password = l.Password,
                    Roles = l.Roles,
                    DOB = l.DOB,
                    Gender = l.Gender,
                    //Photo = l.Photo,
                    Profession = l.Profession,
                    GuideEvents = l.GuideEvents,
                }).FirstOrDefaultAsync();
        }

        public async Task<List<EventView>> GetEvents(string email)
        {
            // Fetch the member along with related events
            var guide = await _DataContext.Guides
                            .Where(m => m.Email == email)
                            .Include(m => m.GuideEvents) // Ensure that events are loaded
                            .FirstOrDefaultAsync();

            // If member is not found or no events are available, return an empty list
            if (guide == null)
            {
                return new List<EventView>();
            }

            // If member has no events, return an empty list
            var events = guide.GuideEvents?.Select(e => new EventView
            {
                id = e.id,
                Name = e.Name,
                Description = e.Description,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                Destination = e.Destination,
            }).ToList() ?? new List<EventView>();

            return events;
        }


        public async Task<bool> AddGuide(string email, GuideRes resource)
        {
            // Fetch the user by email
            var user = await _DataContext.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            // If user is found, create and add a new guide
            if (user == null)
            {
                // If user is not found, return false or handle accordingly
                return false;
                
            }
            // Remove the user from the database
            _DataContext.Users.Remove(user);

            // Create a new Guide from the User
            var guide = new Guide
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Roles = user.Roles,
                DOB = user.DOB,
                Gender = user.Gender,
                Profession = resource.Profession
                // Copy any other necessary properties
            };

            // Add the new guide to the database
            _DataContext.Guides.Add(guide);

            // Save changes to the database
            await _DataContext.SaveChangesAsync();

            return true;

        }



        public async Task<bool> AddEventToGuide(int id, string email)
        {
            Guide? Guide = await _DataContext.Guides.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (Guide == null)
            {
                return false;
            }
            Event? ev = await _DataContext.Events.Where(e => e.id == id).FirstOrDefaultAsync();
            if (ev == null)
            {
                return false; ;
            }
            if (ev.DateFrom <= DateTime.Today || ev.DateFrom >= ev.DateTo)
            {
                return false;
            }
            Guide.GuideEvents ??= new List<Event>();
            Guide.GuideEvents.Add(ev);
            await _DataContext.SaveChangesAsync();
            return true;
        }

    }
}
