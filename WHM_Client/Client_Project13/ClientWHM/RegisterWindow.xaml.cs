using ClientWHM.Models;
using ClientWHM.Request;
using ClientWHM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterRequest registerRequest = new RegisterRequest
            {
                Email = tbEmail.Text,
                Username = tbUsername.Text,
                Password = tbPassword.Password,
                RePassword = tbRePassword.Password
            };

            try
            {
                UserService userService = new UserService();
                bool register = await userService.Register(registerRequest);
                if (register == true)
                {
                    using (var context = new WhmanagementContext())
                    {
                        var user = context.Nhanviens.FirstOrDefault(nv => nv.Username.Equals(tbUsername.Text));
                        if (user != null)
                        {
                            Value.Username = user.Username;
                            Value.Role = user.ChucVu.ToUpper();
                            Value.ShowId = user.MaNv;

                            MessageBox.Show("Dang ky tai khoan thanh cong! Hay bo sung them thong tin");
                            NhanVienWindow form = new NhanVienWindow();
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Dang nhap that bai!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
