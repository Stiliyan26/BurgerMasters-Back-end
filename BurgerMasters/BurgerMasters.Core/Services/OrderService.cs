using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BurgerMasters.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repo;

        public OrderService(IRepository repo)
        {
            _repo = repo;
        }
        public async Task CreateOrderAsync(OrderViewModel orderInfo)
        {
            string format = "yyyy-MM-dd HH:mm";
            DateTime validOrderDate;

            bool isOrderDateValid = DateTime.TryParseExact(orderInfo.OrderDate, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out validOrderDate);

            if (isOrderDateValid)
            {
                ICollection<OrderDetail> orderDetails = orderInfo.MenuItems
                    .Select(od => new OrderDetail()
                    {
                        MenuItemId = od.MenuItemId,
                        Quantity = od.Quantity,
                    })
                    .ToList();

                Order newOrder = new Order()
                {
                    OrderDate = validOrderDate,
                    UserId = orderInfo.UserId,
                    OrderDetails = orderDetails,
                    TotalPrice = orderInfo.OrderPrice
                };

                await _repo.AddAsync(newOrder);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ExportOrderViewModel>> GetAllPendingOrdersAsync()
        {
            return await _repo.AllReadonly<Order>()
                .Where(o => o.IsPending == true)
                .Select(o => new ExportOrderViewModel()
                {
                    OrderId = o.Id,
                    Username = o.ApplicationUser.UserName,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd HH:mm"),
                    Address = o.ApplicationUser.Address,
                    TotalPrice = o.TotalPrice
                })
                .ToListAsync();
        }
    }
}
