using Main_Common.Model.Account;
using Main_Common.Model.Data.Connect;
using Main_Common.Model.Result;
using Main_Common.Tool;
using Main_EF.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Main_Service.Service.S_ConnectSetting
{
    /// <summary>
    /// 【Main】連線設定相關
    /// </summary>
    public class ConnectSettingService_Main
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 資料庫工作單元
        /// </summary>
        public readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 訊息處理相關
        /// </summary>
        public readonly Message_Tool _Message_Tool;
        /// <summary>
        /// 登入者資訊
        /// </summary>
        private readonly UserSession_Model _UserSession_Model;
        #endregion

        #region == 【全域宣告】 ==
        // ...
        #endregion

        //--【建構】=================================================================================

        #region == 建構 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="unitOfWork">資料庫工作單元</param>
        /// <param name="messageTool">訊息處理相關</param>
        public ConnectSettingService_Main(
            IUnitOfWork unitOfWork, 
            Message_Tool messageTool)
        {
            _unitOfWork = unitOfWork;
            _Message_Tool = messageTool;
            _UserSession_Model = new UserSession_Model();
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// 檢查DB連線
        /// </summary>
        /// <param name="isUpdateMigrations">是否為資料庫移轉的檢查</param>
        /// <returns></returns>
        public ResultSimple CheckDbLink(bool isUpdateMigrations)
        {
            var result = _unitOfWork.Check_DbLink(isUpdateMigrations);
            return result;
        }

        /// <summary>
        /// 更新資料庫移轉
        /// </summary>
        /// <param name="targetMigrationVersion">指定移轉目標版本(如果沒給，則移轉至最新)</param>
        /// <returns></returns>
        public ResultSimple Update_DbMigration(string? targetMigrationVersion)
        {
            var result = _unitOfWork.Update_DbMigration(targetMigrationVersion);
            return result;
        }
    }
}
