using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class connectSanPham
    {
        public string url_cn_sql = @"Data Source=DESKTOP-3CBUSTO\SQLEXPRESS;Initial Catalog=QL_CH_LogiTech2;Integrated Security=True";
        //public string url_cn_sql = "Data Source=A107PC32;Initial Catalog=QL_CH_LogiTech;Integrated Security=True";
        static int soluongSP = 0;
        //lấy tất cả sản phẩm
        public List<sanPham> getData()
        {
            List<sanPham> list = new List<sanPham>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "select * from SANPHAM";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sanPham sp = new sanPham();
                sp.maSP = rdr.GetValue(0).ToString();
                sp.tenSP = rdr.GetValue(1).ToString();
                sp.anhSP = rdr.GetValue(2).ToString();
                sp.giaSP = Convert.ToInt32(rdr.GetValue(3).ToString());
                sp.loaiSP = rdr.GetValue(4).ToString();
                sp.moTa = rdr.GetValue(5).ToString();
                sp.soluong = Convert.ToInt32(rdr.GetValue(6).ToString());
                list.Add(sp);
            }
            soluongSP = list.Count() + 1;
            rdr.Close();
            con.Close();
            return list;
        }
        // lấy sản phẩm mostused
        public List<sanPham> MostUsed()
        {
            List<sanPham> ListGetData = getData();
            List<sanPham> ListMostUsed = new List<sanPham>();
            foreach (sanPham item in ListGetData)
            {
                if (item.maSP.Equals("SP0018") || item.maSP.Equals("SP0019") || item.maSP.Equals("SP0020"))
                {
                    ListMostUsed.Add(item);
                }
            }
            return ListMostUsed;
        }
        // lấy sản phẩm featuredProDuct
        public List<sanPham> featuredProDuct()
        {
            List<sanPham> ListGetData = getData();
            List<sanPham> ListfeaturedProDuct = new List<sanPham>();
            foreach (sanPham item in ListGetData)
            {
                if (item.maSP.Equals("SP001") || item.maSP.Equals("SP002") || item.maSP.Equals("SP003") || item.maSP.Equals("SP004"))
                {
                    ListfeaturedProDuct.Add(item);
                }
            }
            return ListfeaturedProDuct;
        }

        public List<sanPham> lastedProDuct()
        {
            List<sanPham> ListGetData = getData();
            List<sanPham> ListLastedProDuct = new List<sanPham>();
            foreach (var item in ListGetData)
            {
                if (item.maSP != "SP001" && item.maSP != "SP002" && item.maSP != "SP003" && item.maSP != "SP004" && item.maSP != "SP0018" && item.maSP != "SP0019" && item.maSP != "SP0020")
                {
                    ListLastedProDuct.Add(item);
                }
            }
            return ListLastedProDuct;
        }
        public List<sanPham> getMaSPcungLoai(string txt_loaiSP)
        {
            List<sanPham> list = new List<sanPham>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "select * from SANPHAM where LOAI LIKE N'" + txt_loaiSP + "'";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sanPham sp = new sanPham();
                sp.maSP = rdr.GetValue(0).ToString();
                sp.tenSP = rdr.GetValue(1).ToString();
                sp.anhSP = rdr.GetValue(2).ToString();
                sp.giaSP = Convert.ToInt32(rdr.GetValue(3).ToString());
                sp.loaiSP = rdr.GetValue(4).ToString();
                sp.moTa = rdr.GetValue(5).ToString();
                sp.soluong = Convert.ToInt32(rdr.GetValue(6).ToString());
                list.Add(sp);
            }
            rdr.Close();
            con.Close();
            return list;
        }
        public List<sanPham> getSPCungLoai(string txt_loaiSP)
        {
            List<sanPham> list = new List<sanPham>();
            List<sanPham> temp = getMaSPcungLoai(txt_loaiSP);
            sanPham[] LMaSPCungLoai;
            LMaSPCungLoai = temp.ToArray();
            Random rd = new Random();
            list = LMaSPCungLoai.OrderBy(x => rd.Next()).Take(4).ToList();
            return list;
        }
        // lấy loại sản phẩm
        public List<string> getLoaiSP()
        {
            List<string> list = new List<string>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "select DISTINCT loai from sanpham";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(rdr.GetValue(0).ToString());
            }
            rdr.Close();
            con.Close();
            return list;
        }
        public List<sanPham> searchSP(string txtsearch)
        {
            List<sanPham> list = new List<sanPham>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "select * from SANPHAM where CONCAT(TENSP, LOAI)like N'%" + txtsearch + "%'";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sanPham sp = new sanPham();
                sp.maSP = rdr.GetValue(0).ToString();
                sp.tenSP = rdr.GetValue(1).ToString();
                sp.anhSP = rdr.GetValue(2).ToString();
                sp.giaSP = Convert.ToInt32(rdr.GetValue(3).ToString());
                sp.loaiSP = rdr.GetValue(4).ToString();
                sp.moTa = rdr.GetValue(5).ToString();
                sp.soluong = Convert.ToInt32(rdr.GetValue(6).ToString());
                list.Add(sp);
            }
            rdr.Close();
            con.Close();
            return list;
        }
        //gán giá trị loại sản phẩm cho type để sắp xếp sản phẩm theo loại sp,giá
        private static string type = string.Empty;
        public void setType(string n)
        {
            type = n;
        }
        public string getType()
        {
            return type;
        }
        public List<sanPham> sortPrice(string price)
        {
            List<sanPham> list = new List<sanPham>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = string.Empty;
            if (price == "increase")
            {
                cmdSQL = "select * from sanpham where loai Like N'%" + type + "%' order by dongia";
            }
            else
            {
                cmdSQL = "select * from sanpham where loai Like N'%" + type + "%' order by dongia desc";
            }
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sanPham sp = new sanPham();
                sp.maSP = rdr.GetValue(0).ToString();
                sp.tenSP = rdr.GetValue(1).ToString();
                sp.anhSP = rdr.GetValue(2).ToString();
                sp.giaSP = Convert.ToInt32(rdr.GetValue(3).ToString());
                sp.loaiSP = rdr.GetValue(4).ToString();
                sp.moTa = rdr.GetValue(5).ToString();
                sp.soluong = Convert.ToInt32(rdr.GetValue(6).ToString());
                list.Add(sp);
            }
            rdr.Close();
            con.Close();
            return list;
        }
        public void AddProduct(sanPham a)
        {
            a.tenSP = a.tenSP.Replace('_', ' ');
            SqlConnection con = new SqlConnection(url_cn_sql);
            a.maSP = "SP00" + (soluongSP + 1);
            string cmdSQL = "insert into SANPHAM values('" + a.maSP + "','" + a.tenSP + "','" + a.anhSP + "'," + a.giaSP + ",N'" + a.loaiSP + "',N'" + a.moTa + "'," + a.soluong + ")";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void deleteProduct(string masp)
        {
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "delete from CHITIETHOADON where MASP='" + masp + "'";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.ExecuteNonQuery();
            cmdSQL = "delete from GIOHANG where MASP='" + masp + "'";
            cmd = new SqlCommand(cmdSQL, con);
            cmd.ExecuteNonQuery();
            cmdSQL = "delete from sanpham where masp like '" + masp + "'";
            cmd = new SqlCommand(cmdSQL, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void editProduct(sanPham a)
        {
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "update sanpham set tensp=N'" + a.tenSP + "',ANH='" + a.anhSP + "',DONGIA=" + a.giaSP + ",LOAI=N'" + a.loaiSP + "',MOTA=N'" + a.moTa + "',SOLUONG=" + a.soluong + " where MASP like '" + a.maSP + "'";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        // sản phẩm đang được mua
        static sanPham a = new sanPham();
        public sanPham getSP()
        {
            return a;
        }
        public void setSP(sanPham b)
        {
            a = b;
        }
    }
}