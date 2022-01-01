using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Data;
using SampleAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appSettings;
        public UserController(MyDbContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {
            try
            {
                var allUser = _context.Users.AsQueryable();
                allUser = allUser.OrderBy(i => i.UserId);

                var categoryList = allUser.Select(user => new UserInfo
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    Role = user.Role
                }).ToList();

                return Ok(allUser);
            }
            catch
            {

                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateById(string id, Models.User model)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(item =>
                    item.UserId == Guid.Parse(id)
                );

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Password = model.Password;
                user.Role = model.Role;

                _context.SaveChanges();

                return Ok(user);
            }
            catch
            {

                return BadRequest();
            }
        }

        [HttpPost("Register")]
        public IActionResult Register(UserModel model)
        {
            try
            {
                var user = new Data.User
                {
                    UserId = new Guid(),
                    UserName = model.UserName,
                    Password = model.Password,
                    Role = "user"
                };

                _context.Add(user);
                _context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = user
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public IActionResult Validate(UserModel model)
        {
            var user = _context.Users.SingleOrDefault(u =>
                u.UserName == model.UserName && u.Password == model.Password
            );
            if (user == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Invalid username/password !!!"
                });
            }

            return Ok(new
            {
                Success = true,
                Message = "Authenticate success !!!",
                Data = GenerateToken(user)
            });
        }

        private string GenerateToken(Data.User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
