using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Data.User;
using Main_Common.Model.Main;
using Main_Common.Model.Message;
using Main_Common.Model.Result;
using Main_Common.Tool;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_User;

namespace Main_Web.Helper
{
    /// <summary>
    /// 通用檢查相關Helper
    /// </summary>
    public class MyCheckHelper
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        private readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】Log相關
        /// </summary>
        private readonly LogService_Main _LogService_Main;
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="httpContextAccessor">HttpContext</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="logService_Main">Log相關</param>
        public MyCheckHelper(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
        }
        #endregion

        #region == 檢查有無輸入資料 ==
        /// <summary>
        /// 檢查是否有無輸入input
        /// </summary>
        /// <typeparam name="T_Model">資料型別</typeparam>
        /// <param name="methodParam">ref 屬性參數</param>
        /// <param name="input">資料</param>
        /// <returns>回傳結果「true，處理成功and有值」「false，處理異常or無值」</returns>
        public bool Check_IsNullInput<T_Model>(ref MethodParameter methodParam, T_Model input)
        {
            #region == 處理 ==
            try
            {
                #region == 檢查-有無input值 (Error Return) ==
                // [有無input][T：有值][F：無值]
                if (input != null)
                {
                    // 不要在這邊處理，什麼時候想寫入就在哪邊加入就好
                    //// 設定Log的請求資料Json
                    //this._LogService_Main.Set_LogQueryJson<T_Model>(methodParam.BindKey, input);
                }
                else
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.無資料, "");
                    methodParam.MessageDTO.Message = $"未接收到資料";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    // 回傳訊息
                    methodParam.ResultJsonDTO = new ResultJson_DTO(false, "【失敗】", methodParam.MessageDTO.Message)
                    {
                        logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
                    };

                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.處理異常, "");
                methodParam.MessageDTO.Message = $"預期外的錯誤";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                // 回傳訊息
                methodParam.ResultJsonDTO = new ResultJson_DTO(false, "【失敗】", methodParam.MessageDTO.Message)
                {
                    logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
                };

                return false;
            }
            #endregion

            return true;
        }
        #endregion

        #region == 檢查Model驗證是否正確 ==
        /// <summary>
        /// 檢查Model的驗證是否正確
        /// <para>methodParam跟input的BindKey建議是相同的，如果有需要特別處理的話，兩者相異可能會有狀況</para>
        /// </summary>
        /// <typeparam name="T_Model">資料型別</typeparam>
        /// <param name="methodParam">ref 屬性參數</param>
        /// <param name="input">資料</param>
        /// <param name="ignoreNames">要忽略的名稱</param>
        /// <returns>回傳結果「true，處理成功and合理」「false，處理異常or不合理」</returns>
        public bool Check_IsValidModel<T_Model>(ref MethodParameter methodParam, T_Model input, List<string> ignoreNames)
        {
            #region == 處理 ==
            try
            {
                #region == Model驗證 ==
                // 清空
                methodParam.Texts = new List<string>();
                // 檢查驗證
                methodParam.Texts = DataValidationTool.ValidationResult_ShowMsg<T_Model>(input, ignoreNames);
                // [T：有錯誤訊息]
                if (methodParam.Texts != null && methodParam.Texts.Count() > 0)
                {
                    methodParam.ErrorTexts.Add($"資料驗證不符合，{string.Join(";", methodParam.Texts)}");
                }
                #endregion

                #region == 錯誤Log紀錄 (Error return) ==
                // [T：有錯誤訊息]
                if (methodParam.ErrorTexts != null && methodParam.ErrorTexts.Count() > 0)
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"Model驗證錯誤，{string.Join(";", methodParam.ErrorTexts)}";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    // 回傳訊息
                    methodParam.ResultJsonDTO = new ResultJson_DTO(false, "【失敗】", "【Model驗證錯誤】")
                    {
                        logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
                    };

                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.處理異常, methodParam.ComFocusText);
                methodParam.MessageDTO.Message = $"預期外的錯誤";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                // 回傳訊息
                methodParam.ResultJsonDTO = new ResultJson_DTO(false, "【失敗】", methodParam.MessageDTO.Message)
                {
                    logUrl = UrlTool.Get_LogSimpleUrl(_MainSystem_DTO.BindKey_ByAction),
                };

                return false;
            }
            #endregion

            return true;
        }
        #endregion
    }
}
