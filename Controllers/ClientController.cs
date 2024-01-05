using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechCarreerBootcampExam.Models.ORM;

namespace TechCarreerBootcampExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClientController : ControllerBase
    {
        private readonly DBContext _context;
        public ClientController(DBContext dBContext)
        {
            _context = dBContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var List = _context.Clients.Include(x => x.Company).ToList();
            if (List.Count() == 0)
            {
                return BadRequest();
            }
            return Ok(List);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var company = _context.Clients.Where(x => x.Id == id).FirstOrDefault();
            return Ok(company);
        }

        [HttpPost]
        public IActionResult Post(Client client)
        {
            var company = _context.Companies.Where(x => x.Id != client.CompanyId).FirstOrDefault();
            client.Company = company;
            _context.Clients.Add(client);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, client);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Client client)
        {
            var clientFromDb = _context.Clients.Where(x => x.Id == client.Id).FirstOrDefault();
            if (clientFromDb != null)
            {
                clientFromDb.Name = client.Name;
                clientFromDb.Surname = client.Surname;
                clientFromDb.BirthDate = client.BirthDate;
                clientFromDb.Address = client.Address;
                clientFromDb.EMail = client.EMail;
                clientFromDb.Password = client.Password;
                
                _context.SaveChanges();
                return Ok(clientFromDb);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == id);
            if (client == null) return NotFound();
            else
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return Ok(client);
            }
        }
    }
}
