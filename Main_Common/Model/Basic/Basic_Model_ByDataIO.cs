using Main_Common.Enum.E_ProjectType;
using Main_Common.Enum.E_StatusType;
using Main_Common.Tool.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Basic
{
    /// <summary>
    /// 【Model】基本屬性(資料輸出輸入)
    /// </summary>
    /// <remarks>盡量別改動</remarks>
    public class Basic_Model_ByDataIO
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
        /// 增刪修註記
        /// </summary>
        [MyRequired()]
        [Display(Name = "增刪修註記")]
        public E_CRUD CRUD { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        [MyRequired()]
        [Display(Name = "公司Id")]
        public long? CompanyId { get; set; }
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
        /// 外部綁定Key
        /// </summary>
        [Display(Name = "外部綁定Key")]
        public string? OutsideBindKey { get; set; }
        /// <summary>
        /// 檔案綁定用Guid
        /// </summary>
        [Display(Name = "檔案綁定用Guid")]
        public Guid? FileBindGuid { get; set; }
        /// <summary>
        /// 是否停用 [T：停用][F：啟用]
        /// </summary>
        [Display(Name = "是否停用")]
        public bool IsStop { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        /// <summary>
        /// 綁定Key (用來將本次執行相關的資料串起來)
        /// </summary>
        [Display(Name = "綁定Key")]
        public Guid BindKey { get; set; }
        /// <summary>
        /// 是否繼續執行
        /// </summary>
        [Display(Name = "是否繼續執行")]
        public bool IsContinueEXE { get; set; }
        /// <summary>
        /// 是否外部資料處理
        /// </summary>
        [Display(Name = "是否處理外部資料")]
        public bool IsProcessingExternalData { get; set; }
        /// <summary>
        /// 建立人Id
        /// </summary>
        [Display(Name = "建立人Id")]
        public long? CreateManId { get; set; }
        /// <summary>
        /// 建立人名稱
        /// </summary>
        [Display(Name = "建立人名稱")]
        public string? CreateManName { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        [Display(Name = "修改人Id")]
        public long? UpdateManId { get; set; }
        /// <summary>
        /// 建立人名稱
        /// </summary>
        [Display(Name = "修改人名稱")]
        public string? UpdateManName { get; set; }
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public Basic_Model_ByDataIO()
        {
            this.CRUD = E_CRUD.R;
            this.IsStop = false;
            this.IsContinueEXE = true;
            this.IsProcessingExternalData = false;
        }
        #endregion
    }
}
