namespace CustomLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AomFieldMeta",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AomMetaId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                        FieldTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AomMeta", t => t.AomMetaId)
                .ForeignKey("dbo.FieldTypes", t => t.FieldTypeId)
                .Index(t => t.AomMetaId)
                .Index(t => t.FieldTypeId);
            
            CreateTable(
                "dbo.AomFieldObject",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AomObjectId = c.Guid(nullable: false),
                        AomFieldMetaId = c.Guid(nullable: false),
                        Value = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AomFieldMeta", t => t.AomFieldMetaId)
                .ForeignKey("dbo.AomObject", t => t.AomObjectId, cascadeDelete: true)
                .Index(t => t.AomObjectId)
                .Index(t => t.AomFieldMetaId);
            
            CreateTable(
                "dbo.AomObject",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AomMetaId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AomMeta", t => t.AomMetaId, cascadeDelete: true)
                .Index(t => t.AomMetaId);
            
            CreateTable(
                "dbo.AomMeta",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RelationshipMeta",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PkAomMetaId = c.Guid(nullable: false),
                        FkAomMetaId = c.Guid(nullable: false),
                        FkAomFieldMetaId = c.Guid(nullable: false),
                        RelationshipTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AomFieldMeta", t => t.FkAomFieldMetaId)
                .ForeignKey("dbo.AomMeta", t => t.FkAomMetaId)
                .ForeignKey("dbo.AomMeta", t => t.PkAomMetaId)
                .ForeignKey("dbo.RelationshipTypes", t => t.RelationshipTypeId)
                .Index(t => t.PkAomMetaId)
                .Index(t => t.FkAomMetaId)
                .Index(t => t.FkAomFieldMetaId)
                .Index(t => t.RelationshipTypeId);
            
            CreateTable(
                "dbo.RelationshipObject",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        RelationshipMetaId = c.Guid(nullable: false),
                        PkAomFieldObjectId = c.Guid(nullable: false),
                        FkAomFieldObjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AomFieldObject", t => t.FkAomFieldObjectId)
                .ForeignKey("dbo.AomFieldObject", t => t.PkAomFieldObjectId)
                .ForeignKey("dbo.RelationshipMeta", t => t.RelationshipMetaId)
                .Index(t => t.RelationshipMetaId)
                .Index(t => t.PkAomFieldObjectId)
                .Index(t => t.FkAomFieldObjectId);
            
            CreateTable(
                "dbo.RelationshipTypes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FieldTypes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AomFieldMeta", "FieldTypeId", "dbo.FieldTypes");
            DropForeignKey("dbo.AomFieldMeta", "AomMetaId", "dbo.AomMeta");
            DropForeignKey("dbo.AomFieldObject", "AomObjectId", "dbo.AomObject");
            DropForeignKey("dbo.AomObject", "AomMetaId", "dbo.AomMeta");
            DropForeignKey("dbo.RelationshipMeta", "RelationshipTypeId", "dbo.RelationshipTypes");
            DropForeignKey("dbo.RelationshipObject", "RelationshipMetaId", "dbo.RelationshipMeta");
            DropForeignKey("dbo.RelationshipObject", "PkAomFieldObjectId", "dbo.AomFieldObject");
            DropForeignKey("dbo.RelationshipObject", "FkAomFieldObjectId", "dbo.AomFieldObject");
            DropForeignKey("dbo.RelationshipMeta", "PkAomMetaId", "dbo.AomMeta");
            DropForeignKey("dbo.RelationshipMeta", "FkAomMetaId", "dbo.AomMeta");
            DropForeignKey("dbo.RelationshipMeta", "FkAomFieldMetaId", "dbo.AomFieldMeta");
            DropForeignKey("dbo.AomFieldObject", "AomFieldMetaId", "dbo.AomFieldMeta");
            DropIndex("dbo.RelationshipObject", new[] { "FkAomFieldObjectId" });
            DropIndex("dbo.RelationshipObject", new[] { "PkAomFieldObjectId" });
            DropIndex("dbo.RelationshipObject", new[] { "RelationshipMetaId" });
            DropIndex("dbo.RelationshipMeta", new[] { "RelationshipTypeId" });
            DropIndex("dbo.RelationshipMeta", new[] { "FkAomFieldMetaId" });
            DropIndex("dbo.RelationshipMeta", new[] { "FkAomMetaId" });
            DropIndex("dbo.RelationshipMeta", new[] { "PkAomMetaId" });
            DropIndex("dbo.AomObject", new[] { "AomMetaId" });
            DropIndex("dbo.AomFieldObject", new[] { "AomFieldMetaId" });
            DropIndex("dbo.AomFieldObject", new[] { "AomObjectId" });
            DropIndex("dbo.AomFieldMeta", new[] { "FieldTypeId" });
            DropIndex("dbo.AomFieldMeta", new[] { "AomMetaId" });
            DropTable("dbo.FieldTypes");
            DropTable("dbo.RelationshipTypes");
            DropTable("dbo.RelationshipObject");
            DropTable("dbo.RelationshipMeta");
            DropTable("dbo.AomMeta");
            DropTable("dbo.AomObject");
            DropTable("dbo.AomFieldObject");
            DropTable("dbo.AomFieldMeta");
        }
    }
}
