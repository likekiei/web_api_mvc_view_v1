using Azure;
using ERP_APP.Service.S_MF_TY;
using ERP_APP.Service.S_Product_Inspection;
using ERP_APP.Service.S_SPC_LST;
//using ERP_APP.Service.S_INV_NO;
//using ERP_APP.Service.S_MF_ARP;
//using ERP_APP.Service.S_MF_PSS;
//using ERP_APP.Service.S_BIL_SPC;
//using ERP_APP.Service.S_CUST;
//using ERP_APP.Service.S_DEPT;
//using ERP_APP.Service.S_MF_YG;
//using ERP_APP.Service.S_MY_WH;
//using ERP_APP.Service.S_Employee;
//using ERP_APP.Service.S_MF_POS;
//using ERP_APP.Service.S_Order;
//using ERP_APP.Service.S_TF_PSS;
using ERP_APP.Service.S_WORD_ORDER;
using ERP_EF.Models;
using Main_Common.GlobalSetting;
using Main_Common.Model.Account;
using Main_Common.Model.Main;
using Main_Common.Model.Tool;
using Main_Common.Tool;
using Main_EF;
using Main_EF.ServiceExtension;
using Main_Service.Service.S_Company;
using Main_Service.Service.S_ConnectSetting;
using Main_Service.Service.S_Log;
using Main_Service.Service.S_Login;
using Main_Service.Service.S_Role;
using Main_Service.Service.S_TestTemplate;
using Main_Service.Service.S_User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Web_API_APP.Service;
using Web_API.Controllers;

namespace Web_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //string path = AppContext.BaseDirectory; //test
            //string temp = "D:\\**\\專案\\**\\***\\Xmls"; //Xml檔本機測試位置 ，因目前是絕對路經所以放到客戶端~都要修改成客戶端路經!
            //string temp = "D:\\**\\**\\**\\***\\Xmls"; //Xml檔本機測試位置 ，因目前是絕對路經所以放到客戶端~都要修改成客戶端路經!
            //string temp = "C:\\KuanYu_API\\KUAN_YU_API";  // IIS測試位置
            //string temp = "C:\\WEB\\KUAN_YU_API";  // IIS測試位置
            string temp = "D:\\***\\****";  //**API IIS測試位置

            //注解說明文字的xml檔可多檔讀入至SWAGGER的XML註解文字方法
            var xmlPaths = new string[]
            {
                    $"{temp}\\main_common.xml",  //Xml檔名,model 注解
                    $"{temp}\\Web_API.xml"   //Xml檔名,action api 注解
            };

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            // 註冊 Swagger 產生器
            services.AddSwaggerGen(options =>
            {
                // API 服務簡介
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0 2024/02/15",                    
                    Title = "*** API",
                    Description = "***<==>***ERP API",
                    //TermsOfService = new Uri("https:///"),
                    Contact = new OpenApiContact
                    {
                        Name = "magic",
                        //Email = string.Empty,
                        //Url = new Uri("https:///"),
                    }
                });

                options.EnableAnnotations(); // 啟用更多註釋

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization"
                });
                
                options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

 

                // 讀取 XML 檔案產生在SWAGGER UI 產生API 注解說明，
                foreach (var xml in xmlPaths)
                {
                    options.IncludeXmlComments(xml);
                }



                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());


                // 讀取 XML 檔案產生 API 說明
                //var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");
                //options.IncludeXmlComments(filePath);
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);

                //services.AddSwaggerGen(c => {
                //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                //    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //    c.IncludeXmlComments(xmlPath);
                //});


            });

            //services.AddSwaggerGenNewtonsoftSupport();



            // Add services to the container.
            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All)); // 避免ViewBag,ViewData,TempData中文亂碼

            #region == Configure相關 ==
            //services.Configure<ConfigFixedValue_DTO>(this.Configuration.GetSection("ConfigFixedValue")); // 改用靜態類別(GlobalParameter)處理了
            #endregion

            #region == Model相關(整個專案全域用) ==
            services.AddScoped<MainSystem_DTO>(); // 主系統資料
            //services.AddScoped<ParametersDefaultValue_DTO>(); // 改用靜態類別(GlobalParameter)處理了
            #endregion

            #region == Cookie登入驗證 (CookieAuthentication) ==
            // 設定 Cookie 式登入驗證，指定登入登出 Action
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = GlobalParameter.AuthCookieName;
                    options.Cookie.HttpOnly = true;
                    options.LoginPath = GlobalParameter.AuthLoginPath; // 未登入時，導向路徑
                    //options.AccessDeniedPath = "/Auth/AccessDenied"; // 未授權時，導向路徑
                    options.ExpireTimeSpan = TimeSpan.FromDays(GlobalParameter.AuthCookieKeepDay); // 設定Cookie存活時間
                    options.SlidingExpiration = true; // 有操作時，是否自動延長時間
                });
            #endregion

            #region == 資料庫服務相關 ==
            services.AddDIServices(Configuration);
            services.AddScoped<TestTemplateService_Main>();
            services.AddScoped<CompanyService_Main>();
            //services.AddScoped<ConnectSettingService_Main>();
            //services.AddScoped<Cust_Service>(); 
            //services.AddScoped<Employee_Service>();
            services.AddScoped<Log_Service>();
            services.AddScoped<Login_Service>();
            services.AddScoped<Order_Service>();            
            services.AddScoped<LogService_Main>();            
            services.AddScoped<LoginService_Main>();
            services.AddScoped<RoleService_Main>();
            services.AddScoped<UserService_Main>();            
            //services.AddScoped<CUST_Service_Erp>();
            //services.AddScoped<DEPT_Service_Erp>();
            //services.AddScoped<MF_POS_Service_Erp>();
            //services.AddScoped<MF_YG_Service_Erp>();
            //services.AddScoped<MY_WH_Service_Erp>();
            //services.AddScoped<BIL_SPC_Service_Erp>();
            //services.AddScoped<Employee_Service_Erp>();
            //services.AddScoped<Order_Service_Erp>();
            //services.AddScoped<MF_PSS_Service_Erp>();
            //services.AddScoped<MF_ARP_Service_Erp>();
            //services.AddScoped<INV_NO_Service_Erp>();
            //services.AddScoped<TF_PSS_Service_Erp>();
            services.AddScoped<Word_Order_Service_Erp>();
            services.AddScoped<Product_Inspection_Serveice_Erp>();
            services.AddScoped<MF_TI_Z_Service_Erp>();
            services.AddScoped<MF_TY_Service_Erp>();
            services.AddScoped<MF_TY_Z_Service_Erp>();
            services.AddScoped<SPC_LST_Service_Erp>();
            services.AddScoped<DB_T014Context>();
            
            #endregion

            #region == 過濾器(Filter) ==
            // 全部Controller都設定，難以達到全部Action都綁定，就不這樣用了
            //services.AddControllersWithViews(x => x.Filters.Add(typeof(MyExceptionFilter))); 
            //services.AddControllersWithViews(x => x.Filters.Add(typeof(MyResultFilter)));
            #endregion

            #region == 其他服務相關 ==
            //services.AddScoped<MyCheckHelper>();
            services.AddScoped<Message_Tool>();
            //services.AddScoped<LogTool>();
            #endregion




        }


 

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            #region == 語系切換相關 ==
            /// 網路上的做法有點跟我想要的不太一樣，感覺限制有點多。
            /// 所以最後決定在程式裡面自行切換，建議再請求進來的時候，能越早切換語系越好。

            //var supportedCultures = new List<CultureInfo>()
            //{
            //    new CultureInfo("en-US"),
            //    new CultureInfo("zh-TW"),
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions()
            //{
            //    DefaultRequestCulture = new RequestCulture("zh-TW"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures,
            //    RequestCultureProviders = new List<IRequestCultureProvider>
            //    {
            //        new CookieRequestCultureProvider(), // 他預設給的Cookie名稱，感覺很容易重複...不想研究，所以自己處理了
            //    },
            //});
            #endregion

            //using (var scope = app.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    SeedData.Initialize(services);
            //}

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // 這樣的話，Controller、Action不必再加上[RequireHttps]屬性
            app.UseStaticFiles();
            app.UseRouting();

            // 留意寫Code順序，先執行驗證...
            app.UseAuthentication();
            // Controller、Action才能加上 [Authorize] 屬性
            app.UseAuthorization();
            // 微軟建議 ASP.net Core 3.0 開始改用 Endpoint
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    //pattern: "{controller=Home}/{action}/{id?}"
                    );            
            
            });

        }



    }
}