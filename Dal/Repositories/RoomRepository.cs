using AutoMapper;
using Business.Models;
using Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
