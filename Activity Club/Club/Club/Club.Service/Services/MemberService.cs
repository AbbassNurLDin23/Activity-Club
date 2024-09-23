using Club.Core.DataModels;
using Club.Core.DTO;
using Club.Core.Repositories.IRepositories;
using Club.Service.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MyDataContext _Datacontext;

        public MemberService(IUnitOfWork unitOfWork, MyDataContext dataContext)
        {
            this.unitOfWork = unitOfWork;
            _Datacontext = dataContext;
        }

        public async Task<List<MemberView>?> GetAllMembers()
        {
            try
            {
                return await unitOfWork.Members.GetAllMembers();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Member?> GetMemberWithEmail(string email)
        {
            try
            {
                return await unitOfWork.Members.GetMemberByEmail(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateMember(string email, MemberRes resource)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return false;
                }
                
                await unitOfWork.Members.AddMember(email, resource);
                await unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<EventView>> GetEvents(string email)
        {
            try
            {
                return await unitOfWork.Members.GetEvents(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateMember(string email, MemberView view)
        {
            try
            {
                await unitOfWork.Members.UpdateMember(email, view);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteMember(string email)
        {
            try
            {
                await unitOfWork.Members.DeleteMember(email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<bool> AddEventToMember(int id, string email)
        {
            try
            {
                await unitOfWork.Members.AddEventToMember(id, email);
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
