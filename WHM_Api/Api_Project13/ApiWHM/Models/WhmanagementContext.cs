using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiWHM.Models;

public partial class WhmanagementContext : DbContext
{
    public WhmanagementContext()
    {
    }

    public WhmanagementContext(DbContextOptions<WhmanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }

    public virtual DbSet<Chitietnhapkho> Chitietnhapkhos { get; set; }

    public virtual DbSet<Chitietxuatkho> Chitietxuatkhos { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Loaisanpham> Loaisanphams { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Nhapkho> Nhapkhos { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<Xuatkho> Xuatkhos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("WHMConStr"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitiethoadon>(entity =>
        {
            entity.HasKey(e => new { e.MaHd, e.MaSp });

            entity.ToTable("CHITIETHOADON");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHOADON_HOADON");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHOADON_SANPHAM");
        });

        modelBuilder.Entity<Chitietnhapkho>(entity =>
        {
            entity.HasKey(e => new { e.MaNhap, e.MaSp }).HasName("PK_CHITIETNHAPHANG");

            entity.ToTable("CHITIETNHAPKHO");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaNhapNavigation).WithMany(p => p.Chitietnhapkhos)
                .HasForeignKey(d => d.MaNhap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETNHAPHANG_NHAPHANG");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.Chitietnhapkhos)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETNHAPHANG_SANPHAM");
        });

        modelBuilder.Entity<Chitietxuatkho>(entity =>
        {
            entity.HasKey(e => new { e.MaXuat, e.MaSp });

            entity.ToTable("CHITIETXUATKHO");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.Chitietxuatkhos)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETXUATKHO_SANPHAM");

            entity.HasOne(d => d.MaXuatNavigation).WithMany(p => p.Chitietxuatkhos)
                .HasForeignKey(d => d.MaXuat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETXUATKHO_XUATKHO");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.MaHd);

            entity.ToTable("HOADON");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.HoTenKh)
                .HasMaxLength(50)
                .HasColumnName("HoTenKH");
            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayLap).HasColumnType("date");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_HOADON_NHANVIEN");
        });

        modelBuilder.Entity<Loaisanpham>(entity =>
        {
            entity.HasKey(e => e.MaLoaiSp);

            entity.ToTable("LOAISANPHAM");

            entity.Property(e => e.MaLoaiSp).HasColumnName("MaLoaiSP");
            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.ChucVu).HasMaxLength(30);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.QueQuan).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Nhapkho>(entity =>
        {
            entity.HasKey(e => e.MaNhap).HasName("PK_NHAPHANG");

            entity.ToTable("NHAPKHO");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayNhap).HasColumnType("date");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Nhapkhos)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_NHAPHANG_NHANVIEN");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.MaSp);

            entity.ToTable("SANPHAM");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.DonVi).HasMaxLength(20);
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaLoaiSp).HasColumnName("MaLoaiSP");
            entity.Property(e => e.MoTa).HasMaxLength(50);
            entity.Property(e => e.SltonKho).HasColumnName("SLTonKho");
            entity.Property(e => e.TenSp)
                .HasMaxLength(50)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaLoaiSpNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.MaLoaiSp)
                .HasConstraintName("FK_SANPHAM_LOAISANPHAM");
        });

        modelBuilder.Entity<Xuatkho>(entity =>
        {
            entity.HasKey(e => e.MaXuat);

            entity.ToTable("XUATKHO");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayXuat).HasColumnType("date");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Xuatkhos)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_XUATKHO_NHANVIEN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
