using ClientWHM.Models;
using ClientWHM.Services;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for QLLoaiSanPhamWindow.xaml
    /// </summary>
    public partial class QLLoaiSanPhamWindow : Window
    {
        public QLLoaiSanPhamWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                BatchService batchService = new BatchService();
                lvLoaiSanPham.ItemsSource = await batchService.GetLoaiSanPhams();
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
                    BatchService batchService = new BatchService();
                    lvLoaiSanPham.ItemsSource = await batchService.SearchLoaiSanPham(text);
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
                BatchService batchService = new BatchService();
                Loaisanpham loaisanpham = new Loaisanpham();

                loaisanpham.TenLoai = tbTenLoai.Text;

                await batchService.AddLoaiSanPham(loaisanpham);
                MessageBox.Show("Them loai san pham thanh cong!");
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
                MessageBoxResult result = MessageBox.Show("Ban chac chan muon xoa loai san pham nay?", "Xac nhan Xoa", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    int id = int.Parse(tbMaLoai.Text);
                    BatchService batchService = new BatchService();
                    await batchService.DeleteLoaiSanPham(id);
                    MessageBox.Show("Xoa loai san pham thanh cong!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Huy xoa loai san pham!");
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
                BatchService batchService = new BatchService();
                Loaisanpham loaisanpham = new Loaisanpham();

                loaisanpham.MaLoaiSp = int.Parse(tbMaLoai.Text);
                loaisanpham.TenLoai = tbTenLoai.Text;

                await batchService.EditLoaiSanPham(loaisanpham);
                MessageBox.Show("Cap nhap loai san pham thanh cong!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
