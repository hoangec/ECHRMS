namespace HNGHRMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_company_pros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "NumberCodeStarRange", c => c.Long(nullable: false));
            AddColumn("dbo.Companies", "NumberCodeEndRange", c => c.Long(nullable: false));
            AddColumn("dbo.Companies", "CompanyCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "CompanyCode");
            DropColumn("dbo.Companies", "NumberCodeEndRange");
            DropColumn("dbo.Companies", "NumberCodeStarRange");
        }
    }
}
