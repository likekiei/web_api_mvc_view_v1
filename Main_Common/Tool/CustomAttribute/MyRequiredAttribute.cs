using Main_Resources.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Tool.CustomAttribute
{
    public class MyRequiredAttribute : RequiredAttribute
    {
        public MyRequiredAttribute()
        {
            //ErrorMessage = "欄位 {0} 是必要項";
            ErrorMessageResourceType = typeof(RES_ModelValidationMessage);
            ErrorMessageResourceName = nameof(RES_ModelValidationMessage.Required);
        }
    }
}
