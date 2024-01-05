using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechCarreerBootcampExam.Models.ORM;

namespace TechCarreerBootcampExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DBContext _context;
        public CompanyController(DBContext dBContext )
        {
            _context = dBContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var List = _context.Companies.ToList();
            if (List.Count()==0)
            {
                return BadRequest();
            }
            return Ok(List);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var company = _context.Companies.Where(x=> x.Id==id).FirstOrDefault(); 
            return Ok(company);
        }

        [HttpPost]
        public IActionResult Post(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, company);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Company company)
        {
            var companyFromDb = _context.Companies.Where(x => x.Id == company.Id).FirstOrDefault();

            if (companyFromDb != null)
            {
                companyFromDb.Name = company.Name;
                companyFromDb.Address = company.Address;

                _context.SaveChanges();
                return Ok(companyFromDb);
            }
            else return NotFound();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null) return NotFound();
            else
            {
                _context.Companies.Remove(company);
                _context.SaveChanges();
                return Ok(company);
            }
        }
    }
}
