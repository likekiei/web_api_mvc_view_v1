using Main_EF.Interface.ITableRepository;
using Main_EF.Interface;
using Main_EF.Repositories.TableRepository;
using Main_EF.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Main_EF.Table;

namespace Main_EF.ServiceExtension
{
    /// <summary>
    /// 服務擴充
    /// </summary>
    public static class ServiceExtension_EF_Main
    {
        /// <summary>
        /// DI注入服務
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext_Main>(options =>
            {
                // 設定連線字串，依「appsettings.json」檔案內的ConnectionStrings Name抓取
                options.UseSqlServer(configuration.GetConnectionString("Main_DB"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork_Main>();
            services.AddScoped<ITestTemplateRepository, TestTemplateRepository_Main>();
            services.AddScoped<ICompanyRepository, CompanyRepository_Main>();
            services.AddScoped<IFunctionCodeRepository, FunctionCodeRepository_Main>();
            services.AddScoped<ILogRepository_Main, LogRepository_Main>();
            services.AddScoped<ISimpleLogRepository_Main, SimpleLogRepository_Main>();
            services.AddScoped<ILoginStatusRepository_Main, LoginStatusRepository_Main>();
            services.AddScoped<IRoleRepository, RoleRepository_Main>();
            services.AddScoped<IUserRepository, UserRepository_Main>();

            return services;
        }
    }
}
