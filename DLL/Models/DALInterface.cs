using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    interface DALInterface<T,U>
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns></returns>
        ResultInfo<List<T>> GetList(T model);
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        ResultInfo<T> GetEnetityByID(long ID);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">要新增的实体</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        ResultInfo<bool> Insert(T model, U user);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">要更新的实体</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        ResultInfo<bool> Update(T model, U user);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">要删除的ID</param>
        /// <param name="user">操作人</param>
        /// <returns></returns>
        ResultInfo<bool> Delete(long ID, U user);
    }
}
