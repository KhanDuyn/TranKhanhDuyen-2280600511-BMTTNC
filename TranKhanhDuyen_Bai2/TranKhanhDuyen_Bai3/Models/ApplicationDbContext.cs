using Microsoft.EntityFrameworkCore;

namespace TranKhanhDuyen_Bai3.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        { }
        //khai báo các bảng dữ liệu sử dụng trong DB
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

    }
}
