using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tasiapi.Dtos;
using tasiapi.Interfaces;
using tasiapi.Models;

namespace tasiapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        NpgsqlConnection _connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=tasinmaz;Username=postgres;Password=1234");
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (_authRepository.UserExists(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("Username", "Username already exists");

                if (ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }
            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };
            var createdUser =  _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);

        }
        [HttpPost("login")]

        public ActionResult Login([FromBody]UserForLoginDto userForLoginDto)
        {
            var user = _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);
            if (user == null)
            {
                return Unauthorized();

            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                }
                ),
                Expires = DateTime.Now.AddDays(40),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512)
            };
            var token=tokenHandler.CreateToken(tokenDescriptor);
            var tokenString= tokenHandler.WriteToken(token);
            return Ok( tokenString );   




            }

        }
    }

