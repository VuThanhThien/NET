using NET.Business.Interfaces;
using NET.Model;
using NET.Model.Dictionary;
using System.Collections.Generic;

namespace NET.Business.Interfaces
{
    public interface IProductBL : IBaseBL<Product>
    {
        /// <summary>
        /// Lấy phân trang kèm filter
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="productSKU"></param>
        /// <param name="productName"></param>
        /// <param name="categoryCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="sellPrice"></param>
        /// <param name="isShow"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        BaseResponse GetPaging(
            int offset = 1,
            int limit = 25,
            string productSKU = "",
            string productName = "",
            int categoryCode = 0,
            int unitCode = 0,
            double sellPrice = 0,
            int isShow = 2,
            int status = 2);

        /// <summary>
        /// Lấy số bản ghi thỏa mãn filter
        /// </summary>
        /// <param name="productSKU"></param>
        /// <param name="productName"></param>
        /// <param name="categoryCode"></param>
        /// <param name="unitCode"></param>
        /// <param name="sellPrice"></param>
        /// <param name="isShow"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        BaseResponse GetLength(
            string productSKU = "",
            string productName = "",
            int categoryCode = 0,
            int unitCode = 0,
            double sellPrice = 0,
            int isShow = 2,
            int status = 2);

        /// <summary>
        /// Lấy mã vạch
        /// </summary>
        /// <returns></returns>
        BaseResponse GetBarCode();

        /// <summary>
        /// Sinh SKU tự động
        /// </summary>
        /// <param name="productKey"></param>
        /// <returns></returns>
        BaseResponse GetSKUGenerate(string productKey);

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        BaseResponse MultiInsert(List<Product> products);

        /// <summary>
        /// Cập nhật form chi tiết
        /// </summary>
        /// <param name="synchronizeWrapper"></param>
        /// <returns></returns>
        BaseResponse Synchronized(SynchronizeWrapper synchronizeWrapper);
    }
}
