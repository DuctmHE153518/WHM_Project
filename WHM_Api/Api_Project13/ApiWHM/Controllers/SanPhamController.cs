using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private WhmanagementContext _context = new WhmanagementContext();
        
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(_context.Sanphams.Include(x => x.MaLoaiSpNavigation).Select(x => new
                {
                    x.MaSp,
                    x.TenSp,
                    x.DonVi,
                    x.MoTa,
                    x.MaLoaiSp,
                    x.GiaBan,
                    x.SltonKho,
                    x.HinhAnh,
                    x.MaLoaiSpNavigation
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult TopSPBanChay()
        {
            try
            {
                var result = _context.Chitiethoadons
                .GroupBy(ct => ct.MaSp)
                .Select(group => new
                {
                    MaSp = group.Key,
                    TotalQuantity = group.Sum(ct => ct.SoLuong)
                }).OrderByDescending(item => item.TotalQuantity)
                .Take(5)
                .ToList();
                List<Sanpham> output = new List<Sanpham>();
                foreach (var item in result)
                {
                    Sanpham sp = _context.Sanphams.Where(x=> x.MaSp == item.MaSp).FirstOrDefault(); 
                    output.Add(sp);
                }

                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Detail(int id)
        {
            try
            {
                return Ok(_context.Sanphams.Include("MaLoaiSpNavigation").Where(x => x.MaSp == id).Select(x => new
                {
                    x.MaSp,
                    x.TenSp,
                    x.DonVi,
                    x.MoTa,
                    x.MaLoaiSp,
                    x.GiaBan,
                    x.SltonKho,
                    x.HinhAnh,
                    x.MaLoaiSpNavigation
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [EnableQuery]
        public IActionResult Add(Sanpham model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Sanphams.Add(model);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(Sanpham sanpham)
        {
            try
            {
                Sanpham sp = _context.Sanphams.FirstOrDefault(n => n.MaSp == sanpham.MaSp);
                if (sp is null)
                {
                    return StatusCode(444, "San pham is not found");
                }
                else
                {
                    sp.TenSp = sanpham.TenSp;
                    sp.DonVi = sanpham.DonVi;
                    sp.MoTa = sanpham.MoTa;
                    sp.MaLoaiSp = sanpham.MaLoaiSp;
                    sp.GiaBan = sanpham.GiaBan;
                    sp.SltonKho = sanpham.SltonKho;
                    sp.HinhAnh = sanpham.HinhAnh;
                    _context.Sanphams.Update(sp);
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
        [EnableQuery]
        public IActionResult Remove(int id)
        {
            try
            {
                Sanpham a = _context.Sanphams.FirstOrDefault(a => a.MaSp == id);
                _context.Sanphams.Remove(a);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{text}")]
        public ActionResult<Sanpham> Search(string text)
        {
            List<Sanpham> sanphams = _context.Sanphams.Where(sp => sp.TenSp.Contains(text)).ToList();
            if (sanphams is null) return NotFound();
            else return Ok(sanphams);
        }
    }
}
