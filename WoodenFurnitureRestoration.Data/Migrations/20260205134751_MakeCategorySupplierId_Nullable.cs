using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodenFurnitureRestoration.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeCategorySupplierId_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Addresses_AddressId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Customers_CustomerId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Restorations_RestorationId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Reviews_ReviewId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Shippings_ShippingId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTags_BlogPosts_BlogPostId",
                table: "BlogPostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTags_Tags_TagId",
                table: "BlogPostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_SupplierMaterials_SupplierMaterialId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Shippings_ShippingId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Invoices_InvoiceId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SupplierMaterials_SupplierMaterialId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Suppliers_SupplierId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingPayments_Payments_PaymentId",
                table: "ShippingPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingProducts_Products_ProductId",
                table: "ShippingProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Inventories_InventoryId",
                table: "Shippings");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Invoices_InvoiceId",
                table: "Shippings");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Orders_OrderId",
                table: "Shippings");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Products_ProductId",
                table: "Shippings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingTags_Products_ProductId",
                table: "ShippingTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingTags_Tags_TagId",
                table: "ShippingTags");

            migrationBuilder.DropForeignKey(
                name: "FK_supplierCategories_Categories_CategoryId",
                table: "supplierCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_supplierCategories_Suppliers_SupplierId",
                table: "supplierCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierMaterials_Shippings_ShippingId",
                table: "SupplierMaterials");

            migrationBuilder.DropTable(
                name: "InvoiceTag");

            migrationBuilder.DropTable(
                name: "OrderTag");

            migrationBuilder.DropTable(
                name: "PaymentTag");

            migrationBuilder.DropIndex(
                name: "IX_SupplierMaterials_ShippingId",
                table: "SupplierMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_supplierCategories",
                table: "supplierCategories");

            migrationBuilder.DropIndex(
                name: "IX_ShippingTags_ProductId",
                table: "ShippingTags");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_InventoryId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_InvoiceId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_OrderId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ShippingId",
                table: "SupplierMaterials");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShippingTags");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "supplierCategories",
                newName: "SupplierCategories");

            migrationBuilder.RenameIndex(
                name: "IX_supplierCategories_CategoryId",
                table: "SupplierCategories",
                newName: "IX_SupplierCategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Shippings",
                newName: "OrderId1");

            migrationBuilder.RenameIndex(
                name: "IX_Shippings_ProductId",
                table: "Shippings",
                newName: "IX_Shippings_OrderId1");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierEmail",
                table: "Suppliers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialSize",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialModel",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialImage",
                table: "SupplierMaterials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialColor",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialBrand",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RestorationServiceImage",
                table: "RestorationServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "RestorationImage",
                table: "Restorations",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingId",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingId",
                table: "BlogPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "BlogPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RestorationId",
                table: "BlogPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "BlogPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "BlogPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Addresses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierCategories",
                table: "SupplierCategories",
                columns: new[] { "SupplierId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CategoryId",
                table: "Tags",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_InvoiceId",
                table: "Tags",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_OrderId",
                table: "Tags",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PaymentId",
                table: "Tags",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductId",
                table: "Tags",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Addresses_AddressId",
                table: "BlogPosts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Customers_CustomerId",
                table: "BlogPosts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Restorations_RestorationId",
                table: "BlogPosts",
                column: "RestorationId",
                principalTable: "Restorations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Reviews_ReviewId",
                table: "BlogPosts",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Shippings_ShippingId",
                table: "BlogPosts",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTags_BlogPosts_BlogPostId",
                table: "BlogPostTags",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTags_Tags_TagId",
                table: "BlogPostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_SupplierMaterials_SupplierMaterialId",
                table: "Inventories",
                column: "SupplierMaterialId",
                principalTable: "SupplierMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Shippings_ShippingId",
                table: "Invoices",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SupplierMaterials_SupplierMaterialId",
                table: "Payments",
                column: "SupplierMaterialId",
                principalTable: "SupplierMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Suppliers_SupplierId",
                table: "Reviews",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingPayments_Payments_PaymentId",
                table: "ShippingPayments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingProducts_Products_ProductId",
                table: "ShippingProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Orders_OrderId1",
                table: "Shippings",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingTags_Tags_TagId",
                table: "ShippingTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCategories_Categories_CategoryId",
                table: "SupplierCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierCategories_Suppliers_SupplierId",
                table: "SupplierCategories",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Categories_CategoryId",
                table: "Tags",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Invoices_InvoiceId",
                table: "Tags",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Orders_OrderId",
                table: "Tags",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Payments_PaymentId",
                table: "Tags",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ProductId",
                table: "Tags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Addresses_AddressId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Customers_CustomerId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Restorations_RestorationId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Reviews_ReviewId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Shippings_ShippingId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTags_BlogPosts_BlogPostId",
                table: "BlogPostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostTags_Tags_TagId",
                table: "BlogPostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_SupplierMaterials_SupplierMaterialId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Shippings_ShippingId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SupplierMaterials_SupplierMaterialId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Suppliers_SupplierId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingPayments_Payments_PaymentId",
                table: "ShippingPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingProducts_Products_ProductId",
                table: "ShippingProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Orders_OrderId1",
                table: "Shippings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingTags_Tags_TagId",
                table: "ShippingTags");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCategories_Categories_CategoryId",
                table: "SupplierCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierCategories_Suppliers_SupplierId",
                table: "SupplierCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Categories_CategoryId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Invoices_InvoiceId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Orders_OrderId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Payments_PaymentId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ProductId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CategoryId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_InvoiceId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_OrderId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PaymentId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProductId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierCategories",
                table: "SupplierCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "SupplierCategories",
                newName: "supplierCategories");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierCategories_CategoryId",
                table: "supplierCategories",
                newName: "IX_supplierCategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "OrderId1",
                table: "Shippings",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Shippings_OrderId1",
                table: "Shippings",
                newName: "IX_Shippings_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierEmail",
                table: "Suppliers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialSize",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialModel",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialImage",
                table: "SupplierMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialColor",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialBrand",
                table: "SupplierMaterials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShippingId",
                table: "SupplierMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ShippingTags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Shippings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Shippings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RestorationServiceImage",
                table: "RestorationServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RestorationImage",
                table: "Restorations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "ShippingId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShippingId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestorationId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Addresses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_supplierCategories",
                table: "supplierCategories",
                columns: new[] { "SupplierId", "CategoryId" });

            migrationBuilder.CreateTable(
                name: "InvoiceTag",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTag", x => new { x.InvoiceId, x.TagId });
                    table.ForeignKey(
                        name: "FK_InvoiceTag_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTag",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTag", x => new { x.OrderId, x.TagId });
                    table.ForeignKey(
                        name: "FK_OrderTag_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTag",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTag", x => new { x.PaymentId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PaymentTag_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierMaterials_ShippingId",
                table: "SupplierMaterials",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingTags_ProductId",
                table: "ShippingTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_InventoryId",
                table: "Shippings",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_InvoiceId",
                table: "Shippings",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_OrderId",
                table: "Shippings",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTag_TagId",
                table: "InvoiceTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTag_TagId",
                table: "OrderTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTag_TagId",
                table: "PaymentTag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Addresses_AddressId",
                table: "BlogPosts",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Customers_CustomerId",
                table: "BlogPosts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Restorations_RestorationId",
                table: "BlogPosts",
                column: "RestorationId",
                principalTable: "Restorations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Reviews_ReviewId",
                table: "BlogPosts",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Shippings_ShippingId",
                table: "BlogPosts",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTags_BlogPosts_BlogPostId",
                table: "BlogPostTags",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostTags_Tags_TagId",
                table: "BlogPostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_SupplierMaterials_SupplierMaterialId",
                table: "Inventories",
                column: "SupplierMaterialId",
                principalTable: "SupplierMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Shippings_ShippingId",
                table: "Invoices",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Invoices_InvoiceId",
                table: "Payments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SupplierMaterials_SupplierMaterialId",
                table: "Payments",
                column: "SupplierMaterialId",
                principalTable: "SupplierMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Suppliers_SupplierId",
                table: "Reviews",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingPayments_Payments_PaymentId",
                table: "ShippingPayments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingProducts_Products_ProductId",
                table: "ShippingProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Inventories_InventoryId",
                table: "Shippings",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Invoices_InvoiceId",
                table: "Shippings",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Orders_OrderId",
                table: "Shippings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Products_ProductId",
                table: "Shippings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingTags_Products_ProductId",
                table: "ShippingTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingTags_Tags_TagId",
                table: "ShippingTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_supplierCategories_Categories_CategoryId",
                table: "supplierCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_supplierCategories_Suppliers_SupplierId",
                table: "supplierCategories",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierMaterials_Shippings_ShippingId",
                table: "SupplierMaterials",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id");
        }
    }
}
