using Main_Common.Enum.E_ProjectType;
using Main_Common.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Main_Common.ExtensionMethod
{
    /// <summary>
    /// 類別型別的擴充功能 [EX：class, model...]
    /// </summary>
    public static class ClassTypeExtension
    {
        #region == HttpPostedFileBase (.net core 不使用了)(要改用 IFormFile)(哪天用到的時候再研究了) ================================================================
        ///// <summary>
        ///// HttpPostedFileBase轉mFile_Model
        ///// </summary>
        ///// <param name="_mFile">檔案</param>
        ///// <param name="input">上傳資料</param>
        ///// <returns></returns>
        //public static mFile_Model To_mFileInput(this HttpPostedFileBase _mFile, mFile_Model input)
        //{
        //    mFile_Model result = null;

        //    if (_mFile != null) //有圖片才處理
        //    {
        //        //將檔案轉成Byte[]存放
        //        var length = _mFile.ContentLength; //檔案大小(bytes)
        //        var buffer = new byte[length]; //用來存放Byte[]
        //        _mFile.InputStream.Read(buffer, 0, length); //讀取檔案，將其存入緩存區(buffer)

        //        result = new mFile_Model
        //        {
        //            //Id = 0,
        //            Id = input.Id,
        //            MIME_Type = _mFile.ContentType,
        //            File_Content = buffer,
        //            File_Type_Id = input.File_Type_Id,
        //            File_Bind_Guid = input.File_Bind_Guid,
        //            DB_Table_Id = input.DB_Table_Id,
        //            Company_Id = input.Company_Id,
        //            //FileName = _mFile.FileName.Split('.')[0],
        //            //Filename_Extension = "." + _mFile.FileName.Split('.')[1],
        //        };

        //        //整理檔名/副檔名
        //        var spltFileName = _mFile.FileName.Split('.');
        //        if (spltFileName.Count() == 2) //拆字後，有2個項目，表示[XX.XXX]
        //        {
        //            result.File_Name = spltFileName[0];
        //            result.File_Name_Extension = "." + spltFileName[1];
        //        }
        //        else //拆字後，沒有2個項目，表示有問題的名稱
        //        {
        //            result.File_Name = _mFile.FileName;
        //        }
        //    }

        //    return result;
        //}
        #endregion


        #region == Json相關 ==
        /// <summary>
        /// 將Model轉換為Json (忽略循環引用)
        /// </summary>
        /// <typeparam name="T">輸入物件格式</typeparam>
        /// <param name="model">Model資料</param>
        /// <returns></returns>
        public static string ModelToJson<T>(T model)
        {
            //忽略循環引用
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(model, jsonSerializerSettings);
        }

        /// <summary>
        /// 將Json轉換為Model
        /// </summary>
        /// <typeparam name="T">輸出物件格式</typeparam>
        /// <param name="jsonString">Json資料</param>
        /// <returns></returns>
        public static T JsonToModel<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 將Model轉換為Model
        /// </summary>
        /// <typeparam name="T1">輸入物件格式</typeparam>
        /// <typeparam name="T2">輸出物件格式</typeparam>
        /// <param name="Data1">輸入物件資訊</param>
        /// <returns>T2</returns>
        public static T2 ModelToModel<T1, T2>(T1 model)
        {
            //忽略循環引用
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            var orgJsonData = JsonConvert.SerializeObject(model, jsonSerializerSettings);
            var newModel = JsonConvert.DeserializeObject<T2>(orgJsonData);
            return newModel;
        }
        #endregion
    }
}
