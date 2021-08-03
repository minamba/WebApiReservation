using Business.Models;
using Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
   public  class RoomService : IRoomService
    {
        private IRoomRepository _repository;


        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<RoomModel>> GetRoomsAsync()
        {
            return await _repository.GetRoomsAsync();
        }
    }
}
