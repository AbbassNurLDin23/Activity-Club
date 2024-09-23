using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MyDataContext _DataContext;
        public UserRepository(MyDataContext dataContext) : base(dataContext)
        {
            _DataContext = dataContext;
        }

        public async Task<List<UserView>> GetAllUsers()
        {
            return await _DataContext.Users.Select(l => new UserView
            {
                Email = l.Email,
                Name = l.Name,
                Password = l.Password,
                Roles = l.Roles,
                DOB = l.DOB,
                Gender = l.Gender,
            }).ToListAsync();
        }

        public async Task<List<UserView>> GetAllAdmins()
        {
            return await _DataContext.Users.Where(u => u.Roles.Contains("admin")).Select(l => new UserView
            {
                Email = l.Email,
                Name = l.Name,
                Password = l.Password,
                Roles = l.Roles,
                DOB = l.DOB,
                Gender = l.Gender,
            }).ToListAsync();
        }

        public async Task<List<MemberView>> GetAllNonMembers()
        {
            return await _DataContext.Users.Where(u => !(u is Member)).Select(l => new MemberView
            {
                Email = l.Email,
                Name = l.Name,
                Password = l.Password,
                Roles = l.Roles,
                DOB = l.DOB,
                Gender = l.Gender,
            }).ToListAsync();

        }

        public async Task<List<GuideView>> GetAllNonGuides()
        {
            return await _DataContext.Users.Where(u => !(u is Guide)).Select(l => new GuideView
            {
                Email = l.Email,
                Name = l.Name,
                Password = l.Password,
                Roles = l.Roles,
                DOB = l.DOB,
                Gender = l.Gender,
            }).ToListAsync();

        }


        public async Task<List<UserView>> GetAllNonAdmins()
        {
            return await _DataContext.Users.Where(u => !u.Roles.Contains("admin")).Select(l => new UserView
            {
                Email = l.Email,
                Name = l.Name,
                Password = l.Password,
                Roles = l.Roles,
                DOB = l.DOB,
                Gender = l.Gender,
            }).ToListAsync();
        }

        public async Task<string> GetUserType(string email)
        {
            var user = await _DataContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null) { return ""; }
            if (user is Member) return "member";
            if (user is Guide) return "guide";
            return "user";
        }
        public async Task<bool> UpdateUser(string email, UserRes view)
        {
            var user = await _DataContext.Users
                .Where(l => l.Email == email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return false; // User not found
            }

            // Update fields only if the view property is not null or empty
            if (!string.IsNullOrEmpty(view.Password))
            {
                user.Password = view.Password;
            }
            if (!string.IsNullOrEmpty(view.Name))
            {
                user.Name = view.Name;
            }
            if (!string.IsNullOrEmpty(view.Gender))
            {
                user.Gender = view.Gender;
            }
             if (view.DOB.HasValue) // Assuming DOB is a DateTime? (nullable DateTime)
            {
                user.DOB = view.DOB;
            }

            

            // Save changes to the database
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> AddUserRole(string email, string role)
        {
            //Handle roles if view.Roles is not null and has elements
            if (role == null )
            {
                return null;
            }
            var user = await _DataContext.Users
              .Where(l => l.Email == email)
              .FirstOrDefaultAsync();
            user.Roles.Add(role);
            await _DataContext.SaveChangesAsync();
            return user.Roles;
        }

        public async Task<bool?> AddAdmin(string email)
        {
            var user = await _DataContext.Users
              .Where(l => l.Email == email)
              .FirstOrDefaultAsync();
            user.Roles.Add("admin");
            await _DataContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> BelongToEvent(string email, int id)
        {
            var user = await _DataContext.Users
              .Where(l => l.Email == email)
              .FirstOrDefaultAsync();
            if (user.Events.Where(e => e.id == id).Any()) 
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUser(string email)
        {
            var User = await _DataContext.Users.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (User == null)
            {
                return false;
            }
            _DataContext.Remove(User);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAdmin(string email)
        {
            var User = await _DataContext.Users.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (User == null)
            {
                return false;
            }
            if (User.Roles.Count > 0) 
            {
                User.Roles.Remove("admin");
            }
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserView?> GetUserByEmail(string email)
        {
            return await _DataContext.Users.Where(l => l.Email == email)
                .Select(l => new UserView
                {
                    Email = email,
                    Name = l.Name,
                    Password = l.Password,
                    Roles = l.Roles,
                    DOB = l.DOB,
                    Gender = l.Gender,
                }).FirstOrDefaultAsync();
        }

        public async Task<UserRes?> AddUser(string email, UserRes UserView)
        {
            var User = new User
            {
                Email = email,
                Name = UserView.Name,
                Password = UserView.Password,
                DOB = UserView.DOB,
                Gender = UserView.Gender,
            };
            _DataContext.Users.Add(User);
            await _DataContext.SaveChangesAsync();
            return UserView;
        }

        //public async Task<bool> AddAdminToUser(string Adminemail, string email)
        //{
        //    User? admin = await _DataContext.Users.Where(l => l.Email == Adminemail).FirstOrDefaultAsync();
        //    if (admin == null)
        //    {
        //        return false;
        //    }
        //    User? user = await _DataContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        return false; ;
        //    }
        //    user.Admins ??= new List<User>();
        //    user.Admins.Add(admin);
        //    await _DataContext.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> AddUserToAdmin(string Useremail, string email)
        //{
        //    User? user = await _DataContext.Users.Where(l => l.Email == Useremail).FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    User? admin = await _DataContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        //    if (admin == null)
        //    {
        //        return false; ;
        //    }
        //    admin.Users ??= new List<User>();
        //    admin.Users.Add(user);
        //    await _DataContext.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> AddEventToUser(int id, string email)
        {
            User? User = await _DataContext.Users.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (User == null)
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
            User.Events ??= new List<Event>();
            User.Events.Add(ev);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddLookupToUser(int order, string email)
        {
            User? User = await _DataContext.Users.Where(l => l.Email == email).FirstOrDefaultAsync();
            if (User == null)
            {
                return false;
            }
            Lookup? lookup = await _DataContext.Lookups.Where(e => e.Order == order).FirstOrDefaultAsync();
            if (lookup == null)
            {
                return false; ;
            }
            User.Lookups ??= new List<Lookup>();
            User.Lookups.Add(lookup);
            await _DataContext.SaveChangesAsync();
            return true;
        }
    }
}
