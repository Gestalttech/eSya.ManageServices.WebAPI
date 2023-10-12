using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eSya.ManageServices.DL.Entities
{
    public partial class eSyaEnterprise : DbContext
    {
        public static string _connString = "";
        public eSyaEnterprise()
        {
        }

        public eSyaEnterprise(DbContextOptions<eSyaEnterprise> options)
            : base(options)
        {
        }

        public virtual DbSet<GtEcapcd> GtEcapcds { get; set; } = null!;
        public virtual DbSet<GtEcbsln> GtEcbslns { get; set; } = null!;
        public virtual DbSet<GtEsdocd> GtEsdocds { get; set; } = null!;
        public virtual DbSet<GtEsdocl> GtEsdocls { get; set; } = null!;
        public virtual DbSet<GtEsdos2> GtEsdos2s { get; set; } = null!;
        public virtual DbSet<GtEsopcl> GtEsopcls { get; set; } = null!;
        public virtual DbSet<GtEspasm> GtEspasms { get; set; } = null!;
        public virtual DbSet<GtEsspbl> GtEsspbls { get; set; } = null!;
        public virtual DbSet<GtEsspcd> GtEsspcds { get; set; } = null!;
        public virtual DbSet<GtEssrbl> GtEssrbls { get; set; } = null!;
        public virtual DbSet<GtEssrcg> GtEssrcgs { get; set; } = null!;
        public virtual DbSet<GtEssrcl> GtEssrcls { get; set; } = null!;
        public virtual DbSet<GtEssrgr> GtEssrgrs { get; set; } = null!;
        public virtual DbSet<GtEssrm> GtEssrms { get; set; } = null!;
        public virtual DbSet<GtEssrty> GtEssrties { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GtEcapcd>(entity =>
            {
                entity.HasKey(e => e.ApplicationCode)
                    .HasName("PK_GT_ECAPCD_1");

                entity.ToTable("GT_ECAPCD");

                entity.Property(e => e.ApplicationCode).ValueGeneratedNever();

                entity.Property(e => e.CodeDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortCode).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEcbsln>(entity =>
            {
                entity.HasKey(e => new { e.BusinessId, e.LocationId });

                entity.ToTable("GT_ECBSLN");

                entity.HasIndex(e => e.BusinessKey, "IX_GT_ECBSLN")
                    .IsUnique();

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.BusinessName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CurrencyCode).HasMaxLength(4);

                entity.Property(e => e.EActiveUsers).HasColumnName("eActiveUsers");

                entity.Property(e => e.EBusinessKey).HasColumnName("eBusinessKey");

                entity.Property(e => e.ENoOfBeds).HasColumnName("eNoOfBeds");

                entity.Property(e => e.ESyaLicenseType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("eSyaLicenseType")
                    .IsFixedLength();

                entity.Property(e => e.EUserLicenses).HasColumnName("eUserLicenses");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.LocationDescription).HasMaxLength(150);

                entity.Property(e => e.LocnDateFormat)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.TocurrConversion).HasColumnName("TOCurrConversion");

                entity.Property(e => e.TolocalCurrency)
                    .IsRequired()
                    .HasColumnName("TOLocalCurrency")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TorealCurrency).HasColumnName("TORealCurrency");
            });

            modelBuilder.Entity<GtEsdocd>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.ToTable("GT_ESDOCD");

                entity.Property(e => e.DoctorId)
                    .ValueGeneratedNever()
                    .HasColumnName("DoctorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DoctorName).HasMaxLength(50);

                entity.Property(e => e.DoctorRegnNo).HasMaxLength(25);

                entity.Property(e => e.DoctorShortName).HasMaxLength(10);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Isdcode).HasColumnName("ISDCode");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.TraiffFrom)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<GtEsdocl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId, e.DoctorId, e.ClinicId })
                    .HasName("PK_GT_ESDOCL_1");

                entity.ToTable("GT_ESDOCL");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsdos2>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ConsultationId, e.ClinicId, e.SpecialtyId, e.DoctorId, e.DayOfWeek, e.SerialNo })
                    .HasName("PK_GT_ESDOS2_1");

                entity.ToTable("GT_ESDOS2");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.DayOfWeek).HasMaxLength(10);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Xlreference)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("XLReference");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.GtEsdos2s)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOS2_GT_ESDOCD");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.GtEsdos2s)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESDOS2_GT_ESSPCD");
            });

            modelBuilder.Entity<GtEsopcl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ClinicId, e.ConsultationId });

                entity.ToTable("GT_ESOPCL");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEspasm>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.ParameterId });

                entity.ToTable("GT_ESPASM");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.GtEspasms)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESPASM_GT_ESSRMS");
            });

            modelBuilder.Entity<GtEsspbl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.SpecialtyId })
                    .HasName("PK_GT_ESSPBL_1");

                entity.ToTable("GT_ESSPBL");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEsspcd>(entity =>
            {
                entity.HasKey(e => e.SpecialtyId);

                entity.ToTable("GT_ESSPCD");

                entity.Property(e => e.SpecialtyId)
                    .ValueGeneratedNever()
                    .HasColumnName("SpecialtyID");

                entity.Property(e => e.AlliedServices)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MedicalIcon).HasMaxLength(150);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.SpecialtyDesc).HasMaxLength(50);

                entity.Property(e => e.SpecialtyType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<GtEssrbl>(entity =>
            {
                entity.HasKey(e => new { e.BusinessKey, e.ServiceId });

                entity.ToTable("GT_ESSRBL");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.HolidayPercentage).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.InternalServiceCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.NightLinePercentage).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ServiceCost).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEssrcg>(entity =>
            {
                entity.HasKey(e => e.ServiceClassId);

                entity.ToTable("GT_ESSRCG");

                entity.Property(e => e.ServiceClassId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceClassID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.IntSccode).HasColumnName("IntSCCode");

                entity.Property(e => e.IntScpattern)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("IntSCPattern");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEssrcl>(entity =>
            {
                entity.HasKey(e => e.ServiceClassId);

                entity.ToTable("GT_ESSRCL");

                entity.Property(e => e.ServiceClassId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceClassID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ServiceClassDesc).HasMaxLength(50);

                entity.Property(e => e.ServiceGroupId).HasColumnName("ServiceGroupID");

                entity.HasOne(d => d.ServiceGroup)
                    .WithMany(p => p.GtEssrcls)
                    .HasForeignKey(d => d.ServiceGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESSRCL_GT_ESSRGR");
            });

            modelBuilder.Entity<GtEssrgr>(entity =>
            {
                entity.HasKey(e => e.ServiceGroupId);

                entity.ToTable("GT_ESSRGR");

                entity.Property(e => e.ServiceGroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceGroupID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ServiceCriteria)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))")
                    .IsFixedLength();

                entity.Property(e => e.ServiceGroupDesc).HasMaxLength(50);

                entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.GtEssrgrs)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESSRGR_GT_ESSRTY");
            });

            modelBuilder.Entity<GtEssrm>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("GT_ESSRMS");

                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.InternalServiceCode).HasMaxLength(15);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ServiceClassId).HasColumnName("ServiceClassID");

                entity.Property(e => e.ServiceCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ServiceDesc).HasMaxLength(75);

                entity.Property(e => e.ServiceShortDesc)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.ServiceClass)
                    .WithMany(p => p.GtEssrms)
                    .HasForeignKey(d => d.ServiceClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GT_ESSRMS_GT_ESSRCL");
            });

            modelBuilder.Entity<GtEssrty>(entity =>
            {
                entity.HasKey(e => e.ServiceTypeId);

                entity.ToTable("GT_ESSRTY");

                entity.Property(e => e.ServiceTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceTypeID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ServiceTypeDesc).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
