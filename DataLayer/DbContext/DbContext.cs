
using Dapper;
using Microsoft.Extensions.Configuration;
using NET.DataLayer.Interface;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace NET.DataLayer.DbContext
{
    /// <summary>
    /// Lớp tạo connect đến CSDL
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    /// created by vtthien 08.03.2021
    public class DbContext<T>: IDbContext<T> where T: class
    {
        /// <summary>
        /// Biến connect đến CSDL bằng Dapper
        /// </summary>
        private readonly IDbConnection _connect;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="config"></param>
        public DbContext(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnect");
            _connect = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Hàm query Store (Store SELECT)
        /// </summary>
        /// <param name="storeProcedure">Tên Store</param>
        /// <param name="parameters">Tham số truyền vào store</param>
        /// <returns>Danh sách các bản ghi query được</returns>
        /// created by vtthien 08.03.2021
        public IEnumerable<T> QueryStore(string storeProcedure, object parameters = null)
        {
            var param = new DynamicParameters(parameters);
            var result = _connect.Query<T>(storeProcedure, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Hàm excute Store (Store INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="storeProcedure">Tên Store</param>
        /// <param name="parameters">Tham số truyền vào store</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// created by vtthien 08.03.2021
        public int ExcuteStore(string storeProcedure, object parameters = null)
        {
            var param = new DynamicParameters(parameters);
            var result = _connect.Execute(storeProcedure, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Hàm excutescala query 1 thuộc tính
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="parameters"></param>
        /// <returns>Lấy về giá trị đầu tiên</returns>
        /// Createdby vtt (20/03/21)
        public object ExcuteScalarStore(string storeName, object parameters = null)
        {
            var param = new DynamicParameters(parameters);
            var result = _connect.ExecuteScalar(storeName, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
