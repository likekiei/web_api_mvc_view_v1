using Main_EF;
using Main_EF.Table;
using Microsoft.EntityFrameworkCore;

namespace Main_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var  host = CreateHostBuilder(args).Build();

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