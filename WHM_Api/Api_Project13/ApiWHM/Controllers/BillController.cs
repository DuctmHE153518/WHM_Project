using ApiWHM.DTO;
using ApiWHM.Models;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiWHM.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BillController : Controller
    {
        private WhmanagementContext _context;

        public BillController(WhmanagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_context.Hoadons.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Hoadon> Get(int id)
        {
            Hoadon? hoadon = _context.Hoadons.FirstOrDefault(nv => nv.MaHd == id);
            if (hoadon is null) return NotFound();
            else return hoadon;
        }

        [HttpGet("{text}")]
        public ActionResult<Hoadon> Search(string text)
        {
            List<Hoadon> hoadons = _context.Hoadons.Where(hd => hd.HoTenKh.Contains(text) || hd.Sdt.Contains(text)).ToList();
            if (hoadons is null) return NotFound();
            else return Ok(hoadons);
        }

        [HttpGet("{id}")]
        public ActionResult<Chitiethoadon> ListBillDetail(int id)
        {
            List<Chitiethoadon> chitiethoadons = _context.Chitiethoadons.Where(bd => bd.MaHd == id).ToList();
            if (chitiethoadons is null) return NotFound();
            else return Ok(chitiethoadons);
        }

        [HttpPost]
        public IActionResult Add(Hoadon hoadon)
        {
            try
            {
                _context.Hoadons.Add(hoadon);
                _context.SaveChanges();
                return Ok(hoadon.MaHd);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Hoadon hoadon = _context.Hoadons.FirstOrDefault(hd => hd.MaHd == id);
                if (hoadon is null)
                {
                    return StatusCode(444, "NhanVien is not found");
                }
                else
                {
                    _context.Hoadons.Remove(hoadon);
                    int result = _context.SaveChanges();
                    return Ok(result);
                }
            }
            catch
            {
                return Conflict();
            }
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public IActionResult ListP()
        {
            return Ok(_context.Sanphams.ToList());
        }

        [HttpGet("{text}")]
        public ActionResult<Sanpham> SearchP(string text)
        {
            List<Sanpham> sanphams = _context.Sanphams.Where(hd => hd.TenSp.Contains(text)).ToList();
            if (sanphams is null) return NotFound();
            else return Ok(sanphams);
        }

    }
}
