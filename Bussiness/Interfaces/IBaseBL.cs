using NET.Model;
using System;

namespace NET.Bussiness.Interfaces
{
    public interface IBaseBL<T> where T : class
    {
        BaseResponse GetAll();

        BaseResponse GetByID(Guid id);
        BaseResponse GetByIDParent(Guid id);

        BaseResponse GetBySKU(string skuCode);

        BaseResponse Insert(T entity);

        BaseResponse Update(Guid id, T entity);

        BaseResponse Delete(Guid id);
    }
}
