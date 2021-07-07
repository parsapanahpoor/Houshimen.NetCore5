using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class InitialFeatureProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ProductCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeature",
                columns: table => new
                {
                    FeatureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    FeatureTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeatureValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeature", x => x.FeatureID);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGallery",
                columns: table => new
                {
                    ProductGalleryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGallery", x => x.ProductGalleryId);
                    table.ForeignKey(
                        name: "FK_ProductGallery_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSelectedCategory",
                columns: table => new
                {
                    ProductSelectedCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ProductCategoriesProductCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSelectedCategory", x => x.ProductSelectedCategoryID);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategory_ProductCategories_ProductCategoriesProductCategoryId",
                        column: x => x.ProductCategoriesProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSelectedCategory_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProductTypeId",
                table: "Comment",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_ProductID",
                table: "ProductFeature",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGallery_ProductID",
                table: "ProductGallery",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategory_ProductCategoriesProductCategoryId",
                table: "ProductSelectedCategory",
                column: "ProductCategoriesProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedCategory_ProductID",
                table: "ProductSelectedCategory",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ProductType_ProductTypeId",
                table: "Comment",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "ProductTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ProductType_ProductTypeId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "ProductFeature");

            migrationBuilder.DropTable(
                name: "ProductGallery");

            migrationBuilder.DropTable(
                name: "ProductSelectedCategory");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ProductTypeId",
                table: "Comment");
        }
    }
}
