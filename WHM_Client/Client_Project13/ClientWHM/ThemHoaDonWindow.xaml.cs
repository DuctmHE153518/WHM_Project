using ClientWHM.Models;
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
    /// Interaction logic for ThemHoaDonWindow.xaml
    /// </summary>
    public partial class ThemHoaDonWindow : Window
    {
        public WhmanagementContext _context = new WhmanagementContext();
        public List<Chitiethoadon> GioHang { get; set; }
        public double TongTien { get; set; }

        public ThemHoaDonWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                ProductService productService = new ProductService();
                lvSanPham.ItemsSource = await productService.GetSanPhams();

                GioHang = new List<Chitiethoadon>();
                TongTien = 0;
                tbTongTien.Text = TongTien.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTroVe_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void tbTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string text = tbTimKiem.Text;
                if (text == null)
                {
                    LoadData();
                }
                else
                {
                    ProductService productService = new ProductService();
                    lvSanPham.ItemsSource = await productService.SearchSanPham(text);
                }
            }
            catch (Exception ex)
            {
                LoadData();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemGioHang_Click(object sender, RoutedEventArgs e)
        {
            var selectedSP = lvSanPham.SelectedItem as Sanpham;
            if (selectedSP == null)
                return;
            else if (string.IsNullOrWhiteSpace(tbSoLuong.Text))
                MessageBox.Show("Chưa nhập đủ thông tin !!!");
            else if (IsNumber(tbSoLuong.Text) == false)
                MessageBox.Show("Số lượng nhập phải là một số !!!");
            else
            {
                var check = GioHang.Where(p => p.MaSp == selectedSP.MaSp).Count();
                if (check > 0)
                {
                    var update = GioHang.Where(p => p.MaSp == selectedSP.MaSp).SingleOrDefault();
                    update.SoLuong += int.Parse(tbSoLuong.Text);
                    if (selectedSP.SltonKho < update.SoLuong)
                        MessageBox.Show("Không đủ sản phẩm !!!");
                    else
                        lvGioHang.ItemsSource = GioHang.ToList();
                }
                else
                {
                    var newNhap = new Chitiethoadon()
                    {
                        MaSp = selectedSP.MaSp,
                        SoLuong = int.Parse(tbSoLuong.Text)
                    };
                    GioHang.Add(newNhap);
                    lvGioHang.ItemsSource = GioHang.ToList();
                }
                TinhTongTien();
            }
        }

        private void btnLapHoaDon_Click(object sender, RoutedEventArgs e)
        {
            var sc = new LapHoaDonWindow(TongTien, GioHang);
            sc.ShowDialog();
            LoadData();
        }

        private bool IsNumber(string a)
        {
            return Regex.IsMatch(a, @"\d");
        }

        void TinhTongTien()
        {
            TongTien = 0;
            foreach (Chitiethoadon n in GioHang)
            {
                var SP = _context.Sanphams.ToList().Where(p => p.MaSp == n.MaSp).SingleOrDefault();
                TongTien += double.Parse(n.SoLuong.ToString()) * double.Parse(SP.GiaBan.ToString());
            }
            tbTongTien.Text = TongTien.ToString();
        }
    }
}
