using System;
using System.Collections.Generic;

namespace ApiWHM.Models;

public partial class Nhanvien
{
    public int MaNv { get; set; }

    public string? HoTen { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? QueQuan { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? ChucVu { get; set; }

    public double? Luong { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual ICollection<Nhapkho> Nhapkhos { get; set; } = new List<Nhapkho>();

    public virtual ICollection<Xuatkho> Xuatkhos { get; set; } = new List<Xuatkho>();
}
