//using Microsoft.AspNetCore.Authentication.Cookies;

//namespace Main_Web
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            #region == DI�`�J ==
//            // Add services to the container.
//            builder.Services.AddControllersWithViews();

//            #region == Cookie�n�J���� (CookieAuthentication) ==
//            // �]�w Cookie ���n�J���ҡA���w�n�J�n�X Action
//            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//                {
//                    options.LoginPath = "/Account/Login";
//                    //options.LogoutPath = "/Auth/Logout";
//                    //options.AccessDeniedPath = "/Auth/AccessDenied"; // �����v�ɡA�ɦV���|
//                    options.ExpireTimeSpan = TimeSpan.FromDays(30); // �]�wCookie�s���ɶ�
//                    options.SlidingExpiration = true; // ���ާ@�ɡA�O�_�۰ʩ����ɶ�
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