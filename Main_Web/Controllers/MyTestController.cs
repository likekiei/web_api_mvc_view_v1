using Main_Common.Model.Test;
using Main_Common.Model.Data.User;
using Microsoft.AspNetCore.Mvc;

namespace Main_Web.Controllers
{
    public class MyTestController : Controller
    {
        public IActionResult Index()
        {

            //var model = new FormModel_Basic();
            var model = new User_Filter();
            return View(model);
        }

        /// <summary>
        /// 提交表單(基礎表單相關)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //public IActionResult Submit_FormBasic(FormModel_Basic data)
        public IActionResult Submit_FormBasic(User_Filter data)
        {
            return Json(new { _isSuccess = false, _message = "Test" });
        }

        // 不顯示一般警示
#pragma warning disable
        public IActionResult IndexA()
        {
            return View();
        }
#pragma warning restore
    }
}
