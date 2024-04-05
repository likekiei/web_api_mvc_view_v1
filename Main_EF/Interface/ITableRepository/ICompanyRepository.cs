using Main_Common.Model.Data;
using Main_Common.Model.Data.Company;
using Main_Common.Model.Result;
using Main_Common.Model.Tool;
using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Interface.ITableRepository
{
    /// <summary>
    /// 【Main】【interface】公司 Repository
    /// </summary>
    /// <remarks></remarks>
    public interface ICompanyRepository : IGenericRepository<Companys>
    {
        // 如非通用方法，加在這邊

        #region == 檢查相關 ==
        /// <summary>
        /// 檢查資料是否存在(ById)
        /// </summary>
        /// <param name="key"></param>
        /// <returns>「true，有」「false，無」</returns>
        bool CheckExist(long? Id);

        /// <summary>
        /// 檢查是否重複(ByNo) [true存在]
        /// </summary>
        /// <param name="id">Key，沒給新增檢查，有給修改檢查</param>
        /// <param name="no">代號</param>
        /// <returns>「true，重複」「false，不重複」</returns>
        public bool CheckRepeat_ByNo(long? id, string no);
        #endregion

        #region == 取資料相關 ==
        /// <summary>
        /// 【Queryable】【ref】過濾出公司的Queryable
        /// </summary>
        /// <param name="dbData">ref 公司的Queryable</param>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public void WhereQueryable(ref IQueryable<Companys> dbData, Company_Filter input);

        /// <summary>
        /// 【資料】取公司DTO
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public Company_DTO? GetDTO(Company_Filter input);

        /// <summary>
        /// 【多筆】【可分頁】取得公司下拉清單
        /// </summary>
        /// <param name="input">Id為[預選項目]的判斷</param>
        /// <param name="pageingDTO">ref 分頁資料</param>
        /// <param name="defaultItemText">是否提供預設的Null項目(在第一筆)[沒給不產生][有給產生相同Text的項目]</param>
        /// <returns></returns>
        public List<SelectItemDTO> GetDropList(Company_Filter input, ref Pageing_DTO pageingDTO, string defaultItemText);
        #endregion

        #region == 更新資料相關 ==
        // ...
        #endregion

        #region == 其他 ==
        /// <summary>
        /// 【單筆】生成代號 (純流水號編碼)
        /// </summary>
        /// <returns></returns>
        public string GenNo();

        /// <summary>
        ///【單筆】生成代號 (前綴+年月日+流水號編碼)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GenNo(DateTime date);

        /// <summary>
        /// 【多筆】生成代號 (流水號編碼，無其他規則)
        /// </summary>
        /// <param name="count">要取幾組代號</param>
        /// <returns></returns>
        public List<string> GenNos(int count);

        /// <summary>
        /// 【多筆】生成代號 (前綴+年月日+流水號編碼)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="count">要生成幾組代號</param>
        /// <returns></returns>
        public List<string> GenNos(DateTime date, int count);
        #endregion
    }
}
