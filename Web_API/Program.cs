//using Main_EF;
//using Main_EF.Interface;
//using Main_EF.Repositories;
//using Main_EF.ServiceExtension;
//using Main_Service.Service.S_Company;
//using Main_Service.Service.S_User;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//#region == ��Ʈw�A�Ȭ��� ==
//builder.Services.AddDbContext<DbContext_Main>(options =>
//{
//    // �]�w�s�u�r��A�̡uappsettings.json�v�ɮפ���ConnectionStrings Name���
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Main_DB"));
//    // options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), new MySqlServerVersion(new Version(8, 0, 32)));       // <---add
//});
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork_Main>();
//builder.Services.AddScoped<CompanyService_Main>();
//builder.Services.AddScoped<UserService_Main>();
//#endregion

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Main_EF;
using Main_EF.Table;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //��l�Ƹ�Ʈw
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<DbContext_Main>();
                    dbContext.Database.EnsureCreated();

                    // Look for any User
                    // DB has been seeded
                    if (!dbContext.User.Any())
                    {
                        dbContext.Companys.Add(
                                new Companys
                                {
                                    GUID = Guid.NewGuid(),
                                    No = "0000",
                                    Name = "0000",
                                    CompanyLevelId = Main_Common.Enum.E_ProjectType.E_CompanyLevel.�̰���,
                                    CompanyLevelName = "�̰���",
                                    IsStop = false,
                                    Rem = "�t�θ�ƪ�l��",
                                    CreateTime = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString())
                                });
                        //context .Dept.Add(
                        //  new Dept
                        //  {
                        //      GUID = Guid.NewGuid(),
                        //      Company_ID = 1,
                        //      No = "Api",
                        //      Name = "Api",
                        //      Is_Stop = false,
                        //      DataFrom_ID = Main_Common.Enum.E_DataFrom.�D�t��,
                        //      DataFrom_Name = "�D�t��",
                        //      Create_DD = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString())
                        //  });
                        dbContext.Role.Add(
                            new Role
                            {
                                GUID = Guid.NewGuid(),
                                CompanyId = 1,
                                No = "Api",
                                Name = "Api",
                                IsStop = false,
                                PermissionTypeId = Main_Common.Enum.E_ProjectType.E_PermissionType.�@��ϥΪ�,
                                PermissionTypeName = "�@��ϥΪ�",
                                CreateTime = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString())
                            });
                        dbContext.User.Add(
                            new User
                            {
                                GUID = Guid.NewGuid(),
                                CompanyId = 1,
                                IsStop = false,
                                No = "Api",
                                Name = "Api",
                                Password = "0000",
                                RoleId = 1,
                                CreateTime = Convert.ToDateTime(DateTime.UtcNow.AddHours(8).ToString())
                            });
                        dbContext.SaveChanges();

                    }


                }
                catch (Exception ex)
                {
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(ex, "Create DB structure error. ");
                }


                host.Run();
            }
        }

        /// <summary>
        /// �Ы�WebHost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
