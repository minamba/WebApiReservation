﻿using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IRoomRepository
    {
        Task<List<RoomModel>> GetRoomsAsync();
    }
}