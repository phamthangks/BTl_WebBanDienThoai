using BTLW_BDT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBanDienThoai.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly BtlLtwQlbdtContext _context;

        public ProductAPIController(BtlLtwQlbdtContext context)
        {
            _context = context;
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
