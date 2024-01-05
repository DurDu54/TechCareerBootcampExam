using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCarreerBootcampExam.Models.ORM;


namespace TechCarreerBootcampExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RoomController : ControllerBase
    {

        private readonly DBContext _context;

        public RoomController(DBContext dBContext )
        {
            _context = dBContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var List = _context.Rooms.ToList();
            if (List.Count() == 0)
            {
                return BadRequest();
            }
            return Ok(List);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var company = _context.Rooms.Where(x => x.Id == id).FirstOrDefault();
            return Ok(company);
        }

        [HttpPost]
        public IActionResult Post(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, room);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Room updatedRoom)
        {
            var roomFromDb = _context.Rooms.FirstOrDefault(x => x.Id == id);

            if (roomFromDb == null)
            {
                return NotFound("Oda bulunamadı");
            }

                roomFromDb.Name = updatedRoom.Name;
            
                roomFromDb.Capacity = updatedRoom.Capacity;
            

            _context.SaveChanges();

            return Ok(roomFromDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = _context.Companies.FirstOrDefault(x => x.Id == id);
            if (room == null) return NotFound();
            else
            {
                _context.Companies.Remove(room);
                _context.SaveChanges();
                return Ok(room);
            }
        }
    }
}
