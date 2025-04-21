using BTLW_BDT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBanDienThoai.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamAPI : ControllerBase
    {
        private readonly BtlLtwQlbdtContext _context;

        public SanPhamAPI(BtlLtwQlbdtContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<SanPham> GetProducts()
        {
            return _context.SanPhams
                     .Include(sp => sp.Roms)
                     .Include(sp => sp.MauSacs)
                     .OrderBy(x => x.TenSanPham)
                     .ToList();
        }

        [HttpGet("GetProductsByID/{maSanPham}")]
        public IActionResult GetProductsByID(string maSanPham)
        {
            var sanPham = _context.SanPhams
                .Include(sp => sp.Roms)
                .Include(sp => sp.MauSacs)
                .Include(sp => sp.AnhSanPhams)
                .FirstOrDefault(x => x.MaSanPham == maSanPham);

            if (sanPham == null)
                return NotFound($"Không tìm thấy sản phẩm với mã {maSanPham}");

            return Ok(sanPham);
        }

        [HttpPost]
        public IActionResult ThemSanPham([FromBody] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra sản phẩm đã tồn tại chưa
                    var existingProduct = _context.SanPhams.Find(sanPham.MaSanPham);
                    if (existingProduct != null)
                    {
                        return BadRequest("Mã sản phẩm đã tồn tại");
                    }

                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            // Thêm sản phẩm
                            _context.SanPhams.Add(sanPham);
                            _context.SaveChanges();

                            // Thêm ROM nếu có
                            if (sanPham.Roms != null && sanPham.Roms.Any())
                            {
                                foreach (var rom in sanPham.Roms)
                                {
                                    // Kiểm tra nếu đã tồn tại cặp MaSanPham và MaRom
                                    var existingRom = _context.Roms
                                        .FirstOrDefault(r => r.MaSanPham == rom.MaSanPham && r.MaRom == rom.MaRom);

                                    if (existingRom == null)
                                    {
                                        // Nếu chưa tồn tại, gán MaSanPham cho ROM và thêm mới
                                        rom.MaSanPham = sanPham.MaSanPham;

                                        Console.WriteLine($"Thêm mới: MaRom: {rom.MaRom}, MaSanPham: {rom.MaSanPham}");
                                        _context.Roms.Add(rom);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Đã tồn tại: MaRom: {rom.MaRom}, MaSanPham: {rom.MaSanPham}");
                                    }
                                }

                                _context.SaveChanges();
                            }
                            if (sanPham.MauSacs != null && sanPham.MauSacs.Any())
                            {
                                foreach (var ms in sanPham.MauSacs)
                                {
                                    // Kiểm tra nếu đã tồn tại cặp MaSanPham và MaRom
                                    var existingMau = _context.MauSacs
                                        .FirstOrDefault(m => m.MaSanPham == ms.MaSanPham && m.MaMau == m.MaMau);

                                    if (existingMau == null)
                                    {
                                        // Nếu chưa tồn tại, gán MaSanPham cho ROM và thêm mới
                                        ms.MaSanPham = sanPham.MaSanPham;

                                        Console.WriteLine($"Thêm mới: MaMau: {ms.MaMau}, MaSanPham: {ms.MaSanPham}");
                                        _context.MauSacs.Add(ms);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Đã tồn tại: MaMau: {ms.MaMau}, MaSanPham: {ms.MaSanPham}");
                                    }
                                }

                                _context.SaveChanges();
                            }
                            if (sanPham.AnhSanPhams != null && sanPham.AnhSanPhams.Any())
                            {
                                foreach (var anh in sanPham.AnhSanPhams)
                                {
                                    // Kiểm tra nếu đã tồn tại cặp MaSanPham và MaRom
                                    var existingAnh = _context.AnhSanPhams
                                        .FirstOrDefault(m => m.MaSanPham == anh.MaSanPham && m.TenFile == anh.TenFile);

                                    if (existingAnh == null)
                                    {
                                        // Nếu chưa tồn tại, gán MaSanPham cho ROM và thêm mới
                                        anh.MaSanPham = sanPham.MaSanPham;

                                        Console.WriteLine($"Thêm mới: MaMau: {anh.TenFile}, MaSanPham: {anh.MaSanPham}");
                                        _context.AnhSanPhams.Add(anh);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Đã tồn tại: MaMau: {anh.TenFile}, MaSanPham: {anh.MaSanPham}");
                                    }
                                }

                                _context.SaveChanges();
                            }

                            // Thêm lịch sử hoạt động
                            var lastMaHoatDong = _context.LichSuHoatDongs
                                                .OrderByDescending(ls => ls.MaHoatDong)
                                                .Select(ls => ls.MaHoatDong)
                                                .FirstOrDefault();

                            int newCnt = lastMaHoatDong != null ?
                                int.Parse(lastMaHoatDong.Substring(2)) + 1 : 1;
                            var newMaHoatDong = "ls" + newCnt.ToString("D3");

                            var lichSu = new LichSuHoatDong
                            {
                                MaHoatDong = newMaHoatDong,
                                LoaiHoatDong = "Thêm sản phẩm",
                                ThoiGianThucHien = DateTime.Now,
                                GhiChu = null,
                                TenDangNhap = User.Identity?.Name
                            };

                            _context.LichSuHoatDongs.Add(lichSu);
                            _context.SaveChanges();

                            transaction.Commit();
                            return Ok(sanPham);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            var innerMessage = ex.InnerException?.Message ?? "Không có chi tiết lỗi";
                            return StatusCode(500, new
                            {
                                Message = "Lỗi trong transaction",
                                Error = ex.Message,
                                InnerError = innerMessage,
                                StackTrace = ex.StackTrace
                            });
                        }
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }

        [HttpPut("SuaSanPham")]
        public IActionResult SuaSanPham([FromBody] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            var existingProduct = _context.SanPhams
                                .Include(sp => sp.Roms)
                                .Include(sp => sp.MauSacs)
                                .Include(sp => sp.AnhSanPhams)
                                .FirstOrDefault(x => x.MaSanPham == sanPham.MaSanPham);
                            if (existingProduct == null)
                            {
                                Console.WriteLine("Không tìm thấy sản phẩm.");
                            }
                            else
                            {
                                Console.WriteLine($"Tìm thấy sản phẩm: {existingProduct.TenSanPham}");
                                Console.WriteLine($"Số lượng màu sắc: {existingProduct.MauSacs?.Count ?? 0}");
                                foreach (var mau in existingProduct.MauSacs)
                                {
                                    Console.WriteLine($"Màu sắc: {mau.TenMau} ({mau.MaMau})");
                                }
                            }

                            // Cập nhật thông tin sản phẩm
                            _context.Entry(existingProduct).CurrentValues.SetValues(sanPham);

                            if (sanPham.Roms != null)
                            {
                                // Xóa ROMs không còn trong danh sách mới
                                var romsToRemove = existingProduct.Roms
                                    .Where(existingRom => !sanPham.Roms.Any(rom => rom.MaRom == existingRom.MaRom))
                                    .ToList();
                                _context.Roms.RemoveRange(romsToRemove);

                                // Thêm ROMs mới
                                foreach (var rom in sanPham.Roms)
                                {
                                    // Kiểm tra nếu chưa có
                                    var existingRom = _context.Roms
                                        .FirstOrDefault(r => r.MaSanPham == rom.MaSanPham && r.MaRom == rom.MaRom);
                                    if (existingRom == null)
                                    {
                                        rom.MaSanPham = sanPham.MaSanPham;
                                        _context.Roms.Add(rom);
                                    }
                                }
                            }

                            // Cập nhật màu sắc
                            if (sanPham.MauSacs != null)
                            {
                                // Xóa màu sắc không còn trong danh sách mới
                                var mausacsToRemove = existingProduct.MauSacs
                                    .Where(existingMau => !sanPham.MauSacs.Any(ms => ms.MaMau == existingMau.MaMau))
                                    .ToList();
                                _context.MauSacs.RemoveRange(mausacsToRemove);

                                // Thêm màu sắc mới
                                foreach (var ms in sanPham.MauSacs)
                                {
                                    var existingMau = _context.MauSacs
                                        .FirstOrDefault(r => r.MaSanPham == ms.MaSanPham && r.MaMau == ms.MaMau);
                                    if (existingMau == null)
                                    {
                                        ms.MaSanPham = sanPham.MaSanPham;
                                        _context.MauSacs.Add(ms);
                                    }
                                }
                            }
                            if (sanPham.AnhSanPhams != null)
                            {
                                // Xóa màu sắc không còn trong danh sách mới
                                var anhToRemove = existingProduct.AnhSanPhams
                                    .Where(existingAnh => !sanPham.AnhSanPhams.Any(ms => ms.TenFile == existingAnh.TenFile))
                                    .ToList();
                                _context.AnhSanPhams.RemoveRange(anhToRemove);

                                // Thêm màu sắc mới
                                foreach (var ms in sanPham.AnhSanPhams)
                                {
                                    var existingAnh = _context.AnhSanPhams
                                        .FirstOrDefault(r => r.MaSanPham == ms.MaSanPham && r.TenFile == ms.TenFile);
                                    if (existingAnh == null)
                                    {
                                        ms.MaSanPham = sanPham.MaSanPham;
                                        _context.AnhSanPhams.Add(ms);
                                    }
                                }
                            }

                            // Thêm lịch sử
                            var lastMaHoatDong = _context.LichSuHoatDongs
                                .OrderByDescending(ls => ls.MaHoatDong)
                                .Select(ls => ls.MaHoatDong)
                                .FirstOrDefault();

                            int newCnt = lastMaHoatDong != null ?
                                int.Parse(lastMaHoatDong.Substring(2)) + 1 : 1;
                            var newMaHoatDong = "ls" + newCnt.ToString("D3");

                            var lichSu = new LichSuHoatDong
                            {
                                MaHoatDong = newMaHoatDong,
                                LoaiHoatDong = "Sửa sản phẩm",
                                ThoiGianThucHien = DateTime.Now,
                                GhiChu = null,
                                TenDangNhap = User.Identity?.Name
                            };

                            _context.LichSuHoatDongs.Add(lichSu);
                            _context.SaveChanges();

                            transaction.Commit();
                            return Ok(sanPham);
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật sản phẩm: {ex.Message}");
            }
        }
        [HttpDelete("delete-rom/{maRom}")]

        public IActionResult DeleteRom(string maRom)
        {
            if (string.IsNullOrEmpty(maRom))
            {
                return BadRequest("MaROM không hợp lệ.");
            }

            // Tìm ROM theo MaROM trong cơ sở dữ liệu
            var rom = _context.Roms.FirstOrDefault(r => r.MaRom == maRom);
            if (rom == null)
            {
                return NotFound("ROM không tìm thấy.");
            }

            // Xóa ROM
            _context.Roms.Remove(_context.Roms.Find(maRom));
            _context.SaveChanges();

            return Ok(new { message = "ROM đã được xóa." });
        }
        [HttpDelete("{maSanPham}")]
        public IActionResult XoaSanPham(string maSanPham)
        {
            var CTGH = _context.ChiTietGioHangs.Where(x => x.MaSanPham == maSanPham).ToList();
            if (CTGH.Any()) _context.RemoveRange(CTGH);
            var CTH_context = _context.ChiTietHoaDonBans.Where(x => x.MaSanPham == maSanPham).ToList();
            if (CTH_context.Any()) _context.RemoveRange(CTH_context);

            var anhSanPhams = _context.AnhSanPhams.Where(x => x.MaSanPham == maSanPham);
            if (anhSanPhams.Any()) _context.RemoveRange(anhSanPhams);

            var rom = _context.Roms.Where(x => x.MaSanPham == maSanPham);
            if (rom.Any()) _context.RemoveRange(rom);

            var mausac = _context.MauSacs.Where(x => x.MaSanPham == maSanPham);
            if (mausac.Any()) _context.RemoveRange(mausac);
            _context.Remove(_context.SanPhams.Find(maSanPham));
            _context.SaveChanges();

            var lastMaHoatDong = _context.LichSuHoatDongs
                                   .OrderByDescending(ls => ls.MaHoatDong)
                                   .Select(ls => ls.MaHoatDong)
                                   .FirstOrDefault();

            // Tính giá trị mới cho MaHoatDong
            int newCnt = lastMaHoatDong != null ? int.Parse(lastMaHoatDong.Substring(2)) + 1 : 1;
            var newMaHoatDong = "ls" + newCnt.ToString("D3");

            var lichSu = new LichSuHoatDong
            {
                MaHoatDong = newMaHoatDong,
                LoaiHoatDong = "Xóa sản phẩm",
                ThoiGianThucHien = DateTime.Now,
                GhiChu = null,
                TenDangNhap = User.Identity.Name
            };

            _context.LichSuHoatDongs.Add(lichSu);
            _context.SaveChanges();

            return Ok(new { message = "Xóa sản phẩm thành công" });

        }
        [HttpPost("SaveHistory")]
        public IActionResult SaveHistory([FromForm] string loaiHoatDong, [FromForm] string ghiChu, [FromForm] string tenDangNhap)
        {
            try
            {
                var lastMaHoatDong = _context.LichSuHoatDongs
                    .OrderByDescending(ls => ls.MaHoatDong)
                    .Select(ls => ls.MaHoatDong)
                    .FirstOrDefault();

                int newCnt = lastMaHoatDong != null ?
                    int.Parse(lastMaHoatDong.Substring(2)) + 1 : 1;
                var newMaHoatDong = "ls" + newCnt.ToString("D3");

                var lichSu = new LichSuHoatDong
                {
                    MaHoatDong = newMaHoatDong,
                    LoaiHoatDong = loaiHoatDong,
                    ThoiGianThucHien = DateTime.Now,
                    GhiChu = ghiChu,
                    TenDangNhap = tenDangNhap
                };

                _context.LichSuHoatDongs.Add(lichSu);
                _context.SaveChanges();

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


        // GET: api/ProductAPI/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SanPham>> GetProduct(string id)
        {
            try
            {
                var product = await _context.SanPhams
                    .Include(p => p.Roms)
                    .Include(p => p.MauSacs)
                    .Include(p => p.AnhSanPhams)
                    .Include(p => p.ChiTietGioHangs)
                    .Include(p => p.ChiTietHoaDonBans)
                    .FirstOrDefaultAsync(p => p.MaSanPham == id);

                if (product == null)
                {
                    return NotFound($"Không tìm thấy sản phẩm với mã: {id}");
                }

                // Chuyển đổi dữ liệu sang dạng phù hợp để trả về
                var result = new
                {
                    maSanPham = product.MaSanPham,
                    tenSanPham = product.TenSanPham,
                    anhDaiDien = product.AnhDaiDien,
                    thoiGianBaoHanh = product.ThoiGianBaoHanh,
                    soLuongTonKho = product.SoLuongTonKho,
                    donGiaBanGoc = product.DonGiaBanGoc,
                    donGiaBanRa = product.DonGiaBanRa,
                    khuyenMai = product.KhuyenMai,
                    danhBa = product.DanhBa,
                    denFlash = product.DenFlash,
                    congNgheManHinh = product.CongNgheManHinh,
                    doSangToiDa = product.DoSangToiDa,
                    loaiPin = product.LoaiPin,
                    baoMatNangCao = product.BaoMatNangCao,
                    ghiAmMacDinh = product.GhiAmMacDinh,
                    jackTaiNghe = product.JackTaiNghe,
                    mangDiDong = product.MangDiDong,
                    sim = product.Sim,
                    maHang = product.MaHang,
                    manHinh = product.ManHinh,
                    pin = product.Pin,
                    camera = product.Camera,
                    kichThuoc = product.KichThuoc,
                    chip = product.Chip,
                    ram = product.Ram,
                    roms = product.Roms.Select(r => new
                    {
                        maRom = r.MaRom,
                        thongSo = r.ThongSo,
                        gia = r.Gia,
                        maSanPham = r.MaSanPham
                    }).ToList(),
                    mausacs = product.MauSacs.Select(m => new
                    {
                        maMau = m.MaMau,
                        tenMau = m.TenMau,
                        maSanPham = m.MaSanPham
                    }).ToList(),
                    anhSanPhams = product.AnhSanPhams.Select(a => new
                    {
                        tenFile = a.TenFile,
                        maMau = a.MaMau,
                        maSanPham = a.MaSanPham
                    }).ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ProductAPI/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, SanPham sanPham)
        {
            if (id != sanPham.MaSanPham)
            {
                return BadRequest();
            }

            try
            {
                // Xóa các ROM, màu sắc và ảnh cũ
                var existingRoms = await _context.Roms.Where(r => r.MaSanPham == id).ToListAsync();
                var existingMausacs = await _context.MauSacs.Where(m => m.MaSanPham == id).ToListAsync();
                var existingAnhs = await _context.AnhSanPhams.Where(a => a.MaSanPham == id).ToListAsync();

                _context.Roms.RemoveRange(existingRoms);
                _context.MauSacs.RemoveRange(existingMausacs);
                _context.AnhSanPhams.RemoveRange(existingAnhs);

                // Cập nhật thông tin sản phẩm
                _context.Entry(sanPham).State = EntityState.Modified;

                // Thêm ROM, màu sắc và ảnh mới
                if (sanPham.Roms != null)
                    _context.Roms.AddRange(sanPham.Roms);
                if (sanPham.MauSacs != null)
                    _context.MauSacs.AddRange(sanPham.MauSacs);
                if (sanPham.AnhSanPhams != null)
                    _context.AnhSanPhams.AddRange(sanPham.AnhSanPhams);

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        private bool ProductExists(string id)
        {
            return _context.SanPhams.Any(e => e.MaSanPham == id);
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");

                // Đường dẫn tới thư mục lưu ảnh
                string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

                string uploadsFolder = Path.Combine(solutionDirectory, "WebBanDienThoai.MVC", "wwwroot", "PhoneImages", "Images");

                Console.WriteLine($"Upload Folder Path: {uploadsFolder}");
                // Tạo tên file độc nhất
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                // Đường dẫn đầy đủ
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // Lưu file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Ok(new { fileName = uniqueFileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


    }
}
