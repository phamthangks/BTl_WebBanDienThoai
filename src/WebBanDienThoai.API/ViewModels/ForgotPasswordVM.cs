﻿using System.ComponentModel.DataAnnotations;

namespace WebBanDienThoai.API.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Email không được bỏ trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
    }
}
