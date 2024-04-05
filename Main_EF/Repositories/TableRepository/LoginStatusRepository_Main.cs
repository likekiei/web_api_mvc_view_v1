using Main_Common.Enum.E_ProjectType;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Common.Model.Data;
using Main_Common.Model.Tool;
using Main_EF.Interface.ITableRepository;
using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Repositories.TableRepository
{
    /// <summary>
    /// 【Main】登入狀態 Repository
    /// </summary>
    public class LoginStatusRepository_Main : GenericRepository_Main<LoginStatus>, ILoginStatusRepository_Main
    {
        public LoginStatusRepository_Main(DbContext_Main dbContext) : base(dbContext)
        {

        }

        #region == 檢查相關 ==
        /// <summary>
        /// 檢查資料是否存在(ById)
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns>「true，有」「false，無」</returns>
        public bool CheckExist(Guid? id)
        {
            return this.Any(x => x.Id == id);
        }

        /// <summary>
        /// 檢查是否重複(ByNo)
        /// </summary>
        /// <param name="id">Key，沒給新增檢查，有給修改檢查</param>
        /// <param name="no">代號</param>
        /// <returns>「true，重複」「false，不重複」</returns>
        //public bool CheckRepeat_ByNo(long? id, string no)
        //{
        //    var result = true;

        //    // [T：有值，修改檢查][F：無值，新增檢查]
        //    if (id.HasValue)
        //    {
        //        result = this.GetAlls(x => x.No == no && x.Id != id).Any();
        //    }
        //    else
        //    {
        //        result = this.GetAlls(x => x.No == no).Any();
        //    }

        //    return result;
        //}
        #endregion

        #region == 取資料相關 ==
        /// <summary>
        /// 【Queryable】【ref】過濾出登入狀態的Queryable
        /// </summary>
        /// <param name="dbData">ref 登入狀態的Queryable</param>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public void WhereQueryable(ref IQueryable<LoginStatus> dbData, LoginStatus_Filter input)
        {
            // Id
            if (input.Id.HasValue)
            {
                dbData = dbData.Where(x => x.Id == input.Id);
            }

            // 關鍵字
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                dbData = dbData.Where(x => x.UserNo.Contains(input.Keyword) || x.UserName.Contains(input.Keyword));
            }

            // 代號
            if (!string.IsNullOrEmpty(input.No))
            {
                dbData = dbData.Where(x => x.UserNo == input.No);
            }

            // 名稱
            if (!string.IsNullOrEmpty(input.Name))
            {
                dbData = dbData.Where(x => x.UserName == input.Name);
            }

            //// 是否啟用。
            //if (input.Is_Enable.HasValue)
            //{
            //    // [T：啟用][F：關閉]
            //    if (input.Is_Enable.Value)
            //    {
            //        dbData = dbData.Where(x => x.Is_Stop == false);
            //    }
            //    else
            //    {
            //        dbData = dbData.Where(x => x.Is_Stop == true);
            //    }
            //}
        }

        /// <summary>
        /// 【資料】取登入狀態DTO
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public LoginStatus_DTO? GetDTO(LoginStatus_Filter input)
        {
            // 語法命令
            var dbData = this.GetAll();
            // 過濾命令(ref)
            this.WhereQueryable(ref dbData, input);
            // 取出資料
            var result = dbData.Select(x => new LoginStatus_DTO
            {
                Id = x.Id,
                UserId = x.UserId,
            }).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 【資料】取登入資料(依登入狀態)
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public UserSession_Model? GetLoginData(LoginStatus_Filter input)
        {
            UserSession_Model result = null;
            // 語法命令
            var dbData = this.GetAll();
            // 過濾命令(ref)
            this.WhereQueryable(ref dbData, input);
            // 取資料
            var loginStatus = dbData.FirstOrDefault();

            // [有無資料][T：有，整理]
            if (loginStatus != null)
            {
                result = new UserSession_Model
                {
                    LoginId = loginStatus.Id,
                    UserId = loginStatus.UserId,
                    UserNo = loginStatus.UserNo,
                    UserName = loginStatus.UserName,
                    CompanyId = loginStatus.CompanyId,
                    CompanyNo = loginStatus.CompanyNo,
                    CompanyName = loginStatus.CompanyName,
                    CompanyLevelId = loginStatus.CompanyLevelId,
                    Account = loginStatus.Account,
                    Password = loginStatus.Password,
                    Mail = loginStatus.Mail,
                    RoleId = loginStatus.RoleId,
                    RoleName = loginStatus.RoleName,
                    PermissionTypeId = loginStatus.PermissionTypeId,
                    Functions = loginStatus.FunctionId_TXTs.Split(',').EM_ToEnum_Function_ByIntStrings(), // string[]轉Enum
                    IsBackDoor = loginStatus.IsBackDoor,
                    IsNeedCheckPassword = loginStatus.IsNeedCheckPassword,
                    BackDoorTypeId = loginStatus.BackDoorTypeId,
                };
            }

            return result;
        }
        #endregion

        #region == 更新資料相關 ==
        // ...
        #endregion

        #region == 其他 ==
        // ...
        #endregion
    }
}
