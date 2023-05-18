using NET.Model.Enum;

namespace NET.Model
{
    /// <summary>
    /// Lớp cơ sở dữ liệu trả về cho Client
    /// </summary>
    /// created by vtthien 09.03.2021
    public class BaseResponse
    {
        /// <summary>
        /// Header HTTP Status Code
        /// </summary>
        public HTTPStatusCode HTTPStatusCode { get; set; }

        /// <summary>
        /// Thông tin dữ liệu trả về
        /// </summary>
        public object? Data { get; set; }
    }
}
