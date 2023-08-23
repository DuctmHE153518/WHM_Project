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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWHM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbShowUsername.Text = Value.Username;
        }

        private void btnSanPham_Click(object sender, RoutedEventArgs e)
        {
            var sc = new QLSanPhamWindow();
            sc.ShowDialog();
        }

        private void btnLoSanPham_Click(object sender, RoutedEventArgs e)
        {
            var sc = new QLLoaiSanPhamWindow();
            sc.ShowDialog();
        }

        private void btnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            if(Value.Role == "Admin")
            {
                var sc = new QLNhanVienWindow();
                sc.ShowDialog();
            }
            else
            {
                NhanVienWindow nhanVienWindow = new NhanVienWindow();
                nhanVienWindow.Show();
            }
        }

        private void btnNhapKho_Click(object sender, RoutedEventArgs e)
        {
            var sc = new QLNhapKhoWindow();
            sc.ShowDialog();
        }

        private void btnXuatKho_Click(object sender, RoutedEventArgs e)
        {
            var sc = new QLXuatKhoWindow();
            sc.ShowDialog();
        }

        private void btnHoaDon_Click(object sender, RoutedEventArgs e)
        {
            var sc = new QLHoaDonWindow();
            sc.ShowDialog();
        }

        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            var sc = new QLThongKeWindow();
            sc.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
