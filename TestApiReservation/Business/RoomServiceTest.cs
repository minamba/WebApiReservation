using Business.Models;
using Business.Repositories;
using Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApiReservation.Business
{
    [TestClass]
    public class RoomServiceTest
    {
        [TestMethod]
        public async Task Shoud_Get_Rooms_In_RoomService()
        {
            var rooms = new List<RoomModel>()
            {
                new RoomModel (1,"Room 0"),
                new RoomModel (1,"Room 1"),
                new RoomModel (1,"Room 2")
            };

            var mockRepository = Substitute.For<IRoomRepository>();
            mockRepository.GetRoomsAsync().Returns(rooms);

            var roomService = new RoomService(mockRepository);
            var result = await roomService.GetRoomsAsync();

            string serialize1 = JsonConvert.SerializeObject(rooms);
            string serialize2 = JsonConvert.SerializeObject(result);

            Assert.AreEqual(serialize1, serialize2);
        }

        [TestMethod]
        public async Task Shoud_Get_Room_By_Id()
        {
            var room = new RoomModel
            {
                Id = 2,
                Name = "Room 2"
            };

            var mockRepository = Substitute.For<IRoomRepository>();
            mockRepository.GetRoomByIdAsync(2).Returns(room);
            var roomService = new RoomService(mockRepository);

            var result = await roomService.GetRoomByIdAsync(2);

            Assert.IsNotNull(result);
            Assert.AreEqual("Room 2", result.Name);
        }

        [TestMethod]
        public async Task Shoud_Add_Room()
        {
            var room = new RoomModel
            {
                Name = "Room 2"
            };

            var mockRepository = Substitute.For<IRoomRepository>();
            mockRepository.AddRoomAsync(room).Returns(new RoomModel
            {
                Id = 2,
                Name = "Room 2"
            });
            var roomService = new RoomService(mockRepository);

            var result = await roomService.AddRoomAsync(room);

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("Room 2", result.Name);
        }

        [TestMethod]
        public async Task Shoud_Update_Room()
        {
            var room = new RoomModel
            {
                Id = 2,
                Name = "Room 2"
            };

            var mockRepository = Substitute.For<IRoomRepository>();
            mockRepository.UpdateRoomAsync(room).Returns(new RoomModel
            {
                Id = 2,
                Name = "Room 3"
            });
            var roomService = new RoomService(mockRepository);

            var result = await roomService.UpdateRoomAsync(room);

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("Room 3", result.Name);
        }

        [TestMethod]
        public async Task Shoud_Delete_Room()
        {
            var mockRepository = Substitute.For<IRoomRepository>();
            mockRepository.DeleteRoomAsync(2).Returns(2);
            var roomService = new RoomService(mockRepository);

            var id = await roomService.DeleteRoomAsync(2);

            Assert.AreEqual(2, id);
        }
    }
}
