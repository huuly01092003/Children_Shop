using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn_WebsiteBanQuanAoTreEm.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống !")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống !")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được trống !")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận không giống nhau")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email không được để trống !")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại liên hệ không được để trống !")]
        public string Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}