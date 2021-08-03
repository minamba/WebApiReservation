using System;
using System.Collections.Generic;


namespace Dal.Entities
{
    public partial class Room
    {
        public Room()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }


        public Room(int _id, string _name)
        {
            Id = _id;
            Name = _name;
        }
    }
}
