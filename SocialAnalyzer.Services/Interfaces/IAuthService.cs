using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RefreshTokenAsync(int usuarioId);
        Task<LoginResponse> LoginAsync(string userName, string password);
    }
}
