using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionСompany.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brigades_Orders_OrderId",
                table: "Brigades");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfJobs_Orders_OrderId",
                table: "TypeOfJobs");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfJobs_OrderId",
                table: "TypeOfJobs");

            migrationBuilder.DropIndex(
                name: "IX_Brigades_OrderId",
                table: "Brigades");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "TypeOfJobs");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Brigades");

            migrationBuilder.AddColumn<int>(
                name: "BrigadeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfJobId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfJobId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BrigadeId",
                table: "Orders",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TypeOfJobId",
                table: "Orders",
                column: "TypeOfJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TypeOfJobId",
                table: "Materials",
                column: "TypeOfJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_TypeOfJobs_TypeOfJobId",
                table: "Materials",
                column: "TypeOfJobId",
                principalTable: "TypeOfJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Brigades_BrigadeId",
                table: "Orders",
                column: "BrigadeId",
                principalTable: "Brigades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TypeOfJobs_TypeOfJobId",
                table: "Orders",
                column: "TypeOfJobId",
                principalTable: "TypeOfJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_TypeOfJobs_TypeOfJobId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Brigades_BrigadeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TypeOfJobs_TypeOfJobId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BrigadeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TypeOfJobId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Materials_TypeOfJobId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "BrigadeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TypeOfJobId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "TypeOfJobId",
                table: "Materials");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "TypeOfJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Brigades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfJobs_OrderId",
                table: "TypeOfJobs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Brigades_OrderId",
                table: "Brigades",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brigades_Orders_OrderId",
                table: "Brigades",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfJobs_Orders_OrderId",
                table: "TypeOfJobs",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
