namespace AutoCareInc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerFname = c.String(),
                        CustomerLname = c.String(),
                        CustomerAddress = c.String(),
                        CustomerEmail = c.String(),
                        CustomerPhone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        InvoiceNotes = c.String(),
                        Customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceID)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID)
                .Index(t => t.Customer_CustomerID);
            
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        InvoiceItemID = c.Int(nullable: false, identity: true),
                        InvoiceItemName = c.String(),
                        InvoiceItemPrice = c.Double(nullable: false),
                        Invoice_InvoiceID = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceItemID)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceID)
                .Index(t => t.Invoice_InvoiceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.InvoiceItems", "Invoice_InvoiceID", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "Invoice_InvoiceID" });
            DropIndex("dbo.Invoices", new[] { "Customer_CustomerID" });
            DropTable("dbo.InvoiceItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
        }
    }
}
