using Main_Common.Enum;
using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Handle.View
{
    /// <summary>
    /// 頁面通用DTO
    /// </summary>
    public class View_DTO
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [Display(Name = "公司ID")]
        public long Company_ID { get; set; }
        /// <summary>
        /// 公司等級
        /// </summary>
        [Display(Name = "公司等級")]
        public E_CompanyLevel Company_Level_ID { get; set; }
        /// <summary>
        /// 角色種類
        /// </summary>
        [Display(Name = "角色種類")]
        public E_Role Role_Type_ID { get; set; }
        ///// <summary>
        ///// 報表種類
        ///// </summary>
        //[Display(Name = "報表種類")]
        //public E_Report? E_Report_ID { get; set; }
        /// <summary>
        /// 前台or後台 [T：後台][F：前台]
        /// </summary>
        public bool Is_AdminView { get; set; }
        /// <summary>
        /// 是否為單筆 [T：單筆][F：多筆]
        /// </summary>
        public bool Is_Single { get; set; }
    }
}
