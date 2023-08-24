using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChitietxuatkhoController : ControllerBase
    {
        private WhmanagementContext _context = new WhmanagementContext();
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(_context.Chitietxuatkhos.Include("MaXuatNavigation").Include("MaSpNavigation").Select(x => new
                {
                    x.MaXuat,
                    x.MaSp,
                    x.SoLuong,
                    x.GiaNhap,
                    x.MaXuatNavigation,
                    x.MaSpNavigation
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Detail(int id, int masp)
        {
            try
            {
                return Ok(_context.Chitietxuatkhos.Include("MaXuatNavigation").Include("MaSpNavigation").Where(x => x.MaXuat == id && x.MaSp == masp).Select(x => new
                {
                    x.MaXuat,
                    x.MaSp,
                    x.SoLuong,
                    x.GiaNhap,
                    x.MaXuatNavigation,
                    x.MaSpNavigation
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}")]
        [EnableQuery]
        public IActionResult Add(List<Chitietxuatkho> model, int idNv)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Xuatkho input = new Xuatkho();
                input.MaXuat = 0;
                input.MaNv = idNv;
                input.NgayXuat = DateTime.Now;

                double tongtienXuat = 0;
                foreach (Chitietxuatkho chitietxuat in model)
                {
                    tongtienXuat = (double)(tongtienXuat + (chitietxuat.GiaNhap * (double)chitietxuat.SoLuong));
                }
                input.TongTien = tongtienXuat;
                _context.Xuatkhos.Add(input);
                _context.SaveChanges();
                var latestValue = _context.Entry(input).Property(e => e.MaXuat).CurrentValue;
                foreach (Chitietxuatkho chitietxuat in model)
                {
                    chitietxuat.MaXuat = latestValue;
                }
                _context.Chitietxuatkhos.AddRange(model);
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
        public IActionResult Update(int id, List<Chitietxuatkho> model)
        {
            try
            {
                List<Chitietxuatkho> a = _context.Chitietxuatkhos.Where(a => a.MaXuat == id).ToList();
                Xuatkho xuatkho = _context.Xuatkhos.FirstOrDefault(x => x.MaXuat == id);
                if (a == null || a.Count == 0 || xuatkho == null)
                {
                    return NotFound();
                }
                double tongtienNhap = 0;
                foreach (Chitietxuatkho chitietxuat in model)
                {
                    tongtienNhap = (double)(tongtienNhap + (chitietxuat.GiaNhap * (double)chitietxuat.SoLuong));
                }
                xuatkho.TongTien = tongtienNhap;
                _context.Xuatkhos.Update(xuatkho);
                _context.SaveChanges();

                foreach (Chitietxuatkho cur in a)
                {
                    foreach (Chitietxuatkho newData in model)
                    {
                        if (cur.MaSp == newData.MaSp)
                        {
                            cur.SoLuong = newData.SoLuong;
                            cur.GiaNhap = newData.GiaNhap;
                        }
                    }
                }
                if (!ModelState.IsValid || a == null)
                {
                    return BadRequest(ModelState);
                }
                _context.Chitietxuatkhos.UpdateRange(a);
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
        public IActionResult Remove(int id, int masp)
        {
            try
            {
                Chitietxuatkho a = _context.Chitietxuatkhos.FirstOrDefault(a => a.MaXuat == id && a.MaSp == masp);
                _context.Chitietxuatkhos.Remove(a);
                _context.SaveChanges();
                Xuatkho xuatkho = _context.Xuatkhos.FirstOrDefault(x => x.MaXuat == id);
                xuatkho.TongTien = xuatkho.TongTien - (a.GiaNhap * (double)a.SoLuong);
                _context.Xuatkhos.Update(xuatkho);
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
