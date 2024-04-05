using Main_EF.Interface;
using Main_EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Service.Service.S_TestTemplate
{
    /// <summary>
    /// TestTemplate相關
    /// </summary>
    public class TestTemplateService_Main
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 資料庫工作單元
        /// </summary>
        public readonly IUnitOfWork _unitOfWork;
        #endregion

        #region == 【全域宣告】 ==
        // ...
        #endregion

        //--【建構】=================================================================================

        #region == 建構 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="unitOfWork">工作單元</param>
        public TestTemplateService_Main(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        //--【方法】=================================================================================

        #region == 方法 ==
        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Create_TestTemplate(_TestTemplate data)
        {
            if (data != null)
            {
                _unitOfWork._TestTemplateRepository.Add(data);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete_TestTemplate(long id)
        {
            if (id > 0)
            {
                var data = _unitOfWork._TestTemplateRepository.GetById(id);
                if (data != null)
                {
                    _unitOfWork._TestTemplateRepository.Delete(data);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 取全部資料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<_TestTemplate> GetAll_TestTemplate()
        {
            var dataList = _unitOfWork._TestTemplateRepository.GetAll();
            return dataList;
        }

        /// <summary>
        /// 取特定Id的資料
        /// </summary>
        /// <param name="TestTemplateId"></param>
        /// <returns></returns>
        public _TestTemplate Get_TestTemplate_ById(long id)
        {
            if (id > 0)
            {
                var data = _unitOfWork._TestTemplateRepository.GetById(id);
                if (data != null)
                {
                    return data;
                }
            }
            return null;
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update_TestTemplate(_TestTemplate data)
        {
            if (data != null)
            {
                var TestTemplate = _unitOfWork._TestTemplateRepository.GetById(data.Id);
                if (TestTemplate != null)
                {
                    TestTemplate.No = data.No;
                    TestTemplate.Name = data.Name;

                    _unitOfWork._TestTemplateRepository.Update(TestTemplate);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        #endregion
    }
}
