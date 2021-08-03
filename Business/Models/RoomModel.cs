using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public RoomModel(int _id, string _name)
        {
            Id = _id;
            Name = _name;
        }
        public RoomModel()
        { }
    }
}
