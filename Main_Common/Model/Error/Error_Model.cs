using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Error
{
    /// <summary>
    /// 錯誤訊息
    /// </summary>
    public class Error_Model
    {
        /// <summary>
        /// 返回Url
        /// </summary>
        public string returnUrl { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        public string message { get; set; }
    }
}
