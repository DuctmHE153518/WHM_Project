using ClientWHM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWHM.Services
{
    internal class ThongKeService : BaseService
    {
        public async Task<List<Hoadon>> ThongKe(DateTime dpTuNgay, DateTime dpDenNgay)
        {
            List<Hoadon> result = await Filter<List<Hoadon>>(dpTuNgay, dpDenNgay);
            return result;
        }

        public async Task<double> GetDoanhThu(DateTime from, DateTime to)
        {
            double loaisanphams = await GetData<double>($"bill/list/{from}/{to}");
            return loaisanphams;
        }
    }
}
