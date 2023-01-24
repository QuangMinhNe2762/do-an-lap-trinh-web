using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //trang chu
        connectSanPham objSP = new connectSanPham();
        connectAccount objAC = new connectAccount();
        connectGioHang objGH = new connectGioHang();
        connectHoaDon objHD = new connectHoaDon();
        static List<sanPham> gioHangChuaDN = new List<sanPham>();
        static List<sanPham> listGetSP_Fr_Gio = new List<sanPham>();
        /// <summary>
        /// kiểm tra thêm sản phẩm vào giỏ hàng chưa đăng nhập
        /// </summary>
        static string checkAdd = null;
        public ActionResult trangChu()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.ListMostUsed = objSP.MostUsed();
            ViewBag.login = objAC.cnLogin();
            if (ViewBag.login != null)
            {
                listGetSP_Fr_Gio = objGH.getSP_Fr_Gio(objAC.saveUserLogin());
            }
            ViewBag.ListFeaturedProDuct = objSP.featuredProDuct();
            List<sanPham> ListLastProducts = objSP.lastedProDuct();
            return View(ListLastProducts);
        }
        /// <summary>
        /// trang Products
        /// </summary>
        /// <returns></returns>
        public ActionResult ProDucts(int? page, int? pagesize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 12;
            }
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            List<sanPham> list = objSP.getData();
            return View(list.ToPagedList((int)page, (int)pagesize));
        }
        [HttpPost]
        public ActionResult ProDucts(int? page, int? pagesize, string txt_Search)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 12;
            }
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            List<sanPham> list = objSP.searchSP(txt_Search);
            return View(list.ToPagedList((int)page, (int)pagesize));
        }

        /// <summary>
        /// trang Products detail
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="loai"></param>
        /// <param name="cost"></param>
        /// <param name="img"></param>
        /// <param name="mota"></param>
        /// <returns></returns>
        public ActionResult ProductDetail(sanPham a)
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            ViewBag.listSPCungLoai = objSP.getSPCungLoai(a.loaiSP);
            int sl = objSP.getData().Single(x => x.maSP == a.maSP).soluong;
            if (sl < a.soluong)
            {
                a.soluong = sl;
                ViewBag.error = "số lượng không đủ";
                return View(a);
            }
            if (a.soluong <= 0)
            {
                ViewBag.error = "vui lòng nhập số lượng phù hợp";
                return View(a);
            }
            if (ViewBag.login == null)
            {
                sanPham checksp = gioHangChuaDN.FirstOrDefault(s => s.maSP == a.maSP);
                if (checksp != null)
                {
                    ViewBag.daThemVaoGioHang = "true";
                    a.soluong = sl;
                    return View(a);
                }
                else
                {
                    a.soluong = sl;
                    return View(a);
                }
            }
            else
            {
                sanPham checksp = listGetSP_Fr_Gio.FirstOrDefault(s => s.maSP == a.maSP);
                if (checksp != null)
                {
                    ViewBag.daThemVaoGioHang = "true";
                    a.soluong = sl;
                }
            }
            return View(a);
        }


        /// <summary>
        /// trang About
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            return View();
        }


        /// <summary>
        /// trang đăng nhập,đăng ký
        /// </summary>
        /// <returns></returns>
        public ActionResult Account()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            return View();
        }
        /// <summary>
        /// đăng nhập user
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Account a)
        {

            int count = 0;
            if (a.userName == null)
            {
                ViewBag.username = "Nhập username";
                count++;
            }
            if (a.PassWord == null)
            {
                ViewBag.password = "Nhập password";
                count++;
            }
            if (count >= 1 && count <= 2)
            {
                return View("Account");
            }
            int error = 0;
            int error1 = 0;
            foreach (Account item in objAC.getData())
            {
                if (a.userName == item.userName && a.PassWord == item.PassWord)
                {
                    objAC.checklogin(a);
                    return RedirectToAction("trangChu", "Home");
                }
                if (a.userName == item.userName && a.PassWord != item.PassWord)
                {
                    error++;
                    if (error == 1)
                    {
                        ViewBag.password = "sai mật khẩu";
                        error = 0;
                        break;
                    }
                }
                if (a.userName != item.userName)
                {
                    if (error1 == objAC.getData().Count - 1)
                    {
                        ViewBag.username = "tài khoản không tồn tại";
                        break;
                    }
                    error1++;
                }
            }
            return View("Account");
        }

        /// <summary>
        /// đăng ký user
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(Account b)
        {
            int count = 0;
            string email = "@gmail.com";
            if (b.RuserName == null)
            {
                ViewBag.Rusername = "Nhập username";
                count++;
            }
            if (b.RpassWord == null)
            {
                ViewBag.Rpassword = "Nhập mật khẩu";
                count++;
            }
            if (b.Remail == null)
            {
                ViewBag.Remail = "Nhập Email";
                count++;
            }
            else
            {
                if (!b.Remail.Contains(email))
                {
                    ViewBag.Remail = "Email lạ lắm";
                    count++;
                }
            }
            if (count >= 1 && count <= 3)
            {
                return View("Account");
            }
            int error = 0;
            foreach (Account item in objAC.getData())
            {
                if (string.Equals(item.userName, b.RuserName, StringComparison.CurrentCultureIgnoreCase))
                {
                    ViewBag.Rusername = "tên tài khoản đã tồn tại";
                    error++;
                    break;
                }
                if (string.Equals(item.PassWord, b.RpassWord, StringComparison.CurrentCultureIgnoreCase))
                {
                    ViewBag.Rpassword = "mật khẩu đã tồn tại";
                    error++;
                    break;
                }
                if (b.Remail == item.Email)
                {
                    ViewBag.Remail = "Email đã được sử dụng";
                    error++;
                    break;
                }
            }
            objAC.insertSaveRegister(b);
            if (error == 0)
            {
                return RedirectToAction("createDetailAc", "Home");
            }
            return View("Account");
        }

        /// <summary>
        /// trang tạo thông tin user
        /// </summary>
        /// <returns></returns>
        public ActionResult createDetailAc()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            return View();
        }


        [HttpPost]
        public ActionResult createDetailAc(Account c, HttpPostedFileBase avatar)
        {

            if (!objAC.checkTenUser(c) && c.sdt.Length != 10)
            {
                ViewBag.tenuser = "tên đã tồn tại";
                ViewBag.sdt = "số điện thoại không tồn tại hoặc đã nhập thiếu số";
                return View("createDetailAc");
            }
            else if (c.sdt.Length != 10)
            {
                ViewBag.sdt = "số điện thoại không tồn tại hoặc đã nhập thiếu số";
                return View("createDetailAc");
            }
            else if (!objAC.checkTenUser(c))
            {
                ViewBag.tenuser = "tên đã tồn tại";
                return View("createDetailAc");
            }
            else
            {
                string path = string.Empty;
                path = Path.Combine(Server.MapPath("~/Content/img/avatar"),avatar.FileName);
                avatar.SaveAs(path);
                c.avatar = avatar.FileName;
                //thêm account register vào table account sql
                objAC.insertSaveRegister(c);
                //check login tài khoản
                objAC.cnLogin();
                return RedirectToAction("trangChu", "Home");
            }
        }


        /// <summary>
        /// trang chi tiết thông tin user
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailProfile()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            return View(objAC.saveUserLogin());
        }



        /// <summary>
        /// nhấn vào nút logout trong trang chi tiết thông tin user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult logout(string name)
        {
            objAC.checklogout();
            return RedirectToAction("trangChu", "Home");
        }


        /// <summary>
        /// trang giỏ hàng
        /// </summary>
        /// <returns></returns>

        public ActionResult pageGioHang()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            if (ViewBag.login == null)
            {
                ViewBag.gioHang = gioHangChuaDN;
                ViewBag.tongTien = tongtienSP();
            }
            else
            {
                Account tkdn = objAC.saveUserLogin();
                ViewBag.gioHang = listGetSP_Fr_Gio;
                ViewBag.tongTien = tongtienSP();
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        public int tongtienSP()
        {
            int tong = 0;
            if (objAC.cnLogin() == null)
            {
                foreach (var item in gioHangChuaDN)
                {
                    tong += item.thanhTien;
                }
            }
            else
            {
                foreach (var item in listGetSP_Fr_Gio)
                {
                    tong += item.thanhTien;
                }
            }
            return tong;
        }
        /// <summary>
        /// thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult add_pro_to_cart(sanPham a)
        {
            if (objSP.getData().Single(x => x.maSP == a.maSP).soluong <= a.soluong)
            {
                return RedirectToAction("ProductDetail", "Home", a);
            }
            if (a.soluong <= 0)
            {
                return RedirectToAction("ProductDetail", "Home", a);
            }
            if (objAC.cnLogin() == null)
            {
                if (checkAdd == null)
                {
                    a.thanhTien = a.soluong * a.giaSP;
                    gioHangChuaDN.Add(a);
                }
            }
            else
            {
                GioHang soluongthem = new GioHang();
                soluongthem.soLuongThem = a.soluong;
                Account tkdn = objAC.saveUserLogin();
                a.thanhTien = a.soluong * a.giaSP;
                objGH.insertProToGioHang(a, tkdn, soluongthem);
                listGetSP_Fr_Gio.Add(a);
            }
            return RedirectToAction("ProductDetail", "Home", a);
        }
        /// <summary>
        /// đánh dấu những sản phẩm đã thêm vào giỏ hàng và không thể thêm đc nữa
        /// hiển thị nút đã thêm vào chi tiết sản phẩm nếu nhấn lần nữa sẽ loại bỏ đã thêm
        /// </summary>
        /// 
        public ActionResult removeAddedFrPageGioHang(sanPham a)
        {
            if (objAC.cnLogin() == null)
            {
                sanPham check = gioHangChuaDN.FirstOrDefault(s => s.maSP == a.maSP);
                gioHangChuaDN.Remove(check);
                checkAdd = null;
            }
            else
            {
                Account tkdn = objAC.saveUserLogin();
                objGH.removeProFrGio(a, tkdn);
                listGetSP_Fr_Gio = objGH.getSP_Fr_Gio(tkdn);
            }
            return RedirectToAction("pageGioHang");
        }
        /// <summary>
        ///  xóa sản phẩm khỏi giỏ hàng chưa đn tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult removePRFrChuaDN(sanPham a)
        {
            if (objAC.cnLogin() == null)
            {
                sanPham check = gioHangChuaDN.FirstOrDefault(s => s.maSP == a.maSP);
                gioHangChuaDN.Remove(check);
            }
            else
            {
                sanPham check = listGetSP_Fr_Gio.FirstOrDefault(s => s.maSP == a.maSP);
                Account tkdn = objAC.saveUserLogin();
                objGH.removeProFrGio(a, tkdn);
                listGetSP_Fr_Gio.Remove(check);
            }
            return RedirectToAction("ProductDetail", a);
        }
        public ActionResult updateThanhTien(sanPham a, int number)
        {
            int thanhtien = a.giaSP * number;
            if (objAC.cnLogin() == null)
            {
                gioHangChuaDN.Find(x => x.maSP == a.maSP).thanhTien = thanhtien;
                gioHangChuaDN.Find(x => x.maSP == a.maSP).soluong = number;
            }
            else
            {
                objGH.updateQtyProFrGio(a.maSP, number);
                listGetSP_Fr_Gio.Find(x => x.maSP == a.maSP).thanhTien = thanhtien;
                listGetSP_Fr_Gio.Find(x => x.maSP == a.maSP).soluong = number;
            }
            return RedirectToAction("pageGioHang", "Home");
        }
        public ActionResult SortType(string type)
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            List<sanPham> list = objSP.getMaSPcungLoai(type);
            objSP.setType(type);
            ViewBag.type = type;
            return View(list);
        }
        public ActionResult SortPrice(string price)
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            List<sanPham> list = objSP.sortPrice(price);
            ViewBag.type = objSP.getType();
            if (ViewBag.type == "")
            {
                ViewBag.type = "All ProDuct";
            }
            return View(list);
        }
        /// <summary>
        /// trang quản lý sản phẩm dành cho admin
        /// </summary>
        /// <returns></returns>
        public ActionResult ProManage()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            List<sanPham> list = objSP.getData();
            return View(list);
        }
        [HttpPost]
        public ActionResult ProManage(string txtsearch)
        {
            ViewBag.admin = objAC.saveUserLogin().tenUser;
            ViewBag.login = objAC.cnLogin();
            List<sanPham> list = objSP.searchSP(txtsearch);
            return View(list);
        }
        /// <summary>
        /// đếm số lần làm không đúng thủ tục nhập sản phẩm
        /// </summary>
        static int count = 0;
        public ActionResult AddProDuct(string name, string gia, string soluong, string img, string loai, string gallery, string mota)
        {

            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            if ((name == null || gia == null || soluong == null || img == null || loai == null || gallery == null || mota == null) && count == 0)
            {
                count++;
                return View();
            }
            if ((name == "" || gia == "" || soluong == "" || img == "" || loai == "" || gallery == "" || mota == "") && count > 0)
            {
                return RedirectToAction("ProManage", "Home");
            }
            if ((name != "" || gia != "" || soluong != "" || img != "" || loai != "" || gallery != "" || mota != "") && count > 0)
            {
                ViewBag.e_name = name;
                ViewBag.e_price = gia;
                ViewBag.e_Quantity = soluong;
                ViewBag.e_imgMain = img;
                ViewBag.e_type = loai;
                ViewBag.e_gallery = gallery;
                ViewBag.e_mota = mota;
            }
            return View();
        }
        /// <summary>
        /// thêm sản phẩm vào cửa hàng
        /// </summary>
        /// <param name="a"></param>
        /// <param name="imgMain"></param>
        /// <param name="multipleIMG"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProDuctHttp(sanPham a, HttpPostedFileBase imgMain, HttpPostedFileWrapper[] multipleIMG)
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            int count = 0;
            string path = string.Empty;
            string name = string.Empty, gia = string.Empty, soluong = string.Empty, img = string.Empty, loai = string.Empty, gallery = string.Empty, mota = string.Empty;
            if (a.tenSP == null)
            {
                count++;
                name = "chưa nhập tên sản phẩm";
            }
            if (a.giaSP == 0)
            {
                count++;
                gia = "chưa nhập giá sản phẩm";
            }
            if (a.soluong == 0)
            {
                count++;
                soluong = "chưa nhập số lượng sản phẩm";
            }
            if (imgMain == null)
            {
                count++;
                img = "thiếu ảnh chính";

            }
            if (a.loaiSP == "none")
            {
                count++;
                loai = "chưa chọn loại sản phẩm";
            }
            if (multipleIMG == null)
            {
                count++;
                gallery = "cần phải đủ 4 ảnh gallery";
            }
            if (a.moTa == null)
            {
                count++;
                mota = "chưa nhập mô tả sản phẩm";
            }
            if (count > 0)
            {
                return RedirectToAction("AddProDuct", "Home", new { @name = name, @gia = gia, @soluong = soluong, @img = img, @loai = loai, @gallery = gallery, @mota = mota });
            }

            string newname = a.tenSP.Replace(' ', '_');
            string extension = Path.GetExtension(imgMain.FileName);
            if (imgMain != null)
            {
                a.anhSP = newname + extension;
                path = Path.Combine(Server.MapPath("~/Content/img/Products"), a.anhSP);
                imgMain.SaveAs(path);
            }
            if (multipleIMG.Length == 4)
            {
                for (int i = 0; i < multipleIMG.Length; i++)
                {
                    string newfilename = "(" + (i + 1) + ")" + newname + extension;
                    path = Path.Combine(Server.MapPath("~/Content/img/gallery"), newfilename);
                    multipleIMG[i].SaveAs(path);
                }
            }
            objSP.AddProduct(a);
            return RedirectToAction("ProManage", "Home");
        }

        /// <summary>
        /// xóa sản phẩm khỏi cửa hàng
        /// </summary>
        public ActionResult DeletePro(sanPham a)
        {
            objSP.deleteProduct(a.maSP);
            return RedirectToAction("ProManage", "Home");
        }
        public ActionResult Edit(sanPham a)
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            return View(a);
        }
        [HttpPost]
        public ActionResult editPro(sanPham a, HttpPostedFileBase imgMain, HttpPostedFileWrapper[] multipleIMG, string masp)
        {
            a.maSP = masp;
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            int count = 0;
            string path = string.Empty;
            string name = string.Empty, gia = string.Empty, soluong = string.Empty, img = string.Empty, loai = string.Empty, gallery = string.Empty, mota = string.Empty;
            if (a.tenSP == null)
            {
                count++;
                name = "chưa nhập tên sản phẩm";
            }
            if (a.giaSP == 0)
            {
                count++;
                gia = "chưa nhập giá sản phẩm";
            }
            if (a.soluong == 0)
            {
                count++;
                soluong = "chưa nhập số lượng sản phẩm";
            }
            if (imgMain == null)
            {
                count++;
                img = "thiếu ảnh chính";

            }
            if (a.loaiSP == "none")
            {
                count++;
                loai = "chưa chọn loại sản phẩm";
            }
            if (multipleIMG == null)
            {
                count++;
                gallery = "cần phải đủ 4 ảnh gallery";
            }
            if (a.moTa == null)
            {
                mota = "chưa nhập mô tả sản phẩm";
            }
            if (count > 0)
            {
                return RedirectToAction("AddProDuct", "Home", new { @name = name, @gia = gia, @soluong = soluong, @img = img, @loai = loai, @gallery = gallery, @mota = mota });
            }

            string newname = a.tenSP.Replace(' ', '_');
            string extension = Path.GetExtension(imgMain.FileName);
            if (imgMain != null)
            {
                a.anhSP = newname + extension;
                path = Path.Combine(Server.MapPath("~/Content/img/Products"), a.anhSP);
                imgMain.SaveAs(path);
            }
            if (multipleIMG.Length == 4)
            {
                for (int i = 0; i < multipleIMG.Length; i++)
                {
                    string newfilename = "(" + (i + 1) + ")" + newname + extension;
                    path = Path.Combine(Server.MapPath("~/Content/img/gallery"), newfilename);
                    multipleIMG[i].SaveAs(path);
                }
            }
            objSP.editProduct(a);
            return RedirectToAction("ProManage", "Home");
        }

        /// <summary>
        /// điền thông tin cho khách hàng chưa đn
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public ActionResult fillInformation(sanPham a)
        {
            objSP.setSP(a);
            if (objAC.cnLogin() != null)
            {
                return RedirectToAction("fillInformationDaDN", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult fillInformation(HoaDon a)
        {
            if (a.tenNguoiNhan == null || a.sdt == null || a.email == null || a.diaChi == null)
            {
                ViewBag.error = "chưa điền đầy đủ thông tin";
                return View();
            }
            objHD.setSP(objSP.getSP());
            objHD.buyProduct(a, objAC.getData());
            sanPham check = gioHangChuaDN.FirstOrDefault(s => s.maSP == objSP.getSP().maSP);
            gioHangChuaDN.Remove(check);
            return RedirectToAction("pageGioHang", "Home");
        }
        /// <summary>
        /// điền thông tin cho khách hàng đã đn
        /// </summary>
        /// <returns></returns>
        public ActionResult fillInformationDaDN()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            return View(objAC.saveUserLogin());
        }
        [HttpPost]
        public ActionResult ganduLieu(Account b)
        {
            HoaDon a = new HoaDon();
            a.maUSerName = b.MUserName;
            a.sdt = b.sdt;
            a.email = b.Email;
            a.diaChi = b.diaChi;
            objHD.setSP(objSP.getSP());
            return RedirectToAction("buyProductDaDN", "Home", a);
        }
        public ActionResult buyProductDaDN(HoaDon a)
        {
            objHD.buyProductDaDN(a);
            sanPham check = listGetSP_Fr_Gio.FirstOrDefault(s => s.maSP == objSP.getSP().maSP);
            listGetSP_Fr_Gio.Remove(check);
            objGH.removeProFrGio(objSP.getSP(), objAC.saveUserLogin());
            return RedirectToAction("pageGioHang", "Home");
        }
        public ActionResult paymentHis()
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            List<HoaDon> list = objHD.getHDMUserName(objAC.saveUserLogin().MUserName);
            return View(list);
        }
        public ActionResult InvoiceDetails(HoaDon a)
        {
            ViewBag.admin = objAC.saveUserLogin().role;
            ViewBag.login = objAC.cnLogin();
            List<chiTietHoaDon> list = objHD.getSPMaHD(a.maHD);
            return View(list);
        }
        [HttpPost]
        public ActionResult updateProFile(Account a, HttpPostedFileBase avatar)
        {
                string path = string.Empty;
            a.avatar = avatar.FileName;
            path = Path.Combine(Server.MapPath("~/Content/img/avatar"), a.avatar);
            avatar.SaveAs(path);
            objAC.updateProfile(a);
            return RedirectToAction("DetailProfile","Home");
        }
    }
}