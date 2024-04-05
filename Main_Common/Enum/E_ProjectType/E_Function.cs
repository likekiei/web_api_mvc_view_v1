using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Enum.E_ProjectType
{
    /// <summary>
    /// 權限/功能方法
    /// </summary>
    public enum E_Function
    {
        //不能亂改，要跟資料庫對照
        //先粗略區分，未來再考慮要不要細分

        [Description("公司")]
        公司 = 200001,
        [Description("角色")]
        角色 = 201001,
        [Description("部門")]
        部門 = 202001,
        [Description("使用者")]
        使用者 = 203001,

        [Description("報表")]
        報表 = 300001,

        [Description("其它")]
        其它 = 900000,
        [Description("初始化設定")]
        初始化設定 = 900001,
        [Description("後台維護")]
        後台維護 = 901001,
        [Description("連線設定")]
        連線設定 = 902001,
        [Description("外部連線設定")]
        外部連線設定 = 903001,
    }
}
