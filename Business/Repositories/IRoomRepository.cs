using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IRoomRepository
    {
        Task<List<RoomModel>> GetRoomsAsync();
        Task<RoomModel> GetRoomByIdAsync(int id);
        Task<RoomModel> AddRoomAsync(RoomModel room);
        Task<RoomModel> UpdateRoomAsync(RoomModel room);
        Task<int> DeleteRoomAsync(int id);
    }
}
