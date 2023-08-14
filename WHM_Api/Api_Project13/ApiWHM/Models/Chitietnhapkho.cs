using System;
using System.Collections.Generic;

namespace ApiWHM.Models;

public partial class Chitietnhapkho
{
    public int MaNhap { get; set; }

    public int MaSp { get; set; }

    public int? SoLuong { get; set; }

    public double? GiaNhap { get; set; }

    public virtual Nhapkho MaNhapNavigation { get; set; } = null!;

    public virtual Sanpham MaSpNavigation { get; set; } = null!;
}
