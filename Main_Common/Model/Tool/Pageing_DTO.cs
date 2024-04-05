using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Tool
{
    /// <summary>
    /// 【DTO】分頁所需欄位
    /// </summary>
    public class Pageing_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 頁數
        /// </summary>
        [Display(Name = "頁數")]
        public int PageNumber { get; set; }
        /// <summary>
        /// 每頁筆數
        /// </summary>
        [Display(Name = "每頁筆數")]
        public int PageSize { get; set; }
        /// <summary>
        /// 總筆數
        /// </summary>
        [Display(Name = "總筆數")]
        public int TotalCount { get; set; }
        /// <summary>
        /// 是否啟用分頁查詢
        /// </summary>
        [Display(Name = "是否啟用分頁查詢")]
        public bool IsEnable { get; set; }
        #endregion
    }
}
