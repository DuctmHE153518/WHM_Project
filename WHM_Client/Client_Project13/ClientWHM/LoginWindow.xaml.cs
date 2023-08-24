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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginRequest loginRequest = new LoginRequest
            {
                Username = tbUsername.Text,
                Password = tbPassword.Password
            };

            try
            {
                UserService userService = new UserService();
                bool login = await userService.Login(loginRequest);
                if (login == true)
                {
                    using (var context = new WhmanagementContext())
                    {
                        var user = context.Nhanviens.FirstOrDefault(nv => nv.Username.Equals(tbUsername.Text));
                        if(user != null)
                        {
                            Value.Username = user.Username;
                            Value.Role = user.ChucVu;
                            Value.ShowId = user.MaNv;
                            if (user.ChucVu.Equals("Staff"))
                            {
                                MessageBox.Show("Dang nhap thanh cong! Chuc vu: Nhan Vien");
                                MainWindow form = new MainWindow();
                                form.Show();
                                this.Hide();
                            }
                            else if (user.ChucVu.Equals("Admin"))// no dang la nguoi thue
                            {
                                MessageBox.Show("Dang nhap thanh cong! Chuc vu: Quan ly");
                                MainWindow form = new MainWindow();
                                form.Show();
                                this.Hide();
                            }
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
