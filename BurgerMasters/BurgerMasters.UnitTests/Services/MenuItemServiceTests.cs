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
using BurgerMasters.Infrastructure.Data.Models;
using AutoMapper;
using BurgerMasters.Core.AutoMapper;

namespace BurgerMasters.UnitTests.Services
{
    [TestFixture]
    public class MenuItemServiceTests
    {
        private IMenuItemService _menuService;
        private IRepository _repo;
        private BurgerMastersDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>(); 
            });

            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase("BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);
            var mapper = config.CreateMapper();
            _menuService = new MenuItemService(_repo, mapper);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllMenuItemsCount()
        {
            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            var dumyData = getMenuItems(newItemType.Id);

            await _repo.AddAsync(newItemType);
            await _repo.AddRangeAsync(dumyData);
            await _repo.SaveChangesAsync();

            var items = await _menuService.GetAllByItemTypeAsync("Burger");

            Assert.That(items.Count, Is.EqualTo(dumyData.Count));
        }

        [Test]
        public async Task GetAllItemTypesAsync_ReurnsCorrectCountOfItemTypes()
        {
            var dumyItems = getItems();

            await _repo.AddRangeAsync(dumyItems);
            await _repo.SaveChangesAsync();

            var allItemTypes = await _menuService.GetAllItemTypesAsync();

            Assert.That(allItemTypes.Count, Is.EqualTo(dumyItems.Count));
        }

        [Test]
        public async Task GetFourSimilarItemsByTypeAsync_ShouldReturnFourSimilarItemsByTheSameType()
        {
            ItemType newItemType = getItemType();

            await _repo.AddAsync(newItemType);

            var dumyData = getManyMenuItems(newItemType.Id);

            await _repo.AddRangeAsync(dumyData);
            await _repo.SaveChangesAsync();

            var similarItems = await _menuService.GetFourSimilarItemsByTypeAsync("Burger", 1);

            Assert.That(similarItems.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task GetItemByIdAsync_ReturnsTheItemById()
        {
            ItemType newItemType = getItemType();

            await _repo.AddAsync(newItemType);

            var item = new MenuItem()
            {
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

            var result = await _menuService.GetItemByIdAsync(item.Id);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task ItemExistsAsync_ReturnsTrueIfItemExists()
        {
            var item = new MenuItem()
            {
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = 1,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
            };

            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();

            bool doesExist = await _menuService.ItemExistsAsync(item.Id);

            Assert.That(doesExist, Is.EqualTo(true));
        }

        [Test]
        public async Task ItemExistsAsync_ReturnsFalseIfItemDoesNotExists()
        {
            var item = new MenuItem()
            {
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = 1,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
            };

            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();

            bool doesExist = await _menuService.ItemExistsAsync(3);

            Assert.That(doesExist, Is.EqualTo(false));
        }

        private ItemType getItemType()
        {
            return new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };
        }

        private static List<ItemType> getItems()
        {
            return new List<ItemType>()
            {
                new ItemType
                {
                    Id = 1,
                    Name = "Burger"
                },
                new ItemType
                {
                    Id = 2,
                    Name = "Fries"
                },
            };
        }

        private static List<MenuItem> getMenuItems(int itemTypeId)
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 630,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
            };
        }

        private static List<MenuItem> getManyMenuItems(int newItemTypeId)
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 630,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "Burger Pie",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "American Cheese Burger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "Triple Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "Smokey Whiskey Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
            };
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
