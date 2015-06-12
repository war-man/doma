namespace doma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DiaChi", c => c.String());
            AddColumn("dbo.AspNetUsers", "SoDienThoai", c => c.String());
            AddColumn("dbo.AspNetUsers", "HoTen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "HoTen");
            DropColumn("dbo.AspNetUsers", "SoDienThoai");
            DropColumn("dbo.AspNetUsers", "DiaChi");
        }
    }
}
