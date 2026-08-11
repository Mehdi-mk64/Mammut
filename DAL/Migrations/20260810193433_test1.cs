using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.EnsureSchema(
                name: "HR");

            migrationBuilder.EnsureSchema(
                name: "Service");

            migrationBuilder.EnsureSchema(
                name: "SMS");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodeGroup = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SendImportance",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendImportance", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SendStatus",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SMSProvider",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APIKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhonSender = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MethodSendUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSProvider", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentUnitID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Unit_Unit_ParentUnitID",
                        column: x => x.ParentUnitID,
                        principalSchema: "HR",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ViewList",
                schema: "Service",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchemaName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ViewName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ViewModelMessages",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumTrySendSMS = table.Column<int>(type: "int", nullable: false),
                    DateSend = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeSend = table.Column<TimeSpan>(type: "time", nullable: true),
                    SmsProviderTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSMSenderTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Importance = table.Column<bool>(type: "bit", nullable: false),
                    OnlyGSMSend = table.Column<bool>(type: "bit", nullable: false),
                    MessageSendID = table.Column<long>(type: "bigint", nullable: true),
                    AddAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    IsComlpete = table.Column<bool>(type: "bit", nullable: false),
                    HasError = table.Column<bool>(type: "bit", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewModelMessages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GenderID = table.Column<byte>(type: "tinyint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Person_Gender_ID",
                        column: x => x.GenderID,
                        principalSchema: "HR",
                        principalTable: "Gender",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsertDataLog",
                schema: "Service",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Descpription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContRecord = table.Column<long>(type: "bigint", nullable: false),
                    ViewListID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsertDataLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ViewList_InsertDataLog_ID",
                        column: x => x.ViewListID,
                        principalSchema: "Service",
                        principalTable: "ViewList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Person_PersonID",
                        column: x => x.PersonID,
                        principalSchema: "HR",
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonGroup",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(type: "bigint", nullable: false),
                    GroupID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonGroup_Group_ID",
                        column: x => x.GroupID,
                        principalSchema: "HR",
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonGroup_Person_ID",
                        column: x => x.PersonID,
                        principalSchema: "HR",
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPost",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(type: "bigint", nullable: false),
                    PostID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonPost_Person_ID",
                        column: x => x.PersonID,
                        principalSchema: "HR",
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPost_Post_ID",
                        column: x => x.PostID,
                        principalSchema: "HR",
                        principalTable: "Post",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonUnit",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(type: "bigint", nullable: false),
                    UnitID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonUnit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonUnit_Person_ID",
                        column: x => x.PersonID,
                        principalSchema: "HR",
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonUnit_Unit_ID",
                        column: x => x.UnitID,
                        principalSchema: "HR",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhonNumbers",
                schema: "HR",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nummber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhonNumbers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Person_ID",
                        column: x => x.PersonID,
                        principalSchema: "HR",
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccesseGroup",
                schema: "Security",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesseGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccesseGroup_Group_ID",
                        column: x => x.GroupID,
                        principalSchema: "HR",
                        principalTable: "Group",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccesseGroup_User_ID",
                        column: x => x.UserID,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageSend",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumberID = table.Column<long>(type: "bigint", nullable: false),
                    InsertDateTime = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "getdate()"),
                    MaximumTrySendSMS = table.Column<int>(type: "int", nullable: false),
                    DateTimeSend = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "getdate()"),
                    SmsProviderID = table.Column<int>(type: "int", nullable: true),
                    SendImportanceID = table.Column<byte>(type: "tinyint", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSend", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MessageSend_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSend_PhoneNumber_ID",
                        column: x => x.PhoneNumberID,
                        principalSchema: "HR",
                        principalTable: "PhonNumbers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageSend_SendImportance_ID",
                        column: x => x.SendImportanceID,
                        principalSchema: "SMS",
                        principalTable: "SendImportance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageSend_SMSProvider_ID",
                        column: x => x.SmsProviderID,
                        principalSchema: "SMS",
                        principalTable: "SMSProvider",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageLog",
                schema: "SMS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageSendID = table.Column<long>(type: "bigint", nullable: false),
                    ActionDateTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    SendStatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    StatusCodeReturn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MessageLog_MessageSend_ID",
                        column: x => x.MessageSendID,
                        principalSchema: "SMS",
                        principalTable: "MessageSend",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageLog_SendStatus_ID",
                        column: x => x.SendStatusID,
                        principalSchema: "SMS",
                        principalTable: "SendStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "HR",
                table: "Gender",
                columns: new[] { "ID", "Title" },
                values: new object[,]
                {
                    { (byte)1, "خانم" },
                    { (byte)2, "آقا" }
                });

            migrationBuilder.InsertData(
                schema: "SMS",
                table: "SendImportance",
                columns: new[] { "ID", "Title" },
                values: new object[,]
                {
                    { (byte)1, "مهم" },
                    { (byte)2, "معمولی" }
                });

            migrationBuilder.InsertData(
                schema: "SMS",
                table: "SendStatus",
                columns: new[] { "ID", "Title" },
                values: new object[,]
                {
                    { (byte)1, "پیامک جدید" },
                    { (byte)2, "ارسال مجدد" },
                    { (byte)3, "ارسال موفق با API" },
                    { (byte)4, "عدم ارسال" }
                });

            migrationBuilder.InsertData(
                schema: "HR",
                table: "Person",
                columns: new[] { "ID", "FirstName", "GenderID", "IsActive", "LastName", "PersonCode" },
                values: new object[] { 1L, "ناشتاس", (byte)2, true, "ناشناس", "0" });

            migrationBuilder.CreateIndex(
                name: "IX_AccesseGroup_GroupID",
                schema: "Security",
                table: "AccesseGroup",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_AccesseGroup_UserID_GroupID",
                schema: "Security",
                table: "AccesseGroup",
                columns: new[] { "UserID", "GroupID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "ApplicationUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_PersonID",
                schema: "Security",
                table: "ApplicationUser",
                column: "PersonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "ApplicationUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UK_Gender_Title",
                schema: "HR",
                table: "Gender",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Group_Title",
                schema: "HR",
                table: "Group",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsertDataLog_ViewListID",
                schema: "Service",
                table: "InsertDataLog",
                column: "ViewListID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageLog_MessageSendID",
                schema: "SMS",
                table: "MessageLog",
                column: "MessageSendID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageLog_SendStatusID",
                schema: "SMS",
                table: "MessageLog",
                column: "SendStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSend_ApplicationUserId",
                schema: "SMS",
                table: "MessageSend",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSend_PhoneNumberID",
                schema: "SMS",
                table: "MessageSend",
                column: "PhoneNumberID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSend_SendImportanceID",
                schema: "SMS",
                table: "MessageSend",
                column: "SendImportanceID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSend_SmsProviderID",
                schema: "SMS",
                table: "MessageSend",
                column: "SmsProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderID",
                schema: "HR",
                table: "Person",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "UK_Person_PersonCode",
                schema: "HR",
                table: "Person",
                column: "PersonCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroup_GroupID",
                schema: "HR",
                table: "PersonGroup",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroup_PersonID_GroupID",
                schema: "HR",
                table: "PersonGroup",
                columns: new[] { "PersonID", "GroupID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonPost_PersonID_PostID",
                schema: "HR",
                table: "PersonPost",
                columns: new[] { "PersonID", "PostID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonPost_PostID",
                schema: "HR",
                table: "PersonPost",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonUnit_PersonID_UnitID",
                schema: "HR",
                table: "PersonUnit",
                columns: new[] { "PersonID", "UnitID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonUnit_UnitID",
                schema: "HR",
                table: "PersonUnit",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_PhonNumbers_PersonID",
                schema: "HR",
                table: "PhonNumbers",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "UK_Post_Title",
                schema: "HR",
                table: "Post",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Gender_Title",
                schema: "SMS",
                table: "SendImportance",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_SendStatus_Title",
                schema: "SMS",
                table: "SendStatus",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_SmsProvider_PhonSender",
                schema: "SMS",
                table: "SMSProvider",
                column: "PhonSender",
                unique: true,
                filter: "[PhonSender] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UK_SmsProvidor_Title",
                schema: "SMS",
                table: "SMSProvider",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParentUnitID",
                schema: "HR",
                table: "Unit",
                column: "ParentUnitID");

            migrationBuilder.CreateIndex(
                name: "UK_Unit_Code",
                schema: "HR",
                table: "Unit",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Unit_Title",
                schema: "HR",
                table: "Unit",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_ViweList_View",
                schema: "Service",
                table: "ViewList",
                columns: new[] { "SchemaName", "ViewName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesseGroup",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InsertDataLog",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "MessageLog",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "PersonGroup",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "PersonPost",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "PersonUnit",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "ViewModelMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ViewList",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "MessageSend",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SendStatus",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Unit",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "ApplicationUser",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "PhonNumbers",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "SendImportance",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "SMSProvider",
                schema: "SMS");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "HR");
        }
    }
}
