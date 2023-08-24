using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietChitietnhapkhoController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ChitietnhapkhoController : ControllerBase
        {
            private WhmanagementContext _context = new WhmanagementContext();
            [HttpGet]
            [Route("List")]
            public IActionResult List()
            {
                try
                {
                    return Ok(_context.Chitietnhapkhos.Include("MaNvNavigation").Select(x => new
                    {
                        x.MaNhap,
                        x.NgayNhap,
                        x.MaNvNavigation,
                        x.TongTien
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
                    return Ok(_context.Chitietnhapkhos.Include("MaNvNavigation").Where(x => x.MaNhap == id).Select(x => new
                    {
                        x.MaNhap,
                        x.NgayNhap,
                        x.MaNvNavigation,
                        x.TongTien
                    }).ToList());
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            [HttpPost]
            [EnableQuery]
            public IActionResult Add(Chitietnhapkho model)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    _context.Chitietnhapkhos.Add(model);
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
            public IActionResult Update(int id, Chitietnhapkho model)
            {
                try
                {
                    Chitietnhapkho a = _context.Chitietnhapkhos.Where(a => a.MaNhap == id).FirstOrDefault();
                    if (a == null)
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
                    _context.Chitietnhapkhos.Update(a);
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
                    Chitietnhapkho a = _context.Chitietnhapkhos.Where(a => a.MaNhap == id).FirstOrDefault();
                    _context.Chitietnhapkhos.Remove(a);
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
}
