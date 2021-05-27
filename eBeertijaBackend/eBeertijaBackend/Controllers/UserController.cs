using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ebeertijaBackend.DatabaseContext;
using ebeertijaBackend.DTOs;
using ebeertijaBackend.Models;
using ebeertijaBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ebeertijaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly BeertijaContext context;
        private readonly AppSettings _appSettings;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            BeertijaContext context)
        {
            _userService = userService;
            _mapper = mapper;
            this.context = context;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Vrsta.ToString())
                }),
                Issuer = "Beertija",
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Vrsta,
                Token = tokenString
            });
        }

        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationUserDto userDto)
        {
            //var user = _mapper.Map<User>(userDto);

            User user = new User();
            user.Username = userDto.Username;
            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.OIB = userDto.OIB;
            user.Vrsta = userDto.Vrsta;

            var adminId = int.Parse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var admin = context.Users.Where(x => x.Id == adminId && x.Vrsta == VrstaUsera.ADMINISTRATOR || x.Vrsta == VrstaUsera.VODITELJ).FirstOrDefault();
            if (admin == null)
                return BadRequest("Konobare može registrirati samo administrator ili voditelj sustava.");

            try
            {
                _userService.Create(user, userDto.Password, userDto.Username);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "ADMINISTRATOR, VODITELJ")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var korisnikPoUloziId = int.Parse(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var korisnikPoUlozi = context.Users.Where(k => k.Id == korisnikPoUloziId).FirstOrDefault();

            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users.Where(u => (int)u.Vrsta >= (int)korisnikPoUlozi.Vrsta && u.IsActive).OrderBy(u => u.Username));
            return Ok(userDtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto userDto)
        {
            // map dto to entity and set id
            var user = context.Users.Where(u => u.Id == id).First();

            if (userDto.IsPasswordUpdate)
            {
                try
                {
                    _userService.Update(user, userDto.Password, userDto.PreviousPassword);
                    return Ok();
                }
                catch (ApplicationException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                try
                {
                    _userService.UpdateGeneral(userDto);
                    return Ok();
                }
                catch (ApplicationException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.Where(u => u.Id == id).First();

            user.IsActive = false;
            context.SaveChanges(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            return Ok();
        }
    }
}
