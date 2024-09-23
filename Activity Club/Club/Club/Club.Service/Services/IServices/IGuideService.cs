using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services.IServices
{
    public interface IGuideService
    {
        Task<List<GuideView>?> GetAllGuides();
        Task<Guide?> GetGuideWithEmail(string email);
        Task<bool> CreateGuide(string email, GuideRes resource);
        Task<List<EventView>> GetEvents(string email);
        Task<bool> UpdateGuide(string email, GuideView view);
        Task<bool> DeleteGuide(string email);
        Task<bool> AddEventToGuide(int id, string email);
    }
}
