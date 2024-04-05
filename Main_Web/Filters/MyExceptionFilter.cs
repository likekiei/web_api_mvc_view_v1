using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Main;
using Main_Common.Model.Message;
using Main_Common.Model.Result;
using Main_Common.Tool;
using Main_Service.Service.S_Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualBasic;
using System.Reflection;

namespace Main_Web.Filters
{
    /// <summary>
    /// 例外過濾器
    /// </summary>
    public class MyExceptionFilter : IExceptionFilter
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

        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <remarks>傳入參數有一定的順序規則，請確保傳入與接收參數的順序是對應的</remarks>
        public MyExceptionFilter(MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
            this._tempDataDictionaryFactory = tempDataDictionaryFactory;
        }
        #endregion

        //--【方法】=================================================================================

        public void OnException(ExceptionContext context)
        {
            //發生Exception時執行
            //context.HttpContext.Response.WriteAsync($"進入 Exception Filter \r\n");

            // [是否執行過][T：沒執行過]
            if(this._MainSystem_DTO.IsRunExceptionFilter == false)
            {
                // 標記為執行過例外過濾器，不知道為什麼，這OnException會多次執行
                this._MainSystem_DTO.IsRunExceptionFilter = true;

                // 方法的通用屬性參數
                var methodParam = new MethodParameter(this._MainSystem_DTO.BindKey_ByException);

                // [檢查有無Log清單][無，添加首筆Log]
                if (this._MainSystem_DTO.LogList.Count() == 0)
                {
                    // 添加執行Log
                    this._LogService_Main.Add_LogActionRecord(this._MainSystem_DTO.MethodBase, methodParam.BindKey, methodParam.TodayFull);
                }

                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, this._MainSystem_DTO.BindKey_ByException, E_StatusLevel.警告, E_StatusCode.處理異常, null);
                methodParam.MessageDTO.Message = $"執行過程發生例外錯誤，建議聯絡相關人員";
                methodParam.MessageDTO.Message_Exception = context.Exception.Message;
                this._LogService_Main.Add_LogResultMessage(methodParam.MessageDTO.Bind_Key.Value, methodParam.MessageDTO);
                // 整理Log
                this._LogService_Main.Refresh_LogInfo();
                // 新增Log
                this._LogService_Main.Create_Log();

                // 回傳訊息
                var resultDTO = new ResultJson_DTO(false, "【例外狀況】", methodParam.MessageDTO.Message)
                {
                    logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
                };

                this.SetErrorResult(context, "Account", "Login", resultDTO);
            }
        }

        /// <summary>
        /// 訊息回傳
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="message"></param>
        private void SetErrorResult(ExceptionContext filterContext, string controller, string action, ResultJson_DTO resultDTO)
        {
            // 判斷是否為Ajax請求
            bool isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            // [T：Ajax請求][F：非Ajax請求]
            if (isAjax) //ajax的請求
            {
                var jsonResult = new JsonResult("");
                //jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                //jsonResult.Value = new { isSuccess = false, message = resultDTO.message };
                jsonResult.Value = resultDTO;
                filterContext.Result = jsonResult;
            }
            else //非ajax的請求
            {
                //filterContext.Result = new JsonResult
                //{
                //    SerializerSettings = 
                //};

                //filterContext.HttpContext.Response.con

                //TempData["a"] = "A";

                //((Controller)filterContext.Controller).TempData["Message"] = message;
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = action }));

                var tempData = _tempDataDictionaryFactory.GetTempData(filterContext.HttpContext);
                tempData.Add("Message", resultDTO.message);
                filterContext.Result = new RedirectToActionResult(action, controller, null);
            }
        }
    }
}
