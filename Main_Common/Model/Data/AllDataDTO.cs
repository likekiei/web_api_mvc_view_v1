using Main_Common.Model.Data.Company;
using Main_Common.Model.Data.Role;
using Main_Common.Model.Data.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Common.Model.Data
{
    /// <summary>
    /// 全部資料的DTO
    /// </summary>
    public class AllDataDTO
    {
        #region == 主要屬性 ===============================================================================
        /// <summary>
        /// 公司清單
        /// </summary>
        [Display(Name = "公司清單")]
        public List<Company_DTO> CompanyDTOs { get; set; }
        /// <summary>
        /// 角色清單
        /// </summary>
        [Display(Name = "角色清單")]
        public List<Role_DTO> RoleDTOs { get; set; }
        /// <summary>
        /// 使用者清單
        /// </summary>
        [Display(Name = "使用者清單")]
        public List<User_DTO> UserDTOs { get; set; }
        #endregion

        #region == 其他屬性 ===============================================================================
        // ...
        #endregion

        #region == 建構 ===============================================================================
        /// <summary>
        /// 建構-初始值
        /// </summary>
        public AllDataDTO()
        {
            this.CompanyDTOs = new List<Company_DTO>();
            this.RoleDTOs = new List<Role_DTO>();
            this.UserDTOs = new List<User_DTO>();
        }
        #endregion
    }
}
