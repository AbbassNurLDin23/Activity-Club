using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services.IServices
{
    public interface IMemberService
    {
        Task<List<MemberView>?> GetAllMembers();
        Task<Member?> GetMemberWithEmail(string email);
        Task<bool> CreateMember(string email, MemberRes resource);
        Task<bool> UpdateMember(string email, MemberView view);
        Task<List<EventView>> GetEvents(string email);
        Task<bool> DeleteMember(string email);
        Task<bool> AddEventToMember(int id, string email);
    }
}
