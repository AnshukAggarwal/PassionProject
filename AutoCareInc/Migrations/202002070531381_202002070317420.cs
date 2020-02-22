namespace AutoCareInc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202002070317420 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceItems", "Invoice_InvoiceID", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "Invoice_InvoiceID" });
            DropColumn("dbo.InvoiceItems", "Invoice_InvoiceID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceItems", "Invoice_InvoiceID", c => c.Int());
            CreateIndex("dbo.InvoiceItems", "Invoice_InvoiceID");
            AddForeignKey("dbo.InvoiceItems", "Invoice_InvoiceID", "dbo.Invoices", "InvoiceID");
        }
    }
}
