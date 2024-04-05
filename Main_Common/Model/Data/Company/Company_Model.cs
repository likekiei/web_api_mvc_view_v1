using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Model.Basic;
using Main_Common.Tool.CustomAttribute;
using Main_Resources.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Data.Company
{
    /// <summary>
    /// 【Model】公司
    /// </summary>
    public class Company_Model : Basic_Model_ByDataIO
    {
        #region == private的屬性 ===============================================================================
        ///// <summary>
        ///// 視力
        ///// </summary>
        //[Display(Name = "視力")]
        //private decimal? _Eyesight { get; set; }
        //private decimal? _AA { get; set; }
        #endregion

        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 主Key
        /// </summary>
        [Display(Name = "主Key")]
        public long? Id { get; set; }
        /// <summary>
        /// 代號 (視同帳號)
        /// </summary>
        [MyMaxLength(128)]
        [MyRequired]
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [MyMaxLength(128)]
        [MyRequired]
        [Display(Name = "名稱")]
        public string? Name { get; set; }
        /// <summary>
        /// 公司分級Id
        /// </summary>
        [MyRequired]
        [Display(Name = "公司分級Id")]
        public E_CompanyLevel CompanyLevelId { get; set; }
        /// <summary>
        /// 統編
        /// </summary>
        [Display(Name = "統編")]
        public string? UnifiedNumber { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        [Display(Name = "電話")]
        public string? Tel { get; set; }
        /// <summary>
        /// 傳真
        /// </summary>
        [Display(Name = "傳真")]
        public string? Fax { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Display(Name = "公司地址")]
        public string? Address1 { get; set; }
        /// <summary>
        /// 通訊地址
        /// </summary>
        [Display(Name = "通訊地址")]
        public string? Address2 { get; set; }
        /// <summary>
        /// 信箱
        /// </summary>
        [EmailAddress(ErrorMessageResourceType = typeof(RES_ModelValidationMessage), ErrorMessageResourceName = nameof(RES_ModelValidationMessage.EmailAddress))]
        [Display(Name = "信箱")]
        public string? EMail { get; set; }
        /// <summary>
        /// 負責人
        /// </summary>
        [Display(Name = "負責人")]
        public string? ResponsibMan { get; set; }
        /// <summary>
        /// 聯絡人
        /// </summary>
        [Display(Name = "聯絡人")]
        public string? ContactMan { get; set; }
        /// <summary>
        /// 聯絡人職稱
        /// </summary>
        [Display(Name = "聯絡人職稱")]
        public string? ContactManJobName { get; set; }
        /// <summary>
        /// 聯絡人手機
        /// </summary>
        [Display(Name = "聯絡人手機")]
        public string? ContactManPhone { get; set; }
        /// <summary>
        /// 是否為預設選取項目
        /// </summary>
        [Display(Name = "是否為預設選取項目")]
        public bool IsDefaultSelected { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string? Rem { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        // ...
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Company_Model()
        {
            // ...
        }
        #endregion
    }
}
