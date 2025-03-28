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
        BtlLtwQlbdtContext db = new BtlLtwQlbdtContext();
        [HttpGet]
        public IEnumerable<SanPham> GetProducts()
        {
            return db.SanPhams
                     .Include(sp => sp.Roms)
                     .Include(sp => sp.MauSacs)
                     .OrderBy(x => x.TenSanPham)
                     .ToList();
        }

        [HttpGet("GetProductsByID/{maSanPham}")]
        public IActionResult GetProductsByID(string maSanPham)
        {
            var sanPham = db.SanPhams
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
                    var existingProduct = db.SanPhams.Find(sanPham.MaSanPham);
                    if (existingProduct != null)
                    {
                        return BadRequest("Mã sản phẩm đã tồn tại");
                    }

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Thêm sản phẩm
                            db.SanPhams.Add(sanPham);
                            db.SaveChanges();

                            // Thêm ROM nếu có
                            if (sanPham.Roms != null && sanPham.Roms.Any())
                            {
                                foreach (var rom in sanPham.Roms)
                                {
                                    // Kiểm tra nếu đã tồn tại cặp MaSanPham và MaRom
                                    var existingRom = db.Roms
                                        .FirstOrDefault(r => r.MaSanPham == rom.MaSanPham && r.MaRom == rom.MaRom);

                                    if (existingRom == null)
                                    {
                                        // Nếu chưa tồn tại, gán MaSanPham cho ROM và thêm mới
                                        rom.MaSanPham = sanPham.MaSanPham;

                                        Console.WriteLine($"Thêm mới: MaRom: {rom.MaRom}, MaSanPham: {rom.MaSanPham}");
                                        db.Roms.Add(rom);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Đã tồn tại: MaRom: {rom.MaRom}, MaSanPham: {rom.MaSanPham}");
                                    }
                                }

                                db.SaveChanges();
                            }
                            if (sanPham.MauSacs != null && sanPham.MauSacs.Any())
                            {
                                foreach (var ms in sanPham.MauSacs)
                                {
                                    // Kiểm tra nếu đã tồn tại cặp MaSanPham và MaRom
                                    var existingMau = db.MauSacs
                                        .FirstOrDefault(m => m.MaSanPham == ms.MaSanPham && m.MaMau == m.MaMau);

                                    if (existingMau == null)
                                    {
                                        // Nếu chưa tồn tại, gán MaSanPham cho ROM và thêm mới
                                        ms.MaSanPham = sanPham.MaSanPham;

                                        Console.WriteLine($"Thêm mới: MaMau: {ms.MaMau}, MaSanPham: {ms.MaSanPham}");
                                        db.MauSacs.Add(ms);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Đã tồn tại: MaMau: {ms.MaMau}, MaSanPham: {ms.MaSanPham}");
                                    }
                                }

                                db.SaveChanges();
                            }
                            if (sanPham.AnhSanPhams != null && sanPham.AnhSanPhams.Any())
                            {
                                foreach (var anh in sanPham.AnhSanPhams)
                                {
                                    // Kiểm tra nếu đã tồn tại cặp MaSanPham và MaRom
                                    var existingAnh = db.AnhSanPhams
                                        .FirstOrDefault(m => m.MaSanPham == anh.MaSanPham && m.TenFile == anh.TenFile);

                                    if (existingAnh == null)
                                    {
                                        // Nếu chưa tồn tại, gán MaSanPham cho ROM và thêm mới
                                        anh.MaSanPham = sanPham.MaSanPham;

                                        Console.WriteLine($"Thêm mới: MaMau: {anh.TenFile}, MaSanPham: {anh.MaSanPham}");
                                        db.AnhSanPhams.Add(anh);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Đã tồn tại: MaMau: {anh.TenFile}, MaSanPham: {anh.MaSanPham}");
                                    }
                                }

                                db.SaveChanges();
                            }

                            // Thêm lịch sử hoạt động
                            var lastMaHoatDong = db.LichSuHoatDongs
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

                            db.LichSuHoatDongs.Add(lichSu);
                            db.SaveChanges();

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
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var existingProduct = db.SanPhams
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
                            db.Entry(existingProduct).CurrentValues.SetValues(sanPham);

                            if (sanPham.Roms != null)
                            {
                                // Xóa ROMs không còn trong danh sách mới
                                var romsToRemove = existingProduct.Roms
                                    .Where(existingRom => !sanPham.Roms.Any(rom => rom.MaRom == existingRom.MaRom))
                                    .ToList();
                                db.Roms.RemoveRange(romsToRemove);

                                // Thêm ROMs mới
                                foreach (var rom in sanPham.Roms)
                                {
                                    // Kiểm tra nếu chưa có
                                    var existingRom = db.Roms
                                        .FirstOrDefault(r => r.MaSanPham == rom.MaSanPham && r.MaRom == rom.MaRom);
                                    if (existingRom == null)
                                    {
                                        rom.MaSanPham = sanPham.MaSanPham;
                                        db.Roms.Add(rom);
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
                                db.MauSacs.RemoveRange(mausacsToRemove);

                                // Thêm màu sắc mới
                                foreach (var ms in sanPham.MauSacs)
                                {
                                    var existingMau = db.MauSacs
                                        .FirstOrDefault(r => r.MaSanPham == ms.MaSanPham && r.MaMau == ms.MaMau);
                                    if (existingMau == null)
                                    {
                                        ms.MaSanPham = sanPham.MaSanPham;
                                        db.MauSacs.Add(ms);
                                    }
                                }
                            }
                            if (sanPham.AnhSanPhams != null)
                            {
                                // Xóa màu sắc không còn trong danh sách mới
                                var anhToRemove = existingProduct.AnhSanPhams
                                    .Where(existingAnh => !sanPham.AnhSanPhams.Any(ms => ms.TenFile == existingAnh.TenFile))
                                    .ToList();
                                db.AnhSanPhams.RemoveRange(anhToRemove);

                                // Thêm màu sắc mới
                                foreach (var ms in sanPham.AnhSanPhams)
                                {
                                    var existingAnh = db.AnhSanPhams
                                        .FirstOrDefault(r => r.MaSanPham == ms.MaSanPham && r.TenFile == ms.TenFile);
                                    if (existingAnh == null)
                                    {
                                        ms.MaSanPham = sanPham.MaSanPham;
                                        db.AnhSanPhams.Add(ms);
                                    }
                                }
                            }

                            // Thêm lịch sử
                            var lastMaHoatDong = db.LichSuHoatDongs
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

                            db.LichSuHoatDongs.Add(lichSu);
                            db.SaveChanges();

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
            var rom = db.Roms.FirstOrDefault(r => r.MaRom == maRom);
            if (rom == null)
            {
                return NotFound("ROM không tìm thấy.");
            }

            // Xóa ROM
            db.Roms.Remove(db.Roms.Find(maRom));
            db.SaveChanges();

            return Ok(new { message = "ROM đã được xóa." });
        }
        [HttpDelete("{maSanPham}")]
        public IActionResult XoaSanPham(string maSanPham)
        {
            var CTGH = db.ChiTietGioHangs.Where(x => x.MaSanPham == maSanPham).ToList();
            if (CTGH.Any()) db.RemoveRange(CTGH);
            var CTHDB = db.ChiTietHoaDonBans.Where(x => x.MaSanPham == maSanPham).ToList();
            if (CTHDB.Any()) db.RemoveRange(CTHDB);

            var anhSanPhams = db.AnhSanPhams.Where(x => x.MaSanPham == maSanPham);
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);

            var rom = db.Roms.Where(x => x.MaSanPham == maSanPham);
            if (rom.Any()) db.RemoveRange(rom);

            var mausac = db.MauSacs.Where(x => x.MaSanPham == maSanPham);
            if (mausac.Any()) db.RemoveRange(mausac);
            db.Remove(db.SanPhams.Find(maSanPham));
            db.SaveChanges();

            var lastMaHoatDong = db.LichSuHoatDongs
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

            db.LichSuHoatDongs.Add(lichSu);
            db.SaveChanges();

            return Ok(new { message = "Xóa sản phẩm thành công" });

        }
        [HttpPost("SaveHistory")]
        public IActionResult SaveHistory([FromForm] string loaiHoatDong, [FromForm] string ghiChu, [FromForm] string tenDangNhap)
        {
            try
            {
                var lastMaHoatDong = db.LichSuHoatDongs
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

                db.LichSuHoatDongs.Add(lichSu);
                db.SaveChanges();

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


    }
}
