using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 檔案副檔名種類
    /// </summary>
    public enum E_FileExtensionName_Type
    {
        Default = 0,

        [Description(".txt")]
        txt = 10001,

        [Description(".png")]
        png = 200001,
        [Description(".jpg")]
        jpg = 200002,
        [Description(".jpeg")]
        jpeg = 200003,

        [Description(".pdf")]
        pdf = 300001,
        [Description(".docx")]
        docx = 300002,
        [Description(".xlsx")]
        xlsx = 300003,
        [Description(".pptx")]
        pptx = 300004,
    }
}
