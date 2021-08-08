using Microsoft.AspNetCore.Mvc;
using ShopBridge.Controllers;
using ShopBridge.DAL;
using System;
using Xunit;
using Moq;
using ShopBridge.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Test
{
    public class InventoryControllerTest
    {
        //InventoryController _controller;
        //IInventoryRepository _service;
        private readonly List<Inventory> _Inventories;
        public Mock<IInventoryRepository> mockRepo = new Mock<IInventoryRepository>();
        public InventoryControllerTest()
        {
            _Inventories = new List<Inventory>()
            {
                new Inventory() { ID=1,Name="Garments",Description="Garment Related Items",Price=2000 } ,
                new Inventory() { ID=2,Name="Musturd Oil",Description="Musturd Oil",Price=185 },
                new Inventory() { ID=3,Name="Vegatibles",Description="Tamoto, Patato",Price=470 },
                new Inventory() { ID=4,Name="Tea",Description="Tata Gold",Price=690 }
            };
        }

        private List<Inventory> GetTestInventory()
        {
            return _Inventories;

        }

        [Fact]
        public void Get_WhenCalled_Passed_ReturnsAllItems_CountMatch()
        {
            // define the setup on the mocked type
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetTestInventory());
            var controller = new InventoryController(mockRepo.Object);
            // Act
            // call the Index() method from the controller
            var okResult = controller.GetAll().Result as OkObjectResult;
            //Asert

            var items = Assert.IsType<List<Inventory>>(okResult.Value);
            Assert.Equal(4, items.Count);

        }

        [Fact]
        public void Get_WhenCalled_Fail_ReturnsAllItems_CountMatch()
        {
            // define the setup on the mocked type
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetTestInventory());
            var controller = new InventoryController(mockRepo.Object);
            // Act
            var okResult = controller.GetAll().Result as OkObjectResult;
            //Asert

            var items = Assert.IsType<List<Inventory>>(okResult.Value);
            Assert.Equal(3, items.Count);

        }
        [Fact]
        public void GetById_UnknownIDPassed_ReturnsNotFoundResult()
        {
            var item = new Inventory()
            {
                ID = 1,
                Name = "Garments",
                Description = "Garment Related Items",
                Price = 2000
            };
            // define the setup on the mocked type
            mockRepo.Setup(repo => repo.GetByID(1)).ReturnsAsync(item);
            var controller = new InventoryController(mockRepo.Object);
            
            // Act
            var Result = controller.GetById(8).Result as OkObjectResult;

            // Assert
            Assert.True(item.Equals(Result.Value));
        }

        [Fact]
        public void GetById_knownIDPassed_ReturnsFoundResult()
        {
            var item = new Inventory()
            {
                ID = 1,
                Name = "Garments",
                Description = "Garment Related Items",
                Price = 2000
            };

            mockRepo.Setup(repo => repo.GetByID(1)).ReturnsAsync(item);
            var controller = new InventoryController(mockRepo.Object);

            // Act
            var Result = controller.GetById(1).Result as OkObjectResult;

            // Assert
            Assert.True(item.Equals(Result.Value));

        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Inventory()
            {
                ID = 5,
                Description = "New Garment Related Items",
                Price = 2500
            };
            // Arrange
            //mockRepo.Setup(repo => repo.Add(nameMissingItem))
            //    .Verifiable();
            var controller = new InventoryController(mockRepo.Object);
            controller.ModelState.AddModelError("Name", "Required");
            //var newEmployee = GetTestInventory();

            // Act
            var badResponse = controller.Create(nameMissingItem).Result;

            // Assert
         //   Assert.IsType<BadRequestObjectResult>(badResponse);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(badResponse);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }


        [Fact]
        public void EditRecord_ReturnsFoundResult()
        {
            var newItem = new Inventory()
            {
                ID = 1,
                Name = "New Garments",
                Description = "New Garment Related Items",
                Price = 2500
            };

            mockRepo.Setup(repo => repo.EditInventory(It.IsAny<Inventory>()))
                .Verifiable();
            var controller = new InventoryController(mockRepo.Object);

            // Act
            var result = controller.Update(1, newItem).Result as OkObjectResult;

            // Assert
             Assert.Equal(200, result.StatusCode);
            //mockRepo.Verify();

        }

        [Fact]
        public void CreateRecord_ReturnsFoundResult()
        {
            var newItem = new Inventory()
            {
                ID = 5,
                Name = "New Garments",
                Description = "New Garment Related Items",
                Price = 2500
            };

            mockRepo.Setup(repo => repo.Add(It.IsAny<Inventory>()))
                .Verifiable();
            var controller = new InventoryController(mockRepo.Object);
          
            // Act
            var result = controller.Create(newItem).Result as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
            mockRepo.Verify();

        }



        [Fact]
        public void Remove_NotExistingIDPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = 7;

            // Act
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetTestInventory());
            var controller = new InventoryController(mockRepo.Object);

            var badResponse = controller.Delete(notExistingGuid).Result;

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = 1;

            // Act
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetTestInventory());
            var controller = new InventoryController(mockRepo.Object);

            var okResponse = controller.Delete(existingGuid).Result as OkResult;

            // Assert
            Assert.IsType<OkResult>(okResponse);
            //Assert.Equal(200,okResponse.StatusCode);
        }
        //[Fact]
        //public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        //{
        //    // Arrange
        //    var Item = new Inventory()
        //    {
        //        ID = 5,
        //        Name="Garment Items",
        //        Description = "New Garment Related Items",
        //        Price = 2500
        //    };

        //    // Act
        //    var createdResponse = _controller.Create(Item);

        //    // Assert
        //    Assert.IsType<CreatedAtActionResult>(createdResponse);
        //}


        //[Fact]
        //public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        //{
        //    // Arrange
        //    var item = new Inventory()
        //    {
        //        ID = 5,
        //        Name = "Garment Items",
        //        Description = "New Garment Related Items",
        //        Price = 2500
        //    };

        //    // Act
        //    var createdResponse = _controller.Create(item) as Task;
        //    //var item = createdResponse ;

        //    // Assert
        //    Assert.IsType<Inventory>(createdResponse);
        //    Assert.Equal("Guinness Original 6 Pack", item.Name);
        //}
    }
}
