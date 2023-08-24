using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWHM.Request
{
    public class Product
    {
        public int MaSp { get; set; }

        public string TenSp { get; set; } = null!;

        public string? DonVi { get; set; }

        public string? MoTa { get; set; }

        public int? MaLoaiSp { get; set; }
        public string TenLoai { get; set; } = null!;

        public double? GiaBan { get; set; }

        public int? SltonKho { get; set; }

        public string? HinhAnh { get; set; }
    }
}
