using Enitities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enitities
{
    public class DataContext : DbContext
    {
        
        public DbSet<Gift> gifts { get; set; }
        public DataContext()
        { }

        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
            Database.EnsureCreated();
            if (!gifts.Any())
            {
                gifts.Add(new Gift { Title = "Øreringe", BoyGift = false, CreationDate = DateTime.Now, Description = "Det her er flotte øreringe", GirlGift = true });
                gifts.Add(new Gift { Title = "Xbox", BoyGift = true, CreationDate = DateTime.Now, Description = "xbox 360", GirlGift = false });
                gifts.Add(new Gift { Title = "FidgetSpinner", BoyGift = true, CreationDate = DateTime.Now, Description = "Fidget the midget", GirlGift = true });
                gifts.Add(new Gift { Title = "kæphest", BoyGift = false, CreationDate = DateTime.Now, Description = "hyp hyp", GirlGift = true });
                SaveChanges();
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
