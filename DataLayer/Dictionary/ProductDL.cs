
using Dapper;
using NET.DataLayer.Base;
using NET.DataLayer.Interface;
using NET.Model.Dictionary;
using NET.Model.Enum;
using System.Collections.Generic;

namespace NET.DataLayer.Dictionary
{
    public class ProductDL : BaseDL<Product>, IProductDL
    {
        public ProductDL(IDbContext<Product> dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Lấy danh sách hàng hoá - có phân trang và tìm kiếm
        /// </summary>
        /// <param name="param">Tham số truyền vào Store</param>
        /// <returns>Danh sách hàng hoá</returns>
        public IEnumerable<Product> GetPaging(object param)
        {
            var procName = Procedure.PROC_GET_PRODUCT_PAGING;

            var parameters = new DynamicParameters(param);

            var result = _dbContext.QueryStore(procName, parameters);

            return result;
        }

        /// <summary>
        /// Lấy số lương bản ghi thảo mãn điều kiện lọc
        /// </summary>
        /// <param name="param">Tham số truyền vào store</param>
        /// <returns>Số bản ghi</returns>
        public long GetLength(object param)
        {
            var procName = Procedure.PROC_GET_LENGTH;

            var parameters = new DynamicParameters(param);

            var result = (long)_dbContext.ExcuteScalarStore(procName, parameters);

            return result;
        }

        /// <summary>
        /// Lấy mã sku sinh tự động
        /// </summary>
        /// <param name="productKey">Chuỗi cắt gọn tên hàng hóa</param>
        /// <returns></returns>
        public string GetSKUGenerate(object param)
        {
            var procName = Procedure.PROC_GEN_PRODUCT_SKU;

            var parameters = new DynamicParameters(param);

            var result = (string)_dbContext.ExcuteScalarStore(procName, parameters);

            return result;
        }

        public int GetBarCode()
        {
            var procName = "Proc_GetNewBarCode";

            var result = (int)_dbContext.ExcuteScalarStore(procName) + 1;

            return result;
        }
    }
}
