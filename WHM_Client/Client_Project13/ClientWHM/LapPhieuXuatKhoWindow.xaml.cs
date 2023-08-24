using ClientWHM.Models;
using ClientWHM.Request;
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

namespace ClientWHM
{
    /// <summary>
    /// Interaction logic for LapPhieuXuatKhoWindow.xaml
    /// </summary>
    public partial class LapPhieuXuatKhoWindow : Window
    {
        public WhmanagementContext db = new WhmanagementContext();
        public List<Chitietxuatkho> GioHang { get; set; }
        public double TongTien { get; set; }
        public LapPhieuXuatKhoWindow(double TT, List<Chitietxuatkho> giohang)
        {
            InitializeComponent();
            TongTien = TT;
            GioHang = new List<Chitietxuatkho>(giohang);
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                tbNgayNhap.Text = DateTime.Now.ToString();
                tbHoTen.Text = Value.Username;
                tbTongTien.Text = TongTien.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLapPhieu_Click(object sender, RoutedEventArgs e)
        {
            var newNH = new Nhapkho()
            {
                NgayNhap = DateTime.Parse(tbNgayNhap.Text),
                MaNv = Value.ShowId,
                TongTien = TongTien
            };
            db.Nhapkhos.Add(newNH);
            db.SaveChanges();

            foreach (Chitietxuatkho ct in GioHang)
            {
                var selectedSp = db.Sanphams.ToList().Where(p => p.MaSp == ct.MaSp).SingleOrDefault();
                selectedSp.SltonKho += ct.SoLuong;
                var newCT = new Chitietxuatkho()
                {
                    MaXuat = newNH.MaNhap,
                    MaSp = ct.MaSp,
                    SoLuong = ct.SoLuong,
                    GiaNhap = ct.GiaNhap
                };
                db.Chitietxuatkhos.Add(newCT);
            }
            db.SaveChanges();
            MessageBox.Show("Lập phiếu thành công");
            this.Close();
        }
    }
}
