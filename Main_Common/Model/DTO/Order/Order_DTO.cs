using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.DTO.Order
{
    public class Order_DTO
    {
        public int Id { get; set; }

        [Display(Name = "業務身份證字號")]
        public string SalespersonIdNumber { get; set; }

        [Display(Name = "訂單編號")]
        public string OrderNumber { get; set; }

        [Display(Name = "據點代碼")]
        public string ServicePointNumber { get; set; }

        [Display(Name = "接單據點")]
        public string ServicePointName { get; set; }

        [Display(Name = "訂單日期")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "修改日期")]
        public DateTime? OrderAmountResetDateTime { get; set; }

        [Display(Name = "轉單日期")]
        public DateTime? TransferDate { get; set; }

        [Display(Name = "取消日期")]
        public DateTime? CancelDateTime { get; set; }

        [Display(Name = "退訂金額")]
        public int ReturnAmount { get; set; }

        [Display(Name = "匯款資訊")]
        public string RemittanceInfor { get; set; }

        [Display(Name = "版次")]
        public int Version { get; set; }

        [Display(Name = "總訂單金額")]
        public int TotalOrderAmount
        {
            get
            {
                return MovingAmount + PackingAmount + UnpackingAmount +
                       WarehousingAmount + CleaningAmount + PackageboxRentalAmount;
            }
        }

        [Display(Name = "搬家費用")]
        public int MovingAmount { get; set; }

        [Display(Name = "裝箱費用")]
        public int PackingAmount { get; set; }

        [Display(Name = "拆箱費用")]
        public int UnpackingAmount { get; set; }

        [Display(Name = "倉儲費用")]
        public int WarehousingAmount { get; set; }

        [Display(Name = "清潔費用")]
        public int CleaningAmount { get; set; }

        [Display(Name = "租借費用")]
        public int PackageboxRentalAmount { get; set; }

        [Display(Name = "月結客戶")]
        public bool IsOpenAccount { get; set; }

        [Display(Name = "公司名稱")]
        public string EnterpriseName { get; set; }

        [Display(Name = "統一編號")]
        public string TaxIdNumber { get; set; }

        [Display(Name = "發票地址")]
        public string InvoceAddress { get; set; }

        [Display(Name = "顧客姓名")]
        public string CustomerName { get; set; }

        [Display(Name = "聯絡電話")]
        public string ContactPhoneNo { get; set; }

        [Display(Name = "遷出地址")]
        public string DepartureAddress { get; set; }

        [Display(Name = "遷入地址")]
        public string DestinationAddress { get; set; }
    }
}
