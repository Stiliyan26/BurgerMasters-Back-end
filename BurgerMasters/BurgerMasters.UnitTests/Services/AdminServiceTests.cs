using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Models;

namespace BurgerMasters.UnitTests.Services
{
    [TestFixture]
    public class AdminServiceTests
    {
        private IAdminService _adminService;
        private IRepository _repo;
        private BurgerMastersDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase("BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);
            _adminService = new AdminService(_repo);
        }

        [Test]

        public async Task CreateMenuItemAsync_CreateNewMenuItem()
        {
            string userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);
            await _repo.SaveChangesAsync();

            var formModel = new FormMenuItemViewModel()
            {
                Name = "Triple Cheeseburger",
                ImageUrl = "TrippleCheese.webp",
                ItemType = "Burger",
                PortionSize = 550,
                Description = "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions",
                Price = 22.20m,
            };

            await _adminService.CreateMenuItemAsync(formModel, userId);

            var result = await _repo.All<MenuItem>()
                .FirstOrDefaultAsync(mi => mi.Name == formModel.Name);

            Assert.That(result.ItemType.Name, Is.EqualTo(formModel.ItemType));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
