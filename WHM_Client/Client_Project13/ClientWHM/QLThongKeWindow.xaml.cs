using ClientWHM.Models;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientWHM.Request;
using ClientWHM.Services;

namespace ClientWHM
{
    /// <summary>
    /// Interaction logic for QLThongKeWindow.xaml
    /// </summary>
    public partial class QLThongKeWindow : Window
    {
        public WhmanagementContext db = new WhmanagementContext();
        public double DoanhThu { get; set; }
        public List<TopSanPham> topSanPhams;

        public QLThongKeWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {

            BillService billService = new BillService();
            lvHoaDon.ItemsSource = await billService.GetHoaDons();
            dpTuNgay.SelectedDate = DateTime.Now;
            dpDenNgay.SelectedDate = DateTime.Now;
            DoanhThu = 0;
        }

        private void btnTroVe_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            /*DoanhThu = 0;
            var hoadons = db.Hoadons.ToList().Where(p => p.NgayLap >= dpTuNgay.SelectedDate && p.NgayLap <= dpDenNgay.SelectedDate).ToList();
            foreach (Hoadon hd in hoadons)
            {
                DoanhThu += double.Parse(hd.TongTien.ToString());
            }
            tbDoanhThu.Text = DoanhThu.ToString();
            lvHoaDon.ItemsSource = hoadons.ToList();*/


            DateTime from = dpTuNgay.SelectedDate.GetValueOrDefault();
            DateTime to = dpDenNgay.SelectedDate.GetValueOrDefault();

            ThongKeService thongKeService = new ThongKeService();
            var hoadons = await thongKeService.ThongKe(from, to);
            foreach (Hoadon hd in hoadons)
            {
                DoanhThu += double.Parse(hd.TongTien.ToString());
            }
            tbDoanhThu.Text = DoanhThu.ToString();
            lvHoaDon.ItemsSource = hoadons;
        }

        private void btnXem_Click(object sender, RoutedEventArgs e)
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
            lvSanPham.ItemsSource = sps.Take(int.Parse(tbtop.Text)).ToList();
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
        }
    }
}
