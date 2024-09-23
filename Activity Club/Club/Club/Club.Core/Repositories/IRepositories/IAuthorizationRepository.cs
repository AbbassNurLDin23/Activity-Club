using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories.IRepositories
{
    public interface IAuthorizationRepository
    {
        public Task<bool> CheckUserEmailAndPassword(string email, string password);
        public Task<bool> Authorize(string email, string password);

    }
}
