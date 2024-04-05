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
    ///  簡易輸入 (通常是沒有特別需要傳入資料的時候用，方便Log執行)
    /// </summary>
    public class SimpleInput
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        [Display(Name = "公司Id")]
        public long? CompanyId { get; set; }
        /// <summary>
        /// 主Key
        /// </summary>
        [Display(Name = "主Key")]
        public long? MainKey { get; set; }
        /// <summary>
        /// 綁定Key (用來將本次執行相關的資料串起來)
        /// </summary>
        [Display(Name = "綁定Key")]
        public Guid BindKey { get; set; }
    }
}
