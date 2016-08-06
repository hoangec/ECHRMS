namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyInsuranceRatePercent = c.Double(nullable: false),
                        LabaratorInsuranceRatePercent = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identity_IdentityNo = c.String(),
                        Identity_DateOfIssue = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Nationality = c.String(),
                        MaritalStatus = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        BirthDay = c.DateTime(precision: 7, storeType: "datetime2"),
                        Photo = c.Binary(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        JoinedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TerminatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        PositionId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Departement = c.String(nullable: false),
                        MadatoryInsurance = c.Double(),
                        VoluntaryInsurance = c.Double(),
                        MadotoryInsuranceDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Salary = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.PositionId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ContractNo = c.String(),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ContractAttachFile = c.String(),
                        Remark = c.String(),
                        ContractTypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContractTypes", t => t.ContractTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ContractTypeId);
            
            CreateTable(
                "dbo.ContractTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractTypeName = c.String(),
                        Duration = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeSalaryComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        SalaryComponentId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Remark = c.String(),
                        ApplyDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndApplyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SalaryComponents", t => t.SalaryComponentId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.SalaryComponentId);
            
            CreateTable(
                "dbo.SalaryComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComponentName = c.String(),
                        IsExtra = c.Boolean(nullable: false),
                        SalaryPayFrequency = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OldCompanyName = c.String(),
                        OldPositionName = c.String(),
                        OldDepartement = c.String(),
                        OldJoinedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OldSalary = c.Double(nullable: false),
                        OldInsurance = c.Double(nullable: false),
                        Reason = c.String(),
                        TransferDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ExperienceYears = c.String(),
                        AttachFile = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Insurances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InsuranceNo = c.String(),
                        DateOfIssue = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsMandatory = c.Boolean(nullable: false),
                        Values = c.Double(),
                        AttachFile = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                        InsuranceRate = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Terminations",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Reason = c.String(),
                        TerminationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Note = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EducationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EducationId);
            
            CreateTable(
                "dbo.EmployeeEducations",
                c => new
                    {
                        EmployeeEducationId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        EducationId = c.Int(nullable: false),
                        UniversityName = c.String(nullable: false),
                        Major = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.EmployeeEducationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Educations", t => t.EducationId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.EducationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfilePicUrl = c.String(),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastLoginTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Activated = c.Boolean(nullable: false),
                        RoleId = c.String(),
                        CompaniesRole = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.EmployeeEducations", "EducationId", "dbo.Educations");
            DropForeignKey("dbo.EmployeeEducations", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Terminations", "Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Insurances", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Experiences", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeSalaryComponents", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeSalaryComponents", "SalaryComponentId", "dbo.SalaryComponents");
            DropForeignKey("dbo.Contracts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Contracts", "ContractTypeId", "dbo.ContractTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.EmployeeEducations", new[] { "EducationId" });
            DropIndex("dbo.EmployeeEducations", new[] { "EmployeeId" });
            DropIndex("dbo.Terminations", new[] { "Id" });
            DropIndex("dbo.Insurances", new[] { "EmployeeId" });
            DropIndex("dbo.Experiences", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeSalaryComponents", new[] { "SalaryComponentId" });
            DropIndex("dbo.EmployeeSalaryComponents", new[] { "EmployeeId" });
            DropIndex("dbo.Contracts", new[] { "ContractTypeId" });
            DropIndex("dbo.Contracts", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EmployeeEducations");
            DropTable("dbo.Educations");
            DropTable("dbo.Terminations");
            DropTable("dbo.Positions");
            DropTable("dbo.Insurances");
            DropTable("dbo.Experiences");
            DropTable("dbo.SalaryComponents");
            DropTable("dbo.EmployeeSalaryComponents");
            DropTable("dbo.ContractTypes");
            DropTable("dbo.Contracts");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
        }
    }
}
