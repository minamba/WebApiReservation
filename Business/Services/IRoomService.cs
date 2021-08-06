using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IRoomService
    {
        Task<RoomModel> GetRoomByIdAsync(int id);
        Task<List<RoomModel>> GetRoomsAsync();
        Task<RoomModel> AddRoomAsync(RoomModel room);
        Task<RoomModel> UpdateRoomAsync(RoomModel room);
        Task<int> DeleteRoomAsync(int id);
    }
}
