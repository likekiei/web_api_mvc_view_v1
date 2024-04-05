//using Microsoft.AspNetCore.Authentication.Cookies;

//namespace Main_Web
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            #region == DI注入 ==
//            // Add services to the container.
//            builder.Services.AddControllersWithViews();

//            #region == Cookie登入驗證 (CookieAuthentication) ==
//            // 設定 Cookie 式登入驗證，指定登入登出 Action
//            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//                {
//                    options.LoginPath = "/Account/Login";
//                    //options.LogoutPath = "/Auth/Logout";
//                    //options.AccessDeniedPath = "/Auth/AccessDenied"; // 未授權時，導向路徑
//                    options.ExpireTimeSpan = TimeSpan.FromDays(30); // 設定Cookie存活時間
//                    options.SlidingExpiration = true; // 有操作時，是否自動延長時間
//                });
//            #endregion

//            builder.Services.AddHttpContextAccessor();
//            #endregion

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthorization();

//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");

//            app.Run();
//        }
//    }
//}