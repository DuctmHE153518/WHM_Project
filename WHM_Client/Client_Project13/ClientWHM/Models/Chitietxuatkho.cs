using System;
using System.Collections.Generic;

namespace ClientWHM.Models;

public partial class Chitietxuatkho
{
    public int MaXuat { get; set; }

    public int MaSp { get; set; }

    public int? SoLuong { get; set; }

    public double? GiaNhap { get; set; }

    public virtual Sanpham MaSpNavigation { get; set; } = null!;

    public virtual Xuatkho MaXuatNavigation { get; set; } = null!;
}
