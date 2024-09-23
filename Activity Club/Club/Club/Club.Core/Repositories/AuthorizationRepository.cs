using Club.Core.DataModels;
using Club.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly MyDataContext _DataContext;
        public AuthorizationRepository(MyDataContext dataContext) 
        {
            _DataContext = dataContext;
        }


        public async Task<bool> CheckUserEmailAndPassword(string email, string password)
        {
            var user = await _DataContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            if(password != user.Password)
            {
                return false ;
            }  
            return true;
        }

        public async Task<bool> Authorize(string email, string password)
        {
            var user = await _DataContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            if (password != user.Password)
            {
                return false;
            }
            if (!user.Roles.Contains("admin"))
            {
                return false;
            }
            return true;
        }
    }
}
