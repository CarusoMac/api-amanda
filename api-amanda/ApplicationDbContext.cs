using api_amanda.DTOs;
using api_amanda.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Diagnostics.CodeAnalysis;

namespace api_amanda {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
            
            {
            modelBuilder.UseSerialColumns();
        }
        //public DbSet<Log> Zaznamy { get; set; }
        public DbSet<CsvRecordDTO> Records { get; set; }
        public DbSet<CsvFileDTO> CsvFiles { get; set; }
    }
}
