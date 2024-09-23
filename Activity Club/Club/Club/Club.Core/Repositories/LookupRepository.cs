using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories
{
    public class LookupRepository : Repository<Lookup>, ILookupRepository
    {
        private readonly MyDataContext _DataContext;
        public LookupRepository(MyDataContext dataContext) : base(dataContext) 
        {
            _DataContext = dataContext;
        }

        public async Task<List<LookupView>> GetAllLookups()
        {
            return await _DataContext.Lookups.Select(l => new LookupView
            {
                Order = l.Order,
                Code = l.Code,
                Name = l.Name,
            }).ToListAsync();
        }

        public async Task<bool> UpdateLookup(int order, LookupView view)
        {
            var lookup = await _DataContext.Lookups
                .Where(l => l.Order == order)
                .FirstOrDefaultAsync();

            if (lookup == null)
            {
                return false; // Lookup not found
            }

            // Update fields only if the view property has a valid value
            if (view.Code != default(int)) // Check if view.Code is not the default value
            {
                lookup.Code = view.Code;
            }
            if (!string.IsNullOrEmpty(view.Name)) // Assuming Name is a string
            {
                lookup.Name = view.Name;
            }

            // Save changes to the database
            await _DataContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteLookup(int order)
        {
            var lookup = await _DataContext.Lookups.Where(l => l.Order == order).FirstOrDefaultAsync();
            if (lookup == null)
            {
                return false;
            }
            _DataContext.Remove(lookup);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<LookupView?> GetLookupByOrder(int order)
        {
            return await _DataContext.Lookups.Where(l => l.Order == order)
                .Select(l => new LookupView
                {
                    Order = order,
                    Code= l.Code,
                    Name= l.Name,
                }).FirstOrDefaultAsync();
        }

        public async Task<LookupView?> AddLookup(LookupView lookupView)
        {
            var lookup = new Lookup
            {
                Code = lookupView.Code,
                Name = lookupView.Name,
            };
            _DataContext.Lookups.Add(lookup);
            await _DataContext.SaveChangesAsync();
            return lookupView;
        }

        public async Task<bool> AddAdminToLookup(string email, int order)
        {
            Lookup? lookup = await _DataContext.Lookups.Where(l => l.Order ==order).FirstOrDefaultAsync();
            if (lookup == null)
            {
                return false;
            }
            User? admin = await _DataContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (admin == null)
            {
                return false; ;
            }
            lookup.Admins ??= new List<User>();
            lookup.Admins.Add(admin);
            await _DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddEventToLookup(int id, int order)
        {
            Lookup? lookup = await _DataContext.Lookups.Where(l => l.Order == order).FirstOrDefaultAsync();
            if (lookup == null)
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
            lookup.Events ??= new List<Event>();
            lookup.Events.Add(ev);
            await _DataContext.SaveChangesAsync();
            return true;
        }
    }
}
