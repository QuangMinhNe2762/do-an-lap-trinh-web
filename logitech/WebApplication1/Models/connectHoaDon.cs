using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class connectHoaDon
    {
        public string url_cn_sql = @"Data Source=DESKTOP-3CBUSTO\SQLEXPRESS;Initial Catalog=QL_CH_LogiTech2;Integrated Security=True";
        public List<HoaDon> getData()
        {
            List<HoaDon> list = new List<HoaDon>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "SELECT t1.MAHOADON,t1.MAUSERNAME,t1.NGAYDATHANG,t1.TONGTIEN,t2.MASP,t2.SOLUONGMUA,t2.DONGIA,t2.THANHTIEN FROM HOADON t1 INNER JOIN CHITIETHOADON t2 ON t1.MAHOADON = t2.MAHOADON";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                HoaDon hd = new HoaDon();
                hd.maHD = rdr.GetValue(0).ToString();
                hd.maUSerName = rdr.GetValue(1).ToString();
                hd.ngayDat = Convert.ToDateTime(rdr.GetValue(2).ToString());
                hd.tongTien = Convert.ToInt32(rdr.GetValue(3).ToString());
                hd.masp = rdr.GetValue(4).ToString();
                hd.soluong = Convert.ToInt32(rdr.GetValue(5).ToString());
                hd.dongia = Convert.ToInt32(rdr.GetValue(6).ToString());
                hd.thanhtien = Convert.ToInt32(rdr.GetValue(7).ToString());
                list.Add(hd);
            }
            rdr.Close();
            con.Close();
            return list;
        }
        //hóa đơn đang đc xử lý cho sản phẩm mua
        static HoaDon hdmake = new HoaDon();
        public HoaDon getSP()
        {
            return hdmake;
        }
        public void setSP(sanPham n)
        {
            hdmake.masp = n.maSP;
            hdmake.soluong = n.soluong;
            hdmake.dongia = n.giaSP;
            hdmake.thanhtien = n.thanhTien;
        }
        public void buyProduct(HoaDon a, List<Account> list)
        {

            hdmake.tenNguoiNhan = a.tenNguoiNhan;
            hdmake.diaChi = a.diaChi;
            hdmake.sdt = a.sdt;
            hdmake.email = a.email;
            hdmake.maHD = "HD00" + (getData().Count + 1);
            hdmake.ngayDat = DateTime.Now.Date;
            hdmake.tongTien = hdmake.thanhtien;
            hdmake.maUSerName = "AC00" + (list.Count + 1);
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "insert into ACCOUNT(MAUSERNAME,EMAIL,SODT,TENUSER,DIACHI) values('" + hdmake.maUSerName + "','" + hdmake.email + "','" + hdmake.sdt + "',N'" + hdmake.tenNguoiNhan + "',N'" + hdmake.diaChi + "')";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmdSQL = "insert into HOADON values('" + hdmake.maHD + "','" + hdmake.maUSerName + "','" + hdmake.ngayDat + "'," + hdmake.tongTien + ")";
            cmd = new SqlCommand(cmdSQL, con);
            cmd.ExecuteNonQuery();
            cmdSQL = "insert into CHITIETHOADON values('" + hdmake.maHD + "','" + hdmake.masp + "'," + hdmake.soluong + "," + hdmake.dongia + "," + hdmake.thanhtien + ")";
            cmd = new SqlCommand(cmdSQL, con);
            cmd.ExecuteNonQuery();
            cmdSQL = "exec capnhatSLSP '" + hdmake.masp + "'," + hdmake.soluong + "";
            cmd = new SqlCommand(cmdSQL, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void buyProductDaDN(HoaDon a)
        {
            hdmake.maUSerName = a.maUSerName;
            hdmake.diaChi = a.diaChi;
            hdmake.sdt = a.sdt;
            hdmake.email = a.email;
            hdmake.maHD = "HD00" + (getData().Count + 1);
            hdmake.ngayDat = DateTime.Now.Date;
            hdmake.tongTien = hdmake.thanhtien;
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "insert into HOADON values('" + hdmake.maHD + "','" + hdmake.maUSerName + "','" + hdmake.ngayDat + "'," + hdmake.tongTien + ")";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmdSQL = "insert into CHITIETHOADON values( '" + hdmake.maHD + "','" + hdmake.masp + "'," + hdmake.soluong + "," + hdmake.dongia + "," + hdmake.thanhtien + ")";
            cmd = new SqlCommand(cmdSQL, con);
            cmd.ExecuteNonQuery();
            cmdSQL = "exec capnhatSLSP '" + hdmake.masp + "'," + hdmake.soluong + "";
            cmd = new SqlCommand(cmdSQL, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<HoaDon> getHDMUserName(string maUsername)
        {
            List<HoaDon> list = new List<HoaDon>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "SELECT t1.MAHOADON,t1.MAUSERNAME,t1.NGAYDATHANG,t1.TONGTIEN,t2.MASP,t2.SOLUONGMUA,t2.DONGIA,t2.THANHTIEN FROM HOADON t1 INNER JOIN CHITIETHOADON t2 ON t1.MAHOADON = t2.MAHOADON where MAUSERNAME='" + maUsername + "'";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                HoaDon hd = new HoaDon();
                hd.maHD = rdr.GetValue(0).ToString();
                hd.maUSerName = rdr.GetValue(1).ToString();
                hd.ngayDat = Convert.ToDateTime(rdr.GetValue(2).ToString());
                hd.tongTien = Convert.ToInt32(rdr.GetValue(3).ToString());
                hd.masp = rdr.GetValue(4).ToString();
                hd.soluong = Convert.ToInt32(rdr.GetValue(5).ToString());
                hd.dongia = Convert.ToInt32(rdr.GetValue(6).ToString());
                hd.thanhtien = Convert.ToInt32(rdr.GetValue(7).ToString());
                list.Add(hd);
            }
            rdr.Close();
            con.Close();
            return list;
        }
        public List<chiTietHoaDon> getSPMaHD(string mahd)
        {
            List<chiTietHoaDon> list = new List<chiTietHoaDon>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "select MAHOADON,SANPHAM.MASP,TENSP,ANH,SOLUONG,SANPHAM.DONGIA,THANHTIEN from chitiethoadon,SANPHAM where MAHOADON='" + mahd + "' and SANPHAM.MASP=CHITIETHOADON.MASP";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                chiTietHoaDon cthd = new chiTietHoaDon();
                cthd.mahd = rdr.GetValue(0).ToString();
                cthd.masp = rdr.GetValue(1).ToString();
                cthd.tensp = rdr.GetValue(2).ToString();
                cthd.anhsp = rdr.GetValue(3).ToString();
                cthd.soluong = Convert.ToInt32(rdr.GetValue(4).ToString());
                cthd.dongia = Convert.ToInt32(rdr.GetValue(5).ToString());
                cthd.thanhtien = Convert.ToInt32(rdr.GetValue(6).ToString());
                list.Add(cthd);
            }
            rdr.Close();
            con.Close();
            return list;
        }
    }

}