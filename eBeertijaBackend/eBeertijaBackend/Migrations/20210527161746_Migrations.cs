using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eBeertijaBackend.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(type: "varchar", maxLength: 30, nullable: true),
                    FullName = table.Column<string>(type: "varchar", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    Vrsta = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    OIB = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "FullName", "IsActive", "OIB", "PasswordHash", "PasswordSalt", "UpdatedAt", "UpdatedBy", "Username", "Vrsta" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "gjerkovic@veleri.hr", "Gabrijela Jerković", true, null, new byte[] { 250, 190, 187, 249, 13, 218, 121, 24, 232, 42, 59, 239, 34, 171, 107, 43, 211, 124, 14, 22, 228, 228, 222, 56, 79, 227, 235, 182, 61, 224, 210, 245, 243, 236, 215, 118, 49, 87, 29, 122, 23, 234, 235, 50, 128, 106, 253, 83, 47, 44, 170, 77, 171, 191, 189, 133, 212, 6, 108, 40, 3, 241, 129, 81 }, new byte[] { 55, 234, 124, 153, 130, 83, 74, 169, 158, 80, 84, 193, 50, 27, 226, 40, 235, 90, 60, 26, 229, 221, 30, 233, 39, 88, 199, 189, 3, 158, 238, 210, 49, 165, 155, 250, 85, 163, 66, 110, 67, 33, 18, 216, 82, 110, 175, 208, 97, 248, 189, 61, 148, 121, 247, 60, 73, 231, 129, 128, 137, 195, 116, 28, 72, 15, 166, 36, 85, 67, 34, 188, 130, 218, 211, 88, 14, 201, 187, 46, 68, 16, 197, 202, 27, 250, 36, 99, 28, 85, 210, 226, 189, 208, 18, 130, 42, 243, 122, 75, 162, 11, 164, 247, 248, 24, 72, 107, 68, 131, 137, 206, 148, 56, 129, 72, 172, 169, 38, 78, 156, 19, 27, 254, 216, 198, 186, 147 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "gabrijela", 1 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "mzivkovic@veleri.hr", "Marin Živković", true, null, new byte[] { 122, 193, 68, 72, 243, 185, 192, 123, 122, 225, 108, 170, 22, 24, 229, 249, 133, 100, 231, 71, 171, 180, 49, 49, 158, 186, 74, 192, 35, 3, 46, 77, 88, 49, 149, 36, 252, 245, 214, 76, 27, 37, 157, 25, 194, 152, 169, 148, 253, 245, 40, 10, 207, 151, 52, 191, 187, 84, 127, 35, 142, 202, 11, 209 }, new byte[] { 236, 26, 70, 114, 47, 237, 152, 92, 169, 125, 233, 121, 155, 218, 143, 49, 18, 32, 169, 184, 127, 103, 90, 29, 5, 83, 251, 184, 171, 93, 210, 46, 13, 206, 252, 76, 3, 24, 88, 2, 139, 231, 9, 109, 70, 122, 225, 207, 143, 201, 198, 108, 68, 117, 20, 161, 164, 208, 19, 8, 78, 33, 167, 162, 140, 41, 242, 236, 177, 151, 29, 49, 25, 70, 143, 73, 171, 70, 243, 198, 201, 82, 206, 154, 244, 79, 40, 126, 167, 68, 44, 23, 88, 50, 61, 211, 26, 99, 224, 183, 69, 146, 220, 18, 87, 164, 82, 58, 180, 50, 168, 78, 223, 44, 83, 192, 158, 208, 146, 153, 199, 181, 103, 98, 158, 238, 1, 152 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "marin", 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "erabar@veleri.hr", "Eni Rabar", true, null, new byte[] { 89, 135, 40, 217, 174, 221, 247, 167, 13, 185, 20, 232, 122, 204, 145, 74, 99, 25, 76, 8, 116, 48, 167, 204, 13, 35, 154, 88, 108, 191, 229, 127, 72, 63, 169, 14, 186, 210, 112, 89, 122, 172, 83, 65, 119, 208, 212, 3, 253, 23, 119, 222, 58, 61, 16, 118, 31, 164, 243, 184, 92, 193, 27, 78 }, new byte[] { 253, 208, 25, 50, 10, 163, 206, 243, 255, 167, 70, 134, 30, 35, 200, 170, 4, 60, 124, 159, 155, 7, 43, 43, 222, 91, 114, 149, 141, 220, 159, 176, 106, 60, 101, 31, 114, 64, 88, 221, 151, 131, 141, 183, 151, 55, 252, 220, 7, 248, 61, 254, 34, 154, 59, 206, 127, 245, 237, 12, 13, 54, 167, 85, 207, 254, 151, 145, 220, 109, 49, 89, 62, 49, 24, 161, 170, 95, 31, 24, 141, 35, 85, 140, 143, 58, 19, 202, 53, 101, 28, 92, 32, 231, 253, 179, 46, 161, 144, 80, 85, 84, 240, 57, 220, 205, 73, 164, 30, 17, 246, 32, 209, 151, 12, 41, 184, 149, 209, 224, 20, 127, 20, 49, 231, 220, 122, 18 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "eni", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
