using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLW_BDT.Models;

public partial class BtlLtwQlbdtContext : DbContext
{
    public BtlLtwQlbdtContext()
    {
    }

    public BtlLtwQlbdtContext(DbContextOptions<BtlLtwQlbdtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChiTietHoaDonBan> ChiTietHoaDonBans { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<Hang> Hangs { get; set; }

    public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LichSuHoatDong> LichSuHoatDongs { get; set; }

    public virtual DbSet<MauSac> MauSacs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Rom> Roms { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TinNhan> TinNhans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CE2QC2S\\MAY1;Initial Catalog=BTL_LTW_QLBDT;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhSanPham>(entity =>
        {
            entity.HasKey(e => e.TenFile);

            entity.ToTable("AnhSanPham");

            entity.Property(e => e.TenFile).HasMaxLength(255);
            entity.Property(e => e.MaMau).HasMaxLength(255);
            entity.Property(e => e.MaSanPham).HasMaxLength(50);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.AnhSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AnhSanPham_SanPham");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietGioHang);

            entity.ToTable("ChiTietGioHang", tb => tb.HasTrigger("theoDoiCustomerGioHang"));

            entity.Property(e => e.MaChiTietGioHang).HasMaxLength(50);
            entity.Property(e => e.MaGioHang).HasMaxLength(50);
            entity.Property(e => e.MaSanPham).HasMaxLength(50);
            entity.Property(e => e.ThongSoMau).HasMaxLength(50);
            entity.Property(e => e.ThongSoRom).HasMaxLength(50);

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK_ChiTietGioHang_GioHang");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK_ChiTietGioHang_SanPham");
        });

        modelBuilder.Entity<ChiTietHoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaChiTietHoaDonBan);

            entity.ToTable("ChiTietHoaDonBan");

            entity.Property(e => e.MaChiTietHoaDonBan).HasMaxLength(50);
            entity.Property(e => e.DonGiaCuoi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaHoaDon).HasMaxLength(50);
            entity.Property(e => e.MaSanPham).HasMaxLength(50);

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDonBans)
                .HasForeignKey(d => d.MaHoaDon)
                .HasConstraintName("FK_ChiTietHoaDonBan_HoaDonBan");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietHoaDonBans)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK_ChiTietHoaDonBan_SanPham");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => new { e.MaSanPham, e.TenDangNhap });

            entity.ToTable(tb => tb.HasTrigger("theoDoiCustomerDanhGia"));

            entity.Property(e => e.MaSanPham).HasMaxLength(50);
            entity.Property(e => e.TenDangNhap).HasMaxLength(100);
            entity.Property(e => e.NoiDung).HasMaxLength(255);
            entity.Property(e => e.ThoiGianDanhGia).HasColumnType("datetime");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_DanhGia_TaiKhoan");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang);

            entity.ToTable("GioHang");

            entity.Property(e => e.MaGioHang).HasMaxLength(50);
            entity.Property(e => e.TenDangNhap).HasMaxLength(100);
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.TenDangNhap)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_GioHang_TaiKhoan");
        });

        modelBuilder.Entity<Hang>(entity =>
        {
            entity.HasKey(e => e.MaHang).HasName("PK__Hang__19C0DB1D0661D8B7");

            entity.ToTable("Hang");

            entity.Property(e => e.MaHang).HasMaxLength(50);
            entity.Property(e => e.TenHang).HasMaxLength(50);
        });

        modelBuilder.Entity<HoaDonBan>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDonBa__835ED13B45446227");

            entity.ToTable("HoaDonBan", tb => tb.HasTrigger("theoDoiCustomerMuaSanPham"));

            entity.Property(e => e.MaHoaDon).HasMaxLength(50);
            entity.Property(e => e.DiaChiGiaoHang).HasMaxLength(255);
            entity.Property(e => e.DiaChiLatitude)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("DiaChi_Latitude");
            entity.Property(e => e.DiaChiLongtitude)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("DiaChi_Longtitude");
            entity.Property(e => e.GhiChuHd)
                .HasMaxLength(255)
                .HasColumnName("GhiChuHD");
            entity.Property(e => e.KhuyenMai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaKhachHang).HasMaxLength(50);
            entity.Property(e => e.MaNhanVien).HasMaxLength(50);
            entity.Property(e => e.PhiGiaoHang).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(100);
            entity.Property(e => e.ThoiGianLap).HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(30);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK_HoaDonBan_KhachHang");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK_HoaDonBan_NhanVien");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E544EEA62B");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang).HasMaxLength(50);
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.DiaChiLatitude)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("DiaChi_Latitude");
            entity.Property(e => e.DiaChiLongitude)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("DiaChi_Longitude");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.LoaiKhachHang).HasMaxLength(100);
            entity.Property(e => e.ResetCodeExpiry).HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(100);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_KhachHang_TaiKhoan");
        });

        modelBuilder.Entity<LichSuHoatDong>(entity =>
        {
            entity.HasKey(e => e.MaHoatDong).HasName("PK__LichSuHo__BD808BE7610FE3DE");

            entity.ToTable("LichSuHoatDong");

            entity.Property(e => e.MaHoatDong).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.LoaiHoatDong).HasMaxLength(100);
            entity.Property(e => e.TenDangNhap).HasMaxLength(100);
            entity.Property(e => e.ThoiGianThucHien).HasColumnType("datetime");

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.LichSuHoatDongs)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_LichSuHoatDong_TaiKhoan");
        });

        modelBuilder.Entity<MauSac>(entity =>
        {
            entity.HasKey(e => e.MaMau);

            entity.ToTable("MauSac");

            entity.Property(e => e.MaMau).HasMaxLength(255);
            entity.Property(e => e.MaSanPham).HasMaxLength(50);
            entity.Property(e => e.TenMau).HasMaxLength(50);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.MauSacs)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MauSac_SanPham");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA474AF22D55");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien).HasMaxLength(50);
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.ChucVu).HasMaxLength(100);
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenDangNhap).HasMaxLength(100);
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);

            entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.TenDangNhap)
                .HasConstraintName("FK_NhanVien_TaiKhoan");
        });

        modelBuilder.Entity<Rom>(entity =>
        {
            entity.HasKey(e => e.MaRom);

            entity.ToTable("ROM");

            entity.Property(e => e.MaRom)
                .HasMaxLength(255)
                .HasColumnName("MaROM");
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaSanPham).HasMaxLength(50);
            entity.Property(e => e.ThongSo).HasMaxLength(50);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.Roms)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ROM_SanPham");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham);

            entity.ToTable("SanPham", tb => tb.HasTrigger("theoDoiAdminSanPham"));

            entity.Property(e => e.MaSanPham).HasMaxLength(50);
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.BaoMatNangCao).HasMaxLength(255);
            entity.Property(e => e.Camera).HasMaxLength(50);
            entity.Property(e => e.Chip).HasMaxLength(50);
            entity.Property(e => e.CongNgheManHinh).HasMaxLength(255);
            entity.Property(e => e.DanhBa).HasMaxLength(255);
            entity.Property(e => e.DenFlash).HasMaxLength(255);
            entity.Property(e => e.DonGiaBanGoc).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonGiaBanRa).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GhiAmMacDinh).HasMaxLength(255);
            entity.Property(e => e.JackTaiNghe).HasMaxLength(255);
            entity.Property(e => e.KhuyenMai).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KichThuoc).HasMaxLength(100);
            entity.Property(e => e.LoaiPin).HasMaxLength(255);
            entity.Property(e => e.MaHang).HasMaxLength(50);
            entity.Property(e => e.ManHinh).HasMaxLength(100);
            entity.Property(e => e.MangDiDong).HasMaxLength(255);
            entity.Property(e => e.Pin).HasMaxLength(50);
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .HasColumnName("RAM");
            entity.Property(e => e.Sim).HasMaxLength(255);
            entity.Property(e => e.TenSanPham).HasMaxLength(100);

            entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaHang)
                .HasConstraintName("FK_SanPham_Hang");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TenDangNhap).HasName("PK__TaiKhoan__55F68FC12C11A322");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.TenDangNhap).HasMaxLength(100);
            entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
        });

        modelBuilder.Entity<TinNhan>(entity =>
        {
            entity.HasKey(e => e.MaTinNhan).HasName("PK__TinNhan__E5B3062AB0149873");

            entity.ToTable("TinNhan");

            entity.Property(e => e.LoaiNguoiGui)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaKhachHang).HasMaxLength(50);
            entity.Property(e => e.ThoiGian)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasDefaultValue(false);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.TinNhans)
                .HasForeignKey(d => d.MaKhachHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TinNhan__MaKhach__09A971A2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
