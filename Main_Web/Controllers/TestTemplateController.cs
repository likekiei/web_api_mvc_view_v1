using Main_Common.Model.Main;
using Main_EF.Table;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_TestTemplate;
using Microsoft.AspNetCore.Mvc;

namespace Main_Web.Controllers
{
    /// <summary>
    /// TestTemplate相關
    /// </summary>
    public class TestTemplateController : BaseWebController
    {
        #region == 【DI注入用宣告】 ==
        /// <summary>
        /// 【DTO】主系統資料
        /// </summary>
        public readonly MainSystem_DTO _MainSystem_DTO;
        /// <summary>
        /// 【Main Service】登入相關
        /// </summary>
        public readonly LoginService_Main _LoginService_Main;
        /// <summary>
        /// 【Main Service】TestTemplate相關
        /// </summary>
        public readonly TestTemplateService_Main _TestTemplateService_Main;
        #endregion

        #region == 【全域宣告】 ==
        // ...
        #endregion

        #region == 【建構】 ==
        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="httpContextAccessor">HttpContext</param>
        /// <param name="mainSystem_DTO">主系統資料</param>
        /// <param name="loginService_Main">登入相關</param>
        /// <param name="testTemplateService_Main">testTemplateService_Main</param>
        public TestTemplateController(IHttpContextAccessor httpContextAccessor,
            MainSystem_DTO mainSystem_DTO,
            LoginService_Main loginService_Main,
            TestTemplateService_Main testTemplateService_Main)
            : base(httpContextAccessor, mainSystem_DTO, loginService_Main)
        {
            this._MainSystem_DTO = mainSystem_DTO;
            this._LoginService_Main = loginService_Main;
            this._TestTemplateService_Main = testTemplateService_Main;
        }
        #endregion

        //--【方法】=================================================================================

        #region == TestTemplate ==
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index_TestTemplate()
        {
            return View();
        }

        /// <summary>
        /// 取全部資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetAll_TestTemplate()
        {
            var TestTemplateList = this._TestTemplateService_Main.GetAll_TestTemplate().ToList();
            if (TestTemplateList == null)
            {
                return Json(new { _isSuccess = false, _message = "Test Error" });
            }

            return Json(new { _isSuccess = false, _message = "Test OK" });
        }

        /// <summary>
        /// 取特定Id的資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Get_TestTemplate_ById(int id)
        {
            //id = 0;
            var TestTemplate = this._TestTemplateService_Main.Get_TestTemplate_ById(id);

            if (TestTemplate != null)
            {
                return Json(new { _isSuccess = false, _message = "Test OK" });
            }
            else
            {
                return Json(new { _isSuccess = false, _message = "Test" });
            }
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="TestTemplate"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create_TestTemplate(_TestTemplate TestTemplate)
        {
            //_TestTemplate TestTemplate = null;
            var isCreate = this._TestTemplateService_Main.Create_TestTemplate(TestTemplate);

            if (isCreate)
            {
                return Json(new { _isSuccess = false, _message = "Test OK" });
            }
            else
            {
                return Json(new { _isSuccess = false, _message = "Test Error" });
            }
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="TestTemplate"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update_TestTemplate(_TestTemplate TestTemplate)
        {
            //_TestTemplate TestTemplate = null;
            if (TestTemplate != null)
            {
                var isUpdate = this._TestTemplateService_Main.Update_TestTemplate(TestTemplate);
                if (isUpdate)
                {
                    return Json(new { _isSuccess = false, _message = "Test OK" });
                }
                else
                {
                    return Json(new { _isSuccess = false, _message = "Test Error" });
                }
            }
            else
            {
                return Json(new { _isSuccess = false, _message = "Test Error" });
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete_TestTemplate(int id)
        {
            //int id = 0;
            var isDelete = this._TestTemplateService_Main.Delete_TestTemplate(id);

            if (isDelete)
            {
                return Json(new { _isSuccess = false, _message = "Test OK" });
            }
            else
            {
                return Json(new { _isSuccess = false, _message = "Test Error" });
            }
        }
        #endregion
    }
}
