using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiReservation.Controllers;
using WebApiReservation.Requests;
using WebApiReservation.Responses;

namespace TestApiReservation.WebApiReservation.Controllers
{
    [TestClass]
    public class RoomControllerTests
    {
        [TestMethod]
        public async Task Shoud_Get_Rooms_In_Controller()
        {
            var rooms = new List<RoomModel>()
            {
                new RoomModel (1, "Room 0"),
                new RoomModel (2, "Room 1"),
                new RoomModel (3, "Room 2")
            };

            var roomService = Substitute.For<IRoomService>();
            roomService.GetRoomsAsync().Returns(rooms);
            var roomController = new RoomController(roomService);

            var result = await roomController.GetRoomsAsync();
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Shoud_Get_Room_By_Id()
        {
            var room = new RoomModel
            {
                Id = 4,
                Name = "Room 4"
            };

            var roomService = Substitute.For<IRoomService>();
            roomService.GetRoomByIdAsync(4).Returns(room);
            var roomController = new RoomController(roomService);

            var result = await roomController.GetRoomByIdAsync(4);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(((RoomResponse)okResult.Value).Room.Name, "Room 4");
        }

        [TestMethod]
        public async Task Shoud_Return_404_When_Getting_Room_By_Unknown_Id()
        {
            var roomService = Substitute.For<IRoomService>();
            var roomController = new RoomController(roomService);

            var result = await roomController.GetRoomByIdAsync(4);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(404, ((NotFoundResult)result).StatusCode);
        }

        [TestMethod]
        public async Task Shoud_Add_Room()
        {
            var roomRequest = new RoomRequest
            {
                Name = "Room 1"
            };

            var roomService = Substitute.For<IRoomService>();
            roomService.AddRoomAsync(default).ReturnsForAnyArgs(new RoomModel
            {
                Id = 1,
                Name = "Room 1"
            });
            var roomController = new RoomController(roomService);

            var result = await roomController.AddRoomAsync(roomRequest);

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(201, ((StatusCodeResult)result).StatusCode);
            //Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            //var createdResult = (CreatedAtActionResult)result;
            //Assert.AreEqual(201, createdResult.StatusCode);
            //Assert.AreEqual("GetRoomByIdAsync", createdResult.ActionName);
        }

        [TestMethod]
        public async Task Shoud_Return_400_When_Adding_Room_With_Bad_Request()
        {
            var roomRequest = new RoomRequest();
            var roomService = Substitute.For<IRoomService>();
            var roomController = new RoomController(roomService);
            roomController.ModelState.AddModelError(string.Empty, string.Empty);

            var result = await roomController.AddRoomAsync(roomRequest);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(400, ((BadRequestResult)result).StatusCode);
        }

        [TestMethod]
        public async Task Shoud_Update_Room()
        {
            var roomRequest = new RoomRequest
            {
                Name = "Room 5"
            };

            var roomService = Substitute.For<IRoomService>();
            roomService.UpdateRoomAsync(default).ReturnsForAnyArgs(new RoomModel
            {
                Id = 5,
                Name = "Room 4"
            });
            var roomController = new RoomController(roomService);

            var result = await roomController.UpdateRoomAsync(5, roomRequest);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = (OkObjectResult)result;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(5, ((RoomResponse)okObjectResult.Value).Room.Id);
            Assert.AreEqual("Room 4", ((RoomResponse)okObjectResult.Value).Room.Name);
        }

        [TestMethod]
        public async Task Shoud_Return_400_When_Updating_Room_With_Bad_Request()
        {
            var roomRequest = new RoomRequest();
            var roomService = Substitute.For<IRoomService>();
            var roomController = new RoomController(roomService);
            roomController.ModelState.AddModelError(string.Empty, string.Empty);

            var result = await roomController.UpdateRoomAsync(5, roomRequest);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            Assert.AreEqual(400, ((BadRequestResult)result).StatusCode);
        }

        [TestMethod]
        public async Task Shoud_Return_404_When_Updating_Room_With_Unknown_Id()
        {
            var roomRequest = new RoomRequest();
            var roomService = Substitute.For<IRoomService>();
            var roomController = new RoomController(roomService);

            var result = await roomController.UpdateRoomAsync(5, roomRequest);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(404, ((NotFoundResult)result).StatusCode);
        }

        [TestMethod]
        public async Task Shoud_Delete_Room()
        {
            var roomService = Substitute.For<IRoomService>();
            roomService.DeleteRoomAsync(5).Returns(5);
            var roomController = new RoomController(roomService);

            var result = await roomController.DeleteRoomAsync(5);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.AreEqual(204, ((NoContentResult)result).StatusCode);
        }

        [TestMethod]
        public async Task Shoud_Return_404_When_Deleting_Room_With_Unknown_Id()
        {
            var roomService = Substitute.For<IRoomService>();
            var roomController = new RoomController(roomService);

            var result = await roomController.DeleteRoomAsync(5);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(404, ((NotFoundResult)result).StatusCode);
        }
    }
}
