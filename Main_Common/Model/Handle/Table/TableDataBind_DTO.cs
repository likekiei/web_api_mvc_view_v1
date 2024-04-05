using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Handle.Table
{
    /// <summary>
    /// 【DTO】Table Data 綁定
    /// </summary>
    public class TableDataBind_DTO
    {
        /// <summary>
        /// Table-名稱
        /// </summary>
        [Display(Name = "Table-名稱")]
        public string Table_Name { get; set; }
        /// <summary>
        /// TableData-Key
        /// </summary>
        [Display(Name = "TableData-Key")]
        public string TableData_Key { get; set; }
    }
}
