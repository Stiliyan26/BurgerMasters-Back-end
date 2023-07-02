using BurgerMasters.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Contracts
{
    public interface IUserService
    {
        Task RegisterUser(RegisterViewModel userModel);
    }
}
