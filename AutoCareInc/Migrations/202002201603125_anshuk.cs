namespace AutoCareInc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anshuk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceItems", "InvoiceID", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoiceItems", "InvoiceID");
            AddForeignKey("dbo.InvoiceItems", "InvoiceID", "dbo.Invoices", "InvoiceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "InvoiceID", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceID" });
            DropColumn("dbo.InvoiceItems", "InvoiceID");
        }
    }
}
