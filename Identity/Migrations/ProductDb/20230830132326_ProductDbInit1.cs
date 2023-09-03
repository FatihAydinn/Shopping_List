using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shopping_List.Migrations.ProductDb
{
    /// <inheritdoc />
    public partial class ProductDbInit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListHeaders",
                columns: table => new
                {
                    HeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListHeaders", x => x.HeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTilte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    ListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_ListHeaders_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "ListHeaders",
                        principalColumn: "HeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListDetails",
                columns: table => new
                {
                    DetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    ProductTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListDetails", x => x.DetailsId);
                    table.ForeignKey(
                        name: "FK_ListDetails_ShoppingLists_ListId",
                        column: x => x.ListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "ListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "ImageUrl", "ProductCategory", "ProductDescription", "ProductTilte" },
                values: new object[,]
                {
                    { 1, "https://spiceworldinc.com/wp-content/uploads/2022/06/SW-Onion-Health-Hero.jpg", "Manav", "Soğan (Allium cepa L., Latince cepa 'soğan'), aynı zamanda kuru soğan veya soğan olarak da bilinen Allium cinsinin en yaygın olarak yetiştirilen bir sebze türüdür. Arpacık soğanı soğan'nın bir botanik çeşididir. 2010 yılına kadar arpacık soğanı ayrı bir tür olarak sınıflandırıldı.", "Soğan" },
                    { 2, "https://www.formsante.com.tr/wp-content/uploads/2021/02/wsi-imageoptim-beyaz-patates-ve-tatli-patates-1200x675.jpg", "Manav", "Patates (Solanum tuberosum), patlıcangiller (Solanaceae) familyasından yumruları yenen otsu bitki türüdür. Patates sözcüğü Amerika yerlilerinin dilinden İspanyolca aracılığıyla çeşitli Avrupa dillerine geçmiş, Türkçeye İtalyanca ve Yunancadan girmiştir. Türkçe eş anlamlısı olarak çisil sözcüğü bulunmaktadır.", "Patates" },
                    { 3, "https://foodiosity.com/wp-content/uploads/2022/03/raw-pasta-1.jpg", "Market", "Makarna glüten oranı yüksek durum buğdayından elde edilen irmikten yapılır. Un ve/veya irmik (semolina) ile üretilen, sayısız şekil ve büyüklükte ürünlere verilen genel bir isimdir. Genellikle 100 ölçek buğday irmiğine, 28 ölçek su katılarak yoğurulan makarna hamuru orta sertlikte bir hamurdur.", "Makarna" },
                    { 4, "https://post.medicalnewstoday.com/wp-content/uploads/sites/3/2020/02/324844_2200-1200x628.jpg", "Market", "Yağ, oda sıcaklığında yüksek viskoziteye sahip, yüksek miktarda karbon ve hidrojen içeren, suyla karışmayan ancak diğer yağlarla kolayca karışabilen maddelerdir. Yağlar yiyecek, yakıt, boya, makine sanayii dâhil birçok değişik amaçla kullanılırlar.", "Yağ" },
                    { 5, "https://www.acarhome.com/petra-pembe-emaye-caydanlik-takimi-caydanlik-acar-619527-45-B.jpg", "Züccaciye", "Çaydanlık, çay yapmada kullanılan, genelde iki parçalı ve metalden yapılma, bazen demliği porselen olan pişirme gereci. Çaydanlık ilk başlarda tek parça halinde kullanılmakta iken süreç içerisinde günlük ihtiyaçların giderilmesine yönelik olarak, alt kısmı daha büyük olacak şekilde iki parçalı hale gelmiştir.", "Çaydanlık" },
                    { 6, "https://cdn.karaca.com/image/cdndata/185/202111/153.03.06.7859/8697918957360-494.jpg", "Züccaciye", "Kahve, çay, süt, içmek için kullanılan ve çeşitli ölçülerde porselenden, çiniden, camdan, tahtadan yapılan küçük kaplar.", "Fincan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListDetails_ListId",
                table: "ListDetails",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_HeaderId",
                table: "ShoppingLists",
                column: "HeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListDetails");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "ListHeaders");
        }
    }
}
