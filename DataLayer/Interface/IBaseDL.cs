
using System;
using System.Collections.Generic;

namespace NET.DataLayer.Interface
{
    /// <summary>
    /// Interface lớp cơ sở tương tác CSDL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDL<T> where T: class
    {
        IEnumerable<T> GetAll();

        T GetByID(Guid id);

        IEnumerable<T> GetByIDParent(Guid id);

        T GetByCode(string code);

        T GetBySKU(string skuCode);

        int Insert(T entity);

        int Update(T entity);

        int Delete(Guid id);
    }
}
