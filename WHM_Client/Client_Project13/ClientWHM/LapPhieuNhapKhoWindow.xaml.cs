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
    /// Interaction logic for LapPhieuNhapKhoWindow.xaml
    /// </summary>
    public partial class LapPhieuNhapKhoWindow : Window
    {
        public LapPhieuNhapKhoWindow()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
