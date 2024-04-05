using Main_Common.Enum.E_ProjectType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.Model.Data.Connect
{
    /// <summary>
    /// 【DTO】連線資訊
    /// </summary>
    public class Connect_DTO
    {
        /// <summary>
        /// 主Key
        /// </summary>
        [Display(Name = "主Key")]
        public long Id { get; set; }
        /// <summary>
        /// 代號
        /// </summary>
        [Display(Name = "代號")]
        public string? No { get; set; }
        /// <summary>
        /// 資料庫種類Id
        /// </summary>
        [Display(Name = "資料庫種類Id")]
        public E_DBType DBTypeId { get; set; }
        /// <summary>
        /// 連線IP+Port
        /// </summary>
        [Display(Name = "連線路徑")]
        public string? Path { get; set; }
        /// <summary>
        /// 資料庫名稱
        /// </summary>
        [Display(Name = "資料庫名稱")]
        public string? Catalog { get; set; }
        /// <summary>
        /// SQL帳號
        /// </summary>
        [Display(Name = "SQL帳號")]
        public string? User { get; set; }
        /// <summary>
        /// SQL密碼(須加密)
        /// </summary>
        [Display(Name = "SQL密碼")]
        public string? Password { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string? Rem { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        [Display(Name = "公司Id")]
        public long? CompanyId { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Display(Name = "公司名稱")]
        public string? CompanyName { get; set; }
    }
}
