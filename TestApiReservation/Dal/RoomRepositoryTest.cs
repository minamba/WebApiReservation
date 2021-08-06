using AutoMapper;
using Business.Models;
using Dal;
using Dal.Entities;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiReservation.Dal
{
    [TestClass]
    public class RoomRepositoryTest
    {
        private readonly static IMapper _mapper;

        [TestMethod]
        public async Task Shoud_Get_Rooms_In_RoomRepository()
        {
            var options = new DbContextOptionsBuilder<KataHotelContext>()
            .UseInMemoryDatabase(databaseName: "Get_rooms_repository")
            .Options;

            using var context = new KataHotelContext(options);
            
            context.Rooms.Add(new Room(1, "room1"));
            context.Rooms.Add(new Room(2, "room2"));
            context.Rooms.Add(new Room(3, "room3"));

            var roomRepository = new RoomRepository(context, _mapper);
            List<Room> expectedList;

            expectedList = context.Rooms.ToList();
            var result = await roomRepository.GetRoomsAsync();

            CollectionAssert.AreEqual(expectedList, result);
        }

        [TestMethod]
        public async Task Shoud_Add_Room()
        {
            var options = new DbContextOptionsBuilder<KataHotelContext>()
            .UseInMemoryDatabase(databaseName: "Add_room_repository")
            .Options;

            var room = new RoomModel
            {
                Name = "Room 2"
            };

            using var context = new KataHotelContext(options);
            
            var roomRepository = new RoomRepository(context, _mapper);
            var id = await roomRepository.AddRoomAsync(room);

            Assert.AreEqual(1, context.Rooms.Count());
        }

        [TestMethod]
        public async Task Shoud_Update_Room()
        {
            var options = new DbContextOptionsBuilder<KataHotelContext>()
            .UseInMemoryDatabase(databaseName: "Update_room_repository")
            .Options;

            var room = new RoomModel
            {
                Id = 2,
                Name = "Room 3"
            };

            using var context = new KataHotelContext(options);
            
            await context.Rooms.AddAsync(new Room
            {
                Id = 2,
                Name = "Room 2"
            });
            await context.SaveChangesAsync();

            var roomRepository = new RoomRepository(context, _mapper);
            var result = await roomRepository.UpdateRoomAsync(room);

            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("Room 3", result.Name);
            Assert.AreEqual(1, context.Rooms.Count());
            Assert.AreEqual("Room 3", context.Rooms.Single(r => r.Id == 2).Name);
        }

        [TestMethod]
        public async Task Shoud_Update_Room_When_Unknown_Id()
        {
            var options = new DbContextOptionsBuilder<KataHotelContext>()
            .UseInMemoryDatabase(databaseName: "Update_room_repository_when_unknown_id")
            .Options;

            var room = new RoomModel
            {
                Id = 3,
                Name = "Room 3"
            };

            using var context = new KataHotelContext(options);
            
            await context.Rooms.AddAsync(new Room
            {
                Id = 2,
                Name = "Room 2"
            });
            await context.SaveChangesAsync();

            var roomRepository = new RoomRepository(context, _mapper);
            var result = await roomRepository.UpdateRoomAsync(room);

            Assert.IsNull(result);
            Assert.AreEqual(1, context.Rooms.Count());
            Assert.AreEqual("Room 2", context.Rooms.Single(r => r.Id == 2).Name);
        }

        [TestMethod]
        public async Task Shoud_Delete_Room()
        {
            var options = new DbContextOptionsBuilder<KataHotelContext>()
            .UseInMemoryDatabase(databaseName: "Delete_room_repository")
            .Options;

            using var context = new KataHotelContext(options);
            
            await context.Rooms.AddAsync(new Room
            {
                Id = 2,
                Name = "Room 2"
            });
            await context.SaveChangesAsync();

            var roomRepository = new RoomRepository(context, _mapper);
            var id = await roomRepository.DeleteRoomAsync(2);

            Assert.AreEqual(2, id);
            Assert.AreEqual(0, context.Rooms.Count());
        }

        [TestMethod]
        public async Task Shoud_Delete_Room_When_Unknown_Id()
        {
            var options = new DbContextOptionsBuilder<KataHotelContext>()
            .UseInMemoryDatabase(databaseName: "Delete_room_repository_when_unknown_id")
            .Options;

            using var context = new KataHotelContext(options);
            
            await context.Rooms.AddAsync(new Room
            {
                Id = 2,
                Name = "Room 2"
            });
            await context.SaveChangesAsync();

            var roomRepository = new RoomRepository(context, _mapper);
            var id = await roomRepository.DeleteRoomAsync(3);

            Assert.AreEqual(0, id);
            Assert.AreEqual(1, context.Rooms.Count());
        }
    }
}
