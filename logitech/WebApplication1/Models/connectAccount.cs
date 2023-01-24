using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class connectAccount
    {
        public string url_cn_sql = @"Data Source=DESKTOP-3CBUSTO\SQLEXPRESS;Initial Catalog=QL_CH_LogiTech2;Integrated Security=True";
        //public string url_cn_sql = "Data Source=A107PC32;Initial Catalog=QL_CH_LogiTech;Integrated Security=True";
        public List<Account> getData()
        {
            List<Account> list = new List<Account>();
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "SELECT * from ACCOUNT";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Account ac = new Account();
                ac.MUserName = rdr.GetValue(0).ToString();
                ac.userName = rdr.GetValue(1).ToString();
                ac.Email = rdr.GetValue(2).ToString();
                ac.sdt = rdr.GetValue(3).ToString();
                ac.PassWord = rdr.GetValue(4).ToString();
                ac.tenUser = rdr.GetValue(5).ToString();
                ac.avatar = rdr.GetValue(6).ToString();
                ac.role = rdr.GetValue(7).ToString();
                ac.gioitinh = rdr.GetValue(8).ToString();
                ac.diaChi = rdr.GetValue(9).ToString();
                list.Add(ac);
            }
            rdr.Close();
            con.Close();
            return list;
        }
        /// <summary>
        /// biến này dùng để check xem user có đăng nhập hay không ?
        /// </summary>
        static Account sa = new Account();
        public void insertSaveRegister(Account n)
        {
            if (n.RuserName != null)
            {
                sa.userName = n.RuserName;
                sa.PassWord = n.RpassWord;
                sa.Email = n.Remail;
            }
            else
            {
                sa.tenUser = n.tenUser;
                sa.sdt = n.sdt;
                sa.avatar = n.avatar;
                sa.gioitinh = n.gioitinh;
                sa.diaChi = n.diaChi;
                InsertAc();
            }
        }
        public void checklogin(Account n)
        {
            if (n != null)
            {
                SqlConnection con = new SqlConnection(url_cn_sql);
                string cmdSQL = "select * from ACCOUNT where USERNAME='" + n.userName + "'";
                SqlCommand cmd = new SqlCommand(cmdSQL, con);
                con.Open();
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                //lấy dữ liệu của user muốn đăng nhập
                rdr.Read();
                sa.MUserName = rdr.GetValue(0).ToString();
                sa.userName = rdr.GetValue(1).ToString();
                sa.Email = rdr.GetValue(2).ToString();
                sa.sdt = rdr.GetValue(3).ToString();
                sa.PassWord = rdr.GetValue(4).ToString();
                sa.tenUser = rdr.GetValue(5).ToString();
                sa.avatar = rdr.GetValue(6).ToString();
                sa.role = rdr.GetValue(7).ToString();
                sa.gioitinh = rdr.GetValue(8).ToString();
                sa.diaChi = rdr.GetValue(9).ToString();
            }
        }
        public Account saveUserLogin()
        {
            return sa;
        }
        public bool checkTenUser(Account a)
        {
            List<Account> list = getData();
            foreach (Account item in list)
            {
                if (string.Equals(item.tenUser, a.tenUser, StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
        public string cnLogin()
        {
            if (sa.tenUser == null)
            {
                return null;
            }
            return "đã vào";
        }
        //thêm account register vào database
        private void InsertAc()
        {
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL;
            sa.role = "user";
            sa.MUserName = "AC00" + (getData().Count + 1);
            if (sa.avatar == null)
            {
                sa.avatar = "1.png";
                cmdSQL = "INSERT INTO ACCOUNT(MAUSERNAME,USERNAME, EMAIL,SODT,PS,TENUSER,ANHDAIDIEN,QUYEN,GIOITINH,DIACHI) VALUES('" + sa.MUserName + "','" + sa.userName + "', '" + sa.Email + "', '" + sa.sdt + "', '" + sa.PassWord + "', N'" + sa.tenUser + "','" + sa.avatar + "','" + sa.role + "','" + sa.gioitinh + "',N'" + sa.diaChi + "')";
            }
            else
            {
                cmdSQL = "INSERT INTO ACCOUNT(MAUSERNAME,USERNAME, EMAIL,SODT,PS,TENUSER,ANHDAIDIEN,QUYEN,GIOITINH,DIACHI) VALUES('" + sa.MUserName + "','" + sa.userName + "', '" + sa.Email + "', '" + sa.sdt + "', '" + sa.PassWord + "', N'" + sa.tenUser + "',N'" + sa.avatar + "','" + sa.role + "','" + sa.gioitinh + "',N'" + sa.diaChi + "')";
            }
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //kiểm tra đăng xuất
        public void checklogout()
        {
            Account temp = new Account();
            sa = temp;
        }
        public void updateProfile(Account a)
        {
            SqlConnection con = new SqlConnection(url_cn_sql);
            string cmdSQL = "update ACCOUNT set EMAIL='"+a.Email+"',SODT='"+a.sdt+"',TENUSER='"+a.tenUser+"',ANHDAIDIEN=N'"+a.avatar+"',GIOITINH=N'"+a.gioitinh+"' where MAUSERNAME='"+a.MUserName+"'";
            SqlCommand cmd = new SqlCommand(cmdSQL, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            sa.Email = a.Email;
            sa.sdt = a.sdt;
            sa.tenUser = a.tenUser;
            sa.avatar = a.avatar;
            sa.gioitinh = a.gioitinh;
        }
    }
}