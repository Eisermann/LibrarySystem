using LibrarySystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LibrarySystem.Models.Book> Book { get; set; } = default!;
        public DbSet<LibrarySystem.Models.BookLoan> BookLoan { get; set; } = default!;
    }
}
