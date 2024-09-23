using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories.IRepositories;
using Club.Service.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MyDataContext _dataContext;

        public UserService(IUnitOfWork unitOfWork, MyDataContext dataContext)
        {
            this.unitOfWork = unitOfWork;
            _dataContext = dataContext;
        }

        public async Task<List<UserView>?> GetAllUsers()
        {
            try
            {
                return await unitOfWork.Users.GetAllUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<UserView>?> GetAllAdmins()
        {
            try
            {
                return await unitOfWork.Users.GetAllAdmins();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<string> GetUserType(string email)
        {
            try
            {
                return await unitOfWork.Users.GetUserType(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<MemberView>?> GetAllNonMembers()
        {
            try
            {
                return await unitOfWork.Users.GetAllNonMembers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<GuideView>?> GetAllNonGuides()
        {
            try
            {
                return await unitOfWork.Users.GetAllNonGuides();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<List<UserView>?> GetAllNonAdmins()
        {
            try
            {
                return await unitOfWork.Users.GetAllNonAdmins();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<bool?> AddAdmin(string email)
        {
            try
            {
                return await unitOfWork.Users.AddAdmin(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<UserView?> GetUserWithEmail(string email)
        {
            try
            {
                return await unitOfWork.Users.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<List<string>> AddUserRole(string email, string role)
        {
            try
            {
                return await unitOfWork.Users.AddUserRole(email, role);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<bool> CreateUser(User view)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(view.Email))
                {
                    return false;
                }
                var user = await _dataContext.Users.Where(u => u.Email == view.Email).FirstOrDefaultAsync();
                if (user != null)
                {
                    return false;
                }
                await unitOfWork.Users.AddAsync(view);
                await unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateUser(string email, UserRes view)
        {
            try
            {
                await unitOfWork.Users.UpdateUser(email, view);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> BelonngToEvent(string email, int id)
        {
            try 
            { 
            return await unitOfWork.Users.BelongToEvent(email, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(string email)
        {
            try
            {
                await unitOfWork.Users.DeleteUser(email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAdmin(string email)
        {
            try
            {
                await unitOfWork.Users.DeleteAdmin(email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //public async Task<bool> AddAdminToUser(string Adminemail, string email)
        //{
        //    try
        //    {
        //        await unitOfWork.Users.AddAdminToUser(Adminemail, email);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        //public async Task<bool> AddUserToAdmin(string Useremail, string email)
        //{
        //    try
        //    {
        //        await unitOfWork.Users.AddAdminToUser(Useremail, email);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        public async Task<bool> AddEventToUser(int id, string email)
        {
            try
            {
                await unitOfWork.Users.AddEventToUser(id, email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddLookupToUser(int order, string email)
        {
            try
            {
                await unitOfWork.Users.AddEventToUser(order, email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
