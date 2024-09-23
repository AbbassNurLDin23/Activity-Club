using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories.IRepositories
{
    public interface ILookupRepository : IRepository<Lookup>
    {
        Task<List<LookupView>> GetAllLookups();
        Task<bool> UpdateLookup(int order, LookupView view);
        Task<LookupView?> GetLookupByOrder(int order);
        Task<LookupView?> AddLookup(LookupView lookupView);
        Task<bool> DeleteLookup(int order);
        Task<bool> AddAdminToLookup(string email, int order);
        Task<bool> AddEventToLookup(int id, int order);

    }
}
