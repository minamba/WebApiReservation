using System;
using System.Collections.Generic;

namespace Dal.Entities
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int StartSlot { get; set; }
        public int EndSlot { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
