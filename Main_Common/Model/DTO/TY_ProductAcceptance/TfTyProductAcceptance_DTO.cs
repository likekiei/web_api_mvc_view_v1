using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.TY_ProductAcceptance
{
    /// <summary>
    /// 生產繳庫驗收單_表身
    /// </summary>
    public class TfTyProductAcceptance_DTO
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
        /// 倉庫(代號)
        /// </summary>
        [Display(Name = "倉庫")]
        public string wh { get; set; }

        /// <summary>
        /// 單位(代號)
        /// </summary>
        [Display(Name = "單位")]
        public string unit { get; set; }

        /// <summary>
        /// 驗貨數量
        /// </summary>
        [Display(Name = "驗貨數量")]
        public decimal qty_chk { get; set; }

        /// <summary>
        /// 合格數量
        /// </summary>
        [Display(Name = "合格數量")]
        public decimal qty_ok { get; set; }

        /// <summary>
        /// 不合格量
        /// </summary>
        [Display(Name = "不合格量")]
        public decimal qty_lost { get; set; }

        /// <summary>
        /// 原因(代號)
        /// </summary>
        [Display(Name = "原因(代號)")] //table name:SPC_LST
        public string spc_no { get; set; }

        /// <summary>
        /// 原因(不合格描述)
        /// </summary>
        [Display(Name = "原因(不合格描述)")]
        public string spc_name { get; set; }

        /// <summary>
        /// 製令單號
        /// </summary>
        [Display(Name = "製令單號")]
        public string mo_no { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [Display(Name = "摘要")]
        public string rem { get; set; }

    }
}
