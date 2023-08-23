using ApiWHM.DTO;
using ApiWHM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        public IActionResult Login(LoginRequest request)
        {
            string username = request.Username;
            string password = request.Password;
            foreach (Nhanvien nv in _context.Nhanviens)
            {
                if (nv.Username == username && nv.Password == password)
                {
                    return Ok(new { message = "Login Successful!" });
                }
            }
            return Conflict(new { message = "Wrong username or password!" });
        }

        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            string email = request.Email;
            string username = request.Username;
            string password = request.Password;
            string repassword = request.RePassword;
            foreach (Nhanvien nv in _context.Nhanviens)
            {
                if (nv.Email == email)
                {
                    return StatusCode(444, "Email already exists!");
                }
                else if (nv.Username == username)
                {
                    return StatusCode(444, "Username already exists!");
                }
                else if (password != repassword)
                {
                    return StatusCode(444, "Wrong password!");
                }
            }

            try
            {
                Nhanvien nhanvien = new Nhanvien
                {
                    HoTen = null,
                    NgaySinh = null,
                    QueQuan = null,
                    Sdt = null,
                    Email = email,
                    ChucVu = "Staff",
                    Luong = 200000,
                    Username = username,
                    Password = password
                };
                _context.Nhanviens.Add(nhanvien);
                _context.SaveChanges();
                return Ok(nhanvien.MaNv);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Nhanvien> Get(int id)
        {
            Nhanvien? nhanvien = _context.Nhanviens.FirstOrDefault(nv => nv.MaNv == id);
            if (nhanvien is null) return NotFound();
            else return nhanvien;
        }

        [HttpGet("{text}")]
        public ActionResult<Nhanvien> Search(string text)
        {
            List<Nhanvien> nhanvien = _context.Nhanviens.Where(nv => nv.HoTen.Contains(text) || nv.Email.Contains(text)
            || nv.Username.Contains(text) || nv.ChucVu.Contains(text)).ToList();
            if (nhanvien is null) return NotFound();
            else return Ok(nhanvien);
        }

        [HttpPost]
        public IActionResult Add(Nhanvien nhanvien)
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

        [HttpPut]
        public IActionResult Edit(Nhanvien nhanvien)
        {
            try
            {
                Nhanvien nv = _context.Nhanviens.FirstOrDefault(n => n.MaNv == nhanvien.MaNv);
                if (nv is null)
                {
                    return StatusCode(444, "NhanVien is not found");
                }
                else
                {
                    nv.HoTen = nhanvien.HoTen;
                    nv.NgaySinh = nhanvien.NgaySinh;
                    nv.QueQuan = nhanvien.QueQuan;
                    nv.Sdt = nhanvien.Sdt;
                    nv.Email = nhanvien.Email;
                    nv.ChucVu = nhanvien.ChucVu;
                    nv.Luong = nhanvien.Luong;
                    nv.Username = nhanvien.Username;
                    nv.Password = nhanvien.Password;
                    _context.Nhanviens.Update(nv);
                    int result = _context.SaveChanges();
                    return Ok(result);
                }
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Nhanvien nhanvien = _context.Nhanviens.FirstOrDefault(nv => nv.MaNv == id);
                if (nhanvien is null)
                {
                    return StatusCode(444, "NhanVien is not found");
                }
                else
                {
                    _context.Nhanviens.Remove(nhanvien);
                    int result = _context.SaveChanges();
                    return Ok(result);
                }
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
