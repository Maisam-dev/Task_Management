using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskCustomerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_taskItems_CustomerId",
                table: "taskItems",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_taskItems_customers_CustomerId",
                table: "taskItems",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_taskItems_customers_CustomerId",
                table: "taskItems");

            migrationBuilder.DropIndex(
                name: "IX_taskItems_CustomerId",
                table: "taskItems");
        }
    }
}