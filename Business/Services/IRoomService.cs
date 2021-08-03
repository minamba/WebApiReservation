using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IRoomService
    {
        Task<List<RoomModel>> GetRoomsAsync();
    }
}
