using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
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
                return Ok(_context.Chitietnhapkhos.Include("MaNhapNavigation").Include("MaSpNavigation").Select(x => new
                {
                    x.MaNhap,
                    x.MaSp,
                    x.SoLuong,
                    x.GiaNhap,
                    x.MaNhapNavigation,
                    x.MaSpNavigation
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public IActionResult Detail(int id, int masp)
        {
            try
            {
                return Ok(_context.Chitietnhapkhos.Include("MaNhapNavigation").Include("MaSpNavigation").Where(x => x.MaNhap == id && x.MaSp == masp).Select(x => new
                {
                    x.MaNhap,
                    x.MaSp,
                    x.SoLuong,
                    x.GiaNhap,
                    x.MaNhapNavigation,
                    x.MaSpNavigation
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [EnableQuery]
        public IActionResult Add(List<Chitietnhapkho> model, int idNv)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Nhapkho input = new Nhapkho();
                input.MaNhap = 0;
                input.MaNv = idNv;
                input.NgayNhap = DateTime.Now;

                double tongtienNhap = 0;
                foreach (Chitietnhapkho chitietnhap in model)
                {
                    tongtienNhap = (double)(tongtienNhap + (chitietnhap.GiaNhap * (double)chitietnhap.SoLuong));
                }
                input.TongTien = tongtienNhap;
                _context.Nhapkhos.Add(input);
                _context.SaveChanges();
                var latestValue = _context.Entry(input).Property(e => e.MaNhap).CurrentValue;
                foreach (Chitietnhapkho chitietnhap in model)
                {
                    chitietnhap.MaNhap = latestValue;
                }
                _context.Chitietnhapkhos.AddRange(model);
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
        public IActionResult Update(int id, List<Chitietnhapkho> model)
        {
            try
            {
                List<Chitietnhapkho> a = _context.Chitietnhapkhos.Where(a => a.MaNhap == id).ToList();
                Nhapkho nhapkho = _context.Nhapkhos.Where(x => x.MaNhap == id).FirstOrDefault();
                if (a == null || a.Count == 0|| nhapkho == null)
                {
                    return NotFound();
                }
                double tongtienNhap = 0;
                foreach (Chitietnhapkho chitietnhap in model)
                {
                    tongtienNhap = (double)(tongtienNhap + (chitietnhap.GiaNhap * (double)chitietnhap.SoLuong));
                }
                nhapkho.TongTien = tongtienNhap;
                _context.Nhapkhos.Update(nhapkho);
                _context.SaveChanges();
                
                foreach (Chitietnhapkho cur in a)
                {
                    foreach (Chitietnhapkho newData in model)
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
                _context.Chitietnhapkhos.UpdateRange(a);
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
                Chitietnhapkho a = _context.Chitietnhapkhos.Where(a => a.MaNhap == id && a.MaSp == masp).FirstOrDefault();
                _context.Chitietnhapkhos.Remove(a);
                _context.SaveChanges();
                Nhapkho nhapkho = _context.Nhapkhos.Where(x => x.MaNhap == id).FirstOrDefault();
                nhapkho.TongTien = nhapkho.TongTien - (a.GiaNhap * (double)a.SoLuong);
                _context.Nhapkhos.Update(nhapkho);
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
