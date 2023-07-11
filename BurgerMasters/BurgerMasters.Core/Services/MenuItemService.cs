using BurgerMasters.Core.Contracts;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository _repo;

        public MenuItemService(IRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ItemType> GetAllItemTypes()
        {
            return _repo.AllReadonly<ItemType>();
        }
    }
}
