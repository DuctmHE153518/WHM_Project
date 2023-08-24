using ClientWHM.Models;
using ClientWHM.Services;
using Microsoft.Win32;
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
    /// Interaction logic for QLSanPhamWindow.xaml
    /// </summary>
    public partial class QLSanPhamWindow : Window
    {
        public QLSanPhamWindow()
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

        private async void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var selectedLSP = cbLoaiSP.SelectedItem as Loaisanpham;
                ProductService productService = new ProductService();
                Sanpham sanpham = new Sanpham();

                sanpham.TenSp = tbTenSP.Text;
                sanpham.DonVi = tbDonVi.Text;
                sanpham.MoTa = tbMoTa.Text;
               // sanpham.MaLoaiSp = selectedLSP.MaLoaiSp;
                sanpham.MaLoaiSp = int.Parse(tbLoaiSp.Text);
                sanpham.GiaBan = double.Parse(tbGiaBan.Text);
                sanpham.SltonKho = int.Parse(tbSLTonKho.Text);
                sanpham.HinhAnh = tbHinhAnh.Text;

                await productService.AddSanPham(sanpham);
                MessageBox.Show("Them san pham thanh cong!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Ban chac chan muon xoa san pham nay?", "Xac nhan Xoa", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    int id = int.Parse(tbMaSP.Text);
                    ProductService productService = new ProductService();
                    await productService.DeleteSanPham(id);
                    MessageBox.Show("Xoa san pham thanh cong!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Huy xoa san pham!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var selectedLSP = cbLoaiSP.SelectedItem as Loaisanpham;
                ProductService productService = new ProductService();
                Sanpham sanpham = new Sanpham();

                sanpham.MaSp = int.Parse(tbMaSP.Text);
                sanpham.TenSp = tbTenSP.Text;
                sanpham.DonVi = tbDonVi.Text;
                sanpham.MoTa = tbMoTa.Text;
                //sanpham.MaLoaiSp = selectedLSP.MaLoaiSp;
                sanpham.MaLoaiSp = int.Parse(tbLoaiSp.Text);
                sanpham.GiaBan = double.Parse(tbGiaBan.Text);
                sanpham.SltonKho = int.Parse(tbSLTonKho.Text);
                sanpham.HinhAnh = tbHinhAnh.Text;

                await productService.EditSanPham(sanpham);
                MessageBox.Show("Cap nhap san pham thanh cong!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sc = new OpenFileDialog();
            sc.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (sc.ShowDialog() == true)
            {
                tbHinhAnh.Text = sc.SafeFileName;
                HinhAnh.Source = new BitmapImage(new Uri(sc.FileName));
            }
        }
    }
}
