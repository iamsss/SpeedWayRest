using SpeedWayRest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWayRest.Services
{
    public  interface IIdentityServices
    {
        Task<AuthenticationResult> RegisterAsync(string Email, string Password);
    }
}
