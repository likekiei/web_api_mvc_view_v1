using Main_Common.Model.ResultApi.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.MO_WorkOrder
{
    /// <summary>
    /// 製令單_表身
    /// </summary>
    public class TfMoWorkOrder_DTO
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
        public string prd_name { get; set;}

        /// <summary>
        /// 倉庫
        /// </summary>
        [Display(Name = "倉庫")]
        public string tf_wh { get; set;}

        /// <summary>
        /// 單位
        /// </summary>
        [Display(Name = "單位")]
        public string tf_umit { get; set;}

        /// <summary>
        /// 應發數
        /// </summary>
        [Display(Name = "應發數")]
        public decimal qty_rsv { get; set;}

        /// <summary>
        /// 單位標準用量
        /// </summary>
        [Display(Name = "單位標準用量")]
        public decimal qty_std { get; set;}

        /// <summary>
        /// 單據追蹤項次
        /// </summary>
        [Display(Name = "單據追蹤項次")]
        public int est_itm { get; set;}

        /// <summary>
        /// 應發數
        /// </summary>
        [Display(Name = "應領量")]
        public decimal qty_rsv_lost { get; set; }


    }
}
