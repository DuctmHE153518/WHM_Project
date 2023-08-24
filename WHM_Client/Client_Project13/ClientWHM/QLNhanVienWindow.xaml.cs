using ClientWHM.Models;
using ClientWHM.Request;
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
    /// Interaction logic for QLNhanVienWindow.xaml
    /// </summary>
    public partial class QLNhanVienWindow : Window
    {
        public QLNhanVienWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                UserService userService = new UserService();
                lvNhanVien.ItemsSource = await userService.GetNhanViens();
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

        private async void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserService userService = new UserService();
                Nhanvien nhanvien = new Nhanvien();

                nhanvien.HoTen = tbHoTen.Text;
                nhanvien.NgaySinh = dpNgaySinh.SelectedDate;
                nhanvien.QueQuan = tbQueQuan.Text;
                nhanvien.Sdt = tbSdt.Text;
                nhanvien.Email = tbEmail.Text;
                nhanvien.ChucVu = tbChucVu.Text;
                nhanvien.Luong = double.Parse(tbLuong.Text);
                nhanvien.Username = tbUsername.Text;
                nhanvien.Password = tbPassword.Text;

                await userService.AddNhanVien(nhanvien);
                MessageBox.Show("Them nhan vien thanh cong!");
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
                MessageBoxResult result = MessageBox.Show("Ban chac chan muon xoa nhan vien nay?", "Xac nhan Xoa", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    int id = int.Parse(tbMaNV.Text);
                    UserService userService = new UserService();
                    await userService.DeleteNhanvien(id);
                    MessageBox.Show("Xoa nhan vien thanh cong!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Huy xoa nhan vien!");
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
                UserService userService = new UserService();
                Nhanvien nhanvien = new Nhanvien();

                nhanvien.MaNv = int.Parse(tbMaNV.Text);
                nhanvien.HoTen = tbHoTen.Text;
                nhanvien.NgaySinh = dpNgaySinh.SelectedDate;
                nhanvien.QueQuan = tbQueQuan.Text;
                nhanvien.Sdt = tbSdt.Text;
                nhanvien.Email = tbEmail.Text;
                nhanvien.ChucVu = tbChucVu.Text;
                nhanvien.Luong = double.Parse(tbLuong.Text);
                nhanvien.Username = tbUsername.Text;
                nhanvien.Password = tbPassword.Text;

                await userService.EditNhanVien(nhanvien);
                MessageBox.Show("Cap nhap thong tin nhan vien thanh cong!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    UserService userService = new UserService();
                    lvNhanVien.ItemsSource = await userService.SearchNhanvien(text);
                } 
            }
            catch (Exception ex)
            {
                LoadData();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
