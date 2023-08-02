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

        [Test]
        public async Task CreateMenuItemAsync_DoesNotCreateNewItemIfItemTypeIsInvalid()
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
                ItemType = "Fries",
                PortionSize = 550,
                Description = "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions",
                Price = 22.20m,
            };

            await _adminService.CreateMenuItemAsync(formModel, userId);

            var result = await _repo.All<MenuItem>()
                .FirstOrDefaultAsync(mi => mi.Name == formModel.Name);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public async Task GetFourSimilarItemsByTypeAndCreatorAsync_ReturnsFourItemsCreatedByCretator()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var dumyData = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 630,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "Burger Pie",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "American Cheese Burger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "Triple Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "Smokey Whiskey Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
            };

            await _repo.AddRangeAsync(dumyData);
            await _repo.SaveChangesAsync();

            var similarItemsByCreator = await _adminService
                .GetFourSimilarItemsByTypeAndCreatorAsync("Burger", 1, creatorId);

            Assert.That(similarItemsByCreator.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task CreatorItemByIdAsync_ReturnsItemById()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var item = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
            };

            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();

            var itemByCreator = await _adminService.CreatorItemByIdAsync(1, creatorId);

            Assert.That(itemByCreator, Is.Not.Null);
        }

        [Test]
        public async Task GetCreatorItemsByTypeAsync_ReturnsProperCountOfItemsCreatedByCreator()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var dumyData = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 630,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "Burger Pie",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemType.Id,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "123131314144"
                },
            };

            await _repo.AddRangeAsync(dumyData);
            await _repo.SaveChangesAsync();

            var itemsByCreator = await _adminService.GetCreatorItemsByTypeAsync(creatorId, newItemType.Name);

            Assert.That(itemsByCreator.Count(), Is.EqualTo(2));
        }


        [Test]
        public async Task ItemExistsByCreatorIdAsync_ReturnsTrueIfItemIsCreatedByCreator()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
            };

            await _repo.AddAsync(menuItem);
            await _repo.SaveChangesAsync();

            bool itemExistsByCreator = await _adminService
                .ItemExistsByCreatorIdAsync(menuItem.Id, creatorId);

            Assert.That(itemExistsByCreator, Is.EqualTo(true));
        }

        [Test]
        public async Task ItemExistsByCreatorIdAsync_ItemExistsByCreatorIdAsync_ReturnsFalseIfItemIsNotCreatedByCreator()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = "13124"
            };

            await _repo.AddAsync(menuItem);
            await _repo.SaveChangesAsync();

            bool itemExistsByCreator = await _adminService
                .ItemExistsByCreatorIdAsync(menuItem.Id, creatorId);

            Assert.That(itemExistsByCreator, Is.EqualTo(false));
        }

        [Test]
        public async Task GetEditItemInfoByItemIdAsync_ReturnsEditItem()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = creatorId
            };

            await _repo.AddAsync(menuItem);
            await _repo.SaveChangesAsync();

            var itemToEdit = await _adminService.GetEditItemInfoByItemIdAsync(menuItem.Id, creatorId);

            Assert.That(itemToEdit, Is.Not.Null);
            Assert.That(itemToEdit.Name, Is.EqualTo(menuItem.Name));
            Assert.That(itemToEdit.ImageUrl, Is.EqualTo(menuItem.ImageUrl));
            Assert.That(itemToEdit.PortionSize, Is.EqualTo(menuItem.PortionSize));
            Assert.That(itemToEdit.Description, Is.EqualTo(menuItem.Description));
            Assert.That(itemToEdit.Price, Is.EqualTo(menuItem.Price));
        }

        [Test]
        public async Task GetEditItemInfoByItemIdAsync_ReturnsNullIfItemDoesNotExist()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = creatorId
            };

            await _repo.AddAsync(menuItem);
            await _repo.SaveChangesAsync();

            var itemToEdit = await _adminService.GetEditItemInfoByItemIdAsync(23, creatorId);

            Assert.That(itemToEdit, Is.Null);
        }


        [Test]
        public async Task EditMenuItemAsync_ReturnsEditedItem()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = creatorId
            };

            await _repo.AddAsync(menuItem);
            await _repo.SaveChangesAsync();

            var formModel = new FormMenuItemViewModel()
            {
                Name = "Triple Cheeseburger",
                ImageUrl = "TrippleCheese.webp",
                PortionSize = 550,
                Description = "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions",
                Price = 22.20m,
            };

            await _adminService.EditMenuItemAsync(formModel, 1, creatorId);

            var editedItem = await _repo.GetByIdAsync<MenuItem>(menuItem.Id);

            Assert.That(editedItem, Is.Not.Null);
            Assert.That(editedItem.Name, Is.EqualTo(formModel.Name));
            Assert.That(editedItem.ImageUrl, Is.EqualTo(formModel.ImageUrl));
            Assert.That(editedItem.PortionSize, Is.EqualTo(formModel.PortionSize));
            Assert.That(editedItem.Description, Is.EqualTo(formModel.Description));
            Assert.That(editedItem.Price, Is.EqualTo(formModel.Price));
        }

        [Test]
        public async Task EditMenuItemAsync_DoesNotEditTheItemIfIdDoesNotExist()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = creatorId
            };

            await _repo.AddAsync(menuItem);
            await _repo.SaveChangesAsync();

            var formModel = new FormMenuItemViewModel()
            {
                Name = "Triple Cheeseburger",
                ImageUrl = "TrippleCheese.webp",
                PortionSize = 550,
                Description = "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions",
                Price = 22.20m,
            };

            await _adminService.EditMenuItemAsync(formModel, 5, creatorId);

            var editedItem = await _repo.GetByIdAsync<MenuItem>(menuItem.Id);

            Assert.That(menuItem.Name, Is.EqualTo(editedItem.Name));
        }

        [Test]
        public async Task DeleteMenuItemAsync_IsActiveIsSetToFalse()
        {
            string creatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var menuItem = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = creatorId
            };

            await _repo.AddAsync(menuItem);
            await _repo.SaveChangesAsync();

            await _adminService.DeleteMenuItemAsync(menuItem.Id, creatorId);

            var deletedItem = await _repo.All<MenuItem>()
                .FirstOrDefaultAsync(mi => mi.Id == menuItem.Id);

            Assert.That(deletedItem.IsActive, Is.EqualTo(false));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
