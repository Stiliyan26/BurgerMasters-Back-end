using BurgerMasters.Core.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Contracts
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderViewModel orderInfo);

        Task<IEnumerable<ExportOrderViewModel>> GetAllPendingOrdersAsync();
    }
}
