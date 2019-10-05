using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepairServiceCenterASP.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Money = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "RepairedModels",
                columns: table => new
                {
                    RepairedModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    TechSpecification = table.Column<string>(nullable: true),
                    Features = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairedModels", x => x.RepairedModelId);
                });

            migrationBuilder.CreateTable(
                name: "ServicedStores",
                columns: table => new
                {
                    ServicedStoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicedStores", x => x.ServicedStoreId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(nullable: true),
                    Experience = table.Column<int>(nullable: true),
                    PostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfFaults",
                columns: table => new
                {
                    TypeOfFaultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RepairedModelId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MethodRepair = table.Column<string>(nullable: true),
                    WorkPrice = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfFaults", x => x.TypeOfFaultId);
                    table.ForeignKey(
                        name: "FK_TypeOfFaults_RepairedModels_RepairedModelId",
                        column: x => x.RepairedModelId,
                        principalTable: "RepairedModels",
                        principalColumn: "RepairedModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOrder = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    FullNameCustumer = table.Column<string>(nullable: true),
                    RepairedModelId = table.Column<int>(nullable: true),
                    TypeOfFaultId = table.Column<int>(nullable: true),
                    ServicedStoreId = table.Column<int>(nullable: true),
                    GuaranteeMark = table.Column<bool>(nullable: true),
                    GuaranteePeriod = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_RepairedModels_RepairedModelId",
                        column: x => x.RepairedModelId,
                        principalTable: "RepairedModels",
                        principalColumn: "RepairedModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ServicedStores_ServicedStoreId",
                        column: x => x.ServicedStoreId,
                        principalTable: "ServicedStores",
                        principalColumn: "ServicedStoreId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_TypeOfFaults_TypeOfFaultId",
                        column: x => x.TypeOfFaultId,
                        principalTable: "TypeOfFaults",
                        principalColumn: "TypeOfFaultId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    SparePartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Functions = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    RepairedModelId = table.Column<int>(nullable: true),
                    TypeOfFaultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.SparePartId);
                    table.ForeignKey(
                        name: "FK_SpareParts_RepairedModels_RepairedModelId",
                        column: x => x.RepairedModelId,
                        principalTable: "RepairedModels",
                        principalColumn: "RepairedModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpareParts_TypeOfFaults_TypeOfFaultId",
                        column: x => x.TypeOfFaultId,
                        principalTable: "TypeOfFaults",
                        principalColumn: "TypeOfFaultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PostId",
                table: "Employees",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RepairedModelId",
                table: "Orders",
                column: "RepairedModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServicedStoreId",
                table: "Orders",
                column: "ServicedStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TypeOfFaultId",
                table: "Orders",
                column: "TypeOfFaultId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_RepairedModelId",
                table: "SpareParts",
                column: "RepairedModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_TypeOfFaultId",
                table: "SpareParts",
                column: "TypeOfFaultId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfFaults_RepairedModelId",
                table: "TypeOfFaults",
                column: "RepairedModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ServicedStores");

            migrationBuilder.DropTable(
                name: "TypeOfFaults");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "RepairedModels");
        }
    }
}
