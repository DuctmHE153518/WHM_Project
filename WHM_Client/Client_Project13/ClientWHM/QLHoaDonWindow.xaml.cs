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
    /// Interaction logic for QLHoaDonWindow.xaml
    /// </summary>
    public partial class QLHoaDonWindow : Window
    {
        public QLHoaDonWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                BillService billService = new BillService();
                lvHoaDon.ItemsSource = await billService.GetHoaDons();
                lvChiTietHoaDon.ItemsSource = null;
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
                    BillService billService = new BillService();
                    lvHoaDon.ItemsSource = await billService.SearchHoaDon(text);
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
            var sc = new ThemHoaDonWindow();
            sc.ShowDialog();
            LoadData();
        }

        private async void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Ban chac chan muon xoa hoa don nay?", "Xac nhan Xoa", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    int id = int.Parse(tbMaHD.Text);
                    BillService billService = new BillService();
                    await billService.DeleteHoaDon(id);
                    MessageBox.Show("Xoa hoa don thanh cong!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Huy xoa hoa don!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void lvHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BillService billService = new BillService();
                var getId = lvHoaDon.SelectedItem as Hoadon;
                if (getId == null) return;
                lvChiTietHoaDon.ItemsSource = await billService.GetChiTietHoaDon(getId.MaHd);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
