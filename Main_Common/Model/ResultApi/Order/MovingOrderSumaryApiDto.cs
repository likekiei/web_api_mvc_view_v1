using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ResultApi.Order
{
    public class MovingOrderSumaryApiDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 業務身份證字號
        /// </summary>
        [Display(Name = "業務身份證字號")]
        public string SalespersonIdNumber { get; set; }
        /// <summary>
        /// 訂單編號
        /// </summary>
        [Display(Name = "訂單編號")]
        public string OrderNumber { get; set; }
        /// <summary>
        /// 據點代碼
        /// </summary>
        [Display(Name = "據點代碼")]
        public string ServicePointNumber { get; set; }
        /// <summary>
        /// 接單據點
        /// </summary>
        [Display(Name = "接單據點")]
        public string ServicePointName { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        [Display(Name = "訂單日期")]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Display(Name = "修改日期")]
        public DateTime? OrderAmountResetDateTime { get; set; }
        /// <summary>
        /// 轉單日期
        /// </summary>
        [Display(Name = "轉單日期")]
        public DateTime? TransferDate { get; set; }
        /// <summary>
        /// 取消日期
        /// </summary>
        [Display(Name = "取消日期")]
        public DateTime? CancelDateTime { get; set; }
        /// <summary>
        /// 退訂金額
        /// </summary>
        [Display(Name = "退訂金額")]
        public int ReturnAmount { get; set; }
        /// <summary>
        /// 匯款資訊
        /// </summary>
        [Display(Name = "匯款資訊")]
        public string RemittanceInfor { get; set; }
        /// <summary>
        /// 版次
        /// </summary>
        [Display(Name = "版次")]
        public int Version { get; set; }
        /// <summary>
        /// 總訂單金額
        /// </summary>
        [Display(Name = "總訂單金額")]
        public int TotalOrderAmount
        {
            get
            {
                return MovingAmount + PackingAmount + UnpackingAmount +
                       WarehousingAmount + CleaningAmount + PackageboxRentalAmount;
            }
        }
        /// <summary>
        /// 搬家費用
        /// </summary>
        [Display(Name = "搬家費用")]
        public int MovingAmount { get; set; }
        /// <summary>
        /// 裝箱費用
        /// </summary>
        [Display(Name = "裝箱費用")]
        public int PackingAmount { get; set; }
        /// <summary>
        /// 拆箱費用
        /// </summary>
        [Display(Name = "拆箱費用")]
        public int UnpackingAmount { get; set; }
        /// <summary>
        /// 倉儲費用
        /// </summary>
        [Display(Name = "倉儲費用")]
        public int WarehousingAmount { get; set; }
        /// <summary>
        /// 清潔費用
        /// </summary>
        [Display(Name = "清潔費用")]
        public int CleaningAmount { get; set; }
        /// <summary>
        /// 租借費用
        /// </summary>
        [Display(Name = "租借費用")]
        public int PackageboxRentalAmount { get; set; }
        /// <summary>
        /// 月結客戶
        /// </summary>
        [Display(Name = "月結客戶")]
        public bool IsOpenAccount { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Display(Name = "公司名稱")]
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 統一編號
        /// </summary>
        [Display(Name = "統一編號")]
        public string TaxIdNumber { get; set; }
        /// <summary>
        /// 發票地址
        /// </summary>
        [Display(Name = "發票地址")]
        public string InvoceAddress { get; set; }
        /// <summary>
        /// 顧客姓名
        /// </summary>
        [Display(Name = "顧客姓名")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 聯絡電話
        /// </summary>
        [Display(Name = "聯絡電話")]
        public string ContactPhoneNo { get; set; }
        /// <summary>
        /// 遷出地址
        /// </summary>
        [Display(Name = "遷出地址")]
        public string DepartureAddress { get; set; }
        /// <summary>
        /// 遷入地址
        /// </summary>
        [Display(Name = "遷入地址")]
        public string DestinationAddress { get; set; }
    }
}
