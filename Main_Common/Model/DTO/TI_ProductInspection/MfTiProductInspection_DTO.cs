using Main_Common.Model.DTO.MO_WorkOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.TI_ProductInspection
{
    /// <summary>
    /// 製成品送檢單_表頭
    /// </summary>
    public class MfTiProductInspection_DTO
    {
        /// <summary>
        /// 入庫日期
        /// </summary>
        [Display(Name = "入庫日期")]
        [DataType(DataType.Date)]
        public DateTime ti_dd { get; set; }

        /// <summary>
        /// 客戶廠商(代號)
        /// </summary>
        [Display(Name = "客戶廠商")]
        public string cus_no { get; set; }

        /// <summary>
        /// 轉入單號(製令單號)
        /// </summary>
        [Display(Name = "轉入單號")]
        public string os_no { get; set; }

        /// <summary>
        /// 批號
        /// </summary>
        [Display(Name = "批號")]
        public string bat_no { get; set; }

        /// <summary>
        /// 經辦人員ID(ERP員工代號)
        /// </summary>
        [Display(Name = "經辦人員")]
        public string sal_no { get; set; }


        /// <summary>
        /// 部門(代號)
        /// </summary>
        [Display(Name = "部門")]
        public string dep { get; set; }

        /// <summary>
        /// 客戶訂單
        /// </summary>
        [Display(Name = "客戶訂單")]
        public string cus_os_no { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string rem { get; set; }

        /// <summary>
        /// 日威送檢單號 單號編碼規則為 T6+2碼民國年+2碼月+2碼日+流水號4碼 
        /// </summary>
        [Display(Name = "日威送檢單號")]
        public string bbnum { get; set; }

        /// <summary>
        /// 表身項目 [要給]
        /// </summary>
        [Display(Name = "表身項目")]
        public List<TfTiProductInspection_DTO> TFs { get; set; }


        /// <summary>
        /// 【建構】
        /// </summary>
        public MfTiProductInspection_DTO()
        {
            this.TFs = new List<TfTiProductInspection_DTO>();
        }

    }
}
