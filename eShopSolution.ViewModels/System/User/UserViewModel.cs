using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopSolution.ViewModels.System.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [Display(Name ="Tên")]
        public string FisrtName { get; set; }
        [Display(Name ="Họ")]
        public string LastName { get; set; }
        [Display(Name = "Số điện thoaị")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateTime DOB { get; set;
        }
    }
}
