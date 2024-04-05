using Main_Common.Model.DTO.TI_ProductInspection;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using Microsoft.Extensions.FileSystemGlobbing.Internal;


namespace Main_Common.Model.DTO.TY_ProductAcceptance
{
    /// <summary>
    /// 生產繳庫驗收單_表頭
    /// </summary>
    public class MfTyProductAcceptance_DTO
    {

        /// <summary>
        /// 驗收日期
        /// </summary>
        [Display(Name = "驗收日期")]
        [DataType(DataType.Date)]
        public DateTime ty_dd { get; set; }

        /// <summary>
        /// 日威入庫單號(日威送檢單單號)
        /// </summary>
        [Display(Name = "入庫單號")] //由日威的送檢單號過濾出ERP的真正送檢單號寫入DB MfTy的OS_NO欄位
        public string ti_no { get; set; }


        /// <summary>
        /// 驗收部門(代號)
        /// </summary>
        [Display(Name = "驗收部門")]
        public string dep { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string rem { get; set; }

        /// <summary>
        /// 驗貨人員(驗收人員 代號)
        /// </summary>
        [Display(Name = "驗收人員")]
        public string sal_no { get; set; }

        /// <summary>
        /// 客戶訂單
        /// </summary>
        [Display(Name = "客戶訂單")]
        public string cus_os_no { get; set; }

        /// <summary>
        /// 機台號碼(部門代號)
        /// </summary>
        [Display(Name = "機台號碼")]
        public string pppnum { get; set; }

        /// <summary>
        /// 上機生產時間(紀錄開機生產時間)
        /// </summary>
        [Display(Name = "上機生產時間")]
        public DateTime ddddsta { get; set; }

        /// <summary>
        /// 下機生產時間(紀錄停機生產時間)
        /// </summary>
        [Display(Name = "下機生產時間")]
        //[DataType(DataType.Date)]
        public DateTime ddddend { get; set; }

        /// <summary>
        /// 綠燈總機時間
        /// </summary>
        [Display(Name = "綠燈總機時間")]
        //[DataType(DataType.Date)]
        public DateTime ddddgre { get; set; }

        /// <summary>
        /// 備註二
        /// </summary>
        [Display(Name = "備註二")]
        public string rrr2 { get; set; }

        /// <summary>
        /// 日威送繳庫驗收單單號 單號編碼規則為 TP+2碼民國年+2碼月+2碼日+流水號4碼 
        /// </summary>
        [Display(Name = "日威送繳庫驗收單單號")]
        public string bbnum { get; set; }


        /// <summary>
        /// 表身項目 [要給]
        /// </summary>
        [Display(Name = "表身項目")]
        public List<TfTyProductAcceptance_DTO> TFs { get; set; }


        /// <summary>
        /// 【建構】
        /// </summary>
        public MfTyProductAcceptance_DTO()
        {
            this.TFs = new List<TfTyProductAcceptance_DTO>();
        }




    }
}
