
using NET.Business.Interfaces;
using NET.DataLayer.Interface;
using NET.Model;
using NET.Model.Dictionary;
using NET.Model.Enum;
using System;

namespace NET.Business.Base
{
    /// <summary>
    /// Lớp base xử lý nghiệp vụ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// created by vthien 09.03.2021
    public class BaseBL<T>: IBaseBL<T> where T: class
    {
        /// <summary>
        /// Biến gọi lên tầng DL
        /// </summary>
        protected readonly IBaseDL<T> _baseDL;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="baseDL"></param>
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        /// <summary>
        /// Hàm lấy tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách các bản ghi</returns>
        public virtual BaseResponse GetAll()
        {
            // lấy dữ liệu từ tầng DL về
            var result = _baseDL.GetAll();

            // danh sách rỗng => trả vè mã 204
            if( result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.No_ConTent,
                    Data = null
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về => trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
        }

        /// <summary>
        /// Lấy thông tin theo ID
        /// </summary>
        /// <param name="id">Định danh cần lấy thông tin</param>
        /// <returns>Đối tượng lấy được</returns>
        public virtual BaseResponse GetByID(Guid id)
        {
            // lấy dữ liệu từ tầng DL về
            var result = _baseDL.GetByID(id);

            // danh sách rỗng => trả vè lỗi 400
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.NotFound, ErrorCode.NotFound)
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về => trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns></returns>
        public virtual BaseResponse Insert(T entity)
        {
            var result = _baseDL.Insert(entity);

            if (result > 0)
            {
                // khởi tạo dữ liệu trả về
                // thành công ==> trả về mã 201
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Created,
                    Data = result
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về
                // Lỗi insert vào db ==> trả về 400
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.Insert_Fail, ErrorCode.DB_Fail)
                };
                return response;
            }
        }

        /// <summary>
        /// Cập nhật thông tin
        /// </summary>
        /// <param name="entity">Đối tượng cập nhật</param>
        /// <returns></returns>
        public virtual BaseResponse Update(Guid id, T entity)
        {
            var result = _baseDL.Update(entity);

            if (result > 0)
            {
                // khởi tạo dữ liệu trả về
                // thành công ==> trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về
                // Lỗi thao tác dữ liệu trong db ==> trả về 400
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.Update_Fail, ErrorCode.DB_Fail)
                };
                return response;
            }
        }

        /// <summary>
        /// Xoá thông tin
        /// </summary>
        /// <param name="id">Id (khoá chính) bản ghi cần xoá</param>
        /// <returns>Số bản ghi bị xoá</returns>
        public virtual BaseResponse Delete(Guid id)
        {
            // kiểm tra xem có tồn tại id cần xoá không
            var entity = GetByID(id);

            // Nếu không tìm thấy => trả về kết quả của service lấy theo ID luôn
            if(entity.HTTPStatusCode != HTTPStatusCode.Ok)
            {
                return entity;
            }

            // tìm thấy đối tượng cần xoá => tiến hành xoá đối tượng này
            var result = _baseDL.Delete(id);

            if (result > 0)
            {
                // khởi tạo dữ liệu trả về
                // thành công ==> trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về
                // Lỗi thao tác dữ liệu trong db ==> trả về 400
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.Delete_Fail, ErrorCode.DB_Fail)
                };
                return response;
            }
        }

        /// <summary>
        /// Lấy thông tin theo mã SKU
        /// </summary>
        /// <param name="skuCode"></param>
        /// <returns></returns>
        public BaseResponse GetBySKU(string skuCode)
        {
            // lấy dữ liệu từ tầng DL về
            var result = _baseDL.GetBySKU(skuCode);

            // danh sách rỗng => trả vè lỗi 400
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.NotFound, ErrorCode.NotFound)
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về => trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
        }

        /// <summary>
        /// Lấy thông tin theo id cha mẹ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual BaseResponse GetByIDParent(Guid id)
        {
            // lấy dữ liệu từ tầng DL về
            var result = _baseDL.GetByIDParent(id);

            // danh sách rỗng => trả vè lỗi 400
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.NotFound, ErrorCode.NotFound)
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về => trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
        }
    }
}
