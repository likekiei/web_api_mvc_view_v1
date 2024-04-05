using Main_Common.Model.Result;
using Main_Common.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Mothod.Page
{
    /// <summary>
    /// 分頁相關
    /// </summary>
    public class Page_Service
    {
        #region == 時間相關 ==
        /// <summary>
        /// 檢查當前頁數是否超過總頁數   [treu-表示超過] [超過時回傳總頁數]
        /// </summary>
        /// <param name="input">分頁資訊</param>
        /// <returns></returns>
        public ResultOutput_Data<int> CheckNowPageOverTotalPage(PageDTO input)
        {
            var result = new ResultOutput_Data<int>();
            //取得總頁數  [總數/數量(無條件進位)]
            var totalPage = int.Parse((Math.Ceiling((decimal)input.TotalCount / (decimal)input.PageSize)).ToString());
            //總頁數 < 當前頁數，表示超過總頁數
            result.IsSuccess = input.PageNumber > totalPage ? true : false;

            if (result.IsSuccess) //超過總頁數，才回寫總頁數
            {
                result.Data = totalPage;
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 整理分頁資料
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public static ResultOutput_Data<string> g(PageDTO input)
        //{

        //}
    }
}
