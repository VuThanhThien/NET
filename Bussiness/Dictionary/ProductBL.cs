using NET.Business.Base;
using NET.Business.Interfaces;
using NET.DataLayer.Interface;
using NET.Model;
using NET.Model.Dictionary;
using NET.Model.Enum;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace MISA.eShop.Business.Dictionary
{
    public class ProductBL : BaseBL<Product>, IProductBL
    {
        private readonly IProductDL _productDL;

        public ProductBL(IBaseDL<Product> _baseDL, IProductDL productDL) : base(_baseDL)
        {
            _productDL = productDL;
        }


        /// <summary>
        /// Lấy danh sách hàng hoá
        /// </summary>
        /// <param name="offset">Vị trí bắt đầu (mặc định là 1)</param>
        /// <param name="limit">Giới hạn bản ghi cần lấy (mặc định là 25)</param>
        /// <param name="productSKU">Mã SKU của sản phẩm</param>
        /// <param name="productName">Tên sản phẩm</param>
        /// <param name="productCategory">Loại sản phẩm</param>
        /// <param name="productUnit">Đơn vị tính</param>
        /// <param name="sellPrice">Giá bán</param>
        /// <param name="isShow">Có hiện thị trên màn hình bán hàng (1-Có, 0-Không, 2-Tất cả)</param>
        /// <param name="status">Trạng thái hàng hoá (1-Đang kinh doanh, 2-Ngừng kinh doanh, 3-Tất cả)</param>
        /// <returns>Danh sách các bản ghi tìm được</returns>
        public BaseResponse GetPaging(
            int offset = 1,
            int limit = 25,
            string productSKU = "",
            string productName = "",
            int categoryCode = 0,
            int unitCode = 0,
            double sellPrice = 0,
            int isShow = 2,
            int status = 2)
        {

            var param = new
            {

                limit = limit,
                offset = offset,
                productSKU = productSKU,
                productName = productName,
                categoryCode = categoryCode,
                unitCode = unitCode,
                sellPrice = sellPrice,
                isShow = isShow,
                status = status
            };

            var result = _productDL.GetPaging(param);

            // danh sách rỗng => trả vè mã 204
            if (result == null)
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
        /// Lấy số bản ghi thỏa mãn điều kiện search
        /// </summary>
        /// <param name="offset">Vị trí bắt đầu (mặc định là 1)</param>
        /// <param name="limit">Giới hạn bản ghi cần lấy (mặc định là 25)</param>
        /// <param name="productSKU">Mã SKU của sản phẩm</param>
        /// <param name="productName">Tên sản phẩm</param>
        /// <param name="productCategory">Loại sản phẩm</param>
        /// <param name="productUnit">Đơn vị tính</param>
        /// <param name="sellPrice">Giá bán</param>
        /// <param name="isShow">Có hiện thị trên màn hình bán hàng (1-Có, 0-Không, 2-Tất cả)</param>
        /// <param name="status">Trạng thái hàng hoá (1-Đang kinh doanh, 2-Ngừng kinh doanh, 3-Tất cả)</param>
        /// <returns>Danh sách các bản ghi tìm được</returns>

        public BaseResponse GetLength(
            string productSKU = "",
            string productName = "",
            int categoryCode = 0,
            int unitCode = 0,
            double sellPrice = 0,
            int isShow = 2,
            int status = 2)
        {

            var param = new
            {
                productSKU = productSKU,
                productName = productName,
                categoryCode = categoryCode,
                unitCode = unitCode,
                sellPrice = sellPrice,
                isShow = isShow,
                status = status
            };

            // khởi tạo dữ liệu trả về => trả về mã 200
            var response = new BaseResponse()
            {
                HTTPStatusCode = HTTPStatusCode.Ok,
                Data = _productDL.GetLength(param)
            };
            return response;
        }

        /// <summary>
        /// Lấy mã sku tự động
        /// </summary>
        /// <param name="productKey"></param>
        /// <returns></returns>
        /// createdby vtt 19/03/21
        public BaseResponse GetSKUGenerate(string productKey)
        {
            var param = new
            {
                productKey = productKey
            };
            //Nếu chuỗi nhập vào null
            if (productKey == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.DuplicateSKU, ErrorCode.DuplicateData)
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = _productDL.GetSKUGenerate(param)
                };
                return response;
            }
        }

        public BaseResponse MultiInsert(List<Product> products)
        {
            var result = new BaseResponse()
            {
                Data = products.Count,
                HTTPStatusCode = HTTPStatusCode.Ok
            };

            try
            {
                using (var ts = new TransactionScope())
                {
                    foreach (var item in products)
                    {
                        Insert(item);
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.Data = ex;
                result.HTTPStatusCode = HTTPStatusCode.Ok;

                return result;
            }

            return result;
        }

        /// <summary>
        /// Ghi đè hàm Insert từ base
        /// Với Product bổ sung thêm đoạn check trùng mã SKU trước khi insert
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public override BaseResponse Insert(Product product)
        {
            // kiểm tra trùng mã SKU không
            var productBySku = _baseDL.GetBySKU(product.SKU);

            if (product.ProductIDParent == null)
            {
                product.ProductIDParent = Guid.Empty;
            }

            // nếu tìm thấy mã Sku trong db => trả về lỗi 400
            if (productBySku != null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.DuplicateSKU, ErrorCode.DuplicateData)
                };
                return response;
            }

            var result = base.Insert(product);
            result.Data = product;

            // không mã sku không tồn tại thì tiến hành insert dữ liệu vào db
            return result;
        }

        /// <summary>
        /// Ghi đè lại hàm Update từ base
        /// Với Product bổ sung thêm đoạn check trước khi update
        /// 1. check 2 id truyền lên qua router và object body có trùng khớp không
        /// 2. check người dùng sửa mã sku sang mã sku của sản phẩm khác
        /// </summary>
        /// <param name="productID">ID hàng hoá</param>
        /// <param name="product">Thông tin hàng hoá sửa đổi</param>
        /// <returns></returns>
        public override BaseResponse Update(Guid productID, Product product)
        {
            // Check Id truyền lên và ID trong object => nếu khác nhau thì trả về lỗi 400
            if (productID != product.ProductID)
            {
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.Invalite_ProductID, ErrorCode.DataConflic)
                };
                return response;
            }

            // check trùng mã SKU trước khi cho phép cập nhật thông tin
            var productBySku = _baseDL.GetBySKU(product.SKU);

            // nếu tìm thấy mã Sku trong db && mã SKU tìm được đang được gắn cho product khác => trả về lỗi 400
            if (productBySku != null && productBySku.ProductID != productID)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = new BaseError(DevMessage.DuplicateSKU, ErrorCode.DuplicateData)
                };
                return response;
            }

            // nếu không tìm thấy ==> sửa sang mã SKU khác
            // hoặc tìm thấy và id (tìm được) trùng với id truyền lên ==> đang sửa chính nó (chỉ sửa các thông tin khác)
            // thì mới cho phép cập nhật
            return base.Update(productID, product);
        }

        /// <summary>
        /// Xử lý thêm sửa xóa cha và con ở form hàng hóa chi tiêys
        /// </summary>
        /// <param name="synchronizeWrapper">Object gồm 2 mảng. Mảng đầu là mảng object thêm sửa. Mảng sau là các id con cần xóa</param>
        /// <returns></returns>
        public BaseResponse Synchronized(SynchronizeWrapper synchronizeWrapper)
        {
            var result = new BaseResponse()
            {
                HTTPStatusCode = HTTPStatusCode.Ok,
                Data = synchronizeWrapper.NewOrEditObject.Count + synchronizeWrapper.DeleteObject.Count
            };

            try
            {
                using (var ts = new TransactionScope())
                {

                    //Gán id parent bằng new guid
                    var idParent = Guid.NewGuid();
                    // xử lý list thêm mới hoặc sửa
                    //For trong mảng đầu tiên
                    for (var i = 0; i < synchronizeWrapper.NewOrEditObject.Count; i++)
                    {
                        //Lấy object theo id truyền vào
                        var productById = GetByID(synchronizeWrapper.NewOrEditObject[i].ProductID);
                        //nếu lấy được tức đã tồn tại bản ghi này => update
                        if (productById.HTTPStatusCode == HTTPStatusCode.Ok && productById.Data != null)
                        {
                            //Nếu là phần tử đầu tiên của mảng <=> hàng hóa cha
                            if (i == 0)
                            {
                                // lấy id của hàng hóa cha ra để chuẩn bị gán cho hàng hóa con
                                idParent = synchronizeWrapper.NewOrEditObject[i].ProductID;
                            }
                            //update hàng hóa 
                            var updateResult = Update(synchronizeWrapper.NewOrEditObject[i].ProductID, synchronizeWrapper.NewOrEditObject[i]);
                            if (updateResult.HTTPStatusCode != HTTPStatusCode.Ok)
                            {
                                return updateResult;
                            }
                        }
                        //Nếu không lây được thông tin tức là thêm mới
                        else
                        {
                            // Mặc định phần tử đầu tiên là sản phẩm cha
                            // nếu thêm mới sản phẩm cha
                            if (i == 0)
                            {
                                // gán Id của sản phẩm cha bằng một chuỗi Id ngẫu nhiên
                                synchronizeWrapper.NewOrEditObject[i].ProductID = idParent;
                            }
                            else
                            {
                                // nếu thêm mới sản phẩm con
                                //sinh id con ngẫu nhiên
                                synchronizeWrapper.NewOrEditObject[i].ProductID = Guid.NewGuid();
                                //id parent con bằng id cha đã lấy bên trên
                                synchronizeWrapper.NewOrEditObject[i].ProductIDParent = idParent;
                            }
                            //insert
                            var insertResult = Insert(synchronizeWrapper.NewOrEditObject[i]);
                            //Thêm thành công thì trả về kết quả
                            if (insertResult.HTTPStatusCode != HTTPStatusCode.Created)
                            {
                                return insertResult;
                            }
                        }
                    }

                    // xử lý xoá
                    foreach (var item in synchronizeWrapper.DeleteObject)
                    {
                        var deleteResult = Delete(item);
                        if (deleteResult.HTTPStatusCode != HTTPStatusCode.Ok)
                        {
                            return deleteResult;
                        }
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.HTTPStatusCode = HTTPStatusCode.Server_Error;
                result.Data = ex;
            }

            return result;
        }

        /// <summary>
        /// lấy mã vạch 
        /// </summary>
        /// <returns></returns>
        public BaseResponse GetBarCode()
        {
            var result = _productDL.GetBarCode();

            var response = new BaseResponse()
            {
                HTTPStatusCode = HTTPStatusCode.Ok,
                Data = result
            };

            return response;

        }

    }
}
