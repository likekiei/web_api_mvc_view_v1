using Main_Common.Enum;
using Main_Common.Enum.E_MothodType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Handle
{
    /// <summary>
    /// 檢查值
    /// </summary>
    public class ValueCheck_DTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        ///// <summary>
        ///// 型別名稱
        ///// </summary>
        //public string TypeName { get; set; }
        ///// <summary>
        ///// 型別名稱
        ///// </summary>
        //public Type Type { get; set; }
        /// <summary>
        /// 型別列舉
        /// </summary>
        public E_TypeCode_MM TypeCode_MM { get; set; }
        /// <summary>
        /// 是否允許Null [預設T]
        /// </summary>
        public bool AllowNull { get; set; }
        /// <summary>
        /// 字串最大值
        /// </summary>
        public int StringMax { get; set; }
        /// <summary>
        /// 字串最小值
        /// </summary>
        public int StringMin { get; set; }
        /// <summary>
        /// 檢查方式清單 (排序會影響檢查順序)
        /// </summary>
        public List<E_CheckMothod> CheckMothod_List { get; set; } 
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public ValueCheck_DTO()
        {
            this.AllowNull = true;
            this.CheckMothod_List = new List<E_CheckMothod>();
        }
        #endregion
    }
}
