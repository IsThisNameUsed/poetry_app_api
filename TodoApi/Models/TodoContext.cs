using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public DbContextOptions<TodoContext> options;
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            this.options = options;
        }

        public TodoContext? CloneContext()
        {
            return MemberwiseClone() as TodoContext;
        }

        public DbSet<PoemItem> PoemItem { get; set; } = null!;
    }
}
