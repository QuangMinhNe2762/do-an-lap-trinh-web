USE [QL_CH_LogiTech2]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 12/28/2022 7:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[MAUSERNAME] [nvarchar](10) NOT NULL,
	[USERNAME] [nvarchar](30) NULL,
	[EMAIL] [nvarchar](30) NOT NULL,
	[SODT] [char](10) NOT NULL,
	[PS] [nvarchar](30) NULL,
	[TENUSER] [nvarchar](50) NULL,
	[ANHDAIDIEN] [nvarchar](max) NULL,
	[QUYEN] [nvarchar](10) NULL,
	[GIOITINH] [nvarchar](3) NULL,
	[DIACHI] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MAUSERNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETHOADON]    Script Date: 12/28/2022 7:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETHOADON](
	[MAHOADON] [nvarchar](10) NOT NULL,
	[MASP] [nvarchar](10) NOT NULL,
	[SOLUONGMUA] [int] NOT NULL,
	[DONGIA] [float] NOT NULL,
	[THANHTIEN] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MAHOADON] ASC,
	[MASP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GIOHANG]    Script Date: 12/28/2022 7:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GIOHANG](
	[MAUSERNAME] [nvarchar](10) NOT NULL,
	[MASP] [nvarchar](10) NOT NULL,
	[SOLUONGTHEM] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MAUSERNAME] ASC,
	[MASP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOADON]    Script Date: 12/28/2022 7:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MAHOADON] [nvarchar](10) NOT NULL,
	[MAUSERNAME] [nvarchar](10) NOT NULL,
	[NGAYDATHANG] [date] NOT NULL,
	[TONGTIEN] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MAHOADON] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SANPHAM]    Script Date: 12/28/2022 7:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SANPHAM](
	[MASP] [nvarchar](10) NOT NULL,
	[TENSP] [nvarchar](50) NULL,
	[ANH] [char](50) NOT NULL,
	[DONGIA] [int] NOT NULL,
	[LOAI] [nvarchar](20) NOT NULL,
	[MOTA] [nvarchar](max) NULL,
	[SOLUONG] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MASP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[ACCOUNT] ([MAUSERNAME], [USERNAME], [EMAIL], [SODT], [PS], [TENUSER], [ANHDAIDIEN], [QUYEN], [GIOITINH], [DIACHI]) VALUES (N'AC001', N'admin', N'helloWorld@gmail.com', N'0123456789', N'admin', N'Admin', N'1.png', N'admin', N'Nam', N'dwadfrsfrfs                                       ')
GO
INSERT [dbo].[GIOHANG] ([MAUSERNAME], [MASP], [SOLUONGTHEM]) VALUES (N'AC001', N'SP0012', 10)
GO
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP001', N'Logitech G Pro K/DA', N'kda2.jpeg                                         ', 200, N'Bàn Phím', N'Các phím switch cơ học GX Tactile tiên tiến được chế tạo để tăng hiệu suất, độ nhạy và độ bền, với phản hồi nhấn phím phát ra âm thanh, có thể cảm nhận được.', 2)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0010', N'Logitech G G305 BLACK', N'AnyConv.com__g304.png                             ', 170, N'Chuột', N'Chuột chơi game không dây LIGHTSPEED được thiết kế để mang lại hiệu suất cao với những cải tiến công nghệ mới nhất. Tuổi thọ pin ấn tượng 250 giờ. Bây giờ trong một loạt các màu sắc sống động.', 12)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0012', N'Logitech G G435 LIGHTSPEED Wireless Gaming Headset', N'g435.jpg                                          ', 130, N'Tai Nghe', N'Chơi game và nghe nhạc với sự thoải mái nhẹ nhàng và âm thanh mạnh mẽ, rõ ràng. Các mic tạo chùm kép giúp giảm tạp âm nền. Kết nối các thiết bị của bạn qua LIGHTSPEED không dây cấp độ chơi game và Bluetooth®.', 12)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0013', N'Logitech G G502 MAX LIGHTSPEED', N'g502 wireless.jpg                                 ', 300, N'Chuột', N'Chuột Logitech G502 là một biểu tượng, đứng đầu các bảng xếp hạng qua mọi thế hệ và là lựa chọn chuột máy tính không dây cho những game thủ nghiêm túc. Giờ đây, G502 gia nhập hàng ngũ những con chuột chơi game không dây tiên tiến nhất thế giới với việc phát hành G502 LIGHTSPEED.', 11)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0014', N'Logitech G G733', N'g773.jpg                                          ', 210, N'Tai Nghe', N'Logitech G733 LIGHTSPEED Wireless Black dòng tai nghe máy tính được thiết kế mang đến sự thoải mái cho game thủ. Đây là mẫu tai nghe không dây được trang bị âm thanh lập thể, các bộ lọc âm thanh và tính năng chiếu sáng tiên tiến bạn cần để nhìn, nói và chơi phong cách hơn bao giờ hết.', 5)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0015', N'Logitech Gaming Mouse Pad 943', N'g840.png                                          ', 140, N'Lót Chuột', N'Bề mặt cứng giúp giảm ma sát, khiến cho con chuột chuyển động nhanh chóng và mượt mà. Đế cao su giữ cố định vị trí trong những cuộc chơi game căng thẳng.', 5)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0016', N'Logitech G G915 TKL', N'g913.png                                          ', 160, N'Bàn Phím', N'Một bước đột phá về thiết kế và kỹ thuật. LIGHTSPEED không dây đẳng cấp chuyên nghiệp, RGB LIGHTSYNC tiên tiến và các phím switch cơ có cấu hình thấp.', 10)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0017', N'Logitech POP Keys', N'POP.png                                           ', 190, N'Bàn Phím', N'Thể hiện cá tính trên bàn làm việc và hơn nữa với POP Keys. Cùng với con Chuột POP phù hợp, hãy để cá tính thật của bạn tỏa sáng với kiểu bàn làm việc đầy phong cách và các phím emoji vui nhộn có thể tùy chỉnh.', 22)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0018', N'G733 LIGHTSPEED RGB', N'g733-black-gallery-1.jpg                          ', 200, N'Tai Nghe', N'Tai nghe chơi game không dây được thiết kế để mang lại hiệu suất và sự thoải mái. Được trang bị âm thanh lập thể, các bộ lọc âm thanh và tính năng chiếu sáng tiên tiến bạn cần để nhìn,', 10)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0019', N'Logitech G Pro Mechanical Tactile – Vikings', N'logi gpro.png                                     ', 250, N'Bàn Phím', N'Bàn phím chơi game CHUYÊN NGHIỆP đã được chứng minh trong thi đấu - nay với các phím switch cơ học GX Blue Clicky tiên tiến.', 9)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP002', N'G502 HERO K/DA', N'kda1.jpeg                                         ', 100, N'Chuột', N'G502 HERO K/DA là một trong những dòng chuột gaming mới nhất đến từ Logitech. Được trang bị đến 11 nút có thể tùy chỉnh giúp bạn dễ dàng chỉ định các lệnh cá nhân, cùng cảm biến quang học tiên tiến cho độ chính xác theo dõi tối đa, tính năng chiếu sáng RGB có thể tùy chỉnh, cảm biến trò chơi từ 200 cho tới 25.600 DPI.', 20)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP0020', N'Logitech G502 Lightspeed', N'logitech_g502.jpg                                 ', 230, N'Chuột', N'Thiết kế mang tính biểu tượng của G502 đáp ứng kết nối không dây LIGHTSPEED chuyên nghiệp để có kết nối cực nhanh, đáng tin cậy.', 19)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP003', N'K/DA G733', N'kda3.jpeg                                         ', 150, N'Tai Nghe', N'Logitech G733 KDA LIGHTSPEED Wireless dòng sản phẩm tai nghe máy tính được thiết kế mang đến sự thoải mái cho game thủ. Đây là mẫu tai nghe không dây được trang bị âm thanh lập thể, các bộ lọc âm thanh và tính năng chiếu sáng tiên tiến bạn cần để nhìn, nói và chơi phong cách hơn bao giờ hết.', 30)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP004', N'K/DA G840', N'kda4.jpeg                                         ', 30, N'Lót Chuột', N'Được thiết kế với không gian sử dụng lớn, bề mặt được tinh chỉnh mang lại trải nghiệm chơi game và sử dụng tối ưu chất lượng cao.Với thiết kế cao cấp từ phiên bản đặc biệt KDA do Logitech phối hợp cùng League of Legends mang lại một cảm giác đổi mới, sáng tạo và phóng khoáng trong cách sử dụng.', 15)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP005', N'Logitech G512 Carbon', N'_g512.png                                         ', 100, N'Bàn Phím', N'G512 là bàn phím chơi game hiệu suất cao có bao gồm lựa chọn các phím switch cơ học GX nâng cao của bạn. Công nghệ chơi game tiên tiến cùng cấu trúc hợp kim nhôm khiến cho G512 trở nên đơn giản, bền và đầy đủ tính năng.

', 25)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP006', N'Logitech G815', N'g815 (1).png                                      ', 190, N'Bàn Phím', N'Công nghệ chơi game mới nhất. Thiết kế siêu mỏng. LIGHTSYNC RGB và các phím G chuyên dụng. Được thiết kế để chơi trò chơi hiệu suất cao với các công tắc cơ GL cấu hình thấp trong tiếng clicky, xúc giác và tuyến tính.', 35)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP007', N'Logitech G PRO X SUPERLIGHT', N'AnyConv.com__g pro.png                            ', 150, N'Chuột', N'Nhẹ hơn 63 gram. Công nghệ LIGHTSPEED không dây tiên tiến có độ trễ thấp. Độ chỉnh xác đến từng micro-mét với cảm biến HERO 25K. Loại bỏ tất cả mọi chướng ngại vật với con chuột PRO nhẹ nhất và nhanh nhất từ trước tới nay của chúng tôi.', 11)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP008', N'Logitech G G203 LIGHTSYNC', N'AnyConv.com__g203_!.png                           ', 200, N'Chuột', N'Chuột chơi game có dây Logitech G203 Lightsync là con chuột trứ danh nhà Logitech về vẻ đẹp thiết kế cũng như độ nhạy bén của chuột. Độ chính xác khi thao tác bấm rất cao, hỗ trợ tối ưu cho người dùng khi làm việc, tìm kiếm,... Đoạn mô tả dưới đây sẽ cho bạn biết chuột chơi game này có thực sự thích hợp với bạn không nhé!', 21)
INSERT [dbo].[SANPHAM] ([MASP], [TENSP], [ANH], [DONGIA], [LOAI], [MOTA], [SOLUONG]) VALUES (N'SP009', N'Logitech G G305 LIGHTSPEED', N'AnyConv.com__g203_2.png                           ', 150, N'Chuột', N'Chuột chơi game không dây LIGHTSPEED được thiết kế để mang lại hiệu suất cao với những cải tiến công nghệ mới nhất. Tuổi thọ pin ấn tượng 250 giờ. Bây giờ trong một loạt các màu sắc sống động.', 31)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ACCOUNT__D1595AF08F4FBCD1]    Script Date: 12/28/2022 7:33:37 PM ******/
ALTER TABLE [dbo].[ACCOUNT] ADD UNIQUE NONCLUSTERED 
(
	[TENUSER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__ACCOUNT__D1595AF0A945BF9D]    Script Date: 12/28/2022 7:33:37 PM ******/
ALTER TABLE [dbo].[ACCOUNT] ADD UNIQUE NONCLUSTERED 
(
	[TENUSER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  CONSTRAINT [DF_AC_ADD]  DEFAULT ('1.png') FOR [ANHDAIDIEN]
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT ('user') FOR [QUYEN]
GO
ALTER TABLE [dbo].[CHITIETHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHD_MASP] FOREIGN KEY([MASP])
REFERENCES [dbo].[SANPHAM] ([MASP])
GO
ALTER TABLE [dbo].[CHITIETHOADON] CHECK CONSTRAINT [FK_CTHD_MASP]
GO
ALTER TABLE [dbo].[CHITIETHOADON]  WITH CHECK ADD  CONSTRAINT [FK_CTHD_MHD] FOREIGN KEY([MAHOADON])
REFERENCES [dbo].[HOADON] ([MAHOADON])
GO
ALTER TABLE [dbo].[CHITIETHOADON] CHECK CONSTRAINT [FK_CTHD_MHD]
GO
ALTER TABLE [dbo].[GIOHANG]  WITH CHECK ADD  CONSTRAINT [FK_GHMASP] FOREIGN KEY([MASP])
REFERENCES [dbo].[SANPHAM] ([MASP])
GO
ALTER TABLE [dbo].[GIOHANG] CHECK CONSTRAINT [FK_GHMASP]
GO
ALTER TABLE [dbo].[GIOHANG]  WITH CHECK ADD  CONSTRAINT [FK_GHMAUSERNAME] FOREIGN KEY([MAUSERNAME])
REFERENCES [dbo].[ACCOUNT] ([MAUSERNAME])
GO
ALTER TABLE [dbo].[GIOHANG] CHECK CONSTRAINT [FK_GHMAUSERNAME]
GO
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_MAUSERNAME] FOREIGN KEY([MAUSERNAME])
REFERENCES [dbo].[ACCOUNT] ([MAUSERNAME])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_MAUSERNAME]
GO
/****** Object:  StoredProcedure [dbo].[capnhatSLSP]    Script Date: 12/28/2022 7:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[capnhatSLSP] @masp nvarchar(10),@soluongmua int
as
	begin
		update SANPHAM set SOLUONG=SOLUONG-@soluongmua where MASP=@masp
	end
GO
