using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Management_Api.Migrations
{
    /// <inheritdoc />
    public partial class add_ForeignKey_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_UserId",
                table: "customers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_Users_UserId",
                table: "customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_Users_UserId",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_UserId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "customers");
        }
    }
}