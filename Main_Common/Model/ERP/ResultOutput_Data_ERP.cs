using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.ERP
{
    public class ResultOutput_Data_ERP<T>
    {
        /// <summary>
        /// 結果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 相關訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 建構
        /// </summary>
        public ResultOutput_Data_ERP()
        {
            //
        }

        /// <summary>
        /// 建構
        /// </summary>
        public ResultOutput_Data_ERP(bool isSucccess, string message, T data)
        {
            this.Result = isSucccess;
            this.Message = message;
            this.Data = data;
        }
    }
}
