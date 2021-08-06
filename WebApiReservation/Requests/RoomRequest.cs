using System.ComponentModel.DataAnnotations;

namespace WebApiReservation.Requests
{
    public class RoomRequest
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
