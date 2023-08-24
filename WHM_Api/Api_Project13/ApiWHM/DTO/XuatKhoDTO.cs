using ApiWHM.Models;

namespace ApiWHM.DTO
{
    public class XuatKhoDTO
    {
        public int MaXuat { get; set; }

        public DateTime? NgayXuat { get; set; }

        public int? MaNv { get; set; }

        public double? TongTien { get; set; }

        public virtual List<Chitietxuatkho>? Chitietxuatkhos { get; set; } = new List<Chitietxuatkho>();

        public virtual Nhanvien? MaNvNavigation { get; set; }
    }
}
