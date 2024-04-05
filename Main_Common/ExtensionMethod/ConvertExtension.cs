using AutoMapper;
using Main_Common.Model.Account;
using Main_Common.Model.Data;
using Main_Common.Model.Other;
using Main_Common.Model.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.ExtensionMethod
{
    /// <summary>
    /// 型別轉換的擴充功能
    /// </summary>
    public static class ConvertExtension
    {
        /// <summary>
        /// 備註
        /// </summary>
        private static void Rem()
        {
            // 主要套件工具
            // AutoMapper
            // Newtonsoft
        }

        #region == Model / Model ================================================================
        #region == 【T】深複製 ==
        /// <summary>
        /// 深複製Model (完整複製)
        /// </summary>
        /// <typeparam name="TInput">輸入型別</typeparam>
        /// <param name="input">資料</param>
        /// <returns></returns>
        public static TInput EM_CopyModel<TInput>(this TInput input)
        {
            //註冊Model間的對映 <A => B>
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInput, TInput>());
            //證驗應對
            //config.AssertConfigurationIsValid();
            //建立Mapper
            var mapper = config.CreateMapper();
            //轉換型別
            return mapper.Map<TInput>(input);
        }

        /// <summary>
        /// 深複製Model List (完整複製)
        /// </summary>
        /// <typeparam name="TInput">輸入型別</typeparam>
        /// <param name="inputs">資料</param>
        /// <returns></returns>
        public static List<TInput> EM_CopyModelList<TInput>(this List<TInput> inputs)
        {
            //註冊Model間的對映 <A => B>
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInput, TInput>());
            //證驗應對
            //config.AssertConfigurationIsValid();
            //建立Mapper
            var mapper = config.CreateMapper();
            //轉換型別
            return mapper.Map<List<TInput>>(inputs);
        }
        #endregion

        #region == 【T】轉換 ==
        /// <summary>
        /// 轉換Model
        /// </summary>
        /// <typeparam name="TInput">輸入型別</typeparam>
        /// <typeparam name="TOutput">輸出型別</typeparam>
        /// <param name="input">資料</param>
        /// <returns></returns>
        public static TOutput ConvertModel<TInput, TOutput>(this TInput input)
        {
            //註冊Model間的對映 <A => B>
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInput, TOutput>());
            //證驗應對
            //config.AssertConfigurationIsValid();
            //建立Mapper
            var mapper = config.CreateMapper();
            //轉換型別
            return mapper.Map<TOutput>(input);
        }

        /// <summary>
        /// 轉換Model List
        /// </summary>
        /// <typeparam name="TInput">輸入型別</typeparam>
        /// <typeparam name="TOutput">輸出型別</typeparam>
        /// <param name="inputs">資料</param>
        /// <returns></returns>
        public static List<TOutput> ConvertModelList<TInput, TOutput>(this List<TInput> inputs)
        {
            //註冊Model間的對映 <A => B>
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInput, TOutput>());
            //證驗應對
            //config.AssertConfigurationIsValid();
            //建立Mapper
            var mapper = config.CreateMapper();
            //轉換型別
            return mapper.Map<List<TOutput>>(inputs);
        }
        #endregion

        #region == 【Employee_Input】Main_Common.Model.DTO.Employee ==
        /// <summary>
        /// 資料轉換 [Employee_Input => Employee]
        /// [Employee_Input]Main_Common.Model.DTO.Employee
        /// [Employee]Main_Web_EF.Table
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public static Employee ToModel_INV_Id_Output(this Employee_Input input)
        //{
        //    //註冊Model間的對映 <A => B>
        //    var autoMapperConfig = new MapperConfiguration(cfg =>
        //        cfg.CreateMap<INV_Id_Input, INV_Id_Output>()
        //        .ForMember(x => x.CHK_CODE, y => y.MapFrom(o => o.CHK_CODE))
        //        .ForMember(x => x.DEP, y => y.MapFrom(o => o.DEP))
        //        .ForMember(x => x.E_SEQ_NO, y => y.MapFrom(o => o.E_SEQ_NO))
        //        .ForMember(x => x.E_SEQ_NO_Val, y => y.MapFrom(o => o.E_SEQ_NO_Val))
        //        .ForMember(x => x.F_SEQ_NO, y => y.MapFrom(o => o.F_SEQ_NO))
        //        .ForMember(x => x.F_SEQ_NO_Val, y => y.MapFrom(o => o.F_SEQ_NO_Val))
        //        .ForMember(x => x.INV_BOOK, y => y.MapFrom(o => o.INV_BOOK))
        //        .ForMember(x => x.INV_Id, y => y.MapFrom(o => o.INV_Id))
        //        .ForMember(x => x.RAND_NO, y => y.MapFrom(o => o.RAND_NO))
        //        .ForMember(x => x.SEQ_NO, y => y.MapFrom(o => o.SEQ_NO))
        //        .ForMember(x => x.SEQ_NO_Val, y => y.MapFrom(o => o.SEQ_NO_Val))
        //        .ForMember(x => x.TRACK_Id, y => y.MapFrom(o => o.TRACK_Id))
        //        .ForMember(x => x.UNI_NO_PAY, y => y.MapFrom(o => o.UNI_NO_PAY))
        //        .ForMember(x => x.UNI_TITLE_PAY, y => y.MapFrom(o => o.UNI_TITLE_PAY))
        //        .ForMember(x => x.USEDEP, y => y.MapFrom(o => o.USEDEP))
        //        .ForMember(x => x.YYMM, y => y.MapFrom(o => o.YYMM))
        //        );

        //    var mapper = autoMapperConfig.CreateMapper(); //建立Mapper
        //    return mapper.Map<INV_Id_Output>(input); //轉換型別
        //}
        #endregion

        #region == 【Employee_License_Record_MF_Output】Main_Common.Model.DTO.Employee ==
        ///// <summary>
        ///// 資料轉換 [Employee_License_Record_MF_Output => Employee_License_Record_MF_Input]
        ///// 批量新增用，員工證件紀錄-表身
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static Employee_License_Record_MF_Input ToModel_Employee_License_Record_MF_Input_GiveBatchCreate(this Employee_License_Record_MF_Output input)
        //{
        //    //註冊Model間的對映 <A => B>
        //    var autoMapperConfig = new MapperConfiguration(cfg =>
        //        cfg.CreateMap<Employee_License_Record_MF_Output, Employee_License_Record_MF_Input>()
        //        .ForMember(x => x.Id, y => y.MapFrom(o => o.Id))
        //        .ForMember(x => x.Check_DD_Sta, y => y.MapFrom(o => o.Check_DD_Sta))
        //        .ForMember(x => x.Employee_No, y => y.MapFrom(o => o.Employee_No))
        //        .ForMember(x => x.Employee_Name, y => y.MapFrom(o => o.Employee_Name))
        //        .ForMember(x => x.License_No, y => y.MapFrom(o => o.License_No))
        //        .ForMember(x => x.License_Name, y => y.MapFrom(o => o.License_Name))
        //        .ForMember(x => x.License_Name, y => y.MapFrom(o => o.License_Name))
        //        );

        //    var mapper = autoMapperConfig.CreateMapper(); //建立Mapper
        //    return mapper.Map<Employee_License_Record_MF_Input>(input); //轉換型別
        //}

        ///// <summary>
        ///// 資料轉換 [Employee_License_Record_MF_Output => Employee_License_Record_MF_Input]
        ///// 批量新增用，員工證件紀錄-表身
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static List<Employee_License_Record_MF_Input> ToModel_Employee_License_Record_MF_Input_GiveBatchCreate(this List<Employee_License_Record_MF_Output> input)
        //{
        //    //註冊Model間的對映 <A => B>
        //    var autoMapperConfig = new MapperConfiguration(cfg =>
        //        cfg.CreateMap<Employee_License_Record_MF_Output, Employee_License_Record_MF_Input>()
        //        .ForMember(x => x.IsClose, y => y.Ignore())
        //        .ForMember(x => x.IsError_LicenseCondition, y => y.Ignore())
        //        .ForMember(x => x.LicenseCondition_Name, y => y.Ignore())
        //        .ForMember(x => x.RemainingCount, y => y.Ignore())
        //        .ForMember(x => x.Age, y => y.Ignore())
        //        .ForMember(x => x.Check_DD_End, y => y.Ignore())
        //        .ForMember(x => x.Check_DD_New, y => y.Ignore())
        //        .ForMember(x => x.Count_Value, y => y.Ignore())
        //        .ForMember(x => x.Count_Year, y => y.Ignore())
        //        .ForMember(x => x.Employee_License_Id, y => y.Ignore())
        //        //.ForMember(x => x.Id, y => y.MapFrom(o => o.Id))
        //        //.ForMember(x => x.Check_DD_Sta, y => y.MapFrom(o => o.Check_DD_Sta))
        //        //.ForMember(x => x.Employee_No, y => y.MapFrom(o => o.Employee_No))
        //        //.ForMember(x => x.Employee_Name, y => y.MapFrom(o => o.Employee_Name))
        //        //.ForMember(x => x.License_No, y => y.MapFrom(o => o.License_No))
        //        //.ForMember(x => x.License_Name, y => y.MapFrom(o => o.License_Name))
        //        //.ForMember(x => x.License_Name, y => y.MapFrom(o => o.License_Name))
        //        );

        //    var mapper = autoMapperConfig.CreateMapper(); //建立Mapper
        //    return mapper.Map<List<Employee_License_Record_MF_Input>>(input); //轉換型別
        //}
        #endregion

        #region == 【轉換】【ResultOutput_Data】Main_Common.Model.Result.ResultOutput_Data ==
        /// <summary>
        /// 資料轉換 [ResultOutput_Data<TInput> => ResultOutput_Data<TOutput>]
        /// [ResultOutput_Data<TInput>]Main_Common.Model.Result.ResultOutput_Data
        /// [ResultOutput_Data<TOutput>]Main_Common.Model.Result.ResultOutput_Data
        /// 忽略泛型欄位(反正欄位也不一樣，轉了也沒用)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ResultOutput_Data<TOutput> EM_ToModel_ResultOutput_Data<TInput, TOutput>(this ResultOutput_Data<TInput> input)
        {
            //註冊Model間的對映 <A => B>
            var autoMapperConfig = new MapperConfiguration(cfg =>
                cfg.CreateMap<ResultOutput_Data<TInput>, ResultOutput_Data<TOutput>>()
                .ForMember(x => x.Data, y => y.Ignore())
                );

            var mapper = autoMapperConfig.CreateMapper(); //建立Mapper
            return mapper.Map<ResultOutput_Data<TOutput>>(input); //轉換型別
        }
        #endregion
        #endregion

        #region == Json / Model ================================================================
        /// <summary>
        /// 將Model轉換為Json (忽略循環引用)
        /// </summary>
        /// <typeparam name="TInput">輸入型別</typeparam>
        /// <param name="input">資料</param>
        /// <returns></returns>
        public static string EM_ModelToJson<TInput>(this TInput input)
        {
            //忽略循環引用
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(input, jsonSerializerSettings);
        }

        /// <summary>
        /// 將Json轉換為Model
        /// </summary>
        /// <typeparam name="TOutput">輸出型別</typeparam>
        /// <param name="jsonString">Json資料</param>
        /// <returns></returns>
        public static TOutput EM_JsonToModel<TOutput>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<TOutput>(jsonString);
        }

        /// <summary>
        /// 將Model轉換為Model 【改用AutoMapper的做法】
        /// </summary>
        /// <typeparam name="TInput">輸入型別</typeparam>
        /// <typeparam name="TOutput">輸出型別</typeparam>
        /// <param name="input">資料</param>
        /// <returns>TOutput</returns>
        //public static TOutput EM_ModelToModel<TInput, TOutput>(this TInput input)
        //{
        //    //忽略循環引用
        //    var jsonSerializerSettings = new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    };

        //    var orgJsonData = JsonConvert.SerializeObject(input, jsonSerializerSettings);
        //    var newModel = JsonConvert.DeserializeObject<TOutput>(orgJsonData);
        //    return newModel;
        //}
        #endregion
    }
}
