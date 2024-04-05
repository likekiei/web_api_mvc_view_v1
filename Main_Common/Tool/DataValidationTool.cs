using Main_Common.Enum.E_ProjectType;
using Main_Common.ExtensionMethod;
using Main_Common.Model.Account;
using Main_Common.Model.Data.User;
using Main_Common.Model.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Tool
{
    /// <summary>
    /// 資料驗證工具
    /// </summary>
    public static class DataValidationTool
    {
        #region == Model驗證 ==
        /// <summary>
        /// 驗證Model，顯示錯誤訊息
        /// </summary>
        /// <typeparam name="T">要驗證的Model型別</typeparam>
        /// <param name="input">驗證的Model</param>
        /// <param name="ignoreNames">要忽略的名稱</param>
        /// <returns>只回傳驗證錯誤的訊息</returns>
        public static List<string> ValidationResult_ShowMsg<T>(T input, List<string> ignoreNames)
        {
            var errorMsgs = new List<string>();

            // 驗證內容
            ValidationContext vldCtx = new ValidationContext(input, null, null);
            // 存放檢核錯誤List
            List<ValidationResult> errors = new List<ValidationResult>();
            // 驗證執行 (注意第四個參數，要填true，才會檢核Required以外的ValidationAttribute)
            bool check = Validator.TryValidateObject(input, vldCtx, errors, true);
            // 整理錯訊
            foreach (var error in errors)
            {
                // 判斷是否需要忽略部分Name。 [T：忽略不顯示的error msg][F：顯示全部error msg]
                if (ignoreNames != null && ignoreNames.Count() > 0)
                {
                    // 任意名稱相同(true)
                    var isIgnore = ignoreNames.Where(x => x == error.MemberNames.FirstOrDefault()).Any();
                    // 判斷是否顯示msg。 [T：顯示][F：忽略]
                    if (!isIgnore)
                    {
                        errorMsgs.Add($"{{{error.MemberNames.FirstOrDefault()}：{error.ErrorMessage}}}");
                    }
                }
                else
                {
                    errorMsgs.Add($"{{{error.MemberNames.FirstOrDefault()}：{error.ErrorMessage}}}");
                }
            }

            return errorMsgs;
        }
        #endregion

        #region == 公司等級驗證 ==
        /// <summary>
        /// 是否允許執行(依公司等級檢查)
        /// </summary>
        /// <param name="userSessionDTO">登入者資訊DTO</param>
        /// <param name="companyId_Input">要比較的公司Id</param>
        /// <returns>回傳處理結果</returns>
        public static ResultSimple Is_AllowAction_ByCompanyLevelCheck(UserSession_Model userSessionDTO, long? companyId_Input)
        {
            var result = new ResultSimple(false, "系統未正常執行，請聯繫相關人員"); // 默認失敗

            #region == 依公司等級檢查 ==
            switch (userSessionDTO.CompanyLevelId)
            {
                case E_CompanyLevel.最高級: // 不需檢查，默認允許存取。 (該級別的公司，不開放申請)
                    return new ResultSimple(true);
                case E_CompanyLevel.企業級: // 登入者公司、要比較的公司，2者公司必須一致才允許存取。
                    // 是否有值。 [T：皆有值][F：任意無值]
                    if (userSessionDTO.CompanyId != 0 && companyId_Input.HasValue)
                    {
                        // 是否相等。 [T：相等][F：不相等]
                        if (userSessionDTO.CompanyId == companyId_Input)
                        {
                            //return new ResultSimple(true);
                        }
                        else
                        {
                            //不允許新增不同公司的資料
                            return new ResultSimple(false, $"不允許存取，不是相同公司的資料，登入者公司[{userSessionDTO.CompanyId}]→處理的公司[{companyId_Input}]，請聯繫相關人員");
                        }
                    }
                    else
                    {
                        return new ResultSimple(false, "不允許存取，缺資料無法判斷，請聯繫相關人員");
                    }

                    return new ResultSimple(true); // 前面檢查OK，才回傳True。
                default:
                    return new ResultSimple(false, "不允許存取，資料不正確，請聯繫相關人員");
            }
            #endregion
        }

        /// <summary>
        /// 是否允許執行(依公司等級檢查) (檢查傳入公司 and DB內公司)
        /// </summary>
        /// <param name="userSessionDTO">登入者資訊DTO</param>
        /// <param name="companyId_Input">前端傳入的公司Id</param>
        /// <param name="companyId_DB">該資料在DB內的的公司Id</param>
        /// <returns>回傳處理結果</returns>
        public static ResultSimple Is_AllowAction_ByCompanyLevelCheck(UserSession_Model userSessionDTO, long? companyId_Input, long? companyId_DB)
        {
            var result = new ResultSimple(false, "系統未正常執行，請聯繫相關人員"); // 默認失敗

            #region == 依公司等級檢查 ==
            switch (userSessionDTO.CompanyLevelId)
            {
                case E_CompanyLevel.最高級: // 不需檢查，默認允許存取。 (該級別的公司，不開放申請)
                    return new ResultSimple(true);
                case E_CompanyLevel.企業級: // 登入者公司、前端傳入公司、資料內公司，3者公司必須一致才允許存取。
                    #region == 與input的公司Id檢查 ==
                    // 是否有值。 [T：皆有值][F：任意無值]
                    if (userSessionDTO.CompanyId != 0 && companyId_Input.HasValue)
                    {
                        // 是否相等。 [T：相等][F：不相等]
                        if (userSessionDTO.CompanyId == companyId_Input)
                        {
                            //return new ResultSimple(true);
                        }
                        else
                        {
                            return new ResultSimple(false, $"不允許存取，不是相同公司的資料，登入者公司[{userSessionDTO.CompanyId}]→處理的公司(傳入)[{companyId_Input}]，請聯繫相關人員");
                        }
                    }
                    else
                    {
                        return new ResultSimple(false, "不允許存取，缺資料無法判斷，請聯繫相關人員");
                    }
                    #endregion

                    #region == 與DB內的公司Id檢查 ==
                    // 是否有值。 [T：皆有值][F：任意無值]
                    if (userSessionDTO.CompanyId != 0 && companyId_DB.HasValue)
                    {
                        // 是否相等。 [T：相等][F：不相等]
                        if (userSessionDTO.CompanyId == companyId_DB)
                        {
                            //return new ResultOutput(true);
                        }
                        else
                        {
                            return new ResultSimple(false, $"不允許存取，不是相同公司的資料，登入者公司[{userSessionDTO.CompanyId}]→處理的公司(DB)[{companyId_DB}]，請聯繫相關人員");
                        }
                    }
                    else
                    {
                        return new ResultSimple(false, "不允許存取，缺資料無法判斷，請聯繫相關人員");
                    }
                    #endregion

                    return new ResultSimple(true); // 前面檢查OK，才回傳True。
                default:
                    return new ResultSimple(false, "不允許存取，資料不正確，請聯繫相關人員");
            }
            #endregion
        }

        /// <summary>
        /// 是否允許執行(依公司等級檢查)
        /// </summary>
        /// <param name="permissionDTO">登入者的權限DTO</param>
        /// <param name="companyId_Input">前端傳入的公司Id</param>
        /// <returns>回傳處理結果</returns>
        //public static ResultOutput Is_AllowAction_ByCompanyLevelCheck(Permission_DTO permissionDTO, long? companyId_Input)
        //{
        //    var result = new ResultOutput(false, "系統未正常執行，請聯繫相關人員"); // 默認失敗

        //    #region == 依公司等級檢查 ==
        //    switch (permissionDTO.Company_Level_Id)
        //    {
        //        case E_Company_Level.最高級:
        //            return new ResultOutput(true);
        //        case E_Company_Level.企業級:
        //            // 是否有值。 [T：皆有值][F：任意無值]
        //            if (permissionDTO.Company_Id != 0 && companyId_Input.HasValue)
        //            {
        //                // 是否相等。 [T：相等][F：不相等]
        //                if (permissionDTO.Company_Id == companyId_Input)
        //                {
        //                    return new ResultOutput(true);
        //                }
        //                else
        //                {
        //                    return new ResultOutput(false, "不允許存取，非登入者所屬公司的資料，請聯繫相關人員");
        //                }
        //            }
        //            else
        //            {
        //                return new ResultOutput(false, "不允許存取，缺資料無法判斷，請聯繫相關人員");
        //            }
        //        default:
        //            return new ResultOutput(false, "不允許存取，資料不正確，請聯繫相關人員");
        //    }
        //    #endregion
        //}
        #endregion

        #region == 檢查Model欄位是否null ==
        /// <summary>
        /// 檢查Model特定屬性是否為Null
        /// <para>依object的方式檢查</para>
        /// </summary>
        /// <param name="input">驗證的Model</param>
        /// <param name="checkNames">要檢查的名稱<值Key, 值名稱></param>
        /// <returns>回傳檢查結果為Null的項目，格式："欄位名稱[欄位Key]"</returns>
        public static List<string> Check_ModelAttrIsNull(object input, Dictionary<string, string> checkNames)
        {
            var errorMsgs = new List<string>();

            // 走訪要檢查屬性
            foreach (var name in checkNames)
            {
                var check = input.EM_CheckIsNull(name.Key);
                if (check)
                {
                    errorMsgs.Add($"{name.Value}[{name.Key}]");
                }
            }

            return errorMsgs;
        }
        #endregion
    }
}
