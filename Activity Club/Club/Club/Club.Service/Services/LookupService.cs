using Club.Service.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Club.Core.Repositories.IRepositories;
using Club.Core.DTO;
using Club.Core.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
namespace Club.Service.Services
{
    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MyDataContext _DataContext;

        public LookupService(IUnitOfWork unitOfWork, MyDataContext dataContext)
        {
            this.unitOfWork = unitOfWork;
            _DataContext = dataContext;
        }

        public async Task<List<LookupView>?> GetAllLookups()
        {
            try
            {
                return await unitOfWork.Lookups.GetAllLookups();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<LookupView?> GetLookupWithOrder(int order)
        {
            try
            {
                return await unitOfWork.Lookups.GetLookupByOrder(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateLookup(Lookup lookup)
        {
            try
            {
                await unitOfWork.Lookups.AddAsync(lookup);
                await unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateLookup(int order, LookupView view)
        {
            try
            {
                await unitOfWork.Lookups.UpdateLookup(order, view);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteLookup(int order)
        {
            try
            {
                await unitOfWork.Lookups.DeleteLookup(order);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddAdminToLookup(string email, int order)
        {
            try
            {
                await unitOfWork.Lookups.AddAdminToLookup(email, order);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddEventToLookup(int id, int order)
        {
            try
            {
                await unitOfWork.Lookups.AddEventToLookup(id, order);
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
