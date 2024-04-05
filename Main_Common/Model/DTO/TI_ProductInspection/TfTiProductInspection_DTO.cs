using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.TI_ProductInspection
{
    /// <summary>
    /// 製成品送檢單_表身
    /// </summary>
    public class TfTiProductInspection_DTO
    {
        /// <summary>
        /// 品號
        /// </summary>
        [Display(Name = "品號")]
        public string prd_no { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [Display(Name = "品名")]
        public string prd_name { get; set; }

        /// <summary>
        /// 倉庫
        /// </summary>
        [Display(Name = "倉庫")]
        public string wh { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        [Display(Name = "單位")]
        public string unit { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [Display(Name = "數量")]
        public decimal qty { get; set; }

        /// <summary>
        /// 配方號
        /// </summary>
        //public string id_no { get; set; }

        /// <summary>
        /// 備註二(自定義欄位)
        /// </summary>
        [Display(Name = "備註二")]
        public string rrr2 { get; set; }

    }
}
