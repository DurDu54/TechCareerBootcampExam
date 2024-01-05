using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCarreerBootcampExam.Models.ORM;

namespace TechCarreerBootcampExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly DBContext _context;
        public ReservationController(DBContext dBContext)
        {
            _context = dBContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var List = _context.Reservations.Include(x => x.Client).ThenInclude(x=>x.Company).Include(x => x.Room).ToList();
            if (List.Count() == 0)
            {
                return BadRequest();
            }
            return Ok(List);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var company = _context.Reservations.Where(x => x.Id == id).FirstOrDefault();
            return Ok(company);
        }

        [HttpPost]
        public IActionResult Post(Reservation reservation)
        {
            var client = _context.Clients.Where(x => x.Id == reservation.Client.CompanyId).FirstOrDefault();
            var room = _context.Rooms.Where(x => x.Id == reservation.RoomId).FirstOrDefault();
            reservation.Client = client;
            reservation.Room = room;
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Reservation reservation)
        {
            var reservationFromDb = _context.Reservations.Where(x => x.Id == reservation.Id).FirstOrDefault();
            if (reservationFromDb != null)
            {
                reservationFromDb.ClientId = reservation.ClientId;
                reservationFromDb.RoomId = reservation.RoomId;
                reservationFromDb.ReservationDate = reservation.ReservationDate;
                reservationFromDb.CheckInDate = reservation.CheckInDate;
                reservationFromDb.CheckOutDate = reservation.CheckOutDate;
                reservationFromDb.Status = reservation.Status;

                _context.SaveChanges();
                return Ok(reservationFromDb);
            }
            else return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);
            if (reservation == null) return NotFound();
            else
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
                return Ok(reservation);
            }
        }
    }
}
