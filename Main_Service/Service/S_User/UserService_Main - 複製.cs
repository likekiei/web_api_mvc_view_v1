//using Main_Common.Model.Result;
//using Main_Common.Model.Tool;
//using Main_Common.Model.Data.User;
//using Main_EF.Interface;
//using Main_EF.Table;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using Main_Common.Enum.E_ProjectType;
//using Main_Common.Enum.E_StatusType;
//using Main_Common.Model.Account;
//using Main_Common.Model.Message;
//using Main_Common.Tool;
//using Main_EF.Interface.ITableRepository;

//namespace Main_Service.Service.S_User
//{
//    /// <summary>
//    /// 【Main】使用者相關
//    /// </summary>
//    public class UserService_Main
//    {
//        #region == 【全域宣告】 ==
//        /// <summary>
//        /// 資料庫工作單元
//        /// </summary>
//        public IUnitOfWork _unitOfWork;
//        /// <summary>
//        /// 訊息處理
//        /// </summary>
//        public Message_Tool _Message_Tool;
//        /// <summary>
//        /// 登入者資訊
//        /// </summary>
//        private UserSession_Model _UserSession_Model;
//        #endregion

//        //--【建構】=================================================================================

//        #region == 建構 ==
//        /// <summary>
//        /// 建構
//        /// </summary>
//        /// <param name="unitOfWork">資料庫工作單元</param>
//        public UserService_Main(IUnitOfWork unitOfWork, 
//            Message_Tool messageTool)
//        {
//            _unitOfWork = unitOfWork;
//            _Message_Tool = messageTool;
//            _UserSession_Model = new UserSession_Model();
//        }
//        #endregion

//        //--【方法】=================================================================================

//        #region == 使用者 ================================================================
//        #region == 檢查相關-使用者 ==
//        /// <summary>
//        /// 檢查是否存在(使用者ID) [true存在]
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public bool Check_IsExist_User(long? key)
//        {
//            var check = _unitOfWork._UserRepository.GetAlls(x => x.ID == key).Any();
//            return check;
//        }

//        /// <summary>
//        /// 檢查是否存在(使用者代號) [true存在]
//        /// </summary>
//        /// <param name="companyID">公司Key</param>
//        /// <param name="no">使用者代號</param>
//        /// <returns></returns>
//        public bool Check_IsExist_User(long companyID, string no)
//        {
//            var check = _unitOfWork._UserRepository.GetAlls(x => x.Company_ID == companyID && x.No == no).Any();
//            return check;
//        }

//        /// <summary>
//        /// 檢查是否重複(使用者代號) [true存在]
//        /// </summary>
//        /// <param name="companyID">公司Key</param>
//        /// <param name="key">沒給新增檢查，有給修改檢查</param>
//        /// <param name="no">代號</param>
//        /// <returns></returns>
//        public bool Check_Repeat_User_ByNo(long companyID, long? key, string no)
//        {
//            var result = true;

//            // [T：有值，修改檢查][F：無值，新增檢查]
//            if (key.HasValue)
//            {
//                result = _unitOfWork._UserRepository.GetAlls(x => x.Company_ID == companyID && x.No == no && x.ID != key.Value).Any();
//            }
//            else
//            {
//                result = _unitOfWork._UserRepository.GetAlls(x => x.Company_ID == companyID && x.No == no).Any();
//            }

//            return result;
//        }
//        #endregion

//        #region == 取資料相關-使用者 ==
//        /// <summary>
//        /// 【單筆】【修改用】取得使用者
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public User_Model GetModel_User_Edit(long key)
//        {
//            User_Model result = null;
//            var dbData = _unitOfWork._UserRepository.Get(x => x.ID == key);
//            if (dbData != null)
//            {
//                result = new User_Model
//                {
//                    CRUD = E_CRUD.U,
//                    //GUID = dbData.GUID,
//                    ID = dbData.ID,
//                    No = dbData.No,
//                    Name = dbData.Name,
//                    Password = dbData.Password,
//                    //Dept_ID = dbData.Dept_ID,
//                    //Role_ID = dbData.Role_ID,
//                    Mail = dbData.Mail,
//                    Is_Stop = dbData.Is_Stop,
//                    Rem = dbData.Rem,
//                    Company_ID = dbData.Company_ID,
//                    DataFrom_ID = dbData.DataFrom_ID,
//                    //File_Bind_Guid = dbData.File_Bind_Guid,
//                };
//            }

//            return result;
//        }

//        /// <summary>
//        /// 【多筆】【可分頁】取使用者清單
//        /// </summary>
//        /// <param name="input">查詢條件</param>
//        /// <param name="pageingDTO">分頁條件</param>
//        /// <returns></returns>
//        public ResultOutput_Data<List<User_List>> GetList_User(User_Filter input, Pageing_DTO pageingDTO)
//        {
//            var result = new ResultOutput_Data<List<User_List>>();
//            var dbDatas = _unitOfWork._UserRepository.GetAll();

//            #region == 過濾 ==
//            // ID
//            if (input.ID.HasValue)
//            {
//                dbDatas = dbDatas.Where(x => x.ID == input.ID);
//            }

//            // 關鍵字
//            if (!string.IsNullOrEmpty(input.Keyword))
//            {
//                dbDatas = dbDatas.Where(x => x.No.Contains(input.Keyword) || x.Name.Contains(input.Keyword));
//            }

//            // 代號
//            if (!string.IsNullOrEmpty(input.No))
//            {
//                dbDatas = dbDatas.Where(x => x.No == input.No);
//            }

//            // 名稱
//            if (!string.IsNullOrEmpty(input.Name))
//            {
//                dbDatas = dbDatas.Where(x => x.Name == input.Name);
//            }

//            //// 部門
//            //if (input.Dept_ID.HasValue)
//            //{
//            //    dbDatas = dbDatas.Where(x => x.Dept_ID == input.Dept_ID);
//            //}

//            //// 角色
//            //if (input.Role_ID.HasValue)
//            //{
//            //    dbDatas = dbDatas.Where(x => x.Role_ID == input.Role_ID);
//            //}

//            // 公司
//            if (input.Company_ID.HasValue)
//            {
//                dbDatas = dbDatas.Where(x => x.Company_ID == input.Company_ID);
//            }
//            #endregion

//            #region == 分頁處理 ==
//            // 是否分頁。 [T：不分頁][F：分頁]
//            if (pageingDTO == null || pageingDTO.IsEnable == false)
//            {
//                result.Pageing_DTO.TotalCount = dbDatas.Count();
//            }
//            else
//            {
//                pageingDTO.TotalCount = dbDatas.Count();
//                result.Pageing_DTO = pageingDTO; //給result值
//                dbDatas = dbDatas.OrderBy(o => o.No).Skip((pageingDTO.PageNumber - 1) * pageingDTO.PageSize).Take(pageingDTO.PageSize);
//            }
//            #endregion

//            #region == 整理資料 ==
//            result.Data = dbDatas.Select(x => new User_List
//            {
//                ID = x.ID,
//                No = x.No,
//                Name = x.Name,
//                Mail = x.Mail,
//                Dept_ID = x.Dept_ID,
//                //Dept_Name = x.Dept_Info.Name,
//                Role_ID = x.Role_ID,
//                //Role_Name = x.Role_Info.Name,
//                Rem = x.Rem,
//                Company_ID = x.Company_ID,
//                //Company_Name = x.Company_Info.Name,
//            }).ToList();
//            #endregion

//            return result;
//        }
//        #endregion

//        #region == 存資料相關-使用者 ==
//        /// <summary>
//        /// 【單筆】新增使用者
//        /// </summary>
//        /// <param name="input">請確保資料是與[主系統]執行[新增]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<long?> Create_User(User_Model input)
//        {
//            var result = new ResultOutput_Data<long?>(); //回傳成功的資料
//            //附值查詢
//            var filter = new List<User_Model> { input };
//            //取得結果
//            var resultData = this.Creates_User(filter);
//            //整理結果
//            result.IsSuccess = resultData.IsSuccess;
//            result.E_StatusCode = resultData.E_StatusCode;
//            result.Title = resultData.Title;
//            result.Message = resultData.Message;
//            result.Message_Exception = resultData.Message_Exception;
//            //result.Message_Infos = resultData.Message_Infos;
//            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
//            return result;
//        }

//        /// <summary>
//        /// 【多筆】新增使用者
//        /// </summary>
//        /// <param name="input">請確保資料是與[主系統]執行[新增]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<List<long>> Creates_User(List<User_Model> inputs)
//        {
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            var result = new ResultOutput_Data<List<long>>(true, new List<long>());
//            var message_DTO = new Message_DTO(true, $"", null);
//            var Com_IsExist = false;

//            #region == 迴圈處理 ==
//            foreach (var input in inputs)
//            {
//                // 通用的默認關鍵值訊息 (如不一樣請客製)
//                var comFocusTXT = $"公司ID[{input.Company_ID}]---Key[{input.ID}]---代號[{input.No}]";
//                // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                var tmpFocusTXT = "";

//                #region == 檢查-必填 ==
//                // 表頭null檢查
//                if (input.Company_ID.HasValue == false
//                    || string.IsNullOrEmpty(input.No)
//                    || string.IsNullOrEmpty(input.Name)
//                    )
//                {
//                    message_DTO = new Message_DTO(false, E_StatusCode.檢查異常, "", comFocusTXT, input.Bind_Key);
//                    message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;
//                    message_DTO.Message = "請檢查以下項目是否有值[公司、代號、名稱]";

//                    result.IsSuccess = false;
//                    result.Message_Infos.Add(message_DTO);
//                    continue;
//                }
//                #endregion

//                #region == 檢查-資料來源地是否可處理 ==
//                switch (input.DataFrom_ID)
//                {
//                    case E_DataFrom.主系統:
//                    case E_DataFrom.ERP導入:
//                    case E_DataFrom.Excel匯入:
//                        break;
//                    default:
//                        message_DTO = new Message_DTO(false, E_StatusCode.檢查異常, "無法處理的資料來源", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                }
//                #endregion

//                #region == 處理 ==
//                // 是否存在
//                Com_IsExist = this.Check_IsExist_User(input.ID);
//                // [T：不存在，新增][F：存在，Error]
//                if (!Com_IsExist)
//                {
//                    User_Model newData = input;

//                    #region == 檢查-權限 ==
//                    //// 檢查
//                    //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, input.Company_ID);
//                    //// [T：失敗]
//                    //if (!Com_Result_DTO.IsSuccess)
//                    //{
//                    //    message_DTO = new Message_DTO(false, E_StatusCode.權限異常, Com_Result_DTO.Message, comFocusTXT, input.Bind_Key);
//                    //    message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                    //    result.IsSuccess = false;
//                    //    result.Message_Infos.Add(message_DTO);
//                    //    continue;
//                    //}
//                    #endregion

//                    #region == 生成新資料+檢查 ==
//                    //// 整理+檢查
//                    //var newData_Result = this.Generate_UpdataData_User(input, today);
//                    //// [T：成功][F：失敗]
//                    //if (newData_Result.IsSuccess)
//                    //{
//                    //    newData = newData_Result.Data;
//                    //}
//                    //else
//                    //{
//                    //    message_DTO = newData_Result.Message_Infos.FirstOrDefault();

//                    //    result.IsSuccess = false;
//                    //    result.Message_Infos.Add(message_DTO);
//                    //    continue;
//                    //}
//                    #endregion

//                    #region == 整理資訊 ==
//                    var addData = new User
//                    {
//                        //GUID = Guid.NewGuid(),
//                        //ID = newData.ID,
//                        No = newData.No,
//                        Name = newData.Name,
//                        Password = newData.Password,
//                        Mail = newData.Mail,
//                        //Dept_ID = newData.Dept_ID.Value,
//                        //Role_ID = newData.Role_ID.Value,
//                        Is_Stop = newData.Is_Stop,
//                        Rem = newData.Rem,

//                        Company_ID = newData.Company_ID.Value,
//                        DataFrom_ID = newData.DataFrom_ID,
//                        DataFrom_Name = newData.DataFrom_ID.ToString(),
//                        //File_Bind_Guid = newData.File_Bind_Guid, // 檔案綁定用Guid，沒給就算了，後續如果執行修改的時候再由系統補上就好
//                        Create_DD = today,
//                        Create_Man_ID = _UserSession_Model.User_ID,
//                        Create_Man_Name = _UserSession_Model.User_Name,
//                        Update_DD = today,
//                        Update_Man_ID = _UserSession_Model.User_ID,
//                        Update_Man_Name = _UserSession_Model.User_Name,
//                    };
//                    #endregion

//                    #region == 【DB】寫入 ==
//                    try
//                    {
//                        _unitOfWork._UserRepository.Add(addData);
//                        _unitOfWork.Save();

//                        // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                        tmpFocusTXT = $"公司ID[{input.Company_ID}]---Key[{addData.ID}]---代號[{input.No}]";
//                        message_DTO = new Message_DTO(true, E_StatusCode.成功, "新增成功", tmpFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = addData.ID.ToString();

//                        result.Message_Infos.Add(message_DTO);
//                        result.Data.Add(addData.ID);
//                    }
//                    catch (Exception ex)
//                    {
//                        message_DTO = new Message_DTO(false, E_StatusCode.資料存取異常, "資料庫存取異常", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;
//                        message_DTO.Message_Exception = ex.Message;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                    }
//                    #endregion
//                }
//                else
//                {
//                    message_DTO = new Message_DTO(false, E_StatusCode.存在相同資料, "存在相同Key", comFocusTXT, input.Bind_Key);
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
//                result.Title = "【新增成功】";
//                result.E_StatusCode = E_StatusCode.成功;
//            }
//            else
//            {
//                result.Title = "【新增失敗】";
//                result.E_StatusCode = E_StatusCode.失敗;
//            }

//            //依狀態碼整理訊息
//            result.Message = _Message_Tool.GetMessage_Result(result.Message_Infos, result.Message);
//            #endregion

//            return result;
//        }

//        /// <summary>
//        /// 【單筆】修改使用者
//        /// </summary>
//        /// <param name="input">請確保資料是與[主系統]執行[修改]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<long?> Edit_User(User_Model input)
//        {
//            var result = new ResultOutput_Data<long?>(); //回傳成功的資料
//            //附值查詢
//            var filter = new List<User_Model> { input };
//            //取得結果
//            var resultData = this.Edits_User(filter);
//            //整理結果
//            result.IsSuccess = resultData.IsSuccess;
//            result.E_StatusCode = resultData.E_StatusCode;
//            result.Title = resultData.Title;
//            result.Message = resultData.Message;
//            result.Message_Exception = resultData.Message_Exception;
//            //result.Message_Infos = resultData.Message_Infos;
//            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
//            return result;
//        }

//        /// <summary>
//        /// 【多筆】修改使用者
//        /// </summary>
//        /// <param name="input">請確保資料是與[主系統]執行[修改]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<List<long>> Edits_User(List<User_Model> inputs)
//        {
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            var result = new ResultOutput_Data<List<long>>(true, new List<long>());
//            var message_DTO = new Message_DTO(true, $"", null);
//            var Com_IsExist = false;

//            #region == 迴圈處理 ==
//            foreach (var input in inputs)
//            {
//                // 通用的默認關鍵值訊息 (如不一樣請客製)
//                var comFocusTXT = $"公司ID[{input.Company_ID}]---Key[{input.ID}]---代號[{input.No}]";
//                // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                var tmpFocusTXT = "";

//                #region == 檢查-必填 ==
//                // 表頭null檢查
//                if (input.Company_ID.HasValue == false
//                    || input.ID.HasValue == false
//                    || string.IsNullOrEmpty(input.No)
//                    || string.IsNullOrEmpty(input.Name)
//                    )
//                {
//                    message_DTO = new Message_DTO(false, E_StatusCode.檢查異常, "", comFocusTXT, input.Bind_Key);
//                    message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;
//                    message_DTO.Message = "請檢查以下項目是否有值[ID、公司、代號、名稱]";

//                    result.IsSuccess = false;
//                    result.Message_Infos.Add(message_DTO);
//                    continue;
//                }
//                #endregion

//                #region == 檢查-資料來源地是否可處理 ==
//                switch (input.DataFrom_ID)
//                {
//                    case E_DataFrom.主系統:
//                    case E_DataFrom.ERP導入:
//                    case E_DataFrom.Excel匯入:
//                        break;
//                    default:
//                        message_DTO = new Message_DTO(false, E_StatusCode.檢查異常, "無法處理的資料來源", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                }
//                #endregion

//                #region == 處理 ==
//                // 取資料
//                var editData = _unitOfWork._UserRepository.Get(x => x.ID == input.ID);
//                // 是否存在。 [T：存在，修改][F：不存在，Error]
//                if (editData != null)
//                {
//                    User_Model newData = input;

//                    #region == 檢查-權限 ==
//                    //// 檢查
//                    //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, input.Company_ID);
//                    //// [T：失敗]
//                    //if (!Com_Result_DTO.IsSuccess)
//                    //{
//                    //    message_DTO = new Message_DTO(false, E_StatusCode.權限異常, Com_Result_DTO.Message, comFocusTXT, input.Bind_Key);
//                    //    message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                    //    result.IsSuccess = false;
//                    //    result.Message_Infos.Add(message_DTO);
//                    //    continue;
//                    //}
//                    #endregion

//                    #region == 生成新資料+檢查 ==
//                    //// 整理+檢查
//                    //var newData_Result = this.Generate_UpdataData_User(input, today);
//                    //// [T：成功][F：失敗]
//                    //if (newData_Result.IsSuccess)
//                    //{
//                    //    newData = newData_Result.Data;
//                    //}
//                    //else
//                    //{
//                    //    message_DTO = newData_Result.Message_Infos.FirstOrDefault();

//                    //    result.IsSuccess = false;
//                    //    result.Message_Infos.Add(message_DTO);
//                    //    continue;
//                    //}
//                    #endregion

//                    #region == 整理資訊 ==
//                    // 共通調整
//                    editData.Name = newData.Name;
//                    editData.Password = input.Password;
//                    editData.Mail = input.Mail;
//                    //editData.Dept_ID = input.Dept_ID.Value;
//                    //editData.Role_ID = input.Role_ID.Value;
//                    editData.Is_Stop = newData.Is_Stop;
//                    editData.Rem = newData.Rem;
//                    editData.Update_DD = today;
//                    editData.Update_Man_ID = _UserSession_Model.User_ID;
//                    editData.Update_Man_Name = _UserSession_Model.User_Name;

//                    //// 檔案綁定用Guid，如果空值，則new新值
//                    //if (!editData.File_Bind_Guid.HasValue)
//                    //{
//                    //    editData.File_Bind_Guid = Guid.NewGuid();
//                    //}
//                    #endregion

//                    #region == 【DB】寫入 ==
//                    try
//                    {
//                        _unitOfWork._UserRepository.Update(editData);
//                        _unitOfWork.Save();

//                        message_DTO = new Message_DTO(true, E_StatusCode.成功, "修改成功", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = editData.ID.ToString();

//                        result.Message_Infos.Add(message_DTO);
//                        result.Data.Add(editData.ID);
//                    }
//                    catch (Exception ex)
//                    {
//                        message_DTO = new Message_DTO(false, E_StatusCode.資料存取異常, "資料庫存取異常", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = editData.ID.ToString();
//                        message_DTO.Message_Exception = ex.Message;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                    }
//                    #endregion
//                }
//                else
//                {
//                    message_DTO = new Message_DTO(false, E_StatusCode.查無項目, "查無項目", comFocusTXT, input.Bind_Key);
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
//                result.Title = "【修改成功】";
//                result.E_StatusCode = E_StatusCode.成功;
//            }
//            else
//            {
//                result.Title = "【修改失敗】";
//                result.E_StatusCode = E_StatusCode.失敗;
//            }

//            //依狀態碼整理訊息
//            result.Message = _Message_Tool.GetMessage_Result(result.Message_Infos, result.Message);
//            #endregion

//            return result;
//        }

//        /// <summary>
//        /// 【單筆】刪除使用者
//        /// </summary>
//        /// <param name="input">請確保資料是與[主系統]執行[刪除]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<long?> Delete_User(User_Model input)
//        {
//            var result = new ResultOutput_Data<long?>(); //回傳成功的資料
//            //附值查詢
//            var filter = new List<User_Model> { input };
//            //取得結果
//            var resultData = this.Deletes_User(filter);
//            //整理結果
//            result.IsSuccess = resultData.IsSuccess;
//            result.E_StatusCode = resultData.E_StatusCode;
//            result.Title = resultData.Title;
//            result.Message = resultData.Message;
//            result.Message_Exception = resultData.Message_Exception;
//            //result.Message_Infos = resultData.Message_Infos;
//            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
//            return result;
//        }

//        /// <summary>
//        /// 【多筆】刪除使用者
//        /// </summary>
//        /// <param name="input">請確保資料是與[主系統]執行[刪除]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<List<long>> Deletes_User(List<User_Model> inputs)
//        {
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            var result = new ResultOutput_Data<List<long>>(true, new List<long>());
//            var message_DTO = new Message_DTO(true, $"", null);
//            var Com_IsExist = false;

//            #region == 迴圈處理 ==
//            foreach (var input in inputs)
//            {
//                #region == 處理 ==
//                var delData = _unitOfWork._UserRepository.Get(x => x.ID == input.ID);
//                //是否存在。 [T：存在，刪除][F：不存在，Error]
//                if(delData != null)
//                {
//                    //把還需要用到的值保留起來，因為刪掉之後就抓不到了
//                    var tempVal = new User_DTO
//                    {
//                        ID = delData.ID,
//                        No = delData.No,
//                    };

//                    // 通用的默認關鍵值訊息 (如不一樣請客製)
//                    var comFocusTXT = $"公司ID[{input.Company_ID}]---Key[{tempVal.ID}]---代號[{tempVal.No}]";
//                    // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                    var tmpFocusTXT = "";

//                    #region == 檢查-權限 ==
//                    //// 檢查
//                    //Com_Result_DTO = Validation_Tool.Is_AllowAction_ByCompanyLevelCheck(_UserSessionModel, input.Company_ID);
//                    //// [T：失敗]
//                    //if (!Com_Result_DTO.IsSuccess)
//                    //{
//                    //    message_DTO = new Message_DTO(false, E_StatusCode.權限異常, Com_Result_DTO.Message, comFocusTXT, input.Bind_Key);
//                    //    message_DTO.Table_Key_Main = input.ID.HasValue ? input.ID.Value.ToString() : null;

//                    //    result.IsSuccess = false;
//                    //    result.Message_Infos.Add(message_DTO);
//                    //    continue;
//                    //}
//                    #endregion

//                    #region == 【DB】寫入 ==
//                    try
//                    {
//                        _unitOfWork._UserRepository.Delete(delData);
//                        _unitOfWork.Save();

//                        message_DTO = new Message_DTO(true, E_StatusCode.成功, "刪除成功", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = tempVal.ID.ToString();

//                        result.Message_Infos.Add(message_DTO);
//                        result.Data.Add(tempVal.ID.Value);
//                    }
//                    catch (Exception ex)
//                    {
//                        message_DTO = new Message_DTO(false, E_StatusCode.資料存取異常, "資料庫存取異常", comFocusTXT, input.Bind_Key);
//                        message_DTO.Table_Key_Main = tempVal.ID.ToString();
//                        message_DTO.Message_Exception = ex.Message;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                    }
//                    #endregion
//                }
//                else
//                {
//                    message_DTO = new Message_DTO(false, E_StatusCode.查無項目, "查無項目", $"Key[{input.ID}]", input.Bind_Key);
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
//                result.Title = "【刪除成功】";
//                result.E_StatusCode = E_StatusCode.成功;
//            }
//            else
//            {
//                result.Title = "【刪除失敗】";
//                result.E_StatusCode = E_StatusCode.失敗;
//            }

//            //依狀態碼整理訊息
//            result.Message = _Message_Tool.GetMessage_Result(result.Message_Infos, result.Message);
//            #endregion

//            return result;
//        }
//        #endregion
//        #endregion

//        #region Default
//        ///// <summary>
//        ///// 新增資料
//        ///// </summary>
//        ///// <param name="data"></param>
//        ///// <returns></returns>
//        //public bool Create_User(User data)
//        //{
//        //    if (data != null)
//        //    {
//        //        _unitOfWork._UserRepository.Add(data);

//        //        var result = _unitOfWork.Save();

//        //        if (result > 0)
//        //            return true;
//        //        else
//        //            return false;
//        //    }
//        //    return false;
//        //}

//        /// <summary>
//        /// 刪除資料
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public bool Delete_User(long id)
//        {
//            if (id > 0)
//            {
//                var data = _unitOfWork._UserRepository.GetById(id);
//                if (data != null)
//                {
//                    _unitOfWork._UserRepository.Delete(data);
//                    var result = _unitOfWork.Save();

//                    if (result > 0)
//                        return true;
//                    else
//                        return false;
//                }
//            }
//            return false;
//        }

//        ///// <summary>
//        ///// 取全部資料
//        ///// </summary>
//        ///// <returns></returns>
//        //public IEnumerable<User> GetAll_User()
//        //{
//        //    var dataList = _unitOfWork._UserRepository.GetAll();
//        //    return dataList;
//        //}

//        /// <summary>
//        /// 取特定ID的資料
//        /// </summary>
//        /// <param name="UserId"></param>
//        /// <returns></returns>
//        public User Get_User_ById(long id)
//        {
//            if (id > 0)
//            {
//                var data = _unitOfWork._UserRepository.GetById(id);
//                if (data != null)
//                {
//                    return data;
//                }
//            }
//            return null;
//        }

//        /// <summary>
//        /// 更新資料
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public bool Update_User(User data)
//        {
//            if (data != null)
//            {
//                var User = _unitOfWork._UserRepository.GetById(data.ID);
//                if (User != null)
//                {
//                    User.No = data.No;
//                    User.Name = data.Name;

//                    _unitOfWork._UserRepository.Update(User);

//                    var result = _unitOfWork.Save();

//                    if (result > 0)
//                        return true;
//                    else
//                        return false;
//                }
//            }
//            return false;
//        }
//        #endregion
//    }
//}
