﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn_WebsiteBanQuanAoTreEm.ViewModel
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Tên đăng nhập không được để trống !")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống !")]

        public string Password { get; set; }
    }
}