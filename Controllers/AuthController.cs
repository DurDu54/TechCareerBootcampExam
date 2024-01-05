using Microsoft.AspNetCore.Mvc;
using TechCarreerBootcampExam.Models.Auth;
using TechCarreerBootcampExam.Models.ORM;

namespace TechCarreerBootcampExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DBContext _context;
        public AuthController(DBContext dBContext)
        {
            _context = dBContext;
        }

        [HttpPost]
        public IActionResult Login(DTOLoginRequestModel model)
        {
            var user = _context.Clients.FirstOrDefault(q => q.EMail == model.Email && q.Password == model.Password);

            if (user != null)
            {
                ExamTokenHandler tokenHandler = new ExamTokenHandler();
                var token = tokenHandler.CreateAccessToken(user.EMail);
                return Ok(token);
            }
            else
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı");
            }
        }
    }
}
