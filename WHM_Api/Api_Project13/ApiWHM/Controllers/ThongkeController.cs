using ApiWHM.Models;
using ApiWHM.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWHM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongkeController : ControllerBase
    {
        /*public ActionResult<Hoadon> Get(int id)
        {
            topSanPhams = new List<TopSanPham>();
            SanPhamBanChayNhat();
            var topsps = topSanPhams.ToList().OrderBy(p => p.SoLuong).ToList();
            var sps = new List<Sanpham>();
            foreach (Sanpham sp in db.Sanphams.ToList())
            {
                foreach (TopSanPham tsp in topsps)
                {
                    if (sp.MaSp == tsp.MaSp)
                    {
                        sps.Add(sp);
                    }
                }
            }
            lvSanPham.ItemsSource = sps.Take(int).ToList();
            List<string> Labels = new List<string>();
            List<double> SoLuongs = new List<double>();
            foreach (Sanpham sp in sps)
            {
                Labels.Add(sp.TenSp);
            }
            foreach (TopSanPham tsp in topsps)
            {
                SoLuongs.Add(tsp.SoLuong);
            }
            ccSanpham.Labels = Labels;
            SeriesCollection series = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Revenue",
                    Values = new ChartValues<double>(SoLuongs.ToList())
                },
            };
            ccThongKe.Series = series;
        }

        private void SanPhamBanChayNhat()
        {
            var chitiethoadons = db.Chitiethoadons.ToList();
            foreach (Chitiethoadon ct in chitiethoadons)
            {
                var check = topSanPhams.ToList().Where(p => p.MaSp == ct.MaSp).SingleOrDefault();
                if (check == null)
                {
                    var newTopSP = new TopSanPham()
                    {
                        MaSp = ct.MaSp,
                        SoLuong = int.Parse(ct.SoLuong.ToString())
                    };
                    topSanPhams.Add(newTopSP);
                }
                else
                {
                    check.SoLuong += int.Parse(ct.SoLuong.ToString());
                }
            }
        }*/
    }
}
