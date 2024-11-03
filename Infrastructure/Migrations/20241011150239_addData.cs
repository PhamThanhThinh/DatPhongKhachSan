using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "Name", "Occupancy", "Price", "SquareMeter", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, "vip pro", "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg", "Khach San 5 Sao", 4, 1000.0, 1000, null },
                    { 2, null, "vip pro", "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg", "Khach San 5 Sao", 4, 1000.0, 1000, null },
                    { 3, null, "vip pro", "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg", "Khach San 5 Sao", 4, 1000.0, 1000, null },
                    { 4, null, "vip pro", "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg", "Khach San 5 Sao", 4, 1000.0, 1000, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
