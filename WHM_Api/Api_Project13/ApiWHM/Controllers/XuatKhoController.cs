using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XuatKhoController : ControllerBase
    {
        private WhmanagementContext _context = new WhmanagementContext();
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            try
            {
                return Ok(_context.Xuatkhos.Include("MaNvNavigation").Select(x => new
                {
                    x.MaXuat,
                    x.NgayXuat,
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
                return Ok(_context.Xuatkhos.Include("MaNvNavigation").Where(x => x.MaXuat == id).Select(x => new
                {
                    x.MaXuat,
                    x.NgayXuat,
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
        public IActionResult Add(Xuatkho model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Xuatkhos.Add(model);
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
        public IActionResult Update(int id, Xuatkho model)
        {
            try
            {
                Xuatkho a = _context.Xuatkhos.Where(a => a.MaXuat == id).FirstOrDefault();
                if (a == null)
                {
                    return NotFound();
                }
                a.MaNv = model.MaNv;
                a.NgayXuat = model.NgayXuat;
                a.TongTien = model.TongTien;
                if (!ModelState.IsValid || a == null)
                {
                    return BadRequest(ModelState);
                }
                _context.Xuatkhos.Update(a);
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
                Xuatkho a = _context.Xuatkhos.Where(a => a.MaXuat == id).FirstOrDefault();
                _context.Xuatkhos.Remove(a);
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
