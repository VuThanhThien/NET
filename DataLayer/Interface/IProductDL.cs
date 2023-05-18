using NET.Model.Dictionary;
using System.Collections.Generic;

namespace NET.DataLayer.Interface
{
    public interface IProductDL: IBaseDL<Product>
    {
        /// <summary>
        /// Lấy danh sách hàng hoá - có phân trang và tìm kiếm
        /// </summary>
        /// <param name="param">Tham số truyền vào Store</param>
        /// <returns>Danh sách hàng hoá</returns>
        IEnumerable<Product> GetPaging(object param);

        /// <summary>
        /// Lấy số lượng bản ghi thỏa mãn điều kiện lọc
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        long GetLength(object param);

        /// <summary>
        /// Lấy SKU sinh tự động
        /// </summary>
        /// <param name="productKey">Chuỗi cắt gọn tên hàng hóa</param>
        /// <returns></returns>
        string GetSKUGenerate(object param);

        /// <summary>
        /// Lấy mã vạch
        /// </summary>
        /// <returns></returns>
        int GetBarCode();
    }
}
