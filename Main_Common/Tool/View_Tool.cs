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
    /// View處理相關工具
    /// </summary>
    public static class View_Tool
    {
        #region == 分頁相關處理 ==
        /// <summary>
        /// 檢查當前頁數是否超過總頁數   
        /// </summary>
        /// <param name="input">分頁資訊</param>
        /// <remarks>[treu:表示超過][超過時回傳總頁數]</remarks>
        /// <returns></returns>
        public static ResultSimpleData<int> Check_NowPageOverTotalPage(Pageing_DTO input)
        {
            var result = new ResultSimpleData<int>();
            // 取得總頁數  [總數/數量(無條件進位)]
            var totalPage = int.Parse((Math.Ceiling((decimal)input.TotalCount / (decimal)input.PageSize)).ToString());
            // 判斷，總頁數 < 當前頁數
            result.IsSuccess = input.PageNumber > totalPage ? true : false;
            // [T：超過總頁數，才回寫總頁數]
            if (result.IsSuccess)
            {
                result.Data = totalPage;
            }

            return result;
        }
        #endregion
    }
}
