using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) 
        {

        }


        public DbSet<Value> Values { get; set; }  // "Values" is used as table name

        public DbSet<Activity> Activities { get; set; }

        // 通过配置的方法让dotnet ef 生成migration代码
        // 这样的缺点是需要设置Id，这对于几张table有relation时不是很方便。解决方法是Persistence下的Seed.cs
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Value>()
            .HasData(
                new Value {Id = 1, Name = "Value 101"},
                new Value {Id = 2, Name = "Value 102"},
                new Value {Id = 3, Name = "Value 103"}
            );
        }
    }
}
