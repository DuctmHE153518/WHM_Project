using System;
using System.Collections.Generic;

namespace ClientWHM.Models;

public partial class Hoadon
{
    public int MaHd { get; set; }

    public int? MaNv { get; set; }

    public string? HoTenKh { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public DateTime? NgayLap { get; set; }

    public double? TongTien { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Nhanvien? MaNvNavigation { get; set; }
}
