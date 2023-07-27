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

        public async Task<IEnumerable<ExportOrderViewModel>> GetAllOrdersByStatus(bool isPending)
        {
            return await _repo.AllReadonly<Order>()
                .Where(o => o.IsPending == isPending && o.IsActive == true)
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

        public async Task<OrderDetailsViewModel> GetOrderByIdAsync(Guid orderId)
        {
            return await _repo.AllReadonly<Order>()
                .Where(o => o.Id == orderId && o.IsActive == true)
                .Select(o => new OrderDetailsViewModel()
                {
                    OrderId = o.Id,
                    Username = o.ApplicationUser.UserName,
                    OrderDate = o.OrderDate.ToString("yyyy-MM-dd HH:mm"),
                    Address = o.ApplicationUser.Address,
                    TotalPrice = o.TotalPrice,
                    MenuItems = o.OrderDetails
                        .Select(od => new OrderDetailsMenuItemViewModel()
                        {
                            Name = od.MenuItem.Name,
                            ImageUrl = od.MenuItem.ImageUrl,
                            ItemType = od.MenuItem.ItemType.Name,
                            PortionSize = od.MenuItem.PortionSize,
                            Price = od.MenuItem.Price,
                            Quantity = od.Quantity
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await _repo.GetByIdAsync<Order>(orderId);
        }

        public async Task AcceptOrderAsync(Guid orderId)
        {
            Order orderToAccept = await GetOrderById(orderId);

            if (orderToAccept != null 
                && orderToAccept.IsPending == true 
                && orderToAccept.IsActive)
            {
                orderToAccept.IsPending = false;

                await _repo.SaveChangesAsync();
            }
        }

        public async Task UnacceptOrderAsync(Guid orderId)
        {
            Order orderToUnaccept = await GetOrderById(orderId);

            if (orderToUnaccept != null 
                && orderToUnaccept.IsPending == false 
                && orderToUnaccept.IsActive)
            {
                orderToUnaccept.IsPending = true;

                await _repo.SaveChangesAsync();
            }
        }

        public async Task DeclineOrderAsync(Guid orderId)
        {
            Order orderToDecline = await GetOrderById(orderId);

            if (orderToDecline != null
                && orderToDecline.IsPending == true
                && orderToDecline.IsActive)
            {
                orderToDecline.IsActive = false;

                await _repo.SaveChangesAsync();
            }
        }
    }
}
