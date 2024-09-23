using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services.IServices
{
    public interface IUserService
    {
        Task<List<UserView>?> GetAllUsers();
        Task<List<UserView>?> GetAllAdmins();
        Task<List<MemberView>?> GetAllNonMembers();
        Task<List<GuideView>?> GetAllNonGuides();
        Task<List<UserView>?> GetAllNonAdmins();
        Task<UserView?> GetUserWithEmail(string email);
        Task<string> GetUserType (string email);
        Task<bool> CreateUser(User user);
        Task <bool?> AddAdmin(string email);
        Task<bool> UpdateUser(string email, UserRes view);
        Task<bool> BelonngToEvent(string email, int id);
        Task<bool> DeleteUser(string email);
        Task<bool> DeleteAdmin(string email);
        Task<List<string>> AddUserRole(string email, string role);
        //Task<bool> AddAdminToUser(string Adminemail, string email);
        //Task<bool> AddUserToAdmin(string Useremail, string email);
        Task<bool> AddEventToUser(int id, string email);
        Task<bool> AddLookupToUser(int order, string email);
    }
}
