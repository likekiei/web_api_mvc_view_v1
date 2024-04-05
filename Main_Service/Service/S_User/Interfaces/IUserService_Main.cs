//using Main_Common.Model.Result;
//using Main_Common.Model.Tool;
//using Main_Common.Model.Data.User;
//using Main_EF.Table;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Main_Service.Service.S_User.Interfaces
//{
//    /// <summary>
//    /// 【interface】使用者相關
//    /// </summary>
//    public interface IUserService_Main
//    {
//        ///// <summary>
//        ///// 取全部資料
//        ///// </summary>
//        ///// <returns></returns>
//        //IEnumerable<User> GetAll_User();

//        /// <summary>
//        /// 【單筆】【修改用】取得使用者
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        User_Model GetModel_User_Edit(long key);

//        /// <summary>
//        /// 【多筆】【可分頁】取使用者清單
//        /// </summary>
//        /// <param name="input">查詢條件</param>
//        /// <param name="pageingDTO">分頁條件</param>
//        /// <returns></returns>
//        ResultOutput_Data<List<User_List>> GetList_User(User_Filter input, Pageing_DTO pageingDTO);

//        /// <summary>
//        /// 【單筆】新增使用者
//        /// </summary>
//        /// <param name="input">資料</param>
//        /// <returns></returns>
//        ResultOutput_Data<long?> Create_User(User_Model input);

//        /// <summary>
//        /// 【多筆】新增使用者
//        /// </summary>
//        /// <param name="inputs">資料</param>
//        /// <returns></returns>
//        ResultOutput_Data<List<long>> Creates_User(List<User_Model> inputs);

//        /// <summary>
//        /// 【單筆】修改使用者
//        /// </summary>
//        /// <param name="input">資料</param>
//        /// <returns></returns>
//        ResultOutput_Data<long?> Edit_User(User_Model input);

//        /// <summary>
//        /// 【多筆】修改使用者
//        /// </summary>
//        /// <param name="inputs">資料</param>
//        /// <returns></returns>
//        ResultOutput_Data<List<long>> Edits_User(List<User_Model> inputs);

//        /// <summary>
//        /// 【單筆】刪除使用者
//        /// </summary>
//        /// <param name="input">資料</param>
//        /// <returns></returns>
//        ResultOutput_Data<long?> Delete_User(User_Model input);

//        /// <summary>
//        /// 【多筆】刪除使用者
//        /// </summary>
//        /// <param name="inputs">資料</param>
//        /// <returns></returns>
//        ResultOutput_Data<List<long>> Deletes_User(List<User_Model> inputs);

//        /// <summary>
//        /// 取特定Id的資料
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        User Get_User_ById(long id);

//        /// <summary>
//        /// 更新資料
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        bool Update_User(User data);

//        /// <summary>
//        /// 刪除資料
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        bool Delete_User(long id);
//    }
//}
