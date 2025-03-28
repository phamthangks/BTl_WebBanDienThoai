USE [BTL_LTW_QLBDT]
GO
/****** Object:  Table [dbo].[AnhSanPham]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnhSanPham](
	[TenFile] [nvarchar](255) NOT NULL,
	[MaMau] [nvarchar](255) NULL,
	[MaSanPham] [nvarchar](50) NULL,
 CONSTRAINT [PK_AnhSanPham] PRIMARY KEY CLUSTERED 
(
	[TenFile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietGioHang]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietGioHang](
	[SoLuong] [int] NULL,
	[MaGioHang] [nvarchar](50) NOT NULL,
	[MaSanPham] [nvarchar](50) NOT NULL,
	[ThongSoMau] [nvarchar](50) NOT NULL,
	[ThongSoRom] [nvarchar](50) NOT NULL,
	[MaChiTietGioHang] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ChiTietGioHang] PRIMARY KEY CLUSTERED 
(
	[MaChiTietGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonBan]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonBan](
	[SoLuongBan] [int] NULL,
	[DonGiaCuoi] [decimal](18, 2) NULL,
	[MaHoaDon] [nvarchar](50) NOT NULL,
	[MaSanPham] [nvarchar](50) NOT NULL,
	[MaChiTietHoaDonBan] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ChiTietHoaDonBan] PRIMARY KEY CLUSTERED 
(
	[MaChiTietHoaDonBan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhGia]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhGia](
	[NoiDung] [nvarchar](255) NULL,
	[Rate] [int] NULL,
	[TenDangNhap] [nvarchar](100) NOT NULL,
	[ThoiGianDanhGia] [datetime] NOT NULL,
	[MaSanPham] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DanhGia] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC,
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioHang]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHang](
	[MaGioHang] [nvarchar](50) NOT NULL,
	[TongTien] [decimal](18, 2) NULL,
	[TenDangNhap] [nvarchar](100) NULL,
 CONSTRAINT [PK_GioHang] PRIMARY KEY CLUSTERED 
(
	[MaGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hang]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hang](
	[MaHang] [nvarchar](50) NOT NULL,
	[TenHang] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDonBan]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonBan](
	[MaHoaDon] [nvarchar](50) NOT NULL,
	[PhuongThucThanhToan] [nvarchar](100) NULL,
	[TongTien] [decimal](18, 2) NULL,
	[KhuyenMai] [decimal](18, 2) NULL,
	[ThoiGianLap] [datetime] NOT NULL,
	[MaNhanVien] [nvarchar](50) NULL,
	[MaKhachHang] [nvarchar](50) NULL,
	[TrangThai] [nvarchar](30) NULL,
	[PhiGiaoHang] [decimal](18, 2) NULL,
	[DiaChiGiaoHang] [nvarchar](255) NULL,
	[TrangThaiGiaoHang] [bit] NULL,
	[DiaChi_Latitude] [decimal](10, 8) NULL,
	[DiaChi_Longtitude] [decimal](11, 8) NULL,
	[GhiChuHD] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKhachHang] [nvarchar](50) NOT NULL,
	[TenKhachHang] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[SoDienThoai] [nvarchar](20) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[LoaiKhachHang] [nvarchar](100) NULL,
	[GhiChu] [nvarchar](255) NULL,
	[AnhDaiDien] [nvarchar](255) NULL,
	[TenDangNhap] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[ResetCode] [int] NULL,
	[ResetCodeExpiry] [datetime] NULL,
	[DiaChi_Latitude] [decimal](10, 8) NULL,
	[DiaChi_Longitude] [decimal](11, 8) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuHoatDong]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuHoatDong](
	[MaHoatDong] [nvarchar](50) NOT NULL,
	[LoaiHoatDong] [nvarchar](100) NULL,
	[ThoiGianThucHien] [datetime] NULL,
	[GhiChu] [nvarchar](255) NULL,
	[TenDangNhap] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoatDong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MauSac]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MauSac](
	[MaMau] [nvarchar](255) NOT NULL,
	[TenMau] [nvarchar](50) NULL,
	[MaSanPham] [nvarchar](50) NULL,
 CONSTRAINT [PK_MauSac] PRIMARY KEY CLUSTERED 
(
	[MaMau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] [nvarchar](50) NOT NULL,
	[TenNhanVien] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[SoDienThoai] [nvarchar](20) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[ChucVu] [nvarchar](100) NULL,
	[GhiChu] [nvarchar](255) NULL,
	[AnhDaiDien] [nvarchar](255) NULL,
	[TenDangNhap] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROM]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROM](
	[MaROM] [nvarchar](255) NOT NULL,
	[ThongSo] [nvarchar](50) NULL,
	[Gia] [decimal](18, 2) NULL,
	[MaSanPham] [nvarchar](50) NULL,
 CONSTRAINT [PK_ROM] PRIMARY KEY CLUSTERED 
(
	[MaROM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSanPham] [nvarchar](50) NOT NULL,
	[TenSanPham] [nvarchar](100) NULL,
	[ThoiGianBaoHanh] [int] NULL,
	[SoLuongTonKho] [int] NULL,
	[DonGiaBanGoc] [decimal](18, 2) NULL,
	[DonGiaBanRa] [decimal](18, 2) NULL,
	[KhuyenMai] [decimal](18, 2) NULL,
	[DanhBa] [nvarchar](255) NULL,
	[DenFlash] [nvarchar](255) NULL,
	[CongNgheManHinh] [nvarchar](255) NULL,
	[DoSangToiDa] [int] NULL,
	[LoaiPin] [nvarchar](255) NULL,
	[BaoMatNangCao] [nvarchar](255) NULL,
	[GhiAmMacDinh] [nvarchar](255) NULL,
	[JackTaiNghe] [nvarchar](255) NULL,
	[MangDiDong] [nvarchar](255) NULL,
	[Sim] [nvarchar](255) NULL,
	[MaHang] [nvarchar](50) NULL,
	[ManHinh] [nvarchar](100) NULL,
	[Pin] [nvarchar](50) NULL,
	[Camera] [nvarchar](50) NULL,
	[KichThuoc] [nvarchar](100) NULL,
	[Chip] [nvarchar](50) NULL,
	[RAM] [nvarchar](50) NULL,
	[AnhDaiDien] [nvarchar](255) NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[TenDangNhap] [nvarchar](100) NOT NULL,
	[MatKhau] [nvarchar](100) NULL,
	[LoaiTaiKhoan] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TinNhan]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TinNhan](
	[MaTinNhan] [int] IDENTITY(1,1) NOT NULL,
	[NoiDung] [nvarchar](max) NULL,
	[ThoiGian] [datetime] NULL,
	[TrangThai] [bit] NULL,
	[MaKhachHang] [nvarchar](50) NOT NULL,
	[LoaiNguoiGui] [varchar](10) NULL,
 CONSTRAINT [PK__TinNhan__E5B3062AB0149873] PRIMARY KEY CLUSTERED 
(
	[MaTinNhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxya05s-1.jpg', N'mss1001', N'ss001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxya05s-2.jpg', N'mss1001', N'ss001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxya05s-3.jpg', N'mss1001', N'ss001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxya15-1.jpg', N'mss2005', N'ss002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxya15-2.jpg', N'mss2005', N'ss002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxya15-3.jpg', N'mss2005', N'ss002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxys24ultra-1.jpg', N'mss3m001', N'ss003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxys24ultra-2.jpg', N'mss3m001', N'ss003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxys24ultra-3.jpg', N'mss3m001', N'ss003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxyzflip6-1.jpg', N'mss4m001', N'ss004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxyzflip6-2.jpg', N'mss4m001', N'ss004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxyzflip6-3.jpg', N'mss4m001', N'ss004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxyzfold6-1', N'mss5m003', N'ss005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxyzfold6-2', N'mss5m003', N'ss005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'galaxyzfold6-3', N'mss5m003', N'ss005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13proden-1.jpg', N'mip35003', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13proden-2.jpg', N'mip35003', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13proden-3.jpg', N'mip35003', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13protrang-1.jpg', N'mip45004', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13protrang-2.jpg', N'mip45004', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13protrang-3.jpg', N'mip45004', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13provang-1.jpg', N'mip55005', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13provang-2.jpg', N'mip55005', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13provang-3.jpg', N'mip55005', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13proxanh-1.jpg', N'mip15001', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13proxanh-2.jpg', N'mip15001', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13proxanh-3.jpg', N'mip15001', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13xanhla-1.jpg', N'mip25002', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13xanhla-2.jpg', N'mip25002', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone13xanhla-3.jpg', N'mip25002', N'ip005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15den-1.jpg', N'mip33003', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15den-2.jpg', N'mip33003', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15den-3.jpg', N'mip33003', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15hong-1.jpg', N'mip23002', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15hong-2.jpg', N'mip23002', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15hong-3.jpg', N'mip23002', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmden-1.jpg', N'mip34003', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmden-2.jpg', N'mip34003', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmden-3.jpg', N'mip34003', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmtrang-1.jpg', N'mip44004', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmtrang-2.jpg', N'mip44004', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmtrang-3.jpg', N'mip44004', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmtt-1.jpg', N'mip54005', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmtt-2.jpg', N'mip54005', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmtt-3.jpg', N'mip54005', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmxanh-1.jpg', N'mip14001', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmxanh-2.jpg', N'mip14001', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15prmxanh-3.jpg', N'mip14001', N'ip004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15vang-1.jpg', N'mip53005', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15vang-2.jpg', N'mip53005', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15vang-3.jpg', N'mip53005', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15xanh-1.jpg', N'mip13001', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15xanh-2.jpg', N'mip13001', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15xanh-3.jpg', N'mip13001', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15xanhla-1.jpg', N'mip43004', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15xanhla-2.jpg', N'mip43004', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone15xanhla-3.jpg', N'mip43004', N'ip003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16den-1.jpg', N'mip32003', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16den-2.jpg', N'mip32003', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16den-3.jpg', N'mip32003', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16hong-1.jpg', N'mip22002', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16hong-2.jpg', N'mip22002', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16hong-3.jpg', N'mip22002', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prm-1.jpg', N'mip11001', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prm-2.jpg', N'mip11001', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prm-3.jpg', N'mip11001', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmden-1.jpg', N'mip31003', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmden-2.jpg', N'mip31003', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmden-3.jpg', N'mip31003', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmsm-1.jpg', N'mip51005', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmsm-2.jpg', N'mip51005', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmsm-3.jpg', N'mip51005', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmtt-1.jpg', N'mip41004', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmtt-2.jpg', N'mip41004', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16prmtt-3.jpg', N'mip41004', N'ip001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16trang-1.jpg', N'mip42004', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16trang-2.jpg', N'mip42004', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16trang-3.jpg', N'mip42004', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16xanh-1.jpg', N'mip12001', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16xanh-2.jpg', N'mip12001', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16xanh-3.jpg', N'mip12001', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16xanhla-1.jpg', N'mip52005', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16xanhla-2.jpg', N'mip52005', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'iphone16xanhla-3.jpg', N'mip52005', N'ip002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppoa3x-1.jpg', N'mop1m001', N'op001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppoa3x-2.jpg', N'mop1m001', N'op001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppoa3x-3.jpg', N'mop1m001', N'op001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppoa60-1.jpg', N'mop2m005', N'op002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppoa60-2.jpg', N'mop2m005', N'op002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppoa60-3.jpg', N'mop2m005', N'op002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppofindn3-1.jpg', N'mop3m004', N'op003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppofindn3-2.jpg', N'mop3m004', N'op003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'oppofindn3-3.jpg', N'mop3m004', N'op003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'opporeno11-1.jpg', N'mop4m005', N'op004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'opporeno11-2.jpg', N'mop4m005', N'op004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'opporeno11-3.jpg', N'mop4m005', N'op004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'opporeno12-1.jpg', N'mop5m004', N'op005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'opporeno12-2.jpg', N'mop5m004', N'op005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'opporeno12-3.jpg', N'mop5m004', N'op005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc51-1.jpg', N'mrm1001', N'rm001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc51-2.jpg', N'mrm1001', N'rm001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc51-3.jpg', N'mrm1001', N'rm001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc60-1.jpg', N'mrm2001', N'rm002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc60-2.jpg', N'mrm2001', N'rm002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc60-3.jpg', N'mrm2001', N'rm002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc65-1.jpg', N'mrm3001', N'rm003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc65-2.jpg', N'mrm3001', N'rm003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc65-3.jpg', N'mrm3001', N'rm003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc67-1.jpg', N'mrm4001', N'rm004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc67-2.jpg', N'mrm4001', N'rm004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmc67-3.jpg', N'mrm4001', N'rm004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmnote20-1.jpg', N'mrm5001', N'rm005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmnote20-2.jpg', N'mrm5001', N'rm005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'rmnote20-3.jpg', N'mrm5001', N'rm005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivov29-1.jpg', N'mvv1001', N'vv001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivov29-2.jpg', N'mvv1001', N'vv001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivov29-3.jpg', N'mvv1001', N'vv001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy17s-1.jpg', N'mvv2001', N'vv002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy17s-2.jpg', N'mvv2001', N'vv002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy17s-3.jpg', N'mvv2001', N'vv002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy18-1.jpg', N'mvv3001', N'vv003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy18-2.jpg', N'mvv3001', N'vv003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy18-3.jpg', N'mvv3001', N'vv003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy28-1.jpg', N'mvv4002', N'vv004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy28-2.jpg', N'mvv4002', N'vv004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy28-3.jpg', N'mvv4002', N'vv004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy36-1.jpg', N'mvv5001', N'vv005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy36-2.jpg', N'mvv5001', N'vv005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'vivoy36-3.jpg', N'mvv5001', N'vv005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14-1.jpg', N'mxm1004', N'xm001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14-2.jpg', N'mxm1004', N'xm001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14-3.jpg', N'mxm1004', N'xm001')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14c-1.jpg', N'mxm2003', N'xm002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14c-2.jpg', N'mxm2003', N'xm002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14c-3.jpg', N'mxm2003', N'xm002')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14ultra-1.jpg', N'mxm3003', N'xm003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14ultra-2.jpg', N'mxm3003', N'xm003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomi14ultra-3.jpg', N'mxm3003', N'xm003')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomia3-1.jpg', N'mxm4001', N'xm004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomia3-2.jpg', N'mxm4001', N'xm004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaomia3-3.jpg', N'mxm4001', N'xm004')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaominote13-1.jpg', N'mxm5004', N'xm005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaominote13-2.jpg', N'mxm5004', N'xm005')
GO
INSERT [dbo].[AnhSanPham] ([TenFile], [MaMau], [MaSanPham]) VALUES (N'xiaominote13-3.jpg', N'mxm5004', N'xm005')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'2b3d3427-f3c9-4cd9-9ff3-3373f0ee3f4e', N'ip003', N'mip13001', N'roip3003', N'be952e2e-4df1-44b3-8b97-cc01abbd6411')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH002', N'ip002', N'xanh', N'256GB', N'CTGH1')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH004', N'vv004', N'xanh', N'256GB', N'CTGH10')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (3, N'GH005', N'ip003', N'xanh', N'256GB', N'CTGH11')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (3, N'GH005', N'ip005', N'xanh', N'256GB', N'CTGH12')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (2, N'GH005', N'vv005', N'xanh', N'256GB', N'CTGH13')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (2, N'GH005', N'xm005', N'xanh', N'256GB', N'CTGH14')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH002', N'ip004', N'xanh', N'256GB', N'CTGH2')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH002', N'vv002', N'xanh', N'256GB', N'CTGH3')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH002', N'xm002', N'xanh', N'256GB', N'CTGH4')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH003', N'op003', N'xanh', N'256GB', N'CTGH5')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH003', N'xm003', N'xanh', N'256GB', N'CTGH6')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH004', N'ip004', N'xanh', N'256GB', N'CTGH7')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH004', N'ip005', N'xanh', N'256GB', N'CTGH8')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'GH004', N'op004', N'xanh', N'256GB', N'CTGH9')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (2, N'd9cc721a-4037-422e-9cd3-9651c4c1927b', N'ip004', N'mip14001', N'roip4002', N'd168c89c-8977-4f4b-a748-eef0c08bf390')
GO
INSERT [dbo].[ChiTietGioHang] ([SoLuong], [MaGioHang], [MaSanPham], [ThongSoMau], [ThongSoRom], [MaChiTietGioHang]) VALUES (1, N'2b3d3427-f3c9-4cd9-9ff3-3373f0ee3f4e', N'ip002', N'mip12001', N'roip2003', N'fcbabf8f-a950-4d35-8e20-bb3332743423')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(30661200.00 AS Decimal(18, 2)), N'HD8491', N'ip004', N'011a50a5-59dd-419d-bf69-672d2cf4896f')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(33000000.00 AS Decimal(18, 2)), N'HD7772', N'ip002', N'1d1b47cc-c189-49fe-b5b2-a347bc16efd8')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(12320000.00 AS Decimal(18, 2)), N'HD6144', N'ip005', N'1d495a72-6428-43b8-997b-41e0b2f401f6')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(26438500.00 AS Decimal(18, 2)), N'HD5686', N'ip003', N'4c87343d-b123-42b4-a930-f7a21cc94fb6')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(33000000.00 AS Decimal(18, 2)), N'HD5686', N'ip002', N'5204e35b-f9f5-4762-8177-98c970bbb10c')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(33000000.00 AS Decimal(18, 2)), N'HD3503', N'ip002', N'54ec1700-7e27-423e-9633-ed87ccd58ad9')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(30661200.00 AS Decimal(18, 2)), N'HD1259', N'ip004', N'74041953-5a1e-4214-8fbf-47a13b16004b')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (4, CAST(2978500.00 AS Decimal(18, 2)), N'HD1933', N'vv002', N'958e834d-bd9b-4d52-8109-376ac6a3e2e8')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(33000000.00 AS Decimal(18, 2)), N'HD6144', N'ip002', N'9f749209-b14b-4cca-993a-edb9d8ba5598')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(26438500.00 AS Decimal(18, 2)), N'HD6540', N'ip003', N'b082f2be-2628-401b-8fe4-8e582c608e05')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(36739500.00 AS Decimal(18, 2)), N'HD011', N'ip001', N'CTHDB1')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(2863500.00 AS Decimal(18, 2)), N'HD012', N'xm004', N'CTHDB10')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(26438500.00 AS Decimal(18, 2)), N'HD013', N'ip003', N'CTHDB11')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(12320000.00 AS Decimal(18, 2)), N'HD013', N'ip005', N'CTHDB12')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(15739500.00 AS Decimal(18, 2)), N'HD013', N'op003', N'CTHDB13')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(6708800.00 AS Decimal(18, 2)), N'HD013', N'vv003', N'CTHDB14')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(30661200.00 AS Decimal(18, 2)), N'HD014', N'ip004', N'CTHDB15')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(6589000.00 AS Decimal(18, 2)), N'HD014', N'op004', N'CTHDB16')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(2474700.00 AS Decimal(18, 2)), N'HD014', N'vv004', N'CTHDB17')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(2863500.00 AS Decimal(18, 2)), N'HD014', N'xm004', N'CTHDB18')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(36739500.00 AS Decimal(18, 2)), N'HD015', N'ip001', N'CTHDB19')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(33000000.00 AS Decimal(18, 2)), N'HD011', N'ip002', N'CTHDB2')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(12320000.00 AS Decimal(18, 2)), N'HD015', N'ip005', N'CTHDB20')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(6428500.00 AS Decimal(18, 2)), N'HD015', N'op005', N'CTHDB21')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(5554500.00 AS Decimal(18, 2)), N'HD015', N'xm005', N'CTHDB22')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(33000000.00 AS Decimal(18, 2)), N'HD016', N'ip002', N'CTHDB23')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(4389000.00 AS Decimal(18, 2)), N'HD016', N'op002', N'CTHDB24')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(6708800.00 AS Decimal(18, 2)), N'HD016', N'vv003', N'CTHDB25')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(2190900.00 AS Decimal(18, 2)), N'HD016', N'xm001', N'CTHDB26')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(26438500.00 AS Decimal(18, 2)), N'HD017', N'ip003', N'CTHDB27')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(30661200.00 AS Decimal(18, 2)), N'HD017', N'ip004', N'CTHDB28')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(6428500.00 AS Decimal(18, 2)), N'HD017', N'op005', N'CTHDB29')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(26438500.00 AS Decimal(18, 2)), N'HD011', N'ip003', N'CTHDB3')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(2474700.00 AS Decimal(18, 2)), N'HD017', N'vv004', N'CTHDB30')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(36739500.00 AS Decimal(18, 2)), N'HD018', N'ip001', N'CTHDB31')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(12320000.00 AS Decimal(18, 2)), N'HD018', N'ip005', N'CTHDB32')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(6600000.00 AS Decimal(18, 2)), N'HD018', N'vv001', N'CTHDB33')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(2978500.00 AS Decimal(18, 2)), N'HD018', N'vv002', N'CTHDB34')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(26438500.00 AS Decimal(18, 2)), N'HD019', N'ip003', N'CTHDB35')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(30661200.00 AS Decimal(18, 2)), N'HD019', N'ip004', N'CTHDB36')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(6589000.00 AS Decimal(18, 2)), N'HD019', N'op004', N'CTHDB37')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(2978500.00 AS Decimal(18, 2)), N'HD019', N'vv002', N'CTHDB38')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(36739500.00 AS Decimal(18, 2)), N'HD020', N'ip001', N'CTHDB39')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(12320000.00 AS Decimal(18, 2)), N'HD011', N'ip005', N'CTHDB4')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(26438500.00 AS Decimal(18, 2)), N'HD020', N'ip003', N'CTHDB40')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(6589000.00 AS Decimal(18, 2)), N'HD020', N'op004', N'CTHDB41')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(2978500.00 AS Decimal(18, 2)), N'HD020', N'vv002', N'CTHDB42')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(6600000.00 AS Decimal(18, 2)), N'HD011', N'vv001', N'CTHDB5')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(2978500.00 AS Decimal(18, 2)), N'HD011', N'vv002', N'CTHDB6')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (3, CAST(33000000.00 AS Decimal(18, 2)), N'HD012', N'ip002', N'CTHDB7')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (3, CAST(30661200.00 AS Decimal(18, 2)), N'HD012', N'ip004', N'CTHDB8')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(3349500.00 AS Decimal(18, 2)), N'HD012', N'xm002', N'CTHDB9')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (2, CAST(33000000.00 AS Decimal(18, 2)), N'HD6540', N'ip002', N'e894cfff-54d0-4138-8980-06deeb83eb6c')
GO
INSERT [dbo].[ChiTietHoaDonBan] ([SoLuongBan], [DonGiaCuoi], [MaHoaDon], [MaSanPham], [MaChiTietHoaDonBan]) VALUES (1, CAST(30661200.00 AS Decimal(18, 2)), N'HD5612', N'ip004', N'f96ef27c-64fa-43a0-8e37-31b0a040d3a1')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'okok', 4, N'user11', CAST(N'2024-11-16T23:27:31.020' AS DateTime), N'ip001')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'Chất lượng sản phẩm tốt nhưng giá hơi cao.', 3, N'user15', CAST(N'2023-10-20T15:25:00.000' AS DateTime), N'ip001')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'tệ', 2, N'user11', CAST(N'2024-11-16T23:48:37.917' AS DateTime), N'ip002')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'Tôi không hài lòng với dịch vụ hỗ trợ.', 1, N'user12', CAST(N'2023-05-25T11:20:00.000' AS DateTime), N'ip002')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'không bằng iphone 14', 4, N'user11', CAST(N'2024-11-16T23:26:45.020' AS DateTime), N'ip003')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'Dịch vụ khách hàng rất tốt!', 4, N'user13', CAST(N'2023-07-15T08:40:00.000' AS DateTime), N'ip003')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'Rất hài lòng, sản phẩm xịn!', 5, N'user12', CAST(N'2023-06-30T13:50:00.000' AS DateTime), N'ip004')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'Sản phẩm giao chậm nhưng chất lượng ổn.', 3, N'user14', CAST(N'2023-09-05T12:10:00.000' AS DateTime), N'ip004')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'Giao hàng nhanh chóng, nhưng sản phẩm bị lỗi.', 5, N'user11', CAST(N'2024-11-16T23:35:02.463' AS DateTime), N'ip005')
GO
INSERT [dbo].[DanhGia] ([NoiDung], [Rate], [TenDangNhap], [ThoiGianDanhGia], [MaSanPham]) VALUES (N'Sản phẩm như mô tả, tôi hài lòng!', 4, N'user13', CAST(N'2023-08-10T17:30:00.000' AS DateTime), N'ip005')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'2b3d3427-f3c9-4cd9-9ff3-3373f0ee3f4e', CAST(59438500.00 AS Decimal(18, 2)), N'user11')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'3999a934-eefa-4b0c-b052-99c80903f6f1', CAST(0.00 AS Decimal(18, 2)), N'user12')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'd9cc721a-4037-422e-9cd3-9651c4c1927b', CAST(61322400.00 AS Decimal(18, 2)), N'dtkiet')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'ea7f2ada-19c2-497a-b554-fc185b2f07b3', CAST(0.00 AS Decimal(18, 2)), N'kietkid4')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'GH001', CAST(0.00 AS Decimal(18, 2)), N'user06')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'GH002', CAST(86443200.00 AS Decimal(18, 2)), N'user07')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'GH003', CAST(75971500.00 AS Decimal(18, 2)), N'user08')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'GH004', CAST(137843400.00 AS Decimal(18, 2)), N'user09')
GO
INSERT [dbo].[GioHang] ([MaGioHang], [TongTien], [TenDangNhap]) VALUES (N'GH005', CAST(260707200.00 AS Decimal(18, 2)), N'user10')
GO
INSERT [dbo].[Hang] ([MaHang], [TenHang]) VALUES (N'ap', N'Apple')
GO
INSERT [dbo].[Hang] ([MaHang], [TenHang]) VALUES (N'op', N'Oppo')
GO
INSERT [dbo].[Hang] ([MaHang], [TenHang]) VALUES (N'rm', N'Realme')
GO
INSERT [dbo].[Hang] ([MaHang], [TenHang]) VALUES (N'ss', N'SamSung')
GO
INSERT [dbo].[Hang] ([MaHang], [TenHang]) VALUES (N'vv', N'Vivo')
GO
INSERT [dbo].[Hang] ([MaHang], [TenHang]) VALUES (N'xm', N'Xiaomi')
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD011', N'Tiền mặt', NULL, NULL, CAST(N'2024-10-01T00:00:00.000' AS DateTime), N'NV001', N'KH011', N'Đã Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD012', N'Chuyển khoản', NULL, NULL, CAST(N'2024-10-02T00:00:00.000' AS DateTime), N'NV002', N'KH012', N'Đã Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD013', N'Thẻ tín dụng', NULL, NULL, CAST(N'2024-10-03T00:00:00.000' AS DateTime), N'NV003', N'KH013', N'Chưa Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD014', N'Tiền mặt', NULL, NULL, CAST(N'2024-10-04T00:00:00.000' AS DateTime), N'NV004', N'KH014', N'Chưa Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD015', N'Chuyển khoản', NULL, NULL, CAST(N'2024-10-05T00:00:00.000' AS DateTime), N'NV005', N'KH015', N'Chưa Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD016', N'Thẻ tín dụng', NULL, NULL, CAST(N'2024-10-06T00:00:00.000' AS DateTime), N'NV001', N'KH016', N'Đã Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD017', N'Tiền mặt', NULL, NULL, CAST(N'2024-10-07T00:00:00.000' AS DateTime), N'NV002', N'KH017', N'Chưa Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD018', N'Chuyển khoản', NULL, NULL, CAST(N'2024-10-08T00:00:00.000' AS DateTime), N'NV003', N'KH018', N'Đã Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD019', N'Thẻ tín dụng', NULL, NULL, CAST(N'2024-10-09T00:00:00.000' AS DateTime), N'NV004', N'KH019', N'Đã Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD020', N'Tiền mặt', NULL, NULL, CAST(N'2024-10-10T00:00:00.000' AS DateTime), N'NV005', N'KH020', N'Đã Thanh Toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD1259', N'Bank transfer via QR code', CAST(30661200.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T22:08:47.077' AS DateTime), NULL, N'FiTCs', N'đã thanh toán', CAST(25000.00 AS Decimal(18, 2)), N'Ng. 277 P. Quan Hoa, Nghĩa Đô, Cầu Giấy, Hà Nội, Vietnam', 0, CAST(21.03834610 AS Decimal(10, 8)), CAST(105.80580130 AS Decimal(11, 8)), N'')
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD1933', N'Pay at store', CAST(11914000.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T21:33:48.420' AS DateTime), NULL, N'KH012', N'chưa thanh toán', CAST(120000.00 AS Decimal(18, 2)), N'Chương Mỹ, Hanoi, Vietnam', NULL, CAST(20.86265160 AS Decimal(10, 8)), CAST(105.66471420 AS Decimal(11, 8)), N'giao vào buổi sáng')
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD3503', N'Pay at store', CAST(66000000.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T21:36:20.067' AS DateTime), NULL, N'KH012', N'chưa thanh toán', CAST(120000.00 AS Decimal(18, 2)), N'Chương Mỹ, Hanoi, Vietnam', 0, CAST(20.86265160 AS Decimal(10, 8)), CAST(105.66471420 AS Decimal(11, 8)), N'giao buổi chiều')
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD5612', N'Bank transfer via QR code', CAST(30661200.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T21:47:26.850' AS DateTime), NULL, N'FiTCs', N'đã thanh toán', CAST(25000.00 AS Decimal(18, 2)), N'Ng. 277 P. Quan Hoa, Nghĩa Đô, Cầu Giấy, Hà Nội, Vietnam', 0, CAST(21.03834610 AS Decimal(10, 8)), CAST(105.80580130 AS Decimal(11, 8)), N'')
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD5686', N'Bank transfer via QR code', CAST(59438500.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T20:52:16.557' AS DateTime), NULL, N'amlGP', N'đã thanh toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD6144', N'Bank transfer via QR code', CAST(45320000.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T20:29:36.640' AS DateTime), NULL, N'amlGP', N'đã thanh toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD6540', N'Pay at store', CAST(92438500.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T20:50:08.203' AS DateTime), NULL, N'amlGP', N'chưa thanh toán', NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD7772', N'Bank transfer via QR code', CAST(66000000.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T21:42:07.747' AS DateTime), NULL, N'KH012', N'đã thanh toán', CAST(120000.00 AS Decimal(18, 2)), N'Chương Mỹ, Hanoi, Vietnam', 0, CAST(20.86265160 AS Decimal(10, 8)), CAST(105.66471420 AS Decimal(11, 8)), N'giao buổi chiều')
GO
INSERT [dbo].[HoaDonBan] ([MaHoaDon], [PhuongThucThanhToan], [TongTien], [KhuyenMai], [ThoiGianLap], [MaNhanVien], [MaKhachHang], [TrangThai], [PhiGiaoHang], [DiaChiGiaoHang], [TrangThaiGiaoHang], [DiaChi_Latitude], [DiaChi_Longtitude], [GhiChuHD]) VALUES (N'HD8491', N'Bank transfer via QR code', CAST(30661200.00 AS Decimal(18, 2)), NULL, CAST(N'2024-11-20T22:14:55.733' AS DateTime), NULL, N'FiTCs', N'đã thanh toán', CAST(40000.00 AS Decimal(18, 2)), N'XQG6+GJJ, La Khê, Hà Đông, Hanoi, Vietnam', 0, CAST(20.97633970 AS Decimal(10, 8)), CAST(105.76152450 AS Decimal(11, 8)), N'')
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'amlGP', N'Nguyễn Văn Kiệt', CAST(N'2004-11-06' AS Date), N'0968727926', N'8 P. Khúc Thừa Dụ, Dịch Vọng, Cầu Giấy, Hà Nội, Vietnam', NULL, NULL, N'12fad9db-078b-467f-ac21-90a8ce8ff229_anh-the-2024.jpg', N'dtkiet', N'vankietdt@gmail.com', NULL, NULL, CAST(21.03470900 AS Decimal(10, 8)), CAST(105.79304330 AS Decimal(11, 8)))
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'FiTCs', N'Nguyễn Văn Kiệt', CAST(N'2004-03-09' AS Date), N'0968727926', N'Ngõ 277', NULL, NULL, N'89e7f191-c1c8-42b0-8440-ec8b4a5a16c9_2.jpg', N'kietkid4', N'vankiet@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH006', N'Nguyễn Văn F', CAST(N'1987-11-15' AS Date), N'0901234566', N'789 Sixth St', N'VIP', N'', NULL, N'user06', N'vankiet11@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH007', N'Trần Thị G', CAST(N'1991-01-29' AS Date), N'0901234567', N'987 Seventh St', N'VIP', N'', NULL, N'user07', N'vankiet12@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH008', N'Lê Văn H', CAST(N'1983-03-17' AS Date), N'0901234568', N'111 Eighth St', N'VIP', N'', NULL, N'user08', N'vankiet13@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH009', N'Phạm Thị I', CAST(N'1998-06-12' AS Date), N'0901234569', N'222 Ninth St', N'VIP', N'', NULL, N'user09', N'vankiet14@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH010', N'Hoàng Văn J', CAST(N'1994-12-05' AS Date), N'0901234570', N'333 Tenth St', N'VIP', N'', NULL, N'user10', N'vankiet15@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH011', N'Nguyễn Văn K', CAST(N'1990-07-14' AS Date), N'0901234571', N'444 Eleventh St', N'Regular', N'', NULL, N'user11', N'vankiet16@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH012', N'Trần Thị L', CAST(N'1986-05-09' AS Date), N'0901234572', N'555 Twelfth St', N'Regular', N'giao buổi chiều', NULL, N'user12', N'vankiet17@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH013', N'Lê Văn M', CAST(N'1993-02-22' AS Date), N'0901234573', N'666 Thirteenth St', N'Regular', N'', NULL, N'user13', N'vankiet18@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH014', N'Phạm Thị N', CAST(N'1989-09-11' AS Date), N'0901234574', N'777 Fourteenth St', N'Regular', N'', NULL, N'user14', N'vankiet19@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH015', N'Hoàng Văn O', CAST(N'1991-11-23' AS Date), N'0901234575', N'888 Fifteenth St', N'Regular', N'', NULL, N'user15', N'vankiet20@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH016', N'Nguyễn Văn P', CAST(N'1992-03-10' AS Date), N'0901234576', N'999 Sixteenth St', N'Regular', N'', NULL, N'user16', N'vankiet21@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH017', N'Trần Thị Q', CAST(N'1985-04-08' AS Date), N'0901234577', N'111 Seventeenth St', N'Regular', N'', NULL, N'user17', N'vankiet22@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH018', N'Lê Văn R', CAST(N'1997-10-30' AS Date), N'0901234578', N'222 Eighteenth St', N'Regular', N'', NULL, N'user18', N'vankiet23@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH019', N'Phạm Thị S', CAST(N'1996-08-27' AS Date), N'0901234579', N'333 Nineteenth St', N'Regular', N'', NULL, N'user19', N'vankiet24@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [NgaySinh], [SoDienThoai], [DiaChi], [LoaiKhachHang], [GhiChu], [AnhDaiDien], [TenDangNhap], [Email], [ResetCode], [ResetCodeExpiry], [DiaChi_Latitude], [DiaChi_Longitude]) VALUES (N'KH020', N'Hoàng Văn T', CAST(N'1993-12-18' AS Date), N'0901234580', N'444 Twentieth St', N'Regular', N'', NULL, N'user20', N'vankiet25@gmail.com', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip11001', N'xanh', N'ip001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip12001', N'xanh', N'ip002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip13001', N'xanh', N'ip003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip14001', N'xanh', N'ip004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip15001', N'xanh', N'ip005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip21002', N'tím', N'ip001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip22002', N'tím', N'ip002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip23002', N'tím', N'ip003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip24002', N'tím', N'ip004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip25002', N'tím', N'ip005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip31003', N'đen', N'ip001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip32003', N'đen', N'ip002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip33003', N'đen', N'ip003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip34003', N'đen', N'ip004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip35003', N'đen', N'ip005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip41004', N'trắng', N'ip001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip42004', N'trắng', N'ip002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip43004', N'trắng', N'ip003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip44004', N'trắng', N'ip004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip45004', N'trắng', N'ip005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip51005', N'vàng', N'ip001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip52005', N'vàng', N'ip002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip53005', N'vàng', N'ip003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip54005', N'vàng', N'ip004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mip55005', N'vàng', N'ip005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mop1001', N'xanh', N'op001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mop2005', N'vàng', N'op002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mop3004', N'trắng', N'op003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mop4005', N'vàng', N'op004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mop5004', N'trắng', N'op005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mrm1001', N'xanh', N'rm001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mrm2001', N'xanh', N'rm002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mrm3001', N'xanh', N'rm003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mrm4001', N'xanh', N'rm004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mrm5001', N'xanh', N'rm005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mss1001', N'xanh', N'ss001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mss2005', N'vàng', N'ss002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mss3001', N'xanh', N'ss003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mss4001', N'xanh', N'ss004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mss5003', N'đen', N'ss005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mvv1001', N'xanh', N'vv001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mvv2001', N'xanh', N'vv002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mvv3001', N'xanh', N'vv003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mvv4002', N'tím', N'vv004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mvv5001', N'xanh', N'vv005')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mxm1004', N'trắng', N'xm001')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mxm2003', N'đen', N'xm002')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mxm3003', N'đen', N'xm003')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mxm4001', N'xanh', N'xm004')
GO
INSERT [dbo].[MauSac] ([MaMau], [TenMau], [MaSanPham]) VALUES (N'mxm5004', N'trắng', N'xm005')
GO
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu], [GhiChu], [AnhDaiDien], [TenDangNhap]) VALUES (N'NV001', N'Nguyễn Văn A', CAST(N'1985-02-10' AS Date), N'0909876541', N'123 First St', N'Quản lý', N'', NULL, N'user01')
GO
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu], [GhiChu], [AnhDaiDien], [TenDangNhap]) VALUES (N'NV002', N'Trần Thị B', CAST(N'1990-06-22' AS Date), N'0909876542', N'456 Second St', N'Nhân viên bán hàng', N'', NULL, N'user02')
GO
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu], [GhiChu], [AnhDaiDien], [TenDangNhap]) VALUES (N'NV003', N'Lê Văn C', CAST(N'1988-09-15' AS Date), N'0909876543', N'789 Third St', N'Nhân viên kho', N'', NULL, N'user03')
GO
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu], [GhiChu], [AnhDaiDien], [TenDangNhap]) VALUES (N'NV004', N'Phạm Thị D', CAST(N'1992-11-30' AS Date), N'0909876544', N'321 Fourth St', N'Nhân viên hỗ trợ', N'', NULL, N'user04')
GO
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [NgaySinh], [SoDienThoai], [DiaChi], [ChucVu], [GhiChu], [AnhDaiDien], [TenDangNhap]) VALUES (N'NV005', N'Hoàng Văn E', CAST(N'1995-04-17' AS Date), N'0909876545', N'654 Fifth St', N'Nhân viên kỹ thuật', N'', NULL, N'user05')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip1003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ip001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip1004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'ip001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip1005', N'1TB', CAST(1200000.00 AS Decimal(18, 2)), N'ip001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip2003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ip002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip2004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'ip002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip2005', N'1TB', CAST(1200000.00 AS Decimal(18, 2)), N'ip002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip3003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ip003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip3004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'ip003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip3005', N'1TB', CAST(1200000.00 AS Decimal(18, 2)), N'ip003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip4002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'ip004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip4003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ip004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip4004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'ip004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip5001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'ip005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip5002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'ip005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roip5003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ip005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop1001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'op001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop1002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'op001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop1003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'op001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop2003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'op002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop2004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'op002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop2005', N'1TB', CAST(1200000.00 AS Decimal(18, 2)), N'op002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop3001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'op003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop3002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'op003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop3003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'op003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop4002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'op004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop4003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'op004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop4004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'op004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop5002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'op005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop5003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'op005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roop5004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'op005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm1001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'rm001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm1002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'rm001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm2001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'rm002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm2002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'rm002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm2003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'rm002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm3002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'rm003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm3003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'rm003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm3004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'rm003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm4002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'rm004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm4003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'rm004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm4004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'rm004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm5002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'rm005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm5003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'rm005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rorm5005', N'1TB', CAST(1200000.00 AS Decimal(18, 2)), N'rm005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross1002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'ss001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross1003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ss001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross1004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'ss001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross2002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'ss002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross2003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ss002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross3001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'ss003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross3002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'ss003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross4001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'ss004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross4002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'ss004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross4003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'ss004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross5001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'ss005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'ross5002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'ss005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv1002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'vv001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv1003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'vv001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv1004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'vv001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv2001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'vv002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv2002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'vv002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv2003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'vv002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv3001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'vv003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv3002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'vv003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv3003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'vv003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv4001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'vv004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv4002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'vv004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv4003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'vv004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv5001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'vv005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv5002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'vv005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv5003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'vv005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv5004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'vv005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'rovv5005', N'1TB', CAST(1200000.00 AS Decimal(18, 2)), N'vv005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm1001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'xm001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm1002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'xm001')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm2001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'xm002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm2002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'xm002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm2003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'xm002')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm3001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'xm003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm3002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'xm003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm3003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'xm003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm3004', N'512GB', CAST(1000000.00 AS Decimal(18, 2)), N'xm003')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm4001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'xm004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm4002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'xm004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm4003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'xm004')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm5001', N'64GB', CAST(200000.00 AS Decimal(18, 2)), N'xm005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm5002', N'128GB', CAST(400000.00 AS Decimal(18, 2)), N'xm005')
GO
INSERT [dbo].[ROM] ([MaROM], [ThongSo], [Gia], [MaSanPham]) VALUES (N'roxm5003', N'256GB', CAST(600000.00 AS Decimal(18, 2)), N'xm005')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ip001', N'iPhone 16 Pro Max', 12, 50, CAST(34990000.00 AS Decimal(18, 2)), CAST(36739500.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'Có', N'Có', N'OLED', 1200, N'Li-ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ap', N'6.7 inch', N'4500 mAh', N'48 MP', N'160.8 x 78.1 x 7.7 mm', N'A16 Bionic', N'12GB', N'ip16prm.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ip002', N'iPhone 16', 12, 92, CAST(30000000.00 AS Decimal(18, 2)), CAST(33000000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'OLED', 1000, N'Li-ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ap', N'6.1 inch', N'4000 mAh', N'12 MP', N'146.7 x 71.5 x 7.7 mm', N'A16 Bionic', N'8GB', N'ip16.png')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ip003', N'iPhone 15', 12, 78, CAST(22990000.00 AS Decimal(18, 2)), CAST(26438500.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), N'Có', N'Có', N'OLED', 1000, N'Li-ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ap', N'6.1 inch', N'4000 mAh', N'12 MP', N'146.7 x 71.5 x 7.8 mm', N'A15 Bionic', N'6GB', N'ip15.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ip004', N'iPhone 15 Pro Max', 12, 37, CAST(28390000.00 AS Decimal(18, 2)), CAST(30661200.00 AS Decimal(18, 2)), CAST(12.00 AS Decimal(18, 2)), N'Có', N'Có', N'OLED', 1200, N'Li-ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ap', N'6.7 inch', N'4500 mAh', N'48 MP', N'160.8 x 78.1 x 7.8 mm', N'A15 Bionic', N'8GB', N'ip15prm.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ip005', N'iPhone 13', 12, 119, CAST(11000000.00 AS Decimal(18, 2)), CAST(12320000.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), N'Có', N'Có', N'OLED', 1000, N'Li-ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ap', N'6.1 inch', N'3300 mAh', N'12 MP', N'146.7 x 71.5 x 7.6 mm', N'A14 Bionic', N'4GB', N'ip13.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'op001', N'Oppo A3x', 12, 90, CAST(2599000.00 AS Decimal(18, 2)), CAST(2988850.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), N'Có', N'Có', N'LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'op', N'6.5 inch', N'4230 mAh', N'13 MP', N'163.6 x 75.5 x 8.3 mm', N'MediaTek Helio P60', N'4GB', N'oppoa3x.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'op002', N'Oppo A60', 12, 70, CAST(3990000.00 AS Decimal(18, 2)), CAST(4389000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'op', N'6.56 inch', N'5000 mAh', N'50 MP', N'163.8 x 75.1 x 8.1 mm', N'MediaTek Dimensity 6020', N'8GB', N'oppoa60.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'op003', N'Oppo Find N3', 24, 50, CAST(14990000.00 AS Decimal(18, 2)), CAST(15739500.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 1200, N'Li-Ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'op', N'7.1 inch', N'4805 mAh', N'50 MP', N'162.4 x 75.7 x 9.2 mm', N'Snapdragon 8 Gen 2', N'12GB', N'oppo_find_n3.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'op004', N'Oppo Reno 11', 12, 60, CAST(5990000.00 AS Decimal(18, 2)), CAST(6589000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 800, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'op', N'6.7 inch', N'4700 mAh', N'64 MP', N'158.2 x 73.4 x 7.6 mm', N'Snapdragon 778G', N'8GB', N'opporeno11.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'op005', N'Oppo Reno 12', 12, 55, CAST(5590000.00 AS Decimal(18, 2)), CAST(6428500.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 900, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'op', N'6.7 inch', N'4500 mAh', N'108 MP', N'160.6 x 73.5 x 8.3 mm', N'MediaTek Dimensity 920', N'8GB', N'opporeno12.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'rm001', N'Realme C51', 12, 100, CAST(2990000.00 AS Decimal(18, 2)), CAST(3438500.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'rm', N'6.7 inch', N'5000 mAh', N'50 MP', N'164.2 x 75.6 x 8.1 mm', N'Unisoc T612', N'4GB', N'realme_c51.jpeg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'rm002', N'Realme C60', 12, 80, CAST(2590000.00 AS Decimal(18, 2)), CAST(2849000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'rm', N'6.7 inch', N'5000 mAh', N'50 MP', N'164.2 x 75.6 x 8.1 mm', N'MediaTek Helio G88', N'4GB', N'realme_c60.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'rm003', N'Realme C65', 12, 70, CAST(2890000.00 AS Decimal(18, 2)), CAST(3034500.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'rm', N'6.5 inch', N'5000 mAh', N'64 MP', N'164.4 x 75.0 x 8.1 mm', N'MediaTek Helio G85', N'6GB', N'realme_c65.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'rm004', N'Realme C67', 12, 60, CAST(3990000.00 AS Decimal(18, 2)), CAST(4389000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'rm', N'6.6 inch', N'5000 mAh', N'64 MP', N'164.4 x 75.0 x 8.1 mm', N'MediaTek Helio G85', N'6GB', N'realme_c67.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'rm005', N'Realme Note 20', 12, 50, CAST(4990000.00 AS Decimal(18, 2)), CAST(4990000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'rm', N'6.6 inch', N'5000 mAh', N'108 MP', N'159.9 x 75.4 x 8.8 mm', N'MediaTek Dimensity 800', N'8GB', N'realme_note20.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ss001', N'Galaxy A05s', 12, 100, CAST(3500000.00 AS Decimal(18, 2)), CAST(3850000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'PLS LCD', 800, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'ss', N'6.5 inch', N'5000 mAh', N'13 MP', N'165.5 x 76.1 x 9.1 mm', N'Exynos 850', N'4GB', N'galaxya05s.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ss002', N'Galaxy A15', 12, 80, CAST(4000000.00 AS Decimal(18, 2)), CAST(4600000.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), N'Có', N'Có', N'TFT LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'ss', N'6.6 inch', N'5000 mAh', N'50 MP', N'167.5 x 76.1 x 8.5 mm', N'Exynos 1330', N'6GB', N'galaxya15.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ss003', N'Galaxy S24 Ultra', 24, 50, CAST(1200000.00 AS Decimal(18, 2)), CAST(1260000.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'Có', N'Có', N'Dynamic AMOLED', 1750, N'Li-Ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ss', N'6.8 inch', N'5000 mAh', N'200 MP', N'162.3 x 79.3 x 8.9 mm', N'Snapdragon 8 Gen 2', N'12GB', N'galaxys24ultra.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ss004', N'Galaxy Z Flip 6', 12, 30, CAST(999000.00 AS Decimal(18, 2)), CAST(999000.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), N'Có', N'Có', N'Dynamic AMOLED', 1200, N'Li-Ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ss', N'6.7 inch', N'3700 mAh', N'12 MP', N'165.2 x 71.9 x 6.9 mm', N'Snapdragon 8 Gen 2', N'8GB', N'galaxyzflip6.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'ss005', N'Galaxy Z Fold 6', 12, 25, CAST(1799000.00 AS Decimal(18, 2)), CAST(1978900.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'Dynamic AMOLED', 1200, N'Li-Ion', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'ss', N'7.6 inch', N'4400 mAh', N'50 MP', N'154.9 x 129.9 x 6.3 mm', N'Snapdragon 8 Gen 2', N'12GB', N'galaxyzfold6.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'vv001', N'Vivo V29', 12, 100, CAST(6000000.00 AS Decimal(18, 2)), CAST(6600000.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 1200, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'vv', N'6.7 inch', N'4600 mAh', N'50 MP', N'164.1 x 74.3 x 7.6 mm', N'Snapdragon 778G', N'8GB', N'vivov29.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'vv002', N'Vivo Y17s', 12, 76, CAST(2590000.00 AS Decimal(18, 2)), CAST(2978500.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'vv', N'6.56 inch', N'5000 mAh', N'50 MP', N'163.8 x 75.6 x 8.3 mm', N'MediaTek Helio G85', N'4GB', N'vivoy17s.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'vv003', N'Vivo Y18', 12, 70, CAST(5990000.00 AS Decimal(18, 2)), CAST(6708800.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'vv', N'6.4 inch', N'5000 mAh', N'50 MP', N'160.7 x 74.2 x 8.4 mm', N'MediaTek Helio G80', N'4GB', N'vivoy18.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'vv004', N'Vivo Y28', 12, 60, CAST(2190000.00 AS Decimal(18, 2)), CAST(2474700.00 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'vv', N'6.2 inch', N'4000 mAh', N'13 MP', N'144.4 x 72.8 x 8.5 mm', N'MediaTek MT6582', N'1GB', N'vivoy28.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'vv005', N'Vivo Y36', 12, 50, CAST(3990000.00 AS Decimal(18, 2)), CAST(4189500.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'Có', N'Có', N'IPS LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'vv', N'6.6 inch', N'5000 mAh', N'50 MP', N'161.5 x 75.5 x 8.2 mm', N'MediaTek Helio G88', N'8GB', N'vivoy36.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'xm001', N'Xiaomi 14', 12, 100, CAST(2190900.00 AS Decimal(18, 2)), CAST(2190900.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 1200, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'xm', N'6.73 inch', N'4500 mAh', N'50 MP', N'152.8 x 71.5 x 8.2 mm', N'Snapdragon 8 Gen 2', N'8GB', N'xiaomi_14.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'xm002', N'Xiaomi 14C', 12, 80, CAST(3190000.00 AS Decimal(18, 2)), CAST(3349500.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'Có', N'Có', N'LCD', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'xm', N'6.67 inch', N'5000 mAh', N'50 MP', N'158.5 x 71.5 x 7.8 mm', N'MediaTek Dimensity 6100+', N'6GB', N'xiaomi_14c.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'xm003', N'Xiaomi 14 Ultra', 12, 70, CAST(29990000.00 AS Decimal(18, 2)), CAST(28490500.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 1200, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'xm', N'6.73 inch', N'5000 mAh', N'200 MP', N'161.1 x 74.3 x 9.6 mm', N'Snapdragon 8 Gen 2', N'12GB', N'xiaomi_14ultra.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'xm004', N'Xiaomi A3', 12, 50, CAST(2490000.00 AS Decimal(18, 2)), CAST(2863500.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 600, N'Li-Po', N'Có', N'Có', N'3.5mm', N'4G', N'Nano SIM', N'xm', N'6.09 inch', N'4030 mAh', N'48 MP', N'153.1 x 71.9 x 8.5 mm', N'Snapdragon 665', N'4GB', N'xiaomi_a3.jpg')
GO
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [ThoiGianBaoHanh], [SoLuongTonKho], [DonGiaBanGoc], [DonGiaBanRa], [KhuyenMai], [DanhBa], [DenFlash], [CongNgheManHinh], [DoSangToiDa], [LoaiPin], [BaoMatNangCao], [GhiAmMacDinh], [JackTaiNghe], [MangDiDong], [Sim], [MaHang], [ManHinh], [Pin], [Camera], [KichThuoc], [Chip], [RAM], [AnhDaiDien]) VALUES (N'xm005', N'Xiaomi Note 13', 12, 60, CAST(5290000.00 AS Decimal(18, 2)), CAST(5554500.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), N'Có', N'Có', N'AMOLED', 1200, N'Li-Po', N'Có', N'Có', N'3.5mm', N'5G', N'Nano SIM', N'xm', N'6.67 inch', N'5000 mAh', N'108 MP', N'158.9 x 74.4 x 8.1 mm', N'MediaTek Dimensity 1080', N'8GB', N'xiaomi_note13.jpg')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'dtkiet', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'kietkid4', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user01', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'admin')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user02', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'admin')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user03', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'admin')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user04', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'admin')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user05', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'admin')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user06', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user07', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user08', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user09', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user10', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user11', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user12', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user13', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user14', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user15', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user16', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user17', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user18', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user19', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [LoaiTaiKhoan]) VALUES (N'user20', N'aS9DS8pleNo1wO/10KU/yYjT3nMnNfkJXFAwKAchA7c=', N'customer')
GO
SET IDENTITY_INSERT [dbo].[TinNhan] ON 
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (6, N'hello', CAST(N'2024-11-17T02:18:19.490' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (7, N'bạn có thể giúp tôi được không', CAST(N'2024-11-17T02:18:31.897' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (8, N'lần 2', CAST(N'2024-11-17T02:42:29.073' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (9, N'phải ok', CAST(N'2024-11-17T02:42:35.927' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (10, N'lần 3 nè', CAST(N'2024-11-17T02:46:24.083' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (11, N'lần 4', CAST(N'2024-11-17T02:49:48.177' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (12, N'lần 5', CAST(N'2024-11-17T02:54:18.333' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (13, N'lần 6', CAST(N'2024-11-17T02:58:18.307' AS DateTime), 1, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (14, N'lần 7', CAST(N'2024-11-17T03:00:41.247' AS DateTime), 0, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (15, N'lần 8', CAST(N'2024-11-17T03:03:35.843' AS DateTime), 0, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (16, N'lần 9 rồi nè', CAST(N'2024-11-17T15:20:18.060' AS DateTime), 0, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (17, N'lần 10', CAST(N'2024-11-17T15:33:59.023' AS DateTime), 0, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (18, N'ok rồi nhé', CAST(N'2024-11-17T15:34:19.117' AS DateTime), 0, N'KH011', N'admin')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (19, N'hahahah', CAST(N'2024-11-17T15:34:22.870' AS DateTime), 0, N'KH011', N'admin')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (20, N'bạn cần trợ giúp gì không ạ', CAST(N'2024-11-17T15:38:21.150' AS DateTime), 0, N'KH011', N'admin')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (21, N'không cần nữa', CAST(N'2024-11-17T15:38:47.773' AS DateTime), 0, N'KH011', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (22, N'hello', CAST(N'2024-11-20T10:47:17.890' AS DateTime), 0, N'FiTCs', N'customer')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (23, N'ok', CAST(N'2024-11-20T10:47:45.517' AS DateTime), 0, N'FiTCs', N'admin')
GO
INSERT [dbo].[TinNhan] ([MaTinNhan], [NoiDung], [ThoiGian], [TrangThai], [MaKhachHang], [LoaiNguoiGui]) VALUES (24, N'hello', CAST(N'2024-11-20T22:03:45.113' AS DateTime), 0, N'KH012', N'customer')
GO
SET IDENTITY_INSERT [dbo].[TinNhan] OFF
GO
ALTER TABLE [dbo].[TinNhan] ADD  CONSTRAINT [DF__TinNhan__ThoiGia__07C12930]  DEFAULT (getdate()) FOR [ThoiGian]
GO
ALTER TABLE [dbo].[TinNhan] ADD  CONSTRAINT [DF__TinNhan__TrangTh__08B54D69]  DEFAULT ((0)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[AnhSanPham]  WITH CHECK ADD  CONSTRAINT [FK_AnhSanPham_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AnhSanPham] CHECK CONSTRAINT [FK_AnhSanPham_SanPham]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietGioHang_GioHang] FOREIGN KEY([MaGioHang])
REFERENCES [dbo].[GioHang] ([MaGioHang])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_ChiTietGioHang_GioHang]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietGioHang_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_ChiTietGioHang_SanPham]
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDonBan_HoaDonBan] FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDonBan] ([MaHoaDon])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan] CHECK CONSTRAINT [FK_ChiTietHoaDonBan_HoaDonBan]
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDonBan_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDonBan] CHECK CONSTRAINT [FK_ChiTietHoaDonBan_SanPham]
GO
ALTER TABLE [dbo].[DanhGia]  WITH CHECK ADD  CONSTRAINT [FK_DanhGia_TaiKhoan] FOREIGN KEY([TenDangNhap])
REFERENCES [dbo].[TaiKhoan] ([TenDangNhap])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DanhGia] CHECK CONSTRAINT [FK_DanhGia_TaiKhoan]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang_TaiKhoan] FOREIGN KEY([TenDangNhap])
REFERENCES [dbo].[TaiKhoan] ([TenDangNhap])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GioHang_TaiKhoan]
GO
ALTER TABLE [dbo].[HoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonBan_KhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[HoaDonBan] CHECK CONSTRAINT [FK_HoaDonBan_KhachHang]
GO
ALTER TABLE [dbo].[HoaDonBan]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonBan_NhanVien] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[HoaDonBan] CHECK CONSTRAINT [FK_HoaDonBan_NhanVien]
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_TaiKhoan] FOREIGN KEY([TenDangNhap])
REFERENCES [dbo].[TaiKhoan] ([TenDangNhap])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_TaiKhoan]
GO
ALTER TABLE [dbo].[LichSuHoatDong]  WITH CHECK ADD  CONSTRAINT [FK_LichSuHoatDong_TaiKhoan] FOREIGN KEY([TenDangNhap])
REFERENCES [dbo].[TaiKhoan] ([TenDangNhap])
GO
ALTER TABLE [dbo].[LichSuHoatDong] CHECK CONSTRAINT [FK_LichSuHoatDong_TaiKhoan]
GO
ALTER TABLE [dbo].[MauSac]  WITH CHECK ADD  CONSTRAINT [FK_MauSac_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MauSac] CHECK CONSTRAINT [FK_MauSac_SanPham]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_TaiKhoan] FOREIGN KEY([TenDangNhap])
REFERENCES [dbo].[TaiKhoan] ([TenDangNhap])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_TaiKhoan]
GO
ALTER TABLE [dbo].[ROM]  WITH CHECK ADD  CONSTRAINT [FK_ROM_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ROM] CHECK CONSTRAINT [FK_ROM_SanPham]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_Hang] FOREIGN KEY([MaHang])
REFERENCES [dbo].[Hang] ([MaHang])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_Hang]
GO
ALTER TABLE [dbo].[TinNhan]  WITH CHECK ADD  CONSTRAINT [FK__TinNhan__MaKhach__09A971A2] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[TinNhan] CHECK CONSTRAINT [FK__TinNhan__MaKhach__09A971A2]
GO
/****** Object:  Trigger [dbo].[theoDoiCustomerGioHang]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[theoDoiCustomerGioHang]
ON [dbo].[ChiTietGioHang]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @LoaiTaiKhoan NVARCHAR(50), @TenDangNhap NVARCHAR(100);
    
    -- Chuyển đổi giá trị SESSION_CONTEXT từ sql_variant sang nvarchar
    SET @LoaiTaiKhoan = CONVERT(NVARCHAR(50), SESSION_CONTEXT(N'LoaiTaiKhoan'));
    SET @TenDangNhap = CONVERT(NVARCHAR(100), SESSION_CONTEXT(N'TenDangNhap'));

    IF @LoaiTaiKhoan = 'customer'
    BEGIN
        -- Thêm vào lịch sử hoạt động
        IF EXISTS (SELECT * FROM inserted)
        BEGIN
            INSERT INTO LichSuHoatDong (MaHoatDong, LoaiHoatDong, ThoiGianThucHien, GhiChu, TenDangNhap)
            VALUES (NEWID(), 'Thêm hoặc Cập nhật Giỏ hàng', GETDATE(), 'Customer đã thêm hoặc cập nhật sản phẩm trong giỏ hàng', @TenDangNhap);
        END
        IF EXISTS (SELECT * FROM deleted)
        BEGIN
            INSERT INTO LichSuHoatDong (MaHoatDong, LoaiHoatDong, ThoiGianThucHien, GhiChu, TenDangNhap)
            VALUES (NEWID(), 'Xóa khỏi Giỏ hàng', GETDATE(), 'Customer đã xóa sản phẩm khỏi giỏ hàng', @TenDangNhap);
        END
    END
END;
GO
ALTER TABLE [dbo].[ChiTietGioHang] ENABLE TRIGGER [theoDoiCustomerGioHang]
GO
/****** Object:  Trigger [dbo].[theoDoiCustomerDanhGia]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[theoDoiCustomerDanhGia]
ON [dbo].[DanhGia]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @LoaiTaiKhoan NVARCHAR(50), @TenDangNhap NVARCHAR(100);
    
    -- Lấy loại tài khoản và tên đăng nhập từ SESSION_CONTEXT và chuyển đổi kiểu dữ liệu
    SET @LoaiTaiKhoan = CONVERT(NVARCHAR(50), SESSION_CONTEXT(N'LoaiTaiKhoan'));
    SET @TenDangNhap = CONVERT(NVARCHAR(100), SESSION_CONTEXT(N'TenDangNhap'));

    IF @LoaiTaiKhoan = 'customer'
    BEGIN
        -- Thêm vào lịch sử hoạt động
        IF EXISTS (SELECT * FROM inserted)
        BEGIN
            INSERT INTO LichSuHoatDong (MaHoatDong, LoaiHoatDong, ThoiGianThucHien, GhiChu, TenDangNhap)
            VALUES (NEWID(), 'Thêm hoặc Cập nhật Đánh giá', GETDATE(), 'Customer đã thêm hoặc cập nhật đánh giá', @TenDangNhap);
        END
        IF EXISTS (SELECT * FROM deleted)
        BEGIN
            INSERT INTO LichSuHoatDong (MaHoatDong, LoaiHoatDong, ThoiGianThucHien, GhiChu, TenDangNhap)
            VALUES (NEWID(), 'Xóa Đánh giá', GETDATE(), 'Customer đã xóa đánh giá', @TenDangNhap);
        END
    END
END;
GO
ALTER TABLE [dbo].[DanhGia] ENABLE TRIGGER [theoDoiCustomerDanhGia]
GO
/****** Object:  Trigger [dbo].[theoDoiCustomerMuaSanPham]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[theoDoiCustomerMuaSanPham]
ON [dbo].[HoaDonBan]
AFTER INSERT
AS
BEGIN
    DECLARE @LoaiTaiKhoan NVARCHAR(50), @TenDangNhap NVARCHAR(100), @MaHoaDon NVARCHAR(50);
    
    -- Lấy loại tài khoản và tên đăng nhập từ SESSION_CONTEXT và chuyển đổi kiểu dữ liệu
    SET @LoaiTaiKhoan = CONVERT(NVARCHAR(50), SESSION_CONTEXT(N'LoaiTaiKhoan'));
    SET @TenDangNhap = CONVERT(NVARCHAR(100), SESSION_CONTEXT(N'TenDangNhap'));

    IF @LoaiTaiKhoan = 'customer'
    BEGIN
        -- Lấy mã hóa đơn từ bảng inserted
        SELECT @MaHoaDon = MaHoaDon FROM inserted;

        -- Ghi vào bảng LichSuHoatDong với chi tiết về hóa đơn mới
        INSERT INTO LichSuHoatDong (MaHoatDong, LoaiHoatDong, ThoiGianThucHien, GhiChu, TenDangNhap)
        VALUES (NEWID(), 'Mua Sản phẩm', GETDATE(), CONCAT('Customer đã mua sản phẩm với Mã hóa đơn: ', @MaHoaDon), @TenDangNhap);
    END
END;
GO
ALTER TABLE [dbo].[HoaDonBan] ENABLE TRIGGER [theoDoiCustomerMuaSanPham]
GO
/****** Object:  Trigger [dbo].[theoDoiAdminSanPham]    Script Date: 11/20/2024 10:17:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[theoDoiAdminSanPham]
ON [dbo].[SanPham]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @LoaiTaiKhoan NVARCHAR(50), @TenDangNhap NVARCHAR(100);
    
    -- Chuyển đổi giá trị SESSION_CONTEXT từ sql_variant sang nvarchar
    SET @LoaiTaiKhoan = CONVERT(NVARCHAR(50), SESSION_CONTEXT(N'LoaiTaiKhoan'));
    SET @TenDangNhap = CONVERT(NVARCHAR(100), SESSION_CONTEXT(N'TenDangNhap'));

    IF @LoaiTaiKhoan = 'admin'
    BEGIN
        -- Thêm vào lịch sử hoạt động
        IF EXISTS (SELECT * FROM inserted)
        BEGIN
            INSERT INTO LichSuHoatDong (MaHoatDong, LoaiHoatDong, ThoiGianThucHien, GhiChu, TenDangNhap)
            VALUES (NEWID(), 'Thêm hoặc Cập nhật Sản phẩm', GETDATE(), 'Admin đã thêm hoặc cập nhật sản phẩm', @TenDangNhap);
        END
        IF EXISTS (SELECT * FROM deleted)
        BEGIN
            INSERT INTO LichSuHoatDong (MaHoatDong, LoaiHoatDong, ThoiGianThucHien, GhiChu, TenDangNhap)
            VALUES (NEWID(), 'Xóa Sản phẩm', GETDATE(), 'Admin đã xóa sản phẩm', @TenDangNhap);
        END
    END
END;
GO
ALTER TABLE [dbo].[SanPham] ENABLE TRIGGER [theoDoiAdminSanPham]
GO
