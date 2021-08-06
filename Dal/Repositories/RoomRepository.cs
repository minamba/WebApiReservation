using AutoMapper;
using Business.Models;
using Business.Repositories;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private KataHotelContext _context { get; set; }
        private readonly IMapper _mapper;

        public RoomRepository(KataHotelContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _context = dbContext;
        }

        public async Task<List<RoomModel>> GetRoomsAsync()
        {
            var roomEntity = await _context.Rooms.ToListAsync();
            var roomModel = roomEntity.Select(r => new RoomModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return roomModel;
        }

        public async Task<RoomModel> GetRoomByIdAsync(int id)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == id);

            if (room == null)
            {
                return null;
            }

            return new RoomModel
            {
                Id = room.Id,
                Name = room.Name
            };
        }

        public async Task<RoomModel> AddRoomAsync(RoomModel room)
        {
            var entity = new Room(0, room.Name);

            var result = await _context.Rooms.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new RoomModel
            {
                Id = result.Entity.Id,
                Name = result.Entity.Name
            };
        }

        public async Task<RoomModel> UpdateRoomAsync(RoomModel room)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == room.Id);

            if (entity == null)
            {
                return null;
            }

            entity.Name = room.Name;
            var result = _context.Rooms.Update(entity);
            await _context.SaveChangesAsync();

            return new RoomModel
            {
                Id = result.Entity.Id,
                Name = result.Entity.Name
            };
        }

        public async Task<int> DeleteRoomAsync(int id)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == id);

            if (entity == null)
            {
                return 0;
            }

            var result = _context.Rooms.Remove(entity);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }
    }
}
