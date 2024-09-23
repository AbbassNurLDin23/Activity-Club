using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private readonly MyDataContext _DataContext;
        public MemberRepository(MyDataContext dataContext) : base(dataContext)
        {
            _DataContext = dataContext;
        }

        public async Task<List<MemberView>> GetAllMembers()
        {
            return await _DataContext.Members.Select(l => new MemberView
            {
                Email = l.Email,
                Name = l.Name,
                Password = l.Password,
                Roles = l.Roles,
                DOB = l.DOB,
                Gender = l.Gender,
                MobileNumber = l.MobileNumber,
                EmergencyNumber = l.EmergencyNumber,
                //Photo = l.Photo,
                Profession = l.Profession,
                Nationality = l.Nationality,
                
            }).ToListAsync();
        }

        public async Task<bool> UpdateMember(string email, MemberView view)
        {
            var member = await _DataContext.Members
                .Where(l => l.Email == email)
                .FirstOrDefaultAsync();

            if (member == null)
            {
                return false; // Member not found
            }

            // Update fields only if the view property is not null or has a valid value
            if (!string.IsNullOrEmpty(view.Password))
            {
                member.Password = view.Password;
            }
            if (!string.IsNullOrEmpty(view.Name))
            {
                member.Name = view.Name;
            }
            if (!string.IsNullOrEmpty(view.Gender))
            {
                member.Gender = view.Gender;
            }
            if (view.DOB.HasValue) // Assuming DOB is a DateTime? (nullable DateTime)
            {
                member.DOB = view.DOB.Value;
            }
            if (view.MobileNumber != default(int)) // Check if MobileNumber is not default value (0)
            {
                member.MobileNumber = view.MobileNumber;
            }
            if (view.EmergencyNumber != default(int)) // Check if EmergencyNumber is not default value (0)
            {
                member.EmergencyNumber = view.EmergencyNumber;
            }
            // if (!string.IsNullOrEmpty(view.Photo)) // Assuming Photo is a string
            // {
            //     member.Photo = view.Photo;
            // }
            if (!string.IsNullOrEmpty(view.Profession))
            {
                member.Profession = view.Profession;
            }
            if (!string.IsNullOrEmpty(view.Nationality))
            {
                member.Nationality = view.Nationality;
            }
            if (view.Roles != null && view.Roles.Count > 0)
            {
                foreach (var role in view.Roles)
                {
                    if (!member.Roles.Contains(role)) // Prevent duplicate roles
                    {
                        member.Roles.Add(role);
                    }
                }
            }

            // Save changes to the database
            await _DataContext.SaveChangesAsync();
            return true;
        }


        public async Task<List<EventView>> GetEvents(string email)
        {
            // Fetch the member along with related events
            var member = await _DataContext.Members
                            .Where(m => m.Email == email)
                            .Include(m => m.MemberEvents) // Ensure that events are loaded
                            .FirstOrDefaultAsync();


            // If member is not found or no events are available, return an empty list
            if (member == null)
            {
                return new List<EventView>();
            }

            // If member has no events, return an empty list
            var events = member.MemberEvents?.Select(e => new EventView
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



        public async Task<bool> DeleteMember(string email)
        {
            var Member = await _DataContext.Members.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (Member == null)
            {
                return false;
            }
            _DataContext.Remove(Member);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Member?> GetMemberByEmail(string email)
        {
            return await _DataContext.Members.Where(l => l.Email == email)
                .Select(l => new Member
                {
                    Email = email,
                    Name = l.Name,
                    Password = l.Password,
                    Roles = l.Roles,
                    DOB = l.DOB,
                    Gender = l.Gender,
                    MobileNumber = l.MobileNumber,
                    EmergencyNumber = l.EmergencyNumber,
                    //Photo = l.Photo,
                    Profession = l.Profession,
                    Nationality = l.Nationality,
                    MemberEvents = l.MemberEvents,
                }).FirstOrDefaultAsync();
        }

        public async Task<bool?> AddMember(string email, MemberRes resource)
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
            var member = new Member
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Roles = user.Roles,
                DOB = user.DOB,
                Gender = user.Gender,
                MobileNumber = resource.MobileNumber,
                EmergencyNumber = resource.EmergencyNumber,
                Profession = resource.Profession,
                Nationality = resource.Nationality,
                //Photo = resource.Photo,
            };

            // Add the new MEMBER to the database
            _DataContext.Members.Add(member);

            // Save changes to the database
            try
            {
                await _DataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }


            return true;
        }



        public async Task<bool> AddEventToMember(int id, string email)
        {
            Member? Member = await _DataContext.Members.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (Member == null)
            {
                return false;
            }
            Event? ev = await _DataContext.Events.Where(e => e.id == id).FirstOrDefaultAsync();
            if (ev == null)
            {
                return false; ;
            }
            if (ev.DateFrom < DateTime.Today )
            {
                return false;
            }

            Member.MemberEvents ??= new List<Event>();
            Member.MemberEvents.Add(ev);
            await _DataContext.SaveChangesAsync();
            return true;
        }

       
    }
}
