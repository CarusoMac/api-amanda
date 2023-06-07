using api_amanda.DTOs;
using api_amanda.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Diagnostics.CodeAnalysis;
using NetTopologySuite;

namespace api_amanda {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options) {
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
            {
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.UseSerialColumns();
        }

        

        public DbSet<CsvRecordDTO> Records { get; set; }
        public DbSet<CsvFileDTO> CsvFiles { get; set; }
        public DbSet<CsvBtsDTO> BtsCoordinates { get; set; }
      
    }
}
