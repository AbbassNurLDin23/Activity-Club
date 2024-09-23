using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories.IRepositories
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<List<MemberView>> GetAllMembers();
        Task<bool> UpdateMember(string email, MemberView view);
        Task<Member?> GetMemberByEmail(string email);
        Task<List<EventView>> GetEvents(string email);
        Task<bool?> AddMember(string email, MemberRes resource);
        Task<bool> DeleteMember(string email);
        Task<bool> AddEventToMember(int id, string email);
    }
}
