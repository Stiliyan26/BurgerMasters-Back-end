using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repo;

        public OrderService(IRepository repo)
        {
            _repo = repo;
        }
        public async Task CreateOrder(OrderViewModel orderInfo)
        {
            string format = "yyyy-MM-dd HH:mm";
            DateTime validOrderDate;

            bool isOrderDateValid = DateTime.TryParseExact(orderInfo.OrderDate, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out validOrderDate);

            if (isOrderDateValid)
            {
                ICollection<OrderDetail> orderDetails = orderInfo.OrderDetails
                    .Select(od => new OrderDetail()
                    {
                        MenuItemId = od.Id,
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
    }
}
