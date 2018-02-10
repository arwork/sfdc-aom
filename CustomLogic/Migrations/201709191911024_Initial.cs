namespace CustomLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RelationshipObject", "RelationshipMetaId", "dbo.RelationshipMeta");
            DropForeignKey("dbo.RelationshipMeta", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropForeignKey("dbo.AomFieldMeta", "AomMetaId", "dbo.AomMeta");
            DropForeignKey("dbo.AomFieldMeta", "FieldTypeId", "dbo.FieldTypes");
            DropIndex("dbo.RelationshipMeta", new[] { "RelationshipTypeId" });
            DropIndex("dbo.RelationshipObject", new[] { "RelationshipMetaId" });
            AddColumn("dbo.AomFieldMeta", "Display", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AddColumn("dbo.AomMeta", "Display", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AddColumn("dbo.RelationshipMeta", "Name", c => c.String(nullable: false, maxLength: 150, unicode: false));
            AddForeignKey("dbo.RelationshipObject", "FkAomFieldObjectId", "dbo.AomFieldMeta", "ID");
            AddForeignKey("dbo.AomFieldMeta", "AomMetaId", "dbo.AomMeta", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AomFieldMeta", "FieldTypeId", "dbo.FieldTypes", "ID", cascadeDelete: true);
            DropColumn("dbo.RelationshipMeta", "RelationshipTypeId");
            DropTable("dbo.RelationshipTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RelationshipTypes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RelationshipMeta", "RelationshipTypeId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.AomFieldMeta", "FieldTypeId", "dbo.FieldTypes");
            DropForeignKey("dbo.AomFieldMeta", "AomMetaId", "dbo.AomMeta");
            DropForeignKey("dbo.RelationshipObject", "FkAomFieldObjectId", "dbo.AomFieldMeta");
            DropColumn("dbo.RelationshipMeta", "Name");
            DropColumn("dbo.AomMeta", "Display");
            DropColumn("dbo.AomFieldMeta", "Display");
            CreateIndex("dbo.RelationshipObject", "RelationshipMetaId");
            CreateIndex("dbo.RelationshipMeta", "RelationshipTypeId");
            AddForeignKey("dbo.AomFieldMeta", "FieldTypeId", "dbo.FieldTypes", "ID");
            AddForeignKey("dbo.AomFieldMeta", "AomMetaId", "dbo.AomMeta", "ID");
            AddForeignKey("dbo.RelationshipMeta", "RelationshipTypeId", "dbo.RelationshipTypes", "ID");
            AddForeignKey("dbo.RelationshipObject", "RelationshipMetaId", "dbo.RelationshipMeta", "ID");
        }
    }
}
