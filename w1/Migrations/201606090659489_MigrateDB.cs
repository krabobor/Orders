namespace w1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nomenclatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Unit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Сomment = c.String(),
                        IsClose = c.Boolean(nullable: false),
                        Xml = c.Binary(),
                        NomenclatureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nomenclatures", t => t.NomenclatureId, cascadeDelete: true)
                .Index(t => t.NomenclatureId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        NomenclatureId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nomenclatures", t => t.NomenclatureId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: false)
                .Index(t => t.OrderId)
                .Index(t => t.NomenclatureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Sales", "NomenclatureId", "dbo.Nomenclatures");
            DropForeignKey("dbo.Orders", "NomenclatureId", "dbo.Nomenclatures");
            DropIndex("dbo.Sales", new[] { "NomenclatureId" });
            DropIndex("dbo.Sales", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "NomenclatureId" });
            DropTable("dbo.Sales");
            DropTable("dbo.Orders");
            DropTable("dbo.Nomenclatures");
        }
    }
}
