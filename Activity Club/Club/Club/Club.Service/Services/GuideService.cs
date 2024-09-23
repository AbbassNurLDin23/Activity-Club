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
    public class GuideService : IGuideService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MyDataContext _DataContext;

        public GuideService(IUnitOfWork unitOfWork, MyDataContext dataContext)
        {
            this.unitOfWork = unitOfWork;
            _DataContext = dataContext;
        }

        public async Task<List<GuideView>?> GetAllGuides()
        {
            try
            {
                return await unitOfWork.Guides.GetAllGuides();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Guide?> GetGuideWithEmail(string email)
        {
            try
            {
                return await unitOfWork.Guides.GetGuideByEmail(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<EventView>> GetEvents(string email)
        {
            try
            {
                return await unitOfWork.Guides.GetEvents(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateGuide(string email, GuideRes resource)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return false;
                }
                await unitOfWork.Guides.AddGuide(email, resource);
                await unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateGuide(string email, GuideView view)
        {
            try
            {
                await unitOfWork.Guides.UpdateGuide(email, view);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteGuide(string email)
        {
            try
            {
                await unitOfWork.Guides.DeleteGuide(email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<bool> AddEventToGuide(int id, string email)
        {
            try
            {
                await unitOfWork.Guides.AddEventToGuide(id, email);
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
