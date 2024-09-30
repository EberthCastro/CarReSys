using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalSystem.Services.RccAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCarsAndCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CardId", "Brand", "IsAvailable", "Model", "PricePerDay", "Type" },
                values: new object[,]
                {
                    { 1, "Toyota", true, "Corolla", "50", "Small" },
                    { 2, "Honda", true, "Civic", "50", "Small" },
                    { 3, "Ford", true, "Explorer", "150", "SUV" },
                    { 4, "Jeep", true, "Grand Cherokee", "150", "SUV" },
                    { 5, "BMW", true, "X5", "300", "Premium" },
                    { 6, "Audi", true, "A8", "300", "Premium" },
                    { 7, "Mercedes", true, "C-Class", "300", "Premium" },
                    { 8, "Hyundai", true, "Elantra", "50", "Small" },
                    { 9, "Kia", true, "Sorento", "150", "SUV" },
                    { 10, "Lexus", true, "RX", "300", "Premium" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "LoyaltyPoints", "Name" },
                values: new object[,]
                {
                    { 1, 10, "John Doe" },
                    { 2, 20, "Jane Smith" },
                    { 3, 15, "Robert Johnson" },
                    { 4, 25, "Emily Davis" },
                    { 5, 30, "Michael Williams" },
                    { 6, 35, "Sarah Brown" },
                    { 7, 40, "David Jones" },
                    { 8, 50, "Linda Garcia" },
                    { 9, 60, "James Miller" },
                    { 10, 70, "Patricia Martinez" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CardId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10);
        }
    }
}
