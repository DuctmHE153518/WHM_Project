using ClientWHM.Models;
using ClientWHM.Request;
using ClientWHM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LapHoaDonWindow.xaml
    /// </summary>
    public partial class LapHoaDonWindow : Window
    {
        public WhmanagementContext _context = new WhmanagementContext();
        public List<Chitiethoadon> GioHang { get; set; }
        public double TongTien { get; set; }
        public LapHoaDonWindow(double TT, List<Chitiethoadon> giohang)
        {
            InitializeComponent();
            TongTien = TT;
            GioHang = new List<Chitiethoadon>(giohang);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                tbNhanVien.Text = Value.Username;
                tbNgayLap.Text = DateTime.Now.ToString();
                tbTongTien.Text = TongTien.ToString();
                tbHoTenKH.Text = "";
                tbDiaChi.Text = "";
                tbSDT.Text = "";
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

        private void btnLapHoaDon_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbHoTenKH.Text) || string.IsNullOrWhiteSpace(tbDiaChi.Text) || string.IsNullOrWhiteSpace(tbSDT.Text))
                MessageBox.Show("Chưa nhập đủ thông tin !!!");
            else if (IsPhone(tbSDT.Text) == false)
                MessageBox.Show("Chưa đúng định dạng của SĐT !!!");
            else
            {
                var newHD = new Hoadon()
                {
                    NgayLap = DateTime.Parse(tbNgayLap.Text),
                    MaNv = Value.ShowId,
                    TongTien = TongTien,
                    HoTenKh = tbHoTenKH.Text,
                    DiaChi = tbDiaChi.Text,
                    Sdt = tbSDT.Text
                };
                _context.Hoadons.Add(newHD);
                _context.SaveChanges();
                foreach (Chitiethoadon ct in GioHang)
                {
                    var selectedSp = _context.Sanphams.ToList().Where(p => p.MaSp == ct.MaSp).SingleOrDefault();
                    selectedSp.SltonKho -= ct.SoLuong;
                    var newCT = new Chitiethoadon()
                    {
                        MaHd = newHD.MaHd,
                        MaSp = ct.MaSp,
                        SoLuong = ct.SoLuong
                    };
                    _context.Chitiethoadons.Add(newCT);
                }
                _context.SaveChanges();
                MessageBox.Show("Lập phiếu thành công");
                this.Close();
            }
        }
        bool IsPhone(string CardNumber)
        {
            return Regex.IsMatch(CardNumber, @"^[0-9]{10}$");
        }
    }
}
