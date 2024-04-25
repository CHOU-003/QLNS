namespace AppHRM.Models
{
    public class Nhanvien
    {

        public String Id { get; set; }
        public String MaNhanVien { get; set; }  
        public String HoTen { get; set; }
        public String GioiTinh { get; set; }
        public String NgaySinh { get; set; } 
        public String DiaChi { get; set; }
        public String Email { get; set; }
        public int MaChucVu { get; set; }
        public int MaPhongBan { get; set; }
        public double Luong { get; set; }
        public String NgayVaoLam { get; set; } 
        public int KinhNghiem { get; set; }
        public String SDT { get; set; }
        public int MaNguoiQuanLy { get; set; } 

    }
}
