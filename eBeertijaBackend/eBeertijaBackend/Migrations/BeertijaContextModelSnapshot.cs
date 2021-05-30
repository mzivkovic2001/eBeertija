﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ebeertijaBackend.DatabaseContext;

namespace eBeertijaBackend.Migrations
{
    [DbContext(typeof(BeertijaContext))]
    partial class BeertijaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ebeertijaBackend.Models.Cjenik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<DateTime>("DatumPocetkaPrimjene")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Cjenici");
                });

            modelBuilder.Entity("ebeertijaBackend.Models.Narudzba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Broj")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOstvarenaNaRacunu")
                        .HasColumnType("boolean");

                    b.Property<double>("PdvIznos")
                        .HasColumnType("double precision");

                    b.Property<int>("StolId")
                        .HasColumnType("integer");

                    b.Property<double>("UkupnoBezPdva")
                        .HasColumnType("double precision");

                    b.Property<double>("UkupnoSaPdvom")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("StolId");

                    b.ToTable("Narudzbe");
                });

            modelBuilder.Entity("ebeertijaBackend.Models.Racun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Broj")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStorniran")
                        .HasColumnType("boolean");

                    b.Property<double>("IznosPdv")
                        .HasColumnType("double precision");

                    b.Property<int?>("NarudzbaId")
                        .HasColumnType("integer");

                    b.Property<string>("NazivKorisnika")
                        .HasColumnType("text");

                    b.Property<int>("StolId")
                        .HasColumnType("integer");

                    b.Property<int?>("StorniraniRacunId")
                        .HasColumnType("integer");

                    b.Property<double>("UkupnoBezPdva")
                        .HasColumnType("double precision");

                    b.Property<double>("UkupnoSaPdvom")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NarudzbaId");

                    b.HasIndex("StolId");

                    b.HasIndex("StorniraniRacunId");

                    b.HasIndex("UserId");

                    b.ToTable("Racuni");
                });

            modelBuilder.Entity("ebeertijaBackend.Models.StavkaCjenika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("CijenaBezPdva")
                        .HasColumnType("double precision");

                    b.Property<double>("CijenaSaPdvom")
                        .HasColumnType("double precision");

                    b.Property<int>("CjenikId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("Kategorija")
                        .HasColumnType("integer");

                    b.Property<string>("Naziv")
                        .HasColumnType("text");

                    b.Property<string>("Opis")
                        .HasColumnType("text");

                    b.Property<double>("PDV")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("CjenikId");

                    b.ToTable("StavkeCjenika");
                });

            modelBuilder.Entity("ebeertijaBackend.Models.StavkaNarudzbe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<double>("JedinicnaCijenaStavke")
                        .HasColumnType("double precision");

                    b.Property<int>("Kolicina")
                        .HasColumnType("integer");

                    b.Property<int>("NarudzbaId")
                        .HasColumnType("integer");

                    b.Property<int>("StavkaCjenikaId")
                        .HasColumnType("integer");

                    b.Property<double>("Ukupno")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("NarudzbaId");

                    b.HasIndex("StavkaCjenikaId");

                    b.ToTable("StavkeNarudzbe");
                });

            modelBuilder.Entity("ebeertijaBackend.Models.StavkaRacuna", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<double>("IznosPdv")
                        .HasColumnType("double precision");

                    b.Property<double>("JedinicnaCijenaStavke")
                        .HasColumnType("double precision");

                    b.Property<int>("Kolicina")
                        .HasColumnType("integer");

                    b.Property<int>("RacunId")
                        .HasColumnType("integer");

                    b.Property<int>("StavkaCjenikaId")
                        .HasColumnType("integer");

                    b.Property<double>("UkupnoBezPdva")
                        .HasColumnType("double precision");

                    b.Property<double>("UkupnoSaPdvom")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("RacunId");

                    b.HasIndex("StavkaCjenikaId");

                    b.ToTable("StavkeRacuna");
                });

            modelBuilder.Entity("ebeertijaBackend.Models.Stol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("Orientation")
                        .HasColumnType("integer");

                    b.Property<string>("OznakaStola")
                        .HasColumnType("text");

                    b.Property<string>("SerijskiBrojUredaja")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Stolovi");
                });

            modelBuilder.Entity("ebeertijaBackend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<string>("Email")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<string>("FullName")
                        .HasColumnType("varchar")
                        .HasMaxLength(20);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("OIB")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar")
                        .HasMaxLength(250);

                    b.Property<string>("Username")
                        .HasColumnType("varchar")
                        .HasMaxLength(30);

                    b.Property<int>("Vrsta")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "gjerkovic@veleri.hr",
                            FullName = "Gabrijela Jerković",
                            IsActive = true,
                            PasswordHash = new byte[] { 245, 70, 205, 35, 247, 248, 38, 198, 152, 125, 169, 203, 28, 106, 2, 244, 66, 203, 226, 87, 191, 213, 7, 247, 177, 49, 13, 174, 163, 153, 74, 211, 253, 21, 38, 234, 64, 134, 103, 162, 226, 22, 236, 122, 197, 61, 4, 58, 129, 173, 194, 124, 187, 175, 229, 177, 119, 124, 21, 66, 17, 104, 255, 58 },
                            PasswordSalt = new byte[] { 227, 133, 12, 226, 219, 252, 110, 224, 240, 66, 180, 143, 248, 225, 45, 198, 157, 106, 154, 222, 195, 143, 182, 255, 94, 130, 193, 87, 85, 196, 186, 144, 185, 173, 101, 113, 102, 244, 137, 226, 231, 29, 163, 148, 74, 152, 215, 158, 74, 111, 222, 147, 148, 41, 51, 116, 173, 207, 28, 73, 210, 182, 71, 107, 106, 243, 224, 80, 3, 95, 73, 123, 200, 26, 32, 223, 105, 175, 46, 100, 107, 151, 35, 251, 65, 44, 107, 185, 231, 190, 250, 235, 6, 117, 217, 191, 73, 165, 37, 195, 227, 96, 116, 90, 199, 142, 117, 129, 156, 164, 0, 126, 101, 29, 200, 235, 80, 204, 30, 119, 94, 136, 194, 217, 55, 53, 182, 19 },
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "gabrijela",
                            Vrsta = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mzivkovic@veleri.hr",
                            FullName = "Marin Živković",
                            IsActive = true,
                            PasswordHash = new byte[] { 137, 107, 109, 22, 100, 53, 152, 67, 39, 37, 181, 33, 149, 135, 156, 54, 174, 212, 34, 130, 182, 99, 92, 217, 215, 217, 69, 62, 76, 90, 16, 201, 13, 42, 196, 59, 28, 233, 253, 169, 71, 58, 223, 111, 232, 70, 23, 73, 75, 248, 61, 81, 61, 190, 195, 249, 164, 100, 168, 57, 252, 1, 7, 30 },
                            PasswordSalt = new byte[] { 94, 74, 74, 87, 64, 188, 48, 199, 199, 125, 139, 252, 159, 111, 207, 49, 68, 156, 215, 180, 54, 109, 44, 144, 36, 133, 59, 212, 249, 213, 232, 219, 83, 240, 190, 200, 73, 17, 20, 16, 74, 152, 203, 146, 78, 100, 245, 82, 108, 120, 217, 208, 16, 101, 179, 242, 116, 193, 89, 32, 66, 240, 180, 189, 226, 96, 5, 17, 104, 55, 204, 208, 1, 135, 73, 84, 79, 47, 190, 243, 65, 98, 74, 156, 137, 69, 106, 254, 104, 183, 217, 12, 142, 238, 70, 161, 182, 60, 217, 197, 148, 34, 149, 249, 32, 39, 19, 200, 24, 79, 20, 242, 198, 51, 141, 230, 248, 23, 78, 75, 4, 173, 37, 219, 159, 54, 134, 156 },
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "marin",
                            Vrsta = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "erabar@veleri.hr",
                            FullName = "Eni Rabar",
                            IsActive = true,
                            PasswordHash = new byte[] { 14, 29, 62, 46, 88, 204, 134, 166, 80, 194, 77, 133, 1, 68, 89, 164, 14, 173, 170, 33, 148, 228, 139, 206, 157, 126, 77, 113, 243, 140, 56, 76, 23, 2, 175, 146, 115, 63, 196, 153, 215, 175, 197, 6, 45, 65, 205, 14, 15, 236, 221, 37, 69, 222, 121, 121, 132, 202, 37, 53, 124, 157, 159, 107 },
                            PasswordSalt = new byte[] { 168, 0, 144, 193, 144, 37, 48, 139, 107, 227, 93, 31, 123, 66, 69, 1, 151, 54, 170, 60, 93, 241, 110, 60, 166, 162, 24, 71, 193, 50, 37, 70, 110, 118, 28, 56, 215, 158, 160, 193, 186, 7, 71, 77, 246, 140, 119, 77, 36, 137, 143, 29, 118, 56, 225, 161, 142, 248, 229, 137, 45, 123, 130, 37, 153, 92, 249, 95, 163, 93, 252, 43, 254, 88, 213, 154, 102, 252, 9, 255, 12, 64, 112, 205, 89, 179, 82, 92, 85, 200, 254, 231, 253, 153, 147, 226, 167, 26, 227, 15, 105, 14, 60, 45, 152, 115, 80, 27, 210, 93, 36, 107, 27, 244, 153, 213, 211, 90, 14, 138, 74, 56, 60, 216, 230, 175, 130, 88 },
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "eni",
                            Vrsta = 1
                        });
                });

            modelBuilder.Entity("ebeertijaBackend.Models.Narudzba", b =>
                {
                    b.HasOne("ebeertijaBackend.Models.Stol", "Stol")
                        .WithMany("Narudzbe")
                        .HasForeignKey("StolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ebeertijaBackend.Models.Racun", b =>
                {
                    b.HasOne("ebeertijaBackend.Models.Narudzba", "Narudzba")
                        .WithMany()
                        .HasForeignKey("NarudzbaId");

                    b.HasOne("ebeertijaBackend.Models.Stol", "Stol")
                        .WithMany("Racuni")
                        .HasForeignKey("StolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ebeertijaBackend.Models.Racun", "StorniraniRacun")
                        .WithMany()
                        .HasForeignKey("StorniraniRacunId");

                    b.HasOne("ebeertijaBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ebeertijaBackend.Models.StavkaCjenika", b =>
                {
                    b.HasOne("ebeertijaBackend.Models.Cjenik", "Cjenik")
                        .WithMany("StavkeCjenika")
                        .HasForeignKey("CjenikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ebeertijaBackend.Models.StavkaNarudzbe", b =>
                {
                    b.HasOne("ebeertijaBackend.Models.Narudzba", "Narudzba")
                        .WithMany("StavkeNarudzbe")
                        .HasForeignKey("NarudzbaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ebeertijaBackend.Models.StavkaCjenika", "StavkaCjenika")
                        .WithMany()
                        .HasForeignKey("StavkaCjenikaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ebeertijaBackend.Models.StavkaRacuna", b =>
                {
                    b.HasOne("ebeertijaBackend.Models.Racun", "Racun")
                        .WithMany("StavkeRacuna")
                        .HasForeignKey("RacunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ebeertijaBackend.Models.StavkaCjenika", "StavkaCjenika")
                        .WithMany()
                        .HasForeignKey("StavkaCjenikaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
