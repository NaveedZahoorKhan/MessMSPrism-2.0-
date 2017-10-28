namespace MessMSPrism.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        ShifId = c.Int(nullable: false),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Cnic = c.String(maxLength: 4000),
                        RoomNo = c.Int(nullable: false),
                        QrPath = c.String(maxLength: 4000),
                        ImgPath = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Student_Id", "dbo.Students");
            DropIndex("dbo.Attendances", new[] { "Student_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Attendances");
        }
    }
}
