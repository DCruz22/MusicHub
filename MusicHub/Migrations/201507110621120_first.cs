namespace MusicHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friendships", "UserId_following", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Friendships", "UserId_following");
            CreateIndex("dbo.Projects", "UserId");
            AddForeignKey("dbo.Friendships", "UserId_following", "dbo.Users", "UserId", cascadeDelete: false);
            AddForeignKey("dbo.Projects", "UserId", "dbo.Users", "UserId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Friendships", "UserId_following", "dbo.Users");
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropIndex("dbo.Friendships", new[] { "UserId_following" });
            DropColumn("dbo.Projects", "UserId");
            DropColumn("dbo.Friendships", "UserId_following");
        }
    }
}
