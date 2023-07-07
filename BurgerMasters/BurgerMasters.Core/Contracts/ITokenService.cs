using BurgerMasters.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(ExportUserDto userInfo, string userId);

        string GenerateRefreshToken(ExportUserDto userInfo, string userId);

        Claim[] GetClaims(ExportUserDto userInfo, string userId);
    }
}
