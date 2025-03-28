using System;

namespace BTLW_BDT.ViewModels
{
    public class ReviewViewModel
    {
        public string TenDangNhap { get; set; }
        public string MaSanPham { get; set; }
        public int Rate { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGianDanhGia { get; set; }
        public bool IsCurrentUser { get; set; }
    }

    public class ReviewFormViewModel
    {
        public string MaSanPham { get; set; }
        public Models.DanhGium ExistingReview { get; set; }
    }

    public class ReviewCreateModel
    {
        public string MaSanPham { get; set; }
        public int Rate { get; set; }
        public string NoiDung { get; set; }
    }

    public class ReviewUpdateModel : ReviewCreateModel
    {
    }
}
