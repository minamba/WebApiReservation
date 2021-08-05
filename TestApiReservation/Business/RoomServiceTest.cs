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
                new RoomModel (1,"Room 2"),

            };

            var mockRepository = Substitute.For<IRoomRepository>();
            mockRepository.GetRoomsAsync().Returns(rooms);

            var roomService = new RoomService(mockRepository);
            var result = await roomService.GetRoomsAsync();

            string serialize1 = JsonConvert.SerializeObject(rooms);
            string serialize2 = JsonConvert.SerializeObject(result);

            Assert.AreEqual(serialize1, serialize2);
        }

    }
}
