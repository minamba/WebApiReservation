using System.Threading.Tasks;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApiReservation.Requests;
using WebApiReservation.Responses;

namespace WebApiReservation.Controllers
{
    [Route("Rooms")]
    public class RoomController : Controller
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetRoomByIdAsync([FromRoute] int id)
        {
            var room = await _service.GetRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(new RoomResponse(room));
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomsAsync()
        {
            var rooms = await _service.GetRoomsAsync();
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoomAsync([FromBody] RoomRequest roomRequest)
        {
            var room = new RoomModel
            {
                Name = roomRequest.Name
            };

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.AddRoomAsync(room);

            //return CreatedAtAction(nameof(GetRoomByIdAsync), new { id }); // Doesn't work...
            return StatusCode(201);
        }

        [Route("{id:int}")]
        [HttpPut]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute] int id, [FromBody] RoomRequest roomRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var room = new RoomModel
            {
                Id = id,
                Name = roomRequest.Name
            };

            var result =  await _service.UpdateRoomAsync(room);
                                                                                                                                                                                                          
            if (result == null)
            {
                return NotFound();
            }

            return Ok(new RoomResponse(result));
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] int id)
        {
            var resultId = await _service.DeleteRoomAsync(id);

            if (resultId == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
