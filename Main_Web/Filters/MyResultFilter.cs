using Main_Common.Model.Main;
using Main_Service.Service.S_Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Main_Web.Filters
{
    public class MyResultFilter : IResultFilter
    {
        #region == 【全域宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        public readonly LogService_Main _LogService_Main;
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <remarks>傳入參數有一定的順序規則，請確保傳入與接收參數的順序是對應的</remarks>
        public MyResultFilter(MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //Result 執行前執行
            //context.HttpContext.Response.WriteAsync($"進入 Result Filter。 \r\n");

            // [有無Log清單][T:有，紀錄Log]
            if(this._MainSystem_DTO.LogList.Count() > 0)
            {
                this._LogService_Main.Refresh_LogInfo();
                this._LogService_Main.Create_Log();
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //Result 執行後執行
            //context.HttpContext.Response.WriteAsync($"離開 Result Filter。 \r\n");
        }
    }
}
