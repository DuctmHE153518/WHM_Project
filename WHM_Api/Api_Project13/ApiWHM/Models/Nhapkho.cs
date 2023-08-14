using System;
using System.Collections.Generic;

namespace ApiWHM.Models;

public partial class Nhapkho
{
    public int MaNhap { get; set; }

    public DateTime? NgayNhap { get; set; }

    public int? MaNv { get; set; }

    public double? TongTien { get; set; }

    public virtual ICollection<Chitietnhapkho> Chitietnhapkhos { get; set; } = new List<Chitietnhapkho>();

    public virtual Nhanvien? MaNvNavigation { get; set; }
}
