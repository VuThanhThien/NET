
namespace NET.Model.Enum
{
    /// <summary>
    /// Định nghĩa các enum Procedure
    /// </summary>
    /// created by vtthien 08.03.2021
    public class Procedure
    {
        #region Tên Store
        /// <summary>
        /// Tiền tố cuả tên procedure
        /// </summary>
        public const string PRE_PROC_NAME = "Proc_";

        /// <summary>
        /// Proc Get All
        /// </summary>
        public const string PROC_GET_ALL = "GetAll";

        /// <summary>
        /// Proc Get
        /// </summary>
        public const string PROC_GET = "Get";

        /// <summary>
        /// Proc get by id
        /// </summary>
        public const string LAST_GET_BYID = "ByID";

        /// <summary>
        /// Proc get by sku code
        /// </summary>
        public const string LAST_GET_BYSKU = "BySKU";

        /// <summary>
        /// Proc get by code
        /// </summary>
        public const string LAST_GET_BYCODE = "ByCode";

        /// <summary>
        /// Proc Insert
        /// </summary>
        public const string PROC_INSERT = "Insert";

        /// <summary>
        /// Proc update
        /// </summary>
        public const string PROC_UPDATE = "Update";

        /// <summary>
        /// Proc delete
        /// </summary>
        public const string PROC_DELETE = "Delete";

        /// <summary>
        /// Lấy thông tin sản phẩm có phân trang
        /// </summary>
        public const string PROC_GET_PRODUCT_PAGING = "Proc_GetProductPaging";

        /// <summary>
        /// Lấy tổng bản ghi thỏa mãn điều kiện search
        /// </summary>
        public const string PROC_GET_LENGTH = "Proc_GetLength";

        /// <summary>
        /// Sinh mã SKU tự động
        /// </summary>
        public const string PROC_GEN_PRODUCT_SKU = "Proc_GenProductSKU";

        /// <summary>
        /// 
        /// </summary>
        public const string LAST_GET_BYPARENT = "ByIDParent";
        #endregion
    }
}
