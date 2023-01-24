using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
namespace WebApplication1.Models
{
    public class connectGioHang
    {
        public string url_cn_sql = @"Data Source=DESKTOP-3CBUSTO\SQLEXPRESS;Initial Catalog=QL_CH_LogiTech2;Integrated Security=True";
        string query = string.Empty;
        //thêm sản phẩm vào giỏ hàng đã đăng nhập tk
        public void insertProToGioHang(sanPham a, Account b, GioHang c)
        {
            SqlConnection con = new SqlConnection(url_cn_sql);
            query = "insert into GIOHANG values('" + b.MUserName + "','" + a.maSP + "','" + c.soLuongThem + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public List<sanPham> getSP_Fr_Gio(Account b)
        {
            List<sanPham> listsp = new List<sanPham>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            query = "select GIOHANG.MASP,ANH,TENSP,DONGIA,LOAI,SOLUONGTHEM from SANPHAM,GIOHANG where SANPHAM.MASP=GIOHANG.MASP and MAUSERNAME='" + b.MUserName + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sanPham sp = new sanPham();
                sp.maSP = rdr.GetValue(0).ToString();
                sp.anhSP = rdr.GetValue(1).ToString();
                sp.tenSP = rdr.GetValue(2).ToString();
                sp.giaSP = Convert.ToInt32(rdr.GetValue(3).ToString());
                sp.loaiSP = rdr.GetValue(4).ToString();
                sp.soluong = Convert.ToInt32(rdr.GetValue(5).ToString());
                sp.thanhTien = sp.giaSP * sp.soluong;
                listsp.Add(sp);
            }
            rdr.Close();
            con.Close();
            return listsp;
        }
        public void removeProFrGio(sanPham a, Account b)
        {
            SqlConnection con = new SqlConnection(url_cn_sql);
            query = "delete from GIOHANG where MASP='" + a.maSP + "'and MAUSERNAME='" + b.MUserName + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void updateQtyProFrGio(string masp, int sl)
        {
            SqlConnection con = new SqlConnection(url_cn_sql);
            query = "update giohang set SOLUONGTHEM=" + sl + " where MASP='" + masp + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}