using Main_Resources.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Tool.CustomAttribute
{
    public class MyMaxLengthAttribute :  MaxLengthAttribute
    {
        public MyMaxLengthAttribute(int length) : base(length)
        {
            //ErrorMessage = "欄位 {0} 字串最大長度不可超過 {1}";
            ErrorMessageResourceType = typeof(RES_ModelValidationMessage);
            ErrorMessageResourceName = nameof(RES_ModelValidationMessage.MaxLength);
        }
    }
}
