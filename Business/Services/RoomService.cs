using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;
using Business.Repositories;

namespace Business.Services
{
    public  class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<RoomModel> GetRoomByIdAsync(int id) => await _repository.GetRoomByIdAsync(id);

        public async Task<List<RoomModel>> GetRoomsAsync()
        {
            return await _repository.GetRoomsAsync();
        }

        public async Task<RoomModel> AddRoomAsync(RoomModel room) => await _repository.AddRoomAsync(room);

        public async Task<RoomModel> UpdateRoomAsync(RoomModel room) => await _repository.UpdateRoomAsync(room);

        public async Task<int> DeleteRoomAsync(int id) => await _repository.DeleteRoomAsync(id);
    }
}
