using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Account
    {
        public string MUserName { get; set; }
        public string userName { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }

        public string tenUser { get; set; }

        public string avatar { get; set; }
        public string role { get; set; }
        public string sdt { get; set; }
        public string gioitinh { get; set; }
        public string RuserName { get; set; }
        public string RpassWord { get; set; }
        public string Remail { get; set; }
        public string diaChi { get; set; }
    }
}