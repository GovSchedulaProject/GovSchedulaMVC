using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class GovSchedulaDbContext : DbContext
{
    public GovSchedulaDbContext()
    {
    }

    public GovSchedulaDbContext(DbContextOptions<GovSchedulaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adminlogin> Adminlogins { get; set; }

    public virtual DbSet<BirthSet> BirthSets { get; set; }

    public virtual DbSet<BloodGroup> BloodGroups { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Dependant> Dependants { get; set; }

    public virtual DbSet<DriverLicenceRegistration> DriverLicenceRegistrations { get; set; }

    public virtual DbSet<EyeTest> EyeTests { get; set; }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<Garantor> Garantors { get; set; }

    public virtual DbSet<GeneralDetail> GeneralDetails { get; set; }

    public virtual DbSet<GhanaCard> GhanaCards { get; set; }

    public virtual DbSet<GhanaCardRegistration> GhanaCardRegistrations { get; set; }

    public virtual DbSet<IdentityProof> IdentityProofs { get; set; }

    public virtual DbSet<NextofKin> NextofKins { get; set; }

    public virtual DbSet<Nhi> Nhis { get; set; }

    public virtual DbSet<Nhisregistration> Nhisregistrations { get; set; }

    public virtual DbSet<PassportRegistration> PassportRegistrations { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }

    public virtual DbSet<VoterId> VoterIds { get; set; }

    public virtual DbSet<VoterIdregistration> VoterIdregistrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACEXEDGE00\\SQLEXPRESS;Database=GovSchedulaDB; Integrated Security = true; MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adminlogin>(entity =>
        {
            entity.HasKey(e => e.AdminId)
                .HasName("PK24")
                .IsClustered(false);

            entity.ToTable("ADMINLOGIN");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Password).HasMaxLength(200);

            entity.HasOne(d => d.Department).WithMany(p => p.Adminlogins)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefDEPARTMENT35");
        });

        modelBuilder.Entity<BirthSet>(entity =>
        {
            entity.HasKey(e => e.BirthSetId)
                .HasName("PK12")
                .IsClustered(false);

            entity.ToTable("BirthSet");

            entity.Property(e => e.BirthSetId).HasColumnName("BirthSetID");
        });

        modelBuilder.Entity<BloodGroup>(entity =>
        {
            entity.HasKey(e => e.BloodGroupId)
                .HasName("PK17")
                .IsClustered(false);

            entity.ToTable("BloodGroup");

            entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");
            entity.Property(e => e.BloodGroup1)
                .HasMaxLength(10)
                .HasColumnName("BloodGroup");
            entity.Property(e => e.ChronicIllness)
                .HasMaxLength(10)
                .HasColumnName("chronicIllness");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId)
                .HasName("PK27")
                .IsClustered(false);

            entity.ToTable("DEPARTMENT");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Dependant>(entity =>
        {
            entity.HasKey(e => e.DependantId)
                .HasName("PK22")
                .IsClustered(false);

            entity.ToTable("DEPENDANT");

            entity.Property(e => e.DependantId).HasColumnName("DependantID");
            entity.Property(e => e.DependantName).HasMaxLength(100);
            entity.Property(e => e.Relationship).HasMaxLength(200);
        });

        modelBuilder.Entity<DriverLicenceRegistration>(entity =>
        {
            entity.HasKey(e => e.DriverLicenceId)
                .HasName("PK14")
                .IsClustered(false);

            entity.ToTable("DriverLIcenceRegistration");

            entity.Property(e => e.DriverLicenceId).HasColumnName("DriverLicenceID");
            entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");
            entity.Property(e => e.EyeTestId).HasColumnName("EyeTestID");
            entity.Property(e => e.GeneralDetailsId).HasColumnName("GeneralDetailsID");
            entity.Property(e => e.NextofKinId).HasColumnName("NextofKinID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.VehicleClass).HasMaxLength(200);

            entity.HasOne(d => d.BloodGroup).WithMany(p => p.DriverLicenceRegistrations)
                .HasForeignKey(d => d.BloodGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefBloodGroup20");

            entity.HasOne(d => d.EyeTest).WithMany(p => p.DriverLicenceRegistrations)
                .HasForeignKey(d => d.EyeTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefEyeTest21");

            entity.HasOne(d => d.GeneralDetails).WithMany(p => p.DriverLicenceRegistrations)
                .HasForeignKey(d => d.GeneralDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefGeneralDetails16");

            entity.HasOne(d => d.NextofKin).WithMany(p => p.DriverLicenceRegistrations)
                .HasForeignKey(d => d.NextofKinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefNextofKin19");
        });

        modelBuilder.Entity<EyeTest>(entity =>
        {
            entity.HasKey(e => e.EyeTestId)
                .HasName("PK19")
                .IsClustered(false);

            entity.ToTable("EyeTest");

            entity.Property(e => e.EyeTestId).HasColumnName("EyeTestID");
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.HasKey(e => e.FamilyId)
                .HasName("PK16")
                .IsClustered(false);

            entity.ToTable("Family");

            entity.Property(e => e.FamilyId).HasColumnName("FamilyID");
            entity.Property(e => e.FatherFullName).HasMaxLength(100);
            entity.Property(e => e.FatherHomeTown).HasMaxLength(200);
            entity.Property(e => e.MotherFullName).HasMaxLength(100);
            entity.Property(e => e.MotherHomeTown).HasMaxLength(200);
            entity.Property(e => e.SpouseName).HasMaxLength(100);
            entity.Property(e => e.SpouseNationality).HasMaxLength(200);
        });

        modelBuilder.Entity<Garantor>(entity =>
        {
            entity.HasKey(e => e.GarantorId)
                .HasName("PK4")
                .IsClustered(false);

            entity.ToTable("Garantor");

            entity.Property(e => e.GarantorId).HasColumnName("GarantorID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.OtherNames).HasMaxLength(100);
            entity.Property(e => e.PostalAddress).HasMaxLength(200);
            entity.Property(e => e.Profession).HasMaxLength(100);

            entity.HasOne(d => d.IdentityProofNavigation).WithMany(p => p.Garantors)
                .HasForeignKey(d => d.IdentityProof)
                .HasConstraintName("RefIdentityProof27");
        });

        modelBuilder.Entity<GeneralDetail>(entity =>
        {
            entity.HasKey(e => e.GeneralDetailsId)
                .HasName("PK2_3")
                .IsClustered(false);

            entity.Property(e => e.GeneralDetailsId).HasColumnName("GeneralDetailsID");
            entity.Property(e => e.BirthPlace).HasMaxLength(200);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Nationality).HasMaxLength(100);
            entity.Property(e => e.Occupation).HasMaxLength(100);
            entity.Property(e => e.PostalAddress)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ResidentalAddress).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Department).WithMany(p => p.GeneralDetails)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefDEPARTMENT42");

            entity.HasOne(d => d.IdentityProofNavigation).WithMany(p => p.GeneralDetails)
                .HasForeignKey(d => d.IdentityProof)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefIdentityProof13");

            entity.HasOne(d => d.User).WithMany(p => p.GeneralDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefUSERPROFILE29");
        });

        modelBuilder.Entity<GhanaCard>(entity =>
        {
            entity.HasKey(e => e.GhanaCardId)
                .HasName("PK5")
                .IsClustered(false);

            entity.ToTable("GhanaCard");

            entity.Property(e => e.GhanaCardId).HasColumnName("GhanaCardID");
            entity.Property(e => e.GhanaCardIdnumber).HasColumnName("GhanaCardIDNumber");
        });

        modelBuilder.Entity<GhanaCardRegistration>(entity =>
        {
            entity.HasKey(e => e.GhanaCardId)
                .HasName("PK13")
                .IsClustered(false);

            entity.ToTable("GhanaCardRegistration");

            entity.Property(e => e.GhanaCardId).HasColumnName("GhanaCardID");
            entity.Property(e => e.District).HasMaxLength(200);
            entity.Property(e => e.FamilyId).HasColumnName("FamilyID");
            entity.Property(e => e.GeneralDetailsId).HasColumnName("GeneralDetailsID");
            entity.Property(e => e.HomeTown).HasMaxLength(200);
            entity.Property(e => e.Religion).HasMaxLength(200);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Family).WithMany(p => p.GhanaCardRegistrations)
                .HasForeignKey(d => d.FamilyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefFamily18");

            entity.HasOne(d => d.GeneralDetails).WithMany(p => p.GhanaCardRegistrations)
                .HasForeignKey(d => d.GeneralDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefGeneralDetails15");
        });

        modelBuilder.Entity<IdentityProof>(entity =>
        {
            entity.HasKey(e => e.IdentityProof1)
                .HasName("PK3")
                .IsClustered(false);

            entity.ToTable("IdentityProof");

            entity.Property(e => e.IdentityProof1).HasColumnName("IdentityProof");
            entity.Property(e => e.BirthSetId).HasColumnName("BirthSetID");
            entity.Property(e => e.GarantorId).HasColumnName("GarantorID");
            entity.Property(e => e.GhanaCardId).HasColumnName("GhanaCardID");
            entity.Property(e => e.Nhisid).HasColumnName("NHISID");
            entity.Property(e => e.VoterId).HasColumnName("VoterID");

            entity.HasOne(d => d.BirthSet).WithMany(p => p.IdentityProofs)
                .HasForeignKey(d => d.BirthSetId)
                .HasConstraintName("RefBirthSet38");

            entity.HasOne(d => d.Garantor).WithMany(p => p.IdentityProofs)
                .HasForeignKey(d => d.GarantorId)
                .HasConstraintName("RefGarantor39");

            entity.HasOne(d => d.GhanaCard).WithMany(p => p.IdentityProofs)
                .HasForeignKey(d => d.GhanaCardId)
                .HasConstraintName("RefGhanaCard37");

            entity.HasOne(d => d.Nhis).WithMany(p => p.IdentityProofs)
                .HasForeignKey(d => d.Nhisid)
                .HasConstraintName("RefNHIS40");

            entity.HasOne(d => d.Voter).WithMany(p => p.IdentityProofs)
                .HasForeignKey(d => d.VoterId)
                .HasConstraintName("RefVoterID41");
        });

        modelBuilder.Entity<NextofKin>(entity =>
        {
            entity.HasKey(e => e.NextofKinId)
                .HasName("PK18")
                .IsClustered(false);

            entity.ToTable("NextofKin");

            entity.Property(e => e.NextofKinId).HasColumnName("NextofKinID");
            entity.Property(e => e.NextofKin1)
                .HasMaxLength(100)
                .HasColumnName("NextofKin");
            entity.Property(e => e.Relationship).HasMaxLength(100);
        });

        modelBuilder.Entity<Nhi>(entity =>
        {
            entity.HasKey(e => e.Nhisid)
                .HasName("PK6")
                .IsClustered(false);

            entity.ToTable("NHIS");

            entity.Property(e => e.Nhisid).HasColumnName("NHISID");
            entity.Property(e => e.Nhisidnumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("NHISIDNumber");
        });

        modelBuilder.Entity<Nhisregistration>(entity =>
        {
            entity.HasKey(e => e.Nhisid)
                .HasName("PK15")
                .IsClustered(false);

            entity.ToTable("NHISRegistration");

            entity.Property(e => e.Nhisid).HasColumnName("NHISID");
            entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");
            entity.Property(e => e.DependantId).HasColumnName("DependantID");
            entity.Property(e => e.EducationalLevel).HasMaxLength(200);
            entity.Property(e => e.GeneralDetailsId).HasColumnName("GeneralDetailsID");
            entity.Property(e => e.NextofKinId).HasColumnName("NextofKinID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.BloodGroup).WithMany(p => p.Nhisregistrations)
                .HasForeignKey(d => d.BloodGroupId)
                .HasConstraintName("RefBloodGroup28");

            entity.HasOne(d => d.Dependant).WithMany(p => p.Nhisregistrations)
                .HasForeignKey(d => d.DependantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefDEPENDANT23");

            entity.HasOne(d => d.GeneralDetails).WithMany(p => p.Nhisregistrations)
                .HasForeignKey(d => d.GeneralDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefGeneralDetails14");

            entity.HasOne(d => d.NextofKin).WithMany(p => p.Nhisregistrations)
                .HasForeignKey(d => d.NextofKinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefNextofKin25");
        });

        modelBuilder.Entity<PassportRegistration>(entity =>
        {
            entity.HasKey(e => e.PassportId)
                .HasName("PK20")
                .IsClustered(false);

            entity.ToTable("PassportRegistration");

            entity.Property(e => e.PassportId).HasColumnName("PassportID");
            entity.Property(e => e.CountryDestination).HasMaxLength(200);
            entity.Property(e => e.FamilyId).HasColumnName("FamilyID");
            entity.Property(e => e.GeneralDetailsId).HasColumnName("GeneralDetailsID");
            entity.Property(e => e.ReasonForApply).HasMaxLength(500);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.TypeOfPassport).HasMaxLength(100);

            entity.HasOne(d => d.Family).WithMany(p => p.PassportRegistrations)
                .HasForeignKey(d => d.FamilyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefFamily26");

            entity.HasOne(d => d.GeneralDetails).WithMany(p => p.PassportRegistrations)
                .HasForeignKey(d => d.GeneralDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefGeneralDetails17");
        });

        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.HasKey(e => e.UserId)
                .HasName("PK25")
                .IsClustered(false);

            entity.ToTable("USERPROFILE");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.PhoneNumber).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<VoterId>(entity =>
        {
            entity.HasKey(e => e.VoterId1)
                .HasName("PK7")
                .IsClustered(false);

            entity.ToTable("VoterID");

            entity.Property(e => e.VoterId1).HasColumnName("VoterID");
            entity.Property(e => e.VoterIdnumber).HasColumnName("VoterIDNumber");
        });

        modelBuilder.Entity<VoterIdregistration>(entity =>
        {
            entity.HasKey(e => e.VoterId)
                .HasName("PK21")
                .IsClustered(false);

            entity.ToTable("VoterIDRegistration");

            entity.Property(e => e.VoterId).HasColumnName("VoterID");
            entity.Property(e => e.GeneralDetailsId).HasColumnName("GeneralDetailsID");

            entity.HasOne(d => d.GeneralDetails).WithMany(p => p.VoterIdregistrations)
                .HasForeignKey(d => d.GeneralDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefGeneralDetails22");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
