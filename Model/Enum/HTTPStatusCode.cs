using System;
using System.Collections.Generic;
using System.Text;

namespace NET.Model.Enum
{
    /// <summary>
    /// Enum định nghĩa 
    /// </summary>
    public enum HTTPStatusCode
    {
        /// <summary>
        /// Success
        /// </summary>
        Ok = 200,

        /// <summary>
        /// Tạo mới thành công
        /// </summary>
        Created = 201,

        /// <summary>
        /// Trả dữ liệu về thành công nhưng không có dữ liệu
        /// </summary>
        No_ConTent = 204,

        /// <summary>
        /// Lỗi dữ liệu
        /// </summary>
        Bad_Request = 400,

        /// <summary>
        /// Không có quyền
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Bị cấm
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// Không tìm thấy
        /// </summary>
        Not_Found = 404,

        /// <summary>
        /// Request Time Out
        /// </summary>
        TimeOut = 408,

        /// <summary>
        /// Lỗi hệ thống
        /// </summary>
        Server_Error = 500
    }
}
