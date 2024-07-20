using System;
using InsurTech.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    [DbContext(typeof(InsurtechContext))]
    partial class InsurtechContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InsurTech.Core.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArticleImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("MinLength", 50);

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("MinLength", 3);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Health insurance covers medical expenses like doctor visits, hospital stays, and medications. We offer plans for individuals, families, and businesses, including short-term and supplemental options.",
                            Name = "HealthInsurance"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Home insurance protects your home and belongings from risks like fire, theft, and natural disasters. Our plans cover repairs, replacements, and living expenses, ensuring peace of mind for homeowners.",
                            Name = "HomeInsurance"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Motor insurance covers your vehicle against accidents, theft, and damage. Our policies offer comprehensive protection, including liability, collision, and personal injury coverage, ensuring peace of mind on the road.",
                            Name = "MotorInsurance"
                        });
                });

            modelBuilder.Entity("InsurTech.Core.Entities.FAQ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("MinLength", 3);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("MinLength", 3);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FAQs");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("InsurancePlanId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InsurancePlanId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)")
                        .HasAnnotation("RegularExpression", "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IsApprove")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasAnnotation("RegularExpression", "^01(0|1|2|5)[0-9]{8}$");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasAnnotation("MinLength", 3)
                        .HasAnnotation("RegularExpression", "^[a-zA-Z][a-zA-Z0-9]*$");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("AppUser");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4d5a70ac-0bda-4b9a-b511-d529a92dac52",

                            Email = "asmaa_ash@gmail.com",
                            EmailConfirmed = true,
                            IsApprove = 1,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            Name = "Asmaa Ashraf",
                            NormalizedEmail = "ASMAA_ASH@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEIVS4LkswPwq8+aOBZKRPRkJdmdtOqXVSxPQwiAieOIlFyHF4HG2OQWeqAYTI05HWA==",
                            PhoneNumber = "01211236779",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c6aaa0a8-8848-42ca-a816-2d95f089c50e",

                            TwoFactorEnabled = false,
                            UserName = "Admin",
                            UserType = 2
                        });
                });

            modelBuilder.Entity("InsurTech.Core.Entities.InsurancePlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AvailableInsurance")
                        .HasColumnType("bit");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<decimal>("Quotation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("YearlyCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.ToTable("InsurancePlans");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.RequestQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("UserRequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRequestId");

                    b.HasIndex("QuestionId", "UserRequestId")
                        .IsUnique();

                    b.ToTable("RequestQuestions");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.UserRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("InsurancePlanId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<decimal>("Quotation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("YearlyCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InsurancePlanId");

                    b.ToTable("Requests");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Company",
                            NormalizedName = "COMPANY"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Identity.Company", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.Identity.AppUser");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Company");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Identity.Customer", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.Identity.AppUser");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("NationalID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HealthInsurancePlan", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.InsurancePlan");

                    b.Property<decimal>("ClinicsCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DentalCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HospitalizationAndSurgery")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MedicalNetwork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OpticalCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("HealthInsurancePlans");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HomeInsurancePlan", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.InsurancePlan");

                    b.Property<decimal>("AttemptedTheft")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FiresAndExplosion")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GlassBreakage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NaturalHazard")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WaterDamage")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("HomeInsurancePlans");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.MotorInsurancePlan", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.InsurancePlan");

                    b.Property<decimal>("LegalExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OwnDamage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PersonalAccident")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Theft")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ThirdPartyLiability")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("MotorInsurancePlans");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HealthPlanRequest", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.UserRequest");

                    b.Property<decimal>("ClinicsCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DentalCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HospitalizationAndSurgery")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MedicalNetwork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OpticalCoverage")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("HealthPlanRequests");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HomePlanRequest", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.UserRequest");

                    b.Property<decimal>("AttemptedTheft")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FiresAndExplosion")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GlassBreakage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NaturalHazard")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WaterDamage")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("HomePlanRequests");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.MotorPlanRequest", b =>
                {
                    b.HasBaseType("InsurTech.Core.Entities.UserRequest");

                    b.Property<decimal>("LegalExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OwnDamage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PersonalAccident")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Theft")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ThirdPartyLiability")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("MotorPlanRequests");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Article", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.FAQ", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Feedback", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.Customer", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("InsurTech.Core.Entities.InsurancePlan", "InsurancePlan")
                        .WithMany("Feedbacks")
                        .HasForeignKey("InsurancePlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("InsurancePlan");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.InsurancePlan", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Category", "Category")
                        .WithMany("InsurancePlans")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsurTech.Core.Entities.Identity.Company", "Company")
                        .WithMany("InsurancePlans")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Notification", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Question", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Category", "Category")
                        .WithMany("QuestionPlans")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.RequestQuestion", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Question", "Question")
                        .WithMany("RequestQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsurTech.Core.Entities.UserRequest", "UserRequest")
                        .WithMany("RequestQuestions")
                        .HasForeignKey("UserRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("UserRequest");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.UserRequest", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsurTech.Core.Entities.InsurancePlan", "InsurancePlan")
                        .WithMany("Requests")
                        .HasForeignKey("InsurancePlanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("InsurancePlan");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HealthInsurancePlan", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.InsurancePlan", null)
                        .WithOne()
                        .HasForeignKey("InsurTech.Core.Entities.HealthInsurancePlan", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HomeInsurancePlan", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.InsurancePlan", null)
                        .WithOne()
                        .HasForeignKey("InsurTech.Core.Entities.HomeInsurancePlan", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.MotorInsurancePlan", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.InsurancePlan", null)
                        .WithOne()
                        .HasForeignKey("InsurTech.Core.Entities.MotorInsurancePlan", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HealthPlanRequest", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.UserRequest", null)
                        .WithOne()
                        .HasForeignKey("InsurTech.Core.Entities.HealthPlanRequest", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.HomePlanRequest", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.UserRequest", null)
                        .WithOne()
                        .HasForeignKey("InsurTech.Core.Entities.HomePlanRequest", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.MotorPlanRequest", b =>
                {
                    b.HasOne("InsurTech.Core.Entities.UserRequest", null)
                        .WithOne()
                        .HasForeignKey("InsurTech.Core.Entities.MotorPlanRequest", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Category", b =>
                {
                    b.Navigation("InsurancePlans");

                    b.Navigation("QuestionPlans");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.InsurancePlan", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Question", b =>
                {
                    b.Navigation("RequestQuestions");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.UserRequest", b =>
                {
                    b.Navigation("RequestQuestions");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Identity.Company", b =>
                {
                    b.Navigation("InsurancePlans");
                });

            modelBuilder.Entity("InsurTech.Core.Entities.Identity.Customer", b =>
                {
                    b.Navigation("Feedbacks");
                });
#pragma warning restore 612, 618
        }
    }
}

