using Club.Core.DataModels;
using Club.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories.IRepositories
{
    public interface IGuideRepository : IRepository<Guide>
    {
        Task<List<GuideView>> GetAllGuides();
        Task<bool> UpdateGuide(string email, GuideView view);
        Task<Guide?> GetGuideByEmail(string email);
        Task<List<EventView>> GetEvents(string email);
        Task<bool> AddGuide(string email, GuideRes resource);
        Task<bool> DeleteGuide(string email);
        Task<bool> AddEventToGuide(int id, string email);

    }
}
