//using Main_Common.Model.Result;
//using Main_Common.Model.Tool;
//using Main_Common.Model.Data.User;
//using Main_EF.Interface;
//using Main_EF.Table;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Xml.Linq;
//using Main_Common.Enum.E_ProjectType;
//using Main_Common.Enum.E_StatusType;
//using Main_Common.Model.Account;
//using Main_Common.Model.Message;
//using Main_Common.Tool;
//using Main_Common.Model.Main;
//using Main_Service.Service.S_Log;
//using Main_Common.ExtensionMethod;

//namespace Main_Service.Service.S_User
//{
//    /// <summary>
//    /// 【Main】使用者相關
//    /// </summary>
//    public class UserService_Main
//    {
//        #region == 【全域變數】DB、Service ==
//        /// <summary>
//        /// 資料庫工作單元
//        /// </summary>
//        public IUnitOfWork _unitOfWork;
//        /// <summary>
//        /// 【Main Service】Log相關
//        /// </summary>
//        public readonly LogService_Main _LogService_Main;
//        /// <summary>
//        /// 【Tool】訊息處理
//        /// </summary>
//        public Message_Tool _Message_Tool;
//        #endregion

//        #region == 【全域宣告】 ==
//        /// <summary>
//        /// 【DTO】主系統資料
//        /// </summary>
//        public readonly MainSystem_DTO _MainSystem_DTO;
//        #endregion

//        //--【建構】=================================================================================

//        #region == 建構 ==
//        /// <summary>
//        /// 建構
//        /// </summary>
//        /// <param name="unitOfWork">資料庫工作單元</param>
//        /// <param name="mainSystem_DTO">主系統資料</param>
//        /// <param name="logService_Main">Log相關</param>
//        /// <param name="messageTool">訊息處理</param>
//        public UserService_Main(IUnitOfWork unitOfWork,
//            MainSystem_DTO mainSystem_DTO,
//            LogService_Main logService_Main,
//            Message_Tool messageTool)
//        {
//            this._unitOfWork = unitOfWork;
//            this._MainSystem_DTO = mainSystem_DTO;
//            this._LogService_Main = logService_Main;
//            this._Message_Tool = messageTool;
//        }
//        #endregion

//        //--【方法】=================================================================================

//        #region == 使用者 ================================================================
//        #region == 檢查相關-使用者 ==
//        /// <summary>
//        /// 檢查是否存在(使用者ID)
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns>「true，存在」</returns>
//        public bool CheckExist_User(long? key)
//        {
//            var check = _unitOfWork._UserRepository.GetAlls(x => x.ID == key).Any();
//            return check;
//        }

//        /// <summary>
//        /// 檢查是否重複(使用者代號)
//        /// </summary>
//        /// <param name="companyID">公司Key</param>
//        /// <param name="key">沒給新增檢查，有給修改檢查</param>
//        /// <param name="no">代號</param>
//        /// <returns>「true，存在」</returns>
//        public bool CheckRepeat_User_ByNo(long companyID, long? key, string no)
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
//        /// 【單筆】【DTO】取使用者DTO
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public User_DTO GetDTO_User(long key)
//        {
//            User_DTO result = null;
//            var dbData = _unitOfWork._UserRepository.Get(x => x.ID == key);
//            if (dbData != null)
//            {
//                result = new User_DTO
//                {
//                    ID = dbData.ID,
//                    No = dbData.No,
//                    Name = dbData.Name,
//                    Company_ID = dbData.Company_ID,
//                };
//            }

//            return result;
//        }

//        /// <summary>
//        /// 【單筆】【修改用】取使用者Model
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public User_Model GetEditModel_User(long key)
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
//                    Rem = dbData.Rem,
//                    Is_Stop = dbData.Is_Stop,
//                    Company_ID = dbData.Company_ID,
//                    Create_Man_Name = dbData.Create_Man_Name,
//                    Update_Man_Name = dbData.Update_Man_Name,
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
//        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
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
//        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<List<long>> Creates_User(List<User_Model> inputs)
//        {
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            var result = new ResultOutput_Data<List<long>>(true, new List<long>());
//            //var message_DTO = new Message_DTO(true, E_StatusLevel.正常, $"", null);
//            var message_DTO = new Message_DTO();
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
//                    message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.檢查異常, comFocusTXT);
//                    message_DTO.Message = $"請檢查以下項目是否有值[公司、代號、名稱]";

//                    result.IsSuccess = false;
//                    result.Message_Infos.Add(message_DTO);
//                    continue;
//                }
//                #endregion

//                #region == 檢查-資料來源地是否可處理 ==
//                //switch (input.DataFrom_ID)
//                //{
//                //    case E_DataFrom.主系統:
//                //    case E_DataFrom.ERP導入:
//                //    case E_DataFrom.Excel匯入:
//                //        break;
//                //    default:
//                //        message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.檢查異常, comFocusTXT);
//                //        message_DTO.Message = $"無法處理的資料來源";

//                //        result.IsSuccess = false;
//                //        result.Message_Infos.Add(message_DTO);
//                //        continue;
//                //}
//                #endregion

//                #region == 處理 ==
//                // 是否存在
//                Com_IsExist = this.CheckExist_User(input.ID);
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
//                        //File_Bind_Guid = newData.File_Bind_Guid, // 檔案綁定用Guid，沒給就算了，後續如果執行修改的時候再由系統補上就好
//                        Create_DD = today,
//                        Create_Man_ID = _MainSystem_DTO.UserSession.User_ID,
//                        Create_Man_Name = _MainSystem_DTO.UserSession.User_Name,
//                        Update_DD = today,
//                        Update_Man_ID = _MainSystem_DTO.UserSession.User_ID,
//                        Update_Man_Name = _MainSystem_DTO.UserSession.User_Name,
//                    };
//                    #endregion

//                    #region == 【DB】寫入 ==
//                    try
//                    {
//                        _unitOfWork._UserRepository.Add(addData);
//                        _unitOfWork.Save();

//                        // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                        tmpFocusTXT = $"公司ID[{input.Company_ID}]---Key[{addData.ID}]---代號[{input.No}]";
//                        message_DTO = new Message_DTO(true, input.Bind_Key, E_StatusLevel.正常, E_StatusCode.成功, tmpFocusTXT);
//                        message_DTO.Message = $"新增成功";

//                        result.Message_Infos.Add(message_DTO);
//                        result.Data.Add(addData.ID);
//                    }
//                    catch (Exception ex)
//                    {
//                        message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.資料存取異常, comFocusTXT);
//                        message_DTO.Message = $"資料庫存取異常";
//                        message_DTO.Message_Exception = ex.Message;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                    }
//                    #endregion
//                }
//                else
//                {
//                    message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.存在相同資料, comFocusTXT);
//                    message_DTO.Message = $"存在相同Key";

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
//        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultSimpleData<long?> Edit_User(User_Model input)
//        {
//            var result = new ResultSimpleData<long?>(); //回傳成功的資料
//            //附值查詢
//            var filter = new List<User_Model> { input };
//            //取得結果
//            var resultData = this.Edits_User(filter);
//            //整理結果
//            result.IsSuccess = resultData.IsSuccess;
//            result.E_StatusCode = resultData.E_StatusCode;
//            result.Title = resultData.Title;
//            result.Message = resultData.Message;
//            //result.Message_Exception = resultData.Message_Exception;
//            //result.Message_Infos = resultData.Message_Infos;
//            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
//            return result;
//        }

//        /// <summary>
//        /// 【多筆】修改使用者
//        /// </summary>
//        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultSimpleData<List<long>> Edits_User(List<User_Model> inputs)
//        {
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            var result = new ResultSimpleData<List<long>>(true, new List<long>());
//            var message_DTO = new Message_DTO();
//            var Com_IsExist = false;
//            var errorTXTs = new List<string>();

//            #region == 迴圈處理 ==
//            foreach (var input in inputs)
//            {
//                // 通用的默認關鍵值訊息 (如不一樣請客製)
//                var comFocusTXT = $"公司ID[{input.Company_ID}]---Key[{input.ID}]---代號[{input.No}]";
//                // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//                var tmpFocusTXT = "";

//                #region == 檢查-必填 (Error continue) ==
//                // 檢查指定屬性有無值
//                errorTXTs = DataValidationTool.Check_ModelAttrIsNull(input, new Dictionary<string, string>()
//                {
//                    { nameof(input.Company_ID), "公司" },
//                    { nameof(input.ID), "ID" },
//                    { nameof(input.No), "代號" },
//                    { nameof(input.Name), "名稱" },
//                });

//                // [T：有錯誤]
//                if (errorTXTs.Count() > 0)
//                {
//                    // 添加Log訊息
//                    message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.檢查異常, comFocusTXT);
//                    message_DTO.Message = $"請檢查以下項目是否有值「{string.Join("、", errorTXTs)}」";
//                    this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//                    result.IsSuccess = false;
//                    continue;
//                }
//                #endregion

//                #region == 檢查-資料來源地是否可處理 (Error continue) ==
//                //switch (input.DataFrom_ID)
//                //{
//                //    case E_DataFrom.主系統:
//                //    case E_DataFrom.ERP導入:
//                //    case E_DataFrom.Excel匯入:
//                //        break;
//                //    default:
//                //        // 添加Log訊息
//                //        message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.檢查異常, comFocusTXT);
//                //        message_DTO.Message = $"無法處理的資料來源";
//                //        this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//                //        result.IsSuccess = false;
//                //        continue;
//                //}
//                #endregion

//                #region == 處理 (Error continue) ==
//                // 取資料
//                var editData = _unitOfWork._UserRepository.Get(x => x.ID == input.ID);
//                // 是否存在。 [T：存在，修改][F：不存在，Error]
//                if (editData != null)
//                {
//                    User_Model newData = input;

//                    #region == 檢查-權限 (Error continue) ==
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

//                    #region == 生成新資料+檢查 (Error continue) ==
//                    // 整理+檢查
//                    var newData_Result = this.GenerateUpdataData_User(input, today);
//                    // [T：成功][F：失敗]
//                    if (newData_Result.IsSuccess)
//                    {
//                        newData = newData_Result.Data;
//                    }
//                    else
//                    {
//                        result.IsSuccess = false;
//                        continue;
//                    }
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
//                    editData.Update_Man_ID = _MainSystem_DTO.UserSession.User_ID;
//                    editData.Update_Man_Name = _MainSystem_DTO.UserSession.User_Name;

//                    //// 檔案綁定用Guid，如果空值，則new新值
//                    //if (!editData.File_Bind_Guid.HasValue)
//                    //{
//                    //    editData.File_Bind_Guid = Guid.NewGuid();
//                    //}
//                    #endregion

//                    #region == 【DB】寫入 (Error continue) ==
//                    try
//                    {
//                        _unitOfWork._UserRepository.Update(editData);
//                        _unitOfWork.Save();

//                        // 添加Log訊息
//                        message_DTO = new Message_DTO(true, input.Bind_Key, E_StatusLevel.正常, E_StatusCode.成功, comFocusTXT);
//                        message_DTO.Message = $"修改成功";
//                        this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//                        result.Data.Add(editData.ID);
//                    }
//                    catch (Exception ex)
//                    {
//                        // 添加Log訊息
//                        message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.資料存取異常, comFocusTXT);
//                        message_DTO.Message = $"資料庫存取異常";
//                        message_DTO.Message_Exception = ex.Message;
//                        this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//                        result.IsSuccess = false;
//                        continue;
//                    }
//                    #endregion
//                }
//                else
//                {
//                    // 添加Log訊息
//                    message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.查無項目, comFocusTXT);
//                    message_DTO.Message = $"查無項目";
//                    this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//                    result.IsSuccess = false;
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
//            #endregion

//            return result;
//        }

//        /// <summary>
//        /// 【單筆】刪除使用者
//        /// </summary>
//        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
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
//        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
//        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
//        public ResultOutput_Data<List<long>> Deletes_User(List<User_Model> inputs)
//        {
//            var todayFull = DateTime.UtcNow.AddHours(8); // 當前時間(含毫秒)
//            var today = Convert.ToDateTime(todayFull.ToString()); // 當前時間(不含毫秒)
//            var result = new ResultOutput_Data<List<long>>(true, new List<long>());
//            var message_DTO = new Message_DTO();
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

//                        message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.正常, E_StatusCode.成功, comFocusTXT);
//                        message_DTO.Message = $"刪除成功";

//                        result.Message_Infos.Add(message_DTO);
//                        result.Data.Add(tempVal.ID.Value);
//                    }
//                    catch (Exception ex)
//                    {
//                        message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.資料存取異常, comFocusTXT);
//                        message_DTO.Message = $"資料庫存取異常";
//                        message_DTO.Message_Exception = ex.Message;

//                        result.IsSuccess = false;
//                        result.Message_Infos.Add(message_DTO);
//                        continue;
//                    }
//                    #endregion
//                }
//                else
//                {
//                    message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.查無項目, $"Key[{input.ID}]");
//                    message_DTO.Message = $"查無項目";

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

//        #region == 整理相關-使用者 ==
//        /// <summary>
//        /// 【整理】重新整理資料規格 (補上Key值)(只處理[主系統]以外的來源資料)(將資料處理成[主系統]執行[新增、修改]時的規格)
//        /// </summary>
//        /// <param name="inputs"></param>
//        /// <param name="dataFrom">資料來源地</param>
//        /// <param name="toCompanyID">要寫進哪間公司</param>
//        /// <returns></returns>
//        //public ResultOutput_Data<List<User_Model>> ImportDatas_Format_User(List<User_Model> inputs, E_DataFrom dataFrom, long toCompanyID)
//        //{
//        //    var result = new ResultOutput_Data<List<User_Model>>(true, new List<User_Model>()); //回傳結果[預設成功]
//        //    var check = false; //檢查結果(使用前請先重置)

//        //    #region == 檢查-input ==
//        //    if (inputs == null || inputs.Count() == 0)
//        //    {
//        //        result = new ResultOutput_Data<List<User_Model>>(false, E_StatusCode.失敗, "未傳入資料", new List<User_Model>());
//        //        return result;
//        //    }
//        //    #endregion

//        //    #region == 處理 ==
//        //    try
//        //    {
//        //        #region == 檢查-外部Key是否有值 ==
//        //        // 任意值為空
//        //        check = inputs.Where(x => string.IsNullOrEmpty(x.No)).Any();
//        //        // [T：錯訊]
//        //        if (check)
//        //        {
//        //            result = new ResultOutput_Data<List<User_Model>>(false, E_StatusCode.失敗, "有未提供[使用者代號]的項目", new List<User_Model>());
//        //            return result;
//        //        }
//        //        #endregion

//        //        #region == 取本次會用到的使用者DTO ==
//        //        //// 查詢值[公司+使用者代號]
//        //        //var userQueryVals = inputs.Where(x => x.Seller_No != null && x.Seller_No != "").Select(x => toCompanyID + "___" + x.Seller_No).Distinct().ToList();
//        //        //_User_DTOs = _User_Repository.GetAlls(x => userQueryVals.Contains(x.Company_ID + "___" + x.No)).Select(x => new User_DTO
//        //        //{
//        //        //    ID = x.ID,
//        //        //    No = x.No,
//        //        //    Name = x.Name,
//        //        //    Company_ID = x.Company_ID,
//        //        //}).ToList();
//        //        #endregion

//        //        #region == 整理-補值 ==
//        //        // 使用者代號清單 (外部Key)
//        //        var cNos = inputs.Select(x => x.No).ToList();
//        //        // 取主系統內的資料 (外部Key + 寫入公司Key)
//        //        var data_MainSys = _User_Repository.GetAlls(x => cNos.Contains(x.No) && x.Company_ID == toCompanyID).ToList();
//        //        // 走訪inputs，填入主系統資料
//        //        foreach (var input in inputs)
//        //        {
//        //            #region == 無須增修判斷的整理 ==
//        //            // 填入公司
//        //            input.Company_ID = toCompanyID;
//        //            // 填入資料來源
//        //            input.DataFrom_ID = dataFrom;
//        //            // 填入使用者Key
//        //            //input.Seller_ID = _User_DTOs.Where(x => x.No == input.Seller_No).Select(x => x.ID).FirstOrDefault();
//        //            // 填入是否外部資料處理
//        //            input.Is_ExternalData_Processing = true;
//        //            #endregion

//        //            #region == 需增修判斷的整理 ==
//        //            var tempData = data_MainSys.Where(x => x.No == input.No).FirstOrDefault();
//        //            // [T：修改整理][F：新增整理]
//        //            if (tempData != null)
//        //            {
//        //                // 表頭整理
//        //                input.CRUD = E_CRUD.U;
//        //                input.ID = tempData.ID;
//        //            }
//        //            else
//        //            {
//        //                // 表頭整理
//        //                input.CRUD = E_CRUD.C;
//        //                input.ID = null;
//        //            }
//        //            #endregion
//        //        }
//        //        #endregion
//        //    }
//        //    catch (Exception)
//        //    {
//        //        result = new ResultOutput_Data<List<User_Model>>(false, E_StatusCode.失敗, "整理資料發生錯誤", new List<User_Model>());
//        //        return result;
//        //    }
//        //    #endregion

//        //    result.Data = inputs;
//        //    return result;
//        //}

//        /// <summary>
//        /// 【資料+檢查】生成公司資料 [含通用檢查]
//        /// </summary>
//        /// <param name="input">建議資料是與[主系統]執行[新增、修改]時的資料規格一致</param>
//        /// <param name="currDate">當前時間</param>
//        /// <returns></returns>
//        public ResultSimpleData<User_Model> GenerateUpdataData_User(User_Model input, DateTime currDate)
//        {
//            var result = new ResultSimpleData<User_Model>(true, null); //回傳結果[預設成功]
//            var message_DTO = new Message_DTO();
//            var check = false;

//            // 通用的默認關鍵值訊息 (如不一樣請客製)
//            var comFocusTXT = $"公司ID[{input.Company_ID}]---Key[{input.ID}]---代號[{input.No}]";
//            // 暫時的默認關鍵值訊息 (當作查詢用的關鍵字而已)
//            var tmpFocusTXT = "";

//            #region == 前置檢查-資料來源地是否可處理 (Error return) ==
//            //switch (input.DataFrom_ID)
//            //{
//            //    case E_DataFrom.主系統:
//            //    case E_DataFrom.ERP導入:
//            //    case E_DataFrom.Excel匯入:
//            //        break;
//            //    default:
//            //        // 添加Log訊息
//            //        message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.檢查異常, comFocusTXT);
//            //        message_DTO.Message = $"無法處理的資料來源";
//            //        this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//            //        result.IsSuccess = false;
//            //        return result;
//            //}
//            #endregion

//            #region == 檢查-公司是否存在 (Error return) ==
//            //check = _unitOfWork._CompanyRepository.CheckExist(input.Company_ID);

//            //// [T：查無]
//            //if (check == false)
//            //{
//            //    // 添加Log訊息
//            //    message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.查無相關資料, comFocusTXT);
//            //    message_DTO.Message = $"查無公司";
//            //    this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//            //    result.IsSuccess = false;
//            //    return result;
//            //}
//            #endregion

//            #region == 依CRUD (Error return) ==
//            switch (input.CRUD)
//            {
//                case E_CRUD.C:
//                    #region == 生成-使用者代號[No] ==
//                    //// [條件：是否提供代號][T：無，由系統生成]
//                    //if (string.IsNullOrEmpty(input.No))
//                    //{
//                    //    // 依資料來源地處理
//                    //    switch (input.DataFrom_ID)
//                    //    {
//                    //        case E_DataFrom.主系統:
//                    //            input.No = this._unitOfWork._UserRepository.GenNo(today);
//                    //            break;
//                    //        default:
//                    //            break;
//                    //    }
//                    //}
//                    #endregion
//                    break;
//                case E_CRUD.U:
//                    break;
//                default:
//                    break;
//            }
//            #endregion

//            #region == 檢查-代號[No] (Error return) ==
//            // 檢查是否有相同代號存在
//            check = this.CheckRepeat_User_ByNo(input.Company_ID.Value, input.ID, input.No);
//            // [T：重複]
//            if (check)
//            {
//                // 添加Log訊息
//                message_DTO = new Message_DTO(false, input.Bind_Key, E_StatusLevel.警告, E_StatusCode.檢查異常, comFocusTXT);
//                message_DTO.Message = $"代號已存在";
//                this._LogService_Main.Add_LogResultMessage(input.Bind_Key, message_DTO);

//                result.IsSuccess = false;
//                return result;
//            }
//            #endregion

//            result.Data = input;
//            return result;
//        }
//        #endregion

//        #region == 其它-使用者 ==
//        /// <summary>
//        /// 【單筆】取得使用者代號 (流水號編碼，無其他規則)
//        /// </summary>
//        /// <returns></returns>
//        //public string GetNo_User()
//        //{
//        //    var presentNO = this.GetNos_User(1).FirstOrDefault();
//        //    return presentNO;
//        //}

//        /// <summary>
//        /// 【多筆】取得使用者代號 (流水號編碼，無其他規則)
//        /// </summary>
//        /// <param name="count">要取幾組代號</param>
//        /// <returns></returns>
//        //public List<string> GetNos_User(int count)
//        //{
//        //    #region == 參數 ==
//        //    var result = new List<string>();
//        //    int snNo; // 流水號的部份
//        //    string snFormat = "0000"; // 流水號的格式
//        //    string presentNO;
//        //    //var numberPrefix = E_NumberPrefix.使用者.GetEnumDescription();
//        //    var query = snFormat;
//        //    var queryLength = query.Length;
//        //    var tryInt = false;
//        //    #endregion

//        //    #region == 取下一個最大的流水號 ==
//        //    // 取下一個流水號
//        //    // [任意代號符合指定碼數，且是數字組成][T：符合，取最大流水號+1][F：不符合，固定流水號1]
//        //    if (_unitOfWork._UserRepository.GetAlls(x => x.No.Length == 4 && SqlFunctions.IsNumeric(x.No) == 1).Any()) // 取最大的SN
//        //    {
//        //        // 嘗試轉型
//        //        tryInt = int.TryParse(_unitOfWork._UserRepository.GetAlls(x => x.No.Length == 4 && SqlFunctions.IsNumeric(x.No) == 1).OrderByDescending(o => o.No).FirstOrDefault().No, out snNo);
//        //        // 不論結果直接+1 (反正轉失敗會得到0)
//        //        snNo++;
//        //    }
//        //    else // 默認SN
//        //    {
//        //        snNo = 1;
//        //    }
//        //    #endregion

//        //    #region  == 生成代號 ==
//        //    // 依需求數遞加流水號
//        //    for (int i = 0; i < count; i++)
//        //    {
//        //        snNo += i == 0 ? 0 : 1; // 遞加i(第一圈不改變，故從0開始)
//        //        presentNO = snNo.ToString(snFormat); // 代號生成
//        //        result.Add(presentNO);
//        //    }
//        //    #endregion

//        //    return result;
//        //}

//        /// <summary>
//        ///【單筆】取得使用者代號
//        /// </summary>
//        /// <param name="date"></param>
//        /// <returns></returns>
//        //public string GetNo_User(DateTime date)
//        //{
//        //    var presentNO = this.GetNos_User(date, 1).FirstOrDefault();
//        //    return presentNO;
//        //}

//        /// <summary>
//        /// 【多筆】取得使用者代號
//        /// </summary>
//        /// <param name="date"></param>
//        /// <param name="count">要取幾組代號</param>
//        /// <returns></returns>
//        //public List<string> GetNos_User(DateTime date, int count)
//        //{
//        //    #region == 參數 ==
//        //    var result = new List<string>();
//        //    int yyyy = date.Year;
//        //    int MM = date.Month;
//        //    int dd = date.Day;
//        //    int snNo; // 流水號的部份
//        //    string presentNO;
//        //    var numberPrefix = E_NumberPrefix.使用者.GetEnumDescription();
//        //    var query = numberPrefix + "_" + yyyy.ToString("0000") + MM.ToString("00") + dd.ToString("00");
//        //    var queryLength = query.Length;
//        //    var check = false;
//        //    #endregion

//        //    #region == 取下一個最大的流水號 ==
//        //    // 是否存在相同開頭的單號
//        //    check = _unitOfWork._UserRepository.Any(x => x.No.StartsWith(query));
//        //    //check = _unitOfWork._UserRepository.GetAlls(x => x.No.StartsWith(query)).Any();
//        //    // [存在，取其最大流水號+1][不存在，流水號設為1]
//        //    if (check) // 取最大的SN
//        //    {
//        //        snNo = int.Parse(_unitOfWork._UserRepository.GetAlls(x => x.No.StartsWith(query)).OrderByDescending(o => o.No).FirstOrDefault().No.Substring(queryLength)) + 1;
//        //    }
//        //    else // 默認SN
//        //    {
//        //        snNo = 1;
//        //    }
//        //    #endregion

//        //    #region == 生成代號 ==
//        //    // 依需求數遞加流水號
//        //    for (int i = 0; i < count; i++)
//        //    {
//        //        snNo += i == 0 ? 0 : 1; // 遞加i(第一圈不改變，故從0開始)
//        //        presentNO = query + snNo.ToString("00000"); // 代號生成
//        //        result.Add(presentNO);
//        //    }
//        //    #endregion

//        //    return result;
//        //}
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
