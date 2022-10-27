using SocialAnalyzer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Helpers.JWT
{
    public interface IJWTHelper
    {
        string GenerateJSONWebToken(Usuario userInfo);
    }
}
