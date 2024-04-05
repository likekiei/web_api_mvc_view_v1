using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main_EF.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_TestTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    No = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLevelId = table.Column<int>(type: "int", nullable: false),
                    CompanyLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnifiedNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibMan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactMan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactManJobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactManPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefaultSelected = table.Column<bool>(type: "bit", nullable: false),
                    IsStop = table.Column<bool>(type: "bit", nullable: false),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateManId = table.Column<long>(type: "bigint", nullable: true),
                    CreateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateManId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutsideBindKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBindGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    StatusCodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    StatusCodeId = table.Column<int>(type: "int", nullable: false),
                    StatusCodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogTypeId = table.Column<int>(type: "int", nullable: false),
                    LogTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageException = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageOther = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBTableId = table.Column<int>(type: "int", nullable: false),
                    DBTableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableKey_Main = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEQ = table.Column<int>(type: "int", nullable: false),
                    BindKey = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BindKey_ByAction = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UserNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBackDoor = table.Column<bool>(type: "bit", nullable: false),
                    BackDoorTypeId = table.Column<int>(type: "int", nullable: false),
                    BackDoorTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNeedCheckPassword = table.Column<bool>(type: "bit", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginKeepDay = table.Column<int>(type: "int", nullable: true),
                    RequestLastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginFromTypeId = table.Column<int>(type: "int", nullable: false),
                    LoginFromTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLevelId = table.Column<int>(type: "int", nullable: false),
                    CompanyLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    PermissionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionId_TXTs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleLog",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    No = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    PermissionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStop = table.Column<bool>(type: "bit", nullable: false),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateManId = table.Column<long>(type: "bigint", nullable: true),
                    CreateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateManId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutsideBindKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBindGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemTimestamp",
                columns: table => new
                {
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    DailyStockDetailReport_FirstChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTimestamp", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_SystemTimestamp_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunctionCode",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    FunctionCodeId = table.Column<int>(type: "int", nullable: false),
                    FunctionCodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsStop = table.Column<bool>(type: "bit", nullable: false),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateManId = table.Column<long>(type: "bigint", nullable: true),
                    CreateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateManId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutsideBindKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBindGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FunctionCode_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FunctionCode_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    No = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DeptId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStop = table.Column<bool>(type: "bit", nullable: false),
                    Rem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateManId = table.Column<long>(type: "bigint", nullable: true),
                    CreateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateManId = table.Column<long>(type: "bigint", nullable: true),
                    UpdateManName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutsideBindKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBindGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "Index_1",
                table: "_TestTemplate",
                columns: new[] { "No", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_001",
                table: "Companys",
                column: "GUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_002",
                table: "Companys",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_001",
                table: "FunctionCode",
                column: "GUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FunctionCode_CompanyId",
                table: "FunctionCode",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionCode_RoleId",
                table: "FunctionCode",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_001",
                table: "Role",
                column: "GUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_002",
                table: "Role",
                columns: new[] { "CompanyId", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_001",
                table: "User",
                column: "GUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_002",
                table: "User",
                columns: new[] { "CompanyId", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_TestTemplate");

            migrationBuilder.DropTable(
                name: "ErrorLog");

            migrationBuilder.DropTable(
                name: "FunctionCode");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "LoginStatus");

            migrationBuilder.DropTable(
                name: "SimpleLog");

            migrationBuilder.DropTable(
                name: "SystemTimestamp");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Companys");
        }
    }
}
