using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLike>()
                .HasKey(k=>new {k.SourceUserId, k.LikedUserId});

            builder.Entity<UserLike>()
                .HasOne(s=>s.SourceUser) // This means UserLike has one to Many mapping between SourceUser to LikeUser
                .WithMany(l=>l.LikedUsers)// SourceUser is parent and LikedUser is child here, Ek User both User ko like kr skta h
                .HasForeignKey(s=>s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade); // While working with SQL CAscade ki jgh Noaction hoga. SourceUserId is acting like a friegn key to Likes table, Jb sourceUser delte hoga then uska Likes bhi delte ho jayega
            
            // Other side of relationship
            builder.Entity<UserLike>()
                .HasOne(s=>s.LikedUser) 
                .WithMany(l=>l.LikedByUsers)
                .HasForeignKey(s=>s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Message>()
                .HasOne(u=>u.Recipient)
                .WithMany(m=>m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict); // because we dont want to remove messages if the other party has  deleted the messages by himself/herself

            // other side senders side
            builder.Entity<Message>()
                .HasOne(u=>u.Sender)
                .WithMany(m=>m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

        }

        //public DbSet<Photo> Photos { get; set; }  X wrong
        // we can create a Dbset of Photos but we need phots inside specific User photo Collection not 
        // like it will be Independently called like a User from DataBase
    }
}