using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using Main_EF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Account;
using Main_Common.Model.Message;
using Main_Common.Tool;
using Main_Common.Model.Main;
using Main_Service.Service.S_Log;
using Main_EF.Table;
using System.Runtime.Intrinsics.Arm;
//using Main_EF.Migrations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Main_Common.Model.Data;
using Main_EF.Interface.ITableRepository;
using Main_Common.Model.Data.User;
using Main_Common.Model.Data.Role;

namespace Main_Service.Service.S_User
{
    /// <summary>
    /// 【Main】使用者相關
    /// </summary>
    public class UserService_Main
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
        /// 【Main Service】Log相關
        /// </summary>
        public readonly LogService_Main _LogService_Main;
        /// <summary>
        /// 【Tool】訊息處理
        /// </summary>
        public readonly Message_Tool _Message_Tool;
        #endregion

        #region == 【全域宣告】 ==
        /// <summary>
        /// 【DTO】全部資料的DTO
        /// </summary>
        public readonly AllDataDTO _AllDataDTO = new AllDataDTO();
        #endregion

        //--【建構】=================================================================================

        #region == 建構 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="unitOfWork">資料庫工作單元</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="logService_Main">Log相關</param>
        /// <param name="messageTool">訊息處理</param>
        public UserService_Main(IUnitOfWork unitOfWork,
            MainSystem_DTO mainSystem_DTO,
            LogService_Main logService_Main,
            Message_Tool messageTool)
        {
            this._unitOfWork = unitOfWork;
            this._MainSystem_DTO = mainSystem_DTO;
            this._LogService_Main = logService_Main;
            this._Message_Tool = messageTool;
        }
        #endregion

        //--【方法】=================================================================================

        /// <summary>
        /// 備註說明用
        /// </summary>
        private void TTRem()
        {
            // 寫法可統一。
            // 有一定標準。
            // 需要給其他Service使用。
            // 建議寫在Table的Repository。
        }

        #region == 使用者 ================================================================
        #region == 檢查相關-使用者 ==
        // ...
        #endregion

        #region == 取資料相關-使用者 ==
        /// <summary>
        /// 【單筆】【DTO】取使用者DTO
        /// </summary>
        /// <param name="bindKey">綁定Key</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public User_DTO? GetDTO_User(Guid bindKey, long? key)
        {
            User_DTO? result = null;
            var methodParam = new MethodParameter(bindKey); // 方法的通用屬性參數

            // 過濾條件
            var query = new User_Filter
            {
                Id = key,
            };

            // 取得資料
            result = this._unitOfWork._UserRepository.GetDTO(query);

            return result;
        }

        /// <summary>
        /// 【單筆】【修改用】取使用者Model
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public User_Model? GetEditModel_User(long key)
        {
            User_Model? result = null;
            var dbData = _unitOfWork._UserRepository.GetAlls(x => x.Id == key)
                                                    .Include(x => x.CompanyInfo)
                                                    .Include(x => x.RoleInfo)
                                                    .FirstOrDefault();

            if (dbData != null)
            {
                result = new User_Model
                {
                    CRUD = E_CRUD.U,
                    Id = dbData.Id,
                    No = dbData.No,
                    Name = dbData.Name,
                    RoleId = dbData.RoleId,
                    RoleName = dbData.RoleInfo.Name,
                    Password = dbData.Password,
                    Mail = dbData.Mail,
                    IsStop = dbData.IsStop,
                    Rem = dbData.Rem,
                    CompanyId = dbData.CompanyId,
                    CompanyName = dbData.CompanyInfo.Name,
                    CreateManName = dbData.CreateManName,
                    UpdateManName = dbData.UpdateManName,
                };
            }

            return result;
        }

        /// <summary>
        /// 【單筆】取使用者Model
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UserSession_Model Get_User_Info(Login_Model input)
        {
            var user = _unitOfWork._UserRepository.Get(x => x.No == input.Account && x.Password == input.Password);
            if(user == null)
            {
                return null;
            }
            else
            {
                UserSession_Model result = new UserSession_Model();
                //var user = _unitOfWork._UserRepository.Get(x => x.No == input.Account);                                                    
                var company = _unitOfWork._CompanyRepository.Get(x => x.Id == user.CompanyId);
                var role = _unitOfWork._RoleRepository.Get(x => x.Id == user.RoleId);

                if (user != null)
                {
                    result = new UserSession_Model
                    {
                        LoginId = user.GUID,
                        Account = user.No,
                        Password = user.Password,
                        CompanyId = user.CompanyId,
                        CompanyNo = company.No,
                        CompanyName = company.Name,
                        CompanyLevelId = company.CompanyLevelId,
                        UserId = user.Id,
                        UserNo = user.No,
                        UserName = user.Name,
                        Mail = user.Mail,
                        RoleId = user.RoleId,
                        RoleName = role.Name,
                        PermissionTypeId = role.PermissionTypeId,
                    };
                    
                }

                return result;
            }
            
        }

        /// <summary>
        /// 【單筆】取使用者Model
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UserSession_Model Check_User_Info(Login_Model input)
        {
            var user = _unitOfWork._UserRepository.Get(x => x.No == input.Account && x.Password == input.Password);
            if (user == null)
            {
                return null;
            }
            else
            {
                UserSession_Model result = new UserSession_Model();
                //var user = _unitOfWork._UserRepository.Get(x => x.No == input.Account);                                                    
                var company = _unitOfWork._CompanyRepository.Get(x => x.Id == user.CompanyId);
                var role = _unitOfWork._RoleRepository.Get(x => x.Id == user.RoleId);

                if (user != null)
                {
                    result = new UserSession_Model
                    {
                        LoginId = user.GUID,
                        Account = user.No,
                        Password = user.Password,
                        CompanyId = user.CompanyId,
                        CompanyNo = company.No,
                        CompanyName = company.Name,
                        CompanyLevelId = company.CompanyLevelId,
                        UserId = user.Id,
                        UserNo = user.No,
                        UserName = user.Name,
                        Mail = user.Mail,
                        RoleId = user.RoleId,
                        RoleName = role.Name,
                        PermissionTypeId = role.PermissionTypeId,
                    };
                }

                return result;
            }

        }
        /// <summary>
        /// 【多筆】【可分頁】取使用者清單
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <param name="pageingDTO">分頁條件</param>
        /// <returns></returns>
        public ResultSimpleData<List<User_List>> GetList_User(User_Filter input, Pageing_DTO pageingDTO)
        {
            var result = new ResultSimpleData<List<User_List>>(true, new List<User_List>());
            var methodParam = new MethodParameter(input.BindKey); // 方法的通用屬性參數

            // 使用者-語法命令
            var dbDatas = _unitOfWork._UserRepository.GetAll();

            #region == 過濾 ==
            // 使用者-過濾命令(ref)
            this._unitOfWork._UserRepository.WhereQueryable(ref dbDatas, input);
            #endregion

            #region == 分頁處理 ==
            // 是否分頁。 [T：不分頁][F：分頁]
            if (pageingDTO == null || pageingDTO.IsEnable == false)
            {
                result.PageingDTO.TotalCount = dbDatas.Count();
            }
            else
            {
                pageingDTO.TotalCount = dbDatas.Count();
                result.PageingDTO = pageingDTO; //給result值
                dbDatas = dbDatas.OrderBy(o => o.No).Skip((pageingDTO.PageNumber - 1) * pageingDTO.PageSize).Take(pageingDTO.PageSize);
            }
            #endregion

            #region == 整理資料 ==
            result.Data = dbDatas.Select(x => new User_List
            {
                Id = x.Id,
                No = x.No,
                Name = x.Name,
                Mail = x.Mail,
                DeptId = x.DeptId,
                RoleId = x.RoleId,
                RoleName = x.RoleInfo.Name,
                Rem = x.Rem,
                CompanyId = x.CompanyId,
            }).ToList();
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】取得使用者下拉清單
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<List<SelectItemDTO>> GetSimpleDropList_User(User_Filter input)
        {
            var result = new ResultSimpleData<List<SelectItemDTO>>(true, new List<SelectItemDTO>());
            var pageingDTO = new Pageing_DTO();

            // 建置查詢值(避免前面多傳，造成過濾不準確)
            var query = new User_Filter
            {
                Id = input.Id,
            };

            result.Data = this._unitOfWork._UserRepository.GetDropList(query, ref pageingDTO, null);
            result.PageingDTO = pageingDTO;
            return result;
        }

        /// <summary>
        /// 【多筆】取得使用者下拉清單
        /// </summary>
        /// <param name="input">Id為[預選項目]的判斷</param>
        /// <param name="defaultItemText">是否提供預設的Null項目(在第一筆)[沒給不產生][有給產生相同Text的項目]</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<List<SelectItemDTO>> GetSimpleDropList_User(User_Filter input, string defaultItemText)
        {
            var result = new ResultSimpleData<List<SelectItemDTO>>(true, new List<SelectItemDTO>());
            var pageingDTO = new Pageing_DTO();

            result.Data = this._unitOfWork._UserRepository.GetDropList(input, ref pageingDTO, defaultItemText);
            result.PageingDTO = pageingDTO;
            return result;
        }

        /// <summary>
        /// 【Obj】【分頁】取得使用者下拉清單資訊
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <param name="pageingDTO">分頁</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<object> GetSimpleDropList_User_Obj(User_Filter input, Pageing_DTO pageingDTO)
        {
            var result = new ResultSimpleData<object>();

            var dropList = this._unitOfWork._UserRepository.GetDropList(input, ref pageingDTO, null);
            result.Data = dropList;
            result.PageingDTO = pageingDTO;
            return result;
        }
        #endregion

        #region == 存資料相關-使用者 ==
        /// <summary>
        /// 【單筆】新增使用者
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Create_User(User_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<User_Model> { input };
            // 取得結果
            var resultData = this.Creates_User(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】新增使用者
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Creates_User(List<User_Model> inputs)
        {
            var result = new ResultSimpleData<List<long>>(true, new List<long>());
            var methodParam = new MethodParameter(); // 方法的通用屬性參數

            #region == 取本次會用到的資料DTO ==
            #region == 角色 ==
            var roleIDs = inputs.Select(x => x.RoleId).Distinct().ToList();
            this._AllDataDTO.RoleDTOs = this._unitOfWork._RoleRepository.GetAlls(x => roleIDs.Contains(x.Id)).Select(x => new Role_DTO
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            #endregion
            #endregion

            #region == 迴圈處理 ==
            foreach (var input in inputs)
            {
                // 重置Model
                methodParam.Reset_Message(input.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                methodParam.ComFocusText = $"公司Key[{input.CompanyId}]---Key[{input.Id}]---代號[{input.No}]";

                #region == 【檢查】必填 (Error continue) ==
                // 檢查指定屬性有無值
                methodParam.ErrorTexts = DataValidationTool.Check_ModelAttrIsNull(input, new Dictionary<string, string>()
                {
                    { nameof(input.CompanyId), "公司" },
                    { nameof(input.No), "代號" },
                    { nameof(input.Name), "名稱" },
                    { nameof(input.RoleId), "角色" },
                });

                // [T：有錯誤]
                if (methodParam.ErrorTexts.Count() > 0)
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"請檢查以下項目是否有值「{string.Join("、", methodParam.ErrorTexts)}」";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    continue;
                }
                #endregion

                #region == 處理 (Error continue) ==
                // 是否存在
                methodParam.CheckResult = this._unitOfWork._UserRepository.CheckExist(input.Id);
                // [T：不存在，新增][F：存在，Error]
                if (!methodParam.CheckResult)
                {
                    User_Model newData = input;

                    #region == 【檢查】權限 (Error continue) ==
                    // 檢查
                    methodParam.ResultSimple = DataValidationTool.Is_AllowAction_ByCompanyLevelCheck(_MainSystem_DTO.UserSession, input.CompanyId);
                    // [T：失敗]
                    if (!methodParam.ResultSimple.IsSuccess)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.權限異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = methodParam.ResultSimple.Message;
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion

                    #region == 【生成+檢查】新資料 (Error continue) ==
                    // 整理+檢查
                    var resultNewData = this.GenerateData_User(input, methodParam.TodayFull);
                    // [T：成功][F：失敗]
                    if (resultNewData.IsSuccess)
                    {
                        newData = resultNewData.Data;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion

                    #region == 【整理】資料 ==
                    var addData = new User
                    {
                        //GUID = Guid.NewGuid(),
                        //Id = newData.Id,
                        No = newData.No,
                        Name = newData.Name,
                        RoleId = newData.RoleId.Value,
                        Password = newData.Password,
                        Mail = newData.Mail,
                        IsStop = newData.IsStop,
                        Rem = newData.Rem,
                        CompanyId = newData.CompanyId.Value,
                        FileBindGuid = newData.FileBindGuid, // 檔案綁定用Guid，沒給就算了，後續如果執行修改的時候再由系統補上就好
                        CreateTime = methodParam.Today,
                        CreateManId = _MainSystem_DTO.UserSession.UserId,
                        CreateManName = _MainSystem_DTO.UserSession.UserName,
                        UpdateTime = methodParam.Today,
                        UpdateManId = _MainSystem_DTO.UserSession.UserId,
                        UpdateManName = _MainSystem_DTO.UserSession.UserName,
                    };
                    #endregion

                    #region == 【DB】寫入 (Error continue) ==
                    try
                    {
                        this._unitOfWork._UserRepository.Add(addData);
                        this._unitOfWork.Save();

                        // 添加Log訊息
                        methodParam.TmpFocusText = $"公司Key[{input.CompanyId}]---Key[{addData.Id}]---代號[{input.No}]";
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.正常, E_StatusCode.成功, methodParam.TmpFocusText);
                        methodParam.MessageDTO.Message = $"新增成功";
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.Data.Add(addData.Id);
                    }
                    catch (Exception ex)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"資料庫存取異常";
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion
                }
                else
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.存在相同資料, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"存在相同Key";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    continue;
                }
                #endregion
            }
            #endregion

            #region == 【整理】錯訊 [沒有直接Error，才會執行到這] ==
            if (result.IsSuccess)
            {
                result.Title = "【新增成功】";
                result.E_StatusCode = E_StatusCode.成功;
            }
            else
            {
                result.Title = "【新增失敗】";
                result.E_StatusCode = E_StatusCode.失敗;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】修改使用者
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Edit_User(User_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值查詢
            var inputs = new List<User_Model> { input };
            // 取得結果
            var resultData = this.Edits_User(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】修改使用者
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Edits_User(List<User_Model> inputs)
        {
            ResultSimpleData<List<long>> result = new ResultSimpleData<List<long>>(true, new List<long>());
            MethodParameter methodParam = new MethodParameter(); // 方法的通用屬性參數

            #region == 取本次會用到的資料DTO ==
            #region == 角色 ==
            var roleIDs = inputs.Select(x => x.RoleId).Distinct().ToList();
            this._AllDataDTO.RoleDTOs = this._unitOfWork._RoleRepository.GetAlls(x => roleIDs.Contains(x.Id)).Select(x => new Role_DTO
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            #endregion
            #endregion

            #region == 迴圈處理 ==
            foreach (var input in inputs)
            {
                // 重置Model
                methodParam.Reset_Message(input.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                methodParam.ComFocusText = $"公司Key[{input.CompanyId}]---Key[{input.Id}]---代號[{input.No}]";

                #region == 【檢查】必填 (Error continue) ==
                // 檢查指定屬性有無值
                methodParam.ErrorTexts = DataValidationTool.Check_ModelAttrIsNull(input, new Dictionary<string, string>()
                {
                    { nameof(input.CompanyId), "公司" },
                    { nameof(input.Id), "Id" },
                    { nameof(input.No), "代號" },
                    { nameof(input.Name), "名稱" },
                    { nameof(input.RoleId), "角色" },
                });

                // [T：有錯誤]
                if (methodParam.ErrorTexts.Count() > 0)
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"請檢查以下項目是否有值「{string.Join("、", methodParam.ErrorTexts)}」";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    continue;
                }
                #endregion

                #region == 處理 (Error continue) ==
                // 取資料
                var editData = _unitOfWork._UserRepository.GetAlls(x => x.Id == input.Id).FirstOrDefault();
                // 是否存在。 [T：存在，修改][F：不存在，Error]
                if (editData != null)
                {
                    User_Model newData = input;

                    #region == 【檢查】權限 (Error continue) ==
                    // 檢查
                    methodParam.ResultSimple = DataValidationTool.Is_AllowAction_ByCompanyLevelCheck(_MainSystem_DTO.UserSession, input.CompanyId);
                    // [T：失敗]
                    if (!methodParam.ResultSimple.IsSuccess)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.權限異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = methodParam.ResultSimple.Message;
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion

                    #region == 【生成+檢查】新資料 (Error continue) ==
                    // 整理+檢查
                    var resultNewData = this.GenerateData_User(input, methodParam.TodayFull);
                    // [T：成功][F：失敗]
                    if (resultNewData.IsSuccess)
                    {
                        newData = resultNewData.Data;
                    }
                    else
                    {
                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion

                    #region == 【整理】資料 ==
                    // 共通調整
                    editData.Name = newData.Name;
                    editData.RoleId = newData.RoleId.Value;
                    editData.Password = input.Password;
                    editData.Mail = input.Mail;
                    editData.IsStop = newData.IsStop;
                    editData.Rem = newData.Rem;
                    editData.UpdateTime = methodParam.Today;
                    editData.UpdateManId = _MainSystem_DTO.UserSession.UserId;
                    editData.UpdateManName = _MainSystem_DTO.UserSession.UserName;

                    // 檔案綁定用Guid，如果空值，則new新值
                    if (!editData.FileBindGuid.HasValue)
                    {
                        editData.FileBindGuid = Guid.NewGuid();
                    }
                    #endregion

                    #region == 【DB】寫入 (Error continue) ==
                    try
                    {
                        this._unitOfWork._UserRepository.Update(editData);
                        this._unitOfWork.Save();

                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(true, methodParam.BindKey, E_StatusLevel.正常, E_StatusCode.成功, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"修改成功";
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.Data.Add(editData.Id);
                    }
                    catch (Exception ex)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"資料庫存取異常";
                        methodParam.MessageDTO.Message_Exception = ex.Message;
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion
                }
                else
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.查無項目, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"查無項目";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    continue;
                }
                #endregion
            }
            #endregion

            #region == 【整理】錯訊 [沒有直接Error，才會執行到這] ==
            if (result.IsSuccess)
            {
                result.Title = "【修改成功】";
                result.E_StatusCode = E_StatusCode.成功;
            }
            else
            {
                result.Title = "【修改失敗】";
                result.E_StatusCode = E_StatusCode.失敗;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】刪除使用者
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Delete_User(User_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<User_Model> { input };
            // 取得結果
            var resultData = this.Deletes_User(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】刪除使用者
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Deletes_User(List<User_Model> inputs)
        {
            var result = new ResultSimpleData<List<long>>(true, new List<long>());
            var methodParam = new MethodParameter(); // 方法的通用屬性參數

            #region == 迴圈處理 ==
            foreach (var input in inputs)
            {
                // 重置Model
                methodParam.Reset_Message(input.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                methodParam.ComFocusText = $"Key[{input.Id}]";

                #region == 處理 (Error continue) ==
                var delData = _unitOfWork._UserRepository.Get(x => x.Id == input.Id);
                //是否存在。 [T：存在，刪除][F：不存在，Error]
                if (delData != null)
                {
                    //把還需要用到的值保留起來，因為刪掉之後就抓不到了
                    var tempVal = new User_DTO
                    {
                        Id = delData.Id,
                        No = delData.No,
                        CompanyId = delData.CompanyId,
                    };

                    #region == 【檢查】權限 (Error continue) ==
                    // 檢查
                    methodParam.ResultSimple = DataValidationTool.Is_AllowAction_ByCompanyLevelCheck(_MainSystem_DTO.UserSession, tempVal.CompanyId);
                    // [T：失敗]
                    if (!methodParam.ResultSimple.IsSuccess)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.權限異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = methodParam.ResultSimple.Message;
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion

                    #region == 【DB】寫入 (Error continue) ==
                    try
                    {
                        this._unitOfWork._UserRepository.Delete(delData);
                        this._unitOfWork.Save();

                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(true, methodParam.BindKey, E_StatusLevel.正常, E_StatusCode.成功, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"刪除成功";
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.Data.Add(tempVal.Id.Value);
                    }
                    catch (Exception ex)
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"資料庫存取異常";
                        methodParam.MessageDTO.Message_Exception = ex.Message;
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion
                }
                else
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.查無項目, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"查無項目";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    continue;
                }
                #endregion
            }
            #endregion

            #region == 整理錯訊 [沒有直接Error，才會執行到這] ==
            if (result.IsSuccess)
            {
                result.Title = "【刪除成功】";
                result.E_StatusCode = E_StatusCode.成功;
            }
            else
            {
                result.Title = "【刪除失敗】";
                result.E_StatusCode = E_StatusCode.失敗;
            }
            #endregion

            return result;
        }
        #endregion

        #region == 整理相關-使用者 ==
        /// <summary>
        /// 【整理】重新整理資料規格 (補上Key值)(只處理[主系統]以外的來源資料)(將資料處理成[主系統]執行[新增、修改]時的規格)
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="dataFrom">資料來源地</param>
        /// <param name="toUserId">要寫進哪間使用者</param>
        /// <returns></returns>
        //public ResultOutput_Data<List<User_Model>> ImportDatas_Format_User(List<User_Model> inputs, E_DataFrom dataFrom, long toUserId)
        //{
        //    var result = new ResultOutput_Data<List<User_Model>>(true, new List<User_Model>()); //回傳結果[預設成功]
        //    var check = false; //檢查結果(使用前請先重置)

        //    #region == 檢查-input ==
        //    if (inputs == null || inputs.Count() == 0)
        //    {
        //        result = new ResultOutput_Data<List<User_Model>>(false, E_StatusCode.失敗, "未傳入資料", new List<User_Model>());
        //        return result;
        //    }
        //    #endregion

        //    #region == 處理 ==
        //    try
        //    {
        //        #region == 檢查-外部Key是否有值 ==
        //        // 任意值為空
        //        check = inputs.Where(x => string.IsNullOrEmpty(x.No)).Any();
        //        // [T：錯訊]
        //        if (check)
        //        {
        //            result = new ResultOutput_Data<List<User_Model>>(false, E_StatusCode.失敗, "有未提供[使用者代號]的項目", new List<User_Model>());
        //            return result;
        //        }
        //        #endregion

        //        #region == 取本次會用到的使用者DTO ==
        //        //// 查詢值[使用者+使用者代號]
        //        //var UserQueryVals = inputs.Where(x => x.Seller_No != null && x.Seller_No != "").Select(x => toUserId + "___" + x.Seller_No).Distinct().ToList();
        //        //_User_DTOs = _User_Repository.GetAlls(x => UserQueryVals.Contains(x.User_Id + "___" + x.No)).Select(x => new User_DTO
        //        //{
        //        //    Id = x.Id,
        //        //    No = x.No,
        //        //    Name = x.Name,
        //        //    User_Id = x.User_Id,
        //        //}).ToList();
        //        #endregion

        //        #region == 整理-補值 ==
        //        // 使用者代號清單 (外部Key)
        //        var cNos = inputs.Select(x => x.No).ToList();
        //        // 取主系統內的資料 (外部Key + 寫入使用者Key)
        //        var data_MainSys = _User_Repository.GetAlls(x => cNos.Contains(x.No) && x.User_Id == toUserId).ToList();
        //        // 走訪inputs，填入主系統資料
        //        foreach (var input in inputs)
        //        {
        //            #region == 無須增修判斷的整理 ==
        //            // 填入使用者
        //            input.User_Id = toUserId;
        //            // 填入資料來源
        //            input.DataFrom_Id = dataFrom;
        //            // 填入使用者Key
        //            //input.Seller_Id = _User_DTOs.Where(x => x.No == input.Seller_No).Select(x => x.Id).FirstOrDefault();
        //            // 填入是否外部資料處理
        //            input.Is_ExternalData_Processing = true;
        //            #endregion

        //            #region == 需增修判斷的整理 ==
        //            var tempData = data_MainSys.Where(x => x.No == input.No).FirstOrDefault();
        //            // [T：修改整理][F：新增整理]
        //            if (tempData != null)
        //            {
        //                // 表頭整理
        //                input.CRUD = E_CRUD.U;
        //                input.Id = tempData.Id;
        //            }
        //            else
        //            {
        //                // 表頭整理
        //                input.CRUD = E_CRUD.C;
        //                input.Id = null;
        //            }
        //            #endregion
        //        }
        //        #endregion
        //    }
        //    catch (Exception)
        //    {
        //        result = new ResultOutput_Data<List<User_Model>>(false, E_StatusCode.失敗, "整理資料發生錯誤", new List<User_Model>());
        //        return result;
        //    }
        //    #endregion

        //    result.Data = inputs;
        //    return result;
        //}

        /// <summary>
        /// 【資料+檢查】生成使用者資料 [含通用檢查]
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增、修改]時的資料規格一致</param>
        /// <param name="currDate">當前時間</param>
        /// <returns></returns>
        public ResultSimpleData<User_Model> GenerateData_User(User_Model input, DateTime currDate)
        {
            var result = new ResultSimpleData<User_Model>(true, null); //回傳結果[預設成功]
            var methodParam = new MethodParameter(currDate); // 方法的通用屬性參數

            // 重置Model
            methodParam.Reset_Message(input.BindKey);
            // 關鍵值訊息 (通用的)(如不一樣請客製)
            methodParam.ComFocusText = $"公司Key[{input.CompanyId}]---Key[{input.Id}]---代號[{input.No}]";

            #region == 【檢查】公司是否存在 (Error return) ==
            methodParam.CheckResult = _unitOfWork._CompanyRepository.CheckExist(input.CompanyId);

            // [T：查無]
            if (methodParam.CheckResult == false)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                methodParam.MessageDTO.Message = $"查無公司[{input.CompanyId}]";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                result.IsSuccess = false;
                return result;
            }
            #endregion

            #region == 依CRUD區分處理 (Error return) ==
            switch (input.CRUD)
            {
                case E_CRUD.C:
                    #region == 【生成】代號(No) ==
                    //// [條件：是否提供代號][T：無，由系統生成]
                    //if (string.IsNullOrEmpty(input.No))
                    //{
                    //    // 依資料來源地處理
                    //    switch (input.DataFrom_Id)
                    //    {
                    //        case E_DataFrom.主系統:
                    //            input.No = this._unitOfWork._UserRepository.GenNo(today);
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                    #endregion
                    break;
                case E_CRUD.U:
                    break;
                default:
                    break;
            }
            #endregion

            #region == 【檢查】代號(No) (Error return) ==
            // 檢查是否有相同代號存在
            methodParam.CheckResult = this._unitOfWork._UserRepository.CheckRepeat_ByNo(input.CompanyId.Value, input.Id, input.No);
            // [T：重複]
            if (methodParam.CheckResult == true)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                methodParam.MessageDTO.Message = $"代號已存在";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                result.IsSuccess = false;
                return result;
            }
            #endregion

            #region == 【檢查】角色是否存在 (Error return) ==
            // [T：有值，檢查]
            if (input.RoleId.HasValue)
            {
                methodParam.CheckResult = this._AllDataDTO.RoleDTOs.Where(x => x.Id == input.RoleId).Any();
                // [T：查無]
                if (methodParam.CheckResult == false)
                {
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"查無角色[{input.RoleId}]";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.IsSuccess = false;
                    return result;
                }
            }
            #endregion

            result.Data = input;
            return result;
        }
        #endregion

        #region == 其它-使用者 ==
        //...
        #endregion
        #endregion
    }
}
