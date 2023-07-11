using BurgerMasters.Core.Models.Auth;
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
        string GenerateToken(ExportUserDto userInfo);

        string GenerateRefreshToken(ExportUserDto userInfo);

        Claim[] GetClaims(ExportUserDto userInfo);
    }
}
