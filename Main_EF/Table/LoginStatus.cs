using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main_Common.Enum.E_ProjectType;

namespace Main_EF.Table
{
    /// <summary>
    /// 登入狀態 (紀錄登入當下的相關資訊)
    /// </summary>
    public class LoginStatus
    {
        #region == 欄位 ===============================================================================
        /// <summary>
        /// 主Key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "主Key")]
        public Guid Id { get; set; }
        /// <summary>
        /// 使用者Id [0：視為程式內部登入]
        /// </summary>
        [Required]
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
        /// 是否為後門權限
        /// </summary>
        [Display(Name = "是否為後門權限")]
        public bool IsBackDoor { get; set; }
        /// <summary>
        /// 後門種類Id
        /// </summary>
        [Display(Name = "後門種類Id")]
        public E_BackDoorType BackDoorTypeId { get; set; }
        /// <summary>
        /// 後門種類名稱
        /// </summary>
        [Display(Name = "後門種類名稱")]
        public string? BackDoorTypeName { get; set; }
        /// <summary>
        /// 是否需檢查密碼
        /// </summary>
        [Display(Name = "是否需檢查密碼")]
        public bool IsNeedCheckPassword { get; set; }
        /// <summary>
        /// 登入時間
        /// </summary>
        [Required]
        [Display(Name = "登入時間")]
        public DateTime LoginDate { get; set; }
        /// <summary>
        /// 登入保持天數
        /// </summary>
        [Display(Name = "登入保持天數")]
        public int? LoginKeepDay { get; set; }
        /// <summary>
        /// 最後請求時間
        /// </summary>
        [Required]
        [Display(Name = "最後請求時間")]
        public DateTime RequestLastDate { get; set; }
        /// <summary>
        /// 登入來源種類Id
        /// </summary>
        [Display(Name = "登入來源種類Id")]
        public E_LoginFromType LoginFromTypeId { get; set; }
        /// <summary>
        /// 登入來源種類名稱
        /// </summary>
        [Display(Name = "登入來源種類名稱")]
        public string? LoginFromTypeName { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string? Rem { get; set; }
        /// <summary>
        /// 公司Id [0：視為程式內部登入]
        /// </summary>
        [Required]
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
        /// 公司等級Id
        /// </summary>
        [Required]
        [Display(Name = "公司等級Id")]
        public E_CompanyLevel CompanyLevelId { get; set; }
        /// <summary>
        /// 公司等級名稱
        /// </summary>
        [Display(Name = "公司等級名稱")]
        public string? CompanyLevelName { get; set; }
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
        /// 信箱
        /// </summary>
        [Display(Name = "信箱")]
        public string? Mail { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
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
        /// 權限種類名稱
        /// </summary>
        [Display(Name = "權限種類名稱")]
        public string? PermissionTypeName { get; set; }
        /// <summary>
        /// 功能代碼Id_TXTs [Id,Id...]
        /// </summary>
        [Display(Name = "功能代碼Id_TXTs")]
        public string? FunctionId_TXTs { get; set; }
        /// <summary>
        /// 其它欄位
        /// </summary>
        [Display(Name = "其它欄位")]
        public string? Other { get; set; }
        #endregion

        #region == 關聯性 ===============================================================================
        ///// <summary>
        ///// 使用者資訊  [1對多][使用者1---*登入狀態]
        ///// </summary>
        //[ForeignKey("User_Id")]
        //public virtual User User_Info { get; set; }
        #endregion
    }
}
