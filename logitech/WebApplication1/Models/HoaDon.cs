using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HoaDon
    {
        public string maHD { get; set; }
        //1
        public string tenNguoiNhan { get; set; }
        public DateTime ngayDat { get; set; }
        //2
        public string diaChi { get; set; }
        public int tongTien { get; set; }
        //sp
        public string masp { get; set; }
        //sp
        public int soluong { get; set; }
        //sp
        public int dongia { get; set; }
        //sp
        public int thanhtien { get; set; }
        //ac
        public string maUSerName { get; set; }
        //3
        public string sdt { get; set; }
        //4
        public string email { get; set; }
    }
}