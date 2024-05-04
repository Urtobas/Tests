using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tests.Models;

namespace Tests.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            try
            {
                //Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            catch
            {

            }
        }
        public DbSet<Test> Tests { get; set; }
        public DbSet<QuestionBlock> QuestionBlocks { get; set; }
        public DbSet<ProgramLanguage> ProgramLanguages { get; set; }
        public DbSet<DifficultyLevel> DifficultyLevels { get; set; }
        public DbSet<TestingUser> TestingUsers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
    }
}