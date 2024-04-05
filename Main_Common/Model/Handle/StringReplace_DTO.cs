using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Handle
{
    /// <summary>
    /// 文字替換DTO
    /// </summary>
    public class StringReplace_DTO
    {
        /// <summary>
        /// 目標文字
        /// </summary>
        [Display(Name = "目標文字")]
        public string Target { get; set; }
        /// <summary>
        /// 來源文字
        /// </summary>
        [Display(Name = "來源文字")]
        public string Source { get; set; }

        //== 建構 =============================================================

        /// <summary>
        /// 建構
        /// </summary>
        /// <param name="target">目標</param>
        /// <param name="source">來源</param>
        public StringReplace_DTO(string target, string source)
        {
            this.Target = target;
            this.Source = source;
        }
    }
}
