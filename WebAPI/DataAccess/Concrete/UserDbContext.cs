using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DataAccess.Concrete
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Tenant> Tenants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedNever(); // Otomatik değer üretme, ben göndereceğim demek.
        }

    }
}   