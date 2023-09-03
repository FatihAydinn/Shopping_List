using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping_List.Models;

namespace Shopping_List.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
    : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ListDetails>()
            //.HasOne(p => p.ListHeaders)
            //.WithMany()
            //.HasForeignKey(p => p.HeaderId)
            //.OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ListDetails>().HasOne(x => x.DetailsId);
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductTilte = "Soğan" , ProductDescription = "Soğan (Allium cepa L., Latince cepa 'soğan'), aynı zamanda kuru soğan veya soğan olarak da bilinen Allium cinsinin en yaygın olarak yetiştirilen bir sebze türüdür. Arpacık soğanı soğan'nın bir botanik çeşididir. 2010 yılına kadar arpacık soğanı ayrı bir tür olarak sınıflandırıldı.",ImageUrl = "https://spiceworldinc.com/wp-content/uploads/2022/06/SW-Onion-Health-Hero.jpg", ProductCategory = "Manav" },
                new Product { ProductId = 2 , ProductTilte = "Patates", ProductDescription = "Patates (Solanum tuberosum), patlıcangiller (Solanaceae) familyasından yumruları yenen otsu bitki türüdür. Patates sözcüğü Amerika yerlilerinin dilinden İspanyolca aracılığıyla çeşitli Avrupa dillerine geçmiş, Türkçeye İtalyanca ve Yunancadan girmiştir. Türkçe eş anlamlısı olarak çisil sözcüğü bulunmaktadır.", ImageUrl = "https://www.formsante.com.tr/wp-content/uploads/2021/02/wsi-imageoptim-beyaz-patates-ve-tatli-patates-1200x675.jpg", ProductCategory = "Manav" },
                new Product { ProductId = 3, ProductTilte = "Makarna", ProductDescription = "Makarna glüten oranı yüksek durum buğdayından elde edilen irmikten yapılır. Un ve/veya irmik (semolina) ile üretilen, sayısız şekil ve büyüklükte ürünlere verilen genel bir isimdir. Genellikle 100 ölçek buğday irmiğine, 28 ölçek su katılarak yoğurulan makarna hamuru orta sertlikte bir hamurdur.", ImageUrl = "https://foodiosity.com/wp-content/uploads/2022/03/raw-pasta-1.jpg", ProductCategory = "Market" },
                new Product { ProductId = 4, ProductTilte = "Yağ", ProductDescription = "Yağ, oda sıcaklığında yüksek viskoziteye sahip, yüksek miktarda karbon ve hidrojen içeren, suyla karışmayan ancak diğer yağlarla kolayca karışabilen maddelerdir. Yağlar yiyecek, yakıt, boya, makine sanayii dâhil birçok değişik amaçla kullanılırlar." , ImageUrl = "https://post.medicalnewstoday.com/wp-content/uploads/sites/3/2020/02/324844_2200-1200x628.jpg", ProductCategory = "Market" },
                new Product { ProductId = 5, ProductTilte = "Çaydanlık", ProductDescription = "Çaydanlık, çay yapmada kullanılan, genelde iki parçalı ve metalden yapılma, bazen demliği porselen olan pişirme gereci. Çaydanlık ilk başlarda tek parça halinde kullanılmakta iken süreç içerisinde günlük ihtiyaçların giderilmesine yönelik olarak, alt kısmı daha büyük olacak şekilde iki parçalı hale gelmiştir.", ImageUrl = "https://www.acarhome.com/petra-pembe-emaye-caydanlik-takimi-caydanlik-acar-619527-45-B.jpg", ProductCategory = "Züccaciye" },
                new Product { ProductId = 6 , ProductTilte = "Fincan", ProductDescription = "Kahve, çay, süt, içmek için kullanılan ve çeşitli ölçülerde porselenden, çiniden, camdan, tahtadan yapılan küçük kaplar.", ImageUrl = "https://cdn.karaca.com/image/cdndata/185/202111/153.03.06.7859/8697918957360-494.jpg", ProductCategory = "Züccaciye" }
                );
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ListHeaders> ListHeaders { get; set; }
        public DbSet<ListDetails> ListDetails { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
    }
}

