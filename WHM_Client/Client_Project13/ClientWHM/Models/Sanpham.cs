using System;
using System.Collections.Generic;

namespace ClientWHM.Models;

public partial class Sanpham
{
    public int MaSp { get; set; }

    public string TenSp { get; set; } = null!;

    public string? DonVi { get; set; }

    public string? MoTa { get; set; }

    public int? MaLoaiSp { get; set; }

    public double? GiaBan { get; set; }

    public int? SltonKho { get; set; }

    public string? HinhAnh { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual ICollection<Chitietnhapkho> Chitietnhapkhos { get; set; } = new List<Chitietnhapkho>();

    public virtual Loaisanpham? MaLoaiSpNavigation { get; set; }
}
