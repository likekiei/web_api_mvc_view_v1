// DbFirst指令
// 指令生成後，產生的DbContext檔案內會有一個「OnConfiguring」的Function，
// -Connection：依「appsettings.json」內的連線字串。
// -NoPluralize：不要生成複數名稱。
// -UseDatabaseNames：不要自動判斷名稱，讓名稱固定與資料庫相同。
// -OutputDir：Model的資料夾路徑。,
// -Tables：要指定的資料庫Table。
// -ContextDir：DbContext的資料夾路徑。,
// -Context：DbContext的檔案名稱。,
using System.Runtime.Remoting.Contexts;

"DbFirstCommand": "Scaffold-DbContext -Connection 'Name=ConnectionStrings:Erp_DB' Microsoft.EntityFrameworkCore.SqlServer -NoPluralize -UseDatabaseNames -ContextDir DbContextEF -Force -Context DbContext_Erp -OutputDir Table -Tables 'MF_IC','TF_IC','PRDT','SALM'"

    //符合這個目前範例專案的 DbFirst ERP DB的MODEL生成指令
    "DbFirstCommand": "Scaffold-DbContext -Connection 'Name=ConnectionStrings:Erp_DB' Microsoft.EntityFrameworkCore.SqlServer -NoPluralize -UseDatabaseNames -ContextDir DbContextEF -Force -Context DbContext_Erp -OutputDir Table -Tables 'INV_ID','INV_NO','MF_ARP','MF_PSS','TF_PSS','TF_PSS_RCV'"
    //只取寬宇需要的table 製令單 表頭表身，製成品送檢 表頭表身，生產繳庫驗收單 表頭表身。
    "DbFirstCommand": "Scaffold-DbContext -Connection 'Server=192.168.6.211,2019;Database=DB_T014;MultipleActiveResultSets=True;TrustServerCertificate=true;user=sa;password=Attn3100;' Microsoft.EntityFrameworkCore.SqlServer -NoPluralize -UseDatabaseNames -ContextDir DbContextEF -Force -Context DbContext_Erp -OutputDir Table -Tables 'MF_MO','TF_MO'"

     //目前需使用到的table 製令單 頭/身，送檢單 頭/身/自定義欄位，繳庫驗收單 頭/身/自定義欄位，不合格原因，
     //192.168.6.211,2019 寬宇測試ERP DB = DB_T014，小心空格或是 指令的前面的-符號不要分開~會無效
     //scaffold-dbcontext "Server=192.168.6.211,2019; Database = DB_T014; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; " Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoPluralize -UseDatabaseNames -Force -Tables 'MF_MO','TF_MO','MF_BOM','TF_TI','MF_TI','TF_TI_Z','MF_TI_Z','MF_TY','TF_TY','MF_TY_Z','SPC_LST'

//寬宇本機測試用ERP DB
//scaffold-dbcontext "Server=localhost; Database = ****; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; " Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoPluralize -UseDatabaseNames -Force -Tables 'MF_MO','TF_MO','MF_BOM'

//寬宇本機正式用ERP DB
//scaffold-dbcontext "Server=localhost; Database = ****; MultipleActiveResultSets = True; TrustServerCertificate = true; user = sa; password = Attn3100; " Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoPluralize -UseDatabaseNames -Force -Tables 'MF_MO','TF_MO','MF_BOM'



// 【PS】DbContext內的「OnConfiguring」方法，要記得刪除或註解，因為該專案會使用DI注入的方式去處理DbContext。



// < !--【客戶端 - 測試】【localhost】-->
//<!--<add name="C_Main_DB" connectionString="data source=localhost;initial catalog=FuWan_ErpPlyShopify_TEST;user id=Sa;password=Attn3100;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
//<add name="ERP_DB" connectionString="metadata=res://*/ERP_DB.csdl|res://*/ERP_DB.ssdl|res://*/ERP_DB.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=DB_TEST;user id=Sa;password=Attn3100;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />-->

//<!--【客戶端-正式】【localhost】-->
//<!--<add name="C_Main_DB" connectionString="data source=localhost;initial catalog=C_FuWan_ErpAndShopifyForConvert_Web;user id=Sa;password=Attn3100;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
//<add name="ERP_DB" connectionString="metadata=res://*/ERP_DB.csdl|res://*/ERP_DB.ssdl|res://*/ERP_DB.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=DB_0000;user id=Sa;password=Attn3100;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />-->