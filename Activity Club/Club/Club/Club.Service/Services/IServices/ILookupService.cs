using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Club.Core.DataModels;
using Club.Core.DTO;

namespace Club.Service.Services.IServices
{
    public interface ILookupService
    {
        Task<List<LookupView>?> GetAllLookups();
        Task<LookupView?> GetLookupWithOrder(int order);
        Task<bool> CreateLookup(Lookup lookup);
        Task<bool> UpdateLookup(int order, LookupView view);
        Task<bool> DeleteLookup(int order);
        Task<bool> AddAdminToLookup(string email, int order);
        Task<bool> AddEventToLookup(int id, int order);
    }
}
