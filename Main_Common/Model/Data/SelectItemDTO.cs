using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Data
{
    /// <summary>
    /// 下拉項目
    /// </summary>
    public class SelectItemDTO
    {
        /// <summary>
        /// 是否已停用
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 是否已選取
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// [Select2]隱藏值
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 隱藏值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 顯示文字
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 允許忽略的項目 (沒有實際意義的項目，EX.空項目)
        /// </summary>
        public bool IsAllowIgnoreItem { get; set; }

        //== Select2用 ==================================================================

        //== 其他 ==================================================================

        /// <summary>
        /// 英文
        /// </summary>
        [Display(Name = "英文")]
        public string TextEN { get; set; }
        /// <summary>
        /// 在出入庫功能是否顯示英文
        /// </summary>
        [Display(Name = "在出入庫功能是否顯示英文")]
        public bool IsShowEN_ByInOutBound { get; set; }
    }
}
