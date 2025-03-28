Hướng dẫn chạy cơ sở dữ liệu: 
Cách 1:	Tạo database bằng sql server = "create database BTL_LTW_QLBDT"
	Trong file BtlLtwQlbdtContext trong thư mục Models dòng 50 thay chuỗi kết nối server
Cách 2: Sử dụng nuget console kết nối cơ sở dữ liệu = "Scaffold-DbContext "Data Source=YOUR_SERVER;Initial Catalog=BTL_LTW_QLBDT;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models"
	Thêm chuỗi "[NotMapped]
		public TinNhan? LastMessage { get; set; }"
	vào dòng 40 file KhachHang.cs trong thư mục Models