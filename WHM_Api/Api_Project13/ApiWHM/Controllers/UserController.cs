using ApiWHM.DTO;
using ApiWHM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public ActionResult<Nhanvien> Get(int id)
        {
            Nhanvien? nhanvien = _context.Nhanviens.FirstOrDefault(nv => nv.MaNv == id);
            if (nhanvien is null) return NotFound();
            else return nhanvien;
        }

        [HttpPost]
        public ActionResult<Nhanvien> Search(string text)
        {
            List<Nhanvien> nhanvien = _context.Nhanviens.Where(nv => nv.HoTen.Equals(text) && nv.Email.Equals(text) 
            && nv.Username.Equals(text) && nv.ChucVu.Equals(text)).ToList();
            if (nhanvien is null) return NotFound();
            else return Ok(nhanvien);
        }

        [HttpPost]
        public IActionResult Add(Nhanvien nv)
        {
            try
            {
                _context.Nhanviens.Add(nv);
                int result = _context.SaveChanges();
                return Ok(nv.MaNv);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpPut]
        public IActionResult Edit(Nhanvien nv)
        {
            try
            {
                Nhanvien nhanvien = _context.Nhanviens.FirstOrDefault(n => n.MaNv == nv.MaNv);
                if (nhanvien is null)
                {
                    return StatusCode(444, "NhanVien is not found");
                }
                else
                {
                    nhanvien.HoTen = nv.HoTen;
                    nhanvien.NgaySinh = nv.NgaySinh;
                    nhanvien.QueQuan = nv.QueQuan;
                    nhanvien.Sdt = nv.Sdt;
                    nhanvien.Email = nv.Email;
                    nhanvien.ChucVu = nv.ChucVu;
                    nhanvien.Luong = nv.Luong;
                    nhanvien.Username = nv.Username;
                    nhanvien.Password = nv.Password;
                    int result = _context.SaveChanges();
                    return Ok(result);
                }
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpDelete]
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
