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
        string GenerateToken(ExportUserDto userInfo);

        ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
    }
}
