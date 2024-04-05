using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Tool
{
    /// <summary>
    /// Url處理相關工具
    /// </summary>
    public static class UrlTool
    {
        /// <summary>
        /// 取得首頁超連結
        /// </summary>
        /// <param name="isAdminHome">[T：後台][F：前台]</param>
        /// <returns></returns>
        public static string Get_HomeUrl(bool isAdminHome)
        {
            var url = isAdminHome ? $"/HomeAdmin/Index" : $"/Home/Index";
            return url;
        }

        /// <summary>
        /// 取得簡易Log頁面超連結
        /// </summary>
        /// <param name="actionBindKey">執行綁定Key</param>
        /// <returns></returns>
        public static string Get_LogSimpleUrl(Guid actionBindKey)
        {
            var url = $"/Log/ActionView_LogSimple?actionBindKey={actionBindKey.ToString()}";
            return url;
        }

        /// <summary>
        /// 取得登入頁面超連結
        /// </summary>
        /// <returns></returns>
        public static string Get_LoginUrl()
        {
            var url = $"/Account/Login";
            return url;
        }
    }
}
