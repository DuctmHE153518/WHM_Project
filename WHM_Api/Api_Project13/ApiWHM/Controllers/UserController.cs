using ApiWHM.DTO;
using ApiWHM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiWHM.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private WhmanagementContext _context;

        public UserController(WhmanagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.Nhanviens.ToList());
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            string username = request.Username;
            string password = request.Password;
            foreach (Nhanvien nv in _context.Nhanviens)
            {
                if (nv.Username == username && nv.Password == password)
                {
                    return Ok(new { message = "Đăng nhập thành công!" });
                }
            }
            return Conflict(new { message = "Username hoặc password sai!" });
        }

        [HttpPost]
        public IActionResult Register(Nhanvien nhanvien)
        {
            try
            {
                _context.Nhanviens.Add(nhanvien);
                int result = _context.SaveChanges();
                return Ok(nhanvien.MaNv);
            }
            catch
            {
                return Conflict();
            }
        }



        /*[HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"]));
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                    );

                return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                return BadRequest("Invalid login attempt.");
            }
        }*/
    }
}
