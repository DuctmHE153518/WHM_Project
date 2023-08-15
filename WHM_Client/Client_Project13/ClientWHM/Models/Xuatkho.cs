using System;
using System.Collections.Generic;

namespace ClientWHM.Models;

public partial class Xuatkho
{
    public int MaXuat { get; set; }

    public DateTime? NgayXuat { get; set; }

    public int? MaNv { get; set; }

    public double? TongTien { get; set; }

    public virtual Nhanvien? MaNvNavigation { get; set; }
}
