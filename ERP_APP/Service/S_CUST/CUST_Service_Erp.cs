//using ERP_EF.Models;
//using ERP_EF.Repository;
//using Main_Common.Enum;
//using Main_Common.Enum.E_StatusType;
//using Main_Common.ExtensionMethod;
//using Main_Common.Model.Account;
//using Main_Common.Model.Data;
//using Main_Common.Model.DTO.Cust;
//using Main_Common.Model.ERP.DTO;
//using Main_Common.Model.ERP;
//using Main_Common.Model.Main;
//using Main_Common.Model.Result;
//using Main_Common.Mothod.Message;
//using Main_Common.Tool;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ERP_APP.Service.S_CUST
//{
//    / <summary>
//    / 客戶相關
//    / </summary>
//    public class CUST_Service_Erp
//    {
//        #region == 【DI注入用宣告】 ==
//        / <summary>
//        / 資料庫工作單元
//        / </summary>
//        public readonly IUnitOfWork _unitOfWork;
//        / <summary>
//        / 【DTO】主系統資料
//        / </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        / <summary>
//        / 【Main Service】Log相關
//        / </summary>
//        public readonly LogService_Main _LogService_Main;
//        / <summary>
//        / 【Tool】訊息處理
//        / </summary>
//        public readonly Message_Tool _Message_Tool;
//        / <summary>
//        / ERPDB
//        / </summary>
//        private readonly DB_T020Context _DB_T020Context;
//        #endregion

//        #region == 【全域宣告】 ==
//        / <summary>
//        / 【DTO】全部資料的DTO
//        / </summary>
//        public readonly AllDataDTO _AllDataDTO = new AllDataDTO();
//        #endregion

//        --【建構】=================================================================================

//        #region == 建構 ==
//        / <summary>
//        / 建構
//        / </summary>
//        / <param name = "_DB_T020Context" > ERP資料庫 </ param >
//        / < param name="mainSystem_DTO">主系統資料</param>
//        / <param name = "logService_Main" > Log相關 </ param >
//        / < param name="messageTool">訊息處理</param>
//        public CUST_Service_Erp(
//            DB_T020Context _DB_T020Context,
//            MainSystem_DTO mainSystem_DTO,
//            LogService_Main logService_Main,
//            Message_Tool messageTool)
//        {

//            this._MainSystem_DTO = mainSystem_DTO;
//            this._DB_T020Context = _DB_T020Context;
//            this._LogService_Main = logService_Main;
//            this._Message_Tool = messageTool;
//        }
//        #endregion

//        --【方法】=================================================================================



//        #region == 【全域變數】DB、Service ==
//        /// <summary>
//        /// [Table]客戶表頭
//        /// </summary>
//        private C_ERP_Repository<MF_POS> _MF_POS_Repository;
//        /// <summary>
//        /// [Table]客戶表頭自訂義
//        /// </summary>
//        private C_ERP_Repository<MF_POS_Z> _MF_POS_Z_Repository;
//        /// <summary>
//        /// [Table]客戶表身
//        /// </summary>
//        private C_ERP_Repository<TF_POS> _TF_POS_Repository;
//        /// <summary>
//        /// [Table]產品
//        /// </summary>
//        private C_ERP_Repository<PRDT> _PRDT_Repository;
//        / <summary>
//        / [Table] 客戶
//        / </summary>
//        private C_ERP_Repository<CUST> _CUST_Repository;

//        / <summary>
//        / [Service] 錯訊相關
//        / </summary>
//        private Message_Service _Message_Service;
//        / <summary>
//        / [Service] 庫存相關
//        / </summary>
//        private Stock_Service_Erp _Stock_Service_Erp;
//        #endregion

//        #region == 【全域變數】參數、屬性 ==
//        private UserSession_Model _UserSession_Model = null; //登入者資訊
//        private string Com_MainKey = null; //共用主要Key(使用前請先重置)
//        private string LogErrorMsg = null; //共用Log錯誤訊息(使用前請先重置)
//        private string ResultMsg_Finally = null; //共用最終回傳訊息(使用前請先重置)
//        private bool Com_Check = false; //共用檢查結果(使用前請先重置)
//        private bool Com_IsExist = false; //共用是否存在(使用前請先重置)
//        private bool Com_Result = false; //共用處理結果(使用前請先重置)
//        private ResultOutput Com_Result_DTO = null; //共用結果(使用前請先重置)
//        private List<string> Com_OtherMsg_List = null; //共用其他訊息清單(使用前請先重置)
//        private List<string> Com_TextList = null; //共用文字清單(使用前請先重置)
//        private List<string> Com_Split = null; //共用Split結果(使用前請先重置)
//        private List<SelectItemDTO> DropList_DTO = null; //共用下拉清單(使用前請先重置)

//        private List<Product_DTO> _Product_DTOs = new List<Product_DTO>();
//        private List<Cust_DTO> _Cust_DTOs = new List<Cust_DTO>();
//        #endregion

//        --【建構】=================================================================================

//        #region == 建構 ==
//        / <summary>
//        / 【建構】同源EntityContext
//        / </summary>
//        / <param name = "input" ></ param >
//        public CUST_Service_Erp(UserSession_Model input)
//        {
//            var db = new DB_T020Context();

//            //_MF_POS_Repository = new C_ERP_Repository<MF_POS>(db);
//            //_MF_POS_Z_Repository = new C_ERP_Repository<MF_POS_Z>(db);
//            //_TF_POS_Repository = new C_ERP_Repository<TF_POS>(db);
//            //_PRDT_Repository = new C_ERP_Repository<PRDT>(db);
//            //_CUST_Repository = new C_ERP_Repository<CUST>(db);

//            _UserSession_Model = input; //保存
//        }
//        #endregion

//        --【方法】=================================================================================

//        #region == 客戶(CUST) ==
//        #region == 檢查相關 ==
//        / <summary>
//        / 檢查客戶是否存在(依單號) [true存在]
//        / </summary>
//        / <param name = "no" > 單號 </ param >
//        / < returns > [true：存在] </ returns >
//        public bool Check_IsExist_CUST_ByNo(string no)
//        {
//            var check = _CUST_Repository.GetAlls(x => x.CUS_NO == no).Any();
//            var result = _DB_T020Context.CUST.Where(x => x.CUS_NO == no).Any();
//            return result;
//        }

//        / <summary>
//        / 【單筆】檢查客戶是否存在(依外部單號) [true存在]
//        / </summary>
//        / <param name = "cNo" > 外部單號 </ param >
//        / < returns > [true：存在] </ returns >
//        public bool Check_IsExist_CUST_ByOuterNo(string cNo)
//        {
//            var check = _CUST_Repository.GetAlls(x => x.OS_ID == "SO" && x.C_No == cNo).Any();
//            return check;
//        }

//        / <summary>
//        / 【多筆】檢查客戶是否存在(依外部單號) [true存在]
//        / </summary>
//        / <param name = "cNos" > 外部單號清單 </ param >
//        / < param name="isReturnExistNo">[true：回傳已存在於Erp的單號][true：回傳不存在於Erp的單號]</param>
//        / <returns>[true：存在]</returns>
//        public List<string> Checks_IsExist_POS_ByOuterNo(List<string> cNos, bool isReturnExistNo)
//        {
//            // 存在於Erp的單號
//            var existNos = _MF_POS_Z_Repository.GetAlls(x => x.OS_ID == "SO" && cNos.Contains(x.C_No)).Select(x => x.C_No).Distinct().ToList();

//            // [T：回傳已存在的單號][F：回傳不存在的單號]
//            if (isReturnExistNo)
//            {
//                return existNos;
//            }
//            else
//            {
//                return cNos.Except(existNos).ToList();
//            }
//        }
//        #endregion


//        #region == 取資料相關 ==
//        / <summary>
//        / 【單筆】取客戶相關資料
//        / </summary>
//        / <param name = "no" ></ param >
//        / < returns > Data </ returns >
//        public CUST Get_CUST_Data(string no)
//        {
//            var CUST = _DB_T020Context.CUST.Where(x => x.CUS_NO == no);

//            return (CUST)CUST;
//        }
//        / <summary>
//        / 【多筆】取客戶相關資料
//        / </summary>
//        / <param name = "no" ></ param >
//        / < returns > Data </ returns >
//        public ResultOutput_Data<List<CUST_DTO>> Get_CUST_Datas()
//        {
//            #region == 參數 ==
//            var result = new ResultOutput_Data<List<CUST_DTO>>(true, new List<CUST_DTO>());
//            MessageInfo exceptionDTO = null; //用來紀錄錯誤訊息
//            #endregion

//            var db = _DB_T020Context.CUST.ToList();
//            var dbData = db.ConvertModelList<CUST, CUST_DTO>();
//            result.Data = dbData;

//            return result;
//        }

//        / <summary>
//        / 依據客戶統編抓取客戶代號
//        / </summary>
//        / <param name = "Id" ></ param >
//        / < returns ></ returns >
//        public string GetCusNo(string Id)
//        {
//            無傳入值
//            if (string.IsNullOrEmpty(Id))
//            {
//                return "";
//            }

//            var data = _DB_T020Context.CUST.Where(x => x.UNI_NO == Id);
//            var tt = data.ToList();
//            if (data.Any())
//            {
//                return data.FirstOrDefault().CUS_NO;
//            }
//            else
//            {
//                return "";
//            }
//        }

//        / <summary>
//        / 取得新客戶代號
//        / </summary>
//        / <param name = "date" ></ param >
//        / < returns > 回傳空值，表示失敗</returns>
//        public string GetNo_Cust(DateTime date)
//        {
//            var result = "";
//            try
//            {
//                int yyyy = date.Year;
//                int MM = date.Month;
//                int dd = date.Day;
//                int snNo = 0; //流水號的部份
//                var query = "" + yyyy.ToString("0000") + MM.ToString("00") + dd.ToString("00");

//                取下一個流水號，是否存在相同開頭的單號(不含流水號)
//                [T：取最大的SN][F：默認SN]
//                if (_DB_T020Context.CUST.Where(x => x.CUS_NO.StartsWith(query)).Any())
//                {
//                    snNo = int.Parse(_DB_T020Context.CUST.Where(x => x.CUS_NO.StartsWith(query)).OrderByDescending(o => o.CUS_NO).FirstOrDefault().CUS_NO.Substring(8)) + 1;
//                }
//                else
//                {
//                    snNo = 1;
//                }

//                result = query + snNo.ToString("0000"); //單號生成
//                return result;
//            }
//            catch (Exception)
//            {
//                return "";
//            }
//        }

//        / <summary>
//        / 依訂單單號抓取客戶代號
//        / </summary>
//        / <param name = "orderNo" ></ param >
//        / < returns ></ returns >
//        public string GetCusNo_ByOrderNo(string orderNo)
//        {
//            無傳入值
//            if (string.IsNullOrEmpty(orderNo))
//            {
//                return "";
//            }

//            var data = _DB_T020Context.CUST.Where(x => x.BUSINOTE == orderNo);
//            if (data.Any())
//            {
//                return data.FirstOrDefault().CUS_NO;
//            }
//            else
//            {
//                return "";
//            }
//        }
//        / <summary>
//        / 檢查是否存在客戶
//        / </summary>
//        / <param name = "no" ></ param >
//        / < returns > true存在 </ returns >
//        public bool Check_IsExist_Cust(string no)
//        {
//            var result = _DB_T020Context.CUST.Where(x => x.CUS_NO == no).Any();
//            return result;
//        }
//        #endregion

//        #region == 存資料相關 ==
//        /// <summary>
//        /// 【單筆】新增客戶(入庫拋轉Erp)
//        /// </summary>
//        /// <param name="today"></param>
//        /// <param name="input"></param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<ActionResultByKey_DTO> Create_POS_ByOutBoundToss(DateTime? today, OutBoundTossErp_MF_Model input)
//        {
//            var result = new ResultOutput_Data<ActionResultByKey_DTO>(); //回傳成功的資料
//            //附值查詢
//            var filter = new List<OutBoundTossErp_MF_Model> { input };
//            //取得結果
//            var resultData = this.Creates_POS_ByOutBoundToss(today, filter);
//            //整理結果
//            result.IsSuccess = resultData.IsSuccess;
//            result.E_StatusCode = resultData.E_StatusCode;
//            result.Title = resultData.Title;
//            result.Message = resultData.Message;
//            result.Message_Exception = resultData.Message_Exception;
//            result.Message_Infos = resultData.Message_Infos;
//            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : null;
//            return result;
//        }

//        /// <summary>
//        /// 【多筆】新增客戶(入庫拋轉Erp)
//        /// </summary>
//        /// <param name="today"></param>
//        /// <param name="input"></param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的DTO)</returns>
//        public ResultOutput_Data<List<ActionResultByKey_DTO>> Creates_POS_ByOutBoundToss(DateTime? today, List<OutBoundTossErp_MF_Model> inputs)
//        {
//            _Stock_Service_Erp = new Stock_Service_Erp(_UserSessionModel);
//            _Message_Service = new Message_Service();
//            today = today.HasValue ? today : Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString());
//            var result = new ResultOutput_Data<List<ActionResultByKey_DTO>>(true, new List<ActionResultByKey_DTO>());
//            var message_DTO = new Message_Model(true, $"", null);

//            #region == 本次會用到的資料DTO抓取 ==
//            #region == 【L1】取本次會用到的產品DTO ==
//            var productNos = inputs.SelectMany(x => x.TF_List).Select(x => x.Product_No).Distinct().ToList();
//            _Product_DTOs = _PRDT_Repository.GetAlls(x => productNos.Contains(x.PRD_NO)).Select(x => new Product_DTO
//            {
//                No = x.PRD_NO,
//                Name = x.NAME,
//                Warehouse_No = x.WH,
//            }).ToList();
//            #endregion

//            #region == 【L1】取本次會用到的客戶DTO ==
//            var custNos = inputs.Select(x => x.Cust_No).Distinct().ToList();
//            _Cust_DTOs = _CUST_Repository.GetAlls(x => custNos.Contains(x.CUS_NO)).Select(x => new Cust_DTO
//            {
//                No = x.CUS_NO,
//                Name = x.NAME,
//            }).ToList();
//            #endregion
//            #endregion

//            #region == 處理 ==
//            foreach (var input in inputs)
//            {
//                var tmPOStherMsgs = new List<string>();
//                // 通用的默認關鍵值訊息 (如不一樣請客製)
//                var comFocusTXT = $"Key[{input.ID}]---代號[{input.No}]";
//                // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                var tmpFocusTXT = "";

//                #region == 檢查-表頭-必填 ==
//                // 檢查
//                if (input.ID.HasValue == false
//                    || input.Doc_Date.HasValue == false
//                    || input.Pit_Date.HasValue == false
//                    || string.IsNullOrEmpty(input.No)
//                    )
//                {
//                    message_DTO = new Message_Model(false, E_StatusCode.檢查異常, "", comFocusTXT, input.Bind_Key);
//                    message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;
//                    message_DTO.Message = "客戶，來源入庫單表頭資料存在空值，請檢查以下項目是否有值『Key、單號、單據日期、進站日期』";

//                    result.IsSuccess = false;
//                    result.Message_Infos.Add(message_DTO);
//                    continue;
//                }
//                #endregion

//                #region == 檢查-表身-必填 ==
//                // 走訪表身
//                foreach (var itemTF in input.TF_List)
//                {
//                    // 檢查
//                    if (itemTF.ID.HasValue == false
//                        || itemTF.SN.HasValue == false
//                        || itemTF.MF_Key.HasValue == false
//                        || itemTF.Product_ID.HasValue == false
//                        || itemTF.Qty.HasValue == false
//                        || string.IsNullOrEmpty(itemTF.MF_No)
//                        || string.IsNullOrEmpty(itemTF.Product_No)
//                        )
//                    {
//                        message_DTO = new Message_Model(false, E_StatusCode.檢查異常, "", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;
//                        message_DTO.Message = "客戶，來源入庫單表身資料存在空值，請檢查以下項目是否有值『Key、SN、表頭Key、表頭單號、產品ID、產品代號、數量』";

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        break; // 結束迴圈
//                    }
//                }
//                #endregion

//                #region == 處理 ==
//                Com_IsExist = this.Check_IsExist_POS_ByOuterNo(input.No);
//                // 是否存在。 [T：不存在，新增][F：存在，Error]
//                if (!Com_IsExist)
//                {
//                    POS_Related newData = null;

//                    #region == 生成新增資料+檢查 (Error continue) ==
//                    // 整理+檢查
//                    var newData_Result = this.GenerateCreateCheckData_POS_ByOutBoundToss(input, today.Value);
//                    // [T：成功][F：失敗]
//                    if (newData_Result.IsSuccess)
//                    {
//                        newData = newData_Result.Data;
//                    }
//                    else
//                    {
//                        message_DTO = newData_Result.Message_Infos.FirstOrDefault();

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                    }
//                    #endregion

//                    #region == 【DB】寫入 (Error continue) ==
//                    try
//                    {
//                        _MF_POS_Repository.Create(newData.MF_POS);
//                        _MF_POS_Z_Repository.Create(newData.MF_POS_Z);
//                    }
//                    catch (Exception ex)
//                    {
//                        message_DTO = new Message_Model(false, E_StatusCode.資料存取異常, "客戶，資料庫存取異常", comFocusTXT, input.Bind_Key);
//                        message_DTO.Message_Exception = ex.Message;
//                        message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                    }
//                    #endregion

//                    #region == 庫存調整 (Error Not Stop) ==
//                    try
//                    {
//                        // 不檢查庫存量是否足夠
//                        // 新增默認不會有修改前資料
//                        // 庫存調整用資料
//                        var finStock_Model = new FinStock_Model();
//                        // 修改後資料
//                        finStock_Model.NowData = newData.MF_POS.TF_POS.Select(x => new Stock_Model
//                        {
//                            Product_No = x.PRD_NO,
//                            Warehouse_No = x.WH,
//                            Qty = x.QTY ?? 0,
//                        }).ToList();

//                        // 調整庫存
//                        var result_UpdateStock = this._Stock_Service_Erp.UpdateStock_ByPOS(finStock_Model);
//                        // [T：失敗]
//                        if (result_UpdateStock.IsSuccess == false)
//                        {
//                            tmPOStherMsgs.Add(result_UpdateStock.Message); // 異常訊息
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        tmPOStherMsgs.Add("客戶，庫存調整時，發生例外狀況"); // 異常訊息
//                    }
//                    #endregion

//                    #region == 成功的Message整理 ==
//                    // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                    tmpFocusTXT = $"Key[{input.ID}]---代號[{input.No}]---Erp客戶單號[{newData.MF_POS.OS_NO}]";
//                    message_DTO = new Message_Model(true, E_StatusCode.成功, "客戶，新增拋轉成功", tmpFocusTXT, input.Bind_Key);
//                    message_DTO.Table_Key_Main = input.ID.ToString();
//                    message_DTO.Message_Other = string.Join("\r\n", tmPOStherMsgs);

//                    result.Message_Infos.Add(message_DTO);
//                    result.Data.Add(new ActionResultByKey_DTO
//                    {
//                        ID = input.ID,
//                        No_Erp = newData.MF_POS.OS_NO,
//                    });
//                    #endregion
//                }
//                else
//                {
//                    message_DTO = new Message_Model(false, E_StatusCode.存在相同資料, "客戶，存在相同Key", comFocusTXT, input.Bind_Key);
//                    message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                    result.IsSuccess = false;
//                    result.Message_Infos.Add(message_DTO);
//                    continue;
//                }
//                #endregion
//            }
//            #endregion

//            #region == 整理錯訊 [沒有直接Error，才會執行到這] ==
//            if (result.IsSuccess)
//            {
//                result.Title = "【新增拋轉成功】";
//                result.E_StatusCode = E_StatusCode.成功;
//            }
//            else
//            {
//                result.Title = "【新增拋轉失敗】";
//                result.E_StatusCode = E_StatusCode.失敗;
//            }

//            //依狀態碼整理訊息
//            result.Message = _Message_Service.GetMessage_Result(result.Message_Infos, result.Message);
//            #endregion

//            return result; //沒有Error才會到這
//        }
//        #endregion

//        #region == 整理相關 ==
//        /// <summary>
//        /// 【生成新增資料(含檢查)】客戶資料(入庫拋轉Erp) [含通用檢查]
//        /// </summary>
//        /// <param name="input">請確保資料是與[主系統]執行[新增、修改]時的資料規格一致</param>
//        /// <param name="today">當前時間</param>
//        /// <returns></returns>
//        public ResultOutput_Data<POS_Related> GenerateCreateCheckData_POS_ByOutBoundToss(OutBoundTossErp_MF_Model input, DateTime today)
//        {
//            var result = new ResultOutput_Data<POS_Related>(true, null); // 回傳結果[預設成功]
//            var message_DTO = new Message_Model(true, $"", null);
//            var check = false;
//            var tmpErrorVals = new List<string>(); // 使用前記得重置
//            var tmpErrorMsgs = new List<string>(); // 使用前記得重置

//            // 通用的默認關鍵值訊息 (如不一樣請客製)
//            var comFocusTXT = $"MF_Key[{input.ID}]---代號[{input.No}]";
//            // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//            var tmpFocusTXT = "";

//            #region == 檢查-客戶是否存在 (Error return) ==
//            check = _Cust_DTOs.Where(x => x.No == input.Cust_No).Any();
//            // [T：查無]
//            if (!check)
//            {
//                message_DTO = new Message_Model(false, E_StatusCode.檢查異常, $"客戶，查無客戶，「客戶代號：{input.Cust_No}」", comFocusTXT, input.Bind_Key);
//                message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                result.IsSuccess = false;
//                result.Message_Infos.Add(message_DTO);
//                return result;
//            }
//            #endregion

//            #region == 檢查-表身產品是否存在 (Error return) ==
//            // 清空
//            tmpErrorMsgs = new List<string>();

//            // 走訪表身
//            foreach (var itemTF in input.TF_List)
//            {
//                check = _Product_DTOs.Where(x => x.No == itemTF.Product_No).Any();
//                // [T：查無]
//                if (!check)
//                {
//                    tmpErrorMsgs.Add($"TF_Key[{itemTF.ID}]---SN[{itemTF.SN}]---產品代號[{itemTF.Product_No}]");
//                }
//            }

//            // [是否存在error][T：error]
//            if (tmpErrorMsgs.Count() > 0)
//            {
//                message_DTO = new Message_Model(false, E_StatusCode.檢查異常, $"客戶，查無產品，「{string.Join(",", tmpErrorMsgs)}」", comFocusTXT, input.Bind_Key);
//                message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                result.IsSuccess = false;
//                result.Message_Infos.Add(message_DTO);
//                return result;
//            }
//            #endregion

//            #region == 整理資料-表頭(MF_POS、MF_POS_Z) ==
//            var posNo = this.GetNo_POS(input.Doc_Date.Value); // 客戶單號
//            var payDate = input.Doc_Date.Value.AddMonths(1).AddDays(0 - (input.Doc_Date.Value.Day - 1)); // 結帳期(+1個月)
//            var payEndDate = input.Doc_Date.Value.AddMonths(2).AddDays(0 - (input.Doc_Date.Value.Day - 1)); // 票據到期日(+2個月)
//            var payRem = $"結帳期:{String.Format("{0:yyyy-MM-dd}", payDate)}; 票據到期日:{String.Format("{0:yyyy-MM-dd}", payEndDate)}";

//            var dataMF_POS = new MF_POS
//            {
//                OS_ID = "SO", // 識別碼
//                OS_NO = posNo, // 單號
//                OS_DD = input.Doc_Date.Value.Date, // 單據日期
//                EST_DD = input.Pit_Date.Value.Date, // 預交日
//                CUS_NO = input.Cust_No, // 客戶代號
//                PO_DEP = "00000000", // 採購部門  
//                ZHANG_ID = "2", // 立帳方式(2.不立帳)
//                TAX_ID = "1", // 稅別
//                DIS_CNT = 0, // 折扣
//                SEND_MTH = "1", // 交貨方式
//                PAY_MTH = "1", // 交易類別
//                PAY_DAYS = 1, // 起算日
//                CHK_DAYS = 30, // 票據天數
//                INT_DAYS = 30, // 間隔天數
//                PAY_DD = payDate.Date, // 結帳期
//                CHK_DD = payEndDate.Date, // 票據到期日
//                PAY_REM = payRem, // 交易摘要
//                PRT_SW = "N", // 列印註記
//                CLS_ID = "F", // 結案註記
//                AMTN_INT = 0, // 訂金本位幣金額
//                EXC_RTO = 1, // 匯率
//                CLS_DATE = today.Date,// 終審日期
//                SYS_DATE = today,// 系統時間
//                USR = "ADMIN", // 製單人
//                CHK_MAN = "ADMIN", // 審核人
//                HIS_PRICE = "T", // 是否入歷史單價
//                BACK_ID = "PC", // 銷貨單/受訂退回區分
//                PRE_ID = "F", // 確認交期否
//                HS_ID = "T", // 訂金是否含稅
//                ISOVERSH = "F", // 終審人可越級審核
//            };

//            var dataMF_POS_Z = new MF_POS_Z
//            {
//                OS_ID = "SO", // 識別碼
//                OS_NO = posNo, // 單號
//                C_No = input.No, // 外部單號
//            };
//            #endregion

//            #region == 整理資料-表身(TF_POS) ==
//            var indexTF = 1;

//            // 走訪表身資料
//            foreach (var itemTF in input.TF_List)
//            {
//                // 產品DTO
//                var productDTO = _Product_DTOs.Where(x => x.No == itemTF.Product_No).FirstOrDefault();

//                var addTF_POS = new TF_POS
//                {
//                    ITM = indexTF, // 項次
//                    EST_ITM = itemTF.SN, // 追蹤已交數量項次
//                    PRE_ITM = itemTF.SN, // 退回項次
//                    //OTH_ITM = itemTF.SN, // 轉入單號項次
//                    OS_ID = dataMF_POS.OS_ID, // 識別碼
//                    OS_NO = dataMF_POS.OS_NO, // 客戶號
//                    OS_DD = dataMF_POS.OS_DD, // 受訂日期
//                    EST_DD = dataMF_POS.EST_DD, // 預交日
//                    PRE_EST_DD = dataMF_POS.EST_DD, // 確認交期
//                    BAT_NO = itemTF.Batch_No, // 批號
//                    PRD_NO = itemTF.Product_No, // 貨品代號
//                    PRD_NAME = productDTO.Name, // 貨品名稱
//                    PRD_MARK = "", // 貨品特徵
//                    WH = string.IsNullOrEmpty(productDTO.Warehouse_No) ? "0000" : productDTO.Warehouse_No, // 倉庫代號
//                    UNIT = "1", // 單位
//                    UP = 0, // 單價
//                    UP_STD = 0, // 單位標準成本
//                    QTY = itemTF.Qty, // 數量
//                    QTY_PS = 0, // 已交數量
//                    QTY1_PS = 0, // 已交副數量
//                    AMT = 0, // 外幣金額
//                    AMTN = 0, // 本位幣金額
//                    TAX = 0, // 稅額
//                    TAX_RTO = 5, // 稅率
//                    CST_STD = 0, // 標準成本
//                    FREE_ID_DEF = "F", // 是否定價政策搭贈品
//                    CLS_MP_ID = "1", // 已分析註記
//                };

//                dataMF_POS.TF_POS.Add(addTF_POS);
//                indexTF++;
//            }
//            #endregion

//            // resultData
//            result.Data = new POS_Related
//            {
//                MF_POS = dataMF_POS,
//                MF_POS_Z = dataMF_POS_Z,
//            };

//            return result;
//        }
//        #endregion

//        / <summary>
//        / 【單筆】更新客戶(特別做法)
//        / </summary>
//        / <param name = "input" ></ param >
//        / < param name="isOnlyCreate">只新增</param>
//        / <returns>執行成功，回傳客戶代號</returns>
//        public ResultOutput_Data_ERP<string> Update_Cust_Special(Cust_Input input, bool isOnlyCreate)
//        {
//            var result = new ResultOutput_Data_ERP<string>(true, "", "");
//            input.Today = input.Today.HasValue ? input.Today : Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString());
//            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString());
//            var isCreateEdit = false; //T新增、F修改

//            #region == 增修判斷 ==
//            有無傳入客戶代號。 [T：無][F：有]
//            if (string.IsNullOrEmpty(input.Cust_No))
//            {
//                有無傳入統編。 [T：有][F：無]
//                if (!string.IsNullOrEmpty(input.Uniform_Number))
//                {
//                    依統編查詢No
//                    var custNo_ByUNI = this.GetCusNo(input.Uniform_Number);
//                    input.Cust_No = this.GetCusNo(input.Uniform_Number);
//                    有無客戶。 [T：有][F：無]
//                    if (!string.IsNullOrEmpty(input.Cust_No))
//                    {
//                        isCreateEdit = false;
//                    }
//                    else
//                    {
//                        isCreateEdit = true;
//                        生成客戶代號
//                        input.Cust_No = this.GetNo_Cust(today);
//                        [T：生成客戶代號失敗]
//                        if (string.IsNullOrEmpty(input.Cust_No))
//                        {
//                            return new ResultOutput_Data_ERP<string>(false, $"【客戶更新失敗】生成客戶代號失敗", null);
//                        }
//                    }
//                }
//                else
//                {
//                    有無傳入訂單單號。 [T：有][F：無]
//                    if (!string.IsNullOrEmpty(input.OrderNumber))
//                    {
//                        依訂單單號查詢No
//                        input.Cust_No = this.GetCusNo_ByOrderNo(input.OrderNumber);
//                        有無客戶。 [T：有][F：無]
//                        if (!string.IsNullOrEmpty(input.Cust_No))
//                        {
//                            isCreateEdit = false;
//                        }
//                        else
//                        {
//                            isCreateEdit = true;
//                            生成客戶代號
//                            input.Cust_No = this.GetNo_Cust(today);
//                            [T：生成客戶代號失敗]
//                            if (string.IsNullOrEmpty(input.Cust_No))
//                            {
//                                return new ResultOutput_Data_ERP<string>(false, $"【客戶更新失敗】生成客戶代號失敗", null);
//                            }
//                        }
//                    }
//                    else
//                    {
//                        return new ResultOutput_Data_ERP<string>(false, $"【客戶更新失敗】無[客戶代號、統編、訂單單號]，無法處理客戶資料增修", null);
//                    }
//                }
//            }
//            else
//            {
//                isCreateEdit = false;
//            }
//            #endregion

//            #region == 整理資料 ==
//            2022.05.04 簡稱最多只取名稱前15碼
//            var snm = input.Cust_Name.Length > 15 ? input.Cust_Name.Substring(0, 15) : input.Cust_Name;
//            var custName = !string.IsNullOrEmpty(input.Company_Name) ? input.Company_Name : input.Cust_Name; //全稱
//            var addCust = new CUST
//            {
//                CUS_NO = input.Cust_No, //客戶代號
//                OBJ_ID = "1", //對象別
//                CUS_ARE = "", //區域-全域客戶
//                NAME = input.Cust_Name, //全稱
//                SNM = snm, //簡稱
//                DEP = input.Dept_No, //部門代號
//                NM_ENG = $"TK_API,{today.ToString("yyyy/MM/dd HH:mm:ss")}", //英文名稱(用來註記API更新)
//                UNI_NO = input.Uniform_Number, //統編
//                ADR1 = input.Invoice_Address, //發票地址
//                ADR2 = input.Company_Name, //公司名稱
//                TEL1 = input.Phone_Number, //連絡電話
//                FAX = input.Version.ToString(), //版次
//                BUSINOTE = input.OrderNumber, //營業性質(用來放訂單單號)
//                REM = $"遷入地址:{input.MoveIn_Address};遷出地址:{input.MoveOut_Address};版本號:{input.Version}",
//                CLS_MTH = "1", //結帳類別
//                CLS_DD = 1, //起算日
//                MM_END = 30, //間隔天數
//                CHK_DD = 30, //票據到期日
//                CRD_ID = "1", //信用管制
//                ID1_TAX = "1", //扣稅類別
//                ID2_TAX = "F", //按外幣沖銷否
//                CLS2 = "1", //立帳方式
//                CUS_LEVEL = "1", //客戶等級
//                CHK_CRD = "F", //信用含票據否
//                SO_CRD = "F", //信用額度含訂單否
//                CHK_FAX = "F", //信用額度含傳真匯款否
//                CHK_CUS_IDX = "1", //信用檢測
//                CY_ID = "F", //承運單位否
//                CHK_INCLUDE = "F", //信用額度含下屬
//                CHK_IRP = "F", //信用額度含預收款否
//                CHK_TRP = "F", //信用額度含暫收款否
//                CHK_MAN = "ADMIN", //審核人 //先給ATTN
//                CLS_DATE = today.Date, //審核日期-年月日
//                CHK_PAY1 = "F", //交易管制方式一
//                PAY_FLAG = "1", //帳齡計算方式
//                CHK_PAY2 = "F", //交易管制方式二
//                CHK_PAY3 = "F", //交易管制方式三
//                CHK_QK = "1", //欠款控制
//                USR1 = "ADMIN", //輸入員 //先給ATTN
//                SYS_DATE = today, //輸入日期-年月日時分秒
//                CHK_IRP2 = "F", //收款含預/暫收款否
//                CHK_BARCODE = "1", //條形碼控制
//                DATEFLAG_FQSK = "1", //首付日期方式
//                CODE_NAME = input.Cust_Name, //帳戶名稱
//                CHK_KD = "F", //快遞否
//                NAME_PY = $"{input.Cust_Name}({snm})", //助記碼 [全稱(簡稱)]
//                CHK_ZHANG_ID2 = "T", //信用管制含不立帳銷/退/折單
//                CHK_TYPE_MINXF = "1", //檢測最低消費類型
//                CHK_CK = "T", //信用額度含出庫
//                CHK_TH = "T", //調匯其他收入單
//                MODIFY_DD = today, //最近修改日期
//                MODIFY_MAN = "", //最近修改人
//            };
//            #endregion

//            有無客戶代號。 [T：空值]
//            if (string.IsNullOrEmpty(input.Cust_No))
//            {
//                return new ResultOutput_Data_ERP<string>(false, $"【客戶更新失敗】無客戶代號", null);
//            }

//            #region == 新增/修改 ==
//            [T：新增][F：修改]
//            if (isCreateEdit)
//            {
//                #region == 新增 ==
//                是否存在客戶代號
//               var isExist = _DB_T020Context.CUST.Where(x => x.CUS_NO == input.Cust_No).Any();
//                [T：查無][F：存在]
//                if (!isExist)
//                {
//                    #region == 【DB】新增 ==
//                    try
//                    {
//                        addCust.NM_ENG = $"TK_API,新增[{today.ToString("yyyy/MM/dd HH:mm:ss")}]"; //英文名稱(用來註記API更新)
//                        _DB_T020Context.CUST.Add(addCust);
//                        result.Data = addCust.CUS_NO;
//                    }
//                    catch (Exception ex)
//                    {
//                        return new ResultOutput_Data_ERP<string>(false, $"【客戶新增失敗】資料庫存取異常", null);
//                    }
//                    #endregion
//                }
//                else
//                {
//                    return new ResultOutput_Data_ERP<string>(false, $"【客戶新增失敗】已存在相同客戶代號[{input.Cust_No}]", null);
//                }
//                #endregion
//            }
//            else
//            {
//                #region == 修改 ==
//                取客戶資料
//               var editData = _DB_T020Context.CUST.Where(x => x.CUS_NO == input.Cust_No);
//                [T：存在]
//                if (editData != null)
//                {
//                    暫時不開放修改，默認修改成功
//                    result.Data = addCust.CUS_NO;

//                    editData.NAME = addCust.NAME;
//                    editData.NM_ENG = $"TK_API,修改[{today.ToString("yyyy/MM/dd HH:mm:ss")}]"; //英文名稱(用來註記API更新)
//                    editData.SNM = addCust.NAME;
//                    editData.UNI_NO = addCust.NAME;
//                    editData.TEL1 = addCust.NAME;
//                    editData.ADR1 = addCust.NAME;
//                    editData.BUSINOTE = addCust.NAME;
//                    editData.REM = addCust.NAME;

//                    #region == 【DB】修改 ==
//                    try
//                    {
//                        _CUSTRepository.SaveChanges();
//                        result.Data = addCust.CUS_NO;
//                    }
//                    catch (Exception)
//                    {
//                        return new ResultOutput_Data_ERP<string>(false, $"【客戶修改失敗】資料庫存取異常", null);
//                    }
//                    #endregion
//                }
//                else
//                {
//                    return new ResultOutput_Data_ERP<string>(false, $"【客戶修改失敗】查無客戶[{input.Cust_No}]", null);
//                }
//                #endregion
//            }
//            #endregion

//            return result;
//        }

//        #endregion

//        #region == 其它-客戶 ==
//        /// <summary>
//        ///【單筆】【客戶】取得客戶單號
//        /// </summary>
//        /// <param name="date"></param>
//        /// <returns></returns>
//        public string GetNo_POS(DateTime date)
//        {
//            var presentNO = this.GetNos_POS(date, 1).FirstOrDefault();
//            return presentNO;
//        }

//        /// <summary>
//        /// 【多筆】【客戶】取得客戶單號
//        /// </summary>
//        /// <param name="date"></param>
//        /// <param name="count">要取幾組代號</param>
//        /// <returns></returns>
//        public List<string> GetNos_POS(DateTime date, int count)
//        {
//            var result = new List<string>();
//            int yyyy = date.Year;
//            int MM = date.Month;
//            int dd = date.Day;
//            int snNo; // 流水號的部份
//            string presentNO;
//            var numberPrefix = E_NumberPrefix.客戶.GetEnumDescription();
//            var query = numberPrefix + "_" + yyyy.ToString("0000") + MM.ToString("00") + dd.ToString("00");
//            var queryLength = query.Length;

//            // 取下一個流水號
//            if (_MF_POS_Repository.GetAlls(x => x.OS_NO.StartsWith(query)).Any()) // 取最大的SN
//            {
//                snNo = int.Parse(_MF_POS_Repository.GetAlls(x => x.OS_NO.StartsWith(query)).OrderByDescending(o => o.OS_NO).FirstOrDefault().OS_NO.Substring(queryLength)) + 1;
//            }
//            else // 默認SN
//            {
//                snNo = 1;
//            }

//            // 依需求數遞加流水號
//            for (int i = 0; i < count; i++)
//            {
//                snNo += i == 0 ? 0 : 1; // 遞加i(第一圈不改變，故從0開始)
//                presentNO = query + snNo.ToString("00000"); // 代號生成
//                result.Add(presentNO);
//            }

//            return result;
//        }
//        #endregion
//    }
//}
