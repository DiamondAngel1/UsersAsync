using Microsoft.EntityFrameworkCore;

namespace MyMvcApp.Data{
    public class Contexta : DbContext{
        public Contexta(){}
        public Contexta(DbContextOptions<Contexta> options)
            : base(options) { }
        
        public DbSet<User> Users { get; set; }
    }
}