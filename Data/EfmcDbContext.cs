using System;
using Data.Entities;
using EFMC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFMC.Data.Common
{
    public class EfmcDbContext : DbContext
    {
        public EfmcDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=efmcdb;Username=huynl;Password=huyvuong0507");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserPharmacy>()
                .HasOne(_ => _.Owners)
                .WithMany(_ => _.OwnerPharmacies)
                .HasForeignKey(_ => _.OwnerId)
                .HasPrincipalKey(_ => _.UserId);

            modelBuilder.Entity<UserPharmacy>()
                .HasOne(_ => _.Pharmacists)
                .WithMany(_ => _.PharmacistPharmacies)
                .HasForeignKey(_ => _.PharmacistId)
                .HasPrincipalKey(_ => _.UserId);
        }

        #region DbSet
        public DbSet<Consignment> Consignments { get; set; }
        public DbSet<ConsignmentDrug> ConsignmentDrugs { get; set; }
        public DbSet<DebtTransaction> DebtTransactions { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugIndustry> DrugIndustries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<MedicalInstructionImage> MedicalInstructionImages { get; set; }
        public DbSet<MedicalInstructionType> MedicalInstructionTypes { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<MedicalRecordDisease> MedicalRecordDiseases { get; set; }
        public DbSet<MedicalRecordImage> MedicalRecordImages { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<PharmacyIndustry> PharmacyIndustries { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPharmacy> UserPharmacies { get; set; }
        #endregion

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}
