using Main_Common.Model.Account;
using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Interface.ITableRepository
{
    /// <summary>
    /// 【Main】【interface】登入狀態 Repository
    /// </summary>
    public interface ILoginStatusRepository_Main : IGenericRepository<LoginStatus>
    {
        // 如非通用方法，加在這邊

        #region == 檢查相關 ==
        /// <summary>
        /// 檢查資料是否存在(ById)
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns>「true，有」「false，無」</returns>
        public bool CheckExist(Guid? id);
        #endregion

        #region == 取資料相關 ==
        /// <summary>
        /// 【Queryable】【ref】過濾出使用者的Queryable
        /// </summary>
        /// <param name="dbData">ref 使用者的Queryable</param>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public void WhereQueryable(ref IQueryable<LoginStatus> dbData, LoginStatus_Filter input);

        /// <summary>
        /// 【資料】取使用者DTO
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public LoginStatus_DTO? GetDTO(LoginStatus_Filter input);

        /// <summary>
        /// 【資料】取登入資料(依登入狀態)
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public UserSession_Model? GetLoginData(LoginStatus_Filter input);
        #endregion

        #region == 更新資料相關 ==
        // ...
        #endregion

        #region == 其他 ==
        // ...
        #endregion
    }
}
