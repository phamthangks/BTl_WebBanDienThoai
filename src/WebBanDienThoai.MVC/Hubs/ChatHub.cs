using BTLW_BDT.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTLW_BDT.Hubs
{
    public class ChatHub : Hub
    {
        private readonly BtlLtwQlbdtContext _context;

        public ChatHub(BtlLtwQlbdtContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string message, string tenDangNhap)
        {
            try
            {
                // Tìm khách hàng thông qua TenDangNhap của TaiKhoan
                var khachHang = await _context.KhachHangs
                    .FirstOrDefaultAsync(kh => kh.TenDangNhapNavigation.TenDangNhap == tenDangNhap);

                if (khachHang == null)
                {
                    throw new Exception("Không tìm thấy thông tin khách hàng");
                }

                var tinNhan = new TinNhan
                {
                    NoiDung = message,
                    ThoiGian = DateTime.Now,
                    TrangThai = false,
                    MaKhachHang = khachHang.MaKhachHang,
                    LoaiNguoiGui = "customer"
                };

                _context.TinNhans.Add(tinNhan);
                await _context.SaveChangesAsync();

                // Gửi tin nhắn đến client
                await Clients.All.SendAsync("ReceiveMessage", tinNhan.LoaiNguoiGui, message, tinNhan.ThoiGian);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendMessage: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task LoadMessages(string tenDangNhap)
        {
            try
            {
                Console.WriteLine($"Loading messages for: {tenDangNhap}");
                
                var khachHang = await _context.KhachHangs
                    .Include(kh => kh.TenDangNhapNavigation)
                    .FirstOrDefaultAsync(kh => kh.TenDangNhapNavigation.TenDangNhap == tenDangNhap);

                if (khachHang != null)
                {
                    var messages = await _context.TinNhans
                        .Where(t => t.MaKhachHang == khachHang.MaKhachHang)
                        .OrderBy(t => t.ThoiGian)
                        .Select(t => new
                        {
                            t.NoiDung,
                            t.ThoiGian,
                            t.LoaiNguoiGui
                        })
                        .ToListAsync();

                    Console.WriteLine($"Found {messages.Count} messages");
                    await Clients.Caller.SendAsync("LoadMessageHistory", messages);
                }
                else
                {
                    Console.WriteLine("Customer not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadMessages: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task MarkAsRead(string tenDangNhap)
        {
            try
            {
                var khachHang = await _context.KhachHangs
                    .FirstOrDefaultAsync(kh => kh.TenDangNhapNavigation.TenDangNhap == tenDangNhap);

                if (khachHang != null)
                {
                    // Cập nhật trạng thái đã đọc cho tất cả tin nhắn của khách hàng
                    var unreadMessages = await _context.TinNhans
                        .Where(t => t.MaKhachHang == khachHang.MaKhachHang && !t.TrangThai.Value)
                        .ToListAsync();

                    foreach (var message in unreadMessages)
                    {
                        message.TrangThai = true;
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MarkAsRead: {ex.Message}");
                throw;
            }
        }

        public async Task SendMessageAsAdmin(string message, string tenDangNhap)
        {
            try
            {
                var khachHang = await _context.KhachHangs
                    .FirstOrDefaultAsync(kh => kh.TenDangNhapNavigation.TenDangNhap == tenDangNhap);

                if (khachHang == null)
                {
                    throw new Exception("Không tìm thấy thông tin khách hàng");
                }

                var tinNhan = new TinNhan
                {
                    NoiDung = message,
                    ThoiGian = DateTime.Now,
                    TrangThai = false,
                    MaKhachHang = khachHang.MaKhachHang,
                    LoaiNguoiGui = "admin"
                };

                _context.TinNhans.Add(tinNhan);
                await _context.SaveChangesAsync();

                // Gửi tin nhắn đến client
                await Clients.All.SendAsync("ReceiveMessage", tinNhan.LoaiNguoiGui, message, tinNhan.ThoiGian);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendMessageAsAdmin: {ex.Message}");
                throw;
            }
        }
    }
} 