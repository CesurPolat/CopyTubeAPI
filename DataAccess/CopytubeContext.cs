using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CopytubeContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"server=DESKTOP-3KGBMS3; Database=CopyTubeDb;Integrated Security=True");
            //optionsBuilder.UseSqlServer(@"server=.\SQLEXPRESS; Database=CopyTubeDb;Integrated Security=True;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
