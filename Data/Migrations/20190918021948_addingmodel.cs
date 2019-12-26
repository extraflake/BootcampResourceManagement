using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addingmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    SecurityQuestion = table.Column<string>(nullable: true),
                    SecurityAnswer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_batch",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_batch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_class",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_customer",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Hiring_Location = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_room",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_type",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee_asset",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Asset_Id = table.Column<string>(nullable: true),
                    Employee_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee_asset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_batch_class",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Batch_Id = table.Column<string>(nullable: true),
                    Class_Id = table.Column<int>(nullable: false),
                    Room_Id = table.Column<string>(nullable: true),
                    Trainer_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_batch_class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_batch_class_tb_m_batch_Batch_Id",
                        column: x => x.Batch_Id,
                        principalTable: "tb_m_batch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_batch_class_tb_m_class_Class_Id",
                        column: x => x.Class_Id,
                        principalTable: "tb_m_class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_batch_class_tb_m_room_Room_Id",
                        column: x => x.Room_Id,
                        principalTable: "tb_m_room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_participant",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    Batch_Class = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_participant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_participant_tb_m_batch_class_Batch_Class",
                        column: x => x.Batch_Class,
                        principalTable: "tb_m_batch_class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_asset",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Type_Id = table.Column<string>(nullable: true),
                    Employee_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_asset_tb_m_participant_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "tb_m_participant",
                        principalColumn: "Id",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_asset_tb_m_type_Type_Id",
                        column: x => x.Type_Id,
                        principalTable: "tb_m_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_placement",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RelationName = table.Column<string>(nullable: true),
                    RelationPhone = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Employee_Id = table.Column<string>(nullable: true),
                    Customer_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_placement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_placement_tb_m_customer_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "tb_m_customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_placement_tb_m_participant_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "tb_m_participant",
                        principalColumn: "Id",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_interviewhistory",
                columns: table => new
                {
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InterviewDate = table.Column<DateTime>(nullable: true),
                    InterviewTime = table.Column<string>(nullable: true),
                    Interviewer = table.Column<string>(nullable: true),
                    Customer_Id = table.Column<string>(nullable: true),
                    Employee_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_interviewhistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_t_interviewhistory_tb_m_customer_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "tb_m_customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_t_interviewhistory_tb_m_participant_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "tb_m_participant",
                        principalColumn: "Id",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_asset_Employee_Id",
                table: "tb_m_asset",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_asset_Type_Id",
                table: "tb_m_asset",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_batch_class_Batch_Id",
                table: "tb_m_batch_class",
                column: "Batch_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_batch_class_Class_Id",
                table: "tb_m_batch_class",
                column: "Class_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_batch_class_Room_Id",
                table: "tb_m_batch_class",
                column: "Room_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_batch_class_Trainer_Id",
                table: "tb_m_batch_class",
                column: "Trainer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_asset_Asset_Id",
                table: "tb_m_employee_asset",
                column: "Asset_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_asset_Employee_Id",
                table: "tb_m_employee_asset",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_participant_Batch_Class",
                table: "tb_m_participant",
                column: "Batch_Class");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_placement_Customer_Id",
                table: "tb_m_placement",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_placement_Employee_Id",
                table: "tb_m_placement",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_interviewhistory_Customer_Id",
                table: "tb_t_interviewhistory",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_interviewhistory_Employee_Id",
                table: "tb_t_interviewhistory",
                column: "Employee_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_asset_tb_m_participant_Employee_Id",
                table: "tb_m_employee_asset",
                column: "Employee_Id",
                principalTable: "tb_m_participant",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_asset_tb_m_asset_Asset_Id",
                table: "tb_m_employee_asset",
                column: "Asset_Id",
                principalTable: "tb_m_asset",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_batch_class_tb_m_participant_Trainer_Id",
                table: "tb_m_batch_class",
                column: "Trainer_Id",
                principalTable: "tb_m_participant",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_batch_class_tb_m_participant_Trainer_Id",
                table: "tb_m_batch_class");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_employee_asset");

            migrationBuilder.DropTable(
                name: "tb_m_placement");

            migrationBuilder.DropTable(
                name: "tb_t_interviewhistory");

            migrationBuilder.DropTable(
                name: "tb_m_asset");

            migrationBuilder.DropTable(
                name: "tb_m_customer");

            migrationBuilder.DropTable(
                name: "tb_m_type");

            migrationBuilder.DropTable(
                name: "tb_m_participant");

            migrationBuilder.DropTable(
                name: "tb_m_batch_class");

            migrationBuilder.DropTable(
                name: "tb_m_batch");

            migrationBuilder.DropTable(
                name: "tb_m_class");

            migrationBuilder.DropTable(
                name: "tb_m_room");
        }
    }
}
