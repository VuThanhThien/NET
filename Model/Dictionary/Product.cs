
using System;

namespace NET.Model.Dictionary
{
    /// <summary>
    /// Model Product - Hàng hoá
    /// </summary>
    /// created by vtthien 08.03.2021
    public class Product
    {
        /// <summary>
        /// ID sản phẩm
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Mã SKU sản phẩm
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Trạng thái sản phẩm (0-Đang kinh doanh, 1-ngừng kinh doanh, 2- cả hai)
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Mã loại hàng
        /// </summary>
        public int? CategoryCode { get; set; }


        /// <summary>
        /// Giá mua
        /// </summary>
        public double? BuyPrice { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        public double? SellPrice { get; set; }

        /// <summary>
        /// Mã đơn vị (1- đôi, 2- chiếc, 3- túi, 4- kg, 5- thùng, 6-met, 7- cuộn, 8-lit)
        /// </summary>
        public int? UnitCode { get; set; }


        /// <summary>
        /// Có show trên màn hình bán hàng (0-không, 1-có, 2- cả hai)
        /// </summary>
        public int? IsShow { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ảnh
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Màu sắc
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Mã vạch
        /// </summary>
        public int? BarCode { get; set; }

        /// <summary>
        /// Đường dẫn từ cha ->con
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// định danh của hàng hóa cha
        /// </summary>
        public Guid? ProductIDParent { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

    }
}
