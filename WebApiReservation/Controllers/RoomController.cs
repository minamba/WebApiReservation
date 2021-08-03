using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiReservation.Controllers
{
    public class RoomController : Controller
    {
        private IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [Route("Rooms")]
        [HttpGet]
        public async Task<IActionResult> GetRoomsAsync()
        {
            var rooms = await _service.GetRoomsAsync();
            return Ok(rooms);
        }
    }
}
