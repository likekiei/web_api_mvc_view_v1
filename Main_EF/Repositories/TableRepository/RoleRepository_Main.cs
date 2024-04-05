using Main_Common.Enum.E_ProjectType;
using Main_Common.Model.Data.Role;
using Main_Common.Model.Data;
using Main_Common.Model.Tool;
using Main_EF.Interface.ITableRepository;
using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Main_Common.ExtensionMethod;

namespace Main_EF.Repositories.TableRepository
{
    /// <summary>
    /// 【Main】角色 Repository
    /// </summary>
    public class RoleRepository_Main : GenericRepository_Main<Role>, IRoleRepository
    {
        public RoleRepository_Main(DbContext_Main dbContext) : base(dbContext)
        {

        }

        #region == 檢查相關 ==
        /// <summary>
        /// 檢查資料是否存在(ById)
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns>「true，有」「false，無」</returns>
        public bool CheckExist(long? id)
        {
            return this.Any(x => x.Id == id);
        }

        /// <summary>
        /// 檢查是否重複(ByNo)
        /// </summary>
        /// <param name="companyId">公司Key</param>
        /// <param name="id">Key，沒給新增檢查，有給修改檢查</param>
        /// <param name="no">代號</param>
        /// <returns>「true，重複」「false，不重複」</returns>
        public bool CheckRepeat_ByNo(long companyId, long? id, string no)
        {
            var result = true;

            // [T：有值，修改檢查][F：無值，新增檢查]
            if (id.HasValue)
            {
                result = this.GetAlls(x => x.CompanyId == companyId && x.No == no && x.Id != id).Any();
            }
            else
            {
                result = this.GetAlls(x => x.CompanyId == companyId && x.No == no).Any();
            }

            return result;
        }
        #endregion

        #region == 取資料相關 ==
        /// <summary>
        /// 【Queryable】【ref】過濾出角色的Queryable
        /// </summary>
        /// <param name="dbData">ref 角色的Queryable</param>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public void WhereQueryable(ref IQueryable<Role> dbData, Role_Filter input)
        {
            // 公司
            if (input.CompanyId.HasValue)
            {
                dbData = dbData.Where(x => x.CompanyId == input.CompanyId);
            }
            else
            {
                // [是否允許查詢全部][T：允許][F：不允許]
                if (input.IsAllowQueryAllCompany)
                {
                    // 不處理，接受查詢全部
                }
                else
                {
                    dbData = dbData.Take(0);
                }
            }

            // Id
            if (input.Id.HasValue)
            {
                dbData = dbData.Where(x => x.Id == input.Id);
            }

            // 關鍵字
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                dbData = dbData.Where(x => x.No.Contains(input.Keyword) || x.Name.Contains(input.Keyword));
            }

            // 代號
            if (!string.IsNullOrEmpty(input.No))
            {
                dbData = dbData.Where(x => x.No == input.No);
            }

            // 名稱
            if (!string.IsNullOrEmpty(input.Name))
            {
                dbData = dbData.Where(x => x.Name == input.Name);
            }

            // 是否啟用。
            if (input.IsEnable.HasValue)
            {
                // [T：啟用][F：關閉]
                if (input.IsEnable.Value)
                {
                    dbData = dbData.Where(x => x.IsStop == false);
                }
                else
                {
                    dbData = dbData.Where(x => x.IsStop == true);
                }
            }
        }

        /// <summary>
        /// 【資料】取角色DTO
        /// </summary>
        /// <param name="input">查詢條件</param>
        /// <returns></returns>
        public Role_DTO? GetDTO(Role_Filter input)
        {
            // 語法命令
            var dbData = this.GetAll();
            // 過濾命令(ref)
            this.WhereQueryable(ref dbData, input);
            // 取出資料
            var result = dbData.Select(x => new Role_DTO
            {
                Id = x.Id,
                No = x.No,
                Name = x.Name,
                CompanyId = x.CompanyId,
                CompanyName = x.CompanyInfo.Name,
            }).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// 【多筆】【可分頁】取得角色下拉清單
        /// </summary>
        /// <param name="input">Id為[預選項目]的判斷</param>
        /// <param name="pageingDTO">ref 分頁資料</param>
        /// <param name="defaultItemText">是否提供預設的Null項目(在第一筆)[沒給不產生][有給產生相同Text的項目]</param>
        /// <returns></returns>
        public List<SelectItemDTO> GetDropList(Role_Filter input, ref Pageing_DTO pageingDTO, string defaultItemText)
        {
            var result = new List<SelectItemDTO>();

            // 語法命令
            var dbData = this.GetAll();
            // 過濾命令(ref)
            this.WhereQueryable(ref dbData, input);

            #region == 分頁處理 ==
            if (pageingDTO == null || pageingDTO.IsEnable == false) // 不分頁
            {
                pageingDTO.TotalCount = dbData.Count();
            }
            else // 分頁處理
            {
                input.IdSelected = null; // 避免要分頁的時候處理到(補已選資料)
                pageingDTO.TotalCount = dbData.Count();
                dbData = dbData.OrderBy(o => o.No).Skip((pageingDTO.PageNumber - 1) * pageingDTO.PageSize).Take(pageingDTO.PageSize);
            }
            #endregion

            #region == 整理資料 ==
            result = dbData.Select(x => new SelectItemDTO
            {
                text = x.No + "---" + x.Name,
                id = x.Id.ToString(),
                value = x.Id.ToString(),
                //Selected = x.Id == input.IdSelected ? true : false, // 有需要在用吧
            }).ToList();

            #region == 補已選資料 【感覺不用了，先關掉】 ==
            //// 如果是過濾停用的查詢，可能會把已停用的預選項目給過濾掉，為了避免這個狀況，直接統一把預選項目補入清單內(如果有的話)。
            //// [T：有已選值]
            //if (input.IdSelected.HasValue)
            //{
            //    // 是否包含已選項目
            //    check = result.Where(x => x.id == input.IdSelected.Value.ToString()).Any();
            //    // [T：未包含，需額外添加]
            //    if (!check)
            //    {
            //        // 取已選資料
            //        var selectedData = this.GetAlls(x => x.Id == input.IdSelected).Select(x => new SelectItemDTO
            //        {
            //            text = x.No + "---" + x.Name,
            //            id = x.Id.ToString(),
            //            value = x.Id.ToString(),
            //        }).FirstOrDefault();

            //        // 插入最前面
            //        result.Insert(0, selectedData);
            //    }
            //}
            #endregion

            // 加入預設項目
            if (!string.IsNullOrEmpty(defaultItemText))
            {
                result.Insert(0, new SelectItemDTO { text = defaultItemText, value = "", Selected = false });
            }
            #endregion

            return result;
        }
        #endregion

        #region == 更新資料相關 ==
        // ...
        #endregion

        #region == 其他 ==
        /// <summary>
        /// 【單筆】生成代號 (純流水號編碼)
        /// </summary>
        /// <returns></returns>
        public string GenNo()
        {
            var presentNO = this.GenNos(1).FirstOrDefault();
            return presentNO;
        }

        /// <summary>
        ///【單筆】生成代號 (前綴+年月日+流水號編碼)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GenNo(DateTime date)
        {
            var presentNO = this.GenNos(date, 1).FirstOrDefault();
            return presentNO;
        }

        /// <summary>
        /// 【多筆】生成代號 (流水號編碼，無其他規則)
        /// </summary>
        /// <param name="count">要取幾組代號</param>
        /// <returns></returns>
        public List<string> GenNos(int count)
        {
            #region == 參數 ==
            var result = new List<string>();
            string genNo; // 生成的代號
            int sn = 0; // 流水號
            string snFormat = "0000"; // 流水號的格式
            //var numberPrefix = E_NumberPrefix.角色.GetEnumDescription();
            //var query = snFormat; // 查詢值
            var queryLength = snFormat.Length; // 查詢值長度(流水號長度)
            var tryInt = false; // 轉型結果
            var check = false;
            #endregion

            #region == 取下一個最大的流水號 ==
            // 任意代號符合指定碼數，且是數字組成
            check = this.Any(x => x.No.Length == queryLength && EF.Functions.IsNumeric(x.No));
            //check = this.GetAlls(x => x.No.Length == queryLength && EF.Functions.IsNumeric(x.No)).Any();
            // [T：符合，取其最大流水號+1][不符合，固定流水號1]
            if (check)
            {
                // 嘗試轉型
                tryInt = int.TryParse(this.GetAlls(x => x.No.Length == queryLength && EF.Functions.IsNumeric(x.No)).OrderByDescending(o => o.No).FirstOrDefault().No, out sn);
                // 不論結果直接+1 (反正轉失敗會得到0)
                sn++;
            }
            else
            {
                sn = 1;
            }
            #endregion

            #region  == 生成代號 ==
            // 依需求數遞加流水號
            for (int i = 0; i < count; i++)
            {
                sn += i == 0 ? 0 : 1; // 遞加i(第一圈不改變，故從0開始)
                genNo = sn.ToString(snFormat); // 生成代號
                result.Add(genNo);
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 【多筆】生成代號 (前綴+年月日+流水號編碼)
        /// </summary>
        /// <param name="date"></param>
        /// <param name="count">要生成幾組代號</param>
        /// <returns></returns>
        public List<string> GenNos(DateTime date, int count)
        {
            #region == 參數 ==
            var result = new List<string>();
            int yyyy = date.Year;
            int MM = date.Month;
            int dd = date.Day;
            string genNo; // 生成的代號
            int sn; // 流水號
            string snFormat = "00000"; // 流水號的格式
            var numberPrefix = E_NumberPrefix.角色.EM_GetEnumDescription(); // 代號前綴
            var query = numberPrefix + "_" + yyyy.ToString("0000") + MM.ToString("00") + dd.ToString("00"); // 查詢值
            var queryLength = query.Length; // 查詢值長度
            var check = false;
            #endregion

            #region == 取下一個最大的流水號 ==
            // 是否存在相同開頭的單號
            check = this.Any(x => x.No.StartsWith(query));
            //check = this.GetAlls(x => x.No.StartsWith(query)).Any();
            // [T：存在，取其最大流水號+1][F：不存在，固定流水號1]
            if (check)
            {
                sn = int.Parse(this.GetAlls(x => x.No.StartsWith(query)).OrderByDescending(o => o.No).FirstOrDefault().No.Substring(queryLength)) + 1;
            }
            else
            {
                sn = 1;
            }
            #endregion

            #region == 生成代號 ==
            // 依需求數遞加流水號
            for (int i = 0; i < count; i++)
            {
                sn += i == 0 ? 0 : 1; // 遞加i(第一圈不改變，故從0開始)
                genNo = query + sn.ToString(snFormat); // 生成代號
                result.Add(genNo);
            }
            #endregion

            return result;
        }
        #endregion
    }
}
