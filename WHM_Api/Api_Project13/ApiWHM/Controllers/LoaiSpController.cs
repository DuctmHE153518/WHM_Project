using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoaiSpController : ControllerBase
    {
        private WhmanagementContext _context = new WhmanagementContext();
        
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(_context.Loaisanphams.Select(x => new
                {
                    x.MaLoaiSp,
                    x.TenLoai
                }).ToList());
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
                return Ok(_context.Loaisanphams.Where(x => x.MaLoaiSp == id).Select(x => new
                {
                    x.MaLoaiSp,
                    x.TenLoai
                }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [EnableQuery]
        public IActionResult Add(Loaisanpham model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Loaisanphams.Add(model);
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
        public IActionResult Update(Loaisanpham model)
        {
            try
            {
                Loaisanpham a = _context.Loaisanphams.FirstOrDefault(a => a.MaLoaiSp == model.MaLoaiSp);
                if (a == null)
                {
                    return NotFound();
                }
                a.TenLoai = model.TenLoai;
                if (!ModelState.IsValid || a == null)
                {
                    return BadRequest(ModelState);
                }
                _context.Loaisanphams.Update(a);
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
                Loaisanpham a = _context.Loaisanphams.Where(a => a.MaLoaiSp == id).FirstOrDefault();
                _context.Loaisanphams.Remove(a);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{text}")]
        public ActionResult<Loaisanpham> Search(string text)
        {
            List<Loaisanpham> loaisanphams = _context.Loaisanphams.Where(lsp => lsp.TenLoai.Contains(text)).ToList();
            if (loaisanphams is null) return NotFound();
            else return Ok(loaisanphams);
        }
    }
}
