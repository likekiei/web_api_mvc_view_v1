using Main_Common.Enum.E_ProjectType;
using Main_Common.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.ExtensionMethod
{
    /// <summary>
    /// 基本型別的擴充功能 [EX：string, int...]
    /// </summary>
    public static class BasicTypeExtension
    {
        #region == object相關 ================================================================
        /// <summary>
        /// 檢查object的指定值是否有值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name">指定值的名稱</param>
        /// <returns>「true，空的(取無屬性視同null)」「false，有值」</returns>
        public static bool EM_CheckIsNull(this object obj, string name)
        {
            #region == 取object屬性 (Error return) ==
            // 取屬性
            var propInfo = obj.GetType().GetProperty(name);
            // [有無該屬性][T：無]
            if (propInfo == null)
            {
                return true;
            }
            #endregion

            #region == 取object屬性值 (Error return) ==
            // 取屬性值
            var objValue = propInfo.GetValue(obj);
            // [有無屬性值][T：無][F：有]
            if (objValue == null || objValue == "")
            {
                return true;
            }
            else
            {
                return false;
            }
            #endregion
        }
        #endregion

        #region == enum相關 ================================================================
        #region == enum to 其它 ==
        /// <summary>
        /// 取得列舉的int值
        /// </summary>
        /// <typeparam name="T">限定[System.Enum]</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int EM_GetEnumInt<T>(this T value) where T : System.Enum
        {
            var result = Convert.ToInt32(value);
            return result;
        }

        /// <summary>
        /// 取得列舉的描述文字 (如沒設定Description，則取列舉名稱)
        /// </summary>
        /// <typeparam name="T">限定[System.Enum]</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EM_GetEnumDescription<T>(this T value) where T : System.Enum
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var text = attributes.Length > 0 ? attributes[0].Description : value.ToString();
            return text;
        }
        #endregion

        #region == 其它 to enum ==
        /// <summary>
        /// 字串轉列舉檢查 (整數字串)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="txt">整數字串</param>
        /// <param name="myTypeCode">要處理的類型</param>
        /// <returns>回傳錯訊</returns>
        public static string EM_ToEnumCheck_ByIntString(this string txt, E_MyTypeCode myTypeCode)
        {
            string result = null;

            // 轉int
            var eId = txt.EM_StringToInt();
            // [T：轉型成功]
            if (eId.HasValue)
            {
                #region == 依條件執行(switch) ==
                switch (myTypeCode)
                {
                    //case E_TypeCode_MM.E_Sex:
                    //    if (System.Enum.IsDefined(typeof(E_Sex), eId.Value) == false)
                    //    {
                    //        return "不存在相同項目(E_Sex)";
                    //    }
                    //    break;
                    default:
                        return $"非預期的執行項[{myTypeCode.ToString()}]";
                }
                #endregion
            }
            else
            {
                return "提供的值非數值";
            }

            return result;
        }

        /// <summary>
        /// 字串轉列舉 (整數字串)(轉型成功才有項目值)
        /// <para>PS.因為不能回傳null泛型，所以如果轉型失敗，會強制回傳列舉的預設值(應該是第一項)</para>
        /// </summary>
        /// <param name="txt">整數字串</param>
        /// <returns></returns>
        public static T EM_ToEnum_ByIntString<T>(this string txt)
        {
            T result = default(T);

            // 轉int
            var eId = txt.EM_StringToInt();
            // [T：轉型成功]
            if (eId.HasValue)
            {
                if (System.Enum.IsDefined(typeof(T), eId.Value))
                {
                    result = (T)System.Enum.Parse(typeof(T), txt);
                }
            }

            return result;
        }

        /// <summary>
        /// 字串轉列舉 (整數字串清單)(只回傳轉型成功的)
        /// </summary>
        /// <param name="txt">整數字串清單</param>
        /// <returns></returns>
        public static List<T> EM_ToEnums_ByIntStringArray<T>(this string[] txts)
        {
            List<T> result = new List<T>();

            foreach (var txt in txts)
            {
                // 轉int
                var eId = txt.EM_StringToInt();
                // [T：轉型成功]
                if (eId.HasValue)
                {
                    if (System.Enum.IsDefined(typeof(T), eId.Value))
                    {
                        result.Add((T)System.Enum.Parse(typeof(T), txt));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 字串轉列舉 (功能權限列舉)(整數字串清單)(只回傳轉型成功的)
        /// </summary>
        /// <param name="txts">整數字串清單</param>
        /// <returns></returns>
        public static List<E_Function> EM_ToEnum_Function_ByIntStrings(this string[] txts)
        {
            var result = new List<E_Function>();

            foreach (var txt in txts)
            {
                // 轉int
                var eId = txt.EM_StringToInt();
                // [T：轉型成功]
                if (eId.HasValue)
                {
                    E_Function tryVal;
                    if (System.Enum.TryParse(txt, out tryVal)) //轉型成功 true
                    {
                        result.Add(tryVal);
                    }
                }
            }

            return result;
        }
        #endregion
        #endregion

        #region == string相關 ================================================================
        /// <summary>
        /// string轉Guid [失敗回傳null][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static Guid? EM_StringToGuid(this string txt)
        {
            Guid? result = null;
            Guid tryResult;

            // [T：轉型成功]
            if (Guid.TryParse(txt, out tryResult))
            {
                result = tryResult;
            }

            return result;
        }

        /// <summary>
        /// string轉int [失敗回傳null][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static int? EM_StringToInt(this string txt)
        {
            int? result = null;
            int tryResult;

            // [T：轉型成功]
            if (int.TryParse(txt, out tryResult))
            {
                result = tryResult;
            }

            return result;
        }

        /// <summary>
        /// string轉long [失敗回傳null][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static long? EM_StringToLong(this string txt)
        {
            long? result = null;
            long tryResult;

            // [T：轉型成功]
            if (long.TryParse(txt, out tryResult))
            {
                result = tryResult;
            }

            return result;
        }

        /// <summary>
        /// string[]轉List<long> [最差回傳list count 0][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static List<long> EM_StringsToLongList(this string[] txts)
        {
            var result = new List<long>();

            foreach (var txt in txts)
            {
                long tryResult;

                // [T：轉型成功]
                if (long.TryParse(txt, out tryResult))
                {
                    result.Add(tryResult);
                }
            }

            return result;
        }

        /// <summary>
        /// string轉decimal [失敗回傳null][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static decimal? EM_StringToDecimal(this string txt)
        {
            decimal? result = null;
            decimal tryResult;

            // [T：轉型成功]
            if (decimal.TryParse(txt, out tryResult))
            {
                result = tryResult;
            }

            return result;
        }

        /// <summary>
        /// string轉Datetime [失敗回傳null][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static DateTime? EM_StringToDatetime(this string txt)
        {
            DateTime? result = null;
            DateTime tryResult;

            // [T：轉型成功]
            if (DateTime.TryParse(txt, out tryResult))
            {
                result = tryResult;
            }

            return result;
        }

        /// <summary>
        /// string轉YYMM [失敗回傳null][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static DateTime? EM_StringToDatetimeYYMM(this string txt)
        {
            DateTime? result = null;
            DateTime tryResult;

            // [T：轉型成功]
            if (DateTime.TryParse(txt, out tryResult))
            {
                result = new DateTime(tryResult.Year, tryResult.Month, 1).Date; //確保[年,月,1]
            }

            return result;
        }

        /// <summary>
        /// string轉DatetimeYYMMDD [失敗回傳null][允許null的值請不要用，沒有特別處理，應該還是回傳null]
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static DateTime? EM_StringToDatetimeYYMMDD(this string txt)
        {
            DateTime? result = null;
            DateTime tryResult;

            // [T：轉型成功]
            if (DateTime.TryParse(txt, out tryResult))
            {
                result = new DateTime(tryResult.Year, tryResult.Month, tryResult.Day).Date; //確保[年,月,日]
            }

            return result;
        }
        #endregion

        #region == decimal ================================================================
        /// <summary>
        /// decimal去除小數位多餘的0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal EM_DecimalToG29(this decimal val)
        {
            var result = decimal.Parse(val.ToString("G29"));
            return result;
        }

        /// <summary>
        /// decimal去除小數位多餘的0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal? EM_DecimalToG29(this decimal? val)
        {
            var result = val.HasValue ? decimal.Parse(val.Value.ToString("G29")) : new Nullable<decimal>();
            return result;
        }
        #endregion

        #region == MethodBase ================================================================
        /// <summary>
        /// 取得Function路徑
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string EM_GetFunctionPath(this MethodBase val)
        {
            // 取方法完整路徑
            var result = $"{val.DeclaringType.FullName}.{val.Name}";
            return result;
        }
        #endregion
    }
}
