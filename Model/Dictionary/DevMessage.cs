
namespace NET.Model.Dictionary
{
    /// <summary>
    /// Enum định nghĩa các message cho đội phát triển
    /// </summary>
    /// created by vtthien 09.03.2021
    public class DevMessage
    {
        /// <summary>
        /// Thông báo mặc định
        /// </summary>
        public const string Default = "Có lỗi xảy ra!";

        /// <summary>
        /// Insert thất bại
        /// </summary>
        public const string Insert_Fail = "Thêm mới thất bại";

        /// <summary>
        /// Update thất bại
        /// </summary>
        public const string Update_Fail = "Cập nhật thông tin thất bại";

        /// <summary>
        /// Delete thất bại
        /// </summary>
        public const string Delete_Fail = "Xoá thông tin thất bại";

        /// <summary>
        /// Không tìm thấy
        /// </summary>
        public const string NotFound = "Không tìm thấy do dữ liệu truyền lên không hợp lệ";

        /// <summary>
        /// Mã SKU bị trùng lặp
        /// </summary>
        public const string DuplicateSKU = "Mã SKU đã tồn tại";

        /// <summary>
        /// Lỗi mã productID
        /// </summary>
        public const string Invalite_ProductID = "Mã productID truyền lên không phù hợp";
    }
}
