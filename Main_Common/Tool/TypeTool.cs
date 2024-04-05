using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Tool
{
    /// <summary>
    /// 型別處理相關
    /// </summary>
    public class TypeTool
    {
        #region == Type相關 ==
        /// <summary>
        /// 取得型別名稱
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTypeName<T>()
        {
            var result = "";
            var type = typeof(T);
            var indexSN = 1; //索引值
            var TXT_List = new Dictionary<int, string>(); //紀錄值

            #region == 取ClassName [Loop] ==
            // [T：允許空值的型別][F：不允許空值]
            if (type.Name == "Nullable`1")
            {
                TXT_List.Add(indexSN, type.GetGenericArguments()[0].Name + "?");
                indexSN++;

                //結束，空值默認為最後一層Type
            }
            else
            {
                TXT_List.Add(indexSN, type.Name.Split('`')[0]);
                indexSN++;

                // 處理下一層。  看起來GetGenericArguments()裡面只會有1個Item。
                if (type.GetGenericArguments().Count() > 0)
                {
                    GetTypeName_NextLoop(type.GetGenericArguments()[0], ref TXT_List, ref indexSN);
                }
            }
            #endregion

            #region == 整理成String ==
            // 大~小走訪，產生ClassName
            foreach (var item in TXT_List)
            {
                var tempName = item.Value;

                #region == ClassName 調整為C#內使用的型別名稱 ==
                switch (tempName)
                {
                    case "Int64":
                        tempName = "long";
                        break;
                    case "Int64?":
                        tempName = "long?";
                        break;
                    default:
                        break;
                }
                #endregion

                result = string.IsNullOrEmpty(result) ? item.Value : $"{result}<{tempName}>";
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 取得型別名稱(NextLoop) (先執行先填入資料)(默認每層都只有一個項目)
        /// </summary>
        /// <param name="type">下一層資料</param>
        /// <param name="_list">紀錄值</param>
        /// <param name="_indexSN">索引值</param>
        private static void GetTypeName_NextLoop(Type type, ref Dictionary<int, string> _list, ref int _indexSN)
        {
            // [T：允許空值的型別][F：不允許空值]
            if (type.Name == "Nullable`1")
            {
                _list.Add(_indexSN, type.GetGenericArguments()[0].Name + "?");
                _indexSN++;

                //結束，空值默認為最後一層Type
            }
            else
            {
                _list.Add(_indexSN, type.Name.Split('`')[0]);
                _indexSN++;

                // 處理下一層
                if (type.GetGenericArguments().Count() > 0)
                {
                    GetTypeName_NextLoop(type.GetGenericArguments()[0], ref _list, ref _indexSN);
                }
            }
        }
        #endregion
    }
}
