﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopSolution.ViewModels.System.User
{
    public class RegisterRequest
    {
        [Display(Name ="Tên")]
        public string FisrtName { get; set; }
        [Display(Name = "Họ")]
        public string LastName { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Xác thực mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
