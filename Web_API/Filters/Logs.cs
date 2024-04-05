using Main_Common.Enum.E_ProjectType;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Main_Common.GlobalSetting;
using Azure;
using System.Security.Claims;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Service.Service.S_User;
using Main_Common.Model.Main;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Main_EF.Table;
using System.Net.Sockets;
using System.Net;
using Azure.Core;
using Main_Common.Enum.E_StatusType;
using Main_Service.Service.S_Log;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Web_API_APP.Service;
using Main_Common.Model.ResultApi;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Web_API.Filters
{
    /// <summary>
    /// 驗證是否有使用該方法的權限
    /// </summary>
    /// <remarks>傳入參數(Arguments)有一定的順序規則，請確保傳入與接收參數的順序是對應的</remarks>
    public class Logs : IActionFilter, IResultFilter
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        public readonly Log_Service _Log_Service;
        /// <summary>
        /// 【Tool】通用工具
        /// </summary>
        //public readonly ToolCom _ToolCom;
        /// <summary>
        /// TempData相關操作
        /// </summary>
        //private readonly ITempDataDictionaryFactory _TempDataDictionaryFactory;
        #endregion

        #region == 【全域宣告】 ==
        /// <summary>
        /// 執行動作
        /// </summary>
        //public readonly E_Action _ActionEnum;
        /// <summary>
        /// 執行目標
        /// </summary>
        //public readonly E_ActionTarget _ActionTargetEnum;
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <remarks>
        /// <para>傳入參數順序(執行動作, 執行目標, 訊息)</para>
        /// <para>傳入參數有一定的順序規則，請確保傳入與接收參數的順序是對應的</para>
        /// </remarks>
        /// <param name="actionEnum">執行動作</param>
        /// <param name="actionTargetEnum">執行目標</param>
        /// <param name="tempDataDictionaryFactory">TempData相關操作</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="toolCom">通用工具</param>
        public Logs(
            //E_Action actionEnum,
            //E_ActionTarget actionTargetEnum,
            //ITempDataDictionaryFactory tempDataDictionaryFactory,
            MainSystem_DTO mainSystem_DTO,
            Log_Service log_Service
            )
            //ToolCom toolCom)
        {
            //this._ActionEnum = actionEnum;
            //this._ActionTargetEnum = actionTargetEnum;
            //this._TempDataDictionaryFactory = tempDataDictionaryFactory;
            this._MainSystem_DTO = mainSystem_DTO;
            this._Log_Service = log_Service;
            //this._ToolCom = toolCom;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// 在執行 Action 之前執行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.Controller.us
            // 取用於登入驗證的身份
            //var identity = context.HttpContext.User.Identities.Where(x => x.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme).FirstOrDefault();

            var slog = new SLog();
            //var today = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
            //ip
            //slog.IPAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            //controller/action
            slog.ActionName = string.Format("{0}/{1}",
                    context.RouteData.Values["controller"].ToString(),
                    context.RouteData.Values["action"].ToString());
            //UserSessionModel 
            // 參數資訊
            slog.Data = JsonConvert.SerializeObject(context.ActionArguments, new JsonSerializerSettings()
            {
                ContractResolver = new ReadablePropertiesOnlyResolver()
            });
            slog.EventLevel = "Info";
            slog.Status = "ActionEcecuting";
            slog.Message = "OK";
            slog.User = "filter";
            _Log_Service.Create_Log(slog);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
            //..
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //throw new NotImplementedException();
            //..

        }

        /// <summary>
        /// 在執行 Action 之後執行
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            var slog = new SLog();
            //slog.IPAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            //controller/action
            slog.ActionName = string.Format("{0}/{1}",
                    context.RouteData.Values["controller"].ToString(),
                    context.RouteData.Values["action"].ToString());
            //UserSessionModel
            // 參數資訊
            //slog.Data = JsonConvert.SerializeObject(context.Result) ;
            slog.Data = JsonConvert.SerializeObject(context.Result, new JsonSerializerSettings()
            {
                ContractResolver = new ReadablePropertiesOnlyResolver()
            });
            //取 context/Result/DeclardType Name的值
            //var DeclaredType = context.Result.GetType().GetProperty("DeclaredType");
            //var DeclaredType_V = DeclaredType.GetValue(context.Result);
            //var DeclaredType_Name = DeclaredType_V.GetType().GetProperty("Name");
            //var DeclaredType_Name_V = DeclaredType_Name.GetValue(DeclaredType_V).ToString();

            //var obj = JsonConvert.DeserializeObject<Login_Token>(slog.Data);           
            slog.EventLevel = "Info";
            slog.Status = "ResultExecuted";
            slog.Message = "OK";           
            slog.User = "filter";

            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                ["Result Type"]=context.Result.GetType().Name,
            };

            _Log_Service.Create_Log(slog);
        }

        /// <summary>
        /// 訊息回傳
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="message"></param>
        //private static void SetErrorResult(ActionExecutingContext filterContext, string controller, string action, string message)
        //{
        //    // 判斷是否為Ajax請求
        //    bool isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //    // [T：Ajax請求][F：非Ajax請求]
        //    if (isAjax) //ajax的請求
        //    {
        //        var jsonResult = new JsonResult("");
        //        //jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //        jsonResult.Value = new { _isSuccess = false, _message = message };
        //        filterContext.Result = jsonResult;
        //    }
        //    else //非ajax的請求
        //    {
        //        ((Controller)filterContext.Controller).TempData["Message"] = message;
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = action }));
        //    }
        //}

        /// <summary>
        /// JsonSerializer 讀取屬性的解析器設定
        /// </summary>
        class ReadablePropertiesOnlyResolver : DefaultContractResolver
        {
            /// <summary>
            /// 建立可呈現（解析）的屬性
            /// </summary>
            /// <returns>呈現的屬性</returns>
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);
                if (typeof(Stream).IsAssignableFrom(property.PropertyType))
                {
                    property.Ignored = true;
                }
                return property;
            }
        }
        
        /// <summary>
        /// 來源 IP
        /// </summary>
        /// <returns></returns>
        public string GetClientIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }



    }
}
