using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Infrastructure.Data.Models;
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

        Task<IEnumerable<ExportOrderViewModel>> GetAllOrdersByStatus(bool isPending);

        Task<OrderDetailsViewModel> GetOrderByIdAsync(Guid orderId);

        Task<Order> GetOrderById(Guid orderId);

        Task AcceptOrderAsync(Guid orderId);

        Task UnacceptOrderAsync(Guid orderId);

        Task DeclineOrderAsync(Guid orderId);
    }
}
