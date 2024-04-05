using Main_Common.Enum.E_ProjectType;
using Main_Common.GlobalSetting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Account
{
    /// <summary>
    /// 【Model】登入者資訊
    /// </summary>
    public class UserSession_Model
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 登入Id
        /// </summary>
        [Display(Name = "登入Id")]
        public Guid? LoginId { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        [Display(Name = "帳號")]
        public string? Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Display(Name = "密碼")]
        public string? Password { get; set; }
        /// <summary>
        /// 公司Id [0：視為程式內部登入]
        /// </summary>
        [Display(Name = "公司Id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司代號
        /// </summary>
        [Display(Name = "公司代號")]
        public string? CompanyNo { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Display(Name = "公司名稱")]
        public string? CompanyName { get; set; }
        /// <summary>
        /// 公司等級
        /// </summary>
        [Display(Name = "公司等級")]
        public E_CompanyLevel CompanyLevelId { get; set; }
        /// <summary>
        /// 使用者Id [0：視為程式內部登入]
        /// </summary>
        [Display(Name = "使用者Id")]
        public long UserId { get; set; }
        /// <summary>
        /// 使用者代號
        /// </summary>
        [Display(Name = "使用者代號")]
        public string? UserNo { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Display(Name = "使用者名稱")]
        public string? UserName { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        [Display(Name = "信箱")]
        public string? Mail { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = "角色Id")]
        public long RoleId { get; set; }
        /// <summary>
        /// 角色名稱
        /// </summary>
        [Display(Name = "角色名稱")]
        public string? RoleName { get; set; }
        /// <summary>
        /// 權限種類Id
        /// </summary>
        [Display(Name = "權限種類Id")]
        public E_PermissionType PermissionTypeId { get; set; }
        /// <summary>
        /// 功能代碼
        /// </summary>
        [Display(Name = "功能代碼")]
        public List<E_Function> Functions { get; set; }
        /// <summary>
        /// 登入保持天數
        /// </summary>
        [Display(Name = "登入保持天數")]
        public int? LoginKeepDay { get; set; }
        /// <summary>
        /// 是否為後門權限
        /// </summary>
        [Display(Name = "是否為後門權限")]
        public bool IsBackDoor { get; set; }
        /// <summary>
        /// 是否需檢查密碼
        /// </summary>
        [Display(Name = "是否需檢查密碼")]
        public bool IsNeedCheckPassword { get; set; }
        /// <summary>
        /// 後門種類Id
        /// </summary>
        [Display(Name = "後門種類Id")]
        public E_BackDoorType BackDoorTypeId { get; set; }
        /// <summary>
        /// 登入來源種類Id
        /// </summary>
        [Display(Name = "登入來源種類Id")]
        public E_LoginFromType LoginFromTypeId { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        /// <summary>
        /// 綁定Key (用來將本次執行相關的資料串起來)
        /// </summary>
        [Display(Name = "綁定Key")]
        public Guid BindKey { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public UserSession_Model()
        {
            this.Functions = new List<E_Function>();
        }

        /// <summary>
        /// 建構-初始值，依後門種類初始化資料
        /// </summary>
        /// <param name="eBackDoorType">後門種類</param>
        /// <remarks>通常只有透過後門帳密登入的會需要初始化</remarks>
        public UserSession_Model(E_BackDoorType eBackDoorType)
        {
            this.Functions = new List<E_Function>();

            this.IsBackDoor = true;
            this.IsNeedCheckPassword = true;
            this.BackDoorTypeId = eBackDoorType;
            this.CompanyLevelId = E_CompanyLevel.最高級;
            this.UserId = 0; 
            this.RoleId = 0;
            this.RoleName = E_PermissionType.AdminBackDoor.ToString();
            this.PermissionTypeId = E_PermissionType.AdminBackDoor;
            this.Functions = System.Enum.GetValues(typeof(E_Function)).Cast<E_Function>().ToList();

            // 依後門種類區分
            switch (eBackDoorType)
            {
                case E_BackDoorType.COM:
                    this.UserNo = GlobalParameter.Account;
                    this.UserName = GlobalParameter.Account;
                    this.Account = GlobalParameter.Account;
                    this.Password = GlobalParameter.Password;
                    break;
                case E_BackDoorType.RD:
                    this.UserNo = GlobalParameter.Account_RD;
                    this.UserName = GlobalParameter.Account_RD;
                    this.Account = GlobalParameter.Account_RD;
                    this.Password = GlobalParameter.Password_RD;
                    break;
                case E_BackDoorType.TK:
                    this.UserNo = GlobalParameter.Account_TK;
                    this.UserName = GlobalParameter.Account_TK;
                    this.Account = GlobalParameter.Account_TK;
                    this.Password = GlobalParameter.Password_TK;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
