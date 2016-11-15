using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class TennisContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<PlayerTeamDivision> PlayerTeamDivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
            .HasMany(a => a.Roles)
            .WithMany(c => c.Players)
            .Map(x =>
            {
                x.MapLeftKey("PlayerId");
                x.MapRightKey("RoleId");
                x.ToTable("PlayerRoles");
            });

            modelBuilder.Entity<Game>()
            .HasRequired(c => c.Player1)
            .WithMany(c => c.Player1Games)
            .HasForeignKey(m => m.Player1Id)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
            .HasRequired(c => c.Player2)
            .WithMany(c => c.Player2Games)
            .HasForeignKey(m => m.Player2Id)
            .WillCascadeOnDelete(false);
        }
    }
}
