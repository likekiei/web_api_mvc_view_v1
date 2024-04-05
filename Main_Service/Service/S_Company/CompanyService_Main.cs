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
using Main_Common.Model.Data.Company;
using Main_EF.Table;
using System.Runtime.Intrinsics.Arm;
using Main_EF.Migrations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Main_Common.Model.Data;
using Main_EF.Interface.ITableRepository;
using Main_Common.Model.Data.User;

namespace Main_Service.Service.S_Company
{
    /// <summary>
    /// 【Main】公司相關
    /// </summary>
    public class CompanyService_Main
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
        public CompanyService_Main(IUnitOfWork unitOfWork,
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

        #region == 公司 ================================================================
        #region == 檢查相關-公司 ==
        // ...
        #endregion

        #region == 取資料相關-公司 ==
        /// <summary>
        /// 【單筆】【DTO】取預選公司DTO
        /// </summary>
        /// <param name="bindKey">綁定Key</param>
        /// <returns></returns>
        public Company_DTO? GetDefaultSelected_Company(Guid bindKey)
        {
            Company_DTO? result = null;
            var methodParam = new MethodParameter(bindKey); // 方法的通用屬性參數

            // 過濾條件
            var query = new Company_Filter
            {
                IsDefaultSelected = true,
            };

            // 取得資料
            result = this._unitOfWork._CompanyRepository.GetDTO(query);

            return result;
        }

        /// <summary>
        /// 【單筆】【DTO】取公司DTO
        /// </summary>
        /// <param name="bindKey">綁定Key</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Company_DTO? GetDTO_Company(Guid bindKey, long? key)
        {
            Company_DTO? result = null;
            var methodParam = new MethodParameter(bindKey); // 方法的通用屬性參數

            // 過濾條件
            var query = new Company_Filter
            {
                Id = key,
            };

            // 取得資料
            result = this._unitOfWork._CompanyRepository.GetDTO(query);

            return result;
        }
        
        /// <summary>
        /// 【單筆】【修改用】取公司Model
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Company_Model? GetEditModel_Company(long key)
        {
            Company_Model? result = null;
            var dbData = _unitOfWork._CompanyRepository.Get(x => x.Id == key);
            if (dbData != null)
            {
                result = new Company_Model
                {
                    CRUD = E_CRUD.U,
                    //GUID = dbData.GUID,
                    Id = dbData.Id,
                    No = dbData.No,
                    Name = dbData.Name,
                    CompanyLevelId = dbData.CompanyLevelId,
                    UnifiedNumber = dbData.UnifiedNumber,
                    EMail = dbData.EMail,
                    Tel = dbData.Tel,
                    Fax = dbData.Fax,
                    Address1 = dbData.Address1,
                    Address2 = dbData.Address2,
                    ContactMan = dbData.ContactMan,
                    ContactManPhone = dbData.ContactManPhone,
                    ContactManJobName = dbData.ContactManJobName,
                    ResponsibMan = dbData.ResponsibMan,
                    Rem = dbData.Rem,
                    IsDefaultSelected = dbData.IsDefaultSelected,
                    IsStop = dbData.IsStop,
                    CreateManName = dbData.CreateManName,
                    UpdateManName = dbData.UpdateManName,
                };
            }

            return result;
        }

        /// <summary>
        /// 【多筆】【可分頁】取公司清單
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <param name="pageingDTO">分頁條件</param>
        /// <returns></returns>
        public ResultSimpleData<List<Company_List>> GetList_Company(Company_Filter input, Pageing_DTO pageingDTO)
        {
            var result = new ResultSimpleData<List<Company_List>>(true, new List<Company_List>());
            var methodParam = new MethodParameter(input.BindKey); // 方法的通用屬性參數

            // 公司-語法命令
            var dbDatas = _unitOfWork._CompanyRepository.GetAll();

            #region == 過濾 ==
            // 公司-過濾命令(ref)
            this._unitOfWork._CompanyRepository.WhereQueryable(ref dbDatas, input);
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
            result.Data = dbDatas.Select(x => new Company_List
            {
                Id = x.Id,
                No = x.No,
                Name = x.Name,
                CompanyLevelId = x.CompanyLevelId,
                Rem = x.Rem,
            }).ToList();
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】取得公司下拉清單
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<List<SelectItemDTO>> GetSimpleDropList_Company(Company_Filter input)
        {
            var result = new ResultSimpleData<List<SelectItemDTO>>(true, new List<SelectItemDTO>());
            var pageingDTO = new Pageing_DTO();

            // 建置查詢值(避免前面多傳，造成過濾不準確)
            var query = new Company_Filter
            {
                Id = input.Id,
            };

            result.Data = this._unitOfWork._CompanyRepository.GetDropList(query, ref pageingDTO, null);
            result.PageingDTO = pageingDTO;
            return result;
        }

        /// <summary>
        /// 【多筆】取得公司下拉清單
        /// </summary>
        /// <param name="input">Id為[預選項目]的判斷</param>
        /// <param name="defaultItemText">是否提供預設的Null項目(在第一筆)[沒給不產生][有給產生相同Text的項目]</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<List<SelectItemDTO>> GetSimpleDropList_Companys(Company_Filter input, string defaultItemText)
        {
            var result = new ResultSimpleData<List<SelectItemDTO>>(true, new List<SelectItemDTO>());
            var pageingDTO = new Pageing_DTO();

            result.Data = this._unitOfWork._CompanyRepository.GetDropList(input, ref pageingDTO, defaultItemText);
            result.PageingDTO = pageingDTO;
            return result;
        }

        /// <summary>
        /// 【Obj】【分頁】取得公司下拉清單資訊
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <param name="pageingDTO">分頁</param>
        /// <returns>未特別處理，默認成功</returns>
        public ResultSimpleData<object> GetSimpleDropList_Company_Obj(Company_Filter input, Pageing_DTO pageingDTO)
        {
            var result = new ResultSimpleData<object>();

            var dropList = this._unitOfWork._CompanyRepository.GetDropList(input, ref pageingDTO, null);
            result.Data = dropList;
            result.PageingDTO = pageingDTO;
            return result;
        }
        #endregion

        #region == 存資料相關-公司 ==
        /// <summary>
        /// 【初始資料】生成公司相關的初始資料 (強制寫死最高級)
        /// </summary>
        /// <returns></returns>
        public ResultSimple Init_CompanyRelatedTable()
        {
            var today = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString());
            var result = new ResultSimple(true, "");
            var check = false;

            #region == 處理 ==
            try
            {
                // 如要調整，請檢查程式內部其它有使用到的地方。
                // 是否存在0000A
                check = _unitOfWork._CompanyRepository.CheckRepeat_ByNo(null, "0000A");
                // [T：查無]
                if (!check)
                {
                    var addData = new Companys
                    {
                        //Id = 0,
                        No = "0000A",
                        Name = "0000A",
                        CompanyLevelId = E_CompanyLevel.最高級,
                        CompanyLevelName = E_CompanyLevel.最高級.ToString(),
                        EMail = "0000A@0000.com",
                        IsDefaultSelected = true,
                        IsStop = false,
                        Rem = "系統資料初始化",
                        CreateTime = today,
                        UpdateTime = today,
                        SystemTimestampInfo = new SystemTimestamp
                        {
                            Rem = "系統生成",
                            CreateTime = today,
                            UpdateTime = today,
                        },
                    };

                    _unitOfWork._CompanyRepository.Add(addData);
                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                return new ResultSimple(false, "【初始化失敗】公司資料，請聯繫相關人員");
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 【單筆】新增公司
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Create_Company(Company_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<Company_Model> { input };
            // 取得結果
            var resultData = this.Creates_Company(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】新增公司
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Creates_Company(List<Company_Model> inputs)
        {
            var result = new ResultSimpleData<List<long>>(true, new List<long>());
            var methodParam = new MethodParameter(); // 方法的通用屬性參數

            #region == 迴圈處理 ==
            foreach (var input in inputs)
            {
                // 重置Model
                methodParam.Reset_Message(input.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                methodParam.ComFocusText = $"Key[{input.Id}]---代號[{input.No}]";

                #region == 【檢查】必填 (Error continue) ==
                // 檢查指定屬性有無值
                methodParam.ErrorTexts = DataValidationTool.Check_ModelAttrIsNull(input, new Dictionary<string, string>()
                {
                    { nameof(input.No), "代號" },
                    { nameof(input.Name), "名稱" },
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
                methodParam.CheckResult = this._unitOfWork._CompanyRepository.CheckExist(input.Id);
                // [T：不存在，新增][F：存在，Error]
                if (!methodParam.CheckResult)
                {
                    Company_Model newData = input;

                    #region == 【檢查】權限 (Error continue) ==
                    // 檢查
                    methodParam.ResultSimple = DataValidationTool.Is_AllowAction_ByCompanyLevelCheck(_MainSystem_DTO.UserSession, null);
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
                    var resultNewData = this.GenerateData_Company(input, methodParam.TodayFull);
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
                    var addData = new Companys
                    {
                        //GUID = Guid.NewGuid(),
                        //Id = newData.Id,
                        No = newData.No,
                        Name = newData.Name,
                        CompanyLevelId = newData.CompanyLevelId,
                        CompanyLevelName = newData.CompanyLevelId.ToString(),
                        EMail = newData.EMail,
                        Tel = newData.Tel,
                        Fax = newData.Fax,
                        Address1 = newData.Address1,
                        Address2 = newData.Address2,
                        ContactMan = newData.ContactMan,
                        ContactManJobName = newData.ContactManJobName,
                        ContactManPhone = newData.ContactManPhone,
                        ResponsibMan = newData.ResponsibMan,
                        UnifiedNumber = newData.UnifiedNumber,
                        IsStop = newData.IsStop,
                        Rem = newData.Rem,
                        CreateTime = methodParam.Today,
                        CreateManId = this._MainSystem_DTO.UserSession.CompanyId,
                        CreateManName = this._MainSystem_DTO.UserSession.CompanyName,
                        UpdateTime = methodParam.Today,
                        UpdateManId = this._MainSystem_DTO.UserSession.CompanyId,
                        UpdateManName = this._MainSystem_DTO.UserSession.CompanyName,
                        SystemTimestampInfo = new SystemTimestamp
                        {
                            CreateTime = methodParam.Today,
                            UpdateTime = methodParam.Today,
                            Rem = "系統生成",
                        },
                    };
                    #endregion

                    #region == 【DB】寫入 (Error continue) ==
                    try
                    {
                        this._unitOfWork._CompanyRepository.Add(addData);
                        this._unitOfWork.Save();

                        // 添加Log訊息
                        methodParam.TmpFocusText = $"Key[{addData.Id}]---代號[{input.No}]";
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
        /// 【單筆】修改公司
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Edit_Company(Company_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值查詢
            var inputs = new List<Company_Model> { input };
            // 取得結果
            var resultData = this.Edits_Company(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】修改公司
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[修改]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Edits_Company(List<Company_Model> inputs)
        {
            ResultSimpleData<List<long>> result = new ResultSimpleData<List<long>>(true, new List<long>());
            MethodParameter methodParam = new MethodParameter(); // 方法的通用屬性參數

            #region == 迴圈處理 ==
            foreach (var input in inputs)
            {
                // 重置Model
                methodParam.Reset_Message(input.BindKey);
                // 關鍵值訊息 (通用的)(如不一樣請客製)
                methodParam.ComFocusText = $"Key[{input.Id}]---代號[{input.No}]";

                #region == 【檢查】必填 (Error continue) ==
                // 檢查指定屬性有無值
                methodParam.ErrorTexts = DataValidationTool.Check_ModelAttrIsNull(input, new Dictionary<string, string>()
                {
                    { nameof(input.Id), "Id" },
                    { nameof(input.No), "代號" },
                    { nameof(input.Name), "名稱" },
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
                var editData = _unitOfWork._CompanyRepository.GetAlls(x => x.Id == input.Id)
                                                             .Include(x => x.SystemTimestampInfo)
                                                             .FirstOrDefault();
                // 是否存在。 [T：存在，修改][F：不存在，Error]
                if (editData != null)
                {
                    Company_Model newData = input;

                    #region == 【檢查】權限 (Error continue) ==
                    // 檢查
                    methodParam.ResultSimple = DataValidationTool.Is_AllowAction_ByCompanyLevelCheck(_MainSystem_DTO.UserSession, null);
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
                    var resultNewData = this.GenerateData_Company(input, methodParam.TodayFull);
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
                    editData.Name = newData.Name;
                    editData.CompanyLevelId = newData.CompanyLevelId;
                    editData.CompanyLevelName = newData.CompanyLevelId.ToString();
                    editData.EMail = newData.EMail;
                    editData.Tel = newData.Tel;
                    editData.Fax = newData.Fax;
                    editData.Address1 = newData.Address1;
                    editData.Address2 = newData.Address2;
                    editData.ContactMan = newData.ContactMan;
                    editData.ContactManJobName = newData.ContactManJobName;
                    editData.ContactManPhone = newData.ContactManPhone;
                    editData.ResponsibMan = newData.ResponsibMan;
                    editData.UnifiedNumber = newData.UnifiedNumber;
                    editData.IsStop = newData.IsStop;
                    editData.Rem = newData.Rem;
                    editData.UpdateTime = methodParam.Today;
                    editData.UpdateManId = this._MainSystem_DTO.UserSession.CompanyId;
                    editData.UpdateManName = this._MainSystem_DTO.UserSession.CompanyName;

                    // [有無系統時間戳記][T：無值，添加]
                    if (editData.SystemTimestampInfo == null)
                    {
                        editData.SystemTimestampInfo = new SystemTimestamp
                        {
                            CompanyId = editData.Id,
                            CreateTime = methodParam.Today,
                            UpdateTime = methodParam.Today,
                            Rem = "系統生成",
                        };
                    }
                    #endregion

                    #region == 【DB】寫入 (Error continue) ==
                    try
                    {
                        this._unitOfWork._CompanyRepository.Update(editData);
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
        /// 【單筆】刪除公司
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<long?> Delete_Company(Company_Model input)
        {
            var result = new ResultSimpleData<long?>(); // 回傳成功的資料
            // 附值
            var inputs = new List<Company_Model> { input };
            // 取得結果
            var resultData = this.Deletes_Company(inputs);
            // 整理結果
            result.IsSuccess = resultData.IsSuccess;
            result.E_StatusCode = resultData.E_StatusCode;
            result.Title = resultData.Title;
            result.Message = resultData.Message;
            result.Data = resultData.Data != null && resultData.Data.Count() > 0 ? resultData.Data.FirstOrDefault() : new Nullable<long>();
            return result;
        }

        /// <summary>
        /// 【多筆】刪除公司
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[刪除]時的資料規格一致</param>
        /// <returns>有需要可多回傳Data(目前回傳成功的Key)</returns>
        public ResultSimpleData<List<long>> Deletes_Company(List<Company_Model> inputs)
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
                var delData = _unitOfWork._CompanyRepository.Get(x => x.Id == input.Id);
                //是否存在。 [T：存在，刪除][F：不存在，Error]
                if (delData != null)
                {
                    //把還需要用到的值保留起來，因為刪掉之後就抓不到了
                    var tempVal = new Company_DTO
                    {
                        Id = delData.Id,
                        No = delData.No,
                    };

                    #region == 【檢查】權限 (Error continue) ==
                    // 檢查
                    methodParam.ResultSimple = DataValidationTool.Is_AllowAction_ByCompanyLevelCheck(_MainSystem_DTO.UserSession, null);
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
                        this._unitOfWork._CompanyRepository.Delete(delData);
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

        #region == 整理相關-公司 ==
        /// <summary>
        /// 【整理】重新整理資料規格 (補上Key值)(只處理[主系統]以外的來源資料)(將資料處理成[主系統]執行[新增、修改]時的規格)
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="dataFrom">資料來源地</param>
        /// <param name="toCompanyId">要寫進哪間公司</param>
        /// <returns></returns>
        //public ResultOutput_Data<List<Company_Model>> ImportDatas_Format_Company(List<Company_Model> inputs, E_DataFrom dataFrom, long toCompanyId)
        //{
        //    var result = new ResultOutput_Data<List<Company_Model>>(true, new List<Company_Model>()); //回傳結果[預設成功]
        //    var check = false; //檢查結果(使用前請先重置)

        //    #region == 檢查-input ==
        //    if (inputs == null || inputs.Count() == 0)
        //    {
        //        result = new ResultOutput_Data<List<Company_Model>>(false, E_StatusCode.失敗, "未傳入資料", new List<Company_Model>());
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
        //            result = new ResultOutput_Data<List<Company_Model>>(false, E_StatusCode.失敗, "有未提供[公司代號]的項目", new List<Company_Model>());
        //            return result;
        //        }
        //        #endregion

        //        #region == 取本次會用到的公司DTO ==
        //        //// 查詢值[公司+公司代號]
        //        //var CompanyQueryVals = inputs.Where(x => x.Seller_No != null && x.Seller_No != "").Select(x => toCompanyId + "___" + x.Seller_No).Distinct().ToList();
        //        //_Company_DTOs = _Company_Repository.GetAlls(x => CompanyQueryVals.Contains(x.Company_Id + "___" + x.No)).Select(x => new Company_DTO
        //        //{
        //        //    Id = x.Id,
        //        //    No = x.No,
        //        //    Name = x.Name,
        //        //    Company_Id = x.Company_Id,
        //        //}).ToList();
        //        #endregion

        //        #region == 整理-補值 ==
        //        // 公司代號清單 (外部Key)
        //        var cNos = inputs.Select(x => x.No).ToList();
        //        // 取主系統內的資料 (外部Key + 寫入公司Key)
        //        var data_MainSys = _Company_Repository.GetAlls(x => cNos.Contains(x.No) && x.Company_Id == toCompanyId).ToList();
        //        // 走訪inputs，填入主系統資料
        //        foreach (var input in inputs)
        //        {
        //            #region == 無須增修判斷的整理 ==
        //            // 填入公司
        //            input.Company_Id = toCompanyId;
        //            // 填入資料來源
        //            input.DataFrom_Id = dataFrom;
        //            // 填入公司Key
        //            //input.Seller_Id = _Company_DTOs.Where(x => x.No == input.Seller_No).Select(x => x.Id).FirstOrDefault();
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
        //        result = new ResultOutput_Data<List<Company_Model>>(false, E_StatusCode.失敗, "整理資料發生錯誤", new List<Company_Model>());
        //        return result;
        //    }
        //    #endregion

        //    result.Data = inputs;
        //    return result;
        //}

        /// <summary>
        /// 【資料+檢查】生成公司資料 [含通用檢查]
        /// </summary>
        /// <param name="input">建議資料是與[主系統]執行[新增、修改]時的資料規格一致</param>
        /// <param name="currDate">當前時間</param>
        /// <returns></returns>
        public ResultSimpleData<Company_Model> GenerateData_Company(Company_Model input, DateTime currDate)
        {
            var result = new ResultSimpleData<Company_Model>(true, null); //回傳結果[預設成功]
            var methodParam = new MethodParameter(currDate); // 方法的通用屬性參數

            // 重置Model
            methodParam.Reset_Message(input.BindKey);
            // 關鍵值訊息 (通用的)(如不一樣請客製)
            methodParam.ComFocusText = $"Key[{input.Id}]---代號[{input.No}]";

            #region == 【檢查】公司是否存在 (Error return) ==
            //methodParam.CheckResult = _unitOfWork._CompanyRepository.CheckExist(input.Company_Id);

            //// [T：查無]
            //if (methodParam.CheckResult == false)
            //{
            //    // 添加Log訊息
            //    methodParam.MessageDTO = new Message_DTO(false, methodParam.BindKey, E_StatusLevel.警告, E_StatusCode.查無相關資料, methodParam.ComFocusText);
            //    methodParam.MessageDTO.Message = $"查無公司";
            //    this._LogService_Main.Add_LogResultMessage(methodParam.BindKey, methodParam.MessageDTO);

            //    result.IsSuccess = false;
            //    return result;
            //}
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
                    //            input.No = this._unitOfWork._CompanyRepository.GenNo(today);
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
            methodParam.CheckResult = this._unitOfWork._CompanyRepository.CheckRepeat_ByNo(input.Id, input.No);
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

            result.Data = input;
            return result;
        }
        #endregion

        #region == 其它-公司 ==
        //...
        #endregion
        #endregion
    }
}
