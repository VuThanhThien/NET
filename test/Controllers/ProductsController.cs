using Microsoft.AspNetCore.Mvc;
using NET.Bussiness.Interfaces;
using NET.Model.Dictionary;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NET.API.Controllers
{
    /// <summary>
    /// API hàng hoá
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BasesController<Product>
    {
        /// <summary>
        /// Biến productBL gọi lên tầng Bussiness xử lý nghiệp vụ hàng hoá
        /// </summary>
        private readonly IProductBL _productBL;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="baseBL"></param>
        /// <param name="productBL"></param>
        public ProductsController(IBaseBL<Product> baseBL, IProductBL productBL) : base(baseBL)
        {
            _productBL = productBL;
        }

        // GET api/<ProductsController>
        /// <summary>
        /// Lấy danh sách hàng hoá - kèm theo tìm kiếm và phân trang
        /// </summary>
        /// <param name="offset">Vị trí bắt đầu (mặc định là 1)</param>
        /// <param name="limit">Giới hạn bản ghi cần lấy (mặc định là 25)</param>
        /// <param name="productSKU">Mã SKU của sản phẩm</param>
        /// <param name="productName">Tên sản phẩm</param>
        /// <param name="categoryCode">Mã loại sản phẩm</param>
        /// <param name="unitCode"> Mã Đơn vị tính( Mặc định là 0, 1- đôi, 2- chiếc, 3- túi, 4- kg, 5- thùng, 6-met, 7- cuộn, 8-lit, 9- hộp)</param>
        /// <param name="sellPrice">Giá bán tối thiểu (Mặc định là 0)</param>
        /// <param name="isShow">Có hiện thị trên màn hình bán hàng (1-Có, 0-Không, 2-Tất cả)</param>
        /// <param name="status">Trạng thái hàng hoá (0-Đang kinh doanh, 1-Ngừng kinh doanh, 2-Tất cả)</param>
        /// <returns>Danh sách các bản ghi tìm được</returns>
        [HttpGet("Paginate")]
        public IActionResult GetProductPaging(
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 25,
            [FromQuery] string productSKU = "",
            [FromQuery] string productName = "",
            [FromQuery] int categoryCode = 0,
            [FromQuery] int unitCode = 0,
            [FromQuery] double sellPrice = 0,
            [FromQuery] int isShow = 2,
            [FromQuery] int status = 2)
        {
            var result = _productBL.GetPaging(offset, limit, productSKU, productName, categoryCode, unitCode, sellPrice, isShow, status);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // GET api/<ProductsController>
        /// <summary>
        /// Lấy số bản ghi thỏa mãn yêu cầu search
        /// </summary>
        /// <param name="productSKU">Mã SKU của sản phẩm</param>
        /// <param name="productName">Tên sản phẩm</param>
        /// <param name="categoryCode">Mã loại sản phẩm</param>
        /// <param name="unitCode"> Mã Đơn vị tính( Mặc định là 0, 1- đôi, 2- chiếc, 3- túi, 4- kg, 5- thùng, 6-met, 7- cuộn, 8-lit, 9- hộp)</param>
        /// <param name="sellPrice">Giá bán tối thiểu (Mặc định là 0)</param>
        /// <param name="isShow">Có hiện thị trên màn hình bán hàng (1-Có, 0-Không, 2-Tất cả)</param>
        /// <param name="status">Trạng thái hàng hoá (0-Đang kinh doanh, 1-Ngừng kinh doanh, 2-Tất cả)</param>
        /// <returns>Danh sách các bản ghi tìm được</returns>
        [HttpGet("Length")]
        public IActionResult GetLength(
            [FromQuery] string productSKU = "",
            [FromQuery] string productName = "",
            [FromQuery] int categoryCode = 0,
            [FromQuery] int unitCode = 0,
            [FromQuery] double sellPrice = 0,
            [FromQuery] int isShow = 2,
            [FromQuery] int status = 2)
        {
            var result = _productBL.GetLength(productSKU, productName, categoryCode, unitCode, sellPrice, isShow, status);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        /// <summary>
        /// Sinh mã SKU tự động từ chuỗi nhập vào
        /// </summary>
        /// <param name="productKey">Chuỗi tên nhập vào</param>
        /// <returns>SKU mới</returns>
        [HttpGet("SKU")]
        public IActionResult GetSKUGenerate([FromQuery] string productKey = "")
        {
            var result = _productBL.GetSKUGenerate(productKey);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        /// <summary>
        /// Lấy Barcode
        /// </summary>
        /// <returns></returns>
        [HttpGet("BarCode")]
        public IActionResult GetBarCode()
        {
            var result = _productBL.GetBarCode();
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // POST api/<ProductsController>
        /// <summary>
        /// Thêm mới một sản phẩm
        /// </summary>
        /// <param name="product">Danh sach Sản phẩm cần thêm mới</param>
        /// <returns></returns>
        [HttpPost]
        public override IActionResult Post([FromBody] Product product)
        {
            var result = _productBL.Insert(product);

            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // POST api/<ProductsController>
        /// <summary>
        /// Thêm mới nhiều sản phẩm
        /// </summary>
        /// <param name="products">Danh sach Sản phẩm cần thêm mới</param>
        /// <returns></returns>
        [HttpPost("Multi")]
        public IActionResult MultiInsert([FromBody] List<Product> products)
        {
            var result = _productBL.MultiInsert(products);

            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // PUT api/<ProductsController>/5
        /// <summary>
        /// Cập nhật thông tin hàng hoá
        /// </summary>
        /// <param name="productId">ID hàng hoá</param>
        /// <param name="product">Thông tin sửa đổi</param>
        /// <returns></returns>
        [HttpPut]
        public override IActionResult Put([FromQuery] Guid productId, [FromBody] Product product)
        {
            var result = _productBL.Update(productId, product);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        /// <summary>
        /// Cập nhật Form chi tiết hàng hóa
        /// Thêm, sửa hàng hóa cha
        /// Thêm, sửa, xóa hàng hóa con
        /// </summary>
        /// <param name="synchronizeWrapper">Một Object gồm 2 mảng, mảng đầu là mảng những object thêm, sửa. Mảng sau là mảng những id cần xóa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy VTThien 24/03/21
        [HttpPost("Sync")]
        public IActionResult Synchronized([FromBody] SynchronizeWrapper synchronizeWrapper)
        {
            var result = _productBL.Synchronized(synchronizeWrapper);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }
    }
}
