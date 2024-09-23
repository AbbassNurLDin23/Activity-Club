using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<UserView>> GetAllUsers();
        Task<List<UserView>> GetAllAdmins();
        Task<List<GuideView>> GetAllNonGuides();
        Task<List<MemberView>> GetAllNonMembers();
        Task<List<UserView>> GetAllNonAdmins();
        Task<bool> UpdateUser(string email, UserRes view);
        Task<UserView?> GetUserByEmail(string email);
        Task<UserRes?> AddUser(string email, UserRes UserView);
        Task<bool?> AddAdmin(string email);
        Task<bool> DeleteUser(string email);
        Task<bool> DeleteAdmin(string email);
        Task<bool> BelongToEvent (string email, int id);
        Task<string> GetUserType(string email);
        Task<List<string>> AddUserRole(string email, string role);
        //Task<bool> AddAdminToUser(string Adminemail, string email);
        //Task<bool> AddUserToAdmin(string Useremail, string email);
        Task<bool> AddEventToUser(int id, string email);
        Task<bool> AddLookupToUser(int order, string email);
    }
}
