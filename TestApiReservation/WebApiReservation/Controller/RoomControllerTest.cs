using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApiReservation.Controllers;

namespace TestApiReservation.WebApiReservation.Controller
{
    public class RoomControllerTest
    {
        [TestMethod]
        public async Task Shoud_Get_Rooms_In_Controller()
        {
            var rooms = new List<RoomModel>()
            {
                new RoomModel (1,"Room 0"),
                new RoomModel (1,"Room 1"),
                new RoomModel (1,"Room 2"),

            };

            var roomService = Substitute.For<IRoomService>();
            roomService.GetRoomsAsync().Returns(rooms);
            var roomController = new RoomController(roomService);

            var result = await roomController.GetRoomsAsync();
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
