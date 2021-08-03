using AutoMapper;
using Dal;
using Dal.Entities;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiReservation.WebApiReservation.Repositories
{
    public class RoomRepositoryTest
    {
        private static IMapper _mapper;

        [TestMethod]
        public async Task Shoud_Get_Rooms_In_RoomRepository()
        {
            var options = new DbContextOptionsBuilder<KataHotelContext>()
            .UseInMemoryDatabase(databaseName: "Get_rooms_repository")
            .Options;

            using (var context = new KataHotelContext(options))
            {
                context.Rooms.Add(new Room(1, "room1"));
                context.Rooms.Add(new Room(2, "room2"));
                context.Rooms.Add(new Room(3, "room3"));

                var roomRepository = new RoomRepository(context, _mapper);
                List<Room> expectedList;

                expectedList = context.Rooms.ToList();
                var result = await roomRepository.GetRoomsAsync();

                CollectionAssert.AreEqual(expectedList, result);
            }
        }



        //[TestMethod]
        //public async Task Shoud_Add_Rooms_In_RoomRepository()
        //{
        //    var options = new DbContextOptionsBuilder<KataHotelContext>()
        //    .UseInMemoryDatabase(databaseName: "Get_rooms_repository")
        //    .Options;

        //    using (var context = new KataHotelContext(options))
        //    {
        //        context.Rooms.Add(new Room(1, "room1"));
        //        context.Rooms.Add(new Room(2, "room2"));
        //        context.Rooms.Add(new Room(3, "room3"));

        //        var roomRepository = new RoomRepository(context, _mapper);
        //        List<Room> expectedList;

        //        expectedList = context.Rooms.ToList();
        //        var result = await roomRepository.GetRooms();

        //        CollectionAssert.AreEqual(expectedList, result);
        //    }
        //}
    }
}
