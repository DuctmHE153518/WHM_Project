﻿using ApiWHM.DTO;
using ApiWHM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class XuatKhoController : ControllerBase
    {
        private WhmanagementContext _context = new WhmanagementContext();
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(_context.Xuatkhos.Include("MaNvNavigation").Select(x => new
                {
                    x.MaXuat,
                    x.NgayXuat,
                    x.MaNvNavigation,
                    x.TongTien,
                    

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
                var xuatkho = _context.Xuatkhos.Include("MaNvNavigation").Where(x => x.MaXuat == id).Select(x => new
                {
                    x.MaXuat,
                    x.NgayXuat,
                    x.MaNvNavigation,
                    x.TongTien
                }).FirstOrDefault();
                XuatKhoDTO dto = new XuatKhoDTO();
                dto.MaXuat = xuatkho.MaXuat;
                dto.NgayXuat = xuatkho.NgayXuat;
                dto.MaNvNavigation = xuatkho.MaNvNavigation;
                dto.TongTien = xuatkho.TongTien;    
                dto.Chitietxuatkhos = _context.Chitietxuatkhos.Where(a => a.MaXuat == xuatkho.MaXuat).ToList();
                return Ok(dto);
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
        [HttpPut]
        [EnableQuery]
        public IActionResult Update(Xuatkho model)
        {
            try
            {
                Xuatkho a = _context.Xuatkhos.FirstOrDefault(a => a.MaXuat == model.MaXuat);
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
                Xuatkho a = _context.Xuatkhos.FirstOrDefault(a => a.MaXuat == id);
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
