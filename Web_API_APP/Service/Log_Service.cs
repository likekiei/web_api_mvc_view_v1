using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Common.Model.Data.Log;
using Main_Common.Model.Main;
using Main_Common.Model.Message;
using Main_Common.Model.Result;
using Main_Common.Model.ResultApi;
using Main_Common.Model.Tool;
using Main_Common.Tool;
using Main_EF.Interface;
using Main_EF.Migrations;
using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_API_APP.Service
{
    /// <summary>
    /// 【Main】Log相關
    /// </summary>
    public class Log_Service
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 資料庫工作單元
        /// </summary>
        public readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 訊息處理
        /// </summary>
        public readonly Message_Tool _Message_Tool;
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
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="messageTool">訊息處理</param>
        public Log_Service(IUnitOfWork unitOfWork,
            MainSystem_DTO mainSystem_DTO,
            Message_Tool messageTool)
        {
            this._unitOfWork = unitOfWork;
            this._MainSystem_DTO = mainSystem_DTO;
            this._Message_Tool = messageTool;
            //this._UserSession_Model = new UserSession_Model();
        }
        #endregion

        //--【方法】=================================================================================

        #region == Log紀錄 ================================================================
        #region == 檢查相關-Log紀錄 ==
        /// <summary>
        /// 檢查是否存在(Log紀錄Id) [true存在]
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Check_IsExist_Log(Guid? key)
        {
            var check = _unitOfWork._LogRepository_Main.GetAlls(x => x.Id == key).Any();
            return check;
        }
        #endregion

        #region == 取資料相關-Log紀錄 ==
        ///// <summary>
        ///// 【單筆】【修改用】取得Log紀錄
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public Log_Model GetModel_Log_Edit(Guid key)
        //{
        //    Log_Model result = null;
        //    var dbData = _unitOfWork._LogRepository_Main.Get(x => x.Id == key);
        //    if (dbData != null)
        //    {
        //        result = new Log_Model
        //        {
        //            CRUD = E_CRUD.U,
        //            //GUID = dbData.GUID,
        //            Id = dbData.Id,
        //            No = dbData.No,
        //            Name = dbData.Name,
        //            Password = dbData.Password,
        //            //Dept_Id = dbData.Dept_Id,
        //            //Role_Id = dbData.Role_Id,
        //            Mail = dbData.Mail,
        //            Is_Stop = dbData.Is_Stop,
        //            Rem = dbData.Rem,
        //            Company_Id = dbData.Company_Id,
        //            DataFrom_Id = dbData.DataFrom_Id,
        //            //File_Bind_Guid = dbData.File_Bind_Guid,
        //        };
        //    }

        //    return result;
        //}

        /// <summary>
        /// 【多筆】【可分頁】取Log紀錄清單
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <param name="pageingDTO">分頁條件</param>
        /// <returns></returns>
        public ResultOutput_Data<List<Log_List>> GetList_Log(Log_Filter input, Pageing_DTO pageingDTO)
        {
            var result = new ResultOutput_Data<List<Log_List>>();
            var dbDatas = _unitOfWork._LogRepository_Main.GetAll();

            #region == 過濾 ==
            // Id
            if (input.Id.HasValue)
            {
                dbDatas = dbDatas.Where(x => x.Id == input.Id);
            }

            if (input.BindKey_ByAction.HasValue)
            {
                dbDatas = dbDatas.Where(x => x.BindKey_ByAction == input.BindKey_ByAction);
            }
            #endregion

            #region == 分頁處理 ==
            // 是否分頁。 [T：不分頁][F：分頁]
            if (pageingDTO == null || pageingDTO.IsEnable == false)
            {
                result.Pageing_DTO.TotalCount = dbDatas.Count();
            }
            else
            {
                pageingDTO.TotalCount = dbDatas.Count();
                result.Pageing_DTO = pageingDTO; //給result值
                dbDatas = dbDatas.OrderBy(o => o.BindKey_ByAction).OrderBy(o => o.SEQ).Skip((pageingDTO.PageNumber - 1) * pageingDTO.PageSize).Take(pageingDTO.PageSize);
            }
            #endregion

            #region == 整理資料 ==
            result.Data = dbDatas.Select(x => new Log_List
            {
                Id = x.Id,
                IsSuccess = x.IsSuccess,
                StatusCodeId = x.StatusCodeId,
                LogTypeId = x.LogTypeId,
                ActionId = x.ActionId,
                //ActionDate = x.ActionDate,
                LogDate = x.LogDate,
                FunctionPath = x.FunctionPath,
                Message = x.Message,
                MessageException = x.MessageException,
                MessageOther = x.MessageOther,
                SEQ = x.SEQ,
                BindKey = x.BindKey,
                BindKey_ByAction = x.BindKey_ByAction,
            }).ToList();
            #endregion

            return result;
        }
        #endregion

        #region == 存資料相關-Log紀錄 ==
        /// <summary>
        /// 新增Log紀錄
        /// </summary>
        /// <remarks>資料來源為 MainSystem_DTO 紀錄的 Log 資料</remarks>
        /// <returns>回傳成功(全部成功才算成功)or失敗</returns>
        public void Create_Log(SLog log)
        {            
            var today = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
            string ipaddr = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipaddr = ip.ToString();
                }
            }
            var addData = new SimpleLog
            {
                EventDateTime = today,
                EventLevel = log.EventLevel,
                Status = log.Status,
                User = log.User,
                IPAddress = ipaddr,
                ActionName = log.ActionName,
                Message = log.Message,
                Data = log.Data,
            };
            #region == 【DB】寫入 (Error continue) ==
            try
            {
                _unitOfWork._SimpleLogRepository_Main.Add(addData);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                //result.IsSuccess = false;
                //errorMsg.Add($"Log寫入DB失敗，資料庫存取異常，BindKey[{itemLog.BindKey}]");
                //continue;
            }
            #endregion

           
            #endregion
          
        }     

        
        #endregion

        #region == Log處理 ==
        /// <summary>
        /// 添加執行的Log紀錄
        /// </summary>
        /// <param name="methodBase">執行方法資訊</param>
        /// <param name="bindKey">綁定用Key</param>
        /// <param name="todayFull">當前完整時間</param>
        /// <param name="eDBTable">資料庫Table</param>
        /// <param name="eLogType">Log種類</param>
        /// <param name="eAction">執行動作</param>
        /// <returns>執行過程是否順利</returns>
        //public bool Add_LogActionRecord(MethodBase methodBase, Guid bindKey, DateTime todayFull, E_DBTable eDBTable, E_LogType eLogType, E_Action eAction)
        public bool Add_LogActionRecord(MethodBase methodBase, Guid bindKey, DateTime todayFull)
        {
            try
            {
                // 取方法完整路徑
                //var functionPath = methodBase.EM_GetFunctionPath();
                // 取下一個次序
                var nextSEQ = _MainSystem_DTO.LogList.OrderByDescending(o => o.LogInfo.SEQ).Select(x => x.LogInfo.SEQ).FirstOrDefault() + 1;

                // 生成Log紀錄
                var addData = new MainSystem_Log
                {
                    BindKey = bindKey,
                    LogInfo = new Log_DTO
                    {
                        IsActionAllSuccess = true, // 還沒處理過，先默認執行過程未發生錯誤
                        IsSuccess = false, // 還沒處理過，先默認失敗
                        //DBTableId = eDBTable,
                        //LogTypeId = eLogType,
                        //ActionId = eAction,
                        DBTableId = this._MainSystem_DTO.DBTableType,
                        LogTypeId = this._MainSystem_DTO.LogType,
                        ActionId = this._MainSystem_DTO.ActionType,
                        StatusCodeId = E_StatusCode.Default,
                        Message = "【執行紀錄】",
                        //FunctionPath = functionPath,
                        FunctionPath = this._MainSystem_DTO.MethodBase.EM_GetFunctionPath(),
                        //CompanyId = this._MainSystem_DTO.UserSession.Company_Id,
                        //CompanyName = this._MainSystem_DTO.UserSession.Company_Name,
                        //UserId = this._MainSystem_DTO.UserSession.User_Id,
                        //UserName = this._MainSystem_DTO.UserSession.User_Name,
                        LogDate = todayFull,
                        SEQ = nextSEQ,
                        BindKey = bindKey,
                        BindKey_ByAction = this._MainSystem_DTO.BindKey_ByAction,
                    },
                    MessageList = new List<Message_DTO>(),
                };

                // [有無登入者資訊][T：有]
                if(this._MainSystem_DTO.UserSession != null)
                {
                    addData.LogInfo.CompanyId = this._MainSystem_DTO.UserSession.CompanyId;
                    addData.LogInfo.CompanyName = this._MainSystem_DTO.UserSession.CompanyName;
                    addData.LogInfo.UserId = this._MainSystem_DTO.UserSession.UserId;
                    addData.LogInfo.UserName = this._MainSystem_DTO.UserSession.UserName;
                }

                // 添加
                this._MainSystem_DTO.LogList.Add(addData);                
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 添加Log的結果訊息
        /// </summary>
        /// <param name="bindKey">綁定用Key</param>
        /// <param name="messageDTO">請求資料</param>
        /// <remarks></remarks>
        /// <returns>執行過程是否順利</returns>
        public bool Add_LogResultMessage(Guid bindKey, Message_DTO messageDTO)
        {
            try
            {
                var log = _MainSystem_DTO.LogList.Where(x => x.BindKey == bindKey).FirstOrDefault();

                //[T：有指定的Log][]
                if (log != null)
                {
                    #region == 設定Log的成功失敗狀態 ==
                    // [T：失敗才設定Log是否全部成功之狀態]
                    if (messageDTO.IsSuccess == false)
                    {
                        switch (messageDTO.E_StatusLevel)
                        {
                            case E_StatusLevel.Default: // 預設狀態也視為失敗
                            case E_StatusLevel.警告:
                                log.LogInfo.IsActionAllSuccess = false;
                                break;
                            case E_StatusLevel.正常:
                            case E_StatusLevel.注意:
                                // 不處理，視為一個成功的執行
                                break;
                            default:
                                // 其他例外狀態都是為失敗
                                log.LogInfo.IsActionAllSuccess = false;
                                break;
                        }
                    }
                    #endregion

                    // 設定下一個次序
                    messageDTO.SEQ = log.MessageList.OrderByDescending(o => o.SEQ).Select(x => x.SEQ).FirstOrDefault() + 1;
                    // 添加
                    log.MessageList.Add(messageDTO);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 設定Log的請求Json
        /// </summary>
        /// <typeparam name="T_QueryData">[泛型]請求資料</typeparam>
        /// <param name="bindKey">綁定用Key</param>
        /// <param name="queryData">請求資料</param>
        /// <remarks></remarks>
        /// <returns>執行過程是否順利</returns>
        public bool Set_LogQueryJson<T_QueryData>(Guid bindKey, T_QueryData queryData)
        {
            try
            {
                var log = _MainSystem_DTO.LogList.Where(x => x.BindKey == bindKey).Select(x => x.LogInfo).FirstOrDefault();

                //[T：有指定的Log][]
                if (log != null)
                {
                    #region == Data ClassName ==
                    try
                    {
                        log.QueryClassName = TypeTool.GetTypeName<T_QueryData>();
                    }
                    catch (Exception)
                    {
                        //不處理例外，影響抓Log的時候會沒辦法轉型成Data Model
                    }
                    #endregion

                    #region == 序列化QueryData ==
                    try
                    {
                        if (queryData != null)
                        {
                            log.QueryJson = JsonSerializer.Serialize(queryData);

                        }
                    }
                    catch (Exception)
                    {
                        log.QueryJson = JsonSerializer.Serialize("序列化失敗");
                    }
                    #endregion
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 重新整理Log資訊
        /// </summary>
        /// <remarks></remarks>
        /// <returns>執行過程是否順利</returns>
        public bool Refresh_LogInfo()
        {
            try
            {
                // 走訪Log迴圈
                foreach (var item in _MainSystem_DTO.LogList)
                {
                    #region == 整理Log資訊 ==
                    // [有無處理訊息][T：有][F：無，默認執行結果為成功]
                    if (item.MessageList.Count() > 0)
                    {
                        // 改成在添加Log訊息的時候就設定Log的IsActionAllSuccess是否為false
                        // [判斷該Log的執行是否皆成功][T：皆成功][F：有失敗]
                        if (item.LogInfo.IsActionAllSuccess)
                        {
                            item.LogInfo.IsSuccess = true;
                            item.LogInfo.StatusCodeId = E_StatusCode.成功;
                        }
                        else
                        {
                            item.LogInfo.IsSuccess = false;
                            item.LogInfo.StatusCodeId = E_StatusCode.失敗;
                        }

                        //// [任意處理訊息為false][T：有false][F：皆為true]
                        //if (item.MessageList.Where(x => x.IsSuccess == false).Any())
                        //{
                        //    // 失敗的訊息項目
                        //    var errorMsgItems = item.MessageList.Where(x => x.IsSuccess == false).ToList();

                        //    var t = errorMsgItems.All(x => x.E_StatusLevel == E_StatusLevel.警告)

                        //    item.LogInfo.IsSuccess = false;
                        //    item.LogInfo.StatusCodeId = E_StatusCode.失敗;
                        //}
                        //else
                        //{
                        //    item.LogInfo.IsSuccess = true;
                        //    item.LogInfo.StatusCodeId = E_StatusCode.成功;
                        //}

                        //依狀態碼整理訊息
                        item.LogInfo.Message = this._Message_Tool.GetMessage_Result(item.MessageList, "");
                        item.LogInfo.MessageException = this._Message_Tool.GetMessageException_Result(item.MessageList, "");
                    }
                    else
                    {
                        item.LogInfo.IsSuccess = true;
                        item.LogInfo.StatusCodeId = E_StatusCode.成功;
                        item.LogInfo.Message = "默認成功";
                        item.LogInfo.MessageException = "";
                        item.LogInfo.MessageOther = "";
                    }
                    #endregion
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 取Log訊息
        /// </summary>
        /// <param name="eLogType">Log種類</param>
        /// <remarks>只取[LogType為處理紀錄]的訊息</remarks>
        /// <returns></returns>
        //public string Get_LogMessage(E_LogType eLogType)
        //{
        //    var result = "";
        //    //var msgList = new List<string>();

        //    try
        //    {
        //        // 只取處理紀錄的Log訊息
        //        var msgList = _MainSystem_DTO.LogList.Where(x => x.LogInfo.LogTypeId == eLogType).Select(x => x.LogInfo.Message);
        //        result = string.Join("\r\n", msgList);
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }

        //    return result;
        //}
        #endregion

        public SLog getip()
        {
            var slog = new SLog();
            string tempIP = string.Empty;
            if (System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Length > 1)
                tempIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();
            //var controller = ControllerContext.RouteData.Values["controller"];
            //var action = ControllerContext.RouteData.Values["action"];

            return slog;
        }
    }
}
