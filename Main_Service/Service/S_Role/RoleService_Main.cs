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
using Main_Common.Model.Data.Role;
using Main_EF.Table;
using System.Runtime.Intrinsics.Arm;
//using Main_EF.Migrations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Main_Common.Model.Data;
using Main_EF.Interface.ITableRepository;
using Main_Common.Model.Data.Role;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Main_Common.Model.Data.FunctionCode;

namespace Main_Service.Service.S_Role
{
    /// <summary>
    /// 【Main】角色相關
    /// </summary>
    public class RoleService_Main
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
        // ...
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
        public RoleService_Main(IUnitOfWork unitOfWork,
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

        #region == 角色 ================================================================
        #region == 檢查相關-角色 ==
        // ...
        #endregion

        #region == 取資料相關-角色 ==
        /// <summary>
        /// 【單筆】【DTO】取角色DTO
        /// </summary>
        /// <param name="bindKey">綁定Key</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Role_DTO? GetDTO_Role(Guid bindKey, long? key)
        {
            Role_DTO? result = null;
            var methodParam = new MethodParameter(bindKey); // 方法的通用屬性參數

            // 過濾條件
            var query = new Role_Filter
            {
                Id = key,
            };

            // 取得資料
            result = this._unitOfWork._RoleRepository.GetDTO(query);

            return result;
        }

        /// <summary>
        /// 【單筆】【修改用】取角色Model
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Role_Model? GetEditModel_Role(long key)
        {
            Role_Model? result = null;
            var dbData = _unitOfWork._RoleRepository.GetAlls(x => x.Id == key)
                                                    .Include(x => x.FunctionCodeList)
                                                    .FirstOrDefault();
            if (dbData != null)
            {
                #region == 整理-角色 ==
                result = new Role_Model
                {
                    CRUD = E_CRUD.U,
                    Id = dbData.Id,
                    No = dbData.No,
                    Name = dbData.Name,
                    PermissionTypeId = dbData.PermissionTypeId,
                    IsStop = dbData.IsStop,
                    Rem = dbData.Rem,
                    CompanyId = dbData.CompanyId,
                    CreateManName = dbData.CreateManName,
                    UpdateManName = dbData.UpdateManName,
                };
                #endregion

                #region == 整理-功能清單 ==
                // 所有功能代碼清單
                var allFunctionCode = ConvertTool.EnumToListEnum<E_Function>();
                // 該角色的功能代碼清單
                var dbFunctionCode = dbData.FunctionCodeList.Select(x => x.FunctionCodeId).ToList();
                // 要新增的功能代碼清單
                var addFunctionCode = allFunctionCode.Except(dbFunctionCode).ToList();
                // 要修改的功能代碼清單
                var editFunctionCode = allFunctionCode.Intersect(dbFunctionCode).ToList();
                // 要刪除的功能代碼清單
                var delFunctionCode = dbFunctionCode.Except(allFunctionCode).ToList();

                // 添加新增項目至result
                result.FunctionCodeList.AddRange(
                    addFunctionCode.Select(x => new FunctionCode_Model
                    {
                        CRUD = E_CRUD.C,
                        CompanyId = result.CompanyId.Value,
                        Id = null,
                        RoleId = result.Id,
                        FunctionCodeId = x,
                        IsStop = true,
                    }).ToList()
                );

                // 添加修改項目至result
                result.FunctionCodeList.AddRange(
                    dbData.FunctionCodeList.Where(x => editFunctionCode.Contains(x.FunctionCodeId)).Select(x => new FunctionCode_Model
                    {
                        CRUD = E_CRUD.U,
                        Id = x.Id,
                        CompanyId = result.CompanyId.Value,
                        RoleId = x.RoleId,
                        FunctionCodeId = x.FunctionCodeId,
                        IsStop = x.IsStop,
                    }).ToList()
                );

                // 添加刪除項目至result
                result.FunctionCodeList.AddRange(
                    dbData.FunctionCodeList.Where(x => delFunctionCode.Contains(x.FunctionCodeId)).Select(x => new FunctionCode_Model
                    {
                        CRUD = E_CRUD.D,
                        Id = x.Id,
                        CompanyId = result.CompanyId.Value,
                        RoleId = x.RoleId,
                        FunctionCodeId = x.FunctionCodeId,
                        IsStop = x.IsStop,
                    }).ToList()
                );
                #endregion
            }

            return result;
        }

        /// <summary>
        /// 【多筆】【可分頁】取角色清單
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <param name="pageingDTO">分頁條件</param>
        /// <returns></returns>
        public ResultSimpleData<List<Role_List>> GetList_Role(Role_Filter input, Pageing_DTO pageingDTO)
        {
            var result = new ResultSimpleData<List<Role_List>>(true, new List<Role_List>());
            var methodParam = new MethodParameter(input.BindKey); // 方法的通用屬性參數

            // 角色-語法命令
            var dbDatas = _unitOfWork._RoleRepository.GetAll();

            #region == 過濾 ==
            // 角色-過濾命令(ref)
            this._unitOfWork._RoleRepository.WhereQueryable(ref dbDatas, input);
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
            result.Data = dbDatas.Select(x => new Role_List
            {
                Id = x.Id,
                No = x.No,
                Name = x.Name,
                PermissionTypeId = x.PermissionTypeId,
                Rem = x.Rem,
                CompanyId = x.CompanyId,
            }).ToList();
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】取得角色下拉清單
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<List<SelectItemDTO>> GetSimpleDropList_Role(Role_Filter input)
        {
            var result = new ResultSimpleData<List<SelectItemDTO>>(true, new List<SelectItemDTO>());
            var pageingDTO = new Pageing_DTO();

            // 建置查詢值(避免前面多傳，造成過濾不準確)
            var query = new Role_Filter
            {
                Id = input.Id,
            };

            result.Data = this._unitOfWork._RoleRepository.GetDropList(query, ref pageingDTO, null);
            result.PageingDTO = pageingDTO;
            return result;
        }

        /// <summary>
        /// 【多筆】取得角色下拉清單
        /// </summary>
        /// <param name="input">Id為[預選項目]的判斷</param>
        /// <param name="defaultItemText">是否提供預設的Null項目(在第一筆)[沒給不產生][有給產生相同Text的項目]</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<List<SelectItemDTO>> GetSimpleDropList_Role(Role_Filter input, string defaultItemText)
        {
            var result = new ResultSimpleData<List<SelectItemDTO>>(true, new List<SelectItemDTO>());
            var pageingDTO = new Pageing_DTO();

            result.Data = this._unitOfWork._RoleRepository.GetDropList(input, ref pageingDTO, defaultItemText);
            result.PageingDTO = pageingDTO;
            return result;
        }

        /// <summary>
        /// 【Obj】【分頁】取得角色下拉清單資訊
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <param name="pageingDTO">分頁</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<object> GetSimpleDropList_Role_Obj(Role_Filter input, Pageing_DTO pageingDTO)
        {
            var result = new ResultSimpleData<object>();

            var dropList = this._unitOfWork._RoleRepository.GetDropList(input, ref pageingDTO, null);
            result.Data = dropList;
            result.PageingDTO = pageingDTO;
            return result;
        }
        #endregion

        #region == 存資料相關-角色 ==
        /// <summary>
        /// 【單筆】新增角色
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Create_Role(Role_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<Role_Model> { input };
            // 取得結果
            var resultData = this.Creates_Role(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】新增角色
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Creates_Role(List<Role_Model> inputs)
        {
            var result = new ResultSimpleData<List<long>>(true, new List<long>());
            var methodParam = new MethodParameter(); // 方法的通用屬性參數

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
                    { nameof(input.PermissionTypeId), "權限" },
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
                methodParam.CheckResult = this._unitOfWork._RoleRepository.CheckExist(input.Id);
                // [T：不存在，新增][F：存在，Error]
                if (!methodParam.CheckResult)
                {
                    Role_Model newData = input;

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
                    var resultNewData = this.GenerateData_Role(input, methodParam.TodayFull);
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

                    #region == 【整理資料】角色 ==
                    var addData = new Role
                    {
                        CompanyId = newData.CompanyId.Value,
                        No = newData.No,
                        Name = newData.Name,
                        PermissionTypeId = newData.PermissionTypeId.Value,
                        PermissionTypeName = newData.PermissionTypeId.Value.ToString(),
                        IsStop = newData.IsStop,
                        Rem = newData.Rem,
                        CreateTime = methodParam.Today,
                        CreateManId = _MainSystem_DTO.UserSession.UserId,
                        CreateManName = _MainSystem_DTO.UserSession.UserName,
                        UpdateTime = methodParam.Today,
                        UpdateManId = _MainSystem_DTO.UserSession.UserId,
                        UpdateManName = _MainSystem_DTO.UserSession.UserName,
                    };
                    #endregion

                    #region == 【整理資料】功能代碼 ==
                    addData.FunctionCodeList = input.FunctionCodeList.Select(x => new FunctionCode
                    {
                        CompanyId = newData.CompanyId.Value,
                        FunctionCodeId = x.FunctionCodeId.Value,
                        FunctionCodeName = x.FunctionCodeId.Value.ToString(),
                        IsStop = x.IsStop,
                        CreateTime = methodParam.Today,
                        CreateManId = _MainSystem_DTO.UserSession.UserId,
                        CreateManName = _MainSystem_DTO.UserSession.UserName,
                        UpdateTime = methodParam.Today,
                        UpdateManId = _MainSystem_DTO.UserSession.UserId,
                        UpdateManName = _MainSystem_DTO.UserSession.UserName,
                    }).ToList();
                    #endregion

                    #region == 【DB】寫入 (Error continue) ==
                    try
                    {
                        this._unitOfWork._RoleRepository.Add(addData);
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
        /// 【單筆】修改角色
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Edit_Role(Role_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值查詢
            var inputs = new List<Role_Model> { input };
            // 取得結果
            var resultData = this.Edits_Role(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】修改角色
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Edits_Role(List<Role_Model> inputs)
        {
            ResultSimpleData<List<long>> result = new ResultSimpleData<List<long>>(true, new List<long>());
            MethodParameter methodParam = new MethodParameter(); // 方法的通用屬性參數

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
                    { nameof(input.PermissionTypeId), "權限" },
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
                var editData = _unitOfWork._RoleRepository.GetAlls(x => x.Id == input.Id).FirstOrDefault();
                // 是否存在。 [T：存在，修改][F：不存在，Error]
                if (editData != null)
                {
                    Role_Model newData = input;

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
                    var resultNewData = this.GenerateData_Role(input, methodParam.TodayFull);
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
                    editData.PermissionTypeId = newData.PermissionTypeId.Value;
                    editData.PermissionTypeName = newData.PermissionTypeId.Value.ToString();
                    editData.IsStop = newData.IsStop;
                    editData.Rem = newData.Rem;
                    editData.UpdateTime = methodParam.Today;
                    editData.UpdateManId = _MainSystem_DTO.UserSession.UserId;
                    editData.UpdateManName = _MainSystem_DTO.UserSession.UserName;
                    #endregion

                    #region == 【DB Save】寫入-角色 (Error continue) ==
                    try
                    {
                        this._unitOfWork._RoleRepository.Update(editData);
                        this._unitOfWork.Save();
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

                    #region == 【DB Save】寫入-功能代碼 (Error continue) ==
                    try
                    {
                        var result_UpdateFunctionCode = this.Update_FunctionCode(newData, newData.FunctionCodeList, methodParam.TodayFull);
                        // [T：成功][F：失敗]
                        if (result_UpdateFunctionCode.IsSuccess)
                        {
                            //// 添加Log訊息
                            //methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.注意, E_StatusCode.成功, methodParam.ComFocusText);
                            //methodParam.MessageDTO.Message = $"功能代碼更新成功";
                            //this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);
                        }
                        else
                        {
                            // 添加Log訊息
                            methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.失敗, methodParam.ComFocusText);
                            methodParam.MessageDTO.Message = $"功能代碼更新失敗";
                            this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                            result.IsSuccess = false;
                            continue;
                        }
                    }
                    catch (Exception ex) //error
                    {
                        // 添加Log訊息
                        methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.失敗, methodParam.ComFocusText);
                        methodParam.MessageDTO.Message = $"功能代碼處理失敗，資料庫存取異常";
                        this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                        result.IsSuccess = false;
                        continue;
                    }
                    #endregion

                    #region == 成功的Message整理 ==
                    // 添加Log訊息
                    methodParam.MessageDTO = new Message_DTO(true, methodParam.BindKey, E_StatusLevel.正常, E_StatusCode.成功, methodParam.ComFocusText);
                    methodParam.MessageDTO.Message = $"修改成功";
                    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                    result.Data.Add(editData.Id);
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
        /// 【多筆】更新功能代碼 [默認表身中，所有表頭Key都相同]
        /// </summary>
        /// <param name="inputMF">表頭資料，請確保資料是與[主系統]執行[新增、修改]時的資料規格一致</param>
        /// <param name="inputTFs">表身資料，請確保資料是與[主系統]執行[新增、修改]時的資料規格一致</param>
        /// <param name="today">當前時間</param>
        /// <returns></returns>
        public ResultOutput_Data<string> Update_FunctionCode(Role_Model inputMF, List<FunctionCode_Model> inputTFs, DateTime today)
        {
            /// 目前只有修改的時候會被內部觸發(表頭處理完後執行)，故不進行外部處發時的檢查，未來如果要外部觸發，請記得重新調整。
            /// 基於內部觸發，表頭處理階段就已經做過[公司權限]的檢查，故此處不再檢查。

            var result = new ResultOutput_Data<string>(true, null);
            MethodParameter methodParam = new MethodParameter(today); // 方法的通用屬性參數

            #region == 增刪修 ==
            //處理順序：D → U → C → R(不處理) 
            foreach (var itemTF in inputTFs.OrderByDescending(o => o.CRUD))
            {
                // 重置Model
                methodParam.Reset_Message(inputMF.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                // [有無功能代碼][T：有][F：無]
                if (itemTF.FunctionCodeId.HasValue)
                {
                    methodParam.ComFocusText = $"公司Key[{inputMF.CompanyId}]---Key[{inputMF.Id}]---代號[{inputMF.No}]---功能代碼Key[{itemTF.Id}]---功能代碼Id[{(int)itemTF.FunctionCodeId}]";
                }
                else
                {
                    methodParam.ComFocusText = $"公司Key[{inputMF.CompanyId}]---Key[{inputMF.Id}]---代號[{inputMF.No}]---功能代碼Key[{itemTF.Id}]";
                }
                

                switch (itemTF.CRUD)
                {
                    case E_CRUD.R:
                        break;
                    case E_CRUD.C:
                        #region == 處理 ==
                        var createCheck = this._unitOfWork._FunctionCodeRepository.GetAlls(x => x.RoleId == inputMF.Id && x.FunctionCodeId == itemTF.FunctionCodeId).Any();
                        // [T：查無，可新增][F：查有，Error]
                        if (!createCheck)
                        {
                            #region == 整理資料 ==
                            var addData = new FunctionCode
                            {
                                CompanyId = inputMF.CompanyId.Value,
                                FunctionCodeId = itemTF.FunctionCodeId.Value,
                                FunctionCodeName = itemTF.FunctionCodeId.Value.ToString(),
                                RoleId = itemTF.RoleId.Value,
                                IsStop = itemTF.IsStop,
                                Rem = itemTF.Rem,
                                CreateTime = methodParam.Today,
                                CreateManId = _MainSystem_DTO.UserSession.UserId,
                                CreateManName = _MainSystem_DTO.UserSession.UserName,
                                UpdateTime = methodParam.Today,
                                UpdateManId = _MainSystem_DTO.UserSession.UserId,
                                UpdateManName = _MainSystem_DTO.UserSession.UserName,
                            };
                            #endregion

                            #region == 【DB】寫入 ==
                            try
                            {
                                this._unitOfWork._FunctionCodeRepository.Add(addData);
                                this._unitOfWork.Save();
                            }
                            catch (Exception ex)
                            {
                                // 添加Log訊息
                                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                                methodParam.MessageDTO.Message = $"功能代碼新增，資料庫存取異常";
                                methodParam.MessageDTO.Message_Exception = ex.Message;
                                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                                result.IsSuccess = false;
                                continue;
                            }
                            finally
                            {
                                //nextSN++;
                            }
                            #endregion
                        }
                        else
                        {
                            // 添加Log訊息
                            methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                            methodParam.MessageDTO.Message = $"功能代碼新增，存在相同資料";
                            this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                            result.IsSuccess = false;
                            continue;
                        }
                        #endregion
                        break;
                    case E_CRUD.U:
                        #region == 處理 ==
                        var editData = this._unitOfWork._FunctionCodeRepository.GetAlls(x => x.Id == itemTF.Id).FirstOrDefault();
                        // [T：查有，修改][F：查無，新增]
                        if (editData != null)
                        {
                            #region == 整理資料 ==
                            //editData.FunctionCodeId = itemTF.FunctionCodeId;
                            editData.FunctionCodeName = itemTF.FunctionCodeId.Value.ToString();
                            editData.IsStop = itemTF.IsStop;
                            editData.Rem = itemTF.Rem;
                            editData.UpdateTime = methodParam.Today;
                            editData.UpdateManId = _MainSystem_DTO.UserSession.UserId;
                            editData.UpdateManName = _MainSystem_DTO.UserSession.UserName;
                            #endregion

                            #region == 【DB】寫入 ==
                            try
                            {
                                this._unitOfWork._FunctionCodeRepository.Update(editData);
                                this._unitOfWork.Save();
                            }
                            catch (Exception ex)
                            {
                                // 添加Log訊息
                                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                                methodParam.MessageDTO.Message = $"功能代碼修改，資料庫存取異常";
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
                            methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                            methodParam.MessageDTO.Message = $"功能代碼修改，查無項目";
                            this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                            result.IsSuccess = false;
                            continue;
                        }
                        #endregion
                        break;
                    case E_CRUD.D:
                        #region == 處理 ==
                        var delData = this._unitOfWork._FunctionCodeRepository.GetAlls(x => x.Id == itemTF.Id).FirstOrDefault();
                        // [T：查有，刪除][F：查無，Error]
                        if (delData != null)
                        {
                            #region == 【DB】刪除 ==
                            try
                            {
                                this._unitOfWork._FunctionCodeRepository.Delete(delData);
                                this._unitOfWork.Save();
                            }
                            catch (Exception ex)
                            {
                                // 添加Log訊息
                                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.資料存取異常, methodParam.ComFocusText);
                                methodParam.MessageDTO.Message = $"功能代碼刪除，資料庫存取異常";
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
                            methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                            methodParam.MessageDTO.Message = $"功能代碼刪除，查無項目";
                            this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                            result.IsSuccess = false;
                            continue;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region == 整理錯訊 [沒有直接Error，才會執行到這] ==
            if (result.IsSuccess)
            {
                result.E_StatusCode = E_StatusCode.成功;
                result.Title = "【表身-更新成功】";
            }
            else
            {
                result.E_StatusCode = E_StatusCode.失敗;
                result.Title = "【表身-更新失敗】";
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】刪除角色
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Delete_Role(Role_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<Role_Model> { input };
            // 取得結果
            var resultData = this.Deletes_Role(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】刪除角色
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Deletes_Role(List<Role_Model> inputs)
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
                var delData = _unitOfWork._RoleRepository.Get(x => x.Id == input.Id);
                //是否存在。 [T：存在，刪除][F：不存在，Error]
                if (delData != null)
                {
                    //把還需要用到的值保留起來，因為刪掉之後就抓不到了
                    var tempVal = new Role_DTO
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

                    #region == 【檢查】關聯 ==
                    // ...
                    #endregion

                    #region == 【DB】寫入 (Error continue) ==
                    try
                    {
                        this._unitOfWork._RoleRepository.Delete(delData);
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

        #region == 整理相關-角色 ==
        /// <summary>
        /// 【整理】重新整理資料規格 (補上Key值)(只處理[主系統]以外的來源資料)(將資料處理成[主系統]執行[新增、修改]時的規格)
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="dataFrom">資料來源地</param>
        /// <param name="toRoleId">要寫進哪間角色</param>
        /// <returns></returns>
        //public ResultOutput_Data<List<Role_Model>> ImportDatas_Format_Role(List<Role_Model> inputs, E_DataFrom dataFrom, long toRoleId)
        //{
        //    var result = new ResultOutput_Data<List<Role_Model>>(true, new List<Role_Model>()); //回傳結果[預設成功]
        //    var check = false; //檢查結果(使用前請先重置)

        //    #region == 檢查-input ==
        //    if (inputs == null || inputs.Count() == 0)
        //    {
        //        result = new ResultOutput_Data<List<Role_Model>>(false, E_StatusCode.失敗, "未傳入資料", new List<Role_Model>());
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
        //            result = new ResultOutput_Data<List<Role_Model>>(false, E_StatusCode.失敗, "有未提供[角色代號]的項目", new List<Role_Model>());
        //            return result;
        //        }
        //        #endregion

        //        #region == 取本次會用到的角色DTO ==
        //        //// 查詢值[角色+角色代號]
        //        //var RoleQueryVals = inputs.Where(x => x.Seller_No != null && x.Seller_No != "").Select(x => toRoleId + "___" + x.Seller_No).Distinct().ToList();
        //        //_Role_DTOs = _Role_Repository.GetAlls(x => RoleQueryVals.Contains(x.Role_Id + "___" + x.No)).Select(x => new Role_DTO
        //        //{
        //        //    Id = x.Id,
        //        //    No = x.No,
        //        //    Name = x.Name,
        //        //    Role_Id = x.Role_Id,
        //        //}).ToList();
        //        #endregion

        //        #region == 整理-補值 ==
        //        // 角色代號清單 (外部Key)
        //        var cNos = inputs.Select(x => x.No).ToList();
        //        // 取主系統內的資料 (外部Key + 寫入角色Key)
        //        var data_MainSys = _Role_Repository.GetAlls(x => cNos.Contains(x.No) && x.Role_Id == toRoleId).ToList();
        //        // 走訪inputs，填入主系統資料
        //        foreach (var input in inputs)
        //        {
        //            #region == 無須增修判斷的整理 ==
        //            // 填入角色
        //            input.Role_Id = toRoleId;
        //            // 填入資料來源
        //            input.DataFrom_Id = dataFrom;
        //            // 填入角色Key
        //            //input.Seller_Id = _Role_DTOs.Where(x => x.No == input.Seller_No).Select(x => x.Id).FirstOrDefault();
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
        //        result = new ResultOutput_Data<List<Role_Model>>(false, E_StatusCode.失敗, "整理資料發生錯誤", new List<Role_Model>());
        //        return result;
        //    }
        //    #endregion

        //    result.Data = inputs;
        //    return result;
        //}

        /// <summary>
        /// 【資料+檢查】生成角色資料 [含通用檢查]
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增、修改]時的資料規格一致</param>
        /// <param name="currDate">當前時間</param>
        /// <returns></returns>
        public ResultSimpleData<Role_Model> GenerateData_Role(Role_Model input, DateTime currDate)
        {
            var result = new ResultSimpleData<Role_Model>(true, null); //回傳結果[預設成功]
            var methodParam = new MethodParameter(currDate); // 方法的通用屬性參數

            // 重置Model
            methodParam.Reset_Message(input.BindKey);
            // 關鍵值訊息 (通用的)(如不一樣請客製)
            methodParam.ComFocusText = $"公司Key[{input.CompanyId}]---Key[{input.Id}]---代號[{input.No}]";

            #region == 檢查-公司是否存在 (Error return) ==
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
                    //            input.No = this._unitOfWork._RoleRepository.GenNo(today);
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
            methodParam.CheckResult = this._unitOfWork._RoleRepository.CheckRepeat_ByNo(input.CompanyId.Value, input.Id, input.No);
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

            #region == 【檢查】功能代碼清單的ID(修改、刪除)是否存在null的項目 (Error return) ==
            methodParam.CheckResult = input.FunctionCodeList.Where(x => !x.Id.HasValue && (x.CRUD == E_CRUD.U || x.CRUD == E_CRUD.D)).Any();
            // [修改、刪除的ID是否存在null][T：存在null]
            if (methodParam.CheckResult)
            {
                // 添加Log訊息
                methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.檢查異常, methodParam.ComFocusText);
                methodParam.MessageDTO.Message = $"功能代碼的[修改、刪除]項目存在[ID]為null的項目";
                this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

                result.IsSuccess = false;
                return result;
            }
            #endregion

            result.Data = input;
            return result;
        }
        #endregion

        #region == 其它-角色 ==
        //...
        #endregion
        #endregion
    }
}
