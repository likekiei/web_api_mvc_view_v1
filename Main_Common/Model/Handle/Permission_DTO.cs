using Main_Common.Enum;
using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Handle
{
    /// <summary>
    /// 【DTO】權限參數
    /// </summary>
    public class Permission_DTO
    {
        /// <summary>
        /// 是否為後門權限
        /// </summary>
        [Display(Name = "是否為後門權限")]
        public bool Is_BackDoor { get; set; }
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
        /// <summary>
        /// 前台or後台 [T：後台][F：前台]
        /// </summary>
        public bool Is_AdminView { get; set; }
    }
}
