using Main_Common.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Tool
{
    /// <summary>
    /// 型別轉換工具 (不適合加在擴充功能的)
    /// </summary>
    public class ConvertTool
    {
        #region == bool相關 ================================================================
        /// <summary>
        /// 將Bool轉換為List [默認第一筆不給預設空項目]
        /// </summary>
        /// <typeparam name="T">傳入的列舉型別</typeparam>
        /// <param name="queryVal">預選項目</param>
        /// <param name="trueText">true的文字</param>
        /// <param name="falseText">false的文字</param>
        /// <param name="defaultItem">是否提供預設項目(在第一筆)[null不提供，有值顯示在Text]</param>
        /// <returns></returns>
        public static List<SelectItemDTO> BoolToList(bool? queryVal, string defaultItem = null, string trueText = "是", string falseText = "否")
        {
            List<SelectItemDTO> result = new List<SelectItemDTO>();
            result.Add(new SelectItemDTO { id = "true", value = "true", text = trueText });
            result.Add(new SelectItemDTO { id = "false", value = "false", text = falseText });

            if (!string.IsNullOrEmpty(defaultItem)) //產生預設項目
            {
                result.Insert(0, new SelectItemDTO { id = null, value = null, text = defaultItem });
            }

            if (queryVal.HasValue) //預選項目
            {
                var target = result.Where(x => x.value == queryVal.ToString().ToLower()).FirstOrDefault();
                if (target != null) { target.Selected = true; }
            }

            return result;
        }
        #endregion

        #region == enum相關 ================================================================
        /// <summary>
        /// 將enum轉換為enumList<T><enum>
        /// </summary>
        /// <typeparam name="T">傳入的列舉型別</typeparam>
        /// <returns></returns>
        public static List<T> EnumToListEnum<T>()
        {
            List<T> result = new List<T>();

            if (typeof(T).IsEnum) //是列舉才處理
            {
                //取得列舉arrList
                result = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
            }

            return result;
        }

        /// <summary>
        /// 將enum轉換為enumList<T><enum>
        /// </summary>
        /// <typeparam name="T">傳入的列舉型別</typeparam>
        /// <param name="ignoreTXTs">要忽略的項目</param>
        /// <param name="keepKeys">要保留的項目</param>
        /// <returns></returns>
        public static List<T> EnumToListEnum<T>(string[] ignoreTXTs, string[] keepKeys)
        {
            List<T> result = new List<T>();

            if (typeof(T).IsEnum) //是列舉才處理
            {
                //取得列舉arrList
                result = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
            }

            //保留特定項目
            if (keepKeys != null && keepKeys.Count() > 0)
            {
                result = result.Where(x => keepKeys.Contains(x.ToString())).ToList();
            }

            //忽略特定項目
            if (ignoreTXTs != null && ignoreTXTs.Count() > 0)
            {
                result = result.Where(x => !ignoreTXTs.Contains(x.ToString())).ToList();
            }

            return result;
        }

        /// <summary>
        /// 將enum轉換為modelList [默認第一筆不給預設空項目]
        /// </summary>
        /// <typeparam name="T">傳入的列舉型別</typeparam>
        /// <param name="key">預選項目</param>
        /// <param name="defaultItemText">是否提供預設的Null項目在第一筆(沒給不產生)(有給產生相同Text的項目)</param>
        /// <returns></returns>
        public static List<SelectItemDTO> EnumToList<T>(int? key, string? defaultItemText)
        {
            List<SelectItemDTO> result = new List<SelectItemDTO>();
            if (typeof(T).IsEnum) //是列舉才處理
            {
                //取得列舉arrList
                var enumList = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
                //走訪列舉，產出結果
                foreach (var item in enumList)
                {
                    var enumVal = System.Convert.ToInt32(item);
                    var addDTO = new SelectItemDTO()
                    {
                        id = enumVal.ToString(),
                        value = enumVal.ToString(),
                        text = item.ToString(),
                        Selected = enumVal == key ? true : false,
                    };

                    result.Add(addDTO);
                }
                //加入預設項目
                if (!string.IsNullOrEmpty(defaultItemText))
                {
                    result.Insert(0, new SelectItemDTO { id = "", value = "", text = defaultItemText, Selected = true });
                }
            }

            return result;
        }

        /// <summary>
        /// 將enum轉換為modelList [默認第一筆不給預設空項目]
        /// </summary>
        /// <typeparam name="T">傳入的列舉型別</typeparam>
        /// <param name="key">預選項目</param>
        /// <param name="defaultItemText">是否提供預設的Null項目在第一筆(沒給不產生)(有給產生相同Text的項目)</param>
        /// <param name="ignoreKeys">要忽略的項目</param>
        /// <returns></returns>
        public static List<SelectItemDTO> EnumToList<T>(int? key, string? defaultItemText, int[] ignoreKeys)
        {
            List<SelectItemDTO> result = new List<SelectItemDTO>();
            if (typeof(T).IsEnum) //是列舉才處理
            {
                //取得列舉arrList
                var enumList = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
                //走訪列舉，產出結果
                foreach (var item in enumList)
                {
                    var enumVal = System.Convert.ToInt32(item);

                    //忽略特定項目
                    if (ignoreKeys != null && ignoreKeys.Count() > 0)
                    {
                        if (ignoreKeys.Contains(enumVal)) //有找到要忽略的Key，忽略該項目
                        {
                            continue;
                        }
                    }

                    var addDTO = new SelectItemDTO()
                    {
                        id = enumVal.ToString(),
                        value = enumVal.ToString(),
                        text = item.ToString(),
                        Selected = enumVal == key ? true : false,
                    };

                    result.Add(addDTO);
                }

                //加入預設項目
                if (!string.IsNullOrEmpty(defaultItemText))
                {
                    result.Insert(0, new SelectItemDTO { id = "", value = "", text = defaultItemText, Selected = true });
                }
            }

            return result;
        }

        /// <summary>
        /// 將enum轉換為modelList [默認第一筆不給預設空項目]
        /// </summary>
        /// <typeparam name="T">傳入的列舉型別</typeparam>
        /// <param name="key">預選項目</param>
        /// <param name="defaultItemText">是否提供預設的Null項目在第一筆(沒給不產生)(有給產生相同Text的項目)</param>
        /// <param name="ignoreKeys">要忽略的項目</param>
        /// <param name="keepKeys">要保留的項目</param>
        /// <returns></returns>
        public static List<SelectItemDTO> EnumToList<T>(int? key, string? defaultItemText, int[] ignoreKeys, int[] keepKeys)
        {
            List<SelectItemDTO> result = new List<SelectItemDTO>();
            if (typeof(T).IsEnum) //是列舉才處理
            {
                //取得列舉arrList
                var enumList = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
                //走訪列舉，產出結果
                foreach (var item in enumList)
                {
                    var enumVal = System.Convert.ToInt32(item);

                    //忽略特定項目
                    if (ignoreKeys != null && keepKeys.Count() > 0)
                    {
                        if (ignoreKeys.Contains(enumVal)) //有找到要忽略的Key，忽略該項目
                        {
                            continue;
                        }
                    }

                    var addDTO = new SelectItemDTO()
                    {
                        id = enumVal.ToString(),
                        value = enumVal.ToString(),
                        text = item.ToString(),
                        Selected = enumVal == key ? true : false,
                    };

                    result.Add(addDTO);
                }

                //只保留特定項目
                if (keepKeys != null && keepKeys.Count() > 0)
                {
                    result = result.Where(x => keepKeys.Contains(int.Parse(x.value))).ToList();
                }

                //加入預設項目
                if (!string.IsNullOrEmpty(defaultItemText))
                {
                    result.Insert(0, new SelectItemDTO { id = "", value = "", text = defaultItemText, Selected = true });
                }
            }

            return result;
        }
        #endregion

        #region == Json相關 ==
        ///// <summary>
        ///// 將Model轉換為Json (忽略循環引用)
        ///// </summary>
        ///// <typeparam name="T">輸入物件格式</typeparam>
        ///// <param name="model">Model資料</param>
        ///// <returns></returns>
        //public static string ModelToJson<T>(T model)
        //{
        //    //忽略循環引用
        //    var jsonSerializerSettings = new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    };

        //    return JsonConvert.SerializeObject(model, jsonSerializerSettings);
        //}

        ///// <summary>
        ///// 將Json轉換為Model
        ///// </summary>
        ///// <typeparam name="T">輸出物件格式</typeparam>
        ///// <param name="jsonString">Json資料</param>
        ///// <returns></returns>
        //public static T JsonToModel<T>(string jsonString)
        //{
        //    return JsonConvert.DeserializeObject<T>(jsonString);
        //}

        ///// <summary>
        ///// 將Model轉換為Model
        ///// </summary>
        ///// <typeparam name="T1">輸入物件格式</typeparam>
        ///// <typeparam name="T2">輸出物件格式</typeparam>
        ///// <param name="Data1">輸入物件資訊</param>
        ///// <returns>T2</returns>
        //public static T2 ModelToModel<T1, T2>(T1 model)
        //{
        //    //忽略循環引用
        //    var jsonSerializerSettings = new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    };

        //    var orgJsonData = JsonConvert.SerializeObject(model, jsonSerializerSettings);
        //    var newModel = JsonConvert.DeserializeObject<T2>(orgJsonData);
        //    return newModel;
        //}
        #endregion
    }
}
