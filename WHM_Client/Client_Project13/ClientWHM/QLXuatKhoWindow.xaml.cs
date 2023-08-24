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
    /// Interaction logic for QLXuatKhoWindow.xaml
    /// </summary>
    public partial class QLXuatKhoWindow : Window
    {
        public WhmanagementContext db = new WhmanagementContext();
        public List<Chitietnhapkho> GioHang { get; set; }
        public double TongTien { get; set; }
        public QLXuatKhoWindow()
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
                GioHang = new List<Chitietnhapkho>();
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

        private void btnXuatHang_Click(object sender, RoutedEventArgs e)
        {
            var sc = new LapPhieuNhapKhoWindow(TongTien, GioHang);
            sc.ShowDialog();
            LoadData();
        }

        private async void tbTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string text = tbTimKiem.Text;
                if (text == "")
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

        private void btnThemDSXuat_Click(object sender, RoutedEventArgs e)
        {
            var selectedSP = lvSanPham.SelectedItem as Sanpham;
            if (selectedSP == null)
                return;
            else if (string.IsNullOrWhiteSpace(tbGiaXuat.Text) || string.IsNullOrWhiteSpace(tbSLXuat.Text))
                MessageBox.Show("Chua nhap du thong tin!");
            else if (IsNumber(tbGiaXuat.Text) == false || IsNumber(tbSLXuat.Text) == false)
                MessageBox.Show("Gia xuat va so luong xuat phai duoc dien!");
            else
            {
                var check = GioHang.Where(p => p.MaSp == selectedSP.MaSp).Count();
                if (check > 0)
                {
                    var update = GioHang.Where(p => p.MaSp == selectedSP.MaSp).SingleOrDefault();
                    update.SoLuong += int.Parse(tbSLXuat.Text);
                    lvSanPhamNhap.ItemsSource = GioHang.ToList();
                }
                else
                {
                    var newNhap = new Chitietnhapkho()
                    {
                        MaSp = selectedSP.MaSp,
                        SoLuong = int.Parse(tbSLXuat.Text),
                        GiaNhap = float.Parse(tbGiaXuat.Text)
                    };
                    GioHang.Add(newNhap);
                    lvSanPhamNhap.ItemsSource = GioHang.ToList();
                }
                TinhTongTien();
            }   
        }

        private bool IsNumber(string a)
        {
            return Regex.IsMatch(a, @"\d");
        }

        void TinhTongTien()
        {
            TongTien = 0;
            foreach (Chitietnhapkho n in GioHang)
            {
                TongTien += double.Parse(n.SoLuong.ToString()) * double.Parse(n.GiaNhap.ToString());
            }
            tbTongTien.Text = TongTien.ToString();
        }
    }
}
