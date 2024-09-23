using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Service.Services.IServices
{
    public interface IAuthorizationService
    {
        Task<IActionResult> GetToken(string email, string password);
        Task<IActionResult> CheckAccount(string email, string password);

    }
}
