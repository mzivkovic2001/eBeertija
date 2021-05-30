using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace eBeertijaBackend.Migrations
{
    public partial class OstaliModeli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cjenici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DatumPocetkaPrimjene = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cjenici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stolovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    OznakaStola = table.Column<string>(nullable: true),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    Orientation = table.Column<int>(nullable: false),
                    SerijskiBrojUredaja = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stolovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StavkeCjenika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    CjenikId = table.Column<int>(nullable: false),
                    Kategorija = table.Column<int>(nullable: false),
                    CijenaSaPdvom = table.Column<double>(nullable: false),
                    CijenaBezPdva = table.Column<double>(nullable: false),
                    PDV = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeCjenika", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StavkeCjenika_Cjenici_CjenikId",
                        column: x => x.CjenikId,
                        principalTable: "Cjenici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Narudzbe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    UkupnoSaPdvom = table.Column<double>(nullable: false),
                    UkupnoBezPdva = table.Column<double>(nullable: false),
                    PdvIznos = table.Column<double>(nullable: false),
                    Broj = table.Column<int>(nullable: false),
                    StolId = table.Column<int>(nullable: false),
                    IsOstvarenaNaRacunu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzbe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Narudzbe_Stolovi_StolId",
                        column: x => x.StolId,
                        principalTable: "Stolovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Broj = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    NarudzbaId = table.Column<int>(nullable: true),
                    UkupnoSaPdvom = table.Column<double>(nullable: false),
                    UkupnoBezPdva = table.Column<double>(nullable: false),
                    IznosPdv = table.Column<double>(nullable: false),
                    StolId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsStorniran = table.Column<bool>(nullable: false),
                    StorniraniRacunId = table.Column<int>(nullable: true),
                    NazivKorisnika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Racuni_Narudzbe_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzbe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Racuni_Stolovi_StolId",
                        column: x => x.StolId,
                        principalTable: "Stolovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racuni_Racuni_StorniraniRacunId",
                        column: x => x.StorniraniRacunId,
                        principalTable: "Racuni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Racuni_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkeNarudzbe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    NarudzbaId = table.Column<int>(nullable: false),
                    StavkaCjenikaId = table.Column<int>(nullable: false),
                    JedinicnaCijenaStavke = table.Column<double>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false),
                    Ukupno = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeNarudzbe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_Narudzbe_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Narudzbe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeNarudzbe_StavkeCjenika_StavkaCjenikaId",
                        column: x => x.StavkaCjenikaId,
                        principalTable: "StavkeCjenika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkeRacuna",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RacunId = table.Column<int>(nullable: false),
                    StavkaCjenikaId = table.Column<int>(nullable: false),
                    JedinicnaCijenaStavke = table.Column<double>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false),
                    UkupnoSaPdvom = table.Column<double>(nullable: false),
                    UkupnoBezPdva = table.Column<double>(nullable: false),
                    IznosPdv = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeRacuna", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StavkeRacuna_Racuni_RacunId",
                        column: x => x.RacunId,
                        principalTable: "Racuni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeRacuna_StavkeCjenika_StavkaCjenikaId",
                        column: x => x.StavkaCjenikaId,
                        principalTable: "StavkeCjenika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 245, 70, 205, 35, 247, 248, 38, 198, 152, 125, 169, 203, 28, 106, 2, 244, 66, 203, 226, 87, 191, 213, 7, 247, 177, 49, 13, 174, 163, 153, 74, 211, 253, 21, 38, 234, 64, 134, 103, 162, 226, 22, 236, 122, 197, 61, 4, 58, 129, 173, 194, 124, 187, 175, 229, 177, 119, 124, 21, 66, 17, 104, 255, 58 }, new byte[] { 227, 133, 12, 226, 219, 252, 110, 224, 240, 66, 180, 143, 248, 225, 45, 198, 157, 106, 154, 222, 195, 143, 182, 255, 94, 130, 193, 87, 85, 196, 186, 144, 185, 173, 101, 113, 102, 244, 137, 226, 231, 29, 163, 148, 74, 152, 215, 158, 74, 111, 222, 147, 148, 41, 51, 116, 173, 207, 28, 73, 210, 182, 71, 107, 106, 243, 224, 80, 3, 95, 73, 123, 200, 26, 32, 223, 105, 175, 46, 100, 107, 151, 35, 251, 65, 44, 107, 185, 231, 190, 250, 235, 6, 117, 217, 191, 73, 165, 37, 195, 227, 96, 116, 90, 199, 142, 117, 129, 156, 164, 0, 126, 101, 29, 200, 235, 80, 204, 30, 119, 94, 136, 194, 217, 55, 53, 182, 19 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 14, 29, 62, 46, 88, 204, 134, 166, 80, 194, 77, 133, 1, 68, 89, 164, 14, 173, 170, 33, 148, 228, 139, 206, 157, 126, 77, 113, 243, 140, 56, 76, 23, 2, 175, 146, 115, 63, 196, 153, 215, 175, 197, 6, 45, 65, 205, 14, 15, 236, 221, 37, 69, 222, 121, 121, 132, 202, 37, 53, 124, 157, 159, 107 }, new byte[] { 168, 0, 144, 193, 144, 37, 48, 139, 107, 227, 93, 31, 123, 66, 69, 1, 151, 54, 170, 60, 93, 241, 110, 60, 166, 162, 24, 71, 193, 50, 37, 70, 110, 118, 28, 56, 215, 158, 160, 193, 186, 7, 71, 77, 246, 140, 119, 77, 36, 137, 143, 29, 118, 56, 225, 161, 142, 248, 229, 137, 45, 123, 130, 37, 153, 92, 249, 95, 163, 93, 252, 43, 254, 88, 213, 154, 102, 252, 9, 255, 12, 64, 112, 205, 89, 179, 82, 92, 85, 200, 254, 231, 253, 153, 147, 226, 167, 26, 227, 15, 105, 14, 60, 45, 152, 115, 80, 27, 210, 93, 36, 107, 27, 244, 153, 213, 211, 90, 14, 138, 74, 56, 60, 216, 230, 175, 130, 88 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 137, 107, 109, 22, 100, 53, 152, 67, 39, 37, 181, 33, 149, 135, 156, 54, 174, 212, 34, 130, 182, 99, 92, 217, 215, 217, 69, 62, 76, 90, 16, 201, 13, 42, 196, 59, 28, 233, 253, 169, 71, 58, 223, 111, 232, 70, 23, 73, 75, 248, 61, 81, 61, 190, 195, 249, 164, 100, 168, 57, 252, 1, 7, 30 }, new byte[] { 94, 74, 74, 87, 64, 188, 48, 199, 199, 125, 139, 252, 159, 111, 207, 49, 68, 156, 215, 180, 54, 109, 44, 144, 36, 133, 59, 212, 249, 213, 232, 219, 83, 240, 190, 200, 73, 17, 20, 16, 74, 152, 203, 146, 78, 100, 245, 82, 108, 120, 217, 208, 16, 101, 179, 242, 116, 193, 89, 32, 66, 240, 180, 189, 226, 96, 5, 17, 104, 55, 204, 208, 1, 135, 73, 84, 79, 47, 190, 243, 65, 98, 74, 156, 137, 69, 106, 254, 104, 183, 217, 12, 142, 238, 70, 161, 182, 60, 217, 197, 148, 34, 149, 249, 32, 39, 19, 200, 24, 79, 20, 242, 198, 51, 141, 230, 248, 23, 78, 75, 4, 173, 37, 219, 159, 54, 134, 156 } });

            migrationBuilder.CreateIndex(
                name: "IX_Narudzbe_StolId",
                table: "Narudzbe",
                column: "StolId");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_NarudzbaId",
                table: "Racuni",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_StolId",
                table: "Racuni",
                column: "StolId");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_StorniraniRacunId",
                table: "Racuni",
                column: "StorniraniRacunId");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_UserId",
                table: "Racuni",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeCjenika_CjenikId",
                table: "StavkeCjenika",
                column: "CjenikId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_NarudzbaId",
                table: "StavkeNarudzbe",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeNarudzbe_StavkaCjenikaId",
                table: "StavkeNarudzbe",
                column: "StavkaCjenikaId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeRacuna_RacunId",
                table: "StavkeRacuna",
                column: "RacunId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeRacuna_StavkaCjenikaId",
                table: "StavkeRacuna",
                column: "StavkaCjenikaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StavkeNarudzbe");

            migrationBuilder.DropTable(
                name: "StavkeRacuna");

            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "StavkeCjenika");

            migrationBuilder.DropTable(
                name: "Narudzbe");

            migrationBuilder.DropTable(
                name: "Cjenici");

            migrationBuilder.DropTable(
                name: "Stolovi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 250, 190, 187, 249, 13, 218, 121, 24, 232, 42, 59, 239, 34, 171, 107, 43, 211, 124, 14, 22, 228, 228, 222, 56, 79, 227, 235, 182, 61, 224, 210, 245, 243, 236, 215, 118, 49, 87, 29, 122, 23, 234, 235, 50, 128, 106, 253, 83, 47, 44, 170, 77, 171, 191, 189, 133, 212, 6, 108, 40, 3, 241, 129, 81 }, new byte[] { 55, 234, 124, 153, 130, 83, 74, 169, 158, 80, 84, 193, 50, 27, 226, 40, 235, 90, 60, 26, 229, 221, 30, 233, 39, 88, 199, 189, 3, 158, 238, 210, 49, 165, 155, 250, 85, 163, 66, 110, 67, 33, 18, 216, 82, 110, 175, 208, 97, 248, 189, 61, 148, 121, 247, 60, 73, 231, 129, 128, 137, 195, 116, 28, 72, 15, 166, 36, 85, 67, 34, 188, 130, 218, 211, 88, 14, 201, 187, 46, 68, 16, 197, 202, 27, 250, 36, 99, 28, 85, 210, 226, 189, 208, 18, 130, 42, 243, 122, 75, 162, 11, 164, 247, 248, 24, 72, 107, 68, 131, 137, 206, 148, 56, 129, 72, 172, 169, 38, 78, 156, 19, 27, 254, 216, 198, 186, 147 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 89, 135, 40, 217, 174, 221, 247, 167, 13, 185, 20, 232, 122, 204, 145, 74, 99, 25, 76, 8, 116, 48, 167, 204, 13, 35, 154, 88, 108, 191, 229, 127, 72, 63, 169, 14, 186, 210, 112, 89, 122, 172, 83, 65, 119, 208, 212, 3, 253, 23, 119, 222, 58, 61, 16, 118, 31, 164, 243, 184, 92, 193, 27, 78 }, new byte[] { 253, 208, 25, 50, 10, 163, 206, 243, 255, 167, 70, 134, 30, 35, 200, 170, 4, 60, 124, 159, 155, 7, 43, 43, 222, 91, 114, 149, 141, 220, 159, 176, 106, 60, 101, 31, 114, 64, 88, 221, 151, 131, 141, 183, 151, 55, 252, 220, 7, 248, 61, 254, 34, 154, 59, 206, 127, 245, 237, 12, 13, 54, 167, 85, 207, 254, 151, 145, 220, 109, 49, 89, 62, 49, 24, 161, 170, 95, 31, 24, 141, 35, 85, 140, 143, 58, 19, 202, 53, 101, 28, 92, 32, 231, 253, 179, 46, 161, 144, 80, 85, 84, 240, 57, 220, 205, 73, 164, 30, 17, 246, 32, 209, 151, 12, 41, 184, 149, 209, 224, 20, 127, 20, 49, 231, 220, 122, 18 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 122, 193, 68, 72, 243, 185, 192, 123, 122, 225, 108, 170, 22, 24, 229, 249, 133, 100, 231, 71, 171, 180, 49, 49, 158, 186, 74, 192, 35, 3, 46, 77, 88, 49, 149, 36, 252, 245, 214, 76, 27, 37, 157, 25, 194, 152, 169, 148, 253, 245, 40, 10, 207, 151, 52, 191, 187, 84, 127, 35, 142, 202, 11, 209 }, new byte[] { 236, 26, 70, 114, 47, 237, 152, 92, 169, 125, 233, 121, 155, 218, 143, 49, 18, 32, 169, 184, 127, 103, 90, 29, 5, 83, 251, 184, 171, 93, 210, 46, 13, 206, 252, 76, 3, 24, 88, 2, 139, 231, 9, 109, 70, 122, 225, 207, 143, 201, 198, 108, 68, 117, 20, 161, 164, 208, 19, 8, 78, 33, 167, 162, 140, 41, 242, 236, 177, 151, 29, 49, 25, 70, 143, 73, 171, 70, 243, 198, 201, 82, 206, 154, 244, 79, 40, 126, 167, 68, 44, 23, 88, 50, 61, 211, 26, 99, 224, 183, 69, 146, 220, 18, 87, 164, 82, 58, 180, 50, 168, 78, 223, 44, 83, 192, 158, 208, 146, 153, 199, 181, 103, 98, 158, 238, 1, 152 } });
        }
    }
}
