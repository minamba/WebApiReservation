using Business.Models;
using WebApiReservation.ViewModels;

namespace WebApiReservation.Responses
{
    public class RoomResponse
    {
        public RoomResponse(RoomModel room) => Room = new RoomViewModel
        {
            Id = room.Id,
            Name = room.Name
        };

        public RoomViewModel Room { get; }
    }
}
