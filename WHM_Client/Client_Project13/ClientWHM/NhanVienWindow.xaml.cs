using ClientWHM.Models;
using ClientWHM.Request;
using ClientWHM.Services;
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
    /// Interaction logic for NhanVienWindow.xaml
    /// </summary>
    public partial class NhanVienWindow : Window
    {
        public NhanVienWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                UserService userService = new UserService();
                Nhanvien nhanvien = await userService.GetNhanvien(Value.ShowId);

                tbMaNV.Text = nhanvien.MaNv.ToString();
                tbHoTen.Text = nhanvien.HoTen;
                dpNgaySinh.SelectedDate = nhanvien.NgaySinh;
                tbQueQuan.Text = nhanvien.QueQuan;
                tbSdt.Text = nhanvien.Sdt;
                tbEmail.Text = nhanvien.Email;
                tbChucVu.Text = nhanvien.ChucVu;
                tbLuong.Text = nhanvien.Luong.ToString();
                tbUsername.Text = nhanvien.Username;
                pbPassword.Password = nhanvien.Password;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnLuuThongTin_Click(object sender, RoutedEventArgs e)
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
                nhanvien.Password = pbPassword.Password;

                await userService.EditNhanVien(nhanvien);
                MessageBox.Show("Nhap thong tin thanh cong! Lam viec thoi nao");
                MainWindow form = new MainWindow();
                form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
