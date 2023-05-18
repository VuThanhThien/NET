

using System.Collections.Generic;

namespace NET.DataLayer.Interface
{
    /// <summary>
    /// Interface DbContext
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    /// created by vtthien 08.03.2021
    public interface IDbContext<T> where T: class
    {
        /// <summary>
        /// Hàm query Store (Store SELECT)
        /// </summary>
        /// <param name="storeProcedure">Tên Store</param>
        /// <param name="parameters">Tham số truyền vào store</param>
        /// <returns>Danh sách các bản ghi query được</returns>
        /// created by vtthien 08.03.2021
        IEnumerable<T> QueryStore(string storeProcedure, object parameters = null);

        /// <summary>
        /// Hàm excute Store (Store INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="storeProcedure">Tên Store</param>
        /// <param name="parameters">Tham số truyền vào store</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// created by vtthien 08.03.2021
        int ExcuteStore(string storeProcedure, object parameters = null);

        /// <summary>
        /// ExcuteScalar để nhận về cột đầu tiên của dòng đầu tiên dưới dạng object
        /// </summary>
        /// <param name="storeName">tên store</param>
        /// <param name="parameters">Tham số truyền vào store</param>
        /// <returns>nhận về cột đầu tiên của dòng đầu tiên dưới dạng object</returns>
        /// createdby vtt(20/03/21)
        object ExcuteScalarStore(string storeName, object parameters = null);
    }
}
