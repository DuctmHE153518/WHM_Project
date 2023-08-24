using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private WhmanagementContext _context = new WhmanagementContext();
        [HttpGet]
        [Route("List")]
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
                    x.Slton,
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
        [Route("Detail/{id}")]
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
                    x.Slton,
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
        [HttpPut("{id}")]
        [EnableQuery]
        public IActionResult Update(int id, Sanpham model)
        {
            try
            {
                Sanpham a = _context.Sanphams.Where(a => a.MaSp == id).FirstOrDefault();
                if (a == null)
                {
                    return NotFound();
                }
                a.TenSp = model.TenSp;
                a.DonVi = model.DonVi;
                a.MoTa = model.MoTa;
                a.MaLoaiSp = model.MaLoaiSp;
                a.GiaBan = model.GiaBan;
                a.Slton = model.Slton;
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
    }
}
