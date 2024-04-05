using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main_Web.Helper
{
    /// <summary>
    /// 自訂HtmlHelper
    /// </summary>
    public static class MyHtmlHelper
    {
        /// <summary>
        /// Html註解文字
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="text">文字</param>
        /// <returns></returns>
        public static IHtmlContent MyHtmlAnnotation(this IHtmlHelper htmlHelper, string text)
        {
            IHtmlContent result = htmlHelper.Raw($"<!-- {text} -->");
            return result;
        }

        /// <summary>
        /// 文字換行
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="text">文字</param>
        /// <param name="isNeedEncode">是否需要先編碼</param>
        /// <returns></returns>
        public static IHtmlContent StringNewLine(this IHtmlHelper htmlHelper, string text, bool isNeedEncode)
        {
            IHtmlContent result = null;

            // [是否要編碼][T：要][F：不用]
            if (isNeedEncode)
            {
                result = string.IsNullOrEmpty(text) ? null : htmlHelper.Raw(htmlHelper.Encode(text).Replace(Environment.NewLine, "<br/>"));
            }
            else
            {
                result = string.IsNullOrEmpty(text) ? null : htmlHelper.Raw(text.Replace(Environment.NewLine, "<br/>"));
            }

            return result;
        }
    }
}
