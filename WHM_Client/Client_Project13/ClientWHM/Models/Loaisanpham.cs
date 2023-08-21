using System;
using System.Collections.Generic;

namespace ClientWHM.Models;

public partial class Loaisanpham
{
    public int MaLoaiSp { get; set; }

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
