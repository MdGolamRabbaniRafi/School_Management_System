namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.String(nullable: false, maxLength: 128, unicode: false));
            AlterColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Users", "BloodGroup", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.Users", "UserType", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserType", c => c.String());
            AlterColumn("dbo.Users", "BloodGroup", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "Id");
        }
    }
}
