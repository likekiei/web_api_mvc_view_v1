using Main_Common.Model.Data.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Common.GlobalSetting
{
    /// <summary>
    /// 【Global】全域開關
    /// </summary>
    public static class GlobalSwitch
    {
        /// <summary>
        /// 正式or測試 [T：正式][F：測試]
        /// </summary>
        public const bool IsFormal = true;
    }

    /// <summary>
    /// 【Global】全域參數
    /// </summary>
    public static class GlobalParameter
    {
        #region == 參數-帳密 ==
        /// <summary>
        /// 預設帳號
        /// </summary>
        [Display(Name = "預設帳號")]
        public static readonly string? Account;
        /// <summary>
        /// 預設密碼
        /// </summary>
        [Display(Name = "預設密碼")]
        public static readonly string? Password;

        /// <summary>
        /// 預設帳號(開發用)
        /// </summary>
        [Display(Name = "預設帳號(開發用)")]
        public static readonly string? Account_RD;
        /// <summary>
        /// 預設密碼(開發用)
        /// </summary>
        [Display(Name = "預設密碼(開發用)")]
        public static readonly string? Password_RD;

        /// <summary>
        /// 預設帳號(開發用)
        /// </summary>
        [Display(Name = "預設帳號(天崗)")]
        public static readonly string? Account_TK;
        /// <summary>
        /// 預設密碼(開發用)
        /// </summary>
        [Display(Name = "預設密碼(天崗)")]
        public static readonly string? Password_TK;
        #endregion

        #region == 參數-Cookie登入驗證相關 ==
        /// <summary>
        /// 驗證的Cookie名稱
        /// </summary>
        [Display(Name = "驗證的Cookie名稱")]
        public static readonly string? AuthCookieName;
        /// <summary>
        /// 驗證的Cookie存活時間
        /// </summary>
        [Display(Name = "驗證的Cookie存活時間")]
        public static readonly int AuthCookieKeepDay;
        /// <summary>
        /// 驗證的登入路徑
        /// </summary>
        [Display(Name = "驗證的登入路徑")]
        public static readonly string? AuthLoginPath;
        #endregion

        #region == 參數-語系相關 ==
        /// <summary>
        /// 語系的Cookie名稱
        /// </summary>
        [Display(Name = "語系的Cookie名稱")]
        public static readonly string? LanguageCookieName;
        #endregion

        #region == Url ==
        /// <summary>
        /// 簡易Log頁面超連結 (提供給Alert用)
        /// </summary>
        [Display(Name = "簡易Log頁面超連結")]
        public static readonly string? Url_LogSimpleActionView;
        #endregion

        #region == 分頁 ==
        /// <summary>
        /// 預設筆數(清單用)
        /// </summary>
        [Display(Name = "預設筆數(清單用)")]
        public static readonly int PageSize_ByList;
        /// <summary>
        /// 預設筆數(下拉選單用)
        /// </summary>
        [Display(Name = "預設筆數(下拉選單用)")]
        public static readonly int PageSize_ByDropList;
        #endregion

        #region == 建構值 ==
        /// <summary>
        /// 建構值
        /// </summary>
        static GlobalParameter()
        {
            #region == 建構值-統一設定 ==
            Account = "magic";
            Password = "0000";

            Account_RD = "AdminRD";
            Password_RD = "@Attn3100";

            Account_TK = "AdminTK";
            Password_TK = "@Attn3100";

            AuthCookieKeepDay = 30;
            AuthLoginPath = "/Account/Login";

            PageSize_ByList = 10;
            PageSize_ByDropList = 30;
            #endregion

            #region == 建構值-區分正式or測試 ==
            // [正式 or 測試][T：正式][F：測試]
            if (GlobalSwitch.IsFormal)
            {
                AuthCookieName = "RD_WorkManageSystem_Web_Auth";
                LanguageCookieName = "RD_WorkManageSystem_Web_Language";
            }
            else
            {
                AuthCookieName = "RD_WorkManageSystem_Web_Auth_ByTest";
                LanguageCookieName = "RD_WorkManageSystem_Web_Language_ByTest";
            }
            #endregion
        }
        #endregion
    }
}
