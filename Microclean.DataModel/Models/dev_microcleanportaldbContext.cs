using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Microclean.DataModel.Models
{
    public partial class dev_microcleanportaldbContext : DbContext
    {
        public dev_microcleanportaldbContext()
        {
        }

        public dev_microcleanportaldbContext(DbContextOptions<dev_microcleanportaldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tbladdress> Tbladdress { get; set; }
        public virtual DbSet<Tbladdresstype> Tbladdresstype { get; set; }
        public virtual DbSet<Tblcategory> Tblcategory { get; set; }
        public virtual DbSet<Tblcity> Tblcity { get; set; }
        public virtual DbSet<Tblcitylocation> Tblcitylocation { get; set; }
        public virtual DbSet<Tblclient> Tblclient { get; set; }
        public virtual DbSet<Tblcountry> Tblcountry { get; set; }
        public virtual DbSet<Tbldistrict> Tbldistrict { get; set; }
        public virtual DbSet<Tbldocumenttype> Tbldocumenttype { get; set; }
        public virtual DbSet<Tblfeedetail> Tblfeedetail { get; set; }
        public virtual DbSet<Tblfeetype> Tblfeetype { get; set; }
        public virtual DbSet<Tblfranchiseepayments> Tblfranchiseepayments { get; set; }
        public virtual DbSet<Tblpaymenttype> Tblpaymenttype { get; set; }
        public virtual DbSet<Tblquestion> Tblquestion { get; set; }
        public virtual DbSet<Tblquestionoptions> Tblquestionoptions { get; set; }
        public virtual DbSet<Tblrole> Tblrole { get; set; }
        public virtual DbSet<Tblstate> Tblstate { get; set; }
        public virtual DbSet<Tbltest> Tbltest { get; set; }
        public virtual DbSet<Tbltestuserright> Tbltestuserright { get; set; }
        public virtual DbSet<Tbltranningdocument> Tbltranningdocument { get; set; }
        public virtual DbSet<Tbluser> Tbluser { get; set; }
        public virtual DbSet<Tbluseraddressmapping> Tbluseraddressmapping { get; set; }
        public virtual DbSet<Tbluserdetail> Tbluserdetail { get; set; }
        public virtual DbSet<Tbluserdoument> Tbluserdoument { get; set; }
        public virtual DbSet<Tbluserrolemapping> Tbluserrolemapping { get; set; }
        public virtual DbSet<Tblusersubmitedanswer> Tblusersubmitedanswer { get; set; }
        public virtual DbSet<Tblusertype> Tblusertype { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbladdress>(entity =>
            {
                entity.ToTable("tbladdress");

                entity.HasIndex(e => e.CityId)
                    .HasName("tbladdress_ibfk_3");

                entity.HasIndex(e => e.CityLocationId)
                    .HasName("CItyLocationId");

                entity.HasIndex(e => e.CountryId)
                    .HasName("tbladdress_ibfk_1");

                entity.HasIndex(e => e.DistrictId)
                    .HasName("DistrictId");

                entity.HasIndex(e => e.StateId)
                    .HasName("tbladdress_ibfk_2");

                entity.Property(e => e.CityLocationId).HasColumnName("CItyLocationId");

                entity.Property(e => e.LandMark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(9,6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9,6)");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Tbladdress)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("tbladdress_ibfk_3");

                entity.HasOne(d => d.CityLocation)
                    .WithMany(p => p.Tbladdress)
                    .HasForeignKey(d => d.CityLocationId)
                    .HasConstraintName("tbladdress_ibfk_5");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Tbladdress)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("tbladdress_ibfk_1");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Tbladdress)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("tbladdress_ibfk_4");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Tbladdress)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("tbladdress_ibfk_2");
            });

            modelBuilder.Entity<Tbladdresstype>(entity =>
            {
                entity.ToTable("tbladdresstype");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");
            });

            modelBuilder.Entity<Tblcategory>(entity =>
            {
                entity.ToTable("tblcategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tblcity>(entity =>
            {
                entity.ToTable("tblcity");

                entity.HasIndex(e => e.StateId)
                    .HasName("StateId");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Tblcity)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("tblcity_ibfk_1");
            });

            modelBuilder.Entity<Tblcitylocation>(entity =>
            {
                entity.ToTable("tblcitylocation");

                entity.HasIndex(e => e.DistrictId)
                    .HasName("DistrictId");

                entity.Property(e => e.CityLocation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ExtendedCityLocation)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Tblcitylocation)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("tblcitylocation_ibfk_1");
            });

            modelBuilder.Entity<Tblclient>(entity =>
            {
                entity.ToTable("tblclient");

                entity.HasIndex(e => e.CityLocationid)
                    .HasName("tblclient_ibfk_1");

                entity.Property(e => e.CompanyGstNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyPanCardNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogoPath)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.CityLocation)
                    .WithMany(p => p.Tblclient)
                    .HasForeignKey(d => d.CityLocationid)
                    .HasConstraintName("tblclient_ibfk_1");
            });

            modelBuilder.Entity<Tblcountry>(entity =>
            {
                entity.ToTable("tblcountry");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sortname)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");
            });

            modelBuilder.Entity<Tbldistrict>(entity =>
            {
                entity.ToTable("tbldistrict");

                entity.HasIndex(e => e.StateId)
                    .HasName("StateId");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Tbldistrict)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("tbldistrict_ibfk_1");
            });

            modelBuilder.Entity<Tbldocumenttype>(entity =>
            {
                entity.ToTable("tbldocumenttype");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tbldocumenttype)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("tbldocumenttype_ibfk_1");
            });

            modelBuilder.Entity<Tblfeedetail>(entity =>
            {
                entity.ToTable("tblfeedetail");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.HasIndex(e => e.FeeTypeId)
                    .HasName("FeeTypeId");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.FeeValue).HasColumnType("decimal(18,2)");

                entity.Property(e => e.PaidAmmout).HasColumnType("decimal(18,2)");

                entity.Property(e => e.TotalFee).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tblfeedetail)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("tblfeedetail_ibfk_1");

                entity.HasOne(d => d.FeeType)
                    .WithMany(p => p.Tblfeedetail)
                    .HasForeignKey(d => d.FeeTypeId)
                    .HasConstraintName("tblfeedetail_ibfk_3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tblfeedetail)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tblfeedetail_ibfk_2");
            });

            modelBuilder.Entity<Tblfeetype>(entity =>
            {
                entity.ToTable("tblfeetype");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tblfeetype)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("tblfeetype_ibfk_1");
            });

            modelBuilder.Entity<Tblfranchiseepayments>(entity =>
            {
                entity.ToTable("tblfranchiseepayments");

                entity.HasIndex(e => e.FeeDetailId)
                    .HasName("FeeDetailId");

                entity.HasIndex(e => e.PaymentTypeId)
                    .HasName("PaymentTypeId");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.OtherFeePayment).HasColumnType("decimal(18,2) unsigned zerofill");

                entity.Property(e => e.PaymentReference)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RecievedPayment).HasColumnType("decimal(18,2) unsigned zerofill");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.Property(e => e.TotalAmountPaid).HasColumnType("decimal(18,2) unsigned zerofill");

                entity.HasOne(d => d.FeeDetail)
                    .WithMany(p => p.Tblfranchiseepayments)
                    .HasForeignKey(d => d.FeeDetailId)
                    .HasConstraintName("tblfranchiseepayments_ibfk_2");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Tblfranchiseepayments)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("tblfranchiseepayments_ibfk_3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tblfranchiseepayments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tblfranchiseepayments_ibfk_1");
            });

            modelBuilder.Entity<Tblpaymenttype>(entity =>
            {
                entity.ToTable("tblpaymenttype");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");
            });

            modelBuilder.Entity<Tblquestion>(entity =>
            {
                entity.ToTable("tblquestion");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("TestId");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tblquestion)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("tblquestion_ibfk_1");
            });

            modelBuilder.Entity<Tblquestionoptions>(entity =>
            {
                entity.ToTable("tblquestionoptions");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("QuestionId");

                entity.Property(e => e.IsMatched).HasColumnType("tinyint(1)");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Tblquestionoptions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("tblquestionoptions_ibfk_1");
            });

            modelBuilder.Entity<Tblrole>(entity =>
            {
                entity.ToTable("tblrole");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tblrole)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("tblrole_ibfk_1");
            });

            modelBuilder.Entity<Tblstate>(entity =>
            {
                entity.ToTable("tblstate");

                entity.HasIndex(e => e.CountryId)
                    .HasName("CountryId");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Tblstate)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("tblstate_ibfk_1");
            });

            modelBuilder.Entity<Tbltest>(entity =>
            {
                entity.ToTable("tbltest");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("ServiceId");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Tbltest)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("tbltest_ibfk_1");
            });

            modelBuilder.Entity<Tbltestuserright>(entity =>
            {
                entity.ToTable("tbltestuserright");

                entity.HasIndex(e => e.TestId)
                    .HasName("TestId");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.InsertDate).HasColumnType("date");

                entity.Property(e => e.IsApproved).HasColumnType("tinyint(1)");

                entity.Property(e => e.LastUpdateDate).HasColumnType("date");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Tbltestuserright)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("tbltestuserright_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbltestuserright)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tbltestuserright_ibfk_1");
            });

            modelBuilder.Entity<Tbltranningdocument>(entity =>
            {
                entity.ToTable("tbltranningdocument");

                entity.Property(e => e.Title)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tbluser>(entity =>
            {
                entity.ToTable("tbluser");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.HasIndex(e => e.UserTypeId)
                    .HasName("UserTypeId");

                entity.Property(e => e.DeviceType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EmailConfirmed).HasColumnType("tinyint(1)");

                entity.Property(e => e.Fcmtoken).HasColumnName("FCMToken");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsApproved).HasColumnType("tinyint(1)");

                entity.Property(e => e.IsGuestUser).HasColumnType("tinyint(1)");

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NormalizedUserName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberConfirmed).HasColumnType("tinyint(1)");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tbluser)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("tbluser_ibfk_2");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Tbluser)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("tbluser_ibfk_1");
            });

            modelBuilder.Entity<Tbluseraddressmapping>(entity =>
            {
                entity.ToTable("tbluseraddressmapping");

                entity.HasIndex(e => e.AddressId)
                    .HasName("AddressId");

                entity.HasIndex(e => e.AddressTypeId)
                    .HasName("AddressTypeId");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.IsDefaultDeliveryLocation).HasColumnType("tinyint(1)");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Tbluseraddressmapping)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("tbluseraddressmapping_ibfk_2");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Tbluseraddressmapping)
                    .HasForeignKey(d => d.AddressTypeId)
                    .HasConstraintName("tbluseraddressmapping_ibfk_3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbluseraddressmapping)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tbluseraddressmapping_ibfk_1");
            });

            modelBuilder.Entity<Tbluserdetail>(entity =>
            {
                entity.ToTable("tbluserdetail");

                entity.HasIndex(e => e.UserId)
                    .HasName("tbluserdetail_ibfk_1");

                entity.Property(e => e.AlternateEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateNumber)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Dob).HasColumnName("DOB");

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerPancardNo)
                    .HasColumnName("OwnerPANCardNo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OwnersAadharCardNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbluserdetail)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tbluserdetail_ibfk_1");
            });

            modelBuilder.Entity<Tbluserdoument>(entity =>
            {
                entity.ToTable("tbluserdoument");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.HasIndex(e => e.DocumentTypeId)
                    .HasName("DocumentTypeId");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.DocImagePath)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsApproved).HasColumnType("tinyint(1)");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Tbluserdoument)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("tbluserdoument_ibfk_2");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Tbluserdoument)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .HasConstraintName("tbluserdoument_ibfk_3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbluserdoument)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tbluserdoument_ibfk_1");
            });

            modelBuilder.Entity<Tbluserrolemapping>(entity =>
            {
                entity.ToTable("tbluserrolemapping");

                entity.HasIndex(e => e.RoleId)
                    .HasName("RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Tbluserrolemapping)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("tbluserrolemapping_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tbluserrolemapping)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tbluserrolemapping_ibfk_2");
            });

            modelBuilder.Entity<Tblusersubmitedanswer>(entity =>
            {
                entity.ToTable("tblusersubmitedanswer");

                entity.HasIndex(e => e.OptionId)
                    .HasName("OptionId");

                entity.HasIndex(e => e.QuestionId)
                    .HasName("QuestionId");

                entity.HasIndex(e => e.TestId)
                    .HasName("TestId");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.IsMatched).HasColumnType("tinyint(1)");

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.Tblusersubmitedanswer)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("tblusersubmitedanswer_ibfk_2");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Tblusersubmitedanswer)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("tblusersubmitedanswer_ibfk_1");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Tblusersubmitedanswer)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("tblusersubmitedanswer_ibfk_4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tblusersubmitedanswer)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tblusersubmitedanswer_ibfk_3");
            });

            modelBuilder.Entity<Tblusertype>(entity =>
            {
                entity.ToTable("tblusertype");

                entity.HasIndex(e => e.ClientId)
                    .HasName("ClientId");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnType("tinyint(1)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
