using Microsoft.EntityFrameworkCore;
using PeopleNotes.Classes;

namespace PeopleNotes.Data
{
    public class PeopleNotesContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=PeopleNotes;Integrated Security=True;");
        }
    }
}