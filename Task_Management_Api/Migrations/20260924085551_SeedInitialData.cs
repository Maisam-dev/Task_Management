using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_Management_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@mail.com", "Admin", "admin", "Admin" },
                    { 2, "alex@mail.com", "Alex", "alex123", "User" },
                    { 3, "Ana@mail.com", "Ana", "ana123", "User" }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Id", "Email", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "contact@techcorp.de", "TechCorp Solutions GmbH", 3 },
                    { 2, "support@logisticsexpress.com", "Logistics Express AG", 2 },
                    { 3, "info@apexfinancial.com", "Apex Financial Group", 2 }
                });

            migrationBuilder.InsertData(
                table: "taskItems",
                columns: new[] { "Id", "CustomerId", "Description", "IsCompleted", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Perform a security and performance check on cloud servers.", false, 2, "System Infrastructure Audit" },
                    { 2, 2, "Integrate shipment tracking API with the main dashboard.", true, 3, "API Integration Setup" },
                    { 3, 2, "Migrate legacy SQL database to PostgreSQL cluster.", false, 1, "Database Migration" },
                    { 4, 2, "Update dependencies and apply server patches.", false, 1, "Quarterly Maintenance" },
                    { 5, 3, "Implement export to PDF/Excel for monthly transactions.", true, 3, "Financial Report Module" },
                    { 6, 3, "Restrict access to sensitive financial records based on claims.", false, 2, "User Role Authorization" },
                    { 7, 3, "Test Stripe integration in sandbox environment.", false, 1, "Payment Gateway Testing" },
                    { 8, 3, "Conduct initial technical meeting for new portal usage.", true, 3, "Client Onboarding Review" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "taskItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
