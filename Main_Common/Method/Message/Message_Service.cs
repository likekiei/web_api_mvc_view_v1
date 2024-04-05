using Main_Common.Enum;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Error;
using Main_Common.Model.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Mothod.Message
{
    /// <summary>
    /// 錯訊相關
    /// </summary>
    public class Message_Service
    {
        /// <summary>
        /// 整理訊息文字
        /// </summary>
        /// <param name="msgInfos">Message資訊</param>
        /// <param name="title">標題</param>
        /// <param name="message">訊息</param>
        /// <param name="statusCode">狀態碼</param>
        /// <returns></returns>
        private string GetMessageString(List<Message_Model> msgInfos, E_StatusCode statusCode, string title, string message)
        {
            var tempMessage_List = new List<string>();
            switch (statusCode)
            {
                case E_StatusCode.成功:
                    tempMessage_List =
                        msgInfos.Where(x => x.E_StatusCode == statusCode && x.Message_Other != null && x.Message_Other != "")
                                .Select(x => "【成功須注意】"
                                           + (!string.IsNullOrEmpty(x.Message_Other) ? $"，【Other】{x.Message_Other}" : "")
                                           + (!string.IsNullOrEmpty(x.Focus_TXT) ? $"，{x.Focus_TXT}" : "")
                                           ).ToList();
                    break;
                // 其餘狀態
                default:
                    tempMessage_List =
                        msgInfos.Where(x => x.E_StatusCode == statusCode)
                                .Select(x => x.Message
                                           + (!string.IsNullOrEmpty(x.Focus_TXT) ? $"，{x.Focus_TXT}" : "")
                                           + (!string.IsNullOrEmpty(x.Message_Other) ? $"，【Other】{x.Message_Other}" : "")
                                           ).ToList();
                    break;
            }

            // 有Title
            if (!string.IsNullOrEmpty(title))
            {
                message += string.IsNullOrEmpty(message) ? $"{title}\r\n" : $"\r\n{title}\r\n";
            }

            //message += string.IsNullOrEmpty(message) ? $"{title}\r\n" : $"\r\n{title}\r\n";
            message += string.Join(";\r\n", tempMessage_List);
            return message;
        }

        /// <summary>
        /// 整理訊息文字 【舊版V1】
        /// </summary>
        /// <param name="msgInfos">Message資訊</param>
        /// <param name="title">標題</param>
        /// <param name="message">訊息</param>
        /// <param name="statusCode">狀態碼</param>
        /// <returns></returns>
        //private string GetMessageString(List<Message_Model> msgInfos, E_StatusCode statusCode, string title, string message)
        //{
        //    var tempMessage_List = new List<string>();
        //    tempMessage_List =
        //        msgInfos.Where(x => x.E_StatusCode == statusCode)
        //                .Select(x => x.Message
        //                           + (!string.IsNullOrEmpty(x.Focus_TXT) ? $"，{x.Focus_TXT}" : "")
        //                           + (!string.IsNullOrEmpty(x.Message_Other) ? $"，【Other】{x.Message_Other}" : "")
        //                           ).ToList();
        //    message += string.IsNullOrEmpty(message) ? $"{title}\r\n" : $"\r\n{title}\r\n";
        //    message += string.Join(";\r\n", tempMessage_List);
        //    return message;
        //}

        /// <summary>
        /// 整理訊息文字
        /// </summary>
        /// <param name="msgInfos">Message資訊</param>
        /// <param name="title">標題</param>
        /// <param name="message">訊息</param>
        /// <param name="statusCode">狀態碼</param>
        /// <returns></returns>
        private string GetErrorMessageString(List<Exception_Model> msgInfos, E_StatusCode statusCode, string title, string message)
        {
            var tempMessage_List = new List<string>();
            tempMessage_List = msgInfos.Where(x => x.E_StatusCode == statusCode).Select(x => x.Message).ToList();
            message += string.IsNullOrEmpty(message) ? $"{title}\r\n" : $"\r\n{title}\r\n";
            message += string.Join(";\r\n", tempMessage_List);
            return message;
        }

        /// <summary>
        /// 取得回傳的訊息 (目前沒整理成功的訊息)
        /// </summary>
        /// <param name="msgInfos">Message資訊</param>
        /// <param name="nowMessage">當前訊息</param>
        /// <returns></returns>
        public string GetMessage_Result(List<Message_Model> msgInfos, string nowMessage)
        {
            var resultMsg = nowMessage;

            // 依狀態碼整理訊息
            foreach (var itemStatusCode in msgInfos.Select(x => x.E_StatusCode).Distinct())
            {
                switch (itemStatusCode)
                {
                    case E_StatusCode.Default:
                        break;
                    case E_StatusCode.成功:
                        resultMsg += this.GetMessageString(msgInfos, itemStatusCode, "", resultMsg);
                        break;
                    // 其餘狀態
                    default:
                        resultMsg += this.GetMessageString(msgInfos, itemStatusCode, $"【注意-{itemStatusCode.ToString()}】", resultMsg);
                        break;
                }
            }

            return resultMsg;
        }

        /// <summary>
        /// 取得回傳的訊息 (Excel訊息DTO)(只整理錯誤訊息)
        /// </summary>
        /// <param name="msgInfos">Message資訊</param>
        /// <param name="nowMessage">當前訊息</param>
        /// <returns></returns>
        public string GetExcelMessage_Result(List<ExcelMessage_Model> msgInfos, string nowMessage)
        {
            var resultMsg = nowMessage;
            var tempMessage_List = new List<string>();

            // 只整理失敗的
            foreach (var item in msgInfos.Where(x => x.IsSuccess == false))
            {
                tempMessage_List.Add($"RowCel[{item.Row_Index}][{item.Cel_Index}]---【Message】{item.Message}");
            }

            // [T：有失敗，回寫整理]
            if (tempMessage_List.Count() > 0)
            {
                var tempTXT = string.Join(";\r\n", tempMessage_List);
                resultMsg += string.IsNullOrEmpty(resultMsg) ? tempTXT : $"\r\n{tempTXT}";
            }

            return resultMsg;
        }

        /// <summary>
        /// 整理Excel用的訊息
        /// </summary>
        /// <param name="msgExcelInfos">[ref]Excel訊息清單</param>
        /// <param name="msgInfos">處理結果訊息清單</param>
        /// <returns>正常不回傳，回傳例外訊息</returns>
        public string Process_ExcelMessage_Model(ref List<ExcelMessage_Model> msgExcelInfos, List<Message_Model> msgInfos)
        {
            try
            {
                #region == 處理 ==
                // 取訊息的BindKey清單
                var bindKeys = msgInfos.Where(x => x.Bind_Key.HasValue).Select(x => x.Bind_Key).ToList();

                // 走訪BindKey清單，處理相同BindKey的資料
                foreach (var bindKey in bindKeys)
                {
                    // 取目標DTO
                    var excelMsgDTO = msgExcelInfos.Where(x => x.Bind_Key == bindKey).FirstOrDefault();
                    // [T：有值，處理]
                    if (excelMsgDTO != null)
                    {
                        // 只取非成功的Message
                        var tempMessage_List = msgInfos
                            .Where(x => x.Bind_Key == bindKey && x.E_StatusCode != E_StatusCode.成功)
                            .Select(x => x.Title + x.Message)
                            .ToList();

                        // [T：有錯訊，整理]
                        if (tempMessage_List.Count() > 0)
                        {
                            // 整理填入Message
                            var tempTXT = string.Join(";\r\n", tempMessage_List);
                            excelMsgDTO.Message += string.IsNullOrEmpty(excelMsgDTO.Message) ? tempTXT : $"\r\n{tempTXT}";
                            excelMsgDTO.IsSuccess = false;
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }
    }
}
