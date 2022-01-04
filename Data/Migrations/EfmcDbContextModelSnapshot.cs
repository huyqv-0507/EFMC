﻿// <auto-generated />
using System;
using EFMC.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EFMC.Data.Migrations
{
    [DbContext(typeof(EfmcDbContext))]
    partial class EfmcDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Data.Entities.Consignment", b =>
                {
                    b.Property<int>("ConsignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateImported")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("From")
                        .HasColumnType("text");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("numeric");

                    b.HasKey("ConsignmentId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Consignments");
                });

            modelBuilder.Entity("Data.Entities.ConsignmentDrug", b =>
                {
                    b.Property<int>("ConsignmentDrugId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ConsignmentId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<int>("DrugId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ConsignmentDrugId");

                    b.HasIndex("ConsignmentId");

                    b.HasIndex("DrugId");

                    b.ToTable("ConsignmentDrugs");
                });

            modelBuilder.Entity("Data.Entities.DebtTransaction", b =>
                {
                    b.Property<int>("DebtTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DatePayback")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("MoneyPayback")
                        .HasColumnType("numeric");

                    b.Property<string>("NamePayback")
                        .HasColumnType("text");

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("integer");

                    b.HasKey("DebtTransactionId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("DebtTransactions");
                });

            modelBuilder.Entity("Data.Entities.Disease", b =>
                {
                    b.Property<int>("DiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("DiseaseId");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("Data.Entities.Drug", b =>
                {
                    b.Property<int>("DrugId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BrandName")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Ingredient")
                        .HasColumnType("text");

                    b.Property<string>("MainIngredient")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Package")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Unit")
                        .HasColumnType("text");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasKey("DrugId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("Data.Entities.DrugIndustry", b =>
                {
                    b.Property<int>("DrugIndustryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DrugId")
                        .HasColumnType("integer");

                    b.Property<int>("IndustryId")
                        .HasColumnType("integer");

                    b.HasKey("DrugIndustryId");

                    b.HasIndex("DrugId");

                    b.HasIndex("IndustryId");

                    b.ToTable("DrugIndustries");
                });

            modelBuilder.Entity("Data.Entities.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("MedicalRecordImageId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("ImageId");

                    b.HasIndex("MedicalRecordImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Data.Entities.Industry", b =>
                {
                    b.Property<int>("IndustryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("IndustryId");

                    b.ToTable("Industries");
                });

            modelBuilder.Entity("Data.Entities.MedicalInstructionImage", b =>
                {
                    b.Property<int>("MedicalInstructionImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MedicalInstructionTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("MedicalRecordId")
                        .HasColumnType("integer");

                    b.Property<int>("MedicalRecordImageId")
                        .HasColumnType("integer");

                    b.HasKey("MedicalInstructionImageId");

                    b.HasIndex("MedicalInstructionTypeId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("MedicalInstructionImages");
                });

            modelBuilder.Entity("Data.Entities.MedicalInstructionType", b =>
                {
                    b.Property<int>("MedicalInstructionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("MedicalInstructionTypeId");

                    b.ToTable("MedicalInstructionTypes");
                });

            modelBuilder.Entity("Data.Entities.MedicalRecord", b =>
                {
                    b.Property<int>("MedicalRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateFinished")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTreatment")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PatientName")
                        .HasColumnType("text");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalDebt")
                        .HasColumnType("numeric");

                    b.HasKey("MedicalRecordId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("Data.Entities.MedicalRecordDisease", b =>
                {
                    b.Property<int>("MedicalRecordDiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DiseaseDescription")
                        .HasColumnType("text");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("integer");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("integer");

                    b.HasKey("MedicalRecordDiseaseId");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("MedicalRecordDiseases");
                });

            modelBuilder.Entity("Data.Entities.MedicalRecordImage", b =>
                {
                    b.Property<int>("MedicalRecordImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("DiseaseId")
                        .HasColumnType("integer");

                    b.Property<int?>("MedicalInstructionImageId")
                        .HasColumnType("integer");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("integer");

                    b.HasKey("MedicalRecordImageId");

                    b.HasIndex("MedicalInstructionImageId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("MedicalRecordImages");
                });

            modelBuilder.Entity("Data.Entities.Pharmacy", b =>
                {
                    b.Property<int>("PharmacyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("PharmacyId");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Data.Entities.PharmacyIndustry", b =>
                {
                    b.Property<int>("PharmacyIndustryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IndustryId")
                        .HasColumnType("integer");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("integer");

                    b.HasKey("PharmacyIndustryId");

                    b.HasIndex("IndustryId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("PharmacyIndustries");
                });

            modelBuilder.Entity("Data.Entities.Prescription", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BuyerName")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateFinished")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateStarted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("MedicalRecordId")
                        .HasColumnType("integer");

                    b.Property<string>("ReasonCanceled")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("PrescriptionId");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Data.Entities.PrescriptionDetail", b =>
                {
                    b.Property<int>("PrescriptionDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Afternoon")
                        .HasColumnType("integer");

                    b.Property<int>("DrugId")
                        .HasColumnType("integer");

                    b.Property<string>("DrugName")
                        .HasColumnType("text");

                    b.Property<int>("Morning")
                        .HasColumnType("integer");

                    b.Property<int>("Night")
                        .HasColumnType("integer");

                    b.Property<int>("Noon")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("integer");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("UseTime")
                        .HasColumnType("text");

                    b.HasKey("PrescriptionDetailId");

                    b.HasIndex("DrugId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("PrescriptionDetails");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool>("IsLogin")
                        .HasColumnType("boolean");

                    b.Property<double?>("LoginFailedCount")
                        .HasColumnType("double precision");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Entities.UserPharmacy", b =>
                {
                    b.Property<int>("UserPharmacyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("PharmacistId")
                        .HasColumnType("integer");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("UserPharmacyId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PharmacistId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("UserPharmacies");
                });

            modelBuilder.Entity("EFMC.Data.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.Entities.Consignment", b =>
                {
                    b.HasOne("Data.Entities.Pharmacy", "Pharmacy")
                        .WithMany("Consignments")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.ConsignmentDrug", b =>
                {
                    b.HasOne("Data.Entities.Consignment", "Consignment")
                        .WithMany("ConsignmentDrugs")
                        .HasForeignKey("ConsignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Drug", "Drug")
                        .WithMany("ConsignmentDrugs")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.DebtTransaction", b =>
                {
                    b.HasOne("Data.Entities.Prescription", null)
                        .WithMany("DebtTransactions")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.DrugIndustry", b =>
                {
                    b.HasOne("Data.Entities.Drug", "Drug")
                        .WithMany("DrugIndustries")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Industry", "Industry")
                        .WithMany("DrugIndustries")
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.Image", b =>
                {
                    b.HasOne("Data.Entities.MedicalRecordImage", "MedicalRecordImage")
                        .WithMany("Images")
                        .HasForeignKey("MedicalRecordImageId");
                });

            modelBuilder.Entity("Data.Entities.MedicalInstructionImage", b =>
                {
                    b.HasOne("Data.Entities.MedicalInstructionType", "MedicalInstructionType")
                        .WithMany("MedicalInstructionImages")
                        .HasForeignKey("MedicalInstructionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.MedicalRecord", null)
                        .WithMany("MedicalInstructionImages")
                        .HasForeignKey("MedicalRecordId");
                });

            modelBuilder.Entity("Data.Entities.MedicalRecord", b =>
                {
                    b.HasOne("Data.Entities.Pharmacy", "Pharmacy")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.MedicalRecordDisease", b =>
                {
                    b.HasOne("Data.Entities.Disease", "Disease")
                        .WithMany("MedicalRecordDiseases")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.MedicalRecord", "MedicalRecord")
                        .WithMany("MedicalRecordDiseases")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.MedicalRecordImage", b =>
                {
                    b.HasOne("Data.Entities.MedicalInstructionImage", "MedicalInstructionImage")
                        .WithMany("MedicalRecordImages")
                        .HasForeignKey("MedicalInstructionImageId");

                    b.HasOne("Data.Entities.MedicalRecord", "MedicalRecord")
                        .WithMany()
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.PharmacyIndustry", b =>
                {
                    b.HasOne("Data.Entities.Industry", "Industry")
                        .WithMany("PharmacyIndustries")
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Pharmacy", "Pharmacy")
                        .WithMany("PharmacyIndustries")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.Prescription", b =>
                {
                    b.HasOne("Data.Entities.MedicalRecord", "MedicalRecord")
                        .WithMany("Prescriptions")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.PrescriptionDetail", b =>
                {
                    b.HasOne("Data.Entities.Drug", "Drug")
                        .WithMany("PrescriptionDetails")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Prescription", "Prescription")
                        .WithMany("PrescriptionDetails")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.HasOne("EFMC.Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.UserPharmacy", b =>
                {
                    b.HasOne("Data.Entities.User", "Owners")
                        .WithMany("OwnerPharmacies")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.User", "Pharmacists")
                        .WithMany("PharmacistPharmacies")
                        .HasForeignKey("PharmacistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Pharmacy", "Pharmacies")
                        .WithMany("UserPharmacies")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
