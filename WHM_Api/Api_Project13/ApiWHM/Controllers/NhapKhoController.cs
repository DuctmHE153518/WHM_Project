using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhapKhoController : ControllerBase
    {
        private WhmanagementContext _context = new WhmanagementContext();
        [HttpGet]
        [EnableQuery]
        [Route("List")]
        public IActionResult List()
        {
            try
            {
                return Ok(_context.Nhapkhos.Include("MaNvNavigation").Include("Chitietnhapkhos").Select(x => new
                {
                    x.MaNhap,
                    x.NgayNhap,
                    x.MaNvNavigation,
                    x.TongTien,
                    x.Chitietnhapkhos
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
                return Ok(_context.Nhapkhos.Include("MaNvNavigation").Include("Chitietnhapkhos").Where(x => x.MaNhap == id).Select(x => new
                {
                    x.MaNhap,
                    x.NgayNhap,
                    x.MaNvNavigation,
                    x.TongTien,
                    x.Chitietnhapkhos
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [EnableQuery]
        public IActionResult Add(Nhapkho model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Nhapkhos.Add(model);
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
        public IActionResult Update(int id, Nhapkho model)
        {
            try
            {
                Nhapkho a = _context.Nhapkhos.Where(a => a.MaNhap == id).FirstOrDefault();
                if(a == null)
                {
                    return NotFound();
                }
                a.MaNv = model.MaNv;
                a.NgayNhap = model.NgayNhap;
                a.TongTien = model.TongTien;
                if (!ModelState.IsValid || a == null)
                {
                    return BadRequest(ModelState);
                }
                _context.Nhapkhos.Update(a);
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
                Nhapkho a = _context.Nhapkhos.Where(a => a.MaNhap == id).FirstOrDefault();
                _context.Nhapkhos.Remove(a);
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
