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
                return Ok(_context.Sanphams.Include("MaLoaiSpNavigation").Select(x => new
                {
                    x.MaSp,
                    x.TenSp,
                    x.DonVi,
                    x.MoTa ,
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
        [EnableQuery]
        public IActionResult Update(Sanpham model)
        {
            try
            {
                Sanpham a = _context.Sanphams.Where(a => a.MaSp == model.MaSp).FirstOrDefault();
                if (a == null)
                {
                    return NotFound();
                }
                a.TenSp = model.TenSp;
                a.DonVi = model.DonVi;
                a.MoTa = model.MoTa;
                a.MaLoaiSp = model.MaLoaiSp;
                a.GiaBan = model.GiaBan;
                a.SltonKho = model.SltonKho;
                a.HinhAnh = model.HinhAnh;

                if (!ModelState.IsValid || a == null)
                {
                    return BadRequest(ModelState);
                }
                _context.Sanphams.Update(a);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [EnableQuery]
        public IActionResult Remove(int id)
        {
            try
            {
                Sanpham a = _context.Sanphams.Where(a => a.MaSp == id).FirstOrDefault();
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
