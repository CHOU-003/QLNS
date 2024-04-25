using MongoDB.Bson; // Thêm dòng này để sử dụng ObjectId

public class Quanly
{
        public ObjectId Id { get; set; } // Thay đổi từ string thành ObjectId
        public int MaNguoiQuanLy { get; set; }
        public String email { get; set; }
        public String password { get; set; }
 }
