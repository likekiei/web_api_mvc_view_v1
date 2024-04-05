using Main_Common.Model.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Account
{
    /// <summary>
    /// 【查詢】登入狀態
    /// </summary>
    public class LoginStatus_Filter : Basic_Filter
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
        public Guid? Id { get; set; }
        /// <summary>
        /// 已選擇Key  (避免已選項目因為過濾被剔除)(僅不分頁的情況能使用)
        /// </summary>
        [Display(Name = "已選擇Key")]
        public long? IdSelected { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        [Display(Name = "名稱")]
        public string? Name { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        // ...
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public LoginStatus_Filter()
        {
            // ...
        }
        #endregion
    }
}
