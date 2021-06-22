using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LORAPI.Models
{
    public class LORContext : DbContext
    {
        // The preceding code creates a DbSet property for each entity set. In EF terminology. An entity set typically corresponds to a database table.
        // An entity corresponds to a row in the table.
        public LORContext(DbContextOptions<LORContext> options) : base(options)
        {   

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<StatusList> Status { get; set; }
        public DbSet<FriendsList> FriendsLists { get; set; }
        public DbSet<UsersLibrary> UsersLibrarys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(b => b.UserID).HasName("PKUserID");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Game>().HasKey(b => b.GameID).HasName("PKGameID");
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<StatusList>().HasKey(b => b.StatusID).HasName("PKStatusID");
            modelBuilder.Entity<StatusList>().ToTable("StatusList");
            modelBuilder.Entity<FriendsList>().HasKey(b => b.UserFriendID).HasName("PKFriendsListID");
            modelBuilder.Entity<FriendsList>().ToTable("FriendsList");
            modelBuilder.Entity<UsersLibrary>().HasKey(b => b.UserGameID).HasName("PKUsersLibraryID");
            modelBuilder.Entity<UsersLibrary>().ToTable("UsersLibrary");
        }
    }
}
