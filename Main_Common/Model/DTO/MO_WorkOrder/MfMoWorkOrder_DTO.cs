using AutoMapper.Execution;
using Main_Common.Model.ResultApi.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.MO_WorkOrder

{   /// <summary>
    ///製令單_表頭
    /// </summary>
    public class MfMoWorkOrder_DTO
    {
        /// <summary>
        /// 製令單號
        /// </summary>
        [Display(Name = "製令單號")]
        public string mo_no { get; set; }

        /// <summary>
        /// 製單日期
        /// </summary>
        [Display(Name = "製單日期")]
        [DataType(DataType.Date)]
        public DateTime mo_dd { get; set; }

        /// <summary>
        /// 生產成品
        /// </summary>
        [Display(Name = "生產成品")]
        public string mrp_no { get; set; }

        /// <summary>
        /// 配方
        /// </summary>
        [Display(Name ="配方")]
        public string id_no { get; set;}

        /// <summary>
        /// 製造部門
        /// </summary>
        [Display(Name = "製造部門")]
        public string dep { get; set;}

        /// <summary>
        /// 數量
        /// </summary>
        [Display(Name ="數量")]
        public decimal qty { get; set;}

        /// <summary>
        /// 製造單位
        /// </summary>
        [Display(Name = "製造單位")]
        public string unit { get; set;}

        /// <summary>
        /// 預入倉庫
        /// </summary>
        [Display(Name = "預入倉庫")]
        public string wh { get; set; }

        /// <summary>
        /// 需求客戶
        /// </summary>
        [Display(Name = "需求客戶")]
        public string cus_no { get; set; }

        /// <summary>
        /// 客戶訂單
        /// </summary>
        [Display(Name = "客戶訂單")]
        public string cus_os_no { get; set; }

        /// <summary>
        /// 預開工日
        /// </summary>
        [Display(Name = "預開工日")]
        [DataType(DataType.Date)]
        public DateTime sta_dd { get; set; }

        /// <summary>
        /// 預完工日
        /// </summary>
        [Display(Name = "預完工日")]
        [DataType(DataType.Date)]
        public DateTime end_dd { get; set; }

        /// <summary>
        /// 需求日期
        /// </summary>
        [Display(Name = "需求日期")]
        [DataType(DataType.Date)]
        public DateTime need_dd { get; set; }

        /// <summary>
        /// 規格
        /// </summary>
        [Display(Name = "規格")]
        public string spc { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Display(Name = "備註")]
        public string rem { get; set; }


        //目前生產狀態的3.暫停生產;4.結束生產上不確定是否取同一個ZT_ID欄位值，先不列
        //生產狀態有2個欄位的值
        /// <summary>
        /// 生產狀態/1.未發放F;2.發放生產T
        /// </summary>
        [Display(Name = "生產狀態/1.未發放F;2.發放生產T")]
        public string cf_id { get; set; }


        /// <summary>
        /// 生產狀態/3.暫停生產
        /// </summary>
        [Display(Name = "生產狀態/3.暫停生產")]
        public string zt_id { get; set; }

        /// <summary>
        /// 生產狀態/4.結束生產
        /// </summary>
        [Display(Name = "生產狀態/4.結束生產")]
        public string close_id { get; set; }

        /// <summary>
        /// 倒沖領料
        /// </summary>
        [Display(Name = "倒沖領料")]
        public string ml_by_mm { get; set; }

        /// <summary>
        /// 批號
        /// </summary>
        [Display(Name = "批號")]
        public string bat_no { get; set; }

        /// <summary>
        /// 表身項目 [要給]
        /// </summary>
        [Display(Name = "表身項目")]
        public List<TfMoWorkOrder_DTO> TFs { get; set; }

        /// <summary>
        /// 【建構】
        /// </summary>
        public MfMoWorkOrder_DTO()
        {
            this.TFs = new List<TfMoWorkOrder_DTO>();
        }


    }
}
