
using System;

namespace NET.Model.Dictionary
{
    /// <summary>
    /// Lớp dữ liệu thông báo request lỗi
    /// </summary>
    /// created by vtthien 09.03.2021
    public class BaseError
    {
        /// <summary>
        /// Thông báo cho đội phát triển
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo dành cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin bổ sung
        /// </summary>
        public string MoreInfos { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// ID tra cứu lỗi
        /// </summary>
        public string TraceId { get; set; }

        public BaseError(string devMsg, string errorCode, string userMsg = "", string moreInfos = "", string traceId = "")
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;

            MoreInfos = string.IsNullOrEmpty(moreInfos) ? MoreInfo.Default: moreInfos;

            TraceId = string.IsNullOrEmpty(traceId) ? Guid.NewGuid().ToString().Replace("-", "") : traceId;

            UserMsg = string.IsNullOrEmpty(userMsg) ? UserMessage.Default : userMsg;
        }
    }

    /// <summary>
    /// Lớp định nghĩa các mã lỗi
    /// </summary>
    public class ErrorCode
    {
        /// <summary>
        /// Lỗi cập nhật database
        /// </summary>
        public const string DB_Fail = "ErrorCode-001";

        /// <summary>
        /// Không tìm thấy
        /// </summary>
        public const string NotFound = "ErrorCode-002";

        /// <summary>
        /// Dữ liệu bị trùng lặp
        /// </summary>
        public const string DuplicateData = "ErrorCode-003";

        /// <summary>
        /// Dữ liệu truyền lên bị xung đột
        /// </summary>
        public const string DataConflic = "ErrorCode-004";
    }

    /// <summary>
    /// Enum định nghĩa các message cho người dùng
    /// </summary>
    /// created by vtthien 09.03.2021
    public class UserMessage
    {
        public const string Default = "Có lỗi xảy ra, vui lòng liên hệ MISA để được hỗ trợ giải quyết!";
    }

    /// <summary>
    /// Enum định nghĩa các thông tin bổ sung để tra cứu lỗi
    /// </summary>
    /// created by vtthien 09.03.2021
    public class MoreInfo
    {
        public const string Default = "Có lỗi xảy ra, vui lòng liên hệ MISA để được hỗ trợ giải quyết!";
    }
}
