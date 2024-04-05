using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 條碼種類
    /// </summary>
    public enum E_BarcodeType
    {
        /// <summary>
        /// Default
        /// </summary>
        [Description("Default")]
        Default = 0,
        /// <summary>
        /// 一維碼
        /// </summary>
        [Description("一維碼")]
        Barcode = 201001,
        /// <summary>
        /// QR碼
        /// </summary>
        [Description("QR碼")]
        QRcode = 202001,
    }
}
